using HarmonyLib;
using UnityEngine;

namespace SuisHack.HereticMod_Classes
{
	[HarmonyPatch]
	public static class HereticMod_UI_scr_ui_holderScaler
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(scr_ui_holderScaler), "Start")]
		public static void StartPostfix(scr_ui_holderScaler __instance)
		{
			__instance.gameObject.AddComponent<Components.HereticMod_UI_Component>();
			MonoBehaviour.Destroy(__instance);
		}
	}
}
