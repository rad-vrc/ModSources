using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace QoLCompendium.Core.UI
{
	// Token: 0x0200026A RID: 618
	public class MouseCenteredUIPanel : UIPanel
	{
		// Token: 0x06000E5D RID: 3677 RVA: 0x00073945 File Offset: 0x00071B45
		public override void OnActivate()
		{
			base.OnActivate();
			this.centeredOnMouse = true;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00073954 File Offset: 0x00071B54
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (!this.centeredOnMouse)
			{
				return;
			}
			this.centeredOnMouse = false;
			this.Left.Set((float)Main.mouseX - this.Width.Pixels / 2f, 0f);
			this.Top.Set((float)Main.mouseY - this.Height.Pixels / 2f, 0f);
			this.Recalculate();
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000739CE File Offset: 0x00071BCE
		protected override void DrawChildren(SpriteBatch spriteBatch)
		{
			if (!this.centeredOnMouse)
			{
				base.DrawChildren(spriteBatch);
			}
		}

		// Token: 0x040005C9 RID: 1481
		private bool centeredOnMouse;
	}
}
