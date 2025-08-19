using System;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000071 RID: 113
	public class GenBase
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0049F5E6 File Offset: 0x0049D7E6
		protected static UnifiedRandom _random
		{
			get
			{
				return WorldGen.genRand;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0049F5ED File Offset: 0x0049D7ED
		protected static ref Tilemap _tiles
		{
			get
			{
				return ref Main.tile;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0049F5F4 File Offset: 0x0049D7F4
		protected static int _worldWidth
		{
			get
			{
				return Main.maxTilesX;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0049F5FB File Offset: 0x0049D7FB
		protected static int _worldHeight
		{
			get
			{
				return Main.maxTilesY;
			}
		}

		// Token: 0x0200082B RID: 2091
		// (Invoke) Token: 0x060050B1 RID: 20657
		public delegate bool CustomPerUnitAction(int x, int y, params object[] args);
	}
}
