using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.ModLoader.Default
{
	/// <summary>
	/// The Terraria 1.3.5.3 theme converted into a ModMenu, so that it better fits with the new system.
	/// </summary>
	// Token: 0x020002C2 RID: 706
	[Autoload(false)]
	internal class MenuOldVanilla : ModMenu
	{
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x0052E2EB File Offset: 0x0052C4EB
		public override bool IsAvailable
		{
			get
			{
				return Main.instance.playOldTile;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x0052E2F7 File Offset: 0x0052C4F7
		public override string DisplayName
		{
			get
			{
				return "Terraria 1.3.5.3";
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x0052E2FE File Offset: 0x0052C4FE
		public override int Music
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x0052E301 File Offset: 0x0052C501
		public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
		{
			return false;
		}
	}
}
