# OU2-Start-Date-Tuner
A mod for Orbi Universo 2 that lets you change the starting date. Good for roleplaying post-apocalyptic scenarios since OU2 isn't year dependent.

## Requirements
- BepInEx 5 x64 (Mono). Download from the official BepInEx releases. https://github.com/bepinex/bepinex/releases

## Install
1. Extract the BepInEx zip into the game folder (next to the exe). Run the game once.
2. Extract **this mod’s zip** into the plugins location so that:
   - BepInEx\plugins\StartDateTuner\StartDateTuner.dll exists
3. Run the game. Check `BepInEx/LogOutput.log` for “"Start Date Tuner loaded. Year={year}"”. After the first launch with the plugin installed you can find the created config in BepInEx/config/ou2.startdatetuner if you want to change the year. Vanilla value is -6000. Default for the mod is 2100.

## Configure
Edit `BepInEx/config/ou2.startdatetuner`:
