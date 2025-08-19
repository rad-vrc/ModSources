using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x02000491 RID: 1169
	public class ChumBucketProjectileHelper
	{
		// Token: 0x060038F3 RID: 14579 RVA: 0x0059585B File Offset: 0x00593A5B
		public void OnPreUpdateAllProjectiles()
		{
			Utils.Swap<Dictionary<Point, int>>(ref this._chumCountsPendingForThisFrame, ref this._chumCountsFromLastFrame);
			this._chumCountsPendingForThisFrame.Clear();
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x0059587C File Offset: 0x00593A7C
		public void AddChumLocation(Vector2 spot)
		{
			Point key = spot.ToTileCoordinates();
			int value = 0;
			this._chumCountsPendingForThisFrame.TryGetValue(key, out value);
			value++;
			this._chumCountsPendingForThisFrame[key] = value;
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x005958B4 File Offset: 0x00593AB4
		public int GetChumsInLocation(Point tileCoords)
		{
			int value = 0;
			this._chumCountsFromLastFrame.TryGetValue(tileCoords, out value);
			return value;
		}

		// Token: 0x04005225 RID: 21029
		private Dictionary<Point, int> _chumCountsPendingForThisFrame = new Dictionary<Point, int>();

		// Token: 0x04005226 RID: 21030
		private Dictionary<Point, int> _chumCountsFromLastFrame = new Dictionary<Point, int>();
	}
}
