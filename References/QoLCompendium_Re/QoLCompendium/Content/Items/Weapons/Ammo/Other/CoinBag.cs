using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Weapons.Ammo.Other
{
	// Token: 0x02000054 RID: 84
	public abstract class CoinBag : BaseAmmo
	{
		// Token: 0x06000185 RID: 389 RVA: 0x000089FC File Offset: 0x00006BFC
		public override void SetDefaults()
		{
			base.Item.notAmmo = false;
			base.Item.useStyle = 0;
			base.Item.useTime = 0;
			base.Item.useAnimation = 0;
			base.Item.createTile = -1;
			base.Item.shoot = 0;
		}

		// Token: 0x020003AA RID: 938
		private class EndlessCopperCoinPouch : CoinBag
		{
			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06001397 RID: 5015 RVA: 0x0008ED67 File Offset: 0x0008CF67
			public override int AmmunitionItem
			{
				get
				{
					return 71;
				}
			}
		}

		// Token: 0x020003AB RID: 939
		private class EndlessSilverCoinPouch : CoinBag
		{
			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06001399 RID: 5017 RVA: 0x0008ED73 File Offset: 0x0008CF73
			public override int AmmunitionItem
			{
				get
				{
					return 72;
				}
			}
		}

		// Token: 0x020003AC RID: 940
		private class EndlessGoldCoinPouch : CoinBag
		{
			// Token: 0x1700023E RID: 574
			// (get) Token: 0x0600139B RID: 5019 RVA: 0x0008ED77 File Offset: 0x0008CF77
			public override int AmmunitionItem
			{
				get
				{
					return 73;
				}
			}
		}

		// Token: 0x020003AD RID: 941
		private class EndlessPlatinumCoinPouch : CoinBag
		{
			// Token: 0x1700023F RID: 575
			// (get) Token: 0x0600139D RID: 5021 RVA: 0x0008ED7B File Offset: 0x0008CF7B
			public override int AmmunitionItem
			{
				get
				{
					return 74;
				}
			}
		}

		// Token: 0x020003AE RID: 942
		public class EndlessCandyCornPie : BaseAmmo
		{
			// Token: 0x17000240 RID: 576
			// (get) Token: 0x0600139F RID: 5023 RVA: 0x0008ED7F File Offset: 0x0008CF7F
			public override int AmmunitionItem
			{
				get
				{
					return 1783;
				}
			}
		}

		// Token: 0x020003AF RID: 943
		public class EndlessExplosiveJackOLantern : BaseAmmo
		{
			// Token: 0x17000241 RID: 577
			// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0008ED86 File Offset: 0x0008CF86
			public override int AmmunitionItem
			{
				get
				{
					return 1785;
				}
			}
		}

		// Token: 0x020003B0 RID: 944
		public class EndlessGelTank : ModItem
		{
			// Token: 0x060013A3 RID: 5027 RVA: 0x000086BD File Offset: 0x000068BD
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.EndlessAmmo;
			}

			// Token: 0x060013A4 RID: 5028 RVA: 0x000086D7 File Offset: 0x000068D7
			public override void SetStaticDefaults()
			{
				base.Item.ResearchUnlockCount = 1;
			}

			// Token: 0x060013A5 RID: 5029 RVA: 0x0008ED90 File Offset: 0x0008CF90
			public override void SetDefaults()
			{
				base.Item.ammo = AmmoID.Gel;
				base.Item.knockBack = 0.5f;
				base.Item.DamageType = DamageClass.Ranged;
				base.Item.SetShopValues(ItemRarityColor.Green2, Item.sellPrice(0, 1, 0, 0));
			}

			// Token: 0x060013A6 RID: 5030 RVA: 0x0000874D File Offset: 0x0000694D
			public override void ModifyTooltips(List<TooltipLine> tooltips)
			{
				TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.EndlessAmmo);
			}

			// Token: 0x060013A7 RID: 5031 RVA: 0x0008EDE4 File Offset: 0x0008CFE4
			public override void AddRecipes()
			{
				Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.EndlessAmmo, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
				itemRecipe.AddIngredient(23, 3996);
				itemRecipe.AddTile(220);
				itemRecipe.Register();
			}
		}

		// Token: 0x020003B1 RID: 945
		public class EndlessNailPouch : BaseAmmo
		{
			// Token: 0x17000242 RID: 578
			// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0008EE41 File Offset: 0x0008D041
			public override int AmmunitionItem
			{
				get
				{
					return 3108;
				}
			}
		}

		// Token: 0x020003B2 RID: 946
		public class EndlessSandPouch : ModItem
		{
			// Token: 0x060013AB RID: 5035 RVA: 0x000086BD File Offset: 0x000068BD
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.EndlessAmmo;
			}

			// Token: 0x060013AC RID: 5036 RVA: 0x000086D7 File Offset: 0x000068D7
			public override void SetStaticDefaults()
			{
				base.Item.ResearchUnlockCount = 1;
			}

			// Token: 0x060013AD RID: 5037 RVA: 0x0008EE48 File Offset: 0x0008D048
			public override void SetDefaults()
			{
				base.Item.CloneDefaults(169);
				base.Item.consumable = false;
				base.Item.maxStack = 1;
				base.Item.useStyle = 0;
				base.Item.useTime = 0;
				base.Item.useAnimation = 0;
				base.Item.createTile = -1;
				base.Item.shoot = 0;
				base.Item.SetShopValues(ItemRarityColor.Green2, Item.sellPrice(0, 1, 0, 0));
			}

			// Token: 0x060013AE RID: 5038 RVA: 0x0000874D File Offset: 0x0000694D
			public override void ModifyTooltips(List<TooltipLine> tooltips)
			{
				TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.EndlessAmmo);
			}

			// Token: 0x060013AF RID: 5039 RVA: 0x0000404D File Offset: 0x0000224D
			public override bool CanUseItem(Player player)
			{
				return false;
			}

			// Token: 0x060013B0 RID: 5040 RVA: 0x0008EED0 File Offset: 0x0008D0D0
			public override void AddRecipes()
			{
				Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.EndlessAmmo, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
				itemRecipe.AddIngredient(169, 3996);
				itemRecipe.AddTile(220);
				itemRecipe.Register();
			}
		}

		// Token: 0x020003B3 RID: 947
		public class EndlessSnowballPouch : ModItem
		{
			// Token: 0x060013B2 RID: 5042 RVA: 0x000086BD File Offset: 0x000068BD
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.EndlessAmmo;
			}

			// Token: 0x060013B3 RID: 5043 RVA: 0x000086D7 File Offset: 0x000068D7
			public override void SetStaticDefaults()
			{
				base.Item.ResearchUnlockCount = 1;
			}

			// Token: 0x060013B4 RID: 5044 RVA: 0x0008EF30 File Offset: 0x0008D130
			public override void SetDefaults()
			{
				base.Item.CloneDefaults(949);
				base.Item.consumable = false;
				base.Item.maxStack = 1;
				base.Item.shoot = 0;
				base.Item.useAnimation = 0;
				base.Item.useTime = 0;
				base.Item.useStyle = 0;
				base.Item.SetShopValues(ItemRarityColor.Green2, Item.sellPrice(0, 1, 0, 0));
			}

			// Token: 0x060013B5 RID: 5045 RVA: 0x0000874D File Offset: 0x0000694D
			public override void ModifyTooltips(List<TooltipLine> tooltips)
			{
				TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.EndlessAmmo);
			}

			// Token: 0x060013B6 RID: 5046 RVA: 0x0000404D File Offset: 0x0000224D
			public override bool CanUseItem(Player player)
			{
				return false;
			}

			// Token: 0x060013B7 RID: 5047 RVA: 0x0008EFAC File Offset: 0x0008D1AC
			public override void AddRecipes()
			{
				Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.EndlessAmmo, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
				itemRecipe.AddIngredient(949, 3996);
				itemRecipe.AddTile(220);
				itemRecipe.Register();
			}
		}

		// Token: 0x020003B4 RID: 948
		public class EndlessStakeBundle : BaseAmmo
		{
			// Token: 0x17000243 RID: 579
			// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0008F00C File Offset: 0x0008D20C
			public override int AmmunitionItem
			{
				get
				{
					return 1836;
				}
			}
		}

		// Token: 0x020003B5 RID: 949
		public class EndlessStarPouch : ModItem
		{
			// Token: 0x060013BB RID: 5051 RVA: 0x000086BD File Offset: 0x000068BD
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.EndlessAmmo;
			}

			// Token: 0x060013BC RID: 5052 RVA: 0x000086D7 File Offset: 0x000068D7
			public override void SetStaticDefaults()
			{
				base.Item.ResearchUnlockCount = 1;
			}

			// Token: 0x060013BD RID: 5053 RVA: 0x0008F013 File Offset: 0x0008D213
			public override void SetDefaults()
			{
				base.Item.ammo = AmmoID.FallenStar;
				base.Item.DamageType = DamageClass.Ranged;
				base.Item.SetShopValues(ItemRarityColor.Green2, Item.sellPrice(0, 1, 0, 0));
			}

			// Token: 0x060013BE RID: 5054 RVA: 0x0000874D File Offset: 0x0000694D
			public override void ModifyTooltips(List<TooltipLine> tooltips)
			{
				TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.EndlessAmmo);
			}

			// Token: 0x060013BF RID: 5055 RVA: 0x0000404D File Offset: 0x0000224D
			public override bool CanUseItem(Player player)
			{
				return false;
			}

			// Token: 0x060013C0 RID: 5056 RVA: 0x0008F04C File Offset: 0x0008D24C
			public override void AddRecipes()
			{
				Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.EndlessAmmo, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
				itemRecipe.AddIngredient(75, 3996);
				itemRecipe.AddTile(220);
				itemRecipe.Register();
			}
		}

		// Token: 0x020003B6 RID: 950
		public class EndlessStyngerBoltQuiver : BaseAmmo
		{
			// Token: 0x17000244 RID: 580
			// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0008F0A9 File Offset: 0x0008D2A9
			public override int AmmunitionItem
			{
				get
				{
					return 1261;
				}
			}
		}
	}
}
