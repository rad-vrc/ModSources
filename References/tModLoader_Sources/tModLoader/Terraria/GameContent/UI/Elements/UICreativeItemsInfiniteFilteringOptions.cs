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
	// Token: 0x02000513 RID: 1299
	public class UICreativeItemsInfiniteFilteringOptions : UIElement
	{
		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06003E95 RID: 16021 RVA: 0x005D3A20 File Offset: 0x005D1C20
		// (remove) Token: 0x06003E96 RID: 16022 RVA: 0x005D3A58 File Offset: 0x005D1C58
		public event Action OnClickingOption;

		// Token: 0x06003E97 RID: 16023 RVA: 0x005D3A90 File Offset: 0x005D1C90
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
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Tabs_B");
			for (int i = 0; i < this._filterer.AvailableFilters.Count; i++)
			{
				IItemEntryFilter itemEntryFilter = this._filterer.AvailableFilters[i];
				asset.Frame(2, 4, 0, 0, 0, 0).OffsetSize(-2, -2);
				UIImageFramed uIImageFramed = new UIImageFramed(asset, asset.Frame(2, 4, 0, 0, 0, 0).OffsetSize(-2, -2));
				uIImageFramed.Left.Set((float)(num * i), 0f);
				uIImageFramed.OnLeftClick += this.singleFilterButtonClick;
				uIImageFramed.OnMouseOver += this.button_OnMouseOver;
				uIImageFramed.SetPadding(0f);
				uIImageFramed.SetSnapPoint(snapPointsName, i, null, null);
				this.AddOnHover(itemEntryFilter, uIImageFramed, i);
				UIElement image = itemEntryFilter.GetImage();
				image.IgnoresMouseInteraction = true;
				image.Left = new StyleDimension(6f, 0f);
				image.HAlign = 0f;
				uIImageFramed.Append(image);
				this._filtersByButtons[uIImageFramed] = itemEntryFilter;
				this._iconsByButtons[uIImageFramed] = image;
				base.Append(uIImageFramed);
				this.UpdateVisuals(uIImageFramed, i);
			}
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x005D3C76 File Offset: 0x005D1E76
		private void button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x005D3C90 File Offset: 0x005D1E90
		private void singleFilterButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			UIImageFramed uIImageFramed = evt.Target as UIImageFramed;
			IItemEntryFilter value;
			if (uIImageFramed == null || !this._filtersByButtons.TryGetValue(uIImageFramed, out value))
			{
				return;
			}
			int num = this._filterer.AvailableFilters.IndexOf(value);
			if (num != -1)
			{
				if (!this._filterer.ActiveFilters.Contains(value))
				{
					this._filterer.ActiveFilters.Clear();
				}
				this._filterer.ToggleFilter(num);
				this.UpdateVisuals(uIImageFramed, num);
				if (this.OnClickingOption != null)
				{
					this.OnClickingOption();
				}
			}
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x005D3D20 File Offset: 0x005D1F20
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

		// Token: 0x06003E9B RID: 16027 RVA: 0x005D3DA0 File Offset: 0x005D1FA0
		private void AddOnHover(IItemEntryFilter filter, UIElement button, int indexOfFilter)
		{
			button.OnUpdate += delegate(UIElement element)
			{
				this.ShowButtonName(element, filter, indexOfFilter);
			};
			button.OnUpdate += delegate(UIElement <p0>)
			{
				this.UpdateVisuals(button as UIImageFramed, indexOfFilter);
			};
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x005D3E00 File Offset: 0x005D2000
		private void ShowButtonName(UIElement element, IItemEntryFilter number, int indexOfFilter)
		{
			if (element.IsMouseHovering)
			{
				string textValue = Language.GetTextValue(number.GetDisplayNameKey());
				Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x04005722 RID: 22306
		private EntryFilterer<Item, IItemEntryFilter> _filterer;

		// Token: 0x04005723 RID: 22307
		private Dictionary<UIImageFramed, IItemEntryFilter> _filtersByButtons = new Dictionary<UIImageFramed, IItemEntryFilter>();

		// Token: 0x04005724 RID: 22308
		private Dictionary<UIImageFramed, UIElement> _iconsByButtons = new Dictionary<UIImageFramed, UIElement>();

		// Token: 0x04005725 RID: 22309
		private const int barFramesX = 2;

		// Token: 0x04005726 RID: 22310
		private const int barFramesY = 4;
	}
}
