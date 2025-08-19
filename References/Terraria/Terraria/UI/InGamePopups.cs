using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Achievements;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Social.Base;

namespace Terraria.UI
{
	// Token: 0x02000090 RID: 144
	public class InGamePopups
	{
		// Token: 0x02000540 RID: 1344
		public class AchievementUnlockedPopup : IInGameNotification
		{
			// Token: 0x170003A0 RID: 928
			// (get) Token: 0x060030BF RID: 12479 RVA: 0x005E3E3B File Offset: 0x005E203B
			// (set) Token: 0x060030C0 RID: 12480 RVA: 0x005E3E43 File Offset: 0x005E2043
			public bool ShouldBeRemoved { get; private set; }

			// Token: 0x170003A1 RID: 929
			// (get) Token: 0x060030C1 RID: 12481 RVA: 0x005E3E4C File Offset: 0x005E204C
			// (set) Token: 0x060030C2 RID: 12482 RVA: 0x005E3E54 File Offset: 0x005E2054
			public object CreationObject { get; private set; }

			// Token: 0x060030C3 RID: 12483 RVA: 0x005E3E60 File Offset: 0x005E2060
			public AchievementUnlockedPopup(Achievement achievement)
			{
				this.CreationObject = achievement;
				this._ingameDisplayTimeLeft = 300;
				this._theAchievement = achievement;
				this._title = achievement.FriendlyName.Value;
				int iconIndex = Main.Achievements.GetIconIndex(achievement.Name);
				this._iconIndex = iconIndex;
				this._achievementIconFrame = new Rectangle(iconIndex % 8 * 66, iconIndex / 8 * 66, 64, 64);
				this._achievementTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievements", 2);
				this._achievementBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 2);
			}

			// Token: 0x060030C4 RID: 12484 RVA: 0x005E3EFC File Offset: 0x005E20FC
			public void Update()
			{
				this._ingameDisplayTimeLeft--;
				if (this._ingameDisplayTimeLeft < 0)
				{
					this._ingameDisplayTimeLeft = 0;
				}
			}

			// Token: 0x170003A2 RID: 930
			// (get) Token: 0x060030C5 RID: 12485 RVA: 0x005E3F1C File Offset: 0x005E211C
			private float Scale
			{
				get
				{
					if (this._ingameDisplayTimeLeft < 30)
					{
						return MathHelper.Lerp(0f, 1f, (float)this._ingameDisplayTimeLeft / 30f);
					}
					if (this._ingameDisplayTimeLeft > 285)
					{
						return MathHelper.Lerp(1f, 0f, ((float)this._ingameDisplayTimeLeft - 285f) / 15f);
					}
					return 1f;
				}
			}

			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x060030C6 RID: 12486 RVA: 0x005E3F88 File Offset: 0x005E2188
			private float Opacity
			{
				get
				{
					float scale = this.Scale;
					if (scale <= 0.5f)
					{
						return 0f;
					}
					return (scale - 0.5f) / 0.5f;
				}
			}

			// Token: 0x060030C7 RID: 12487 RVA: 0x005E3FB8 File Offset: 0x005E21B8
			public void PushAnchor(ref Vector2 anchorPosition)
			{
				float num = 50f * this.Opacity;
				anchorPosition.Y -= num;
			}

