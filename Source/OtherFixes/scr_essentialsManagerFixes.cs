using HarmonyLib;

namespace SuisHack.OtherFixes
{
	public static class scr_essentialsManagerFixes
	{
		public static bool Initialized;
		public static void Initialize()
		{
			if (Initialized)
				return;

			{
				var sourceClassType = typeof(scr_essentialsManager);
				var sourceMethod = sourceClassType.GetMethod("Start", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

				var targetClassType = typeof(scr_essentialsManagerFixes);
				var targetMethod = new HarmonyMethod(targetClassType.GetMethod(nameof(AdditionalStartStuff), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static));

				Plugin.HarmonyInst.Patch(sourceMethod, postfix: targetMethod);

			}


			Initialized = true;
		}

		private static void AdditionalStartStuff(scr_loadScene __instance)
		{
			var go = __instance.transform.Find("UI/Canvas/Holder/pauseScreen/videoSettingsMenu");
			if (go != null)
			{
				if (!go.TryGetComponent<OtherFixes.ResolutionFixComponent>(out _))
					go.gameObject.AddComponent<OtherFixes.ResolutionFixComponent>();
			}
		}
	}
}
