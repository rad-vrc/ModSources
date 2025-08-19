using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000698 RID: 1688
	public class ModBiomeBestiaryInfoElement : ModBestiaryInfoElement, IBestiaryBackgroundImagePathAndColorProvider
	{
		// Token: 0x06004811 RID: 18449 RVA: 0x00647156 File Offset: 0x00645356
		public ModBiomeBestiaryInfoElement(Mod mod, string displayName, string iconPath, string backgroundPath, Color? backgroundColor)
		{
			this._mod = mod;
			this._displayName = displayName;
			this._iconPath = iconPath;
			this._backgroundPath = backgroundPath;
			this._backgroundColor = backgroundColor;
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x00647184 File Offset: 0x00645384
		public override UIElement GetFilterImage()
		{
			Asset<Texture2D> asset;
			if (this._iconPath != null && ModContent.RequestIfExists<Texture2D>(this._iconPath, out asset, 1))
			{
				if (asset.Size() == new Vector2(30f))
				{
					return new UIImage(asset)
					{
						HAlign = 0.5f,
						VAlign = 0.5f
					};
				}
				this._mod.Logger.Info(this._iconPath + " needs to be 30x30 pixels.");
			}
			asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow");
			return new UIImageFramed(asset, asset.Frame(16, 5, 0, 4, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x00647238 File Offset: 0x00645438
		public Asset<Texture2D> GetBackgroundImage()
		{
			Asset<Texture2D> asset;
			if (this._backgroundPath == null || !ModContent.RequestIfExists<Texture2D>(this._backgroundPath, out asset, 1))
			{
				return null;
			}
			if (asset.Size() == new Vector2(115f, 65f))
			{
				return asset;
			}
			this._mod.Logger.Info(this._backgroundPath + " needs to be 115x65 pixels.");
			return null;
		}

		// Token: 0x06004814 RID: 18452 RVA: 0x0064729E File Offset: 0x0064549E
		public Color? GetBackgroundColor()
		{
			return this._backgroundColor;
		}
	}
}
