using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using QoLCompendium.Core.UI.Panels;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.FavoriteEffect
{
	// Token: 0x020001B7 RID: 439
	public class EntityManipulator : ModItem
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x0001C9D6 File Offset: 0x0001ABD6
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.EntityManipulator;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001C9F0 File Offset: 0x0001ABF0
		public override void SetDefaults()
		{
			base.Item.width = 16;
			base.Item.height = 16;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.MenuOpen);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001CA7F File Offset: 0x0001AC7F
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.EntityManipulator);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001CA97 File Offset: 0x0001AC97
		public override bool? UseItem(Player player)
		{
			if (!EntityManipulatorUI.visible)
			{
				EntityManipulatorUI.timeStart = Main.GameUpdateCount;
			}
			EntityManipulatorUI.visible = true;
			return base.UseItem(player);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001CAB8 File Offset: 0x0001ACB8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.EntityManipulator, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(300, 10);
			itemRecipe.AddIngredient(148, 3);
			itemRecipe.AddIngredient(2324, 10);
			itemRecipe.AddIngredient(3117, 3);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		public override void UpdateInventory(Player player)
		{
			if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.NoModifier"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.SpawnIncrease"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 2)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.SpawnDecrease"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 3)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.CanceledSpawns"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 4)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.CanceledEvents"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 5)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.CanceledSpawnsAndEvents"));
			}
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 1)
				{
					player.GetModPlayer<QoLCPlayer>().increasedSpawns = true;
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 2)
				{
					player.GetModPlayer<QoLCPlayer>().decreasedSpawns = true;
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 3)
				{
					player.GetModPlayer<QoLCPlayer>().noSpawns = true;
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 4)
				{
					if (Main.invasionType != 0)
					{
						Main.invasionType = 0;
					}
					if (Main.pumpkinMoon)
					{
						Main.pumpkinMoon = false;
					}
					if (Main.snowMoon)
					{
						Main.snowMoon = false;
					}
					if (Main.eclipse)
					{
						Main.eclipse = false;
					}
					if (Main.bloodMoon)
					{
						Main.bloodMoon = false;
					}
					if (Main.WindyEnoughForKiteDrops)
					{
						Main.windSpeedTarget = 0f;
						Main.windSpeedCurrent = 0f;
					}
					if (Main.slimeRain)
					{
						Main.StopSlimeRain(true);
						Main.slimeWarningDelay = 1;
						Main.slimeWarningTime = 1;
					}
					if (BirthdayParty.PartyIsUp)
					{
						BirthdayParty.CheckNight();
					}
					if (DD2Event.Ongoing && Main.netMode != 1)
					{
						DD2Event.StopInvasion(false);
					}
					if (Sandstorm.Happening)
					{
						Sandstorm.Happening = false;
						Sandstorm.TimeLeft = 0.0;
						Sandstorm.IntendedSeverity = 0f;
					}
					if (NPC.downedTowers && (NPC.LunarApocalypseIsUp || NPC.ShieldStrengthTowerNebula > 0 || NPC.ShieldStrengthTowerSolar > 0 || NPC.ShieldStrengthTowerStardust > 0 || NPC.ShieldStrengthTowerVortex > 0))
					{
						NPC.LunarApocalypseIsUp = false;
						NPC.ShieldStrengthTowerNebula = 0;
						NPC.ShieldStrengthTowerSolar = 0;
						NPC.ShieldStrengthTowerStardust = 0;
						NPC.ShieldStrengthTowerVortex = 0;
						for (int i = 0; i < 200; i++)
						{
							if (Main.npc[i].active && (Main.npc[i].type == 507 || Main.npc[i].type == 517 || Main.npc[i].type == 493 || Main.npc[i].type == 422))
							{
								Main.npc[i].dontTakeDamage = false;
								Main.npc[i].StrikeInstantKill();
							}
						}
					}
					if (Main.IsItRaining || Main.IsItStorming)
					{
						Main.StopRain();
						Main.cloudAlpha = 0f;
						if (Main.netMode == 2)
						{
							Main.SyncRain();
						}
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 5)
				{
					player.GetModPlayer<QoLCPlayer>().noSpawns = true;
					if (Main.invasionType != 0)
					{
						Main.invasionType = 0;
					}
					if (Main.pumpkinMoon)
					{
						Main.pumpkinMoon = false;
					}
					if (Main.snowMoon)
					{
						Main.snowMoon = false;
					}
					if (Main.eclipse)
					{
						Main.eclipse = false;
					}
					if (Main.bloodMoon)
					{
						Main.bloodMoon = false;
					}
					if (Main.WindyEnoughForKiteDrops)
					{
						Main.windSpeedTarget = 0f;
						Main.windSpeedCurrent = 0f;
					}
					if (Main.slimeRain)
					{
						Main.StopSlimeRain(true);
						Main.slimeWarningDelay = 1;
						Main.slimeWarningTime = 1;
					}
					if (BirthdayParty.PartyIsUp)
					{
						BirthdayParty.CheckNight();
					}
					if (DD2Event.Ongoing && Main.netMode != 1)
					{
						DD2Event.StopInvasion(false);
					}
					if (Sandstorm.Happening)
					{
						Sandstorm.Happening = false;
						Sandstorm.TimeLeft = 0.0;
						Sandstorm.IntendedSeverity = 0f;
					}
					if (NPC.downedTowers && (NPC.LunarApocalypseIsUp || NPC.ShieldStrengthTowerNebula > 0 || NPC.ShieldStrengthTowerSolar > 0 || NPC.ShieldStrengthTowerStardust > 0 || NPC.ShieldStrengthTowerVortex > 0))
					{
						NPC.LunarApocalypseIsUp = false;
						NPC.ShieldStrengthTowerNebula = 0;
						NPC.ShieldStrengthTowerSolar = 0;
						NPC.ShieldStrengthTowerStardust = 0;
						NPC.ShieldStrengthTowerVortex = 0;
						for (int j = 0; j < 200; j++)
						{
							if (Main.npc[j].active && (Main.npc[j].type == 507 || Main.npc[j].type == 517 || Main.npc[j].type == 493 || Main.npc[j].type == 422))
							{
								Main.npc[j].dontTakeDamage = false;
								Main.npc[j].StrikeInstantKill();
							}
						}
					}
					if (Main.IsItRaining || Main.IsItStorming)
					{
						Main.StopRain();
						Main.cloudAlpha = 0f;
						if (Main.netMode == 2)
						{
							Main.SyncRain();
						}
					}
				}
			}
		}
	}
}
