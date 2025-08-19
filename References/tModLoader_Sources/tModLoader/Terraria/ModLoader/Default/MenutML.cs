using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.ModLoader.Default
{
	/// <summary>
	/// This is the default modmenu - the one that tML uses and the default one upon entering the game for the first time.
	/// </summary>
	// Token: 0x020002C0 RID: 704
	[Autoload(false)]
	internal class MenutML : ModMenu
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06002D6D RID: 11629 RVA: 0x0052E2BB File Offset: 0x0052C4BB
		public override string DisplayName
		{
			get
			{
				return "tModLoader";
			}
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x0052E2C2 File Offset: 0x0052C4C2
		public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
		{
			logoScale *= 0.84f;
			return true;
		}
	}
}
