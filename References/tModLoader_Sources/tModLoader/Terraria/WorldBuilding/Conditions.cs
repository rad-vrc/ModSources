using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200006F RID: 111
	public static class Conditions
	{
		// Token: 0x02000825 RID: 2085
		public class IsTile : GenCondition
		{
			// Token: 0x060050A4 RID: 20644 RVA: 0x00694648 File Offset: 0x00692848
			public IsTile(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x060050A5 RID: 20645 RVA: 0x00694658 File Offset: 0x00692858
			protected unsafe override bool CheckValidity(int x, int y)
			{
				if (GenBase._tiles[x, y].active())
				{
					for (int i = 0; i < this._types.Length; i++)
					{
						if (*GenBase._tiles[x, y].type == this._types[i])
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x0400688B RID: 26763
			private ushort[] _types;
		}

		// Token: 0x02000826 RID: 2086
		public class Continue : GenCondition
		{
			// Token: 0x060050A6 RID: 20646 RVA: 0x006946B0 File Offset: 0x006928B0
			protected override bool CheckValidity(int x, int y)
			{
				return false;
			}
		}

		// Token: 0x02000827 RID: 2087
		public class MysticSnake : GenCondition
		{
			// Token: 0x060050A8 RID: 20648 RVA: 0x006946BC File Offset: 0x006928BC
			protected unsafe override bool CheckValidity(int x, int y)
			{
				return GenBase._tiles[x, y].active() && !Main.tileCut[(int)(*GenBase._tiles[x, y].type)] && *GenBase._tiles[x, y].type != 504;
			}
		}

		// Token: 0x02000828 RID: 2088
		public class IsSolid : GenCondition
		{
			// Token: 0x060050AA RID: 20650 RVA: 0x00694728 File Offset: 0x00692928
			protected unsafe override bool CheckValidity(int x, int y)
			{
				return WorldGen.InWorld(x, y, 10) && GenBase._tiles[x, y].active() && Main.tileSolid[(int)(*GenBase._tiles[x, y].type)];
			}
		}

		// Token: 0x02000829 RID: 2089
		public class HasLava : GenCondition
		{
			// Token: 0x060050AC RID: 20652 RVA: 0x00694780 File Offset: 0x00692980
			protected unsafe override bool CheckValidity(int x, int y)
			{
				return *GenBase._tiles[x, y].liquid > 0 && GenBase._tiles[x, y].liquidType() == 1;
			}
		}

		// Token: 0x0200082A RID: 2090
		public class NotNull : GenCondition
		{
			// Token: 0x060050AE RID: 20654 RVA: 0x006947C6 File Offset: 0x006929C6
			protected override bool CheckValidity(int x, int y)
			{
				return GenBase._tiles[x, y] != null;
			}
		}
	}
}
