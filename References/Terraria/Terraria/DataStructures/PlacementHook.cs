using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200045E RID: 1118
	public struct PlacementHook
	{
		// Token: 0x06002CCF RID: 11471 RVA: 0x005BC0F9 File Offset: 0x005BA2F9
		public PlacementHook(Func<int, int, int, int, int, int, int> hook, int badReturn, int badResponse, bool processedCoordinates)
		{
			this.hook = hook;
			this.badResponse = badResponse;
			this.badReturn = badReturn;
			this.processedCoordinates = processedCoordinates;
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x005BC118 File Offset: 0x005BA318
		public static bool operator ==(PlacementHook first, PlacementHook second)
		{
			return first.hook == second.hook && first.badResponse == second.badResponse && first.badReturn == second.badReturn && first.processedCoordinates == second.processedCoordinates;
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x005BC164 File Offset: 0x005BA364
		public static bool operator !=(PlacementHook first, PlacementHook second)
		{
			return first.hook != second.hook || first.badResponse != second.badResponse || first.badReturn != second.badReturn || first.processedCoordinates != second.processedCoordinates;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x005BC1B3 File Offset: 0x005BA3B3
		public override bool Equals(object obj)
		{
			return obj is PlacementHook && this == (PlacementHook)obj;
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x005BC1D0 File Offset: 0x005BA3D0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04005114 RID: 20756
		public Func<int, int, int, int, int, int, int> hook;

		// Token: 0x04005115 RID: 20757
		public int badReturn;

		// Token: 0x04005116 RID: 20758
		public int badResponse;

		// Token: 0x04005117 RID: 20759
		public bool processedCoordinates;

		// Token: 0x04005118 RID: 20760
		public static PlacementHook Empty = new PlacementHook(null, 0, 0, false);

		// Token: 0x04005119 RID: 20761
		public const int Response_AllInvalid = 0;
	}
}
