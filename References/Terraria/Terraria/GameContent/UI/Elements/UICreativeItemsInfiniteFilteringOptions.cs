using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000363 RID: 867
	public class UICreativeItemsInfiniteFilteringOptions : UIElement
	{
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060027E4 RID: 10212 RVA: 0x00586C0C File Offset: 0x00584E0C
		// (remove) Token: 0x060027E5 RID: 10213 RVA: 0x00586C44 File Offset: 0x00584E44
		public event Action OnClickingOption;

		// Token: 0x060027E6 RID: 10214 RVA: 0x00586C7C File Offset: 0x00584E7C
		public UICreativeItemsInfiniteFilteringOptions(EntryFilterer<Item, IItemEntryFilter> filterer, string snapPointsName)
		{
			this._filterer = filterer;
			int num = 40;
			int count = this._filterer.AvailableFilters.Count;
			int num2 = num * count;
			this.Height = new StyleDimension((float)num, 0f);
			this.Width = new StyleDimension((float)num2, 0f);
			this.Top = new StyleDimension(4f, 0f);
			base.SetPadding(0f);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Tabs_B", 1);
			for (int i = 0; i < this._filterer.AvailableFilters.Count; i++)
			{
				IItemEntryFilter itemEntryFilter = this._filterer.AvailableFilters[i];
				asset.Frame(2, 4, 0, 0, 0, 0).OffsetSize(-2, -2);
				UIImageFramed uiimageFramed = new UIImageFramed(asset, asset.Frame(2, 4, 0, 0, 0, 0).OffsetSize(-2, -2));
				uiimageFramed.Left.Set((float)(num * i), 0f);
				uiimageFramed.OnLeftClick += this.singleFilterButtonClick;
				uiimageFramed.OnMouseOver += this.button_OnMouseOver;
				uiimageFramed.SetPadding(0f);
				uiimageFramed.SetSnapPoint(snapPointsName, i, null, null);
				this.AddOnHover(itemEntryFilter, uiimageFramed, i);
				UIElement image = itemEntryFilter.GetImage();
				image.IgnoresMouseInteraction = true;
				image.Left = new StyleDimension(6f, 0f);
				image.HAlign = 0f;
				uiimageFramed.Append(image);
				this._filtersByButtons[uiimageFramed] = itemEntryFilter;
				this._iconsByButtons[uiimageFramed] = image;
				base.Append(uiimageFramed);
				this.UpdateVisuals(uiimageFramed, i);
			}
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x00570D36 File Offset: 0x0056EF36
		private void button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x00586E64 File Offset: 0x00585064
		private void singleFilterButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			UIImageFramed uiimageFramed = evt.Target as UIImageFramed;
			if (uiimageFramed == null)
			{
				return;
			}
			IItemEntryFilter item;
			if (!this._filtersByButtons.TryGetValue(uiimageFramed, out item))
			{
				return;
			}
			int num = this._filterer.AvailableFilters.IndexOf(item);
			if (num == -1)
			{
				return;
			}
			if (!this._filterer.ActiveFilters.Contains(item))
			{
				this._filterer.ActiveFilters.Clear();
			}
			this._filterer.ToggleFilter(num);
			this.UpdateVisuals(uiimageFramed, num);
			if (this.OnClickingOption != null)
			{
				this.OnClickingOption();
			}
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x00586EF4 File Offset: 0x005850F4
		private void UpdateVisuals(UIImageFramed button, int indexOfFilter)
		{
			bool flag = this._filterer.IsFilterActive(indexOfFilter);
			bool isMouseHovering = button.IsMouseHovering;
			int frameX = flag.ToInt();
			int frameY = flag.ToInt() * 2 + isMouseHovering.ToInt();
			button.SetFrame(2, 4, frameX, frameY, -2, -2);
			IColorable colorable = this._iconsByButtons[button] as IColorable;
			if (colorable != null)
			{
				colorable.Color = (flag ? Color.White : (Color.White * 0.5f));
			}
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x00586F74 File Offset: 0x00585174
		private void AddOnHover(IItemEntryFilter filter, UIElement button, int indexOfFilter)
		{
			button.OnUpdate += delegate(UIElement element)
			{
				this.ShowButtonName(element, filter, indexOfFilter);
			};
			button.OnUpdate += delegate(UIElement element)
			{
				this.UpdateVisuals(button as UIImageFramed, indexOfFilter);
			};
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x00586FD4 File Offset: 0x005851D4
		private void ShowButtonName(UIElement element, IItemEntryFilter number, int indexOfFilter)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			string textValue = Language.GetTextValue(number.GetDisplayNameKey());
			Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x04004B29 RID: 19241
		private EntryFilterer<Item, IItemEntryFilter> _filterer;

		// Token: 0x04004B2A RID: 19242
		private Dictionary<UIImageFramed, IItemEntryFilter> _filtersByButtons = new Dictionary<UIImageFramed, IItemEntryFilter>();

		// Token: 0x04004B2B RID: 19243
		private Dictionary<UIImageFramed, UIElement> _iconsByButtons = new Dictionary<UIImageFramed, UIElement>();

		// Token: 0x04004B2D RID: 19245
		private const int barFramesX = 2;

		// Token: 0x04004B2E RID: 19246
		private const int barFramesY = 4;
	}
}
