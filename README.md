# Hand of Doom - Sui's Hack
 A hack for Hand of Doom, which should allow to play the game at more than 25 fps. Currently only for testing purposes.
 
# Installation
* Download [MelonLoader](https://github.com/LavaGang/MelonLoader/releases) and install it (prefably using installer).
* ~~Download [Sui's Hack](https://github.com/SuiMachine/Hand-of-Doom---Sui-s-hack/releases).~~ For now use Releases folder.
* Extract the zip archive and move it to the game's directory, putting **SuisHack.dll** into MelonLoader's Mods directory.
* While in game press F11 and set the your desired FPS (in the future a config file should be implemented. 

# Installation (SteamDeck)
These instructions are written with path assuming you install it on build in drive. They may need to be modified, if installing on SD card.
* Switch to desktop mode.
* Download [MelonLoader](https://github.com/LavaGang/MelonLoader/releases).
* Launch the installer using Protontricks Launcher (if it is missing, install it using Discovery).
* When the screen of protontricks launcher pops up, selected Deadly Premonition 2.
* Select browse and navigate to: `/home/deck/`
* In text field type in `.local` and press enter (it's a hidden folder).
* Further navigate down to: `/home/deck/.local/share/Steam/common/Hand of Doom` (or wherever the games install on SD cards)
* Select `Hand of Doom.exe` and install it.
* Once it is installed download the [Sui's Hack]https://github.com/SuiMachine/Hand-of-Doom---Sui-s-hack/releases).
* Navigate to `/home/deck/.local/share/Steam/common/Hand of Doom` (or wherever the games install on SD cards)
* Extract the files from the newly downloaded archive file to that folder.
* Finally right click on the game in Steam libary and choose `Properties`.
* Under launch options paste in the following `WINEDLLOVERRIDES="version.dll=n,b" %command%`
* Launch the game. If the MelonLoader appears you should be all set.
 
# In depth changes
* Implemented menu to configure FPS cap using Unity's GUILayout, which tests Unity's TargetFramerate variable.
* Reworked game's **scr_spin** update behaviour to scale correctly with FPS.

# Requirements
* The official copy of the game.
* [MelonLoader 6.x](https://melonwiki.xyz/#/) and HarmonyX (bundled with the release).
