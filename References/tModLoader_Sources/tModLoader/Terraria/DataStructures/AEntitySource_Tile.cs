using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Abstract base class for entities which come from a tile. <br /><br />
	///
	/// If the entity comes from a Player/NPC/Projectile, and a tile is only incidentally involved, consider making your own subclass of <see cref="T:Terraria.DataStructures.EntitySource_Parent" /> instead
	/// </summary>
	// Token: 0x020006CE RID: 1742
	[NullableContext(2)]
	[Nullable(0)]
	public class AEntitySource_Tile : IEntitySource
	{
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060048F5 RID: 18677 RVA: 0x0064C1DC File Offset: 0x0064A3DC
		public Point TileCoords { get; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x0064C1E4 File Offset: 0x0064A3E4
		public string Context { get; }

		// Token: 0x060048F7 RID: 18679 RVA: 0x0064C1EC File Offset: 0x0064A3EC
		public AEntitySource_Tile(int tileCoordsX, int tileCoordsY, string context)
		{
			this.TileCoords = new Point(tileCoordsX, tileCoordsY);
			this.Context = context;
		}
	}
}
