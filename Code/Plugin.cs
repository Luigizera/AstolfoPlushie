using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace AstolfoPlushie
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency(LethalLib.Plugin.ModGUID)]
    public class AstolfoPlushieMod : BaseUnityPlugin
    {
        private const string modGUID = "Lugom.AstolfoPlushie";
        private const string modName = "Astolfo Plushie";
        private const string modVersion = "1.1.0";
        private readonly Harmony harmony = new Harmony(modGUID);
        private static AstolfoPlushieMod instance;
        public static ManualLogSource logger;
        public static ConfigFile config;
        
        private void Awake()
        {
            instance = this;
            logger = Logger;
            config = Config;

            AstolfoPlushie.Config.Load();
            AstolfoPlushie.Content.Load();
            logger.LogMessage("\n                                                      %%                        \n                           ,/@@@@@@@@@@@@@@&(,            @/\n                     *@ @/ ....@/@@(, ..&@                      @.                \n                  @@......................**         @           %#             \n              *@........,% &..................*/  /* @@@@@&          @           \n          @@@@........@..........,..............(@@@@@@@             #          \n           @.......(...............@........   ....@@@@                @        \n          %......@..................@........     ...@                  /       \n        @......@......................*.......      ...(                /       \n       @.....,.........................@......        ..(                @      \n      @.....@..................................         ..&               /     \n     @........,....@......................@.....      .....@              @     \n    (....@...@     . #...%,   @.%* @.*@.@...#...   .. ......%             .     \n    @...&.., @,@##&/ .        . , (@####@@/@.*.. ............,             @    \n   #...,..*   %######           @#########   @.@...............*           #    \n   @..@...@   @ ..,#@          @ @/./......   @..@.........@#/,*,           *   \n   #....../    @*.*@              @.....@    @.....@........                @   \n  @..*...@@*          &,,,,,,,,,@             @......@....#                 (   \n   @.@...&  @           @*,,,,,*%             @..(.....%..@                 /&  \n      @... #                                 @.../.......*       ,              \n       @..@ %@@  @@.                         @...#......(      ,(               \n         @              @@*                 @...,@......@ @#    @               \n                                  ,@@@@,   @....*@..,#,                         \n                                           @..*@@@@,(                           \n                                           &.@@@@@@@@%                          \n                                          /.*@.,@@@@@@@@.                       \n                                         @.......@   @                          \n                                          @.....@                               \n                                            @/@                                 ");
            harmony.PatchAll(typeof(AstolfoPlushieMod));
        }

    }
}
