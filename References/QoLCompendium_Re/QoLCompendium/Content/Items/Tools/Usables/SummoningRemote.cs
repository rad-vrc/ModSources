using System;
using System.Collections.Generic;
using Humanizer;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Other;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using QoLCompendium.Core.UI.Panels;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x02000185 RID: 389
	public class SummoningRemote : ModItem
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x000160E3 File Offset: 0x000142E3
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.SummoningRemote;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000160FD File Offset: 0x000142FD
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 0;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00016118 File Offset: 0x00014318
		public override void SetDefaults()
		{
			base.Item.width = 7;
			base.Item.height = 17;
			base.Item.useStyle = 4;
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
			base.Item.SetShopValues(ItemRarityColor.White0, Item.buyPrice(0, 1, 0, 0));
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00016189 File Offset: 0x00014389
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.SummoningRemote);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000161A4 File Offset: 0x000143A4
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				return false;
			}
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			if (player.GetModPlayer<QoLCPlayer>().bossToSpawn != 0 && player.GetModPlayer<QoLCPlayer>().bossSpawn)
			{
				if (player.GetModPlayer<QoLCPlayer>().bossToSpawn == 113)
				{
					return false;
				}
				if (player.GetModPlayer<QoLCPlayer>().bossToSpawn == 125)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, 126f, 0f, 0f);
				}
				Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, (float)player.GetModPlayer<QoLCPlayer>().bossToSpawn, 0f, 0f);
				SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn != 0 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 14)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, 507f, 0f, 0f);
					SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 15)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, 517f, 0f, 0f);
					SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 16)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, 493f, 0f, 0f);
					SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 17)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, 422f, 0f, 0f);
					SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
				}
			}
			return false;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00016444 File Offset: 0x00014644
		public override bool? UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (!SummoningRemoteUI.visible)
				{
					SummoningRemoteUI.timeStart = Main.GameUpdateCount;
				}
				SummoningRemoteUI.visible = true;
				SoundEngine.PlaySound(SoundID.MenuOpen, new Vector2?(player.Center), null);
				return new bool?(true);
			}
			if (player.GetModPlayer<QoLCPlayer>().bossToSpawn != 0 && player.GetModPlayer<QoLCPlayer>().bossSpawn && player.GetModPlayer<QoLCPlayer>().bossToSpawn == 113)
			{
				NPC.SpawnWOF(player.Center);
				SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
				return new bool?(true);
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn != 0 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 1)
				{
					LanternNight.GenuineLanterns = false;
					LanternNight.ManualLanterns = false;
					Main.rainTime = (double)(86400 / 24 * 12);
					Main.raining = true;
					Main.maxRaining = (Main.cloudAlpha = 0.9f);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						Main.SyncRain();
					}
					TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventRain"), new Color(50, 255, 130), false);
					SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 2)
				{
					Main.windSpeedTarget = (Main.windSpeedCurrent = 0.8f);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventWind"), new Color(50, 255, 130), false);
					SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 3)
				{
					Sandstorm.StartSandstorm();
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventSandstorm"), new Color(50, 255, 130), false);
					SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 4)
				{
					BirthdayParty.PartyDaysOnCooldown = 0;
					if (Main.netMode != 1)
					{
						int checks = 0;
						while (checks < 100 && !BirthdayParty.PartyIsUp)
						{
							BirthdayParty.CheckMorning();
							checks++;
						}
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 5)
				{
					Main.StartSlimeRain(true);
					Main.slimeWarningDelay = 1;
					Main.slimeWarningTime = 1;
					SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 6 && !Main.dayTime)
				{
					Main.bloodMoon = true;
					SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
					TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventBloodMoon"), new Color(50, 255, 130), false);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 7)
				{
					Main.StartInvasion(1);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 8)
				{
					Main.StartInvasion(2);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 9)
				{
					Main.StartInvasion(3);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 10 && Main.dayTime)
				{
					Main.eclipse = true;
					SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
					TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventEclipse"), new Color(50, 255, 130), false);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 11 && !Main.dayTime)
				{
					Main.startPumpkinMoon();
					TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventPumpkinMoon"), new Color(50, 255, 130), false);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 12 && !Main.dayTime)
				{
					Main.startSnowMoon();
					TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventFrostMoon"), new Color(50, 255, 130), false);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 13)
				{
					Main.StartInvasion(4);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 18)
				{
					WorldGen.TriggerLunarApocalypse();
					NPC.LunarApocalypseIsUp = true;
				}
			}
			return new bool?(true);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000169F4 File Offset: 0x00014BF4
		public override void UpdateInventory(Player player)
		{
			if (player.GetModPlayer<QoLCPlayer>().bossToSpawn > 0 && player.GetModPlayer<QoLCPlayer>().bossToSpawn != 125 && player.GetModPlayer<QoLCPlayer>().bossToSpawn != 398 && player.GetModPlayer<QoLCPlayer>().bossSpawn)
			{
				base.Item.SetNameOverride(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.Boss"), new object[]
				{
					ContentSamples.NpcsByNetId[player.GetModPlayer<QoLCPlayer>().bossToSpawn].FullName
				}));
			}
			if (player.GetModPlayer<QoLCPlayer>().bossToSpawn == 125 && player.GetModPlayer<QoLCPlayer>().bossSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.BossTwins"));
			}
			if (player.GetModPlayer<QoLCPlayer>().bossToSpawn == 398 && player.GetModPlayer<QoLCPlayer>().bossSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.BossMoonLord"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 1 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventRain"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 2 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventWind"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 3 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventSandstorm"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 4 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventParty"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 5 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventSlimeRain"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 6 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventBloodMoon"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 7 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventGoblinArmy"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 8 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventSnowLegion"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 9 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventPirateInvasion"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 10 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventEclipse"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 11 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventPumpkinMoon"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 12 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventFrostMoon"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 13 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventMartianMadness"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 14 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventNebulaPillar"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 15 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventSolarPillar"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 16 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventStardustPillar"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 17 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventVortexPillar"));
			}
			if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 18 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventLunar"));
			}
			if (!player.GetModPlayer<QoLCPlayer>().bossSpawn && !player.GetModPlayer<QoLCPlayer>().eventSpawn)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.NoModifier"));
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00002430 File Offset: 0x00000630
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00016E78 File Offset: 0x00015078
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.SummoningRemote, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
			itemRecipe.AddIngredient(178, 5);
			itemRecipe.AddIngredient(38, 2);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
