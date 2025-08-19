using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000376 RID: 886
	public class UICharacterNameButton : UIElement
	{
		// Token: 0x06002874 RID: 10356 RVA: 0x0058AD3C File Offset: 0x00588F3C
		public UICharacterNameButton(LocalizedText titleText, LocalizedText emptyContentText, LocalizedText description = null)
		{
			this.Width = StyleDimension.FromPixels(400f);
			this.Height = StyleDimension.FromPixels(40f);
			this.Description = description;
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", 1);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
			this._textToShowWhenEmpty = emptyContentText;
			float textScale = 1f;
			UIText uitext = new UIText(titleText, textScale, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = StyleDimension.FromPixels(10f)
			};
			base.Append(uitext);
			this._title = uitext;
			UIText uitext2 = new UIText(Language.GetText("UI.PlayerNameSlot"), textScale, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = StyleDimension.FromPixels(150f)
			};
			base.Append(uitext2);
			this._text = uitext2;
			this.SetContents(null);
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x0058AE5C File Offset: 0x0058905C
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

		// Token: 0x06002876 RID: 10358 RVA: 0x0058AF2C File Offset: 0x0058912C
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

		// Token: 0x06002877 RID: 10359 RVA: 0x0058AFB8 File Offset: 0x005891B8
		public void TrimDisplayIfOverElementDimensions(int padding)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Point point = new Point((int)dimensions.X, (int)dimensions.Y);
			Point point2 = new Point(point.X + (int)dimensions.Width, point.Y + (int)dimensions.Height);
			Rectangle rectangle = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
			CalculatedStyle dimensions2 = this._text.GetDimensions();
			Point point3 = new Point((int)dimensions2.X, (int)dimensions2.Y);
			Point point4 = new Point(point3.X + (int)dimensions2.Width, point3.Y + (int)dimensions2.Height);
			Rectangle rectangle2 = new Rectangle(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
			int num = 0;
			while (rectangle2.Right > rectangle.Right - padding)
			{
				this._text.SetText(this._text.Text.Substring(0, this._text.Text.Length - 1));
				num++;
				this.RecalculateChildren();
				dimensions2 = this._text.GetDimensions();
				point3 = new Point((int)dimensions2.X, (int)dimensions2.Y);
				point4 = new Point(point3.X + (int)dimensions2.Width, point3.Y + (int)dimensions2.Height);
				rectangle2 = new Rectangle(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
			}
			if (num > 0)
			{
				this._text.SetText(this._text.Text.Substring(0, this._text.Text.Length - 1) + "…");
			}
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x005885C7 File Offset: 0x005867C7
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			base.LeftMouseDown(evt);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x0058B1C1 File Offset: 0x005893C1
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0058B1D1 File Offset: 0x005893D1
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x04004BA3 RID: 19363
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x04004BA4 RID: 19364
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x04004BA5 RID: 19365
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04004BA6 RID: 19366
		private bool _hovered;

		// Token: 0x04004BA7 RID: 19367
		private bool _soundedHover;

		// Token: 0x04004BA8 RID: 19368
		private readonly LocalizedText _textToShowWhenEmpty;

		// Token: 0x04004BA9 RID: 19369
		private string actualContents;

		// Token: 0x04004BAA RID: 19370
		private UIText _text;

		// Token: 0x04004BAB RID: 19371
		private UIText _title;

		// Token: 0x04004BAC RID: 19372
		public readonly LocalizedText Description;

		// Token: 0x04004BAD RID: 19373
		public float DistanceFromTitleToOption = 20f;
	}
}
