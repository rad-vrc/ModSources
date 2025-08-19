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
	// Token: 0x02000502 RID: 1282
	public class UIBestiaryEntryButton : UIElement
	{
		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x005CD0FC File Offset: 0x005CB2FC
		// (set) Token: 0x06003DCB RID: 15819 RVA: 0x005CD104 File Offset: 0x005CB304
		public BestiaryEntry Entry { get; private set; }

		// Token: 0x06003DCC RID: 15820 RVA: 0x005CD110 File Offset: 0x005CB310
		public UIBestiaryEntryButton(BestiaryEntry entry, bool isAPrettyPortrait)
		{
			this.Entry = entry;
			this.Height.Set(72f, 0f);
			this.Width.Set(72f, 0f);
			base.SetPadding(0f);
			UIElement uIElement = new UIElement
			{
				Width = new StyleDimension(-4f, 1f),
				Height = new StyleDimension(-4f, 1f),
				IgnoresMouseInteraction = true,
				OverflowHidden = true,
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			uIElement.SetPadding(0f);
			uIElement.Append(new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Back"))
			{
				VAlign = 0.5f,
				HAlign = 0.5f
			});
			if (isAPrettyPortrait)
			{
				Asset<Texture2D> asset = this.TryGettingBackgroundImageProvider(entry);
				if (asset != null)
				{
					uIElement.Append(new UIImage(asset)
					{
						HAlign = 0.5f,
						VAlign = 0.5f
					});
				}
			}
			UIBestiaryEntryIcon uIBestiaryEntryIcon = new UIBestiaryEntryIcon(entry, isAPrettyPortrait);
			uIElement.Append(uIBestiaryEntryIcon);
			base.Append(uIElement);
			this._icon = uIBestiaryEntryIcon;
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
			this._bordersGlow = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Selection"))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			this._bordersOverlay = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Overlay"))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true,
				Color = Color.White * 0.6f
			};
			base.Append(this._bordersOverlay);
			UIImage uIImage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Front"))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			base.Append(uIImage);
			this._borders = uIImage;
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

		// Token: 0x06003DCD RID: 15821 RVA: 0x005CD3AC File Offset: 0x005CB5AC
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
				Asset<Texture2D> asset = bestiaryBackgroundImagePathAndColorProvider.GetBackgroundImage();
				if (asset != null)
				{
					return asset;
				}
			}
			foreach (IBestiaryBackgroundImagePathAndColorProvider bestiaryBackgroundImagePathAndColorProvider2 in enumerable)
			{
				Asset<Texture2D> asset = bestiaryBackgroundImagePathAndColorProvider2.GetBackgroundImage();
				if (asset != null)
				{
					return asset;
				}
			}
			return null;
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x005CD4B4 File Offset: 0x005CB6B4
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

		// Token: 0x06003DCF RID: 15823 RVA: 0x005CD50C File Offset: 0x005CB70C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (base.IsMouseHovering)
			{
				Main.instance.MouseText(this._icon.GetHoverText(), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x005CD540 File Offset: 0x005CB740
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

		// Token: 0x06003DD1 RID: 15825 RVA: 0x005CD5AC File Offset: 0x005CB7AC
		private void MouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			base.RemoveChild(this._borders);
			base.RemoveChild(this._bordersGlow);
			base.RemoveChild(this._bordersOverlay);
			base.Append(this._bordersOverlay);
			base.Append(this._borders);
			this._icon.ForceHover = false;
		}

		// Token: 0x04005695 RID: 22165
		private UIImage _bordersGlow;

		// Token: 0x04005696 RID: 22166
		private UIImage _bordersOverlay;

		// Token: 0x04005697 RID: 22167
		private UIImage _borders;

		// Token: 0x04005698 RID: 22168
		private UIBestiaryEntryIcon _icon;
	}
}
