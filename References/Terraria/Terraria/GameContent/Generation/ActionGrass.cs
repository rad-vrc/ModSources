using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x020003DC RID: 988
	public class ActionGrass : GenAction
	{
		// Token: 0x06002AA8 RID: 10920 RVA: 0x0059AAA8 File Offset: 0x00598CA8
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			if (GenBase._tiles[x, y].active() || GenBase._tiles[x, y - 1].active())
			{
				return false;
			}
			WorldGen.PlaceTile(x, y, (int)Utils.SelectRandom<ushort>(GenBase._random, new ushort[]
			{
				3,
				73
			}), true, false, -1, 0);
			return base.UnitApply(origin, x, y, args);
		}
	}
}
