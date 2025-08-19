using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000248 RID: 584
	public class AFKPetsMiracleFruitFix : GlobalTile
	{
		// Token: 0x06000DD0 RID: 3536 RVA: 0x0006ED6D File Offset: 0x0006CF6D
		public override void SetStaticDefaults()
		{
			if (!ModConditions.afkpetsLoaded)
			{
				return;
			}
			Main.tileCut[Common.GetModTile(ModConditions.afkpetsMod, "Plants")] = false;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0006ED90 File Offset: 0x0006CF90
		public unsafe override void RandomUpdate(int i, int j, int type)
		{
			if (!ModConditions.afkpetsLoaded)
			{
				return;
			}
			if (type == Common.GetModTile(ModConditions.afkpetsMod, "Plants"))
			{
				Tile plant = Main.tile[i, j];
				if (*plant.TileFrameX / 18 == 6)
				{
					*plant.TileFrameY = 18;
				}
			}
		}
	}
}
