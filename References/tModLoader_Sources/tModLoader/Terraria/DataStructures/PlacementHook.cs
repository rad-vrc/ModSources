using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000722 RID: 1826
	public struct PlacementHook
	{
		/// <summary>
		/// Used by <see cref="T:Terraria.ObjectData.TileObjectData" /> to provide custom logic to tile placement.
		/// <para /> <paramref name="hook" />: The method to call. The return value will be compared against <see cref="F:Terraria.DataStructures.PlacementHook.badReturn" /> if relevant. The parameters are: X tile coordinate, Y tile coordinate, tile type, tile style, direction, and alternate index. Use these parameters to execute extra logic for this tile placement. <see cref="T:Terraria.ModLoader.ModTileEntity" /> has a few existing methods to use for various placement hooks.
		/// <para /> <paramref name="badReturn" />: Declares what return value from the <see cref="F:Terraria.DataStructures.PlacementHook.hook" /> should be interpreted as a failure. Typically -1.
		/// <para /> <paramref name="badResponse" />: Set to 0.
		/// <para /> <paramref name="processedCoordinates" />: If true, the top left corner coordinates of the tile will be passed into <see cref="F:Terraria.DataStructures.PlacementHook.hook" /> rather than the placement coordinate (the placement coordinate is where the mouse would be (<see cref="F:Terraria.Player.tileTargetX" /> and <see cref="F:Terraria.Player.tileTargetY" />), accounting for the <see cref="P:Terraria.ObjectData.TileObjectData.Origin" /> offset). Typically a true value will make all the logic in the <paramref name="hook" /> method much simpler.
		/// </summary>
		// Token: 0x060049FF RID: 18943 RVA: 0x0064EC0D File Offset: 0x0064CE0D
		public PlacementHook(Func<int, int, int, int, int, int, int> hook, int badReturn, int badResponse, bool processedCoordinates)
		{
			this.hook = hook;
			this.badResponse = badResponse;
			this.badReturn = badReturn;
			this.processedCoordinates = processedCoordinates;
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x0064EC2C File Offset: 0x0064CE2C
		public static bool operator ==(PlacementHook first, PlacementHook second)
		{
			return first.hook == second.hook && first.badResponse == second.badResponse && first.badReturn == second.badReturn && first.processedCoordinates == second.processedCoordinates;
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x0064EC78 File Offset: 0x0064CE78
		public static bool operator !=(PlacementHook first, PlacementHook second)
		{
			return first.hook != second.hook || first.badResponse != second.badResponse || first.badReturn != second.badReturn || first.processedCoordinates != second.processedCoordinates;
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x0064ECC7 File Offset: 0x0064CEC7
		public override bool Equals(object obj)
		{
			return obj is PlacementHook && this == (PlacementHook)obj;
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x0064ECE4 File Offset: 0x0064CEE4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04005F23 RID: 24355
		public Func<int, int, int, int, int, int, int> hook;

		// Token: 0x04005F24 RID: 24356
		public int badReturn;

		// Token: 0x04005F25 RID: 24357
		public int badResponse;

		// Token: 0x04005F26 RID: 24358
		public bool processedCoordinates;

		// Token: 0x04005F27 RID: 24359
		public static PlacementHook Empty = new PlacementHook(null, 0, 0, false);

		// Token: 0x04005F28 RID: 24360
		public const int Response_AllInvalid = 0;
	}
}
