using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x0200021B RID: 539
	public class FastTreeGrowth : GlobalTile
	{
		// Token: 0x06000D22 RID: 3362 RVA: 0x00066BE4 File Offset: 0x00064DE4
		public unsafe override void RandomUpdate(int i, int j, int type)
		{
			if (!QoLCompendium.mainConfig.FastTreeGrowth || !Main.tile[i, j].HasTile)
			{
				return;
			}
			for (int time = 0; time < 4; time++)
			{
				if (type <= 590)
				{
					if (type == 20)
					{
						if ((*Main.tile[i, j].TileFrameX < 324 || *Main.tile[i, j].TileFrameX >= 540) ? WorldGen.GrowTree(i, j) : (WorldGen.GrowPalmTree(i, j) && WorldGen.PlayerLOS(i, j)))
						{
							WorldGen.TreeGrowFXCheck(i, j);
						}
						return;
					}
					if (type == 590)
					{
						if (WorldGen.genRand.NextBool(5))
						{
							int style = (int)(*Main.tile[i, j].TileFrameX / 54);
							if (WorldGen.TryGrowingTreeByType(583 + style, i, j) && WorldGen.PlayerLOS(i, j))
							{
								WorldGen.TreeGrowFXCheck(i, j);
							}
						}
						return;
					}
				}
				else if (type == 595 || type == 615)
				{
					if (WorldGen.genRand.NextBool(5) && WorldGen.TryGrowingTreeByType(type + 1, i, j) && WorldGen.PlayerLOS(i, j))
					{
						WorldGen.TreeGrowFXCheck(i, j);
					}
					return;
				}
				if (TileID.Sets.TreeSapling[type])
				{
					ModTile tile = TileLoader.GetTile(type);
					if (tile != null)
					{
						tile.RandomUpdate(i, j);
					}
				}
			}
		}
	}
}
