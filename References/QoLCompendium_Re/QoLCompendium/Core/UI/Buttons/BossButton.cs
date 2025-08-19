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
	// Token: 0x02000293 RID: 659
	public class BossButton : CustomUIImageButton
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0008668E File Offset: 0x0008488E
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00086696 File Offset: 0x00084896
		public LocalizedText Tooltip { get; set; }

		// Token: 0x06001118 RID: 4376 RVA: 0x000866A0 File Offset: 0x000848A0
		public BossButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Backgrounds/Background_Red_" + BossButton.backgroundTexture.ToString(), 2))
		{
			this.faceTexture = faceTexture;
			base.SetHoverImage(ModContent.Request<Texture2D>("QoLCompendium/Assets/Summons/Boss_Hover_" + BossButton.bossTexture.ToString(), 2));
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000866F4 File Offset: 0x000848F4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00086747 File Offset: 0x00084947
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.Tooltip.Value;
				Main.mouseText = true;
			}
		}

		// Token: 0x0400073A RID: 1850
		private readonly Asset<Texture2D> faceTexture;

		// Token: 0x0400073C RID: 1852
		public static int bossTexture;

		// Token: 0x0400073D RID: 1853
		public static int backgroundTexture;
	}
}
