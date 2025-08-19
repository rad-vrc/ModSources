using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006A8 RID: 1704
	public class SpawnConditionBestiaryOverlayInfoElement : FilterProviderInfoElement, IBestiaryBackgroundOverlayAndColorProvider, IBestiaryPrioritizedElement
	{
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06004867 RID: 18535 RVA: 0x0064910B File Offset: 0x0064730B
		// (set) Token: 0x06004868 RID: 18536 RVA: 0x00649113 File Offset: 0x00647313
		public float DisplayPriority { get; set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06004869 RID: 18537 RVA: 0x0064911C File Offset: 0x0064731C
		// (set) Token: 0x0600486A RID: 18538 RVA: 0x00649124 File Offset: 0x00647324
		public float OrderPriority { get; set; }

		// Token: 0x0600486B RID: 18539 RVA: 0x0064912D File Offset: 0x0064732D
		public SpawnConditionBestiaryOverlayInfoElement(string nameLanguageKey, int filterIconFrame, string overlayImagePath = null, Color? overlayColor = null) : base(nameLanguageKey, filterIconFrame)
		{
			this._overlayImagePath = overlayImagePath;
			this._overlayColor = overlayColor;
		}

		// Token: 0x0600486C RID: 18540 RVA: 0x00649146 File Offset: 0x00647346
		public Asset<Texture2D> GetBackgroundOverlayImage()
		{
			if (this._overlayImagePath == null)
			{
				return null;
			}
			return Main.Assets.Request<Texture2D>(this._overlayImagePath);
		}

		// Token: 0x0600486D RID: 18541 RVA: 0x00649162 File Offset: 0x00647362
		public Color? GetBackgroundOverlayColor()
		{
			return this._overlayColor;
		}

		// Token: 0x04005C31 RID: 23601
		private string _overlayImagePath;

		// Token: 0x04005C32 RID: 23602
		private Color? _overlayColor;
	}
}
