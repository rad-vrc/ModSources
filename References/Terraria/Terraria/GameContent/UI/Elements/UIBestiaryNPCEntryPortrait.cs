using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000364 RID: 868
	public class UIBestiaryNPCEntryPortrait : UIElement
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x00587008 File Offset: 0x00585208
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x00587010 File Offset: 0x00585210
		public BestiaryEntry Entry { get; private set; }

		// Token: 0x060027EE RID: 10222 RVA: 0x0058701C File Offset: 0x0058521C
		public UIBestiaryNPCEntryPortrait(BestiaryEntry entry, Asset<Texture2D> portraitBackgroundAsset, Color portraitColor, List<IBestiaryBackgroundOverlayAndColorProvider> overlays)
		{
			this.Entry = entry;
			this.Height.Set(112f, 0f);
			this.Width.Set(193f, 0f);
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
			if (portraitBackgroundAsset != null)
			{
				uielement.Append(new UIImage(portraitBackgroundAsset)
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
				uielement.Append(new UIImage(backgroundOverlayImage)
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
			uielement.Append(element);
			base.Append(uielement);
			base.Append(new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Portrait_Front", 1))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true
			});
		}
	}
}
