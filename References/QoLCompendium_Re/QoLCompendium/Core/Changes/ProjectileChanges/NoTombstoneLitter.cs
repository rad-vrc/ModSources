using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ProjectileChanges
{
	// Token: 0x02000229 RID: 553
	public class NoTombstoneLitter : GlobalProjectile
	{
		// Token: 0x06000D58 RID: 3416 RVA: 0x00067E26 File Offset: 0x00066026
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return NoTombstoneLitter._graveMarkerProjectileTypeToItemType.ContainsKey(entity.type);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00067E38 File Offset: 0x00066038
		public override bool PreAI(Projectile projectile)
		{
			if (!QoLCompendium.mainConfig.NoLittering)
			{
				return base.PreAI(projectile);
			}
			if (projectile.owner == Main.myPlayer)
			{
				int itemIndex = Item.NewItem(projectile.GetSource_DropAsItem(null), projectile.Hitbox, NoTombstoneLitter._graveMarkerProjectileTypeToItemType[projectile.type], 1, false, 0, false, false);
				Main.item[itemIndex].noGrabDelay = 0;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(21, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
				}
			}
			projectile.Kill();
			return false;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00067EC8 File Offset: 0x000660C8
		// Note: this type is marked as 'beforefieldinit'.
		static NoTombstoneLitter()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			dictionary.Add(43, 321);
			dictionary.Add(201, 1173);
			dictionary.Add(202, 1174);
			dictionary.Add(203, 1175);
			dictionary.Add(204, 1176);
			dictionary.Add(205, 1177);
			dictionary.Add(527, 3229);
			dictionary.Add(528, 3230);
			dictionary.Add(529, 3231);
			dictionary.Add(530, 3232);
			dictionary.Add(531, 3233);
			NoTombstoneLitter._graveMarkerProjectileTypeToItemType = dictionary;
		}

		// Token: 0x0400058C RID: 1420
		private static readonly Dictionary<int, int> _graveMarkerProjectileTypeToItemType;
	}
}
