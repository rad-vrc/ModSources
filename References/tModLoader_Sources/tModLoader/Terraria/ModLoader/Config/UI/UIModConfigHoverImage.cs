using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003B0 RID: 944
	internal class UIModConfigHoverImage : UIImage
	{
		// Token: 0x06003281 RID: 12929 RVA: 0x0054464C File Offset: 0x0054284C
		public UIModConfigHoverImage(Asset<Texture2D> texture, string hoverText) : base(texture)
		{
			this.HoverText = hoverText;
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x0054465C File Offset: 0x0054285C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			if (base.IsMouseHovering)
			{
				UIModConfig.Tooltip = this.HoverText;
			}
		}

		// Token: 0x04001DAB RID: 7595
		internal string HoverText;
	}
}
