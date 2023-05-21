using HarmonyLib;
using UnityEngine;

namespace SuisHack.Timescaling_fixes
{
	[HarmonyPatch]
	public static class CharacterControllerHook
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(CharacterController), "Awake")]
		public static void AwakePostfix()
		{
			Application.targetFrameRate = Config.FPS_Limit.Value;
			QualitySettings.vSyncCount = Config.Vsync_Count.Value;
		}
	}
}
