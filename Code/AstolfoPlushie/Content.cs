using AstolfoPlushie.MonoBehaviours;
using LethalLib.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.Netcode.Components;
using UnityEngine;

namespace AstolfoPlushie
{
    class Content
    {
        public static AssetBundle MainAssets;
        public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();
        private const string assetName = "astolfomod";
        public class CustomItem
        {
            public string name = "";
            public string itemPath = "";
            public string infoPath = "";
            public Action<Item> itemAction = (item) => { };
            public bool enabled = true;

            public CustomItem(string name, string itemPath, string infoPath, Action<Item> action = null)
            {
                this.name = name;
                this.itemPath = itemPath;
                this.infoPath = infoPath;
                if (action != null)
                {
                    itemAction = action;
                }
            }

            public static CustomItem Add(string name, string itemPath, string infoPath = null, Action<Item> action = null)
            {
                CustomItem item = new CustomItem(name, itemPath, infoPath, action);
                return item;
            }
        }

        public class CustomScrap : CustomItem
        {
            public Levels.LevelTypes levelType = Levels.LevelTypes.All;
            public int rarity = 0;

            public CustomScrap(string name, string itemPath, Levels.LevelTypes levelType, int rarity, Action<Item> action = null) : base(name, itemPath, null, action)
            {
                this.levelType = levelType;
                this.rarity = rarity;
            }

            public static CustomScrap Add(string name, string itemPath, Levels.LevelTypes levelType, int rarity, Action<Item> action = null)
            {
                CustomScrap item = new CustomScrap(name, itemPath, levelType, rarity, action);
                return item;
            }

            public static CustomScrap AddWithVerification(string name, string itemPath, Levels.LevelTypes levelType, int rarity, Action<Item> action = null, bool enabled = true)
            {
                CustomScrap item = new CustomScrap(name, itemPath, levelType, rarity, action);
                item.enabled = enabled;
                return item;
            }
        }

