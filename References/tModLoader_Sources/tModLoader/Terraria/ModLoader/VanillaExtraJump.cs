using System;
using System.Collections.Generic;

namespace Terraria.ModLoader
{
	// Token: 0x0200021A RID: 538
	[Autoload(false)]
	public abstract class VanillaExtraJump : ExtraJump
	{
		// Token: 0x06002829 RID: 10281 RVA: 0x00509A48 File Offset: 0x00507C48
		public sealed override ExtraJump.Position GetDefaultPosition()
		{
			return null;
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x00509A4B File Offset: 0x00507C4B
		public sealed override IEnumerable<ExtraJump.Position> GetModdedConstraints()
		{
			return null;
		}
	}
}
