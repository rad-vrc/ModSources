using System;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000061 RID: 97
	public class GenBase
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0048C7E1 File Offset: 0x0048A9E1
		protected static UnifiedRandom _random
		{
			get
			{
				return WorldGen.genRand;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0048C7E8 File Offset: 0x0048A9E8
		protected static Tile[,] _tiles
		{
			get
			{
				return Main.tile;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0048C7EF File Offset: 0x0048A9EF
		protected static int _worldWidth
		{
			get
			{
				return Main.maxTilesX;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0048C7F6 File Offset: 0x0048A9F6
		protected static int _worldHeight
		{
			get
			{
				return Main.maxTilesY;
			}
		}

		// Token: 0x0200050A RID: 1290
		// (Invoke) Token: 0x06003040 RID: 12352
		public delegate bool CustomPerUnitAction(int x, int y, params object[] args);
	}
}
