using HarmonyLib;
using UnityEngine;

namespace SuisHack.Timescaling_fixes
{
	public static class scr_spinfix
	{
		private static bool Initialized;
		public static void Initialize()
		{
			if (Initialized)
				return;

			var sourceClassType = typeof(scr_spin);
			var sourceMethod = sourceClassType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

			var targetClassType = typeof(scr_spinfix);
			var targetMethod = new HarmonyMethod(targetClassType.GetMethod(nameof(Updatefix), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static));

			Plugin.HarmonyInst.Patch(sourceMethod, prefix: targetMethod);
			Initialized = true;
		}

		private static bool Updatefix(scr_spin __instance, ref float ___xSpin, ref float ___ySpin, ref float ___zSpin)
		{
			if (__instance.axis == "x")
			{
				___xSpin = 1f;
				___ySpin = 0f;
				___zSpin = 0f;
			}
			else if (__instance.axis == "y")
			{
				___xSpin = 0f;
				___ySpin = 1f;
				___zSpin = 0f;
			}
			else if (__instance.axis == "z")
			{
				___xSpin = 0f;
				___ySpin = 0f;
				___zSpin = 1f;
			}

			float spinRate = __instance.spinRate * 25f;
			__instance.transform.Rotate(___xSpin * spinRate * Time.deltaTime, ___ySpin * spinRate * Time.deltaTime, ___zSpin * spinRate * Time.deltaTime);
			return false;
		}
	}
}
