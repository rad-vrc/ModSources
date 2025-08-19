using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.ModLoader.UI.Elements
{
	// Token: 0x02000274 RID: 628
	public class FixedUIScrollbar : UIScrollbar
	{
		// Token: 0x06002B44 RID: 11076 RVA: 0x00521CAA File Offset: 0x0051FEAA
		public FixedUIScrollbar(UserInterface userInterface)
		{
			this.userInterface = userInterface;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x00521CB9 File Offset: 0x0051FEB9
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			UserInterface activeInstance = UserInterface.ActiveInstance;
			UserInterface.ActiveInstance = this.userInterface;
			base.DrawSelf(spriteBatch);
			UserInterface.ActiveInstance = activeInstance;
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x00521CD7 File Offset: 0x0051FED7
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			UserInterface activeInstance = UserInterface.ActiveInstance;
			UserInterface.ActiveInstance = this.userInterface;
			base.LeftMouseDown(evt);
			UserInterface.ActiveInstance = activeInstance;
		}

		// Token: 0x04001BC9 RID: 7113
		internal UserInterface userInterface;
	}
}
