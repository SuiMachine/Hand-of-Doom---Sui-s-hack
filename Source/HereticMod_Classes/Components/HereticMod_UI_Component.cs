using UnityEngine;
using UnityEngine.UI;

namespace SuisHack.HereticMod_Classes.Components
{
	public class HereticMod_UI_Component : MonoBehaviour
	{
		public static HereticMod_UI_Component Instance;
		public RawImage GameScreen { get; private set; }
		public scr_renderTextureRaycast RenderTextureRaycast { get; private set; }
		public Camera GameCam { get; private set; }
		private RenderTexture newRenderTexture;


		void Start()
		{
			if (Instance != null)
			{
				Destroy(this);
				return;
			}

			Instance = this;

			Destroy(this.transform.Find("overlay").gameObject);
			Destroy(this.transform.Find("gameScreenBG").gameObject);
			Destroy(this.transform.Find("weaponAnimsHider").gameObject);
			Destroy(this.transform.Find("Image").gameObject);
			Destroy(this.transform.Find("mapButtons").gameObject);

			{
				var buttons = this.transform.Find("buttons").GetComponentsInChildren<Image>();
				var canvasRenderers = this.transform.Find("buttons").GetComponentsInChildren<CanvasRenderer>();

				foreach (var button in buttons)
					Destroy(button);

				foreach (var canvasRenderer in canvasRenderers)
					Destroy(canvasRenderer);
			}


			GameScreen = this.transform.Find("gameScreenHolder/gameScreen").GetComponent<RawImage>();
			GameCam = FindObjectOfType<scr_CameraFarClipHandler>().GetComponent<Camera>();
			ResizeUIs();
			GameCam.fieldOfView = Config.Heretic_FOV.Value;
		}

		void ResizeUIs()
		{
			GameScreen.transform.parent.GetComponent<RectTransform>().SetStrech();
			GameScreen.rectTransform.SetStrech();

			this.transform.Find("miniMapOrb").transform.position = new Vector3(0, -900, 0);
			FindObjectOfType<scr_minimap>().mapCamera.orthographicSize = 195f;
			var healthBottle = this.transform.Find("healthBottle");
			healthBottle.transform.position = new Vector3(healthBottle.transform.position.x, -700, 0);
			var miniInventory = this.transform.Find("MiniInventory");
			miniInventory.transform.position = new Vector3(miniInventory.transform.position.x, 800, 0);

			if (GameCam.targetTexture != null)
			{
				GameCam.targetTexture.Release();
				Destroy(GameCam.targetTexture);
			}

			if (!Config.Heretic_NativeResolutionRendering.Value)
			{
				int height = 180;
				GameCam.aspect = Screen.width * 1f / Screen.height;

				int width = Mathf.FloorToInt(height * 1.0f * GameCam.aspect);
				newRenderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32, 1);
				Plugin.LogMessage($"Calculated render texture to be {width}, {height} with aspect ratio of {GameCam.aspect}");
				newRenderTexture.filterMode = FilterMode.Point;
			}
			else
			{
				newRenderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
				newRenderTexture.filterMode = FilterMode.Bilinear;
			}

			GameCam.targetTexture = newRenderTexture;
			GameScreen.texture = newRenderTexture;
		}

		void OnDestroy()
		{
			if (newRenderTexture != null)
			{
				newRenderTexture.Release();
				Destroy(newRenderTexture);
			}
		}
	}
}
