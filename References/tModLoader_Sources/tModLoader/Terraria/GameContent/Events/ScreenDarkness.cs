using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Events
{
	// Token: 0x02000632 RID: 1586
	public class ScreenDarkness
	{
		// Token: 0x06004592 RID: 17810 RVA: 0x00614068 File Offset: 0x00612268
		public static void Update()
		{
			float value = 0f;
			float amount = 0.016666668f;
			Vector2 mountedCenter = Main.player[Main.myPlayer].MountedCenter;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 370 && Main.npc[i].Distance(mountedCenter) < 3000f && (Main.npc[i].ai[0] >= 10f || (Main.npc[i].ai[0] == 9f && Main.npc[i].ai[2] > 120f)))
				{
					value = 0.95f;
					ScreenDarkness.frontColor = new Color(0, 0, 120) * 0.3f;
					amount = 0.03f;
				}
				if (Main.npc[i].active && Main.npc[i].type == 113 && Main.npc[i].Distance(mountedCenter) < 3000f)
				{
					float num = Utils.Remap(Main.npc[i].Distance(mountedCenter), 2000f, 3000f, 1f, 0f, true);
					value = Main.npc[i].localAI[1] * num;
					amount = 1f;
					ScreenDarkness.frontColor = Color.Black;
				}
			}
			ScreenDarkness.screenObstruction = MathHelper.Lerp(ScreenDarkness.screenObstruction, value, amount);
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x006141D4 File Offset: 0x006123D4
		public static void DrawBack(SpriteBatch spriteBatch)
		{
			if (ScreenDarkness.screenObstruction != 0f)
			{
				Color color = Color.Black * ScreenDarkness.screenObstruction;
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
			}
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x00614234 File Offset: 0x00612434
		public static void DrawFront(SpriteBatch spriteBatch)
		{
			if (ScreenDarkness.screenObstruction != 0f)
			{
				Color color = ScreenDarkness.frontColor * ScreenDarkness.screenObstruction;
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
			}
		}

		// Token: 0x04005B01 RID: 23297
		public static float screenObstruction;

		// Token: 0x04005B02 RID: 23298
		public static Color frontColor = new Color(0, 0, 120);
	}
}
