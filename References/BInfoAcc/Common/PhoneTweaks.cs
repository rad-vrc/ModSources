using BInfoAcc.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Common
{
    public class RecipeTweak : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModContent.GetInstance<ConfigServer>().extendedRecipe)
            {
                return;
            }
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasIngredient(ItemID.GPS) && recipe.HasIngredient(ItemID.FishFinder) 
                    && recipe.HasIngredient(ItemID.GoblinTech) && recipe.HasIngredient(ItemID.REK)          
                    && recipe.HasTile(TileID.TinkerersWorkbench) 
                    && recipe.HasResult(ItemID.PDA)
                    && !recipe.HasIngredient<ScryingMirror>() && !recipe.HasIngredient<RSH>() && !recipe.HasIngredient<ThreatAnalyzer>())
                {
                    recipe.AddIngredient<ScryingMirror>();
                    recipe.AddIngredient<RSH>();
                    recipe.AddIngredient<ThreatAnalyzer>();
                }
            }
        }
    }

    public class PhoneTweaks : GlobalItem 
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if(entity.type == ItemID.PDA || entity.type == ItemID.CellPhone ||
               entity.type == ItemID.Shellphone || entity.type == ItemID.ShellphoneSpawn ||
               entity.type == ItemID.ShellphoneHell || entity.type == ItemID.ShellphoneOcean)
            {
                return true;
            }


            // Thorium Cross-Mod and support for The Depths's Shellphone variant 
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium) && thorium.TryFind("WishingGlass", out ModItem wishingGlass)
                && entity.type == wishingGlass.Type)
            {
                return true;
            }
            if(ModLoader.TryGetMod("TheDepths", out Mod depths) && depths.TryFind("ShellPhoneDepths", out ModItem shellphoneDepths)
                && entity.type == shellphoneDepths.Type)
            {
                return true;
            }
            if (ModLoader.TryGetMod("Avalon", out Mod avalon))
            {
                List<int> phoneTypes = new List<int>();
                if(avalon.TryFind("ShadowPhoneHome", out ModItem sPhoneHome))
                {
                    phoneTypes.Add(sPhoneHome.Type);
                }
                if (avalon.TryFind("ShadowPhoneRandom", out ModItem sPhoneRand))
                {
                    phoneTypes.Add(sPhoneRand.Type);
                }
                if (avalon.TryFind("ShadowPhoneJungleTropics", out ModItem sPhoneJungle))
                {
                    phoneTypes.Add(sPhoneJungle.Type);
                }
                if (avalon.TryFind("ShadowPhoneHell", out ModItem sPhoneHell))
                {
                    phoneTypes.Add(sPhoneHell.Type);
                }
                if (avalon.TryFind("ShadowPhoneOcean", out ModItem sPhoneOcean))
                {
                    phoneTypes.Add(sPhoneOcean.Type);
                }
                if (avalon.TryFind("ShadowPhoneDungeon", out ModItem sPhoneDungeon))
                {
                    phoneTypes.Add(sPhoneDungeon.Type);
                }
                if (avalon.TryFind("ShadowPhoneSurface", out ModItem sPhoneSurface))
                {
                    phoneTypes.Add(sPhoneSurface.Type);
                }
                if (avalon.TryFind("ShadowPhoneNone", out ModItem sPhoneNone))
                {
                    phoneTypes.Add(sPhoneNone.Type);
                }


                if (phoneTypes.Contains(entity.type))
                {
                    return true;
                }
            }
            if (ModLoader.TryGetMod("MaddieQoL", out Mod completenessMod))
            {
                List<int> phoneTypes = new List<int>();
                if (completenessMod.TryFind("ShellphonePlusHome", out ModItem sPhonePlusHome))
                {
                    phoneTypes.Add(sPhonePlusHome.Type);
                }
                if (completenessMod.TryFind("ShellphonePlusRandomReturn", out ModItem sPhonePlusReturn))
                {
                    phoneTypes.Add(sPhonePlusReturn.Type);
                }
                if (completenessMod.TryFind("ShellphonePlusHell", out ModItem sPhonePlusHell))
                {
                    phoneTypes.Add(sPhonePlusHell.Type);
                }
                if (completenessMod.TryFind("ShellphonePlusOcean", out ModItem sPhonePlusOcean))
                {
                    phoneTypes.Add(sPhonePlusOcean.Type);
                }
                if (completenessMod.TryFind("ShellphonePlusSpawn", out ModItem sPhonePlusSpawn))
                {
                    phoneTypes.Add(sPhonePlusSpawn.Type);
                }
                if (completenessMod.TryFind("ShellphonePlusDummy", out ModItem sPhonePlusNone))
                {
                    phoneTypes.Add(sPhonePlusNone.Type);
                }
                if (completenessMod.TryFind("CellPhonePlus", out ModItem phonePlus))
                {
                    phoneTypes.Add(phonePlus.Type);
                }

                if (phoneTypes.Contains(entity.type))
                {
                    return true;
                }
            }
            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoSouls))
            {
                List<int> soulTypes = new List<int>();
                if (fargoSouls.TryFind("WorldShaperSoul", out ModItem worldSoul))
                {
                    soulTypes.Add(worldSoul.Type);
                }
                if (fargoSouls.TryFind("DimensionSoul", out ModItem dimentionSoul))
                {
                    soulTypes.Add(dimentionSoul.Type);
                }
                if (fargoSouls.TryFind("EternitySoul", out ModItem eternitySoul))
                {
                    soulTypes.Add(eternitySoul.Type);
                }


                if (soulTypes.Contains(entity.type))
                {
                    return true;
                }
            }

            return false;
        }

        public override void UpdateInfoAccessory(Item item, Player player)
        {
            if (!ModContent.GetInstance<ConfigServer>().updatedPhones)
            {
                return;
            }

            player.GetModPlayer<InfoPlayer>().comboDisplay = true;
            player.GetModPlayer<InfoPlayer>().regenDisplay = true;
            player.GetModPlayer<InfoPlayer>().spawnRateDisplay = true;
            player.GetModPlayer<InfoPlayer>().minionDisplay = true;
            player.GetModPlayer<InfoPlayer>().sentryDisplay = true;
            player.GetModPlayer<InfoPlayer>().luckDisplay = true;
            player.GetModPlayer<InfoPlayer>().biomeDisplay = true;
            player.GetModPlayer<InfoPlayer>().aggroDisplay = true;
            player.GetModPlayer<InfoPlayer>().manaDisplay = true;
        }
    }
}