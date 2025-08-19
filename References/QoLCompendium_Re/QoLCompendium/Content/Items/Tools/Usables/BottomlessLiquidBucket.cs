using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x0200017A RID: 378
	public class BottomlessLiquidBucket : ModItem
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x00014A48 File Offset: 0x00012C48
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BottomlessBuckets;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00014A64 File Offset: 0x00012C64
		public override void SetDefaults()
		{
			base.Item.useStyle = 1;
			base.Item.useTurn = true;
			base.Item.useAnimation = 12;
			base.Item.useTime = 12;
			base.Item.autoReuse = true;
			base.Item.width = 15;
			base.Item.height = 14;
			base.Item.SetShopValues(ItemRarityColor.Yellow8, Item.sellPrice(0, 20, 0, 0));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00014ADF File Offset: 0x00012CDF
		public override void SaveData(TagCompound tag)
		{
			tag["BottomlessLiquidBucketMode"] = this.Mode;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00014AF7 File Offset: 0x00012CF7
		public override void LoadData(TagCompound tag)
		{
			this.Mode = tag.GetInt("BottomlessLiquidBucketMode");
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00002430 File Offset: 0x00000630
		public override bool CanRightClick()
		{
			return true;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00014B0A File Offset: 0x00012D0A
		public override void RightClick(Player player)
		{
			this.Mode++;
			if (this.Mode > 3)
			{
				this.Mode = 0;
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override void OnConsumeItem(Player player)
		{
			base.Item.stack++;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00014B2C File Offset: 0x00012D2C
		public override bool? UseItem(Player player)
		{
			if (this.Mode == 0)
			{
				BottomlessLiquidBucket.UseBucket(player, BottomlessLiquidBucket.LiquidTypes.Water);
			}
			if (this.Mode == 1)
			{
				BottomlessLiquidBucket.UseBucket(player, BottomlessLiquidBucket.LiquidTypes.Lava);
			}
			if (this.Mode == 2)
			{
				BottomlessLiquidBucket.UseBucket(player, BottomlessLiquidBucket.LiquidTypes.Honey);
			}
			if (this.Mode == 3)
			{
				BottomlessLiquidBucket.UseBucket(player, BottomlessLiquidBucket.LiquidTypes.Shimmer);
			}
			return new bool?(true);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00014B84 File Offset: 0x00012D84
		public override void UpdateInventory(Player player)
		{
			if (this.Mode == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.BottomlessLiquidBucket.Water"));
			}
			if (this.Mode == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.BottomlessLiquidBucket.Lava"));
			}
			if (this.Mode == 2)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.BottomlessLiquidBucket.Honey"));
			}
			if (this.Mode == 3)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.BottomlessLiquidBucket.Shimmer"));
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00014C08 File Offset: 0x00012E08
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.BottomlessBuckets);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00014C20 File Offset: 0x00012E20
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.BottomlessBuckets, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(3031, 1);
			itemRecipe.AddIngredient(4820, 1);
			itemRecipe.AddIngredient(5302, 1);
			itemRecipe.AddIngredient(5364, 1);
			itemRecipe.AddTile(283);
			itemRecipe.Register();
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00014CA4 File Offset: 0x00012EA4
		internal unsafe static bool PlaceLiquid(int x, int y, BottomlessLiquidBucket.LiquidTypes type)
		{
			Tile tileSafely = Framing.GetTileSafely(x, y);
			if (*tileSafely.LiquidAmount < 230 && (!tileSafely.HasUnactuatedTile || !Main.tileSolid[(int)(*tileSafely.TileType)] || Main.tileSolidTop[(int)(*tileSafely.TileType)]) && (*tileSafely.LiquidAmount == 0 || tileSafely.LiquidType == (int)type))
			{
				tileSafely.LiquidType = (int)type;
				*tileSafely.LiquidAmount = byte.MaxValue;
				WorldGen.SquareTileFrame(x, y, true);
				if (Main.netMode != 0)
				{
					NetMessage.sendWater(x, y);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00014D34 File Offset: 0x00012F34
		internal static bool UseBucket(Player player, BottomlessLiquidBucket.LiquidTypes type)
		{
			if (player.whoAmI == Main.myPlayer && !player.noBuilding && BottomlessLiquidBucket.PlaceLiquid(Player.tileTargetX, Player.tileTargetY, type))
			{
				SoundEngine.PlaySound(SoundID.Item19, new Vector2?(player.Center), null);
			}
			return true;
		}

		// Token: 0x04000036 RID: 54
		public int Mode;

		// Token: 0x020004B6 RID: 1206
		internal enum LiquidTypes : byte
		{
			// Token: 0x040009B6 RID: 2486
			Water,
			// Token: 0x040009B7 RID: 2487
			Lava,
			// Token: 0x040009B8 RID: 2488
			Honey,
			// Token: 0x040009B9 RID: 2489
			Shimmer
		}
	}
}
