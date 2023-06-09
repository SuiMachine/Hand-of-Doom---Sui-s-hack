using SuisHack;
using SuisHack.HereticMod_Classes;

public static class HereticMod
{
	public static bool Initialized { get; private set; }

	public static void Initialize()
	{
		if (!Config.UseHereticMod.Value)
			return;

		HereticMod_UI_scr_ui_holderScaler.InitializeHook();
		HereticMod_CharacterController.Initialize();
		Initialized = true;
	}
}