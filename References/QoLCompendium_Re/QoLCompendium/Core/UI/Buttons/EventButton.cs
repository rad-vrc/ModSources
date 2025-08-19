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
	// Token: 0x02000294 RID: 660
	public class EventButton : CustomUIImageButton
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0008676E File Offset: 0x0008496E
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x00086776 File Offset: 0x00084976
		public LocalizedText Tooltip { get; set; }

		// Token: 0x0600111D RID: 4381 RVA: 0x00086780 File Offset: 0x00084980
		public EventButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Backgrounds/Background_Red_" + EventButton.backgroundTexture.ToString(), 2))
		{
			this.faceTexture = faceTexture;
			base.SetHoverImage(ModContent.Request<Texture2D>("QoLCompendium/Assets/Summons/Event_Hover_" + EventButton.eventTexture.ToString(), 2));
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000867D4 File Offset: 0x000849D4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00086827 File Offset: 0x00084A27
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.Tooltip.Value;
				Main.mouseText = true;
			}
		}

		// Token: 0x0400073E RID: 1854
		private readonly Asset<Texture2D> faceTexture;

		// Token: 0x04000740 RID: 1856
		public static int eventTexture;

		// Token: 0x04000741 RID: 1857
		public static int backgroundTexture;
	}
}
