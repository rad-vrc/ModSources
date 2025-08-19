using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000521 RID: 1313
	public class UIItemSlot : UIElement
	{
		// Token: 0x06003EE3 RID: 16099 RVA: 0x005D5A5C File Offset: 0x005D3C5C
		public UIItemSlot(Item[] itemArray, int itemIndex, int itemSlotContext)
		{
			this._itemArray = itemArray;
			this._itemIndex = itemIndex;
			this._itemSlotContext = itemSlotContext;
			this.Width = new StyleDimension(48f, 0f);
			this.Height = new StyleDimension(48f, 0f);
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x005D5AB0 File Offset: 0x005D3CB0
		private void HandleItemSlotLogic()
		{
			if (base.IsMouseHovering)
			{
				Main.LocalPlayer.mouseInterface = true;
				Item inv = this._itemArray[this._itemIndex];
				ItemSlot.OverrideHover(ref inv, this._itemSlotContext);
				ItemSlot.LeftClick(ref inv, this._itemSlotContext);
				ItemSlot.RightClick(ref inv, this._itemSlotContext);
				ItemSlot.MouseHover(ref inv, this._itemSlotContext);
				this._itemArray[this._itemIndex] = inv;
			}
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x005D5B20 File Offset: 0x005D3D20
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this.HandleItemSlotLogic();
			Item inv = this._itemArray[this._itemIndex];
			Vector2 position = base.GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;
			ItemSlot.Draw(spriteBatch, ref inv, this._itemSlotContext, position, default(Color));
		}

		// Token: 0x0400576A RID: 22378
		private Item[] _itemArray;

		// Token: 0x0400576B RID: 22379
		private int _itemIndex;

		// Token: 0x0400576C RID: 22380
		private int _itemSlotContext;
	}
}
