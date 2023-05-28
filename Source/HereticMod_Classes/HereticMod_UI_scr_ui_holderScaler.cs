using HarmonyLib;
using UnityEngine;

namespace SuisHack.HereticMod_Classes
{
	public static class HereticMod_UI_scr_ui_holderScaler
	{
		public static bool Initialized;

		public static void InitializeHook()
		{
			if (Initialized)
				return;

			var sourceClassType = typeof(scr_ui_holderScaler);
			var sourceMethod = sourceClassType.GetMethod("Start", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

			var targetClassType = typeof(HereticMod_UI_scr_ui_holderScaler);
			var targetMethod = new HarmonyMethod(targetClassType.GetMethod(nameof(StartPostfix), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static));

			Plugin.HarmonyInst.Patch(sourceMethod, postfix: targetMethod);
			Initialized = true;
		}

		private static void StartPostfix(scr_ui_holderScaler __instance)
		{
			__instance.gameObject.AddComponent<Components.HereticMod_UI_Component>();
			MonoBehaviour.Destroy(__instance);
		}
	}
}
