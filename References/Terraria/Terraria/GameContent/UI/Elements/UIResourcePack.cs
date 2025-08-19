using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000384 RID: 900
	public class UIResourcePack : UIPanel
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x0058DAC1 File Offset: 0x0058BCC1
		// (set) Token: 0x060028B6 RID: 10422 RVA: 0x0058DAC9 File Offset: 0x0058BCC9
		public int Order { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060028B7 RID: 10423 RVA: 0x0058DAD2 File Offset: 0x0058BCD2
		// (set) Token: 0x060028B8 RID: 10424 RVA: 0x0058DADA File Offset: 0x0058BCDA
		public UIElement ContentPanel { get; private set; }

		// Token: 0x060028B9 RID: 10425 RVA: 0x0058DAE4 File Offset: 0x0058BCE4
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
			this._iconBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 1);
			this.OverflowHidden = true;
			this.BuildChildren();
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x0058DB94 File Offset: 0x0058BD94
		private void BuildChildren()
		{
			StyleDimension styleDimension = StyleDimension.FromPixels(77f);
			StyleDimension styleDimension2 = StyleDimension.FromPixels(4f);
			UIText uitext = new UIText(this.ResourcePack.Name, 1f, false)
			{
				Left = styleDimension,
				Top = styleDimension2
			};
			base.Append(uitext);
			styleDimension2.Pixels += uitext.GetOuterDimensions().Height + 6f;
			UIText uitext2 = new UIText(Language.GetTextValue("UI.Author", this.ResourcePack.Author), 0.7f, false)
			{
				Left = styleDimension,
				Top = styleDimension2
			};
			base.Append(uitext2);
			styleDimension2.Pixels += uitext2.GetOuterDimensions().Height + 10f;
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			UIImage uiimage = new UIImage(asset)
			{
				Left = StyleDimension.FromPixels(72f),
				Top = styleDimension2,
				Height = StyleDimension.FromPixels((float)asset.Height()),
				Width = StyleDimension.FromPixelsAndPercent(-80f, 1f),
				ScaleToFit = true
			};
			this.Recalculate();
			base.Append(uiimage);
			styleDimension2.Pixels += uiimage.GetOuterDimensions().Height + 5f;
			UIElement uielement = new UIElement
			{
				Left = styleDimension,
				Top = styleDimension2,
				Height = StyleDimension.FromPixels(92f - styleDimension2.Pixels),
				Width = StyleDimension.FromPixelsAndPercent(-styleDimension.Pixels, 1f)
			};
			base.Append(uielement);
			this.ContentPanel = uielement;
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x0058DD30 File Offset: 0x0058BF30
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

		// Token: 0x060028BC RID: 10428 RVA: 0x0058DDC4 File Offset: 0x0058BFC4
		private void DrawIcon(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			spriteBatch.Draw(this.ResourcePack.Icon, new Rectangle((int)innerDimensions.X + 4, (int)innerDimensions.Y + 4 + 10, 64, 64), Color.White);
			spriteBatch.Draw(this._iconBorderTexture.Value, new Rectangle((int)innerDimensions.X, (int)innerDimensions.Y + 10, 72, 72), Color.White);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0058DE3C File Offset: 0x0058C03C
		public override int CompareTo(object obj)
		{
			return this.Order.CompareTo(((UIResourcePack)obj).Order);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x0058DE62 File Offset: 0x0058C062
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = UIResourcePack.HoverBackgroundColor;
			this.BorderColor = UIResourcePack.HoverBorderColor;
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x0058DE81 File Offset: 0x0058C081
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = UIResourcePack.DefaultBackgroundColor;
			this.BorderColor = UIResourcePack.DefaultBorderColor;
		}

		// Token: 0x04004BFB RID: 19451
		private const int PANEL_PADDING = 5;

		// Token: 0x04004BFC RID: 19452
		private const int ICON_SIZE = 64;

		// Token: 0x04004BFD RID: 19453
		private const int ICON_BORDER_PADDING = 4;

		// Token: 0x04004BFE RID: 19454
		private const int HEIGHT_FLUFF = 10;

		// Token: 0x04004BFF RID: 19455
		private const float HEIGHT = 102f;

		// Token: 0x04004C00 RID: 19456
		private const float MIN_WIDTH = 102f;

		// Token: 0x04004C01 RID: 19457
		private static readonly Color DefaultBackgroundColor = new Color(26, 40, 89) * 0.8f;

		// Token: 0x04004C02 RID: 19458
		private static readonly Color DefaultBorderColor = new Color(13, 20, 44) * 0.8f;

		// Token: 0x04004C03 RID: 19459
		private static readonly Color HoverBackgroundColor = new Color(46, 60, 119);

		// Token: 0x04004C04 RID: 19460
		private static readonly Color HoverBorderColor = new Color(20, 30, 56);

		// Token: 0x04004C05 RID: 19461
		public readonly ResourcePack ResourcePack;

		// Token: 0x04004C07 RID: 19463
		private readonly Asset<Texture2D> _iconBorderTexture;
	}
}
