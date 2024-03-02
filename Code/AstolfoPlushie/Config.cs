using BepInEx.Configuration;

namespace AstolfoPlushie
{
    public class Config
    {
        public static ConfigEntry<bool> devMode;
        public static ConfigEntry<bool> extraAstolfos;
        #region Astolfo Variables
        public static ConfigEntry<int> int_astolfoPlushie_spawnChance;
        public static ConfigEntry<int> int_astolfoPlushie_gold_spawnChance;
        public static ConfigEntry<int> int_astolfoPlushie_red_spawnChance;
        public static ConfigEntry<int> int_astolfoPlushie_green_spawnChance;
        public static ConfigEntry<int> int_astolfoPlushie_blue_spawnChance;
        public static ConfigEntry<int> int_astolfoPlushie_yellow_spawnChance;
        public static ConfigEntry<int> int_astolfoPlushie_diamond_spawnChance;
        public static ConfigEntry<int> int_astolfoPlushie_emerald_spawnChance;
        #endregion
        #region Multiplier Variables
        public static ConfigEntry<float> int_astolfoPlushie_spawnMultiplier_experimentation;
        public static ConfigEntry<float> int_astolfoPlushie_spawnMultiplier_assurance;
        public static ConfigEntry<float> int_astolfoPlushie_spawnMultiplier_vow;
        public static ConfigEntry<float> int_astolfoPlushie_spawnMultiplier_offense;
        public static ConfigEntry<float> int_astolfoPlushie_spawnMultiplier_march;
        public static ConfigEntry<float> int_astolfoPlushie_spawnMultiplier_rend;
        public static ConfigEntry<float> int_astolfoPlushie_spawnMultiplier_dine;
        public static ConfigEntry<float> int_astolfoPlushie_spawnMultiplier_titan;
        #endregion

        public static void Load()
        {
            extraAstolfos = AstolfoPlushieMod.config.Bind<bool>("Extras", "TooManyAstolfos", true, "This option enables every Astolfo Plushie variation avaliable in the mod, Example: Red Astolfo Plushie");
            devMode = AstolfoPlushieMod.config.Bind<bool>("Extras", "Developer Mode", false, "This option adds every Astolfo Plushie variation avaliable to the Shop");

            int_astolfoPlushie_spawnChance = AstolfoPlushieMod.config.Bind<int>("Astolfo Rarity", "Common Astolfo", 10, "How much Astolfo Plushie spawns, higher = more");
            int_astolfoPlushie_gold_spawnChance = AstolfoPlushieMod.config.Bind<int>("Astolfo Rarity", "Gold Astolfo", 1, "How much Gold Astolfo Plushie spawns, higher = more (Not affected by Multipliers)");
            int_astolfoPlushie_red_spawnChance = AstolfoPlushieMod.config.Bind<int>("TooManyAstolfos Rarity", "Red Astolfo", 20, "How much Red Astolfo Plushie spawns, higher = more");
            int_astolfoPlushie_green_spawnChance = AstolfoPlushieMod.config.Bind<int>("TooManyAstolfos Rarity", "Green Astolfo", 20, "How much Green Astolfo Plushie spawns, higher = more");
            int_astolfoPlushie_blue_spawnChance = AstolfoPlushieMod.config.Bind<int>("TooManyAstolfos Rarity", "Blue Astolfo", 20, "How much Blue Astolfo Plushie spawns, higher = more");
            int_astolfoPlushie_yellow_spawnChance = AstolfoPlushieMod.config.Bind<int>("TooManyAstolfos Rarity", "Yellow Astolfo", 20, "How much Yellow Astolfo Plushie spawns, higher = more");
            int_astolfoPlushie_emerald_spawnChance = AstolfoPlushieMod.config.Bind<int>("TooManyAstolfos Rarity", "Emerald Astolfo", 1, "How much Emerald Astolfo Plushie spawns, higher = more (Not affected by Multipliers)");
            int_astolfoPlushie_diamond_spawnChance = AstolfoPlushieMod.config.Bind<int>("TooManyAstolfos Rarity", "Diamond Astolfo", 1, "How much Diamond Astolfo Plushie spawns, higher = more (Not affected by Multipliers)");

            int_astolfoPlushie_spawnMultiplier_experimentation = AstolfoPlushieMod.config.Bind<float>("Rarity Multiplier", "Experimentation", 0.3f, "Rarity multiplier for Experimentation");
            int_astolfoPlushie_spawnMultiplier_assurance = AstolfoPlushieMod.config.Bind<float>("Rarity Multiplier", "Assurance", 0.5f, "Rarity multiplier for Assurance");
            int_astolfoPlushie_spawnMultiplier_vow = AstolfoPlushieMod.config.Bind<float>("Rarity Multiplier", "Vow", 1f, "Rarity multiplier for Vow");
            int_astolfoPlushie_spawnMultiplier_offense = AstolfoPlushieMod.config.Bind<float>("Rarity Multiplier", "Offense", 1f, "Rarity multiplier for Offense");
            int_astolfoPlushie_spawnMultiplier_march = AstolfoPlushieMod.config.Bind<float>("Rarity Multiplier", "March", 1f, "Rarity multiplier for March");
            int_astolfoPlushie_spawnMultiplier_rend = AstolfoPlushieMod.config.Bind<float>("Rarity Multiplier", "Rend", 2f, "Rarity multiplier for Rend");
            int_astolfoPlushie_spawnMultiplier_dine = AstolfoPlushieMod.config.Bind<float>("Rarity Multiplier", "Dine", 1.5f, "Rarity multiplier for Dine");
            int_astolfoPlushie_spawnMultiplier_titan = AstolfoPlushieMod.config.Bind<float>("Rarity Multiplier", "Titan", 1.5f, "Rarity multiplier for Titan");
            
        }
    }
}
