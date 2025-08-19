using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x02000222 RID: 546
	public class UnbreakableQueenBeeLarva : ModSystem
	{
		// Token: 0x06000D3D RID: 3389 RVA: 0x0006750D File Offset: 0x0006570D
		public override void Load()
		{
			UnbreakableQueenBeeLarva.oldBreak = Main.tileCut[231];
			if (QoLCompendium.mainConfig.NoLarvaBreak)
			{
				Main.tileCut[231] = false;
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00067537 File Offset: 0x00065737
		public override void Unload()
		{
			Main.tileCut[231] = UnbreakableQueenBeeLarva.oldBreak;
		}

		// Token: 0x04000584 RID: 1412
		private static bool oldBreak;
	}
}
