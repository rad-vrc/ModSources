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
	// Token: 0x02000295 RID: 661
	public class GenericUIButton : CustomUIImageButton
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0008684E File Offset: 0x00084A4E
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x00086856 File Offset: 0x00084A56
		public LocalizedText Tooltip { get; set; }

		// Token: 0x06001122 RID: 4386 RVA: 0x00086860 File Offset: 0x00084A60
		public GenericUIButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Backgrounds/Background_" + GenericUIButton.backgroundTexture.ToString(), 2))
		{
			this.faceTexture = faceTexture;
			base.SetHoverImage(ModContent.Request<Texture2D>("QoLCompendium/Assets/Buttons/Button_Small_Hover_" + GenericUIButton.buttonTexture.ToString(), 2));
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000868B4 File Offset: 0x00084AB4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00086907 File Offset: 0x00084B07
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.Tooltip.Value;
				Main.mouseText = true;
			}
		}

		// Token: 0x04000742 RID: 1858
		private readonly Asset<Texture2D> faceTexture;

		// Token: 0x04000744 RID: 1860
		public static int buttonTexture;

		// Token: 0x04000745 RID: 1861
		public static int backgroundTexture;
	}
}
