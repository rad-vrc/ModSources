using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Determines the styles of <see cref="F:Terraria.ID.TileID.Plants" /> that an <see cref="T:Terraria.Item" /> creates on certain types of grass.
	/// </summary>
	// Token: 0x0200070C RID: 1804
	public class FlowerPacketInfo
	{
		/// <summary>
		/// The tile styles created on pure grass or in planters (<see cref="F:Terraria.ID.TileID.Grass" />, <see cref="F:Terraria.ID.TileID.ClayPot" />, <see cref="F:Terraria.ID.TileID.PlanterBox" />, <see cref="F:Terraria.ID.TileID.GolfGrass" />, <see cref="F:Terraria.ID.TileID.RockGolemHead" />).
		/// </summary>
		// Token: 0x04005EF0 RID: 24304
		public List<int> stylesOnPurity = new List<int>();

		/// <summary>
		/// <strong>Unused.</strong>
		/// </summary>
		// Token: 0x04005EF1 RID: 24305
		public List<int> stylesOnCorruption = new List<int>();

		/// <summary>
		/// <strong>Unused.</strong>
		/// </summary>
		// Token: 0x04005EF2 RID: 24306
		public List<int> stylesOnCrimson = new List<int>();

		/// <summary>
		/// <strong>Unused.</strong>
		/// </summary>
		// Token: 0x04005EF3 RID: 24307
		public List<int> stylesOnHallow = new List<int>();
	}
}
