using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Weapons.Ammo.Rockets
{
	// Token: 0x02000047 RID: 71
	public abstract class RocketPouch : BaseAmmo
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000137 RID: 311
		public abstract int RocketProjectile { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000138 RID: 312
		public abstract int SnowmanProjectile { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000139 RID: 313
		public abstract int GrenadeProjectile { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600013A RID: 314
		public abstract int MineProjectile { get; }

		// Token: 0x0600013B RID: 315 RVA: 0x00008814 File Offset: 0x00006A14
		public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
		{
			int type2 = weapon.type;
			switch (type2)
			{
			case 758:
				type = this.GrenadeProjectile;
				return;
			case 759:
				type = this.RocketProjectile;
				return;
			case 760:
				type = this.MineProjectile;
				return;
			default:
				if (type2 != 1946)
				{
					return;
				}
				type = this.SnowmanProjectile;
				return;
			}
		}
	}
}
