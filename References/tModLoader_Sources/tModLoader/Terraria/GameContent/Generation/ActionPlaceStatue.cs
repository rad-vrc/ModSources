using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x0200061F RID: 1567
	public class ActionPlaceStatue : GenAction
	{
		// Token: 0x060044E1 RID: 17633 RVA: 0x0060C2D9 File Offset: 0x0060A4D9
		public ActionPlaceStatue(int index = -1)
		{
			this._statueIndex = index;
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x0060C2E8 File Offset: 0x0060A4E8
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			Point16 point = (this._statueIndex != -1) ? GenVars.statueList[this._statueIndex] : GenVars.statueList[GenBase._random.Next(2, GenVars.statueList.Length)];
			WorldGen.PlaceTile(x, y, (int)point.X, true, false, -1, (int)point.Y);
			return base.UnitApply(origin, x, y, args);
		}

		// Token: 0x04005AAF RID: 23215
		private int _statueIndex;
	}
}
