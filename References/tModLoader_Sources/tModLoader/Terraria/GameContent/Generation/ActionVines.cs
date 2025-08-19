using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x02000621 RID: 1569
	public class ActionVines : GenAction
	{
		// Token: 0x060044E5 RID: 17637 RVA: 0x0060C36C File Offset: 0x0060A56C
		public ActionVines(int minLength = 6, int maxLength = 10, int vineId = 52)
		{
			this._minLength = minLength;
			this._maxLength = maxLength;
			this._vineId = vineId;
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x0060C38C File Offset: 0x0060A58C
		public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
		{
			int num = GenBase._random.Next(this._minLength, this._maxLength + 1);
			int i = 0;
			while (i < num && !GenBase._tiles[x, y + i].active())
			{
				*GenBase._tiles[x, y + i].type = (ushort)this._vineId;
				GenBase._tiles[x, y + i].active(true);
				i++;
			}
			return i > 0 && base.UnitApply(origin, x, y, args);
		}

		// Token: 0x04005AB0 RID: 23216
		private int _minLength;

		// Token: 0x04005AB1 RID: 23217
		private int _maxLength;

		// Token: 0x04005AB2 RID: 23218
		private int _vineId;
	}
}
