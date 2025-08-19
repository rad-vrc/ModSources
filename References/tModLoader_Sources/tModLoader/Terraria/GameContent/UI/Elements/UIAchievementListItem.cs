using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Achievements;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000501 RID: 1281
	public class UIAchievementListItem : UIPanel
	{
		// Token: 0x06003DBF RID: 15807 RVA: 0x005CC2FC File Offset: 0x005CA4FC
		public UIAchievementListItem(Achievement achievement, bool largeForOtherLanguages)
		{
			this._large = largeForOtherLanguages;
			this.BackgroundColor = new Color(26, 40, 89) * 0.8f;
			this.BorderColor = new Color(13, 20, 44) * 0.8f;
			float num = (float)(16 + this._large.ToInt() * 20);
			float num2 = (float)(this._large.ToInt() * 6);
			float num3 = (float)(this._large.ToInt() * 12);
			this._achievement = achievement;
			this.Height.Set(66f + num, 0f);
			this.Width.Set(0f, 1f);
			this.PaddingTop = 8f;
			this.PaddingLeft = 9f;
			int num4 = this._iconIndex = Main.Achievements.GetIconIndex(achievement.Name);
			this._iconFrameUnlocked = new Rectangle(num4 % 8 * 66, num4 / 8 * 66, 64, 64);
			this._iconFrameLocked = this._iconFrameUnlocked;
			this._iconFrameLocked.X = this._iconFrameLocked.X + 528;
			this._iconFrame = this._iconFrameLocked;
			this.UpdateIconFrame();
			this._achievementIcon = new UIImageFramed(Main.Assets.Request<Texture2D>("Images/UI/Achievements"), this._iconFrame);
			this._achievementIcon.Left.Set(num2, 0f);
			this._achievementIcon.Top.Set(num3, 0f);
			base.Append(this._achievementIcon);
			this._achievementIconBorders = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders"));
			this._achievementIconBorders.Left.Set(-4f + num2, 0f);
			this._achievementIconBorders.Top.Set(-4f + num3, 0f);
			base.Append(this._achievementIconBorders);
			this._innerPanelTopTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_InnerPanelTop");
			if (this._large)
			{
				this._innerPanelBottomTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_InnerPanelBottom_Large");
			}
			else
			{
				this._innerPanelBottomTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_InnerPanelBottom");
			}
			this._categoryTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Categories");
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x005CC544 File Offset: 0x005CA744
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			int num = this._large.ToInt() * 6;
			Vector2 vector;
			vector..ctor((float)num, 0f);
			this._locked = !this._achievement.IsCompleted;
			this.UpdateIconFrame();
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._achievementIconBorders.GetDimensions();
			float num2 = dimensions.X + dimensions.Width;
			Vector2 vector4 = new Vector2(num2 + 7f, innerDimensions.Y);
			Tuple<decimal, decimal> trackerValues = this.GetTrackerValues();
			bool flag = false;
			if ((!(trackerValues.Item1 == 0m) || !(trackerValues.Item2 == 0m)) && this._locked)
			{
				flag = true;
			}
			float num3 = innerDimensions.Width - dimensions.Width + 1f - (float)(num * 2);
			Vector2 baseScale;
			baseScale..ctor(0.85f);
			Vector2 baseScale2;
			baseScale2..ctor(0.92f);
			string text = FontAssets.ItemStack.Value.CreateWrappedText(this._achievement.Description.Value, (num3 - 20f) * (1f / baseScale2.X), Language.ActiveCulture.CultureInfo);
			Vector2 stringSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, text, baseScale2, num3);
			if (!this._large)
			{
				stringSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, this._achievement.Description.Value, baseScale2, num3);
			}
			float num4 = 38f + (float)(this._large ? 20 : 0);
			if (stringSize.Y > num4)
			{
				baseScale2.Y *= num4 / stringSize.Y;
			}
			Color value = this._locked ? Color.Silver : Color.Gold;
			value = Color.Lerp(value, Color.White, base.IsMouseHovering ? 0.5f : 0f);
			Color value2 = this._locked ? Color.DarkGray : Color.Silver;
			value2 = Color.Lerp(value2, Color.White, base.IsMouseHovering ? 1f : 0f);
			Color color = base.IsMouseHovering ? Color.White : Color.Gray;
			Vector2 vector2 = vector4 - Vector2.UnitY * 2f + vector;
			this.DrawPanelTop(spriteBatch, vector2, num3, color);
			AchievementCategory category = this._achievement.Category;
			vector2.Y += 2f;
			vector2.X += 4f;
			spriteBatch.Draw(this._categoryTexture.Value, vector2, new Rectangle?(this._categoryTexture.Frame(4, 2, (int)category, 0, 0, 0)), base.IsMouseHovering ? Color.White : Color.Silver, 0f, Vector2.Zero, 0.5f, 0, 0f);
			vector2.X += 4f;
			vector2.X += 17f;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this._achievement.FriendlyName.Value, vector2, value, 0f, Vector2.Zero, baseScale, num3, 2f);
			vector2.X -= 17f;
			Vector2 position = vector4 + Vector2.UnitY * 27f + vector;
			this.DrawPanelBottom(spriteBatch, position, num3, color);
			position.X += 8f;
			position.Y += 4f;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position, value2, 0f, Vector2.Zero, baseScale2, -1f, 2f);
			if (flag)
			{
				Vector2 vector3 = vector2 + Vector2.UnitX * num3 + Vector2.UnitY;
				string text2 = ((int)trackerValues.Item1).ToString() + "/" + ((int)trackerValues.Item2).ToString();
				Vector2 baseScale3;
				baseScale3..ctor(0.75f);
				Vector2 stringSize2 = ChatManager.GetStringSize(FontAssets.ItemStack.Value, text2, baseScale3, -1f);
				float progress = (float)(trackerValues.Item1 / trackerValues.Item2);
				float num5 = 80f;
				Color color2;
				color2..ctor(100, 255, 100);
				if (!base.IsMouseHovering)
				{
					color2 = Color.Lerp(color2, Color.Black, 0.25f);
				}
				Color color3;
				color3..ctor(255, 255, 255);
				if (!base.IsMouseHovering)
				{
					color3 = Color.Lerp(color3, Color.Black, 0.25f);
				}
				this.DrawProgressBar(spriteBatch, progress, vector3 - Vector2.UnitX * num5 * 0.7f, num5, color3, color2, color2.MultiplyRGBA(new Color(new Vector4(1f, 1f, 1f, 0.5f))));
				vector3.X -= num5 * 1.4f + stringSize2.X;
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text2, vector3, value, 0f, new Vector2(0f, 0f), baseScale3, 90f, 2f);
			}
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x005CCA9D File Offset: 0x005CAC9D
		private void UpdateIconFrame()
		{
			if (!this._locked)
			{
				this._iconFrame = this._iconFrameUnlocked;
			}
			else
			{
				this._iconFrame = this._iconFrameLocked;
			}
			if (this._achievementIcon != null)
			{
				this._achievementIcon.SetFrame(this._iconFrame);
			}
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x005CCADC File Offset: 0x005CACDC
		private void DrawPanelTop(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			spriteBatch.Draw(this._innerPanelTopTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 2, this._innerPanelTopTexture.Height())), color);
			spriteBatch.Draw(this._innerPanelTopTexture.Value, new Vector2(position.X + 2f, position.Y), new Rectangle?(new Rectangle(2, 0, 2, this._innerPanelTopTexture.Height())), color, 0f, Vector2.Zero, new Vector2((width - 4f) / 2f, 1f), 0, 0f);
			spriteBatch.Draw(this._innerPanelTopTexture.Value, new Vector2(position.X + width - 2f, position.Y), new Rectangle?(new Rectangle(4, 0, 2, this._innerPanelTopTexture.Height())), color);
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x005CCBC4 File Offset: 0x005CADC4
		private void DrawPanelBottom(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			spriteBatch.Draw(this._innerPanelBottomTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 6, this._innerPanelBottomTexture.Height())), color);
			spriteBatch.Draw(this._innerPanelBottomTexture.Value, new Vector2(position.X + 6f, position.Y), new Rectangle?(new Rectangle(6, 0, 7, this._innerPanelBottomTexture.Height())), color, 0f, Vector2.Zero, new Vector2((width - 12f) / 7f, 1f), 0, 0f);
			spriteBatch.Draw(this._innerPanelBottomTexture.Value, new Vector2(position.X + width - 6f, position.Y), new Rectangle?(new Rectangle(13, 0, 6, this._innerPanelBottomTexture.Height())), color);
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x005CCCAB File Offset: 0x005CAEAB
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = new Color(46, 60, 119);
			this.BorderColor = new Color(20, 30, 56);
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x005CCCD6 File Offset: 0x005CAED6
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = new Color(26, 40, 89) * 0.8f;
			this.BorderColor = new Color(13, 20, 44) * 0.8f;
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x005CCD15 File Offset: 0x005CAF15
		public Achievement GetAchievement()
		{
			return this._achievement;
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x005CCD20 File Offset: 0x005CAF20
		private Tuple<decimal, decimal> GetTrackerValues()
		{
			if (!this._achievement.HasTracker)
			{
				return Tuple.Create<decimal, decimal>(0m, 0m);
			}
			IAchievementTracker tracker = this._achievement.GetTracker();
			if (tracker.GetTrackerType() == TrackerType.Int)
			{
				AchievementTracker<int> achievementTracker = (AchievementTracker<int>)tracker;
				return Tuple.Create<decimal, decimal>(achievementTracker.Value, achievementTracker.MaxValue);
			}
			if (tracker.GetTrackerType() == TrackerType.Float)
			{
				AchievementTracker<float> achievementTracker2 = (AchievementTracker<float>)tracker;
				return Tuple.Create<decimal, decimal>((decimal)achievementTracker2.Value, (decimal)achievementTracker2.MaxValue);
			}
			return Tuple.Create<decimal, decimal>(0m, 0m);
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x005CCDBC File Offset: 0x005CAFBC
		private void DrawProgressBar(SpriteBatch spriteBatch, float progress, Vector2 spot, float Width = 169f, Color BackColor = default(Color), Color FillingColor = default(Color), Color BlipColor = default(Color))
		{
			if (BlipColor == Color.Transparent)
			{
				BlipColor..ctor(255, 165, 0, 127);
			}
			if (FillingColor == Color.Transparent)
			{
				FillingColor..ctor(255, 241, 51);
			}
			if (BackColor == Color.Transparent)
			{
				FillingColor..ctor(255, 255, 255);
			}
			Texture2D value = TextureAssets.ColorBar.Value;
			Texture2D value3 = TextureAssets.ColorBlip.Value;
			Texture2D value2 = TextureAssets.MagicPixel.Value;
			float num = MathHelper.Clamp(progress, 0f, 1f);
			float num2 = Width * 1f;
			float num3 = 8f;
			float num4 = num2 / 169f;
			Vector2 position = spot + Vector2.UnitY * num3 + Vector2.UnitX * 1f;
			spriteBatch.Draw(value, spot, new Rectangle?(new Rectangle(5, 0, value.Width - 9, value.Height)), BackColor, 0f, new Vector2(84.5f, 0f), new Vector2(num4, 1f), 0, 0f);
			spriteBatch.Draw(value, spot + new Vector2((0f - num4) * 84.5f - 5f, 0f), new Rectangle?(new Rectangle(0, 0, 5, value.Height)), BackColor, 0f, Vector2.Zero, Vector2.One, 0, 0f);
			spriteBatch.Draw(value, spot + new Vector2(num4 * 84.5f, 0f), new Rectangle?(new Rectangle(value.Width - 4, 0, 4, value.Height)), BackColor, 0f, Vector2.Zero, Vector2.One, 0, 0f);
			position += Vector2.UnitX * (num - 0.5f) * num2;
			position.X -= 1f;
			spriteBatch.Draw(value2, position, new Rectangle?(new Rectangle(0, 0, 1, 1)), FillingColor, 0f, new Vector2(1f, 0.5f), new Vector2(num2 * num, num3), 0, 0f);
			if (progress != 0f)
			{
				spriteBatch.Draw(value2, position, new Rectangle?(new Rectangle(0, 0, 1, 1)), BlipColor, 0f, new Vector2(1f, 0.5f), new Vector2(2f, num3), 0, 0f);
			}
			spriteBatch.Draw(value2, position, new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Black, 0f, new Vector2(0f, 0.5f), new Vector2(num2 * (1f - num), num3), 0, 0f);
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x005CD090 File Offset: 0x005CB290
		public override int CompareTo(object obj)
		{
			UIAchievementListItem uIAchievementListItem = obj as UIAchievementListItem;
			if (uIAchievementListItem == null)
			{
				return 0;
			}
			if (this._achievement.IsCompleted && !uIAchievementListItem._achievement.IsCompleted)
			{
				return -1;
			}
			if (!this._achievement.IsCompleted && uIAchievementListItem._achievement.IsCompleted)
			{
				return 1;
			}
			return this._achievement.Id.CompareTo(uIAchievementListItem._achievement.Id);
		}

		// Token: 0x04005686 RID: 22150
		private Achievement _achievement;

		// Token: 0x04005687 RID: 22151
		private UIImageFramed _achievementIcon;

		// Token: 0x04005688 RID: 22152
		private UIImage _achievementIconBorders;

		// Token: 0x04005689 RID: 22153
		private const int _iconSize = 64;

		// Token: 0x0400568A RID: 22154
		private const int _iconSizeWithSpace = 66;

		// Token: 0x0400568B RID: 22155
		private const int _iconsPerRow = 8;

		// Token: 0x0400568C RID: 22156
		private int _iconIndex;

		// Token: 0x0400568D RID: 22157
		private Rectangle _iconFrame;

		// Token: 0x0400568E RID: 22158
		private Rectangle _iconFrameUnlocked;

		// Token: 0x0400568F RID: 22159
		private Rectangle _iconFrameLocked;

		// Token: 0x04005690 RID: 22160
		private Asset<Texture2D> _innerPanelTopTexture;

		// Token: 0x04005691 RID: 22161
		private Asset<Texture2D> _innerPanelBottomTexture;

		// Token: 0x04005692 RID: 22162
		private Asset<Texture2D> _categoryTexture;

		// Token: 0x04005693 RID: 22163
		private bool _locked;

		// Token: 0x04005694 RID: 22164
		private bool _large;
	}
}
