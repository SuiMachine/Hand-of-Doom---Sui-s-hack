using HarmonyLib;
using UnityEngine;

namespace SuisHack.Timescaling_fixes
{
	[HarmonyPatch]
	public static class scr_spinfix
	{
		[HarmonyPrefix]
		[HarmonyPatch(typeof(scr_spin), "Update")]
		public static bool Updatefix(scr_spin __instance, ref float ___xSpin, ref float ___ySpin, ref float ___zSpin)
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
