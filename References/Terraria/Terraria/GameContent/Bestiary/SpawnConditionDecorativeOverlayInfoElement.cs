using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000312 RID: 786
	public class SpawnConditionDecorativeOverlayInfoElement : IBestiaryInfoElement, IBestiaryBackgroundOverlayAndColorProvider
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x0055898E File Offset: 0x00556B8E
		// (set) Token: 0x06002414 RID: 9236 RVA: 0x00558996 File Offset: 0x00556B96
		public float DisplayPriority { get; set; }

		// Token: 0x06002415 RID: 9237 RVA: 0x0055899F File Offset: 0x00556B9F
		public SpawnConditionDecorativeOverlayInfoElement(string overlayImagePath = null, Color? overlayColor = null)
		{
			this._overlayImagePath = overlayImagePath;
			this._overlayColor = overlayColor;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x005589B5 File Offset: 0x00556BB5
		public Asset<Texture2D> GetBackgroundOverlayImage()
		{
			if (this._overlayImagePath == null)
			{
				return null;
			}
			return Main.Assets.Request<Texture2D>(this._overlayImagePath, 1);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x005589D2 File Offset: 0x00556BD2
		public Color? GetBackgroundOverlayColor()
		{
			return this._overlayColor;
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x0400486B RID: 18539
		private string _overlayImagePath;

		// Token: 0x0400486C RID: 18540
		private Color? _overlayColor;
	}
}
