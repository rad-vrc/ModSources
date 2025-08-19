using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameInput;
using Terraria.Graphics;
using Terraria.Graphics.Renderers;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004CF RID: 1231
	public class LegacyMultiplayerClosePlayersOverlay : IMultiplayerClosePlayersOverlay
	{
		// Token: 0x06003AC1 RID: 15041 RVA: 0x005AC170 File Offset: 0x005AA370
		public void Draw()
		{
			int teamNamePlateDistance = Main.teamNamePlateDistance;
			if (teamNamePlateDistance <= 0)
			{
				return;
			}
			SpriteBatch spriteBatch = Main.spriteBatch;
			spriteBatch.End();
			spriteBatch.Begin(1, null, null, null, null, null, Main.UIScaleMatrix);
			PlayerInput.SetZoom_World();
			int screenWidth = Main.screenWidth;
			int screenHeight = Main.screenHeight;
			Vector2 screenPosition = Main.screenPosition;
			PlayerInput.SetZoom_UI();
			float uIScale = Main.UIScale;
			int num = teamNamePlateDistance * 8;
			Player[] player = Main.player;
			int myPlayer = Main.myPlayer;
			SpriteViewMatrix gameViewMatrix = Main.GameViewMatrix;
			byte mouseTextColor = Main.mouseTextColor;
			Color[] teamColor = Main.teamColor;
			Camera camera = Main.Camera;
			IPlayerRenderer playerRenderer = Main.PlayerRenderer;
			Vector2 screenPosition2 = Main.screenPosition;
			for (int i = 0; i < 255; i++)
			{
				if (player[i].active && myPlayer != i && !player[i].dead && player[myPlayer].team > 0 && player[myPlayer].team == player[i].team)
				{
					string name = player[i].name;
					Vector2 namePlatePos = FontAssets.MouseText.Value.MeasureString(name);
					float num2 = 0f;
					if (player[i].chatOverhead.timeLeft > 0)
					{
						num2 = (0f - namePlatePos.Y) * uIScale;
					}
					else if (player[i].emoteTime > 0)
					{
						num2 = (0f - namePlatePos.Y) * uIScale;
					}
					Vector2 vector;
					vector..ctor((float)(screenWidth / 2) + screenPosition.X, (float)(screenHeight / 2) + screenPosition.Y);
					Vector2 position = player[i].position;
					position += (position - vector) * (gameViewMatrix.Zoom - Vector2.One);
					float num3 = 0f;
					float num4 = (float)mouseTextColor / 255f;
					Color namePlateColor;
					namePlateColor..ctor((int)((byte)((float)teamColor[player[i].team].R * num4)), (int)((byte)((float)teamColor[player[i].team].G * num4)), (int)((byte)((float)teamColor[player[i].team].B * num4)), (int)mouseTextColor);
					float num5 = position.X + (float)(player[i].width / 2) - vector.X;
					float num6 = position.Y - namePlatePos.Y - 2f + num2 - vector.Y;
					float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
					int num8 = screenHeight;
					if (screenHeight > screenWidth)
					{
						num8 = screenWidth;
					}
					num8 = num8 / 2 - 50;
					if (num8 < 100)
					{
						num8 = 100;
					}
					if (num7 < (float)num8)
					{
						namePlatePos.X = position.X + (float)(player[i].width / 2) - namePlatePos.X / 2f - screenPosition.X;
						namePlatePos.Y = position.Y - namePlatePos.Y - 2f + num2 - screenPosition.Y;
					}
					else
					{
						num3 = num7;
						num7 = (float)num8 / num7;
						namePlatePos.X = (float)(screenWidth / 2) + num5 * num7 - namePlatePos.X / 2f;
						namePlatePos.Y = (float)(screenHeight / 2) + num6 * num7 + 40f * uIScale;
					}
					Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(name);
					namePlatePos += vector2 / 2f;
					namePlatePos *= 1f / uIScale;
					namePlatePos -= vector2 / 2f;
					if (player[myPlayer].gravDir == -1f)
					{
						namePlatePos.Y = (float)screenHeight - namePlatePos.Y;
					}
					if (num3 > 0f)
					{
						float num9 = 20f;
						float num10 = -27f;
						num10 -= (vector2.X - 85f) / 2f;
						num5 = player[i].Center.X - player[myPlayer].Center.X;
						num6 = player[i].Center.Y - player[myPlayer].Center.Y;
						float num11 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
						if (num11 <= (float)num)
						{
							string textValue = Language.GetTextValue("GameUI.PlayerDistance", (int)(num11 / 16f * 2f));
							Vector2 npDistPos = FontAssets.MouseText.Value.MeasureString(textValue);
							npDistPos.X = namePlatePos.X - num10;
							npDistPos.Y = namePlatePos.Y + vector2.Y / 2f - npDistPos.Y / 2f - num9;
							LegacyMultiplayerClosePlayersOverlay.DrawPlayerName2(spriteBatch, ref namePlateColor, textValue, ref npDistPos);
							Color playerHeadBordersColor = Main.GetPlayerHeadBordersColor(player[i]);
							Vector2 position2;
							position2..ctor(namePlatePos.X, namePlatePos.Y - num9);
							position2.X -= 22f + num10;
							position2.Y += 8f;
							playerRenderer.DrawPlayerHead(camera, player[i], position2, 1f, 0.8f, playerHeadBordersColor);
							Vector2 vector3 = npDistPos + screenPosition2 + new Vector2(26f, 20f);
							if (player[i].statLife != player[i].statLifeMax2)
							{
								Main.instance.DrawHealthBar(vector3.X, vector3.Y, player[i].statLife, player[i].statLifeMax2, 1f, 1.25f, true);
							}
							ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, name, namePlatePos + new Vector2(0f, -40f), namePlateColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
						}
					}
					else
					{
						LegacyMultiplayerClosePlayersOverlay.DrawPlayerName(spriteBatch, name, ref namePlatePos, ref namePlateColor);
					}
				}
			}
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x005AC754 File Offset: 0x005AA954
		private static void DrawPlayerName2(SpriteBatch spriteBatch, ref Color namePlateColor, string npDist, ref Vector2 npDistPos)
		{
			float scale = 0.85f;
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, npDist, new Vector2(npDistPos.X - 2f, npDistPos.Y), Color.Black, 0f, default(Vector2), scale, 0, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, npDist, new Vector2(npDistPos.X + 2f, npDistPos.Y), Color.Black, 0f, default(Vector2), scale, 0, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, npDist, new Vector2(npDistPos.X, npDistPos.Y - 2f), Color.Black, 0f, default(Vector2), scale, 0, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, npDist, new Vector2(npDistPos.X, npDistPos.Y + 2f), Color.Black, 0f, default(Vector2), scale, 0, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, npDist, npDistPos, namePlateColor, 0f, default(Vector2), scale, 0, 0f);
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x005AC8A4 File Offset: 0x005AAAA4
		private static void DrawPlayerName(SpriteBatch spriteBatch, string namePlate, ref Vector2 namePlatePos, ref Color namePlateColor)
		{
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, namePlate, new Vector2(namePlatePos.X - 2f, namePlatePos.Y), Color.Black, 0f, default(Vector2), 1f, 0, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, namePlate, new Vector2(namePlatePos.X + 2f, namePlatePos.Y), Color.Black, 0f, default(Vector2), 1f, 0, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, namePlate, new Vector2(namePlatePos.X, namePlatePos.Y - 2f), Color.Black, 0f, default(Vector2), 1f, 0, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, namePlate, new Vector2(namePlatePos.X, namePlatePos.Y + 2f), Color.Black, 0f, default(Vector2), 1f, 0, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, namePlate, namePlatePos, namePlateColor, 0f, default(Vector2), 1f, 0, 0f);
		}
	}
}
