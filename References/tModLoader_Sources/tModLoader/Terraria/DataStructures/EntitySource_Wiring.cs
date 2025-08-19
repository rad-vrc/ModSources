using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when wiring activates an effect like a cannon or fireworks
	/// </summary>
	// Token: 0x02000705 RID: 1797
	public class EntitySource_Wiring : AEntitySource_Tile
	{
		// Token: 0x0600498E RID: 18830 RVA: 0x0064DA59 File Offset: 0x0064BC59
		[NullableContext(2)]
		public EntitySource_Wiring(int tileCoordsX, int tileCoordsY, string context = null) : base(tileCoordsX, tileCoordsY, context)
		{
		}
	}
}
