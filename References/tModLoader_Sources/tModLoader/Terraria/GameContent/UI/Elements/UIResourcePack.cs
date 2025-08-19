using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200052A RID: 1322
	public class UIResourcePack : UIPanel
	{
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x005D7B55 File Offset: 0x005D5D55
		// (set) Token: 0x06003F17 RID: 16151 RVA: 0x005D7B5D File Offset: 0x005D5D5D
		public int Order { get; set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x005D7B66 File Offset: 0x005D5D66
		// (set) Token: 0x06003F19 RID: 16153 RVA: 0x005D7B6E File Offset: 0x005D5D6E
		public UIElement ContentPanel { get; private set; }

		// Token: 0x06003F1A RID: 16154 RVA: 0x005D7B78 File Offset: 0x005D5D78
		public UIResourcePack(ResourcePack pack, int order)
		{
			this.ResourcePack = pack;
			this.Order = order;
			this.BackgroundColor = UIResourcePack.DefaultBackgroundColor;
			this.BorderColor = UIResourcePack.DefaultBorderColor;
			this.Height = StyleDimension.FromPixels(102f);
			this.MinHeight = this.Height;
			this.MaxHeight = this.Height;
			this.MinWidth = StyleDimension.FromPixels(102f);
			this.Width = StyleDimension.FromPercent(1f);
			base.SetPadding(5f);
			this._iconBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders");
			this.OverflowHidden = true;
			this.BuildChildren();
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x005D7C24 File Offset: 0x005D5E24
		private void BuildChildren()
		{
			StyleDimension left = StyleDimension.FromPixels(77f);
			StyleDimension top = StyleDimension.FromPixels(4f);
			UIText uIText = new UIText(this.ResourcePack.Name, 1f, false)
			{
				Left = left,
				Top = top
			};
			base.Append(uIText);
			top.Pixels += uIText.GetOuterDimensions().Height + 6f;
			UIText uIText2 = new UIText(Language.GetTextValue("UI.Author", this.ResourcePack.Author), 0.7f, false)
			{
				Left = left,
				Top = top
			};
			base.Append(uIText2);
			top.Pixels += uIText2.GetOuterDimensions().Height + 10f;
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider");
			UIImage uIImage = new UIImage(asset)
			{
				Left = StyleDimension.FromPixels(72f),
				Top = top,
				Height = StyleDimension.FromPixels((float)asset.Height()),
				Width = StyleDimension.FromPixelsAndPercent(-80f, 1f),
				ScaleToFit = true
			};
			this.Recalculate();
			base.Append(uIImage);
			top.Pixels += uIImage.GetOuterDimensions().Height + 5f;
			UIElement uIElement = new UIElement
			{
				Left = left,
				Top = top,
				Height = StyleDimension.FromPixels(92f - top.Pixels),
				Width = StyleDimension.FromPixelsAndPercent(0f - left.Pixels, 1f)
			};
			base.Append(uIElement);
			this.ContentPanel = uIElement;
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x005D7DC4 File Offset: 0x005D5FC4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			this.DrawIcon(spriteBatch);
			if (this.ResourcePack.Branding == ResourcePack.BrandingType.SteamWorkshop)
			{
				Asset<Texture2D> asset = TextureAssets.Extra[243];
				spriteBatch.Draw(asset.Value, new Vector2(base.GetDimensions().X + base.GetDimensions().Width - (float)asset.Width() - 3f, base.GetDimensions().Y + 2f), new Rectangle?(asset.Frame(1, 1, 0, 0, 0, 0)), Color.White);
			}
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x005D7E58 File Offset: 0x005D6058
		private void DrawIcon(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			spriteBatch.Draw(this.ResourcePack.Icon, new Rectangle((int)innerDimensions.X + 4, (int)innerDimensions.Y + 4 + 10, 64, 64), Color.White);
			spriteBatch.Draw(this._iconBorderTexture.Value, new Rectangle((int)innerDimensions.X, (int)innerDimensions.Y + 10, 72, 72), Color.White);
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x005D7ED0 File Offset: 0x005D60D0
		public override int CompareTo(object obj)
		{
			return this.Order.CompareTo(((UIResourcePack)obj).Order);
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x005D7EF6 File Offset: 0x005D60F6
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = UIResourcePack.HoverBackgroundColor;
			this.BorderColor = UIResourcePack.HoverBorderColor;
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x005D7F15 File Offset: 0x005D6115
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = UIResourcePack.DefaultBackgroundColor;
			this.BorderColor = UIResourcePack.DefaultBorderColor;
		}

		// Token: 0x04005790 RID: 22416
		private const int PANEL_PADDING = 5;

		// Token: 0x04005791 RID: 22417
		private const int ICON_SIZE = 64;

		// Token: 0x04005792 RID: 22418
		private const int ICON_BORDER_PADDING = 4;

		// Token: 0x04005793 RID: 22419
		private const int HEIGHT_FLUFF = 10;

		// Token: 0x04005794 RID: 22420
		private const float HEIGHT = 102f;

		// Token: 0x04005795 RID: 22421
		private const float MIN_WIDTH = 102f;

		// Token: 0x04005796 RID: 22422
		private static readonly Color DefaultBackgroundColor = new Color(26, 40, 89) * 0.8f;

		// Token: 0x04005797 RID: 22423
		private static readonly Color DefaultBorderColor = new Color(13, 20, 44) * 0.8f;

		// Token: 0x04005798 RID: 22424
		private static readonly Color HoverBackgroundColor = new Color(46, 60, 119);

		// Token: 0x04005799 RID: 22425
		private static readonly Color HoverBorderColor = new Color(20, 30, 56);

		// Token: 0x0400579A RID: 22426
		public readonly ResourcePack ResourcePack;

		// Token: 0x0400579B RID: 22427
		private readonly Asset<Texture2D> _iconBorderTexture;
	}
}
