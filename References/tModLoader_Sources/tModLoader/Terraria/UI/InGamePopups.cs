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
	// Token: 0x020000A8 RID: 168
	public class InGamePopups
	{
		// Token: 0x02000867 RID: 2151
		public class AchievementUnlockedPopup : IInGameNotification
		{
			// Token: 0x170008BD RID: 2237
			// (get) Token: 0x06005145 RID: 20805 RVA: 0x00696211 File Offset: 0x00694411
			// (set) Token: 0x06005146 RID: 20806 RVA: 0x00696219 File Offset: 0x00694419
			public bool ShouldBeRemoved { get; private set; }

			// Token: 0x170008BE RID: 2238
			// (get) Token: 0x06005147 RID: 20807 RVA: 0x00696222 File Offset: 0x00694422
			// (set) Token: 0x06005148 RID: 20808 RVA: 0x0069622A File Offset: 0x0069442A
			public object CreationObject { get; private set; }

			// Token: 0x170008BF RID: 2239
			// (get) Token: 0x06005149 RID: 20809 RVA: 0x00696234 File Offset: 0x00694434
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

			// Token: 0x170008C0 RID: 2240
			// (get) Token: 0x0600514A RID: 20810 RVA: 0x006962A0 File Offset: 0x006944A0
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

			// Token: 0x0600514B RID: 20811 RVA: 0x006962D0 File Offset: 0x006944D0
			public AchievementUnlockedPopup(Achievement achievement)
			{
				this.CreationObject = achievement;
				this._ingameDisplayTimeLeft = 300;
				this._theAchievement = achievement;
				this._title = achievement.FriendlyName.Value;
				int num = this._iconIndex = Main.Achievements.GetIconIndex(achievement.Name);
				this._achievementIconFrame = new Rectangle(num % 8 * 66, num / 8 * 66, 64, 64);
				this._achievementTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievements", 2);
				this._achievementBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 2);
			}

			// Token: 0x0600514C RID: 20812 RVA: 0x0069636E File Offset: 0x0069456E
			public void Update()
			{
				this._ingameDisplayTimeLeft--;
				if (this._ingameDisplayTimeLeft < 0)
				{
					this._ingameDisplayTimeLeft = 0;
				}
			}

			// Token: 0x0600514D RID: 20813 RVA: 0x00696390 File Offset: 0x00694590
			public void PushAnchor(ref Vector2 anchorPosition)
			{
				float num = 50f * this.Opacity;
				anchorPosition.Y -= num;
			}

			// Token: 0x0600514E RID: 20814 RVA: 0x006963B8 File Offset: 0x006945B8
			public void DrawInGame(SpriteBatch sb, Vector2 bottomAnchorPosition)
			{
				float opacity = this.Opacity;
				if (opacity > 0f)
				{
					float num = this.Scale * 1.1f;
					Vector2 size = (FontAssets.ItemStack.Value.MeasureString(this._title) + new Vector2(58f, 10f)) * num;
					Rectangle r = Utils.CenteredRectangle(bottomAnchorPosition + new Vector2(0f, (0f - size.Y) * 0.5f), size);
					Vector2 mouseScreen = Main.MouseScreen;
					bool flag = r.Contains(mouseScreen.ToPoint());
					Color color = flag ? (new Color(64, 109, 164) * 0.75f) : (new Color(64, 109, 164) * 0.5f);
					Utils.DrawInvBG(sb, r, color);
					float num2 = num * 0.3f;
					Vector2 vector = r.Right() - Vector2.UnitX * num * (12f + num2 * (float)this._achievementIconFrame.Width);
					sb.Draw(this._achievementTexture.Value, vector, new Rectangle?(this._achievementIconFrame), Color.White * opacity, 0f, new Vector2(0f, (float)(this._achievementIconFrame.Height / 2)), num2, 0, 0f);
					sb.Draw(this._achievementBorderTexture.Value, vector, null, Color.White * opacity, 0f, new Vector2(0f, (float)(this._achievementIconFrame.Height / 2)), num2, 0, 0f);
					color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)(Main.mouseTextColor / 5), (int)Main.mouseTextColor) * opacity;
					Utils.DrawBorderString(sb, this._title, vector - Vector2.UnitX * 10f, color, num * 0.9f, 1f, 0.4f, -1);
					if (flag)
					{
						this.OnMouseOver();
					}
				}
			}

			// Token: 0x0600514F RID: 20815 RVA: 0x006965CC File Offset: 0x006947CC
			private void OnMouseOver()
			{
				if (!PlayerInput.IgnoreMouseInterface)
				{
					Main.player[Main.myPlayer].mouseInterface = true;
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						Main.mouseLeftRelease = false;
						IngameFancyUI.OpenAchievementsAndGoto(this._theAchievement);
						this._ingameDisplayTimeLeft = 0;
						this.ShouldBeRemoved = true;
					}
				}
			}

			// Token: 0x06005150 RID: 20816 RVA: 0x0069661E File Offset: 0x0069481E
			public void DrawInNotificationsArea(SpriteBatch spriteBatch, Rectangle area, ref int gamepadPointLocalIndexTouse)
			{
				Utils.DrawInvBG(spriteBatch, area, Color.Red);
			}

			// Token: 0x0400691F RID: 26911
			private Achievement _theAchievement;

			// Token: 0x04006920 RID: 26912
			private Asset<Texture2D> _achievementTexture;

			// Token: 0x04006921 RID: 26913
			private Asset<Texture2D> _achievementBorderTexture;

			// Token: 0x04006922 RID: 26914
			private const int _iconSize = 64;

			// Token: 0x04006923 RID: 26915
			private const int _iconSizeWithSpace = 66;

			// Token: 0x04006924 RID: 26916
			private const int _iconsPerRow = 8;

			// Token: 0x04006925 RID: 26917
			private int _iconIndex;

			// Token: 0x04006926 RID: 26918
			private Rectangle _achievementIconFrame;

			// Token: 0x04006927 RID: 26919
			private string _title;

			// Token: 0x04006928 RID: 26920
			private int _ingameDisplayTimeLeft;
		}

		// Token: 0x02000868 RID: 2152
		public class PlayerWantsToJoinGamePopup : IInGameNotification
		{
			// Token: 0x170008C1 RID: 2241
			// (get) Token: 0x06005151 RID: 20817 RVA: 0x0069662C File Offset: 0x0069482C
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

			// Token: 0x170008C2 RID: 2242
			// (get) Token: 0x06005152 RID: 20818 RVA: 0x00696698 File Offset: 0x00694898
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

			// Token: 0x170008C3 RID: 2243
			// (get) Token: 0x06005153 RID: 20819 RVA: 0x006966C7 File Offset: 0x006948C7
			// (set) Token: 0x06005154 RID: 20820 RVA: 0x006966CF File Offset: 0x006948CF
			public object CreationObject { get; private set; }

			// Token: 0x170008C4 RID: 2244
			// (get) Token: 0x06005155 RID: 20821 RVA: 0x006966D8 File Offset: 0x006948D8
			public bool ShouldBeRemoved
			{
				get
				{
					return this._timeLeft <= 0;
				}
			}

			// Token: 0x06005156 RID: 20822 RVA: 0x006966E8 File Offset: 0x006948E8
			public PlayerWantsToJoinGamePopup(UserJoinToServerRequest request)
			{
				this._request = request;
				this.CreationObject = request;
				this._timeLeft = 1800;
				switch (Main.rand.Next(5))
				{
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
				default:
					this._displayTextWithoutTime = "This Bloke Wants to Join you";
					return;
				}
			}

			// Token: 0x06005157 RID: 20823 RVA: 0x00696773 File Offset: 0x00694973
			public void Update()
			{
				this._timeLeft--;
			}

			// Token: 0x06005158 RID: 20824 RVA: 0x00696784 File Offset: 0x00694984
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
					Vector2 size = (FontAssets.ItemStack.Value.MeasureString(text) + new Vector2(58f, 10f)) * num;
					Rectangle r = Utils.CenteredRectangle(bottomAnchorPosition + new Vector2(0f, (0f - size.Y) * 0.5f), size);
					Vector2 mouseScreen = Main.MouseScreen;
					Color c = r.Contains(mouseScreen.ToPoint()) ? (new Color(64, 109, 164) * 0.75f) : (new Color(64, 109, 164) * 0.5f);
					Utils.DrawInvBG(spriteBatch, r, c);
					Vector2 vector;
					vector..ctor((float)r.Left, (float)r.Center.Y);
					vector.X += 32f;
					Texture2D value = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay").Value;
					Vector2 vector2;
					vector2..ctor((float)(r.Left + 7), MathHelper.Lerp((float)r.Top, (float)r.Bottom, 0.5f) - (float)(value.Height / 2) - 1f);
					bool flag = Utils.CenteredRectangle(vector2 + new Vector2((float)(value.Width / 2), 0f), value.Size()).Contains(mouseScreen.ToPoint());
					spriteBatch.Draw(value, vector2, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * value.Size(), 1f, 0, 0f);
					if (flag)
					{
						this.OnMouseOver(false);
					}
					value = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete").Value;
					vector2..ctor((float)(r.Left + 7), MathHelper.Lerp((float)r.Top, (float)r.Bottom, 0.5f) + (float)(value.Height / 2) + 1f);
					flag = Utils.CenteredRectangle(vector2 + new Vector2((float)(value.Width / 2), 0f), value.Size()).Contains(mouseScreen.ToPoint());
					spriteBatch.Draw(value, vector2, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * value.Size(), 1f, 0, 0f);
					if (flag)
					{
						this.OnMouseOver(true);
					}
					Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)(Main.mouseTextColor / 5), (int)Main.mouseTextColor) * opacity;
					Utils.DrawBorderString(spriteBatch, text, r.Center.ToVector2() + new Vector2(10f, 0f), color, num * 0.9f, 0.5f, 0.4f, -1);
				}
			}

			// Token: 0x06005159 RID: 20825 RVA: 0x00696AF0 File Offset: 0x00694CF0
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

			// Token: 0x0600515A RID: 20826 RVA: 0x00696B4C File Offset: 0x00694D4C
			public void PushAnchor(ref Vector2 positionAnchorBottom)
			{
				float num = 70f * this.Opacity;
				positionAnchorBottom.Y -= num;
			}

			// Token: 0x0600515B RID: 20827 RVA: 0x00696B74 File Offset: 0x00694D74
			public void DrawInNotificationsArea(SpriteBatch spriteBatch, Rectangle area, ref int gamepadPointLocalIndexTouse)
			{
				string userWrapperText = this._request.GetUserWrapperText();
				string text = this._request.UserDisplayName;
				Utils.TrimTextIfNeeded(ref text, FontAssets.MouseText.Value, 0.9f, (float)(area.Width / 4));
				string text2 = Utils.FormatWith(userWrapperText, new
				{
					DisplayName = text,
					FullId = this._request.UserFullIdentifier
				});
				Vector2 mouseScreen = Main.MouseScreen;
				Color c = area.Contains(mouseScreen.ToPoint()) ? (new Color(64, 109, 164) * 0.75f) : (new Color(64, 109, 164) * 0.5f);
				Utils.DrawInvBG(spriteBatch, area, c);
				Vector2 pos;
				pos..ctor((float)area.Left, (float)area.Center.Y);
				pos.X += 32f;
				Texture2D value = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay").Value;
				Vector2 vector;
				vector..ctor((float)(area.Left + 7), MathHelper.Lerp((float)area.Top, (float)area.Bottom, 0.5f) - (float)(value.Height / 2) - 1f);
				bool flag = Utils.CenteredRectangle(vector + new Vector2((float)(value.Width / 2), 0f), value.Size()).Contains(mouseScreen.ToPoint());
				spriteBatch.Draw(value, vector, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * value.Size(), 1f, 0, 0f);
				if (flag)
				{
					this.OnMouseOver(false);
				}
				value = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete").Value;
				vector..ctor((float)(area.Left + 7), MathHelper.Lerp((float)area.Top, (float)area.Bottom, 0.5f) + (float)(value.Height / 2) + 1f);
				flag = Utils.CenteredRectangle(vector + new Vector2((float)(value.Width / 2), 0f), value.Size()).Contains(mouseScreen.ToPoint());
				spriteBatch.Draw(value, vector, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * value.Size(), 1f, 0, 0f);
				if (flag)
				{
					this.OnMouseOver(true);
				}
				pos.X += 6f;
				Color color;
				color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)(Main.mouseTextColor / 5), (int)Main.mouseTextColor);
				Utils.DrawBorderString(spriteBatch, text2, pos, color, 0.9f, 0f, 0.4f, -1);
			}

			// Token: 0x0400692B RID: 26923
			private int _timeLeft;

			// Token: 0x0400692C RID: 26924
			private const int _timeLeftMax = 1800;

			// Token: 0x0400692D RID: 26925
			private string _displayTextWithoutTime;

			// Token: 0x0400692E RID: 26926
			private UserJoinToServerRequest _request;
		}
	}
}
