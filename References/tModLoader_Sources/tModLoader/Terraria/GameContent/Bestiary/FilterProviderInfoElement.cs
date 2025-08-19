using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000681 RID: 1665
	public class FilterProviderInfoElement : IFilterInfoProvider, IProvideSearchFilterString, IBestiaryInfoElement
	{
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060047D5 RID: 18389 RVA: 0x0064684C File Offset: 0x00644A4C
		// (set) Token: 0x060047D6 RID: 18390 RVA: 0x00646854 File Offset: 0x00644A54
		public int DisplayTextPriority { get; set; }

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060047D7 RID: 18391 RVA: 0x0064685D File Offset: 0x00644A5D
		// (set) Token: 0x060047D8 RID: 18392 RVA: 0x00646865 File Offset: 0x00644A65
		public bool HideInPortraitInfo { get; set; }

		// Token: 0x060047D9 RID: 18393 RVA: 0x0064686E File Offset: 0x00644A6E
		public FilterProviderInfoElement(string nameLanguageKey, int filterIconFrame)
		{
			this._key = nameLanguageKey;
			this._filterIconFrame.X = filterIconFrame % 16;
			this._filterIconFrame.Y = filterIconFrame / 16;
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x0064689C File Offset: 0x00644A9C
		public UIElement GetFilterImage()
		{
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow");
			return new UIImageFramed(asset, asset.Frame(16, 5, this._filterIconFrame.X, this._filterIconFrame.Y, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x006468F4 File Offset: 0x00644AF4
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return Language.GetText(this._key).Value;
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x00646910 File Offset: 0x00644B10
		public string GetDisplayNameKey()
		{
			return this._key;
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x00646918 File Offset: 0x00644B18
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (this.HideInPortraitInfo)
			{
				return null;
			}
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			UIElement uIElement = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel"), null, 12, 7)
			{
				Width = new StyleDimension(-14f, 1f),
				Height = new StyleDimension(34f, 0f),
				BackgroundColor = new Color(43, 56, 101),
				BorderColor = Color.Transparent,
				Left = new StyleDimension(5f, 0f)
			};
			uIElement.SetPadding(0f);
			uIElement.PaddingRight = 5f;
			UIElement filterImage = this.GetFilterImage();
			filterImage.HAlign = 0f;
			filterImage.Left = new StyleDimension(5f, 0f);
			UIText element = new UIText(Language.GetText(this.GetDisplayNameKey()), 0.8f, false)
			{
				HAlign = 0f,
				Left = new StyleDimension(38f, 0f),
				TextOriginX = 0f,
				TextOriginY = 0f,
				VAlign = 0.5f,
				DynamicallyScaleDownToWidth = true
			};
			if (filterImage != null)
			{
				uIElement.Append(filterImage);
			}
			uIElement.Append(element);
			this.AddOnHover(uIElement);
			return uIElement;
		}

		// Token: 0x060047DE RID: 18398 RVA: 0x00646A62 File Offset: 0x00644C62
		private void AddOnHover(UIElement button)
		{
			button.OnUpdate += delegate(UIElement e)
			{
				this.ShowButtonName(e);
			};
		}

		// Token: 0x060047DF RID: 18399 RVA: 0x00646A78 File Offset: 0x00644C78
		private void ShowButtonName(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				string textValue = Language.GetTextValue(this.GetDisplayNameKey());
				Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x04005C02 RID: 23554
		private const int framesPerRow = 16;

		// Token: 0x04005C03 RID: 23555
		private const int framesPerColumn = 5;

		// Token: 0x04005C04 RID: 23556
		private Point _filterIconFrame;

		// Token: 0x04005C05 RID: 23557
		private string _key;
	}
}
