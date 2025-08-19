using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Golf
{
	// Token: 0x0200061B RID: 1563
	public class GolfBallTrackRecord
	{
		// Token: 0x060044B9 RID: 17593 RVA: 0x0060B6B5 File Offset: 0x006098B5
		public void RecordHit(Vector2 position)
		{
			this._hitLocations.Add(position);
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x0060B6C4 File Offset: 0x006098C4
		public int GetAccumulatedScore()
		{
			double totalDistancePassed;
			int hitsMade;
			this.GetTrackInfo(out totalDistancePassed, out hitsMade);
			int num3 = (int)(totalDistancePassed / 16.0);
			int num2 = hitsMade + 2;
			return num3 / num2;
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x0060B6F0 File Offset: 0x006098F0
		private void GetTrackInfo(out double totalDistancePassed, out int hitsMade)
		{
			hitsMade = 0;
			totalDistancePassed = 0.0;
			int num = 0;
			while (num < this._hitLocations.Count - 1)
			{
				totalDistancePassed += (double)Vector2.Distance(this._hitLocations[num], this._hitLocations[num + 1]);
				num++;
				hitsMade++;
			}
		}

		// Token: 0x04005A9D RID: 23197
		private List<Vector2> _hitLocations = new List<Vector2>();
	}
}
