using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Graphics
{
	// Token: 0x0200043D RID: 1085
	public struct SpriteRenderTargetHelper
	{
		// Token: 0x060035B9 RID: 13753 RVA: 0x00578760 File Offset: 0x00576960
		public static void GetDrawBoundary(List<DrawData> playerDrawData, out Vector2 lowest, out Vector2 highest)
		{
			lowest = Vector2.Zero;
			highest = Vector2.Zero;
			for (int i = 0; i <= playerDrawData.Count; i++)
			{
				if (i != playerDrawData.Count)
				{
					DrawData cdd = playerDrawData[i];
					if (i == 0)
					{
						lowest = cdd.position;
						highest = cdd.position;
					}
					SpriteRenderTargetHelper.GetHighsAndLowsOf(ref lowest, ref highest, ref cdd);
				}
			}
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x005787CC File Offset: 0x005769CC
		public static void GetHighsAndLowsOf(ref Vector2 lowest, ref Vector2 highest, ref DrawData cdd)
		{
			Vector2 origin = cdd.origin;
			Rectangle rectangle = cdd.destinationRectangle;
			if (cdd.sourceRect != null)
			{
				rectangle = cdd.sourceRect.Value;
			}
			if (cdd.sourceRect == null)
			{
				rectangle = cdd.texture.Frame(1, 1, 0, 0, 0, 0);
			}
			rectangle.X = 0;
			rectangle.Y = 0;
			Vector2 pos = cdd.position;
			SpriteRenderTargetHelper.GetHighsAndLowsOf(ref lowest, ref highest, ref cdd, ref pos, ref origin, new Vector2(0f, 0f));
			SpriteRenderTargetHelper.GetHighsAndLowsOf(ref lowest, ref highest, ref cdd, ref pos, ref origin, new Vector2((float)rectangle.Width, 0f));
			SpriteRenderTargetHelper.GetHighsAndLowsOf(ref lowest, ref highest, ref cdd, ref pos, ref origin, new Vector2(0f, (float)rectangle.Height));
			SpriteRenderTargetHelper.GetHighsAndLowsOf(ref lowest, ref highest, ref cdd, ref pos, ref origin, new Vector2((float)rectangle.Width, (float)rectangle.Height));
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x005788AC File Offset: 0x00576AAC
		public static void GetHighsAndLowsOf(ref Vector2 lowest, ref Vector2 highest, ref DrawData cdd, ref Vector2 pos, ref Vector2 origin, Vector2 corner)
		{
			Vector2 corner2 = SpriteRenderTargetHelper.GetCorner(ref cdd, ref pos, ref origin, corner);
			lowest = Vector2.Min(lowest, corner2);
			highest = Vector2.Max(highest, corner2);
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x005788EC File Offset: 0x00576AEC
		public static Vector2 GetCorner(ref DrawData cdd, ref Vector2 pos, ref Vector2 origin, Vector2 corner)
		{
			Vector2 spinningpoint = corner - origin;
			return pos + spinningpoint.RotatedBy((double)cdd.rotation, default(Vector2)) * cdd.scale;
		}
	}
}
