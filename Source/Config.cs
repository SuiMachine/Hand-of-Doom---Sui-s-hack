using MelonLoader;
using UnityEngine;

namespace SuisHack
{
	public class Config
	{
		public static MelonPreferences_Entry<int> FPS_Limit;
		public static MelonPreferences_Entry<int> Vsync_Count;

		public static MelonPreferences_Entry<bool> UseHereticMod;
		public static MelonPreferences_Entry<bool> Heretic_NativeResolutionRendering;
		public static MelonPreferences_Entry<float> Heretic_FOV;


		public static void Init()
		{
			var cat = MelonPreferences.CreateCategory("General");
			FPS_Limit = cat.CreateEntry("FPS limit", 25, "A setting that allows for overriding FPS limit - default is 25", validator: new MelonLoader.Preferences.ValueRange<int>(0, 240));
			FPS_Limit.OnEntryValueChanged.Subscribe(new LemonAction<int, int>((int oldValue, int newValue) => { Application.targetFrameRate = newValue; }));
			Vsync_Count = cat.CreateEntry("Vsync count", 0, "V-sync count. If this is more than 1 - Vsync is enabled and FPS limit isn't applied. If this is 0, FPS limit is used and there is no Vsync. Setting this to 2 causes Vsync to be sync to monitor's every 2nd refresh rate (so 30 fps on 60 Hz monitor). 3 - every 3rd (I think) etc.", validator: new MelonLoader.Preferences.ValueRange<int>(0, 4));
			Vsync_Count.OnEntryValueChanged.Subscribe(new LemonAction<int, int>((int oldValue, int newValue) => { QualitySettings.vSyncCount = newValue; }));

			var hereticCat = MelonPreferences.CreateCategory("Heretic Mod");
			UseHereticMod = hereticCat.CreateEntry("Use Mod", false, "Set this to true to use Heretic Mod - this will unlock options that are modifying gameplay and affect aethetic in significent ways");
			Heretic_NativeResolutionRendering = hereticCat.CreateEntry("Native resolution rendering", false, "Makes the game render image at screen resolution (this severely changes the game aesthetic!)");
			Heretic_FOV = hereticCat.CreateEntry("FOV", 70f, "Since the screen size is bigger in heretic, this should also be increased - it is a vertical FOV - 75 is approx. 90 on 4:3 and 107 on 16:9, so it should be optimal.");

		}
	}
}
