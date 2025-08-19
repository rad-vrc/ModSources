using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x020004B2 RID: 1202
	public class ShimmerHelper
	{
		// Token: 0x060039C7 RID: 14791 RVA: 0x0059BB78 File Offset: 0x00599D78
		public static Vector2? FindSpotWithoutShimmer(Entity entity, int startX, int startY, int expand, bool allowSolidTop)
		{
			Vector2 vector;
			vector..ctor((float)(-(float)entity.width / 2), (float)(-(float)entity.height));
			for (int i = 0; i < expand; i++)
			{
				float num4 = (float)(startX - i);
				int num2 = startY - expand;
				Vector2 vector2 = new Vector2(num4 * (float)16, (float)(num2 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector2, allowSolidTop))
				{
					return new Vector2?(vector2);
				}
				vector2 = new Vector2((float)((startX + i) * 16), (float)(num2 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector2, allowSolidTop))
				{
					return new Vector2?(vector2);
				}
				float num5 = (float)(startX - i);
				num2 = startY + expand;
				vector2 = new Vector2(num5 * (float)16, (float)(num2 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector2, allowSolidTop))
				{
					return new Vector2?(vector2);
				}
				vector2 = new Vector2((float)((startX + i) * 16), (float)(num2 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector2, allowSolidTop))
				{
					return new Vector2?(vector2);
				}
			}
			for (int j = 0; j < expand; j++)
			{
				float num6 = (float)(startX - expand);
				int num3 = startY - j;
				Vector2 vector3 = new Vector2(num6 * (float)16, (float)(num3 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector3, allowSolidTop))
				{
					return new Vector2?(vector3);
				}
				vector3 = new Vector2((float)((startX + expand) * 16), (float)(num3 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector3, allowSolidTop))
				{
					return new Vector2?(vector3);
				}
				float num7 = (float)(startX - expand);
				num3 = startY + j;
				vector3 = new Vector2(num7 * (float)16, (float)(num3 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector3, allowSolidTop))
				{
					return new Vector2?(vector3);
				}
				vector3 = new Vector2((float)((startX + expand) * 16), (float)(num3 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector3, allowSolidTop))
				{
					return new Vector2?(vector3);
				}
			}
			return null;
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x0059BD44 File Offset: 0x00599F44
		private static bool IsSpotShimmerFree(Entity entity, Vector2 landingPosition, bool allowSolidTop)
		{
			return !Collision.SolidCollision(landingPosition, entity.width, entity.height) && Collision.SolidCollision(landingPosition + new Vector2(0f, (float)entity.height), entity.width, 100, allowSolidTop) && (!Collision.WetCollision(landingPosition, entity.width, entity.height + 100) || !Collision.shimmer);
		}
	}
}
