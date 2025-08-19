using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.DataStructures;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003B6 RID: 950
	public class ClassicPlayerResourcesDisplaySet : IPlayerResourcesDisplaySet, IConfigKeyHolder
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x00596952 File Offset: 0x00594B52
		// (set) Token: 0x06002A0C RID: 10764 RVA: 0x0059695A File Offset: 0x00594B5A
		public string NameKey { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x00596963 File Offset: 0x00594B63
		// (set) Token: 0x06002A0E RID: 10766 RVA: 0x0059696B File Offset: 0x00594B6B
		public string ConfigKey { get; private set; }

		// Token: 0x06002A0F RID: 10767 RVA: 0x00596974 File Offset: 0x00594B74
		public ClassicPlayerResourcesDisplaySet(string nameKey, string configKey)
		{
			this.NameKey = nameKey;
			this.ConfigKey = configKey;
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x0059699D File Offset: 0x00594B9D
		public void Draw()
		{
			this.UI_ScreenAnchorX = Main.screenWidth - 800;
			this.DrawLife();
			this.DrawMana();
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x005969BC File Offset: 0x00594BBC
		private void DrawLife()
		{
			Player localPlayer = Main.LocalPlayer;
			SpriteBatch spriteBatch = Main.spriteBatch;
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			this.UIDisplay_LifePerHeart = 20f;
			if (localPlayer.ghost)
			{
				return;
			}
			int num = localPlayer.statLifeMax / 20;
			int num2 = (localPlayer.statLifeMax - 400) / 5;
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 0)
			{
				num = localPlayer.statLifeMax / (20 + num2 / 4);
				this.UIDisplay_LifePerHeart = (float)localPlayer.statLifeMax / 20f;
			}
			int num3 = localPlayer.statLifeMax2 - localPlayer.statLifeMax;
			this.UIDisplay_LifePerHeart += (float)(num3 / num);
			int num4 = (int)((float)localPlayer.statLifeMax2 / this.UIDisplay_LifePerHeart);
			if (num4 >= 10)
			{
				num4 = 10;
			}
			string text = string.Concat(new object[]
			{
				Lang.inter[0].Value,
				" ",
				localPlayer.statLifeMax2,
				"/",
				localPlayer.statLifeMax2
			});
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(text);
			if (!localPlayer.ghost)
			{
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Lang.inter[0].Value, new Vector2((float)(500 + 13 * num4) - vector.X * 0.5f + (float)this.UI_ScreenAnchorX, 6f), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, localPlayer.statLife + "/" + localPlayer.statLifeMax2, new Vector2((float)(500 + 13 * num4) + vector.X * 0.5f + (float)this.UI_ScreenAnchorX, 6f), color, 0f, new Vector2(FontAssets.MouseText.Value.MeasureString(localPlayer.statLife + "/" + localPlayer.statLifeMax2).X, 0f), 1f, SpriteEffects.None, 0f);
			}
			for (int i = 1; i < (int)((float)localPlayer.statLifeMax2 / this.UIDisplay_LifePerHeart) + 1; i++)
			{
				float num5 = 1f;
				bool flag = false;
				int num6;
				if ((float)localPlayer.statLife >= (float)i * this.UIDisplay_LifePerHeart)
				{
					num6 = 255;
					if ((float)localPlayer.statLife == (float)i * this.UIDisplay_LifePerHeart)
					{
						flag = true;
					}
				}
				else
				{
					float num7 = ((float)localPlayer.statLife - (float)(i - 1) * this.UIDisplay_LifePerHeart) / this.UIDisplay_LifePerHeart;
					num6 = (int)(30f + 225f * num7);
					if (num6 < 30)
					{
						num6 = 30;
					}
					num5 = num7 / 4f + 0.75f;
					if ((double)num5 < 0.75)
					{
						num5 = 0.75f;
					}
					if (num7 > 0f)
					{
						flag = true;
					}
				}
				if (flag)
				{
					num5 += Main.cursorScale - 1f;
				}
				int num8 = 0;
				int num9 = 0;
				if (i > 10)
				{
					num8 -= 260;
					num9 += 26;
				}
				int a = (int)((double)num6 * 0.9);
				if (!localPlayer.ghost)
				{
					if (num2 > 0)
					{
						num2--;
						spriteBatch.Draw(TextureAssets.Heart2.Value, new Vector2((float)(500 + 26 * (i - 1) + num8 + this.UI_ScreenAnchorX + TextureAssets.Heart.Width() / 2), 32f + ((float)TextureAssets.Heart.Height() - (float)TextureAssets.Heart.Height() * num5) / 2f + (float)num9 + (float)(TextureAssets.Heart.Height() / 2)), new Rectangle?(new Rectangle(0, 0, TextureAssets.Heart.Width(), TextureAssets.Heart.Height())), new Color(num6, num6, num6, a), 0f, new Vector2((float)(TextureAssets.Heart.Width() / 2), (float)(TextureAssets.Heart.Height() / 2)), num5, SpriteEffects.None, 0f);
					}
					else
					{
						spriteBatch.Draw(TextureAssets.Heart.Value, new Vector2((float)(500 + 26 * (i - 1) + num8 + this.UI_ScreenAnchorX + TextureAssets.Heart.Width() / 2), 32f + ((float)TextureAssets.Heart.Height() - (float)TextureAssets.Heart.Height() * num5) / 2f + (float)num9 + (float)(TextureAssets.Heart.Height() / 2)), new Rectangle?(new Rectangle(0, 0, TextureAssets.Heart.Width(), TextureAssets.Heart.Height())), new Color(num6, num6, num6, a), 0f, new Vector2((float)(TextureAssets.Heart.Width() / 2), (float)(TextureAssets.Heart.Height() / 2)), num5, SpriteEffects.None, 0f);
					}
				}
			}
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x00596EC4 File Offset: 0x005950C4
		private void DrawMana()
		{
			Player localPlayer = Main.LocalPlayer;
			SpriteBatch spriteBatch = Main.spriteBatch;
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			this.UIDisplay_ManaPerStar = 20;
			if (localPlayer.ghost)
			{
				return;
			}
			if (localPlayer.statManaMax2 > 0)
			{
				int num = localPlayer.statManaMax2 / 20;
				Vector2 vector = FontAssets.MouseText.Value.MeasureString(Lang.inter[2].Value);
				int num2 = 50;
				if (vector.X >= 45f)
				{
					num2 = (int)vector.X + 5;
				}
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Lang.inter[2].Value, new Vector2((float)(800 - num2 + this.UI_ScreenAnchorX), 6f), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				for (int i = 1; i < localPlayer.statManaMax2 / this.UIDisplay_ManaPerStar + 1; i++)
				{
					bool flag = false;
					float num3 = 1f;
					int num4;
					if (localPlayer.statMana >= i * this.UIDisplay_ManaPerStar)
					{
						num4 = 255;
						if (localPlayer.statMana == i * this.UIDisplay_ManaPerStar)
						{
							flag = true;
						}
					}
					else
					{
						float num5 = (float)(localPlayer.statMana - (i - 1) * this.UIDisplay_ManaPerStar) / (float)this.UIDisplay_ManaPerStar;
						num4 = (int)(30f + 225f * num5);
						if (num4 < 30)
						{
							num4 = 30;
						}
						num3 = num5 / 4f + 0.75f;
						if ((double)num3 < 0.75)
						{
							num3 = 0.75f;
						}
						if (num5 > 0f)
						{
							flag = true;
						}
					}
					if (flag)
					{
						num3 += Main.cursorScale - 1f;
					}
					int a = (int)((double)num4 * 0.9);
					spriteBatch.Draw(TextureAssets.Mana.Value, new Vector2((float)(775 + this.UI_ScreenAnchorX), (float)(30 + TextureAssets.Mana.Height() / 2) + ((float)TextureAssets.Mana.Height() - (float)TextureAssets.Mana.Height() * num3) / 2f + (float)(28 * (i - 1))), new Rectangle?(new Rectangle(0, 0, TextureAssets.Mana.Width(), TextureAssets.Mana.Height())), new Color(num4, num4, num4, a), 0f, new Vector2((float)(TextureAssets.Mana.Width() / 2), (float)(TextureAssets.Mana.Height() / 2)), num3, SpriteEffects.None, 0f);
				}
			}
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x00597150 File Offset: 0x00595350
		public void TryToHover()
		{
			Vector2 mouseScreen = Main.MouseScreen;
			Player localPlayer = Main.LocalPlayer;
			int num = 26 * localPlayer.statLifeMax2 / (int)this.UIDisplay_LifePerHeart;
			int num2 = 0;
			if (localPlayer.statLifeMax2 > 200)
			{
				num = 260;
				num2 += 26;
			}
			if (mouseScreen.X > (float)(500 + this.UI_ScreenAnchorX) && mouseScreen.X < (float)(500 + num + this.UI_ScreenAnchorX) && mouseScreen.Y > 32f && mouseScreen.Y < (float)(32 + TextureAssets.Heart.Height() + num2))
			{
				CommonResourceBarMethods.DrawLifeMouseOver();
			}
			num = 24;
			num2 = 28 * localPlayer.statManaMax2 / this.UIDisplay_ManaPerStar;
			if (mouseScreen.X > (float)(762 + this.UI_ScreenAnchorX) && mouseScreen.X < (float)(762 + num + this.UI_ScreenAnchorX) && mouseScreen.Y > 30f && mouseScreen.Y < (float)(30 + num2))
			{
				CommonResourceBarMethods.DrawManaMouseOver();
			}
		}

		// Token: 0x04004CDC RID: 19676
		private int UIDisplay_ManaPerStar = 20;

		// Token: 0x04004CDD RID: 19677
		private float UIDisplay_LifePerHeart = 20f;

		// Token: 0x04004CDE RID: 19678
		private int UI_ScreenAnchorX;
	}
}
