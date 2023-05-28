using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace SuisHack.OtherFixes
{
	public static class ResolutionFixes
	{
		private static bool Initialized;

		public static void Initialize()
		{
			if (Initialized)
				return;

			var sourceClassType = typeof(ResolutionMenu);
			var sourceMethod = sourceClassType.GetMethod("CreateResolutionDropdown", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

			var targetClassType = typeof(ResolutionFixes);
			var targetMethod = new HarmonyMethod(targetClassType.GetMethod(nameof(CreateResolutionDropdownFixed), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static));

			Plugin.HarmonyInst.Patch(sourceMethod, prefix: targetMethod);
			Initialized = true;
		}

		private static bool CreateResolutionDropdownFixed(ResolutionMenu __instance, ref Resolution ___selectedResolution, ref Resolution[] ___resolutions)
		{
			__instance.resolutionDropdown.ClearOptions();
			List<string> list = new List<string>();
			List<Resolution> resolutionsLimited = new List<Resolution>();

			int selectedResolutionId = 0;

			var iterator = 0;
			var resolutions = ___resolutions;
			for (int i = 0; i < resolutions.Length; i++)
			{
				var currentResolution = resolutions[i];
				string item = $"{currentResolution.width} x {currentResolution.height} @ {currentResolution.refreshRate}";

				if (list.Contains(item))
					continue;
				list.Add(item);
				resolutionsLimited.Add(currentResolution);
				iterator++;

				if (currentResolution.width == ___selectedResolution.width &&
					currentResolution.height == ___selectedResolution.height &&
					currentResolution.refreshRate == ___selectedResolution.refreshRate)
					selectedResolutionId = iterator;
			}

			__instance.resolutionDropdown.AddOptions(list);
			__instance.resolutionDropdown.value = selectedResolutionId;
			__instance.resolutionDropdown.RefreshShownValue();
			___resolutions = resolutionsLimited.ToArray();

			var rectTransform = __instance.resolutionDropdown.GetComponent<RectTransform>();
			if (rectTransform != null)
			{
				rectTransform.sizeDelta = new Vector2(220, rectTransform.sizeDelta.y);
			}
			return false;
		}
	}
}
