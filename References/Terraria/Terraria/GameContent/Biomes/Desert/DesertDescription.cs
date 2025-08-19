using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.Desert
{
	// Token: 0x020002D8 RID: 728
	public class DesertDescription
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x0054A546 File Offset: 0x00548746
		// (set) Token: 0x060022E8 RID: 8936 RVA: 0x0054A54E File Offset: 0x0054874E
		public Rectangle CombinedArea { get; private set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x0054A557 File Offset: 0x00548757
		// (set) Token: 0x060022EA RID: 8938 RVA: 0x0054A55F File Offset: 0x0054875F
		public Rectangle Desert { get; private set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x0054A568 File Offset: 0x00548768
		// (set) Token: 0x060022EC RID: 8940 RVA: 0x0054A570 File Offset: 0x00548770
		public Rectangle Hive { get; private set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x0054A579 File Offset: 0x00548779
		// (set) Token: 0x060022EE RID: 8942 RVA: 0x0054A581 File Offset: 0x00548781
		public Vector2D BlockScale { get; private set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0054A58A File Offset: 0x0054878A
		// (set) Token: 0x060022F0 RID: 8944 RVA: 0x0054A592 File Offset: 0x00548792
		public int BlockColumnCount { get; private set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x0054A59B File Offset: 0x0054879B
		// (set) Token: 0x060022F2 RID: 8946 RVA: 0x0054A5A3 File Offset: 0x005487A3
		public int BlockRowCount { get; private set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x0054A5AC File Offset: 0x005487AC
		// (set) Token: 0x060022F4 RID: 8948 RVA: 0x0054A5B4 File Offset: 0x005487B4
		public bool IsValid { get; private set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x0054A5BD File Offset: 0x005487BD
		// (set) Token: 0x060022F6 RID: 8950 RVA: 0x0054A5C5 File Offset: 0x005487C5
		public SurfaceMap Surface { get; private set; }

		// Token: 0x060022F7 RID: 8951 RVA: 0x0000B904 File Offset: 0x00009B04
		private DesertDescription()
		{
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x0054A5D0 File Offset: 0x005487D0
		public void UpdateSurfaceMap()
		{
			this.Surface = SurfaceMap.FromArea(this.CombinedArea.Left - 5, this.CombinedArea.Width + 10);
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x0054A608 File Offset: 0x00548808
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

		// Token: 0x060022FA RID: 8954 RVA: 0x0054A7A8 File Offset: 0x005489A8
		private static bool RowHasInvalidTiles(int startX, int startY, int width)
		{
			if (GenVars.skipDesertTileCheck)
			{
				return false;
			}
			for (int i = startX; i < startX + width; i++)
			{
				ushort type = Main.tile[i, startY].type;
				if (type == 59 || type == 60)
				{
					return true;
				}
				if (type == 161 || type == 147)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040047F3 RID: 18419
		public static readonly DesertDescription Invalid = new DesertDescription
		{
			IsValid = false
		};

		// Token: 0x040047F4 RID: 18420
		private static readonly Vector2D DefaultBlockScale = new Vector2D(4.0, 2.0);

		// Token: 0x040047F5 RID: 18421
		private const int SCAN_PADDING = 5;
	}
}
