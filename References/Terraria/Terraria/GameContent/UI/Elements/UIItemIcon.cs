using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200036B RID: 875
	public class UIItemIcon : UIElement
	{
		// Token: 0x06002818 RID: 10264 RVA: 0x00587F63 File Offset: 0x00586163
		public UIItemIcon(Item item, bool blackedOut)
		{
			this._item = item;
			this.Width.Set(32f, 0f);
			this.Height.Set(32f, 0f);
			this._blackedOut = blackedOut;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00587FA4 File Offset: 0x005861A4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			ItemSlot.DrawItemIcon(this._item, 31, spriteBatch, dimensions.Center(), this._item.scale, 32f, this._blackedOut ? Color.Black : Color.White);
		}

		// Token: 0x04004B41 RID: 19265
		private Item _item;

		// Token: 0x04004B42 RID: 19266
		private bool _blackedOut;
	}
}
