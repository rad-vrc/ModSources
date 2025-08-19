using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Achievements;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004D5 RID: 1237
	public class UIAchievementsMenu : UIState
	{
		// Token: 0x06003B00 RID: 15104 RVA: 0x005AF25C File Offset: 0x005AD45C
		public void InitializePage()
		{
			base.RemoveAllChildren();
			this._categoryButtons.Clear();
			this._achievementElements.Clear();
			this._achievementsList = null;
			bool flag = true;
			int num = flag.ToInt() * 100;
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(0f, 0.8f);
			uIElement.MaxWidth.Set(800f + (float)num, 0f);
			uIElement.MinWidth.Set(600f + (float)num, 0f);
			uIElement.Top.Set(220f, 0f);
			uIElement.Height.Set(-220f, 1f);
			uIElement.HAlign = 0.5f;
			this._outerContainer = uIElement;
			base.Append(uIElement);
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set(-110f, 1f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uIPanel.PaddingTop = 0f;
			uIElement.Append(uIPanel);
			this._achievementsList = new UIList();
			this._achievementsList.Width.Set(-25f, 1f);
			this._achievementsList.Height.Set(-50f, 1f);
			this._achievementsList.Top.Set(50f, 0f);
			this._achievementsList.ListPadding = 5f;
			uIPanel.Append(this._achievementsList);
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Achievements"), 1f, true);
			uITextPanel.HAlign = 0.5f;
			uITextPanel.Top.Set(-33f, 0f);
			uITextPanel.SetPadding(13f);
			uITextPanel.BackgroundColor = new Color(73, 94, 171);
			uIElement.Append(uITextPanel);
			UITextPanel<LocalizedText> uITextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uITextPanel2.Width.Set(-10f, 0.5f);
			uITextPanel2.Height.Set(50f, 0f);
			uITextPanel2.VAlign = 1f;
			uITextPanel2.HAlign = 0.5f;
			uITextPanel2.Top.Set(-45f, 0f);
			uITextPanel2.OnMouseOver += this.FadedMouseOver;
			uITextPanel2.OnMouseOut += this.FadedMouseOut;
			uITextPanel2.OnLeftClick += this.GoBackClick;
			uIElement.Append(uITextPanel2);
			this._backpanel = uITextPanel2;
			List<Achievement> list = Main.Achievements.CreateAchievementsList();
			for (int i = 0; i < list.Count; i++)
			{
				UIAchievementListItem item = new UIAchievementListItem(list[i], flag);
				this._achievementsList.Add(item);
				this._achievementElements.Add(item);
			}
			UIScrollbar uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(-50f, 1f);
			uIScrollbar.Top.Set(50f, 0f);
			uIScrollbar.HAlign = 1f;
			uIPanel.Append(uIScrollbar);
			this._achievementsList.SetScrollbar(uIScrollbar);
			UIElement uIElement2 = new UIElement();
			uIElement2.Width.Set(0f, 1f);
			uIElement2.Height.Set(32f, 0f);
			uIElement2.Top.Set(10f, 0f);
			Asset<Texture2D> texture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Categories");
			for (int j = 0; j < 4; j++)
			{
				UIToggleImage uIToggleImage = new UIToggleImage(texture, 32, 32, new Point(34 * j, 0), new Point(34 * j, 34));
				uIToggleImage.Left.Set((float)(j * 36 + 8), 0f);
				uIToggleImage.SetState(true);
				uIToggleImage.OnLeftClick += this.FilterList;
				this._categoryButtons.Add(uIToggleImage);
				uIElement2.Append(uIToggleImage);
			}
			uIPanel.Append(uIElement2);
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x005AF6A8 File Offset: 0x005AD8A8
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			for (int i = 0; i < this._categoryButtons.Count; i++)
			{
				if (this._categoryButtons[i].IsMouseHovering)
				{
					string text;
					switch (i)
					{
					case -1:
						text = Language.GetTextValue("Achievements.NoCategory");
						break;
					case 0:
						text = Language.GetTextValue("Achievements.SlayerCategory");
						break;
					case 1:
						text = Language.GetTextValue("Achievements.CollectorCategory");
						break;
					case 2:
						text = Language.GetTextValue("Achievements.ExplorerCategory");
						break;
					case 3:
						text = Language.GetTextValue("Achievements.ChallengerCategory");
						break;
					default:
						text = Language.GetTextValue("Achievements.NoCategory");
						break;
					}
					float x = FontAssets.MouseText.Value.MeasureString(text).X;
					Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY) + new Vector2(16f);
					if (vector.Y > (float)(Main.screenHeight - 30))
					{
						vector.Y = (float)(Main.screenHeight - 30);
					}
					if (vector.X > (float)Main.screenWidth - x)
					{
						vector.X = (float)(Main.screenWidth - 460);
					}
					Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, vector.X, vector.Y, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, Vector2.Zero, 1f);
					break;
				}
			}
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x005AF828 File Offset: 0x005ADA28
		public void GotoAchievement(Achievement achievement)
		{
			this._achievementsList.Goto(delegate(UIElement element)
			{
				UIAchievementListItem uIAchievementListItem = element as UIAchievementListItem;
				return uIAchievementListItem != null && uIAchievementListItem.GetAchievement() == achievement;
			});
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x005AF859 File Offset: 0x005ADA59
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.menuMode = 0;
			IngameFancyUI.Close();
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x005AF868 File Offset: 0x005ADA68
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x005AF8BD File Offset: 0x005ADABD
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x005AF8FC File Offset: 0x005ADAFC
		private void FilterList(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._achievementsList.Clear();
			foreach (UIAchievementListItem achievementElement in this._achievementElements)
			{
				if (this._categoryButtons[(int)achievementElement.GetAchievement().Category].IsOn)
				{
					this._achievementsList.Add(achievementElement);
				}
			}
			this.Recalculate();
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x005AF998 File Offset: 0x005ADB98
		public override void OnActivate()
		{
			this.InitializePage();
			if (Main.gameMenu)
			{
				this._outerContainer.Top.Set(220f, 0f);
				this._outerContainer.Height.Set(-220f, 1f);
			}
			else
			{
				this._outerContainer.Top.Set(120f, 0f);
				this._outerContainer.Height.Set(-120f, 1f);
			}
			this._achievementsList.UpdateOrder();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3002);
			}
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x005AFA38 File Offset: 0x005ADC38
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 3;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backpanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num + 1, this._outerContainer.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num2 = num;
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Up = num2 + 1;
			num2++;
			UILinkPoint uilinkPoint2 = UILinkPointNavigator.Points[num2];
			uilinkPoint2.Unlink();
			uilinkPoint2.Up = num2 + 1;
			uilinkPoint2.Down = num2 - 1;
			for (int i = 0; i < this._categoryButtons.Count; i++)
			{
				num2 = (UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num2 + 1);
				UILinkPointNavigator.SetPosition(num2, this._categoryButtons[i].GetInnerDimensions().ToRectangle().Center.ToVector2());
				UILinkPoint uilinkPoint3 = UILinkPointNavigator.Points[num2];
				uilinkPoint3.Unlink();
				uilinkPoint3.Left = ((i == 0) ? -3 : (num2 - 1));
				uilinkPoint3.Right = ((i == this._categoryButtons.Count - 1) ? -4 : (num2 + 1));
				uilinkPoint3.Down = num;
			}
		}

		// Token: 0x040054E8 RID: 21736
		private UIList _achievementsList;

		// Token: 0x040054E9 RID: 21737
		private List<UIAchievementListItem> _achievementElements = new List<UIAchievementListItem>();

		// Token: 0x040054EA RID: 21738
		private List<UIToggleImage> _categoryButtons = new List<UIToggleImage>();

		// Token: 0x040054EB RID: 21739
		private UIElement _backpanel;

		// Token: 0x040054EC RID: 21740
		private UIElement _outerContainer;
	}
}
