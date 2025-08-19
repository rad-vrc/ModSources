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
	// Token: 0x02000296 RID: 662
	public class MinibossButton : CustomUIImageButton
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0008692E File Offset: 0x00084B2E
		// (set) Token: 0x06001126 RID: 4390 RVA: 0x00086936 File Offset: 0x00084B36
		public LocalizedText Tooltip { get; set; }

		// Token: 0x06001127 RID: 4391 RVA: 0x00086940 File Offset: 0x00084B40
		public MinibossButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Backgrounds/Background_Red_" + MinibossButton.backgroundTexture.ToString(), 2))
		{
			this.faceTexture = faceTexture;
			base.SetHoverImage(ModContent.Request<Texture2D>("QoLCompendium/Assets/Summons/Miniboss_Hover_" + MinibossButton.minibossTexture.ToString(), 2));
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00086994 File Offset: 0x00084B94
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x000869E7 File Offset: 0x00084BE7
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.Tooltip.Value;
				Main.mouseText = true;
			}
		}

		// Token: 0x04000746 RID: 1862
		private readonly Asset<Texture2D> faceTexture;

		// Token: 0x04000748 RID: 1864
		public static int minibossTexture;

		// Token: 0x04000749 RID: 1865
		public static int backgroundTexture;
	}
}
