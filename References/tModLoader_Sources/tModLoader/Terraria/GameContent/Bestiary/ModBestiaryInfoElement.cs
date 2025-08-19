using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000697 RID: 1687
	public abstract class ModBestiaryInfoElement : IFilterInfoProvider, IProvideSearchFilterString, IBestiaryInfoElement
	{
		// Token: 0x06004809 RID: 18441 RVA: 0x00646EF8 File Offset: 0x006450F8
		public virtual UIElement GetFilterImage()
		{
			Asset<Texture2D> asset;
			if (this._iconPath != null && ModContent.RequestIfExists<Texture2D>(this._iconPath, out asset, 2))
			{
				if (asset.Size() == new Vector2(30f))
				{
					return new UIImage(asset)
					{
						HAlign = 0.5f,
						VAlign = 0.5f
					};
				}
				this._mod.Logger.Info(this._iconPath + " needs to be 30x30 pixels.");
			}
			asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow");
			return new UIImageFramed(asset, asset.Frame(16, 5, 0, 4, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x00646FAC File Offset: 0x006451AC
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
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

		// Token: 0x0600480B RID: 18443 RVA: 0x006470E1 File Offset: 0x006452E1
		private void AddOnHover(UIElement button)
		{
			button.OnUpdate += delegate(UIElement e)
			{
				this.ShowButtonName(e);
			};
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x006470F8 File Offset: 0x006452F8
		private void ShowButtonName(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				string textValue = Language.GetTextValue(this.GetDisplayNameKey());
				Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0064712B File Offset: 0x0064532B
		public string GetDisplayNameKey()
		{
			return this._displayName;
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x00647133 File Offset: 0x00645333
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return this._displayName;
		}

		// Token: 0x04005C0F RID: 23567
		internal Mod _mod;

		// Token: 0x04005C10 RID: 23568
		internal string _displayName;

		// Token: 0x04005C11 RID: 23569
		internal string _iconPath;

		// Token: 0x04005C12 RID: 23570
		internal string _backgroundPath;

		// Token: 0x04005C13 RID: 23571
		internal Color? _backgroundColor;
	}
}