			// Token: 0x060030C8 RID: 12488 RVA: 0x005E3FE0 File Offset: 0x005E21E0
			public void DrawInGame(SpriteBatch sb, Vector2 bottomAnchorPosition)
			{
				float opacity = this.Opacity;
				if (opacity > 0f)
				{
					float num = this.Scale * 1.1f;
					Vector2 vector = (FontAssets.ItemStack.Value.MeasureString(this._title) + new Vector2(58f, 10f)) * num;
					Rectangle r = Utils.CenteredRectangle(bottomAnchorPosition + new Vector2(0f, -vector.Y * 0.5f), vector);
					Vector2 mouseScreen = Main.MouseScreen;
					bool flag = r.Contains(mouseScreen.ToPoint());
					Color c = flag ? (new Color(64, 109, 164) * 0.75f) : (new Color(64, 109, 164) * 0.5f);
					Utils.DrawInvBG(sb, r, c);
					float num2 = num * 0.3f;
					Vector2 vector2 = r.Right() - Vector2.UnitX * num * (12f + num2 * (float)this._achievementIconFrame.Width);
					sb.Draw(this._achievementTexture.Value, vector2, new Rectangle?(this._achievementIconFrame), Color.White * opacity, 0f, new Vector2(0f, (float)(this._achievementIconFrame.Height / 2)), num2, SpriteEffects.None, 0f);
					sb.Draw(this._achievementBorderTexture.Value, vector2, null, Color.White * opacity, 0f, new Vector2(0f, (float)(this._achievementIconFrame.Height / 2)), num2, SpriteEffects.None, 0f);
					Color value = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)(Main.mouseTextColor / 5), (int)Main.mouseTextColor);
					Utils.DrawBorderString(sb, this._title, vector2 - Vector2.UnitX * 10f, value * opacity, num * 0.9f, 1f, 0.4f, -1);
					if (flag)
					{
						this.OnMouseOver();
					}
				}
			}

			// Token: 0x060030C9 RID: 12489 RVA: 0x005E41F0 File Offset: 0x005E23F0
			private void OnMouseOver()
			{
				if (PlayerInput.IgnoreMouseInterface)
				{
					return;
				}
				Main.player[Main.myPlayer].mouseInterface = true;
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					Main.mouseLeftRelease = false;
					IngameFancyUI.OpenAchievementsAndGoto(this._theAchievement);
					this._ingameDisplayTimeLeft = 0;
					this.ShouldBeRemoved = true;
				}
			}

			// Token: 0x060030CA RID: 12490 RVA: 0x005E4243 File Offset: 0x005E2443
			public void DrawInNotificationsArea(SpriteBatch spriteBatch, Rectangle area, ref int gamepadPointLocalIndexTouse)
			{
				Utils.DrawInvBG(spriteBatch, area, Color.Red);
			}

			// Token: 0x04005851 RID: 22609
			private Achievement _theAchievement;

			// Token: 0x04005852 RID: 22610
			private Asset<Texture2D> _achievementTexture;

			// Token: 0x04005853 RID: 22611
			private Asset<Texture2D> _achievementBorderTexture;

			// Token: 0x04005854 RID: 22612
			private const int _iconSize = 64;

			// Token: 0x04005855 RID: 22613
			private const int _iconSizeWithSpace = 66;

			// Token: 0x04005856 RID: 22614
			private const int _iconsPerRow = 8;

			// Token: 0x04005857 RID: 22615
			private int _iconIndex;

			// Token: 0x04005858 RID: 22616
			private Rectangle _achievementIconFrame;

			// Token: 0x04005859 RID: 22617
			private string _title;

			// Token: 0x0400585A RID: 22618
			private int _ingameDisplayTimeLeft;
		}

		// Token: 0x02000541 RID: 1345
		public class PlayerWantsToJoinGamePopup : IInGameNotification
		{
			// Token: 0x170003A4 RID: 932
			// (get) Token: 0x060030CB RID: 12491 RVA: 0x005E4254 File Offset: 0x005E2454
			private float Scale
			{
				get
				{
					if (this._timeLeft < 30)
					{
						return MathHelper.Lerp(0f, 1f, (float)this._timeLeft / 30f);
					}
					if (this._timeLeft > 1785)
					{
						return MathHelper.Lerp(1f, 0f, ((float)this._timeLeft - 1785f) / 15f);
					}
					return 1f;
				}
			}

			// Token: 0x170003A5 RID: 933
			// (get) Token: 0x060030CC RID: 12492 RVA: 0x005E42C0 File Offset: 0x005E24C0
			private float Opacity
			{
				get
				{
					float scale = this.Scale;
					if (scale <= 0.5f)
					{
						return 0f;
					}
					return (scale - 0.5f) / 0.5f;
				}
			}

			// Token: 0x170003A6 RID: 934
			// (get) Token: 0x060030CD RID: 12493 RVA: 0x005E42EF File Offset: 0x005E24EF
			// (set) Token: 0x060030CE RID: 12494 RVA: 0x005E42F7 File Offset: 0x005E24F7
			public object CreationObject { get; private set; }

			// Token: 0x060030CF RID: 12495 RVA: 0x005E4300 File Offset: 0x005E2500
			public PlayerWantsToJoinGamePopup(UserJoinToServerRequest request)
			{
				this._request = request;
				this.CreationObject = request;
				this._timeLeft = 1800;
				switch (Main.rand.Next(5))
				{
				default:
					this._displayTextWithoutTime = "This Bloke Wants to Join you";
					return;
				case 1:
					this._displayTextWithoutTime = "This Fucker Wants to Join you";
					return;
				case 2:
					this._displayTextWithoutTime = "This Weirdo Wants to Join you";
					return;
				case 3:
					this._displayTextWithoutTime = "This Great Gal Wants to Join you";
					return;
				case 4:
					this._displayTextWithoutTime = "The one guy who beat you up 30 years ago Wants to Join you";
					return;
				}
			}

			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x060030D0 RID: 12496 RVA: 0x005E438D File Offset: 0x005E258D
			public bool ShouldBeRemoved
			{
				get
				{
					return this._timeLeft <= 0;
				}
			}

			// Token: 0x060030D1 RID: 12497 RVA: 0x005E439B File Offset: 0x005E259B
			public void Update()
			{
				this._timeLeft--;
			}

			// Token: 0x060030D2 RID: 12498 RVA: 0x005E43AC File Offset: 0x005E25AC
			public void DrawInGame(SpriteBatch spriteBatch, Vector2 bottomAnchorPosition)
			{
				float opacity = this.Opacity;
				if (opacity > 0f)
				{
					string text = Utils.FormatWith(this._request.GetUserWrapperText(), new
					{
						DisplayName = this._request.UserDisplayName,
						FullId = this._request.UserFullIdentifier
					});
					float num = this.Scale * 1.1f;
					Vector2 vector = (FontAssets.ItemStack.Value.MeasureString(text) + new Vector2(58f, 10f)) * num;
					Rectangle r = Utils.CenteredRectangle(bottomAnchorPosition + new Vector2(0f, -vector.Y * 0.5f), vector);
					Vector2 mouseScreen = Main.MouseScreen;
					Color c = r.Contains(mouseScreen.ToPoint()) ? (new Color(64, 109, 164) * 0.75f) : (new Color(64, 109, 164) * 0.5f);
					Utils.DrawInvBG(spriteBatch, r, c);
					Vector2 vector2 = new Vector2((float)r.Left, (float)r.Center.Y);
					vector2.X += 32f;
					Texture2D value = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", 1).Value;
					Vector2 vector3 = new Vector2((float)(r.Left + 7), MathHelper.Lerp((float)r.Top, (float)r.Bottom, 0.5f) - (float)(value.Height / 2) - 1f);
					bool flag = Utils.CenteredRectangle(vector3 + new Vector2((float)(value.Width / 2), 0f), value.Size()).Contains(mouseScreen.ToPoint());
					spriteBatch.Draw(value, vector3, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * value.Size(), 1f, SpriteEffects.None, 0f);
					if (flag)
					{
						this.OnMouseOver(false);
					}
					value = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete", 1).Value;
					vector3 = new Vector2((float)(r.Left + 7), MathHelper.Lerp((float)r.Top, (float)r.Bottom, 0.5f) + (float)(value.Height / 2) + 1f);
					flag = Utils.CenteredRectangle(vector3 + new Vector2((float)(value.Width / 2), 0f), value.Size()).Contains(mouseScreen.ToPoint());
					spriteBatch.Draw(value, vector3, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * value.Size(), 1f, SpriteEffects.None, 0f);
					if (flag)
					{
						this.OnMouseOver(true);
					}
					Color value2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)(Main.mouseTextColor / 5), (int)Main.mouseTextColor);
					Utils.DrawBorderString(spriteBatch, text, r.Center.ToVector2() + new Vector2(10f, 0f), value2 * opacity, num * 0.9f, 0.5f, 0.4f, -1);
				}
			}

			// Token: 0x060030D3 RID: 12499 RVA: 0x005E4718 File Offset: 0x005E2918
			private void OnMouseOver(bool reject = false)
			{
				if (PlayerInput.IgnoreMouseInterface)
				{
					return;
				}
				Main.player[Main.myPlayer].mouseInterface = true;
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					Main.mouseLeftRelease = false;
					this._timeLeft = 0;
					if (reject)
					{
						this._request.Reject();
						return;
					}
					this._request.Accept();
				}
			}

			// Token: 0x060030D4 RID: 12500 RVA: 0x005E4774 File Offset: 0x005E2974
			public void PushAnchor(ref Vector2 positionAnchorBottom)
			{
				float num = 70f * this.Opacity;
				positionAnchorBottom.Y -= num;
			}

			// Token: 0x060030D5 RID: 12501 RVA: 0x005E479C File Offset: 0x005E299C
			public void DrawInNotificationsArea(SpriteBatch spriteBatch, Rectangle area, ref int gamepadPointLocalIndexTouse)
			{
				string userWrapperText = this._request.GetUserWrapperText();
				string userDisplayName = this._request.UserDisplayName;
				Utils.TrimTextIfNeeded(ref userDisplayName, FontAssets.MouseText.Value, 0.9f, (float)(area.Width / 4));
				string text = Utils.FormatWith(userWrapperText, new
				{
					DisplayName = userDisplayName,
					FullId = this._request.UserFullIdentifier
				});
				Vector2 mouseScreen = Main.MouseScreen;
				Color c = area.Contains(mouseScreen.ToPoint()) ? (new Color(64, 109, 164) * 0.75f) : (new Color(64, 109, 164) * 0.5f);
				Utils.DrawInvBG(spriteBatch, area, c);
				Vector2 pos = new Vector2((float)area.Left, (float)area.Center.Y);
				pos.X += 32f;
				Texture2D value = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", 1).Value;
				Vector2 vector = new Vector2((float)(area.Left + 7), MathHelper.Lerp((float)area.Top, (float)area.Bottom, 0.5f) - (float)(value.Height / 2) - 1f);
				bool flag = Utils.CenteredRectangle(vector + new Vector2((float)(value.Width / 2), 0f), value.Size()).Contains(mouseScreen.ToPoint());
				spriteBatch.Draw(value, vector, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * value.Size(), 1f, SpriteEffects.None, 0f);
				if (flag)
				{
					this.OnMouseOver(false);
				}
				value = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete", 1).Value;
				vector = new Vector2((float)(area.Left + 7), MathHelper.Lerp((float)area.Top, (float)area.Bottom, 0.5f) + (float)(value.Height / 2) + 1f);
				flag = Utils.CenteredRectangle(vector + new Vector2((float)(value.Width / 2), 0f), value.Size()).Contains(mouseScreen.ToPoint());
				spriteBatch.Draw(value, vector, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * value.Size(), 1f, SpriteEffects.None, 0f);
				if (flag)
				{
					this.OnMouseOver(true);
				}
				pos.X += 6f;
				Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)(Main.mouseTextColor / 5), (int)Main.mouseTextColor);
				Utils.DrawBorderString(spriteBatch, text, pos, color, 0.9f, 0f, 0.4f, -1);
			}

			// Token: 0x0400585D RID: 22621
			private int _timeLeft;

			// Token: 0x0400585E RID: 22622
			private const int _timeLeftMax = 1800;

			// Token: 0x0400585F RID: 22623
			private string _displayTextWithoutTime;

			// Token: 0x04005860 RID: 22624
			private UserJoinToServerRequest _request;
		}
	}
}
