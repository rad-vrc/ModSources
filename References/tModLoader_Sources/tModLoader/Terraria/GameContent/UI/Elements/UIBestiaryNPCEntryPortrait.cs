using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000509 RID: 1289
	public class UIBestiaryNPCEntryPortrait : UIElement
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x005CEFEB File Offset: 0x005CD1EB
		// (set) Token: 0x06003E13 RID: 15891 RVA: 0x005CEFF3 File Offset: 0x005CD1F3
		public BestiaryEntry Entry { get; private set; }

		// Token: 0x06003E14 RID: 15892 RVA: 0x005CEFFC File Offset: 0x005CD1FC
		public UIBestiaryNPCEntryPortrait(BestiaryEntry entry, Asset<Texture2D> portraitBackgroundAsset, Color portraitColor, List<IBestiaryBackgroundOverlayAndColorProvider> overlays)
		{
			this.Entry = entry;
			this.Height.Set(112f, 0f);
			this.Width.Set(193f, 0f);
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
			if (portraitBackgroundAsset != null)
			{
				uIElement.Append(new UIImage(portraitBackgroundAsset)
				{
					HAlign = 0.5f,
					VAlign = 0.5f,
					ScaleToFit = true,
					Width = new StyleDimension(0f, 1f),
					Height = new StyleDimension(0f, 1f),
					Color = portraitColor
				});
			}
			for (int i = 0; i < overlays.Count; i++)
			{
				Asset<Texture2D> backgroundOverlayImage = overlays[i].GetBackgroundOverlayImage();
				Color? backgroundOverlayColor = overlays[i].GetBackgroundOverlayColor();
				uIElement.Append(new UIImage(backgroundOverlayImage)
				{
					HAlign = 0.5f,
					VAlign = 0.5f,
					ScaleToFit = true,
					Width = new StyleDimension(0f, 1f),
					Height = new StyleDimension(0f, 1f),
					Color = ((backgroundOverlayColor != null) ? backgroundOverlayColor.Value : Color.Lerp(Color.White, portraitColor, 0.5f))
				});
			}
			UIBestiaryEntryIcon element = new UIBestiaryEntryIcon(entry, true);
			uIElement.Append(element);
			base.Append(uIElement);
			base.Append(new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Portrait_Front"))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true
			});
		}
	}
}
