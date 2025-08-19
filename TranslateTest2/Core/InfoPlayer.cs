using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TranslateTest2.Core
{
    // Biome 情報表示用の最小限 ModPlayer 実装
    public partial class InfoPlayer : ModPlayer
    {
        public bool biomeDisplay;
        public List<string> biomeNames;
        public int displayTimer; // 経過フレーム
        public static readonly List<string> EmptyList = new List<string>(0);

        public override void Initialize()
        {
            biomeNames = new List<string>();
        }

        public override void ResetInfoAccessories()
        {
            biomeDisplay = false;
        }

        public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
        {
            if (otherPlayer.GetModPlayer<InfoPlayer>().biomeDisplay)
                biomeDisplay = true;
        }

        public override void PreUpdate()
        {
            // 参照実装互換: 1tickごと増加し 4000 超でリセット
            if (++displayTimer > 4000)
                displayTimer = 0;
        }

        public override void PostUpdate()
        {
            // 1秒毎に再計算
            if (biomeDisplay && (biomeNames == null || Main.GameUpdateCount % 60 == 0))
            {
                // Use the new MOD biome detection system from InfoPlayer.ModBiomes.cs
                biomeNames = GetBiomes(Player, prioModBiomes: true);
                
                // Fallback to basic vanilla detection if no biomes found
                if (biomeNames.Count == 0)
                    biomeNames = GetCurrentBiomes();
            }
        }

        private List<string> GetCurrentBiomes()
        {
            var list = new List<string>();
            Player p = Player;

            if (p.townNPCs > 2f && !p.ZoneShadowCandle)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Town"));
            if (p.ZoneUnderworldHeight)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Underworld"));
            else if (p.ZoneSkyHeight)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Space"));
            else if (p.ZoneRockLayerHeight || p.ZoneDirtLayerHeight)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Underground"));
            if (p.ZoneBeach)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Ocean"));
            if (p.ZoneSnow)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Snow"));
            if (p.ZoneDesert || p.ZoneUndergroundDesert)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Desert"));
            if (p.ZoneJungle)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Jungle"));
            if (p.ZoneHallow)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Hallow"));
            if (p.ZoneCorrupt)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Corruption"));
            if (p.ZoneCrimson)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Crimson"));
            if (p.ZoneGlowshroom)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Shroom"));
            if (p.ZoneDungeon)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Dungeon"));
            if (p.ZoneMeteor)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Meteor"));
            if (p.ZoneGranite)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Granite"));
            if (p.ZoneMarble)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Marble"));
            if (p.ZoneLihzhardTemple)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Temple"));
            if (p.ZoneGraveyard)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Graveyard"));

            if (list.Count == 0)
                list.Add(Language.GetTextValue("Mods.BInfoAcc.Biomes.Vanilla.Forest"));

            var seen = new HashSet<string>();
            var dedup = new List<string>();
            foreach (var b in list)
                if (seen.Add(b)) dedup.Add(b);
            return dedup;
        }
    }
}
