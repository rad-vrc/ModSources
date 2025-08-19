using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x020003DF RID: 991
	public class ActionVines : GenAction
	{
		// Token: 0x06002AAE RID: 10926 RVA: 0x0059ABA5 File Offset: 0x00598DA5
		public ActionVines(int minLength = 6, int maxLength = 10, int vineId = 52)
		{
			this._minLength = minLength;
			this._maxLength = maxLength;
			this._vineId = vineId;
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x0059ABC4 File Offset: 0x00598DC4
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			int num = GenBase._random.Next(this._minLength, this._maxLength + 1);
			int num2 = 0;
			while (num2 < num && !GenBase._tiles[x, y + num2].active())
			{
				GenBase._tiles[x, y + num2].type = (ushort)this._vineId;
				GenBase._tiles[x, y + num2].active(true);
				num2++;
			}
			return num2 > 0 && base.UnitApply(origin, x, y, args);
		}

		// Token: 0x04004D54 RID: 19796
		private int _minLength;

		// Token: 0x04004D55 RID: 19797
		private int _maxLength;

		// Token: 0x04004D56 RID: 19798
		private int _vineId;
	}
}
