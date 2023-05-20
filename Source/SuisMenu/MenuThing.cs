using System;
using UnityEngine;

namespace SuisHack.SuisMenu
{
	public class MenuThing : MonoBehaviour
	{
		static MenuThing instance;
		bool displayGUI;
		bool oldCursorVisible;
		CursorLockMode oldCursorLock;

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.F11))
			{
				displayGUI = !displayGUI;
/*				if (displayGUI)
				{
					oldCursorVisible = Cursor.visible;
					oldCursorLock = Cursor.lockState;

					Cursor.lockState = CursorLockMode.None;
				}
				else
				{
					Cursor.visible = oldCursorVisible;
					Cursor.lockState = oldCursorLock;
				}*/
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

				var oldValue = Application.targetFrameRate;

				GUILayout.Label($"Target framerate {oldValue}");
				var newValue = (int)GUILayout.HorizontalSlider(oldValue, 0, 120);
				if (newValue != oldValue)
					Application.targetFrameRate = (int)newValue;
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
		}
	}
}
