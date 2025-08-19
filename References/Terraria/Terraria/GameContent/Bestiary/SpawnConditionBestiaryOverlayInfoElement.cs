using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000311 RID: 785
	public class SpawnConditionBestiaryOverlayInfoElement : FilterProviderInfoElement, IBestiaryBackgroundOverlayAndColorProvider, IBestiaryPrioritizedElement
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x0055892E File Offset: 0x00556B2E
		// (set) Token: 0x0600240D RID: 9229 RVA: 0x00558936 File Offset: 0x00556B36
		public float DisplayPriority { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x0055893F File Offset: 0x00556B3F
		// (set) Token: 0x0600240F RID: 9231 RVA: 0x00558947 File Offset: 0x00556B47
		public float OrderPriority { get; set; }

		// Token: 0x06002410 RID: 9232 RVA: 0x00558950 File Offset: 0x00556B50
		public SpawnConditionBestiaryOverlayInfoElement(string nameLanguageKey, int filterIconFrame, string overlayImagePath = null, Color? overlayColor = null) : base(nameLanguageKey, filterIconFrame)
		{
			this._overlayImagePath = overlayImagePath;
			this._overlayColor = overlayColor;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00558969 File Offset: 0x00556B69
		public Asset<Texture2D> GetBackgroundOverlayImage()
		{
			if (this._overlayImagePath == null)
			{
				return null;
			}
			return Main.Assets.Request<Texture2D>(this._overlayImagePath, 1);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00558986 File Offset: 0x00556B86
		public Color? GetBackgroundOverlayColor()
		{
			return this._overlayColor;
		}

		// Token: 0x04004867 RID: 18535
		private string _overlayImagePath;

		// Token: 0x04004868 RID: 18536
		private Color? _overlayColor;
	}
}
