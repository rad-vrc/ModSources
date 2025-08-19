using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x02000620 RID: 1568
	public class ActionStalagtite : GenAction
	{
		// Token: 0x060044E3 RID: 17635 RVA: 0x0060C34F File Offset: 0x0060A54F
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			WorldGen.PlaceTight(x, y, false);
			return base.UnitApply(origin, x, y, args);
		}
	}
}
