using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003B1 RID: 945
	internal class UIModConfigHoverImageSplit : UIImage
	{
		// Token: 0x06003283 RID: 12931 RVA: 0x00544678 File Offset: 0x00542878
		public UIModConfigHoverImageSplit(Asset<Texture2D> texture, string hoverTextUp, string hoverTextDown) : base(texture)
		{
			this.HoverTextUp = hoverTextUp;
			this.HoverTextDown = hoverTextDown;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x00544690 File Offset: 0x00542890
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			Rectangle r = base.GetDimensions().ToRectangle();
			if (base.IsMouseHovering)
			{
				if (Main.mouseY < r.Y + r.Height / 2)
				{
					UIModConfig.Tooltip = this.HoverTextUp;
					return;
				}
				UIModConfig.Tooltip = this.HoverTextDown;
			}
		}

		// Token: 0x04001DAC RID: 7596
		internal string HoverTextUp;

		// Token: 0x04001DAD RID: 7597
		internal string HoverTextDown;
	}
}
