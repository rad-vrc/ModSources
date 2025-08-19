using System;
using QoLCompendium.Content.Projectiles.MobileStorages;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.PlayerChanges
{
	// Token: 0x0200022D RID: 557
	public class BankPlayer : ModPlayer
	{
		// Token: 0x06000D6B RID: 3435 RVA: 0x000683B0 File Offset: 0x000665B0
		public override void SetControls()
		{
			if (base.Player.whoAmI == Main.myPlayer && this.chests)
			{
				Main.SmartCursorShowing = false;
				Player.tileRangeX = 9999;
				Player.tileRangeY = 5000;
				if (base.Player.chest >= -1)
				{
					this.pig = -1;
					this.safe = -1;
					this.defenders = -1;
					this.vault = -1;
					this.chests = false;
				}
				if (this.safe != -1 && Main.projectile[this.safe].type != ModContent.ProjectileType<FlyingSafeProjectile>())
				{
					this.safe = -1;
					this.chests = false;
				}
				if (this.defenders != -1 && Main.projectile[this.defenders].type != ModContent.ProjectileType<EtherianConstructProjectile>())
				{
					this.defenders = -1;
					this.chests = false;
				}
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00068488 File Offset: 0x00066688
		public override void UpdateEquips()
		{
			if (QoLCompendium.mainConfig.UtilityAccessoriesWorkInBanks)
			{
				for (int i = 0; i < base.Player.bank.item.Length; i++)
				{
					this.CheckForBankItems(base.Player.bank.item[i]);
				}
				for (int j = 0; j < base.Player.bank2.item.Length; j++)
				{
					this.CheckForBankItems(base.Player.bank2.item[j]);
				}
				for (int k = 0; k < base.Player.bank3.item.Length; k++)
				{
					this.CheckForBankItems(base.Player.bank3.item[k]);
				}
				for (int l = 0; l < base.Player.bank4.item.Length; l++)
				{
					this.CheckForBankItems(base.Player.bank4.item[l]);
				}
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00068578 File Offset: 0x00066778
		public void CheckForBankItems(Item item)
		{
			if (Common.BankItems.Contains(item.type))
			{
				if (item.type == Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ElectromagneticDeterrent"))
				{
					return;
				}
				base.Player.GetModPlayer<QoLCPlayer>().activeItems.Add(item.type);
				base.Player.ApplyEquipFunctional(item, true);
				base.Player.RefreshInfoAccsFromItemType(item);
				base.Player.RefreshMechanicalAccsFromItemType(item.type);
				if (item.type == 3090)
				{
					base.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "Clot")] = true;
					base.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "GelatinousCube")] = true;
					base.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "GelatinousSludge")] = true;
					base.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "GildedSlime")] = true;
					base.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "GildedSlimeling")] = true;
					base.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "GraniteFusedSlime")] = true;
					base.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "LivingHemorrhage")] = true;
					base.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "SpaceSlime")] = true;
					base.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "CrownofThorns")] = true;
					base.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "BloodDrop")] = true;
				}
			}
		}

		// Token: 0x04000593 RID: 1427
		internal bool chests;

		// Token: 0x04000594 RID: 1428
		internal int pig = -1;

		// Token: 0x04000595 RID: 1429
		internal int safe = -1;

		// Token: 0x04000596 RID: 1430
		internal int defenders = -1;

		// Token: 0x04000597 RID: 1431
		internal int vault = -1;
	}
}
