using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000063 RID: 99
	public static class Conditions
	{
		// Token: 0x0200050C RID: 1292
		public class IsTile : GenCondition
		{
			// Token: 0x06003043 RID: 12355 RVA: 0x005E2227 File Offset: 0x005E0427
			public IsTile(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x06003044 RID: 12356 RVA: 0x005E2238 File Offset: 0x005E0438
			protected override bool CheckValidity(int x, int y)
			{
				if (GenBase._tiles[x, y].active())
				{
					for (int i = 0; i < this._types.Length; i++)
					{
						if (GenBase._tiles[x, y].type == this._types[i])
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x040057D7 RID: 22487
			private ushort[] _types;
		}

		// Token: 0x0200050D RID: 1293
		public class Continue : GenCondition
		{
			// Token: 0x06003045 RID: 12357 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			protected override bool CheckValidity(int x, int y)
			{
				return false;
			}
		}

		// Token: 0x0200050E RID: 1294
		public class MysticSnake : GenCondition
		{
			// Token: 0x06003047 RID: 12359 RVA: 0x005E2294 File Offset: 0x005E0494
			protected override bool CheckValidity(int x, int y)
			{
				return GenBase._tiles[x, y].active() && !Main.tileCut[(int)GenBase._tiles[x, y].type] && GenBase._tiles[x, y].type != 504;
			}
		}

		// Token: 0x0200050F RID: 1295
		public class IsSolid : GenCondition
		{
			// Token: 0x06003049 RID: 12361 RVA: 0x005E22EA File Offset: 0x005E04EA
			protected override bool CheckValidity(int x, int y)
			{
				return WorldGen.InWorld(x, y, 10) && GenBase._tiles[x, y].active() && Main.tileSolid[(int)GenBase._tiles[x, y].type];
			}
		}

		// Token: 0x02000510 RID: 1296
		public class HasLava : GenCondition
		{
			// Token: 0x0600304B RID: 12363 RVA: 0x005E2325 File Offset: 0x005E0525
			protected override bool CheckValidity(int x, int y)
			{
				return GenBase._tiles[x, y].liquid > 0 && GenBase._tiles[x, y].liquidType() == 1;
			}
		}

		// Token: 0x02000511 RID: 1297
		public class NotNull : GenCondition
		{
			// Token: 0x0600304D RID: 12365 RVA: 0x005E2351 File Offset: 0x005E0551
			protected override bool CheckValidity(int x, int y)
			{
				return GenBase._tiles[x, y] != null;
			}
		}
	}
}
