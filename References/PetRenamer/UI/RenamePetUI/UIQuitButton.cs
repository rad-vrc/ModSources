using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace PetRenamer.UI.RenamePetUI
{
	// Token: 0x0200000B RID: 11
	internal class UIQuitButton : UIPanel
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003F20 File Offset: 0x00002120
		internal UIQuitButton(string hoverText)
		{
			if (UIQuitButton.asset == null)
			{
				UIQuitButton.asset = ModContent.Request<Texture2D>("PetRenamer/UI/RenamePetUI/UIQuitButton", 1);
			}
			this.hoverText = hoverText;
			this.BackgroundColor = Color.Transparent;
			this.BorderColor = Color.Transparent;
			this.Width.Pixels = (float)UIQuitButton.asset.Width();
			this.Height.Pixels = (float)UIQuitButton.asset.Height();
			this.Recalculate();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003F9C File Offset: 0x0000219C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 pos = new Vector2(innerDimensions.X, innerDimensions.Y) - new Vector2((float)((int)(this.Width.Pixels * 0.75f)), (float)((int)(this.Height.Pixels * 0.75f)));
			Texture2D value = UIQuitButton.asset.Value;
			spriteBatch.Draw(value, pos, new Rectangle?(value.Bounds), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.hoverText;
			}
		}

		// Token: 0x04000048 RID: 72
		internal static Asset<Texture2D> asset;

		// Token: 0x04000049 RID: 73
		internal string hoverText;
	}
}
