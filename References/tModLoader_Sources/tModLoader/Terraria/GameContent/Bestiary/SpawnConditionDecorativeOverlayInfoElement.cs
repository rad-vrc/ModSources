using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006A9 RID: 1705
	public class SpawnConditionDecorativeOverlayInfoElement : IBestiaryInfoElement, IBestiaryBackgroundOverlayAndColorProvider
	{
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600486E RID: 18542 RVA: 0x0064916A File Offset: 0x0064736A
		// (set) Token: 0x0600486F RID: 18543 RVA: 0x00649172 File Offset: 0x00647372
		public float DisplayPriority { get; set; }

		// Token: 0x06004870 RID: 18544 RVA: 0x0064917B File Offset: 0x0064737B
		public SpawnConditionDecorativeOverlayInfoElement(string overlayImagePath = null, Color? overlayColor = null)
		{
			this._overlayImagePath = overlayImagePath;
			this._overlayColor = overlayColor;
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x00649191 File Offset: 0x00647391
		public Asset<Texture2D> GetBackgroundOverlayImage()
		{
			if (this._overlayImagePath == null)
			{
				return null;
			}
			return Main.Assets.Request<Texture2D>(this._overlayImagePath);
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x006491AD File Offset: 0x006473AD
		public Color? GetBackgroundOverlayColor()
		{
			return this._overlayColor;
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x006491B5 File Offset: 0x006473B5
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005C35 RID: 23605
		private string _overlayImagePath;

		// Token: 0x04005C36 RID: 23606
		private Color? _overlayColor;
	}
}
