using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000359 RID: 857
	public class UIDynamicItemCollection : UIElement
	{
		// Token: 0x060027A1 RID: 10145 RVA: 0x00584CAC File Offset: 0x00582EAC
		public UIDynamicItemCollection()
		{
			this.Width = new StyleDimension(0f, 1f);
			this.HAlign = 0.5f;
			this.UpdateSize();
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x00584D14 File Offset: 0x00582F14
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Main.inventoryScale = 0.84615386f;
			int startX;
			int startY;
			int num;
			int num2;
			this.GetGridParameters(out startX, out startY, out num, out num2);
			int num3 = this._itemsPerLine;
			for (int i = num; i < num2; i++)
			{
				int num4 = this._itemIdsAvailableToShow[i];
				Rectangle itemSlotHitbox = this.GetItemSlotHitbox(startX, startY, num, i);
				Item item = ContentSamples.ItemsByType[num4];
				int context = 29;
				if (TextureAssets.Item[num4].State == null)
				{
					num3--;
				}
				bool creative_ItemSlotShouldHighlightAsSelected = false;
				if (base.IsMouseHovering && itemSlotHitbox.Contains(Main.MouseScreen.ToPoint()) && !PlayerInput.IgnoreMouseInterface)
				{
					this._item.SetDefaults(item.type);
					item = this._item;
					Main.LocalPlayer.mouseInterface = true;
					ItemSlot.OverrideHover(ref item, context);
					ItemSlot.LeftClick(ref item, context);
					ItemSlot.RightClick(ref item, context);
					ItemSlot.MouseHover(ref item, context);
					creative_ItemSlotShouldHighlightAsSelected = true;
				}
				UILinkPointNavigator.Shortcuts.CREATIVE_ItemSlotShouldHighlightAsSelected = creative_ItemSlotShouldHighlightAsSelected;
				ItemSlot.Draw(spriteBatch, ref item, context, itemSlotHitbox.TopLeft(), default(Color));
				if (num3 <= 0)
				{
					IL_14E:
					while (this._itemIdsToLoadTexturesFor.Count > 0 && num3 > 0)
					{
						int num5 = this._itemIdsToLoadTexturesFor[0];
						this._itemIdsToLoadTexturesFor.RemoveAt(0);
						if (TextureAssets.Item[num5].State == null)
						{
							Main.instance.LoadItem(num5);
							num3 -= 4;
						}
					}
					return;
				}
			}
			goto IL_14E;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x00584E84 File Offset: 0x00583084
		private Rectangle GetItemSlotHitbox(int startX, int startY, int startItemIndex, int i)
		{
			int num = i - startItemIndex;
			int num2 = num % this._itemsPerLine;
			int num3 = num / this._itemsPerLine;
			return new Rectangle(startX + num2 * 44, startY + num3 * 44, 44, 44);
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x00584EBC File Offset: 0x005830BC
		private void GetGridParameters(out int startX, out int startY, out int startItemIndex, out int endItemIndex)
		{
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			Rectangle viewCullingArea = base.Parent.GetViewCullingArea();
			int x = rectangle.Center.X;
			startX = x - (int)((float)(44 * this._itemsPerLine) * 0.5f);
			startY = rectangle.Top;
			startItemIndex = 0;
			endItemIndex = this._itemIdsAvailableToShow.Count;
			int num = (Math.Min(viewCullingArea.Top, rectangle.Top) - viewCullingArea.Top) / 44;
			startY += -num * 44;
			startItemIndex += -num * this._itemsPerLine;
			int num2 = (int)Math.Ceiling((double)((float)viewCullingArea.Height / 44f)) * this._itemsPerLine;
			if (endItemIndex > num2 + startItemIndex + this._itemsPerLine)
			{
				endItemIndex = num2 + startItemIndex + this._itemsPerLine;
			}
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x00584F95 File Offset: 0x00583195
		public override void Recalculate()
		{
			base.Recalculate();
			this.UpdateSize();
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x00584FA3 File Offset: 0x005831A3
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (base.IsMouseHovering)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x00584FBF File Offset: 0x005831BF
		public void SetContentsToShow(List<int> itemIdsToShow)
		{
			this._itemIdsAvailableToShow.Clear();
			this._itemIdsToLoadTexturesFor.Clear();
			this._itemIdsAvailableToShow.AddRange(itemIdsToShow);
			this._itemIdsToLoadTexturesFor.AddRange(itemIdsToShow);
			this.UpdateSize();
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x00584FF5 File Offset: 0x005831F5
		public int GetItemsPerLine()
		{
			return this._itemsPerLine;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x00585000 File Offset: 0x00583200
		public override List<SnapPoint> GetSnapPoints()
		{
			List<SnapPoint> list = new List<SnapPoint>();
			int startX;
			int startY;
			int num;
			int num2;
			this.GetGridParameters(out startX, out startY, out num, out num2);
			int itemsPerLine = this._itemsPerLine;
			Rectangle viewCullingArea = base.Parent.GetViewCullingArea();
			int num3 = num2 - num;
			while (this._dummySnapPoints.Count < num3)
			{
				this._dummySnapPoints.Add(new SnapPoint("CreativeInfinitesSlot", 0, Vector2.Zero, Vector2.Zero));
			}
			int num4 = 0;
			Vector2 value = base.GetDimensions().Position();
			for (int i = num; i < num2; i++)
			{
				Point center = this.GetItemSlotHitbox(startX, startY, num, i).Center;
				if (viewCullingArea.Contains(center))
				{
					SnapPoint snapPoint = this._dummySnapPoints[num4];
					snapPoint.ThisIsAHackThatChangesTheSnapPointsInfo(Vector2.Zero, center.ToVector2() - value, num4);
					snapPoint.Calculate(this);
					num4++;
					list.Add(snapPoint);
				}
			}
			foreach (UIElement uielement in this.Elements)
			{
				list.AddRange(uielement.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x00585144 File Offset: 0x00583344
		public void UpdateSize()
		{
			int num = base.GetDimensions().ToRectangle().Width / 44;
			this._itemsPerLine = num;
			int num2 = (int)Math.Ceiling((double)((float)this._itemIdsAvailableToShow.Count / (float)num));
			this.MinHeight.Set((float)(44 * num2), 0f);
		}

		// Token: 0x04004B01 RID: 19201
		private List<int> _itemIdsAvailableToShow = new List<int>();

		// Token: 0x04004B02 RID: 19202
		private List<int> _itemIdsToLoadTexturesFor = new List<int>();

		// Token: 0x04004B03 RID: 19203
		private int _itemsPerLine;

		// Token: 0x04004B04 RID: 19204
		private const int sizePerEntryX = 44;

		// Token: 0x04004B05 RID: 19205
		private const int sizePerEntryY = 44;

		// Token: 0x04004B06 RID: 19206
		private List<SnapPoint> _dummySnapPoints = new List<SnapPoint>();

		// Token: 0x04004B07 RID: 19207
		private Item _item = new Item();
	}
}
