using System;
using Terraria.IO;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000069 RID: 105
	public static class Passes
	{
		// Token: 0x0200052B RID: 1323
		public class Clear : GenPass
		{
			// Token: 0x06003089 RID: 12425 RVA: 0x005E3005 File Offset: 0x005E1205
			public Clear() : base("clear", 1.0)
			{
			}

			// Token: 0x0600308A RID: 12426 RVA: 0x005E301C File Offset: 0x005E121C
			protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
			{
				for (int i = 0; i < GenBase._worldWidth; i++)
				{
					for (int j = 0; j < GenBase._worldHeight; j++)
					{
						if (GenBase._tiles[i, j] == null)
						{
							GenBase._tiles[i, j] = new Tile();
						}
						else
						{
							GenBase._tiles[i, j].ClearEverything();
						}
					}
				}
			}
		}

		// Token: 0x0200052C RID: 1324
		public class ScatterCustom : GenPass
		{
			// Token: 0x0600308B RID: 12427 RVA: 0x005E307B File Offset: 0x005E127B
			public ScatterCustom(string name, double loadWeight, int count, GenBase.CustomPerUnitAction perUnit = null) : base(name, loadWeight)
			{
				this._perUnit = perUnit;
				this._count = count;
			}

			// Token: 0x0600308C RID: 12428 RVA: 0x005E3094 File Offset: 0x005E1294
			public void SetCustomAction(GenBase.CustomPerUnitAction perUnit)
			{
				this._perUnit = perUnit;
			}

			// Token: 0x0600308D RID: 12429 RVA: 0x005E30A0 File Offset: 0x005E12A0
			protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
			{
				int i = this._count;
				while (i > 0)
				{
					if (this._perUnit(GenBase._random.Next(1, GenBase._worldWidth), GenBase._random.Next(1, GenBase._worldHeight), new object[0]))
					{
						i--;
					}
				}
			}

			// Token: 0x04005801 RID: 22529
			private GenBase.CustomPerUnitAction _perUnit;

			// Token: 0x04005802 RID: 22530
			private int _count;
		}
	}
}
