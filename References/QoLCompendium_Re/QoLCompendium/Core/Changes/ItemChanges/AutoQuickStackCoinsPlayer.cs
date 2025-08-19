using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ItemChanges
{
	// Token: 0x0200025B RID: 603
	public class AutoQuickStackCoinsPlayer : ModPlayer
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x00070BA0 File Offset: 0x0006EDA0
		public override void PostUpdate()
		{
			this.cooldown++;
			if (Main.myPlayer != base.Player.whoAmI)
			{
				return;
			}
			if (!QoLCompendium.mainConfig.AutoMoneyQuickStack)
			{
				return;
			}
			if (this.cooldown % 10 == 0)
			{
				this.DetectCoins();
			}
			if (this.cooldown % 30 == 0)
			{
				Common.PlatinumMaxStack = new Item(74, 1, 0).maxStack;
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00070C0C File Offset: 0x0006EE0C
		private void DetectCoins()
		{
			bool isDepositSucceed = false;
			for (int i = 50; i <= 53; i++)
			{
				Item item = base.Player.inventory[i];
				if (!item.IsAir && item.IsACoin && AutoQuickStackCoins.TryDepositACoin(item, base.Player))
				{
					isDepositSucceed = true;
					item.TurnToAir(false);
				}
			}
			if (isDepositSucceed)
			{
				Recipe.FindRecipes(false);
			}
		}

		// Token: 0x040005B4 RID: 1460
		private int cooldown;
	}
}
