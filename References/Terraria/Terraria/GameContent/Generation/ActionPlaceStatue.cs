using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x020003DD RID: 989
	public class ActionPlaceStatue : GenAction
	{
		// Token: 0x06002AAA RID: 10922 RVA: 0x0059AB17 File Offset: 0x00598D17
		public ActionPlaceStatue(int index = -1)
		{
			this._statueIndex = index;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0059AB28 File Offset: 0x00598D28
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			Point16 point;
			if (this._statueIndex == -1)
			{
				point = GenVars.statueList[GenBase._random.Next(2, GenVars.statueList.Length)];
			}
			else
			{
				point = GenVars.statueList[this._statueIndex];
			}
			WorldGen.PlaceTile(x, y, (int)point.X, true, false, -1, (int)point.Y);
			return base.UnitApply(origin, x, y, args);
		}

		// Token: 0x04004D53 RID: 19795
		private int _statueIndex;
	}
}
