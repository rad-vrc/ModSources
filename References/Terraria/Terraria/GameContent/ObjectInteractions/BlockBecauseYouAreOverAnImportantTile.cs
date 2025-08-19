using System;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x02000263 RID: 611
	public class BlockBecauseYouAreOverAnImportantTile : ISmartInteractBlockReasonProvider
	{
		// Token: 0x06001F8D RID: 8077 RVA: 0x005153BC File Offset: 0x005135BC
		public bool ShouldBlockSmartInteract(SmartInteractScanSettings settings)
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
				ushort type = tile.type;
				if (type <= 410)
				{
					if (type <= 33)
					{
						if (type != 4 && type != 33)
						{
							return false;
						}
					}
					else if (type != 334 && type != 395 && type != 410)
					{
						return false;
					}
				}
				else if (type <= 480)
				{
					if (type != 455 && type != 471 && type != 480)
					{
						return false;
					}
				}
				else if (type != 509 && type != 520 && type - 657 > 1)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
