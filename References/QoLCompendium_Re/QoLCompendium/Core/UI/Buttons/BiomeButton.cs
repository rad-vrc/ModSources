using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Buttons
{
	// Token: 0x02000292 RID: 658
	public class BiomeButton : CustomUIImageButton
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x000865AE File Offset: 0x000847AE
		// (set) Token: 0x06001112 RID: 4370 RVA: 0x000865B6 File Offset: 0x000847B6
		public LocalizedText Tooltip { get; set; }

		// Token: 0x06001113 RID: 4371 RVA: 0x000865C0 File Offset: 0x000847C0
		public BiomeButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Backgrounds/Background_" + BiomeButton.backgroundTexture.ToString(), 2))
		{
			this.faceTexture = faceTexture;
			base.SetHoverImage(ModContent.Request<Texture2D>("QoLCompendium/Assets/Biomes/Biome_Hover_" + BiomeButton.biomeTexture.ToString(), 2));
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00086614 File Offset: 0x00084814
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00086667 File Offset: 0x00084867
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.Tooltip.Value;
				Main.mouseText = true;
			}
		}

		// Token: 0x04000736 RID: 1846
		private readonly Asset<Texture2D> faceTexture;

		// Token: 0x04000738 RID: 1848
		public static int biomeTexture;

		// Token: 0x04000739 RID: 1849
		public static int backgroundTexture;
	}
}
