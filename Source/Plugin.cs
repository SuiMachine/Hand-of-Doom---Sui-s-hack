using MelonLoader;

namespace SuisHack
{
	public class Plugin : MelonMod
	{
		//HarmonyLib

		public override void OnLateInitializeMelon()
		{
			base.OnLateInitializeMelon();

			SuisMenu.MenuThing.Initialize();

			LoggerInstance.Msg("Patching");
			HarmonyInstance.PatchAll();
			LoggerInstance.Msg("Patched");
		}
	}
}
