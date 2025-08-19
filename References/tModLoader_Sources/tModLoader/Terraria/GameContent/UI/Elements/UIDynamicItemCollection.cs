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
	// Token: 0x02000516 RID: 1302
	public class UIDynamicItemCollection : UIElement
	{
		// Token: 0x06003EA3 RID: 16035 RVA: 0x005D40C4 File Offset: 0x005D22C4
		public UIDynamicItemCollection()
		{
			this.Width = new StyleDimension(0f, 1f);
			this.HAlign = 0.5f;
			this.UpdateSize();
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x005D412C File Offset: 0x005D232C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Main.inventoryScale = 0.84615386f;
			int startX;
			int startY;
			int startItemIndex;
			int endItemIndex;
			this.GetGridParameters(out startX, out startY, out startItemIndex, out endItemIndex);
			int num = this._itemsPerLine;
			for (int i = startItemIndex; i < endItemIndex; i++)
			{
				int num2 = this._itemIdsAvailableToShow[i];
				Rectangle itemSlotHitbox = this.GetItemSlotHitbox(startX, startY, startItemIndex, i);
				Item inv = ContentSamples.ItemsByType[num2];
				int context = 29;
				if (TextureAssets.Item[num2].State == null)
				{
					num--;
				}
				bool cREATIVE_ItemSlotShouldHighlightAsSelected = false;
				if (base.IsMouseHovering && itemSlotHitbox.Contains(Main.MouseScreen.ToPoint()) && !PlayerInput.IgnoreMouseInterface)
				{
					this._item.SetDefaults(inv.type);
					inv = this._item;
					Main.LocalPlayer.mouseInterface = true;
					ItemSlot.OverrideHover(ref inv, context);
					ItemSlot.LeftClick(ref inv, context);
					ItemSlot.RightClick(ref inv, context);
					ItemSlot.MouseHover(ref inv, context);
					cREATIVE_ItemSlotShouldHighlightAsSelected = true;
				}
				UILinkPointNavigator.Shortcuts.CREATIVE_ItemSlotShouldHighlightAsSelected = cREATIVE_ItemSlotShouldHighlightAsSelected;
				ItemSlot.Draw(spriteBatch, ref inv, context, itemSlotHitbox.TopLeft(), default(Color));
				if (num <= 0)
				{
					IL_14E:
					while (this._itemIdsToLoadTexturesFor.Count > 0 && num > 0)
					{
						int num3 = this._itemIdsToLoadTexturesFor[0];
						this._itemIdsToLoadTexturesFor.RemoveAt(0);
						if (TextureAssets.Item[num3].State == null)
						{
							Main.instance.LoadItem(num3);
							num -= 4;
						}
					}
					return;
				}
			}
			goto IL_14E;
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x005D429C File Offset: 0x005D249C
		private Rectangle GetItemSlotHitbox(int startX, int startY, int startItemIndex, int i)
		{
			int num4 = i - startItemIndex;
			int num2 = num4 % this._itemsPerLine;
			int num3 = num4 / this._itemsPerLine;
			return new Rectangle(startX + num2 * 44, startY + num3 * 44, 44, 44);
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x005D42D4 File Offset: 0x005D24D4
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

		// Token: 0x06003EA7 RID: 16039 RVA: 0x005D43AD File Offset: 0x005D25AD
		public override void Recalculate()
		{
			base.Recalculate();
			this.UpdateSize();
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x005D43BB File Offset: 0x005D25BB
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (base.IsMouseHovering)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x005D43D7 File Offset: 0x005D25D7
		public void SetContentsToShow(List<int> itemIdsToShow)
		{
			this._itemIdsAvailableToShow.Clear();
			this._itemIdsToLoadTexturesFor.Clear();
			this._itemIdsAvailableToShow.AddRange(itemIdsToShow);
			this._itemIdsToLoadTexturesFor.AddRange(itemIdsToShow);
			this.UpdateSize();
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x005D440D File Offset: 0x005D260D
		public int GetItemsPerLine()
		{
			return this._itemsPerLine;
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x005D4418 File Offset: 0x005D2618
		public override List<SnapPoint> GetSnapPoints()
		{
			List<SnapPoint> list = new List<SnapPoint>();
			int startX;
			int startY;
			int startItemIndex;
			int endItemIndex;
			this.GetGridParameters(out startX, out startY, out startItemIndex, out endItemIndex);
			int itemsPerLine = this._itemsPerLine;
			Rectangle viewCullingArea = base.Parent.GetViewCullingArea();
			int num = endItemIndex - startItemIndex;
			while (this._dummySnapPoints.Count < num)
			{
				this._dummySnapPoints.Add(new SnapPoint("CreativeInfinitesSlot", 0, Vector2.Zero, Vector2.Zero));
			}
			int num2 = 0;
			Vector2 vector = base.GetDimensions().Position();
			for (int i = startItemIndex; i < endItemIndex; i++)
			{
				Point center = this.GetItemSlotHitbox(startX, startY, startItemIndex, i).Center;
				if (viewCullingArea.Contains(center))
				{
					SnapPoint snapPoint = this._dummySnapPoints[num2];
					snapPoint.ThisIsAHackThatChangesTheSnapPointsInfo(Vector2.Zero, center.ToVector2() - vector, num2);
					snapPoint.Calculate(this);
					num2++;
					list.Add(snapPoint);
				}
			}
			foreach (UIElement element in this.Elements)
			{
				list.AddRange(element.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x005D455C File Offset: 0x005D275C
		public void UpdateSize()
		{
			int num = this._itemsPerLine = base.GetDimensions().ToRectangle().Width / 44;
			int num2 = (int)Math.Ceiling((double)((float)this._itemIdsAvailableToShow.Count / (float)num));
			this.MinHeight.Set((float)(44 * num2), 0f);
		}

		// Token: 0x04005730 RID: 22320
		private List<int> _itemIdsAvailableToShow = new List<int>();

		// Token: 0x04005731 RID: 22321
		private List<int> _itemIdsToLoadTexturesFor = new List<int>();

		// Token: 0x04005732 RID: 22322
		private int _itemsPerLine;

		// Token: 0x04005733 RID: 22323
		private const int sizePerEntryX = 44;

		// Token: 0x04005734 RID: 22324
		private const int sizePerEntryY = 44;

		// Token: 0x04005735 RID: 22325
		private List<SnapPoint> _dummySnapPoints = new List<SnapPoint>();

		// Token: 0x04005736 RID: 22326
		private Item _item = new Item();
	}
}
