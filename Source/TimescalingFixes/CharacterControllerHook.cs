using HarmonyLib;
using UnityEngine;

namespace SuisHack.Timescaling_fixes
{
	public static class CharacterControllerHook
	{
		private static bool Initialized;
		public static void Initialize()
		{
			if (Initialized)
				return;

			var sourceClassType = typeof(CharacterController);
			var sourceMethod = sourceClassType.GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

			var targetClassType = typeof(CharacterControllerHook);
			var targetMethod = new HarmonyMethod(targetClassType.GetMethod(nameof(AwakePostfix), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static));

			Plugin.HarmonyInst.Patch(sourceMethod, postfix: targetMethod);
			Initialized = true;
		}

		private static void AwakePostfix()
		{
			Application.targetFrameRate = Config.FPS_Limit.Value;
			QualitySettings.vSyncCount = Config.Vsync_Count.Value;
		}
	}
}
