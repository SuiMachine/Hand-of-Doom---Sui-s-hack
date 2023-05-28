using SuisHack;

public static class HereticMod
{
	public static bool Initialized { get; private set; }

	public static void Initialize()
	{
		if (!Config.UseHereticMod.Value)
			return;

		Plugin.LogMessage("Initializing Heretic mod!");
		Plugin.HarmonyInst.PatchAll();
		Plugin.LogMessage("Heretic mod initialized!");
		Initialized = true;
	}
}