using UnityEngine;

namespace SuisHack.SuisMenu
{
	public class MenuThing : MonoBehaviour
	{
		static MenuThing instance;
		bool displayGUI;

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.F11))
			{
				displayGUI = !displayGUI;
			}
		}

		internal static void Initialize()
		{
			if (instance == null)
			{
				var gameObject = new GameObject("SuisMenu");
				instance = gameObject.AddComponent<MenuThing>();
				DontDestroyOnLoad(gameObject);
			}
		}

		void OnGUI()
		{
			if (displayGUI)
			{
				Cursor.visible = true;
				GUILayout.BeginHorizontal();
				GUILayout.BeginVertical(GUI.skin.box);

				GUILayout.Label($"Target framerate {Application.targetFrameRate}");
				var desiredFPS = (int)GUILayout.HorizontalSlider(Application.targetFrameRate, 0, 240);
				if (desiredFPS != Application.targetFrameRate)
					Config.FPS_Limit.Value = desiredFPS;

				GUILayout.Label($"Vsync count {QualitySettings.vSyncCount}");
				var desiredVsync = (int)GUILayout.HorizontalSlider(QualitySettings.vSyncCount, 0, 4);
				if (desiredVsync != QualitySettings.vSyncCount)
					Config.Vsync_Count.Value = desiredVsync;

				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
		}
	}
}
