using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200030B RID: 779
	public class FilterProviderInfoElement : IFilterInfoProvider, IProvideSearchFilterString, IBestiaryInfoElement
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x005585DD File Offset: 0x005567DD
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x005585E5 File Offset: 0x005567E5
		public int DisplayTextPriority { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x005585EE File Offset: 0x005567EE
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x005585F6 File Offset: 0x005567F6
		public bool HideInPortraitInfo { get; set; }

		// Token: 0x060023F1 RID: 9201 RVA: 0x005585FF File Offset: 0x005567FF
		public FilterProviderInfoElement(string nameLanguageKey, int filterIconFrame)
		{
			this._key = nameLanguageKey;
			this._filterIconFrame.X = filterIconFrame % 16;
			this._filterIconFrame.Y = filterIconFrame / 16;
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x0055862C File Offset: 0x0055682C
		public UIElement GetFilterImage()
		{
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow", 1);
			return new UIImageFramed(asset, asset.Frame(16, 5, this._filterIconFrame.X, this._filterIconFrame.Y, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00558685 File Offset: 0x00556885
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return Language.GetText(this._key).Value;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x005586A1 File Offset: 0x005568A1
		public string GetDisplayNameKey()
		{
			return this._key;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x005586AC File Offset: 0x005568AC
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
			UIElement uielement = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", 1), null, 12, 7)
			{
				Width = new StyleDimension(-14f, 1f),
				Height = new StyleDimension(34f, 0f),
				BackgroundColor = new Color(43, 56, 101),
				BorderColor = Color.Transparent,
				Left = new StyleDimension(5f, 0f)
			};
			uielement.SetPadding(0f);
			uielement.PaddingRight = 5f;
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
				uielement.Append(filterImage);
			}
			uielement.Append(element);
			this.AddOnHover(uielement);
			return uielement;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x005587F7 File Offset: 0x005569F7
		private void AddOnHover(UIElement button)
		{
			button.OnUpdate += delegate(UIElement e)
			{
				this.ShowButtonName(e);
			};
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x0055880C File Offset: 0x00556A0C
		private void ShowButtonName(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			string textValue = Language.GetTextValue(this.GetDisplayNameKey());
			Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x0400485B RID: 18523
		private const int framesPerRow = 16;

		// Token: 0x0400485C RID: 18524
		private const int framesPerColumn = 5;

		// Token: 0x0400485D RID: 18525
		private Point _filterIconFrame;

		// Token: 0x0400485E RID: 18526
		private string _key;
	}
}
