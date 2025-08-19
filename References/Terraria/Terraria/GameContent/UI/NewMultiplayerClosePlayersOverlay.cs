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
	// Token: 0x02000333 RID: 819
	public class NewMultiplayerClosePlayersOverlay : IMultiplayerClosePlayersOverlay
	{
		// Token: 0x060024FB RID: 9467 RVA: 0x00567028 File Offset: 0x00565228
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
						Vector2 vector;
						float num3;
						Vector2 vector2;
						NewMultiplayerClosePlayersOverlay.GetDistance(screenWidth, screenHeight, screenPosition, player2, value, player3, name, out vector, out num3, out vector2);
						Color color = new Color((int)((byte)((float)teamColor[player3.team].R * num2)), (int)((byte)((float)teamColor[player3.team].G * num2)), (int)((byte)((float)teamColor[player3.team].B * num2)), (int)mouseTextColor);
						if (num3 > 0f)
						{
							float num4 = player3.Distance(player2.Center);
							if (num4 <= (float)num)
							{
								float num5 = 20f;
								float num6 = -27f;
								num6 -= (vector2.X - 85f) / 2f;
								string textValue = Language.GetTextValue("GameUI.PlayerDistance", (int)(num4 / 16f * 2f));
								Vector2 vector3 = value.MeasureString(textValue);
								vector3.X = vector.X - num6;
								vector3.Y = vector.Y + vector2.Y / 2f - vector3.Y / 2f - num5;
								this._playerOffScreenCache.Add(new NewMultiplayerClosePlayersOverlay.PlayerOffScreenCache(name, vector, color, vector3, textValue, player3, vector2));
							}
						}
						else
						{
							this._playerOnScreenCache.Add(new NewMultiplayerClosePlayersOverlay.PlayerOnScreenCache(name, vector, color));
						}
					}
				}
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
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
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
			for (int m = 0; m < this._playerOffScreenCache.Count; m++)
			{
				this._playerOffScreenCache[m].DrawLifeBar();
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
			for (int n = 0; n < this._playerOffScreenCache.Count; n++)
			{
				this._playerOffScreenCache[n].DrawPlayerHead();
			}
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x005673B4 File Offset: 0x005655B4
		private static void GetDistance(int testWidth, int testHeight, Vector2 testPosition, Player localPlayer, DynamicSpriteFont font, Player player, string nameToShow, out Vector2 namePlatePos, out float namePlateDist, out Vector2 measurement)
		{
			float uiscale = Main.UIScale;
			SpriteViewMatrix gameViewMatrix = Main.GameViewMatrix;
			namePlatePos = font.MeasureString(nameToShow);
			float num = 0f;
			if (player.chatOverhead.timeLeft > 0)
			{
				num = -namePlatePos.Y * uiscale;
			}
			else if (player.emoteTime > 0)
			{
				num = -namePlatePos.Y * uiscale;
			}
			Vector2 vector = new Vector2((float)(testWidth / 2) + testPosition.X, (float)(testHeight / 2) + testPosition.Y);
			Vector2 vector2 = player.position;
			vector2 += (vector2 - vector) * (gameViewMatrix.Zoom - Vector2.One);
			namePlateDist = 0f;
			float num2 = vector2.X + (float)(player.width / 2) - vector.X;
			float num3 = vector2.Y - namePlatePos.Y - 2f + num - vector.Y;
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
				namePlatePos.X = vector2.X + (float)(player.width / 2) - namePlatePos.X / 2f - testPosition.X;
				namePlatePos.Y = vector2.Y - namePlatePos.Y - 2f + num - testPosition.Y;
			}
			else
			{
				namePlateDist = num4;
				num4 = (float)num5 / num4;
				namePlatePos.X = (float)(testWidth / 2) + num2 * num4 - namePlatePos.X / 2f;
				namePlatePos.Y = (float)(testHeight / 2) + num3 * num4 + 40f * uiscale;
			}
			measurement = font.MeasureString(nameToShow);
			namePlatePos += measurement / 2f;
			namePlatePos *= 1f / uiscale;
			namePlatePos -= measurement / 2f;
			if (localPlayer.gravDir == -1f)
			{
				namePlatePos.Y = (float)testHeight - namePlatePos.Y;
			}
		}

		// Token: 0x040048FC RID: 18684
		private List<NewMultiplayerClosePlayersOverlay.PlayerOnScreenCache> _playerOnScreenCache = new List<NewMultiplayerClosePlayersOverlay.PlayerOnScreenCache>();

		// Token: 0x040048FD RID: 18685
		private List<NewMultiplayerClosePlayersOverlay.PlayerOffScreenCache> _playerOffScreenCache = new List<NewMultiplayerClosePlayersOverlay.PlayerOffScreenCache>();

		// Token: 0x0200071A RID: 1818
		private struct PlayerOnScreenCache
		{
			// Token: 0x060037C0 RID: 14272 RVA: 0x006111F8 File Offset: 0x0060F3F8
			public PlayerOnScreenCache(string name, Vector2 pos, Color color)
			{
				this._name = name;
				this._pos = pos;
				this._color = color;
			}

			// Token: 0x060037C1 RID: 14273 RVA: 0x00611210 File Offset: 0x0060F410
			public void DrawPlayerName_WhenPlayerIsOnScreen(SpriteBatch spriteBatch)
			{
				this._pos = this._pos.Floor();
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X - 2f, this._pos.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X + 2f, this._pos.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X, this._pos.Y - 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X, this._pos.Y + 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, this._pos, this._color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			}

			// Token: 0x0400632D RID: 25389
			private string _name;

			// Token: 0x0400632E RID: 25390
			private Vector2 _pos;

			// Token: 0x0400632F RID: 25391
			private Color _color;
		}

		// Token: 0x0200071B RID: 1819
		private struct PlayerOffScreenCache
		{
			// Token: 0x060037C2 RID: 14274 RVA: 0x006113C0 File Offset: 0x0060F5C0
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

			// Token: 0x060037C3 RID: 14275 RVA: 0x0061140C File Offset: 0x0060F60C
			public void DrawPlayerName(SpriteBatch spriteBatch)
			{
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, this.nameToShow, this.namePlatePos + new Vector2(0f, -40f), this.namePlateColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}

			// Token: 0x060037C4 RID: 14276 RVA: 0x0061146C File Offset: 0x0060F66C
			public void DrawPlayerHead()
			{
				float num = 20f;
				float num2 = -27f;
				num2 -= (this.measurement.X - 85f) / 2f;
				Color playerHeadBordersColor = Main.GetPlayerHeadBordersColor(this.player);
				Vector2 vector = new Vector2(this.namePlatePos.X, this.namePlatePos.Y - num);
				vector.X -= 22f + num2;
				vector.Y += 8f;
				vector = vector.Floor();
				Main.MapPlayerRenderer.DrawPlayerHead(Main.Camera, this.player, vector, 1f, 0.8f, playerHeadBordersColor);
			}

			// Token: 0x060037C5 RID: 14277 RVA: 0x00611514 File Offset: 0x0060F714
			public void DrawLifeBar()
			{
				Vector2 vector = Main.screenPosition + this.distanceDrawPosition + new Vector2(26f, 20f);
				if (this.player.statLife != this.player.statLifeMax2)
				{
					Main.instance.DrawHealthBar(vector.X, vector.Y, this.player.statLife, this.player.statLifeMax2, 1f, 1.25f, true);
				}
			}

			// Token: 0x060037C6 RID: 14278 RVA: 0x00611598 File Offset: 0x0060F798
			public void DrawPlayerDistance(SpriteBatch spriteBatch)
			{
				float num = 0.85f;
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X - 2f, this.distanceDrawPosition.Y), Color.Black, 0f, default(Vector2), num, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X + 2f, this.distanceDrawPosition.Y), Color.Black, 0f, default(Vector2), num, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X, this.distanceDrawPosition.Y - 2f), Color.Black, 0f, default(Vector2), num, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X, this.distanceDrawPosition.Y + 2f), Color.Black, 0f, default(Vector2), num, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, this.distanceDrawPosition, this.namePlateColor, 0f, default(Vector2), num, SpriteEffects.None, 0f);
			}

			// Token: 0x04006330 RID: 25392
			private Player player;

			// Token: 0x04006331 RID: 25393
			private string nameToShow;

			// Token: 0x04006332 RID: 25394
			private Vector2 namePlatePos;

			// Token: 0x04006333 RID: 25395
			private Color namePlateColor;

			// Token: 0x04006334 RID: 25396
			private Vector2 distanceDrawPosition;

			// Token: 0x04006335 RID: 25397
			private string distanceString;

			// Token: 0x04006336 RID: 25398
			private Vector2 measurement;
		}
	}
}
