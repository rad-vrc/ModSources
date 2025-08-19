using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000248 RID: 584
	internal class UIHoverImage : UIImage
	{
		// Token: 0x060029A1 RID: 10657 RVA: 0x0051382B File Offset: 0x00511A2B
		public UIHoverImage(Asset<Texture2D> texture, string hoverText) : base(texture)
		{
			this.HoverText = hoverText;
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x0051383C File Offset: 0x00511A3C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			if (base.IsMouseHovering)
			{
				Rectangle bounds = base.Parent.GetDimensions().ToRectangle();
				bounds.Y = 0;
				bounds.Height = Main.screenHeight;
				if (this.UseTooltipMouseText)
				{
					UICommon.TooltipMouseText(this.HoverText);
					return;
				}
				UICommon.DrawHoverStringInBounds(spriteBatch, this.HoverText, new Rectangle?(bounds));
			}
		}

		// Token: 0x04001A6B RID: 6763
		internal string HoverText;

		// Token: 0x04001A6C RID: 6764
		internal bool UseTooltipMouseText;
	}
}
