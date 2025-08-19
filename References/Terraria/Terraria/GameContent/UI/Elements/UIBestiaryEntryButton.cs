using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200035F RID: 863
	public class UIBestiaryEntryButton : UIElement
	{
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x00585616 File Offset: 0x00583816
		// (set) Token: 0x060027BE RID: 10174 RVA: 0x0058561E File Offset: 0x0058381E
		public BestiaryEntry Entry { get; private set; }

		// Token: 0x060027BF RID: 10175 RVA: 0x00585628 File Offset: 0x00583828
		public UIBestiaryEntryButton(BestiaryEntry entry, bool isAPrettyPortrait)
		{
			this.Entry = entry;
			this.Height.Set(72f, 0f);
			this.Width.Set(72f, 0f);
			base.SetPadding(0f);
			UIElement uielement = new UIElement
			{
				Width = new StyleDimension(-4f, 1f),
				Height = new StyleDimension(-4f, 1f),
				IgnoresMouseInteraction = true,
				OverflowHidden = true,
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			uielement.SetPadding(0f);
			uielement.Append(new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Back", 1))
			{
				VAlign = 0.5f,
				HAlign = 0.5f
			});
			if (isAPrettyPortrait)
			{
				Asset<Texture2D> asset = this.TryGettingBackgroundImageProvider(entry);
				if (asset != null)
				{
					uielement.Append(new UIImage(asset)
					{
						HAlign = 0.5f,
						VAlign = 0.5f
					});
				}
			}
			UIBestiaryEntryIcon uibestiaryEntryIcon = new UIBestiaryEntryIcon(entry, isAPrettyPortrait);
			uielement.Append(uibestiaryEntryIcon);
			base.Append(uielement);
			this._icon = uibestiaryEntryIcon;
			int? num = this.TryGettingDisplayIndex(entry);
			if (num != null)
			{
				UIText element = new UIText(num.Value.ToString(), 0.9f, false)
				{
					Top = new StyleDimension(10f, 0f),
					Left = new StyleDimension(10f, 0f),
					IgnoresMouseInteraction = true
				};
				base.Append(element);
			}
			this._bordersGlow = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Selection", 1))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			this._bordersOverlay = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Overlay", 1))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true,
				Color = Color.White * 0.6f
			};
			base.Append(this._bordersOverlay);
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Front", 1));
			uiimage.VAlign = 0.5f;
			uiimage.HAlign = 0.5f;
			uiimage.IgnoresMouseInteraction = true;
			base.Append(uiimage);
			this._borders = uiimage;
			if (isAPrettyPortrait)
			{
				base.RemoveChild(this._bordersOverlay);
			}
			if (!isAPrettyPortrait)
			{
				base.OnMouseOver += this.MouseOver;
				base.OnMouseOut += this.MouseOut;
			}
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x005858E0 File Offset: 0x00583AE0
		private Asset<Texture2D> TryGettingBackgroundImageProvider(BestiaryEntry entry)
		{
			IEnumerable<IBestiaryBackgroundImagePathAndColorProvider> enumerable = from x in entry.Info
			where x is IBestiaryBackgroundImagePathAndColorProvider
			select x as IBestiaryBackgroundImagePathAndColorProvider;
			IEnumerable<IPreferenceProviderElement> preferences = entry.Info.OfType<IPreferenceProviderElement>();
			foreach (IBestiaryBackgroundImagePathAndColorProvider bestiaryBackgroundImagePathAndColorProvider in from provider in enumerable
			where preferences.Any((IPreferenceProviderElement preference) => preference.Matches(provider))
			select provider)
			{
				Asset<Texture2D> backgroundImage = bestiaryBackgroundImagePathAndColorProvider.GetBackgroundImage();
				if (backgroundImage != null)
				{
					return backgroundImage;
				}
			}
			foreach (IBestiaryBackgroundImagePathAndColorProvider bestiaryBackgroundImagePathAndColorProvider2 in enumerable)
			{
				Asset<Texture2D> backgroundImage = bestiaryBackgroundImagePathAndColorProvider2.GetBackgroundImage();
				if (backgroundImage != null)
				{
					return backgroundImage;
				}
			}
			return null;
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x005859E8 File Offset: 0x00583BE8
		private int? TryGettingDisplayIndex(BestiaryEntry entry)
		{
			int? result = null;
			IBestiaryInfoElement bestiaryInfoElement = entry.Info.FirstOrDefault((IBestiaryInfoElement x) => x is IBestiaryEntryDisplayIndex);
			if (bestiaryInfoElement != null)
			{
				result = new int?((bestiaryInfoElement as IBestiaryEntryDisplayIndex).BestiaryDisplayIndex);
			}
			return result;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x00585A40 File Offset: 0x00583C40
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (base.IsMouseHovering)
			{
				Main.instance.MouseText(this._icon.GetHoverText(), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x00585A74 File Offset: 0x00583C74
		private void MouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.RemoveChild(this._borders);
			base.RemoveChild(this._bordersGlow);
			base.RemoveChild(this._bordersOverlay);
			base.Append(this._borders);
			base.Append(this._bordersGlow);
			this._icon.ForceHover = true;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x00585AE0 File Offset: 0x00583CE0
		private void MouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			base.RemoveChild(this._borders);
			base.RemoveChild(this._bordersGlow);
			base.RemoveChild(this._bordersOverlay);
			base.Append(this._bordersOverlay);
			base.Append(this._borders);
			this._icon.ForceHover = false;
		}

		// Token: 0x04004B15 RID: 19221
		private UIImage _bordersGlow;

		// Token: 0x04004B16 RID: 19222
		private UIImage _bordersOverlay;

		// Token: 0x04004B17 RID: 19223
		private UIImage _borders;

		// Token: 0x04004B18 RID: 19224
		private UIBestiaryEntryIcon _icon;
	}
}
