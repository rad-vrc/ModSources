using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using QoLCompendium.Core.UI.Other;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace QoLCompendium.Content.Items.Tools.Mirrors
{
	// Token: 0x020001A5 RID: 421
	public class MosaicMirror : ModItem
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0001A84F File Offset: 0x00018A4F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Mirrors;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001AD38 File Offset: 0x00018F38
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(50);
			base.Item.SetShopValues(ItemRarityColor.Lime7, Item.buyPrice(0, 10, 0, 0));
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001AD60 File Offset: 0x00018F60
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Mirrors);
			if (!QoLCompendium.itemConfig.InformationAccessories)
			{
				TooltipLine tooltipLine = tooltips.Find((TooltipLine l) => l.Name == "Tooltip1");
				tooltipLine.Text = tooltipLine.Text + " " + Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Disabled");
				tooltipLine.OverrideColor = new Color?(Color.Red);
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override void OnConsumeItem(Player player)
		{
			base.Item.stack++;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001ADE3 File Offset: 0x00018FE3
		public override void SaveData(TagCompound tag)
		{
			tag["MosaicMirrorMode"] = this.Mode;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001ADFB File Offset: 0x00018FFB
		public override void LoadData(TagCompound tag)
		{
			this.Mode = tag.GetInt("MosaicMirrorMode");
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001AE10 File Offset: 0x00019010
		public override void UpdateInventory(Player player)
		{
			player.GetModPlayer<QoLCPlayer>().warpMirror = true;
			if (this.Mode == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MosaicMirror.CursedMirror"));
			}
			if (this.Mode == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MosaicMirror.MirrorOfReturn"));
			}
			if (this.Mode == 2)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MosaicMirror.TeleportationMirror"));
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001AE84 File Offset: 0x00019084
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (this.Mode == 0)
			{
				if (Main.rand.NextBool())
				{
					Dust.NewDust(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Yellow, 1.1f);
				}
				if (player.itemTime == 0)
				{
					player.ApplyItemTime(base.Item, 1f, null);
					return;
				}
				if (player.itemTime == player.itemTimeMax / 2)
				{
					for (int i = 0; i < 70; i++)
					{
						Dust.NewDust(player.position, player.width, player.height, 15, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default(Color), 1.5f);
					}
					player.grappling[0] = -1;
					player.grapCount = 0;
					for (int j = 0; j < 1000; j++)
					{
						if (Main.projectile[j].active && Main.projectile[j].owner == player.whoAmI && Main.projectile[j].aiStyle == 7)
						{
							Main.projectile[j].Kill();
						}
					}
					if (player.lastDeathPostion.X != 0f && player.lastDeathPostion.Y != 0f)
					{
						Vector2 vector;
						vector..ctor(player.lastDeathPostion.X - 16f, player.lastDeathPostion.Y - 24f);
						player.Teleport(vector, 0, 0);
					}
					else if (player == Main.player[Main.myPlayer])
					{
						Main.NewText("No sign of recent death appears in the mirror", byte.MaxValue, byte.MaxValue, byte.MaxValue);
					}
					for (int k = 0; k < 70; k++)
					{
						Dust.NewDust(player.position, player.width, player.height, 15, 0f, 0f, 150, default(Color), 1.5f);
					}
				}
			}
			if (this.Mode == 1)
			{
				if (Main.rand.NextBool())
				{
					Dust.NewDustDirect(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Cyan, 1.1f).velocity *= 0.5f;
				}
				if (player.ItemTimeIsZero)
				{
					player.ApplyItemTime(base.Item, 1f, null);
				}
				if (player.itemTime == player.itemTimeMax / 2)
				{
					for (int l = 0; l < 70; l++)
					{
						Dust.NewDustDirect(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Cyan, 1.5f).velocity *= 0.5f;
					}
					player.grappling[0] = -1;
					player.grapCount = 0;
					for (int m = 0; m < Main.projectile.Length; m++)
					{
						if (Main.projectile[m].active && Main.projectile[m].owner == player.whoAmI && Main.projectile[m].aiStyle == 7)
						{
							Main.projectile[m].Kill();
						}
					}
					player.DoPotionOfReturnTeleportationAndSetTheComebackPoint();
					for (int n = 0; n < 70; n++)
					{
						Dust.NewDustDirect(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Cyan, 1.5f).velocity *= 0.5f;
					}
				}
			}
			if (this.Mode == 2)
			{
				if (Main.rand.NextBool())
				{
					Dust.NewDustDirect(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Cyan, 1.1f).velocity *= 0.5f;
				}
				if (player.ItemTimeIsZero)
				{
					player.ApplyItemTime(base.Item, 1f, null);
				}
				if (player.itemTime == player.itemTimeMax / 2)
				{
					for (int i2 = 0; i2 < 70; i2++)
					{
						Dust.NewDustDirect(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Cyan, 1.5f).velocity *= 0.5f;
					}
					player.grappling[0] = -1;
					player.grapCount = 0;
					for (int i3 = 0; i3 < Main.projectile.Length; i3++)
					{
						if (Main.projectile[i3].active && Main.projectile[i3].owner == player.whoAmI && Main.projectile[i3].aiStyle == 7)
						{
							Main.projectile[i3].Kill();
						}
					}
					player.TeleportationPotion();
					for (int i4 = 0; i4 < 70; i4++)
					{
						Dust.NewDustDirect(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Cyan, 1.5f).velocity *= 0.5f;
					}
				}
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00002430 File Offset: 0x00000630
		public override bool CanRightClick()
		{
			return true;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001B3F4 File Offset: 0x000195F4
		public override void RightClick(Player player)
		{
			this.Mode++;
			if (this.Mode > 2)
			{
				this.Mode = 0;
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001B414 File Offset: 0x00019614
		public override void UpdateInfoAccessory(Player player)
		{
			if (!QoLCompendium.itemConfig.InformationAccessories)
			{
				return;
			}
			player.GetModPlayer<InfoPlayer>().battalionLog = true;
			player.GetModPlayer<InfoPlayer>().harmInducer = true;
			player.GetModPlayer<InfoPlayer>().headCounter = true;
			player.GetModPlayer<InfoPlayer>().kettlebell = true;
			player.GetModPlayer<InfoPlayer>().luckyDie = true;
			player.GetModPlayer<InfoPlayer>().metallicClover = true;
			player.GetModPlayer<InfoPlayer>().plateCracker = true;
			player.GetModPlayer<InfoPlayer>().regenerator = true;
			player.GetModPlayer<InfoPlayer>().reinforcedPanel = true;
			player.GetModPlayer<InfoPlayer>().replenisher = true;
			player.GetModPlayer<InfoPlayer>().trackingDevice = true;
			player.GetModPlayer<InfoPlayer>().wingTimer = true;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001B4C0 File Offset: 0x000196C0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Mirrors, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<CursedMirror>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<MirrorOfReturn>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<TeleportationMirror>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<WarpMirror>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<WormholeMirror>(), 1);
			itemRecipe.AddTile(114);
			itemRecipe.Register();
		}

		// Token: 0x04000040 RID: 64
		public int Mode;
	}
}
