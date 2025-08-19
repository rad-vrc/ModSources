using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006A7 RID: 1703
	public class SpawnConditionBestiaryInfoElement : FilterProviderInfoElement, IBestiaryBackgroundImagePathAndColorProvider, IBestiaryPrioritizedElement
	{
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x006490BD File Offset: 0x006472BD
		// (set) Token: 0x06004863 RID: 18531 RVA: 0x006490C5 File Offset: 0x006472C5
		public float OrderPriority { get; set; }

		// Token: 0x06004864 RID: 18532 RVA: 0x006490CE File Offset: 0x006472CE
		public SpawnConditionBestiaryInfoElement(string nameLanguageKey, int filterIconFrame, string backgroundImagePath = null, Color? backgroundColor = null) : base(nameLanguageKey, filterIconFrame)
		{
			this._backgroundImagePath = backgroundImagePath;
			this._backgroundColor = backgroundColor;
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x006490E7 File Offset: 0x006472E7
		public Asset<Texture2D> GetBackgroundImage()
		{
			if (this._backgroundImagePath == null)
			{
				return null;
			}
			return Main.Assets.Request<Texture2D>(this._backgroundImagePath);
		}

		// Token: 0x06004866 RID: 18534 RVA: 0x00649103 File Offset: 0x00647303
		public Color? GetBackgroundColor()
		{
			return this._backgroundColor;
		}

		// Token: 0x04005C2E RID: 23598
		private string _backgroundImagePath;

		// Token: 0x04005C2F RID: 23599
		private Color? _backgroundColor;
	}
}
