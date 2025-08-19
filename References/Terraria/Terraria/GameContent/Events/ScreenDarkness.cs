using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Events
{
	// Token: 0x020002AC RID: 684
	public class ScreenDarkness
	{
		// Token: 0x06002171 RID: 8561 RVA: 0x00525524 File Offset: 0x00523724
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

		// Token: 0x06002172 RID: 8562 RVA: 0x00525690 File Offset: 0x00523890
		public static void DrawBack(SpriteBatch spriteBatch)
		{
			if (ScreenDarkness.screenObstruction == 0f)
			{
				return;
			}
			Color color = Color.Black * ScreenDarkness.screenObstruction;
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x005256F0 File Offset: 0x005238F0
		public static void DrawFront(SpriteBatch spriteBatch)
		{
			if (ScreenDarkness.screenObstruction == 0f)
			{
				return;
			}
			Color color = ScreenDarkness.frontColor * ScreenDarkness.screenObstruction;
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
		}

		// Token: 0x04004745 RID: 18245
		public static float screenObstruction;

		// Token: 0x04004746 RID: 18246
		public static Color frontColor = new Color(0, 0, 120);
	}
}