        public static void TryLoadAssets()
        {
            if (MainAssets == null)
            {
                MainAssets = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assetName));
                AstolfoPlushieMod.logger.LogInfo("Loaded asset bundle");
            }
        }

        public static List<CustomItem> customItems;
        public static void Load()
        {
            TryLoadAssets();
            customItems = new List<CustomItem>()
            {
                CustomScrap.Add("AstolfoPlushie", "Assets/Astolfo/AstolfoPlushie_Item.asset", Levels.LevelTypes.None, Config.int_astolfoPlushie_spawnChance.Value),
                //This one doesn`t work on the terminal because of the name Gold conflicting with something I don't know. Conclusion: Two whole days wasted (I'm dumb)
                CustomScrap.Add("AstolfoPlushie_Gold", "Assets/Astolfo/AstolfoGold/AstolfoPlushieGold_Item.asset", Levels.LevelTypes.All, Config.int_astolfoPlushie_gold_spawnChance.Value),
                CustomScrap.AddWithVerification("AstolfoPlushie_Red", "Assets/Astolfo/AstolfoRed/AstolfoPlushieRed_Item.asset", Levels.LevelTypes.None, Config.int_astolfoPlushie_red_spawnChance.Value, enabled: Config.extraAstolfos.Value),
                CustomScrap.AddWithVerification("AstolfoPlushie_Green", "Assets/Astolfo/AstolfoGreen/AstolfoPlushieGreen_Item.asset", Levels.LevelTypes.None, Config.int_astolfoPlushie_green_spawnChance.Value, enabled: Config.extraAstolfos.Value),
                CustomScrap.AddWithVerification("AstolfoPlushie_Blue", "Assets/Astolfo/AstolfoBlue/AstolfoPlushieBlue_Item.asset", Levels.LevelTypes.None, Config.int_astolfoPlushie_blue_spawnChance.Value, enabled: Config.extraAstolfos.Value),
                CustomScrap.AddWithVerification("AstolfoPlushie_Yellow", "Assets/Astolfo/AStolfoYellow/AstolfoPlushieYellow_Item.asset", Levels.LevelTypes.None, Config.int_astolfoPlushie_yellow_spawnChance.Value, enabled: Config.extraAstolfos.Value),
                CustomScrap.AddWithVerification("AstolfoPlushie_Emerald", "Assets/Astolfo/AstolfoEmerald/AstolfoPlushieEmerald_Item.asset", Levels.LevelTypes.All, Config.int_astolfoPlushie_emerald_spawnChance.Value, enabled: Config.extraAstolfos.Value),
                CustomScrap.AddWithVerification("AstolfoPlushie_Diamond", "Assets/Astolfo/AstolfoDiamond/AstolfoPlushieDiamond_Item.asset", Levels.LevelTypes.All, Config.int_astolfoPlushie_diamond_spawnChance.Value, enabled: Config.extraAstolfos.Value),

            };

            foreach (var item in customItems)
            {
                if (!item.enabled)
                {
                    continue;
                }

                var itemAsset = MainAssets.LoadAsset<Item>(item.itemPath);
                
                if (itemAsset.spawnPrefab.GetComponent<NetworkTransform>() == null && itemAsset.spawnPrefab.GetComponent<CustomNetworkTransform>() == null)
                {
                    var networkTransform = itemAsset.spawnPrefab.AddComponent<NetworkTransform>();
                    networkTransform.SlerpPosition = false;
                    networkTransform.Interpolate = false;
                    networkTransform.SyncPositionX = false;
                    networkTransform.SyncPositionY = false;
                    networkTransform.SyncPositionZ = false;
                    networkTransform.SyncScaleX = false;
                    networkTransform.SyncScaleY = false;
                    networkTransform.SyncScaleZ = false;
                    networkTransform.UseHalfFloatPrecision = true;
                }
                Prefabs.Add(item.name, itemAsset.spawnPrefab);
                LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(itemAsset.spawnPrefab);
                LethalLib.Modules.Utilities.FixMixerGroups(itemAsset.spawnPrefab);
                item.itemAction(itemAsset);
                if(item is CustomScrap && Config.devMode.Value)
                {
                    TerminalNode node = ScriptableObject.CreateInstance<TerminalNode>();
                    node.clearPreviousText = true;
                    node.displayText = $"This is a dangerous Plushie\n\n";
                    Items.RegisterShopItem(itemAsset, null, null, node, 0);
                }
                if (item is CustomScrap)
                {
                    AstolfoPlushieMod.logger.LogInfo($"Registering scrap item {item.name}");
                    if(((CustomScrap)item).levelType == Levels.LevelTypes.All)
                    {
                        Items.RegisterScrap(itemAsset, ((CustomScrap)item).rarity, ((CustomScrap)item).levelType);
                    }
                    else
                    {
                        Items.RegisterScrap(itemAsset, (int)(((CustomScrap)item).rarity * Config.int_astolfoPlushie_spawnMultiplier_experimentation.Value), Levels.LevelTypes.ExperimentationLevel);
                        Items.RegisterScrap(itemAsset, (int)(((CustomScrap)item).rarity * Config.int_astolfoPlushie_spawnMultiplier_assurance.Value), Levels.LevelTypes.AssuranceLevel);
                        Items.RegisterScrap(itemAsset, (int)(((CustomScrap)item).rarity * Config.int_astolfoPlushie_spawnMultiplier_vow.Value), Levels.LevelTypes.VowLevel);
                        Items.RegisterScrap(itemAsset, (int)(((CustomScrap)item).rarity * Config.int_astolfoPlushie_spawnMultiplier_offense.Value), Levels.LevelTypes.OffenseLevel);
                        Items.RegisterScrap(itemAsset, (int)(((CustomScrap)item).rarity * Config.int_astolfoPlushie_spawnMultiplier_march.Value), Levels.LevelTypes.MarchLevel);
                        Items.RegisterScrap(itemAsset, (int)(((CustomScrap)item).rarity * Config.int_astolfoPlushie_spawnMultiplier_rend.Value), Levels.LevelTypes.RendLevel);
                        Items.RegisterScrap(itemAsset, (int)(((CustomScrap)item).rarity * Config.int_astolfoPlushie_spawnMultiplier_dine.Value), Levels.LevelTypes.DineLevel);
                        Items.RegisterScrap(itemAsset, (int)(((CustomScrap)item).rarity * Config.int_astolfoPlushie_spawnMultiplier_titan.Value), Levels.LevelTypes.TitanLevel);
                    }

                }

            }

            AstolfoPlushieMod.logger.LogInfo("Loaded all Astolfo Plushies!");
        }
    }
}
