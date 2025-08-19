using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameInput;
using Terraria.Graphics;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004D0 RID: 1232
	public class NewMultiplayerClosePlayersOverlay : IMultiplayerClosePlayersOverlay
	{
		// Token: 0x06003AC5 RID: 15045 RVA: 0x005ACA08 File Offset: 0x005AAC08
		public void Draw()
		{
			int teamNamePlateDistance = Main.teamNamePlateDistance;
			if (teamNamePlateDistance <= 0)
			{
				return;
			}
			this._playerOnScreenCache.Clear();
			this._playerOffScreenCache.Clear();
			SpriteBatch spriteBatch = Main.spriteBatch;
			PlayerInput.SetZoom_World();
			int screenWidth = Main.screenWidth;
			int screenHeight = Main.screenHeight;
			Vector2 screenPosition = Main.screenPosition;
			PlayerInput.SetZoom_UI();
			int num = teamNamePlateDistance * 8;
			Player[] player = Main.player;
			int myPlayer = Main.myPlayer;
			byte mouseTextColor = Main.mouseTextColor;
			Color[] teamColor = Main.teamColor;
			Vector2 screenPosition2 = Main.screenPosition;
			Player player2 = player[myPlayer];
			float num2 = (float)mouseTextColor / 255f;
			if (player2.team == 0)
			{
				return;
			}
			DynamicSpriteFont value = FontAssets.MouseText.Value;
			for (int i = 0; i < 255; i++)
			{
				if (i != myPlayer)
				{
					Player player3 = player[i];
					if (player3.active && !player3.dead && player3.team == player2.team)
					{
						string name = player3.name;
						Vector2 namePlatePos;
						float namePlateDist;
						Vector2 measurement;
						NewMultiplayerClosePlayersOverlay.GetDistance(screenWidth, screenHeight, screenPosition, player2, value, player3, name, out namePlatePos, out namePlateDist, out measurement);
						Color color;
						color..ctor((int)((byte)((float)teamColor[player3.team].R * num2)), (int)((byte)((float)teamColor[player3.team].G * num2)), (int)((byte)((float)teamColor[player3.team].B * num2)), (int)mouseTextColor);
						if (namePlateDist > 0f)
						{
							float num3 = player3.Distance(player2.Center);
							if (num3 <= (float)num)
							{
								float num4 = 20f;
								float num5 = -27f;
								num5 -= (measurement.X - 85f) / 2f;
								string textValue = Language.GetTextValue("GameUI.PlayerDistance", (int)(num3 / 16f * 2f));
								Vector2 npDistPos = value.MeasureString(textValue);
								npDistPos.X = namePlatePos.X - num5;
								npDistPos.Y = namePlatePos.Y + measurement.Y / 2f - npDistPos.Y / 2f - num4;
								this._playerOffScreenCache.Add(new NewMultiplayerClosePlayersOverlay.PlayerOffScreenCache(name, namePlatePos, color, npDistPos, textValue, player3, measurement));
							}
						}
						else
						{
							this._playerOnScreenCache.Add(new NewMultiplayerClosePlayersOverlay.PlayerOnScreenCache(name, namePlatePos, color));
						}
					}
				}
			}
			spriteBatch.End();
			spriteBatch.Begin(0, null, null, null, null, null, Main.UIScaleMatrix);
			for (int j = 0; j < this._playerOnScreenCache.Count; j++)
			{
				this._playerOnScreenCache[j].DrawPlayerName_WhenPlayerIsOnScreen(spriteBatch);
			}
			for (int k = 0; k < this._playerOffScreenCache.Count; k++)
			{
				this._playerOffScreenCache[k].DrawPlayerName(spriteBatch);
			}
			for (int l = 0; l < this._playerOffScreenCache.Count; l++)
			{
				this._playerOffScreenCache[l].DrawPlayerDistance(spriteBatch);
			}
			spriteBatch.End();
			spriteBatch.Begin(0, null, null, null, null, null, Main.UIScaleMatrix);
			for (int m = 0; m < this._playerOffScreenCache.Count; m++)
			{
				this._playerOffScreenCache[m].DrawLifeBar();
			}
			spriteBatch.End();
			spriteBatch.Begin(1, null, null, null, null, null, Main.UIScaleMatrix);
			for (int n = 0; n < this._playerOffScreenCache.Count; n++)
			{
				this._playerOffScreenCache[n].DrawPlayerHead();
			}
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x005ACD94 File Offset: 0x005AAF94
		private static void GetDistance(int testWidth, int testHeight, Vector2 testPosition, Player localPlayer, DynamicSpriteFont font, Player player, string nameToShow, out Vector2 namePlatePos, out float namePlateDist, out Vector2 measurement)
		{
			float uIScale = Main.UIScale;
			SpriteViewMatrix gameViewMatrix = Main.GameViewMatrix;
			namePlatePos = font.MeasureString(nameToShow);
			float num = 0f;
			if (player.chatOverhead.timeLeft > 0)
			{
				num = (0f - namePlatePos.Y) * uIScale;
			}
			else if (player.emoteTime > 0)
			{
				num = (0f - namePlatePos.Y) * uIScale;
			}
			Vector2 vector;
			vector..ctor((float)(testWidth / 2) + testPosition.X, (float)(testHeight / 2) + testPosition.Y);
			Vector2 position = player.position;
			position += (position - vector) * (gameViewMatrix.Zoom - Vector2.One);
			namePlateDist = 0f;
			float num2 = position.X + (float)(player.width / 2) - vector.X;
			float num3 = position.Y - namePlatePos.Y - 2f + num - vector.Y;
			float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
			int num5 = testHeight;
			if (testHeight > testWidth)
			{
				num5 = testWidth;
			}
			num5 = num5 / 2 - 50;
			if (num5 < 100)
			{
				num5 = 100;
			}
			if (num4 < (float)num5)
			{
				namePlatePos.X = position.X + (float)(player.width / 2) - namePlatePos.X / 2f - testPosition.X;
				namePlatePos.Y = position.Y - namePlatePos.Y - 2f + num - testPosition.Y;
			}
			else
			{
				namePlateDist = num4;
				num4 = (float)num5 / num4;
				namePlatePos.X = (float)(testWidth / 2) + num2 * num4 - namePlatePos.X / 2f;
				namePlatePos.Y = (float)(testHeight / 2) + num3 * num4 + 40f * uIScale;
			}
			measurement = font.MeasureString(nameToShow);
			namePlatePos += measurement / 2f;
			namePlatePos *= 1f / uIScale;
			namePlatePos -= measurement / 2f;
			if (localPlayer.gravDir == -1f)
			{
				namePlatePos.Y = (float)testHeight - namePlatePos.Y;
			}
		}

		// Token: 0x040054C1 RID: 21697
		private List<NewMultiplayerClosePlayersOverlay.PlayerOnScreenCache> _playerOnScreenCache = new List<NewMultiplayerClosePlayersOverlay.PlayerOnScreenCache>();

		// Token: 0x040054C2 RID: 21698
		private List<NewMultiplayerClosePlayersOverlay.PlayerOffScreenCache> _playerOffScreenCache = new List<NewMultiplayerClosePlayersOverlay.PlayerOffScreenCache>();

		// Token: 0x02000BD3 RID: 3027
		private struct PlayerOnScreenCache
		{
			// Token: 0x06005DEE RID: 24046 RVA: 0x006C9B92 File Offset: 0x006C7D92
			public PlayerOnScreenCache(string name, Vector2 pos, Color color)
			{
				this._name = name;
				this._pos = pos;
				this._color = color;
			}

			// Token: 0x06005DEF RID: 24047 RVA: 0x006C9BAC File Offset: 0x006C7DAC
			public void DrawPlayerName_WhenPlayerIsOnScreen(SpriteBatch spriteBatch)
			{
				this._pos = this._pos.Floor();
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X - 2f, this._pos.Y), Color.Black, 0f, default(Vector2), 1f, 0, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X + 2f, this._pos.Y), Color.Black, 0f, default(Vector2), 1f, 0, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X, this._pos.Y - 2f), Color.Black, 0f, default(Vector2), 1f, 0, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X, this._pos.Y + 2f), Color.Black, 0f, default(Vector2), 1f, 0, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, this._pos, this._color, 0f, default(Vector2), 1f, 0, 0f);
			}

			// Token: 0x04007748 RID: 30536
			private string _name;

			// Token: 0x04007749 RID: 30537
			private Vector2 _pos;

			// Token: 0x0400774A RID: 30538
			private Color _color;
		}

		// Token: 0x02000BD4 RID: 3028
		private struct PlayerOffScreenCache
		{
			// Token: 0x06005DF0 RID: 24048 RVA: 0x006C9D5C File Offset: 0x006C7F5C
			public PlayerOffScreenCache(string name, Vector2 pos, Color color, Vector2 npDistPos, string npDist, Player thePlayer, Vector2 theMeasurement)
			{
				this.nameToShow = name;
				this.namePlatePos = pos.Floor();
				this.namePlateColor = color;
				this.distanceDrawPosition = npDistPos.Floor();
				this.distanceString = npDist;
				this.player = thePlayer;
				this.measurement = theMeasurement;
			}

			// Token: 0x06005DF1 RID: 24049 RVA: 0x006C9DA8 File Offset: 0x006C7FA8
			public void DrawPlayerName(SpriteBatch spriteBatch)
			{
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, this.nameToShow, this.namePlatePos + new Vector2(0f, -40f), this.namePlateColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}

			// Token: 0x06005DF2 RID: 24050 RVA: 0x006C9E08 File Offset: 0x006C8008
			public void DrawPlayerHead()
			{
				float num = 20f;
				float num2 = -27f;
				num2 -= (this.measurement.X - 85f) / 2f;
				Color playerHeadBordersColor = Main.GetPlayerHeadBordersColor(this.player);
				Vector2 vec;
				vec..ctor(this.namePlatePos.X, this.namePlatePos.Y - num);
				vec.X -= 22f + num2;
				vec.Y += 8f;
				vec = vec.Floor();
				Main.MapPlayerRenderer.DrawPlayerHead(Main.Camera, this.player, vec, 1f, 0.8f, playerHeadBordersColor);
			}

			// Token: 0x06005DF3 RID: 24051 RVA: 0x006C9EB0 File Offset: 0x006C80B0
			public void DrawLifeBar()
			{
				Vector2 vector = Main.screenPosition + this.distanceDrawPosition + new Vector2(26f, 20f);
				if (this.player.statLife != this.player.statLifeMax2)
				{
					Main.instance.DrawHealthBar(vector.X, vector.Y, this.player.statLife, this.player.statLifeMax2, 1f, 1.25f, true);
				}
			}

			// Token: 0x06005DF4 RID: 24052 RVA: 0x006C9F34 File Offset: 0x006C8134
			public void DrawPlayerDistance(SpriteBatch spriteBatch)
			{
				float scale = 0.85f;
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X - 2f, this.distanceDrawPosition.Y), Color.Black, 0f, default(Vector2), scale, 0, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X + 2f, this.distanceDrawPosition.Y), Color.Black, 0f, default(Vector2), scale, 0, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X, this.distanceDrawPosition.Y - 2f), Color.Black, 0f, default(Vector2), scale, 0, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X, this.distanceDrawPosition.Y + 2f), Color.Black, 0f, default(Vector2), scale, 0, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, this.distanceDrawPosition, this.namePlateColor, 0f, default(Vector2), scale, 0, 0f);
			}

			// Token: 0x0400774B RID: 30539
			private Player player;

			// Token: 0x0400774C RID: 30540
			private string nameToShow;

			// Token: 0x0400774D RID: 30541
			private Vector2 namePlatePos;

			// Token: 0x0400774E RID: 30542
			private Color namePlateColor;

			// Token: 0x0400774F RID: 30543
			private Vector2 distanceDrawPosition;

			// Token: 0x04007750 RID: 30544
			private string distanceString;

			// Token: 0x04007751 RID: 30545
			private Vector2 measurement;
		}
	}
}
