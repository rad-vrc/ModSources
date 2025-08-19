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
	// Token: 0x02000297 RID: 663
	public class MoonPhaseButton : CustomUIImageButton
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00086A0E File Offset: 0x00084C0E
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x00086A16 File Offset: 0x00084C16
		public LocalizedText Tooltip { get; set; }

		// Token: 0x0600112C RID: 4396 RVA: 0x00086A20 File Offset: 0x00084C20
		public MoonPhaseButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Backgrounds/Background_" + MoonPhaseButton.backgroundTexture.ToString(), 2))
		{
			this.faceTexture = faceTexture;
			base.SetHoverImage(ModContent.Request<Texture2D>("QoLCompendium/Assets/Moons/Moon_Hover_" + MoonPhaseButton.moonTexture.ToString(), 2));
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00086A74 File Offset: 0x00084C74
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00086AC7 File Offset: 0x00084CC7
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.Tooltip.Value;
				Main.mouseText = true;
			}
		}

		// Token: 0x0400074A RID: 1866
		private readonly Asset<Texture2D> faceTexture;

		// Token: 0x0400074C RID: 1868
		public static int moonTexture;

		// Token: 0x0400074D RID: 1869
		public static int backgroundTexture;
	}
}
