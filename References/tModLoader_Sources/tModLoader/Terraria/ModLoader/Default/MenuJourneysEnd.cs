using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.ModLoader.Default
{
	/// <summary>
	/// The Journey's End theme converted into a ModMenu, so that it better fits with the new system.
	/// </summary>
	// Token: 0x020002C1 RID: 705
	[Autoload(false)]
	internal class MenuJourneysEnd : ModMenu
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x0052E2D9 File Offset: 0x0052C4D9
		public override string DisplayName
		{
			get
			{
				return "Journey's End";
			}
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x0052E2E0 File Offset: 0x0052C4E0
		public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
		{
			return false;
		}
	}
}
