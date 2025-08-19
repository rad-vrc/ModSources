using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020004EC RID: 1260
	public class ClassicPlayerResourcesDisplaySet : IPlayerResourcesDisplaySet, IConfigKeyHolder
	{
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x005C82CC File Offset: 0x005C64CC
		public string DisplayedName
		{
			get
			{
				return Language.GetTextValue("UI.HealthManaStyle_" + this.NameKey);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x005C82E3 File Offset: 0x005C64E3
		// (set) Token: 0x06003D23 RID: 15651 RVA: 0x005C82EB File Offset: 0x005C64EB
		public string NameKey { get; private set; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x005C82F4 File Offset: 0x005C64F4
		// (set) Token: 0x06003D25 RID: 15653 RVA: 0x005C82FC File Offset: 0x005C64FC
		public string ConfigKey { get; private set; }

		// Token: 0x06003D26 RID: 15654 RVA: 0x005C8305 File Offset: 0x005C6505
		public ClassicPlayerResourcesDisplaySet(string nameKey, string configKey)
		{
			this.NameKey = nameKey;
			this.ConfigKey = configKey;
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x005C8331 File Offset: 0x005C6531
		public void Draw()
		{
			this.UI_ScreenAnchorX = Main.screenWidth - 800;
			this.DrawLife();
			this.DrawMana();
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x005C8350 File Offset: 0x005C6550
		private void DrawLife()
		{
			Player localPlayer = Main.LocalPlayer;
			SpriteBatch spriteBatch = Main.spriteBatch;
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			this.UIDisplay_LifePerHeart = 20f;
			PlayerStatsSnapshot snapshot = new PlayerStatsSnapshot(localPlayer);
			if (localPlayer.ghost || localPlayer.statLifeMax2 <= 0 || snapshot.AmountOfLifeHearts <= 0)
			{
				return;
			}
			this.UIDisplay_LifePerHeart = snapshot.LifePerSegment;
			int num2 = snapshot.LifeFruitCount;
			bool drawText;
			bool drawHearts = ResourceOverlayLoader.PreDrawResourceDisplay(snapshot, this, true, ref color, out drawText);
			if (drawText)
			{
				int num3 = (int)((float)localPlayer.statLifeMax2 / this.UIDisplay_LifePerHeart);
				if (num3 >= 10)
				{
					num3 = 10;
				}
				string text = string.Concat(new string[]
				{
					Lang.inter[0].Value,
					" ",
					localPlayer.statLifeMax2.ToString(),
					"/",
					localPlayer.statLifeMax2.ToString()
				});
				Vector2 vector = FontAssets.MouseText.Value.MeasureString(text);
				if (!localPlayer.ghost)
				{
					DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Lang.inter[0].Value, new Vector2((float)(500 + 13 * num3) - vector.X * 0.5f + (float)this.UI_ScreenAnchorX, 6f), color, 0f, default(Vector2), 1f, 0, 0f);
					DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, localPlayer.statLife.ToString() + "/" + localPlayer.statLifeMax2.ToString(), new Vector2((float)(500 + 13 * num3) + vector.X * 0.5f + (float)this.UI_ScreenAnchorX, 6f), color, 0f, new Vector2(FontAssets.MouseText.Value.MeasureString(localPlayer.statLife.ToString() + "/" + localPlayer.statLifeMax2.ToString()).X, 0f), 1f, 0, 0f);
				}
			}
			if (drawHearts)
			{
				for (int i = 1; i < (int)((float)localPlayer.statLifeMax2 / this.UIDisplay_LifePerHeart) + 1; i++)
				{
					float num4 = 1f;
					bool flag = false;
					int num5;
					if ((float)localPlayer.statLife >= (float)i * this.UIDisplay_LifePerHeart)
					{
						num5 = 255;
						if ((float)localPlayer.statLife == (float)i * this.UIDisplay_LifePerHeart)
						{
							flag = true;
						}
					}
					else
					{
						float num6 = ((float)localPlayer.statLife - (float)(i - 1) * this.UIDisplay_LifePerHeart) / this.UIDisplay_LifePerHeart;
						num5 = (int)(30f + 225f * num6);
						if (num5 < 30)
						{
							num5 = 30;
						}
						num4 = num6 / 4f + 0.75f;
						if ((double)num4 < 0.75)
						{
							num4 = 0.75f;
						}
						if (num6 > 0f)
						{
							flag = true;
						}
					}
					if (flag)
					{
						num4 += Main.cursorScale - 1f;
					}
					int num7 = 0;
					int num8 = 0;
					if (i > 10)
					{
						num7 -= 260;
						num8 += 26;
					}
					int a = (int)((double)num5 * 0.9);
					if (!localPlayer.ghost)
					{
						Asset<Texture2D> heartTexture = (num2 > 0) ? TextureAssets.Heart2 : TextureAssets.Heart;
						if (num2 > 0)
						{
							num2--;
						}
						Vector2 position;
						position..ctor((float)(500 + 26 * (i - 1) + num7 + this.UI_ScreenAnchorX + heartTexture.Width() / 2), 32f + (float)heartTexture.Height() * (1f - num4) / 2f + (float)num8 + (float)(heartTexture.Height() / 2));
						ResourceOverlayLoader.DrawResource(new ResourceOverlayDrawContext(snapshot, this, i - 1, heartTexture)
						{
							position = position,
							color = new Color(num5, num5, num5, a),
							origin = heartTexture.Size() / 2f,
							scale = new Vector2(num4)
						});
					}
				}
			}
			ResourceOverlayLoader.PostDrawResourceDisplay(snapshot, this, true, color, drawText);
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x005C8768 File Offset: 0x005C6968
		private void DrawMana()
		{
			Player localPlayer = Main.LocalPlayer;
			SpriteBatch spriteBatch = Main.spriteBatch;
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			this.UIDisplay_ManaPerStar = 20f;
			PlayerStatsSnapshot snapshot = new PlayerStatsSnapshot(localPlayer);
			if (localPlayer.ghost || localPlayer.statManaMax2 <= 0 || snapshot.AmountOfManaStars <= 0)
			{
				return;
			}
			this.UIDisplay_ManaPerStar = snapshot.ManaPerSegment;
			bool drawText;
			bool flag2 = ResourceOverlayLoader.PreDrawResourceDisplay(snapshot, this, false, ref color, out drawText);
			if (drawText)
			{
				int num5 = localPlayer.statManaMax2 / 20;
				Vector2 vector = FontAssets.MouseText.Value.MeasureString(Lang.inter[2].Value);
				int num = 50;
				if (vector.X >= 45f)
				{
					num = (int)vector.X + 5;
				}
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Lang.inter[2].Value, new Vector2((float)(800 - num + this.UI_ScreenAnchorX), 6f), color, 0f, default(Vector2), 1f, 0, 0f);
			}
			if (flag2)
			{
				int i = 1;
				while ((float)i < (float)localPlayer.statManaMax2 / this.UIDisplay_ManaPerStar + 1f)
				{
					bool flag = false;
					float num2 = 1f;
					int num3;
					if ((float)localPlayer.statMana >= (float)i * this.UIDisplay_ManaPerStar)
					{
						num3 = 255;
						if ((float)localPlayer.statMana == (float)i * this.UIDisplay_ManaPerStar)
						{
							flag = true;
						}
					}
					else
					{
						float num4 = ((float)localPlayer.statMana - (float)(i - 1) * this.UIDisplay_ManaPerStar) / this.UIDisplay_ManaPerStar;
						num3 = (int)(30f + 225f * num4);
						if (num3 < 30)
						{
							num3 = 30;
						}
						num2 = num4 / 4f + 0.75f;
						if ((double)num2 < 0.75)
						{
							num2 = 0.75f;
						}
						if (num4 > 0f)
						{
							flag = true;
						}
					}
					if (flag)
					{
						num2 += Main.cursorScale - 1f;
					}
					Vector2 position;
					position..ctor((float)(775 + this.UI_ScreenAnchorX), (float)(30 + TextureAssets.Mana.Height() / 2) + (float)TextureAssets.Mana.Height() * (1f - num2) / 2f + (float)(28 * (i - 1)));
					int a = (int)((double)num3 * 0.9);
					ResourceOverlayLoader.DrawResource(new ResourceOverlayDrawContext(snapshot, this, i - 1, TextureAssets.Mana)
					{
						position = position,
						color = new Color(num3, num3, num3, a),
						origin = TextureAssets.Mana.Size() / 2f,
						scale = new Vector2(num2)
					});
					i++;
				}
			}
			ResourceOverlayLoader.PostDrawResourceDisplay(snapshot, this, false, color, drawText);
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x005C8A34 File Offset: 0x005C6C34
		public void TryToHover()
		{
			Vector2 mouseScreen = Main.MouseScreen;
			Player localPlayer = Main.LocalPlayer;
			PlayerStatsSnapshot snapshot = new PlayerStatsSnapshot(localPlayer);
			int num = 26 * snapshot.AmountOfLifeHearts;
			float num2 = 0f;
			if (snapshot.AmountOfLifeHearts > 10)
			{
				num = 260;
				num2 += 26f;
			}
			if (mouseScreen.X > (float)(500 + this.UI_ScreenAnchorX) && mouseScreen.X < (float)(500 + num + this.UI_ScreenAnchorX) && mouseScreen.Y > 32f && mouseScreen.Y < (float)(32 + TextureAssets.Heart.Height()) + num2 && ResourceOverlayLoader.DisplayHoverText(snapshot, this, true))
			{
				CommonResourceBarMethods.DrawLifeMouseOver();
			}
			num = 24;
			num2 = (float)(28 * snapshot.AmountOfManaStars);
			if (mouseScreen.X > (float)(762 + this.UI_ScreenAnchorX) && mouseScreen.X < (float)(762 + num + this.UI_ScreenAnchorX) && mouseScreen.Y > 30f && mouseScreen.Y < 30f + num2 && ResourceOverlayLoader.DisplayHoverText(snapshot, this, false))
			{
				CommonResourceBarMethods.DrawManaMouseOver();
			}
		}

		// Token: 0x040055FC RID: 22012
		private float UIDisplay_ManaPerStar = 20f;

		// Token: 0x040055FD RID: 22013
		private float UIDisplay_LifePerHeart = 20f;

		// Token: 0x040055FE RID: 22014
		private int UI_ScreenAnchorX;
	}
}
