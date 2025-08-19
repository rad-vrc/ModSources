using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000699 RID: 1689
	public class ModSourceBestiaryInfoElement : ModBestiaryInfoElement
	{
		// Token: 0x06004815 RID: 18453 RVA: 0x006472A6 File Offset: 0x006454A6
		public ModSourceBestiaryInfoElement(Mod mod, string displayName)
		{
			this._mod = mod;
			this._displayName = displayName;
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x006472BC File Offset: 0x006454BC
		public override UIElement GetFilterImage()
		{
			Asset<Texture2D> asset;
			if (this._mod.HasAsset("icon_small"))
			{
				asset = this._mod.Assets.Request<Texture2D>("icon_small");
				if (asset.Size() == new Vector2(30f))
				{
					return new UIImage(asset)
					{
						HAlign = 0.5f,
						VAlign = 0.5f
					};
				}
				this._mod.Logger.Info("icon_small needs to be 30x30 pixels.");
			}
			asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow");
			return new UIImageFramed(asset, asset.Frame(16, 5, 0, 4, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
		}
	}
}
