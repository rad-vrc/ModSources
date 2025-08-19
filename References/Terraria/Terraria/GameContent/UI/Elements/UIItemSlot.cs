using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200035A RID: 858
	public class UIItemSlot : UIElement
	{
		// Token: 0x060027AB RID: 10155 RVA: 0x0058519C File Offset: 0x0058339C
		public UIItemSlot(Item[] itemArray, int itemIndex, int itemSlotContext)
		{
			this._itemArray = itemArray;
			this._itemIndex = itemIndex;
			this._itemSlotContext = itemSlotContext;
			this.Width = new StyleDimension(48f, 0f);
			this.Height = new StyleDimension(48f, 0f);
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x005851F0 File Offset: 0x005833F0
		private void HandleItemSlotLogic()
		{
			if (!base.IsMouseHovering)
			{
				return;
			}
			Main.LocalPlayer.mouseInterface = true;
			Item item = this._itemArray[this._itemIndex];
			ItemSlot.OverrideHover(ref item, this._itemSlotContext);
			ItemSlot.LeftClick(ref item, this._itemSlotContext);
			ItemSlot.RightClick(ref item, this._itemSlotContext);
			ItemSlot.MouseHover(ref item, this._itemSlotContext);
			this._itemArray[this._itemIndex] = item;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x00585264 File Offset: 0x00583464
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this.HandleItemSlotLogic();
			Item item = this._itemArray[this._itemIndex];
			Vector2 position = base.GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;
			ItemSlot.Draw(spriteBatch, ref item, this._itemSlotContext, position, default(Color));
		}

		// Token: 0x04004B08 RID: 19208
		private Item[] _itemArray;

		// Token: 0x04004B09 RID: 19209
		private int _itemIndex;

		// Token: 0x04004B0A RID: 19210
		private int _itemSlotContext;
	}
}
