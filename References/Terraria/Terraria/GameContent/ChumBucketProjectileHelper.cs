using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x020001CE RID: 462
	public class ChumBucketProjectileHelper
	{
		// Token: 0x06001BE6 RID: 7142 RVA: 0x004F09AD File Offset: 0x004EEBAD
		public void OnPreUpdateAllProjectiles()
		{
			Utils.Swap<Dictionary<Point, int>>(ref this._chumCountsPendingForThisFrame, ref this._chumCountsFromLastFrame);
			this._chumCountsPendingForThisFrame.Clear();
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x004F09CC File Offset: 0x004EEBCC
		public void AddChumLocation(Vector2 spot)
		{
			Point key = spot.ToTileCoordinates();
			int num = 0;
			this._chumCountsPendingForThisFrame.TryGetValue(key, out num);
			num++;
			this._chumCountsPendingForThisFrame[key] = num;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x004F0A04 File Offset: 0x004EEC04
		public int GetChumsInLocation(Point tileCoords)
		{
			int result = 0;
			this._chumCountsFromLastFrame.TryGetValue(tileCoords, out result);
			return result;
		}

		// Token: 0x04004353 RID: 17235
		private Dictionary<Point, int> _chumCountsPendingForThisFrame = new Dictionary<Point, int>();

		// Token: 0x04004354 RID: 17236
		private Dictionary<Point, int> _chumCountsFromLastFrame = new Dictionary<Point, int>();
	}
}
