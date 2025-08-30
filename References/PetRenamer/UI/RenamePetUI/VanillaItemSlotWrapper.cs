using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

namespace PetRenamer.UI.RenamePetUI
{
	// Token: 0x0200000C RID: 12
	internal class VanillaItemSlotWrapper : UIElement
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000087 RID: 135 RVA: 0x00004044 File Offset: 0x00002244
		// (remove) Token: 0x06000088 RID: 136 RVA: 0x0000407C File Offset: 0x0000227C
		internal event Action<int> OnEmptyMouseover;

		// Token: 0x06000089 RID: 137 RVA: 0x000040B4 File Offset: 0x000022B4
		internal VanillaItemSlotWrapper(int context = 4, float scale = 1f)
		{
			this._context = context;
			this._scale = scale;
			this.Item = new Item();
			this.Item.SetDefaults(0);
			Asset<Texture2D> inventoryBack9 = TextureAssets.InventoryBack9;
			this.Width.Set((float)inventoryBack9.Width() * scale, 0f);
			this.Height.Set((float)inventoryBack9.Height() * scale, 0f);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004124 File Offset: 0x00002324
		internal bool Valid(Item item)
		{
			return this.ValidItemFunc(item);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004132 File Offset: 0x00002332
		internal void HandleMouseItem()
		{
			if (this.ValidItemFunc == null || this.Valid(Main.mouseItem))
			{
				ItemSlot.Handle(ref this.Item, this._context);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000415C File Offset: 0x0000235C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float inventoryScale = Main.inventoryScale;
			Main.inventoryScale = this._scale;
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			bool contains = this.ContainsPoint(Main.MouseScreen);
			if (contains && !PlayerInput.IgnoreMouseInterface)
			{
				Main.LocalPlayer.mouseInterface = true;
				this.HandleMouseItem();
			}
			ItemSlot.Draw(spriteBatch, ref this.Item, this._context, rectangle.TopLeft(), default(Color));
			if (contains && this.Item.IsAir)
			{
				this.timer++;
				Action<int> onEmptyMouseover = this.OnEmptyMouseover;
				if (onEmptyMouseover != null)
				{
					onEmptyMouseover(this.timer);
				}
			}
			else if (!contains)
			{
				this.timer = 0;
			}
			Main.inventoryScale = inventoryScale;
		}

		// Token: 0x0400004A RID: 74
		internal Item Item;

		// Token: 0x0400004B RID: 75
		private readonly int _context;

		// Token: 0x0400004C RID: 76
		private readonly float _scale;

		// Token: 0x0400004D RID: 77
		internal Func<Item, bool> ValidItemFunc;

		// Token: 0x0400004F RID: 79
		private int timer;
	}
}
