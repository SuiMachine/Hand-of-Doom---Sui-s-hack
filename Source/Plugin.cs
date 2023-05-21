using MelonLoader;

namespace SuisHack
{
	public class Plugin : MelonMod
	{
		private static MelonLogger.Instance loggerInstance;

		public override void OnInitializeMelon()
		{
			base.OnInitializeMelon();
			loggerInstance = LoggerInstance;
			Config.Init();

			LoggerInstance.Msg("Patching");
			HarmonyInstance.PatchAll();
			LoggerInstance.Msg("Patched");
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
	}
}
