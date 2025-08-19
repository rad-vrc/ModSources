using System;
using Terraria.ID;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005C6 RID: 1478
	public class BlockBecauseYouAreOverAnImportantTile : ISmartInteractBlockReasonProvider
	{
		// Token: 0x060042CD RID: 17101 RVA: 0x005FA7FC File Offset: 0x005F89FC
		public unsafe bool ShouldBlockSmartInteract(SmartInteractScanSettings settings)
		{
			int tileTargetX = Player.tileTargetX;
			int tileTargetY = Player.tileTargetY;
			if (!WorldGen.InWorld(tileTargetX, tileTargetY, 10))
			{
				return true;
			}
			Tile tile = Main.tile[tileTargetX, tileTargetY];
			if (tile == null)
			{
				return true;
			}
			if (tile.active())
			{
				ref ushort type = ref tile.type;
				return TileID.Sets.DisableSmartInteract[(int)(*tile.type)];
			}
			return false;
		}
	}
}
