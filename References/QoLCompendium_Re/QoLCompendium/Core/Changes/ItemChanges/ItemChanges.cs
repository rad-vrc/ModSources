using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ItemChanges
{
	// Token: 0x0200025E RID: 606
	public class ItemChanges : GlobalItem
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x00070F2D File Offset: 0x0006F12D
		public override void Load()
		{
			On_Player.ItemCheck_UseMiningTools_TryHittingWall += delegate(On_Player.orig_ItemCheck_UseMiningTools_TryHittingWall orig, Player player, Item item, int x, int y)
			{
				orig.Invoke(player, item, x, y);
				if (player.itemTime == item.useTime / 2)
				{
					player.itemTime = (int)Math.Max(1f, (1f - QoLCompendium.mainConfig.IncreaseToolSpeed) * ((float)item.useTime / 2f));
				}
			};
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00070F54 File Offset: 0x0006F154
		public override void SetDefaults(Item item)
		{
			if (QoLCompendium.mainConfig.NoDeveloperSetsFromBossBags && ItemID.Sets.BossBag[item.type])
			{
				ItemID.Sets.PreHardmodeLikeBossBag[item.type] = true;
			}
			if (QoLCompendium.mainConfig.IncreaseMaxStack > 0 && item.maxStack > 10 && item.maxStack != 100 && !Common.IsCoin(item.type))
			{
				item.maxStack = QoLCompendium.mainConfig.IncreaseMaxStack;
			}
			if (QoLCompendium.mainConfig.StackableQuestItems && item.questItem && QoLCompendium.mainConfig.IncreaseMaxStack > 0)
			{
				item.maxStack = QoLCompendium.mainConfig.IncreaseMaxStack;
			}
			if (QoLCompendium.mainConfig.AutoReuseUpgrades && Common.PermanentMultiUseUpgrades.Contains(item.type))
			{
				item.autoReuse = true;
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0007101C File Offset: 0x0006F21C
		public override void ExtractinatorUse(int extractType, int extractinatorBlockType, ref int resultType, ref int resultStack)
		{
			if (QoLCompendium.mainConfig.FasterExtractinator)
			{
				Main.LocalPlayer.itemAnimation = 1;
				Main.LocalPlayer.itemTime = 1;
				Main.LocalPlayer.itemTimeMax = 1;
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0007104C File Offset: 0x0006F24C
		public override float UseTimeMultiplier(Item item, Player player)
		{
			if (item.pick > 0 || item.hammer > 0 || item.axe > 0 || item.type == 510)
			{
				return 1f - QoLCompendium.mainConfig.IncreaseToolSpeed;
			}
			return base.UseTimeMultiplier(item, player);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0007109A File Offset: 0x0006F29A
		public override bool ReforgePrice(Item item, ref int reforgePrice, ref bool canApplyDiscount)
		{
			reforgePrice = (int)((float)reforgePrice * (1f - (float)QoLCompendium.mainConfig.ReforgePriceChange * 0.01f));
			return true;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000710BC File Offset: 0x0006F2BC
		public override bool ConsumeItem(Item item, Player player)
		{
			return (!item.consumable || item.damage != -1 || item.stack < QoLCompendium.mainConfig.EndlessItemAmount || !QoLCompendium.mainConfig.EndlessConsumables) && (!item.consumable || item.damage <= 0 || item.stack < QoLCompendium.mainConfig.EndlessWeaponAmount || !QoLCompendium.mainConfig.EndlessWeapons) && base.ConsumeItem(item, player);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00071133 File Offset: 0x0006F333
		public override bool CanBeConsumedAsAmmo(Item ammo, Item weapon, Player player)
		{
			return (ammo.ammo <= AmmoID.None || ammo.stack < QoLCompendium.mainConfig.EndlessAmmoAmount || !QoLCompendium.mainConfig.EndlessAmmo) && base.CanBeConsumedAsAmmo(ammo, weapon, player);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0007116B File Offset: 0x0006F36B
		public override bool? CanConsumeBait(Player player, Item bait)
		{
			if (bait.bait > 0 && bait.stack >= QoLCompendium.mainConfig.EndlessBaitAmount && QoLCompendium.mainConfig.EndlessBait)
			{
				return new bool?(false);
			}
			return base.CanConsumeBait(player, bait);
		}
	}
}
