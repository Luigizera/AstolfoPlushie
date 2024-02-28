using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalLib.Modules;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace AstolfoPlushie
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class AstolfoPlushieMod : BaseUnityPlugin
    {
        private const string modGUID = "Lugom.AstolfoPlushie";
        private const string modName = "Astolfo Plushie";
        private const string modVersion = "1.0.0";
        private const string prefabName = "astolfomod";
        private const string assetLocation = "Assets/AstolfoItem.asset";
        private readonly Harmony harmony = new Harmony(modGUID);
        private static AstolfoPlushieMod instance;
        private static ManualLogSource mls;
        public static int rarity = 20;
        public static bool devMode = false;

        void Awake()
        {
            instance = this;
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), prefabName);
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);

            Item astolfoPlushie = bundle.LoadAsset<Item>(assetLocation);

            devMode = Config.Bind<bool>("Developer Mode", "Active", false, "This will make Astolfo Plushie appear in the shop for testing if set to true").Value;
            rarity = Config.Bind<int>("Rarity", "Base rarity", 20, "Rarity of the astolfo plushie between 1 - 100 (100 being common, 1 being rare)").Value;

            NetworkPrefabs.RegisterNetworkPrefab(astolfoPlushie.spawnPrefab);
            Utilities.FixMixerGroups(astolfoPlushie.spawnPrefab);
            Items.RegisterScrap(astolfoPlushie, (int)(rarity * Config.Bind<float>("Rarity", "Experimentation", 0.3f, "Rarity multiplier for Experimentation").Value), Levels.LevelTypes.ExperimentationLevel);
            Items.RegisterScrap(astolfoPlushie, (int)(rarity * Config.Bind<float>("Rarity", "Assurance", 0.5f, "Rarity multiplier for Assurance").Value), Levels.LevelTypes.AssuranceLevel);
            Items.RegisterScrap(astolfoPlushie, (int)(rarity * Config.Bind<float>("Rarity", "Vow", 1f, "Rarity multiplier for Vow").Value), Levels.LevelTypes.VowLevel);
            Items.RegisterScrap(astolfoPlushie, (int)(rarity * Config.Bind<float>("Rarity", "Offense", 1f, "Rarity multiplier for Offense").Value), Levels.LevelTypes.OffenseLevel);
            Items.RegisterScrap(astolfoPlushie, (int)(rarity * Config.Bind<float>("Rarity", "March", 1f, "Rarity multiplier for March").Value), Levels.LevelTypes.MarchLevel);
            Items.RegisterScrap(astolfoPlushie, (int)(rarity * Config.Bind<float>("Rarity", "Rend", 2f, "Rarity multiplier for Rend").Value), Levels.LevelTypes.RendLevel);
            Items.RegisterScrap(astolfoPlushie, (int)(rarity * Config.Bind<float>("Rarity", "Dine", 1.5f, "Rarity multiplier for Dine").Value), Levels.LevelTypes.DineLevel);
            Items.RegisterScrap(astolfoPlushie, (int)(rarity * Config.Bind<float>("Rarity", "Titan", 1.5f, "Rarity multiplier for Titan").Value), Levels.LevelTypes.TitanLevel);

            if(devMode)
            {
                testingAstolfoPlushie(astolfoPlushie);
            }
            harmony.PatchAll(typeof(AstolfoPlushieMod));
            mls.LogInfo($"Plugin {modName} is loaded!");
        }

        
        private static void testingAstolfoPlushie(Item astolfoPlushie)
        {
            TerminalNode node = ScriptableObject.CreateInstance<TerminalNode>();
            node.clearPreviousText = true;
            node.displayText = $"This is a dangerous Plushie\n\n";
            Items.RegisterShopItem(astolfoPlushie, null, null, node, 0);
        }
        
    }
}
