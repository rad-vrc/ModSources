using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when an entity interacts with a tile. <br />
	/// Used by vanilla when players activate mechanisms like cannons and fireworks
	/// </summary>
	// Token: 0x02000703 RID: 1795
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_TileInteraction : AEntitySource_Tile
	{
		/// <summary>
		/// The entity interacting with the tile. <br />
		/// Normally a <see cref="T:Terraria.Player" />
		/// </summary>
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x0064DA33 File Offset: 0x0064BC33
		public Entity Entity { get; }

		// Token: 0x0600498C RID: 18828 RVA: 0x0064DA3B File Offset: 0x0064BC3B
		public EntitySource_TileInteraction(Entity entity, int tileCoordsX, int tileCoordsY, [Nullable(2)] string context = null) : base(tileCoordsX, tileCoordsY, context)
		{
			this.Entity = entity;
		}
	}
}
