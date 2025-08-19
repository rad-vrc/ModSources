using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200052D RID: 1325
	public class FixedUIScrollbar : UIScrollbar
	{
		// Token: 0x06003F35 RID: 16181 RVA: 0x005D8525 File Offset: 0x005D6725
		public FixedUIScrollbar(UserInterface userInterface)
		{
			this.userInterface = userInterface;
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x005D8534 File Offset: 0x005D6734
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			UserInterface activeInstance = UserInterface.ActiveInstance;
			UserInterface.ActiveInstance = this.userInterface;
			base.DrawSelf(spriteBatch);
			UserInterface.ActiveInstance = activeInstance;
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x005D8552 File Offset: 0x005D6752
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			UserInterface activeInstance = UserInterface.ActiveInstance;
			UserInterface.ActiveInstance = this.userInterface;
			base.LeftMouseDown(evt);
			UserInterface.ActiveInstance = activeInstance;
		}

		// Token: 0x040057A9 RID: 22441
		private UserInterface userInterface;
	}
}
