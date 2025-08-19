using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000547 RID: 1351
	public class BigProgressBarHelper
	{
		// Token: 0x06004019 RID: 16409 RVA: 0x005DE0F0 File Offset: 0x005DC2F0
		public static void DrawBareBonesBar(SpriteBatch spriteBatch, float lifePercent)
		{
			Rectangle rectangle = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0f, -50f), new Vector2(400f, 20f));
			Rectangle destinationRectangle = rectangle;
			destinationRectangle.Inflate(2, 2);
			Texture2D value = TextureAssets.MagicPixel.Value;
			Rectangle value2;
			value2..ctor(0, 0, 1, 1);
			Rectangle destinationRectangle2 = rectangle;
			destinationRectangle2.Width = (int)((float)destinationRectangle2.Width * lifePercent);
			spriteBatch.Draw(value, destinationRectangle, new Rectangle?(value2), Color.White * 0.6f);
			spriteBatch.Draw(value, rectangle, new Rectangle?(value2), Color.Black * 0.6f);
			spriteBatch.Draw(value, destinationRectangle2, new Rectangle?(value2), Color.LimeGreen * 0.5f);
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x005DE1D4 File Offset: 0x005DC3D4
		public static void DrawFancyBar(SpriteBatch spriteBatch, float lifeAmount, float lifeMax, Texture2D barIconTexture, Rectangle barIconFrame)
		{
			if (BossBarLoader.drawingInfo != null)
			{
				BigProgressBarHelper.DrawFancyBar(spriteBatch, lifeAmount, lifeMax, barIconTexture, barIconFrame, 0f, 0f);
				return;
			}
			Texture2D value = Main.Assets.Request<Texture2D>("Images/UI/UI_BossBar").Value;
			Point p;
			p..ctor(456, 22);
			Point p2;
			p2..ctor(32, 24);
			int verticalFrames = 6;
			Rectangle value2 = value.Frame(1, verticalFrames, 0, 3, 0, 0);
			Color color = Color.White * 0.2f;
			float num = lifeAmount / lifeMax;
			int num2 = (int)((float)p.X * num);
			num2 -= num2 % 2;
			Rectangle value3 = value.Frame(1, verticalFrames, 0, 2, 0, 0);
			value3.X += p2.X;
			value3.Y += p2.Y;
			value3.Width = 2;
			value3.Height = p.Y;
			Rectangle value4 = value.Frame(1, verticalFrames, 0, 1, 0, 0);
			value4.X += p2.X;
			value4.Y += p2.Y;
			value4.Width = 2;
			value4.Height = p.Y;
			Rectangle rectangle = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0f, -50f), p.ToVector2());
			Vector2 vector = rectangle.TopLeft() - p2.ToVector2();
			spriteBatch.Draw(value, vector, new Rectangle?(value2), color, 0f, Vector2.Zero, 1f, 0, 0f);
			spriteBatch.Draw(value, rectangle.TopLeft(), new Rectangle?(value3), Color.White, 0f, Vector2.Zero, new Vector2((float)(num2 / value3.Width), 1f), 0, 0f);
			spriteBatch.Draw(value, rectangle.TopLeft() + new Vector2((float)(num2 - 2), 0f), new Rectangle?(value4), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
			Rectangle value5 = value.Frame(1, verticalFrames, 0, 0, 0, 0);
			spriteBatch.Draw(value, vector, new Rectangle?(value5), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
			Vector2 vector2 = new Vector2(4f, 20f) + new Vector2(26f, 28f) / 2f;
			spriteBatch.Draw(barIconTexture, vector + vector2, new Rectangle?(barIconFrame), Color.White, 0f, barIconFrame.Size() / 2f, 1f, 0, 0f);
			if (BigProgressBarSystem.ShowText)
			{
				BigProgressBarHelper.DrawHealthText(spriteBatch, rectangle, lifeAmount, lifeMax);
			}
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x005DE4A8 File Offset: 0x005DC6A8
		private static void DrawHealthText(SpriteBatch spriteBatch, Rectangle area, float current, float max)
		{
			DynamicSpriteFont value = FontAssets.ItemStack.Value;
			Vector2 vector = area.Center.ToVector2();
			vector.Y += 1f;
			string text = "/";
			Vector2 vector2 = value.MeasureString(text);
			Utils.DrawBorderStringFourWay(spriteBatch, value, text, vector.X, vector.Y, Color.White, Color.Black, vector2 * 0.5f, 1f);
			text = ((int)current).ToString();
			vector2 = value.MeasureString(text);
			Utils.DrawBorderStringFourWay(spriteBatch, value, text, vector.X - 5f, vector.Y, Color.White, Color.Black, vector2 * new Vector2(1f, 0.5f), 1f);
			text = ((int)max).ToString();
			vector2 = value.MeasureString(text);
			Utils.DrawBorderStringFourWay(spriteBatch, value, text, vector.X + 5f, vector.Y, Color.White, Color.Black, vector2 * new Vector2(0f, 0.5f), 1f);
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x005DE5C0 File Offset: 0x005DC7C0
		public static void DrawFancyBar(SpriteBatch spriteBatch, float lifeAmount, float lifeMax, Texture2D barIconTexture, Rectangle barIconFrame, float shieldCurrent, float shieldMax)
		{
			Texture2D value = Main.Assets.Request<Texture2D>("Images/UI/UI_BossBar").Value;
			if (BossBarLoader.drawingInfo != null)
			{
				BigProgressBarInfo info = BossBarLoader.drawingInfo.Value;
				BossBarLoader.drawingInfo = null;
				Vector2 barCenter = Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0f, -50f);
				Color iconColor = Color.White;
				float iconScale = 1f;
				BossBarDrawParams drawParams = new BossBarDrawParams(value, barCenter, barIconTexture, barIconFrame, iconColor, lifeAmount, lifeMax, shieldCurrent, shieldMax, iconScale, info.showText, Vector2.Zero);
				if (BossBarLoader.PreDraw(spriteBatch, info, ref drawParams))
				{
					BossBarLoader.DrawFancyBar_TML(spriteBatch, drawParams);
				}
				BossBarLoader.PostDraw(spriteBatch, info, drawParams);
				return;
			}
			Point p;
			p..ctor(456, 22);
			Point p2;
			p2..ctor(32, 24);
			int verticalFrames = 6;
			Rectangle value2 = value.Frame(1, verticalFrames, 0, 3, 0, 0);
			Color color = Color.White * 0.2f;
			float num = lifeAmount / lifeMax;
			int num2 = (int)((float)p.X * num);
			num2 -= num2 % 2;
			Rectangle value3 = value.Frame(1, verticalFrames, 0, 2, 0, 0);
			value3.X += p2.X;
			value3.Y += p2.Y;
			value3.Width = 2;
			value3.Height = p.Y;
			Rectangle value4 = value.Frame(1, verticalFrames, 0, 1, 0, 0);
			value4.X += p2.X;
			value4.Y += p2.Y;
			value4.Width = 2;
			value4.Height = p.Y;
			float num3 = shieldCurrent / shieldMax;
			int num4 = (int)((float)p.X * num3);
			num4 -= num4 % 2;
			Rectangle value5 = value.Frame(1, verticalFrames, 0, 5, 0, 0);
			value5.X += p2.X;
			value5.Y += p2.Y;
			value5.Width = 2;
			value5.Height = p.Y;
			Rectangle value6 = value.Frame(1, verticalFrames, 0, 4, 0, 0);
			value6.X += p2.X;
			value6.Y += p2.Y;
			value6.Width = 2;
			value6.Height = p.Y;
			Rectangle rectangle = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0f, -50f), p.ToVector2());
			Vector2 vector = rectangle.TopLeft() - p2.ToVector2();
			spriteBatch.Draw(value, vector, new Rectangle?(value2), color, 0f, Vector2.Zero, 1f, 0, 0f);
			spriteBatch.Draw(value, rectangle.TopLeft(), new Rectangle?(value3), Color.White, 0f, Vector2.Zero, new Vector2((float)(num2 / value3.Width), 1f), 0, 0f);
			spriteBatch.Draw(value, rectangle.TopLeft() + new Vector2((float)(num2 - 2), 0f), new Rectangle?(value4), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
			if (shieldMax > 0f)
			{
				spriteBatch.Draw(value, rectangle.TopLeft(), new Rectangle?(value5), Color.White, 0f, Vector2.Zero, new Vector2((float)(num4 / value5.Width), 1f), 0, 0f);
				spriteBatch.Draw(value, rectangle.TopLeft() + new Vector2((float)(num4 - 2), 0f), new Rectangle?(value6), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
			}
			Rectangle value7 = value.Frame(1, verticalFrames, 0, 0, 0, 0);
			spriteBatch.Draw(value, vector, new Rectangle?(value7), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
			Vector2 vector2 = new Vector2(4f, 20f) + barIconFrame.Size() / 2f;
			spriteBatch.Draw(barIconTexture, vector + vector2, new Rectangle?(barIconFrame), Color.White, 0f, barIconFrame.Size() / 2f, 1f, 0, 0f);
			if (BigProgressBarSystem.ShowText)
			{
				if (shieldCurrent > 0f)
				{
					BigProgressBarHelper.DrawHealthText(spriteBatch, rectangle, shieldCurrent, shieldMax);
					return;
				}
				BigProgressBarHelper.DrawHealthText(spriteBatch, rectangle, lifeAmount, lifeMax);
			}
		}

		/// <summary>
		/// Draws "<paramref name="current" />/<paramref name="max" />" as text centered on <paramref name="area" />, offset by <paramref name="textOffset" />.
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="area">The Rectangle that the text is centered on</param>
		/// <param name="textOffset">Offset for the text position</param>
		/// <param name="current">Number shown left of the "/"</param>
		/// <param name="max">Number shown right of the "/"</param>
		// Token: 0x0600401D RID: 16413 RVA: 0x005DEA64 File Offset: 0x005DCC64
		public static void DrawHealthText(SpriteBatch spriteBatch, Rectangle area, Vector2 textOffset, float current, float max)
		{
			DynamicSpriteFont font = FontAssets.ItemStack.Value;
			Vector2 center = area.Center.ToVector2() + textOffset;
			center.Y += 1f;
			string text = "/";
			Vector2 textSize = font.MeasureString(text);
			Utils.DrawBorderStringFourWay(spriteBatch, font, text, center.X, center.Y, Color.White, Color.Black, textSize * 0.5f, 1f);
			text = ((int)current).ToString();
			textSize = font.MeasureString(text);
			Utils.DrawBorderStringFourWay(spriteBatch, font, text, center.X - 5f, center.Y, Color.White, Color.Black, textSize * new Vector2(1f, 0.5f), 1f);
			text = ((int)max).ToString();
			textSize = font.MeasureString(text);
			Utils.DrawBorderStringFourWay(spriteBatch, font, text, center.X + 5f, center.Y, Color.White, Color.Black, textSize * new Vector2(0f, 0.5f), 1f);
		}

		// Token: 0x04005843 RID: 22595
		private const string _bossBarTexturePath = "Images/UI/UI_BossBar";
	}
}
