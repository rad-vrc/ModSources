using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Graphics
{
	// Token: 0x020000EB RID: 235
	public struct SpriteRenderTargetHelper
	{
		// Token: 0x060015AF RID: 5551 RVA: 0x004C3330 File Offset: 0x004C1530
		public static void GetDrawBoundary(List<DrawData> playerDrawData, out Vector2 lowest, out Vector2 highest)
		{
			lowest = Vector2.Zero;
			highest = Vector2.Zero;
			for (int i = 0; i <= playerDrawData.Count; i++)
			{
				if (i != playerDrawData.Count)
				{
					DrawData drawData = playerDrawData[i];
					if (i == 0)
					{
						lowest = drawData.position;
						highest = drawData.position;
					}
					SpriteRenderTargetHelper.GetHighsAndLowsOf(ref lowest, ref highest, ref drawData);
				}
			}
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x004C339C File Offset: 0x004C159C
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
			Vector2 position = cdd.position;
			SpriteRenderTargetHelper.GetHighsAndLowsOf(ref lowest, ref highest, ref cdd, ref position, ref origin, new Vector2(0f, 0f));
			SpriteRenderTargetHelper.GetHighsAndLowsOf(ref lowest, ref highest, ref cdd, ref position, ref origin, new Vector2((float)rectangle.Width, 0f));
			SpriteRenderTargetHelper.GetHighsAndLowsOf(ref lowest, ref highest, ref cdd, ref position, ref origin, new Vector2(0f, (float)rectangle.Height));
			SpriteRenderTargetHelper.GetHighsAndLowsOf(ref lowest, ref highest, ref cdd, ref position, ref origin, new Vector2((float)rectangle.Width, (float)rectangle.Height));
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x004C347C File Offset: 0x004C167C
		public static void GetHighsAndLowsOf(ref Vector2 lowest, ref Vector2 highest, ref DrawData cdd, ref Vector2 pos, ref Vector2 origin, Vector2 corner)
		{
			Vector2 corner2 = SpriteRenderTargetHelper.GetCorner(ref cdd, ref pos, ref origin, corner);
			lowest = Vector2.Min(lowest, corner2);
			highest = Vector2.Max(highest, corner2);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x004C34BC File Offset: 0x004C16BC
		public static Vector2 GetCorner(ref DrawData cdd, ref Vector2 pos, ref Vector2 origin, Vector2 corner)
		{
			Vector2 spinningpoint = corner - origin;
			return pos + spinningpoint.RotatedBy((double)cdd.rotation, default(Vector2)) * cdd.scale;
		}
	}
}
