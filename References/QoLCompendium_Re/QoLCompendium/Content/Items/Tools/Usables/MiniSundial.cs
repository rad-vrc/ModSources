using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x0200017F RID: 383
	public class MiniSundial : ModItem
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x00015552 File Offset: 0x00013752
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.MiniSundial;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001556C File Offset: 0x0001376C
		public override void SetDefaults()
		{
			base.Item.width = 12;
			base.Item.height = 12;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.Item4);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 0, 90, 0));
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000155E4 File Offset: 0x000137E4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.MiniSundial);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000155FC File Offset: 0x000137FC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.MiniSundial, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(173, 12);
			itemRecipe.AddIngredient(824, 12);
			itemRecipe.AddTile(305);
			itemRecipe.Register();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00002430 File Offset: 0x00000630
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00015667 File Offset: 0x00013867
		public override bool CanUseItem(Player player)
		{
			return !Main.IsFastForwardingTime();
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00015674 File Offset: 0x00013874
		public override bool? UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Main.sundialCooldown = 0;
				SoundEngine.PlaySound(SoundID.Item4, new Vector2?(player.position), null);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(51, -1, -1, null, Main.myPlayer, 3f, 0f, 0f, 0, 0, 0);
					return new bool?(true);
				}
				if (Main.dayTime)
				{
					Main.fastForwardTimeToDusk = true;
				}
				else
				{
					Main.fastForwardTimeToDawn = true;
				}
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			else
			{
				int noon = 27000;
				int midnight = 16200;
				if (Main.dayTime && Main.time < (double)noon)
				{
					Main.time = (double)noon;
				}
				else if (Main.time < (double)midnight)
				{
					Main.time = (double)midnight;
				}
				else
				{
					Main.dayTime = !Main.dayTime;
					Main.time = 0.0;
					if (Main.dayTime)
					{
						BirthdayParty.CheckMorning();
						Chest.SetupTravelShop();
					}
					else
					{
						BirthdayParty.CheckNight();
						if (!Main.dayTime && ++Main.moonPhase > 7)
						{
							Main.moonPhase = 0;
						}
					}
				}
			}
			return new bool?(true);
		}
	}
}
