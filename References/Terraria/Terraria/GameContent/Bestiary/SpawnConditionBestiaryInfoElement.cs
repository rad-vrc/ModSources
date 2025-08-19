using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000310 RID: 784
	public class SpawnConditionBestiaryInfoElement : FilterProviderInfoElement, IBestiaryBackgroundImagePathAndColorProvider, IBestiaryPrioritizedElement
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x005588DF File Offset: 0x00556ADF
		// (set) Token: 0x06002408 RID: 9224 RVA: 0x005588E7 File Offset: 0x00556AE7
		public float OrderPriority { get; set; }

		// Token: 0x06002409 RID: 9225 RVA: 0x005588F0 File Offset: 0x00556AF0
		public SpawnConditionBestiaryInfoElement(string nameLanguageKey, int filterIconFrame, string backgroundImagePath = null, Color? backgroundColor = null) : base(nameLanguageKey, filterIconFrame)
		{
			this._backgroundImagePath = backgroundImagePath;
			this._backgroundColor = backgroundColor;
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x00558909 File Offset: 0x00556B09
		public Asset<Texture2D> GetBackgroundImage()
		{
			if (this._backgroundImagePath == null)
			{
				return null;
			}
			return Main.Assets.Request<Texture2D>(this._backgroundImagePath, 1);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x00558926 File Offset: 0x00556B26
		public Color? GetBackgroundColor()
		{
			return this._backgroundColor;
		}

		// Token: 0x04004864 RID: 18532
		private string _backgroundImagePath;

		// Token: 0x04004865 RID: 18533
		private Color? _backgroundColor;
	}
}
