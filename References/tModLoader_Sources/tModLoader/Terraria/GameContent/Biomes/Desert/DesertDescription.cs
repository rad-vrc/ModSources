using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.Desert
{
	// Token: 0x02000662 RID: 1634
	public class DesertDescription
	{
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06004713 RID: 18195 RVA: 0x00639DF2 File Offset: 0x00637FF2
		// (set) Token: 0x06004714 RID: 18196 RVA: 0x00639DFA File Offset: 0x00637FFA
		public Rectangle CombinedArea { get; private set; }

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06004715 RID: 18197 RVA: 0x00639E03 File Offset: 0x00638003
		// (set) Token: 0x06004716 RID: 18198 RVA: 0x00639E0B File Offset: 0x0063800B
		public Rectangle Desert { get; private set; }

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06004717 RID: 18199 RVA: 0x00639E14 File Offset: 0x00638014
		// (set) Token: 0x06004718 RID: 18200 RVA: 0x00639E1C File Offset: 0x0063801C
		public Rectangle Hive { get; private set; }

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06004719 RID: 18201 RVA: 0x00639E25 File Offset: 0x00638025
		// (set) Token: 0x0600471A RID: 18202 RVA: 0x00639E2D File Offset: 0x0063802D
		public Vector2D BlockScale { get; private set; }

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600471B RID: 18203 RVA: 0x00639E36 File Offset: 0x00638036
		// (set) Token: 0x0600471C RID: 18204 RVA: 0x00639E3E File Offset: 0x0063803E
		public int BlockColumnCount { get; private set; }

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600471D RID: 18205 RVA: 0x00639E47 File Offset: 0x00638047
		// (set) Token: 0x0600471E RID: 18206 RVA: 0x00639E4F File Offset: 0x0063804F
		public int BlockRowCount { get; private set; }

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600471F RID: 18207 RVA: 0x00639E58 File Offset: 0x00638058
		// (set) Token: 0x06004720 RID: 18208 RVA: 0x00639E60 File Offset: 0x00638060
		public bool IsValid { get; private set; }

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x00639E69 File Offset: 0x00638069
		// (set) Token: 0x06004722 RID: 18210 RVA: 0x00639E71 File Offset: 0x00638071
		public SurfaceMap Surface { get; private set; }

		// Token: 0x06004723 RID: 18211 RVA: 0x00639E7A File Offset: 0x0063807A
		private DesertDescription()
		{
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x00639E84 File Offset: 0x00638084
		public void UpdateSurfaceMap()
		{
			this.Surface = SurfaceMap.FromArea(this.CombinedArea.Left - 5, this.CombinedArea.Width + 10);
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x00639EBC File Offset: 0x006380BC
		public static DesertDescription CreateFromPlacement(Point origin)
		{
			Vector2D defaultBlockScale = DesertDescription.DefaultBlockScale;
			double num = (double)Main.maxTilesX / 4200.0;
			int num2 = (int)(80.0 * num);
			int num3 = (int)((WorldGen.genRand.NextDouble() * 0.5 + 1.5) * 170.0 * num);
			if (WorldGen.remixWorldGen)
			{
				num3 = (int)(340.0 * num);
			}
			int num4 = (int)(defaultBlockScale.X * (double)num2);
			int num5 = (int)(defaultBlockScale.Y * (double)num3);
			origin.X -= num4 / 2;
			SurfaceMap surfaceMap = SurfaceMap.FromArea(origin.X - 5, num4 + 10);
			if (DesertDescription.RowHasInvalidTiles(origin.X, surfaceMap.Bottom, num4))
			{
				return DesertDescription.Invalid;
			}
			int num6 = (int)(surfaceMap.Average + (double)surfaceMap.Bottom) / 2;
			origin.Y = num6 + WorldGen.genRand.Next(40, 60);
			int num7 = 0;
			if (Main.tenthAnniversaryWorld)
			{
				num7 = (int)(20.0 * num);
			}
			return new DesertDescription
			{
				CombinedArea = new Rectangle(origin.X, num6, num4, origin.Y + num5 - num6),
				Hive = new Rectangle(origin.X, origin.Y + num7, num4, num5 - num7),
				Desert = new Rectangle(origin.X, num6, num4, origin.Y + num5 / 2 - num6 + num7),
				BlockScale = defaultBlockScale,
				BlockColumnCount = num2,
				BlockRowCount = num3,
				Surface = surfaceMap,
				IsValid = true
			};
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x0063A05C File Offset: 0x0063825C
		private unsafe static bool RowHasInvalidTiles(int startX, int startY, int width)
		{
			if (GenVars.skipDesertTileCheck)
			{
				return false;
			}
			for (int i = startX; i < startX + width; i++)
			{
				ushort num = *Main.tile[i, startY].type;
				if (num - 59 <= 1)
				{
					return true;
				}
				if (num == 147 || num == 161)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04005BAC RID: 23468
		public static readonly DesertDescription Invalid = new DesertDescription
		{
			IsValid = false
		};

		// Token: 0x04005BAD RID: 23469
		private static readonly Vector2D DefaultBlockScale = new Vector2D(4.0, 2.0);

		// Token: 0x04005BAE RID: 23470
		private const int SCAN_PADDING = 5;
	}
}
