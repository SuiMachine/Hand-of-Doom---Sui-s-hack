using MelonLoader;

namespace SuisHack
{
	public class Plugin : MelonMod
	{
		private static MelonLogger.Instance loggerInstance;
		public static HarmonyLib.Harmony HarmonyInst { get; private set; }

		public override void OnInitializeMelon()
		{
			base.OnInitializeMelon();
			loggerInstance = LoggerInstance;
			HarmonyInst = HarmonyInstance;
			Config.Init();

			LoggerInstance.Msg("Patching methods");
			InitializeFPSFix();
			OtherFixes.ResolutionFixes.Initialize();
			LoggerInstance.Msg("Done - stuff should be better now");

			HereticMod.Initialize();
		}

		public override void OnLateInitializeMelon()
		{
			base.OnLateInitializeMelon();
			SuisMenu.MenuThing.Initialize();
		}

		public static void LogMessage(object obj)
		{
			loggerInstance.Msg(obj);
		}

		public static void LogError(object obj)
		{
			loggerInstance.Error(obj);
		}

		public void InitializeFPSFix()
		{
			Timescaling_fixes.CharacterControllerHook.Initialize();
			Timescaling_fixes.scr_spinfix.Initialize();
		}
	}
}
