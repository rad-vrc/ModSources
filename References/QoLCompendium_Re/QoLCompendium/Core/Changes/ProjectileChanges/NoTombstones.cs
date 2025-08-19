using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ProjectileChanges
{
	// Token: 0x0200022B RID: 555
	public class NoTombstones : ModSystem
	{
		// Token: 0x06000D5F RID: 3423 RVA: 0x00067FBA File Offset: 0x000661BA
		public override void Load()
		{
			On_Player.DropTombstone += new On_Player.hook_DropTombstone(this.DontSpawnTombstones);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00067FCD File Offset: 0x000661CD
		public override void Unload()
		{
			On_Player.DropTombstone -= new On_Player.hook_DropTombstone(this.DontSpawnTombstones);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00067FE0 File Offset: 0x000661E0
		private void DontSpawnTombstones(On_Player.orig_DropTombstone orig, Player self, long coinsOwned, NetworkText deathText, int hitDirection)
		{
			if (!QoLCompendium.mainConfig.NoTombstoneDrops)
			{
				orig.Invoke(self, coinsOwned, deathText, hitDirection);
			}
		}
	}
}
