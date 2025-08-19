using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Golf
{
	// Token: 0x020002A2 RID: 674
	public class GolfBallTrackRecord
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x0051F368 File Offset: 0x0051D568
		public void RecordHit(Vector2 position)
		{
			this._hitLocations.Add(position);
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0051F378 File Offset: 0x0051D578
		public int GetAccumulatedScore()
		{
			double num;
			int num2;
			this.GetTrackInfo(out num, out num2);
			int num3 = (int)(num / 16.0);
			int num4 = num2 + 2;
			return num3 / num4;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0051F3A4 File Offset: 0x0051D5A4
		private void GetTrackInfo(out double totalDistancePassed, out int hitsMade)
		{
			hitsMade = 0;
			totalDistancePassed = 0.0;
			int i = 0;
			while (i < this._hitLocations.Count - 1)
			{
				totalDistancePassed += (double)Vector2.Distance(this._hitLocations[i], this._hitLocations[i + 1]);
				i++;
				hitsMade++;
			}
		}

		// Token: 0x04004701 RID: 18177
		private List<Vector2> _hitLocations = new List<Vector2>();
	}
}
