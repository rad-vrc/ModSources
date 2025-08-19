using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000520 RID: 1312
	public class UIItemIcon : UIElement
	{
		// Token: 0x06003EE1 RID: 16097 RVA: 0x005D59CB File Offset: 0x005D3BCB
		public UIItemIcon(Item item, bool blackedOut)
		{
			this._item = item;
			this.Width.Set(32f, 0f);
			this.Height.Set(32f, 0f);
			this._blackedOut = blackedOut;
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x005D5A0C File Offset: 0x005D3C0C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Vector2 screenPositionForItemCenter = base.GetDimensions().Center();
			ItemSlot.DrawItemIcon(this._item, 31, spriteBatch, screenPositionForItemCenter, this._item.scale, 32f, this._blackedOut ? Color.Black : Color.White);
		}

		// Token: 0x04005768 RID: 22376
		private Item _item;

		// Token: 0x04005769 RID: 22377
		private bool _blackedOut;
	}
}
