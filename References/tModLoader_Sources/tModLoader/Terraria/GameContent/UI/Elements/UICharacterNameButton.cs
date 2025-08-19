using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200050D RID: 1293
	public class UICharacterNameButton : UIElement
	{
		// Token: 0x06003E47 RID: 15943 RVA: 0x005D1334 File Offset: 0x005CF534
		public UICharacterNameButton(LocalizedText titleText, LocalizedText emptyContentText, LocalizedText description = null)
		{
			this.Width = StyleDimension.FromPixels(400f);
			this.Height = StyleDimension.FromPixels(40f);
			this.Description = description;
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel");
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight");
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");
			this._textToShowWhenEmpty = emptyContentText;
			float textScale = 1f;
			UIText uIText = new UIText(titleText, textScale, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = StyleDimension.FromPixels(10f)
			};
			base.Append(uIText);
			this._title = uIText;
			UIText uIText2 = new UIText(Language.GetText("UI.PlayerNameSlot"), textScale, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = StyleDimension.FromPixels(150f)
			};
			base.Append(uIText2);
			this._text = uIText2;
			this.SetContents(null);
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x005D1450 File Offset: 0x005CF650
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._hovered)
			{
				if (!this._soundedHover)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				this._soundedHover = true;
			}
			else
			{
				this._soundedHover = false;
			}
			CalculatedStyle dimensions = base.GetDimensions();
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.White * 0.5f);
			if (this._hovered)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.White);
			}
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x005D1520 File Offset: 0x005CF720
		public void SetContents(string name)
		{
			this.actualContents = name;
			if (string.IsNullOrEmpty(this.actualContents))
			{
				this._text.TextColor = Color.Gray;
				this._text.SetText(this._textToShowWhenEmpty);
			}
			else
			{
				this._text.TextColor = Color.White;
				this._text.SetText(this.actualContents);
			}
			this._text.Left = StyleDimension.FromPixels(this._title.GetInnerDimensions().Width + this.DistanceFromTitleToOption);
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x005D15AC File Offset: 0x005CF7AC
		public void TrimDisplayIfOverElementDimensions(int padding)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Point point;
			point..ctor((int)dimensions.X, (int)dimensions.Y);
			Point point2;
			point2..ctor(point.X + (int)dimensions.Width, point.Y + (int)dimensions.Height);
			Rectangle rectangle;
			rectangle..ctor(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
			CalculatedStyle dimensions2 = this._text.GetDimensions();
			Point point3;
			point3..ctor((int)dimensions2.X, (int)dimensions2.Y);
			Point point4;
			point4..ctor(point3.X + (int)dimensions2.Width, point3.Y + (int)dimensions2.Height);
			Rectangle rectangle2;
			rectangle2..ctor(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
			int num = 0;
			while (rectangle2.Right > rectangle.Right - padding)
			{
				this._text.SetText(this._text.Text.Substring(0, this._text.Text.Length - 1));
				num++;
				this.RecalculateChildren();
				dimensions2 = this._text.GetDimensions();
				point3..ctor((int)dimensions2.X, (int)dimensions2.Y);
				point4..ctor(point3.X + (int)dimensions2.Width, point3.Y + (int)dimensions2.Height);
				rectangle2..ctor(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
			}
			if (num > 0)
			{
				this._text.SetText(this._text.Text.Substring(0, this._text.Text.Length - 1) + "…");
			}
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x005D17B5 File Offset: 0x005CF9B5
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			base.LeftMouseDown(evt);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x005D17BE File Offset: 0x005CF9BE
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x005D17CE File Offset: 0x005CF9CE
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x040056D6 RID: 22230
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x040056D7 RID: 22231
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x040056D8 RID: 22232
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x040056D9 RID: 22233
		private bool _hovered;

		// Token: 0x040056DA RID: 22234
		private bool _soundedHover;

		// Token: 0x040056DB RID: 22235
		private readonly LocalizedText _textToShowWhenEmpty;

		// Token: 0x040056DC RID: 22236
		private string actualContents;

		// Token: 0x040056DD RID: 22237
		private UIText _text;

		// Token: 0x040056DE RID: 22238
		private UIText _title;

		// Token: 0x040056DF RID: 22239
		public readonly LocalizedText Description;

		// Token: 0x040056E0 RID: 22240
		public float DistanceFromTitleToOption = 20f;
	}
}
