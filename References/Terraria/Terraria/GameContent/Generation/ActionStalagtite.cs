using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x020003DE RID: 990
	public class ActionStalagtite : GenAction
	{
		// Token: 0x06002AAC RID: 10924 RVA: 0x0059AB90 File Offset: 0x00598D90
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			WorldGen.PlaceTight(x, y, false);
			return base.UnitApply(origin, x, y, args);
		}
	}
}
