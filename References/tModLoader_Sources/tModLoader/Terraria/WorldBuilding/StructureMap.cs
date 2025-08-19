using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.WorldBuilding
{
	/// <summary>
	/// Used during world generation to track and check important world generation features to prevent overlap. Access this via <see cref="F:Terraria.WorldBuilding.GenVars.structures" />.
	/// <para /> The <see cref="M:Terraria.WorldBuilding.StructureMap.AddProtectedStructure(Microsoft.Xna.Framework.Rectangle,System.Int32)" /> method is used to indicate an area of the world to be protected from other world generation code.
	/// <para /> The <see cref="M:Terraria.WorldBuilding.StructureMap.CanPlace(Microsoft.Xna.Framework.Rectangle,System.Int32)" /> method will check if the designated area is free from protected structures as well as any tiles that are not <see cref="F:Terraria.ID.TileID.Sets.GeneralPlacementTiles" />. This is used to find an area that is unused.
	/// <para /> It is up to modders to properly use StructureMap to both mark areas as occupied by structures and check for areas free of existing structures.
	/// <para /> The <see href="https://github.com/tModLoader/tModLoader/wiki/World-Generation#structuremap">Structure Map section of the World Generation wiki page</see> has more information and useful visualizations.
	/// </summary>
	// Token: 0x02000083 RID: 131
	public class StructureMap
	{
		/// <summary>
		/// Checks if the provided tile coordinate <paramref name="area" /> padded by the <paramref name="padding" /> is free from protected structures and tiles that are not <see cref="F:Terraria.ID.TileID.Sets.GeneralPlacementTiles" />. Use this to find a suitable place for placing a structure feature free from any existing structures or protected tile, then use <see cref="M:Terraria.WorldBuilding.StructureMap.AddProtectedStructure(Microsoft.Xna.Framework.Rectangle,System.Int32)" /> after placing a structure.
		/// </summary>
		// Token: 0x0600140D RID: 5133 RVA: 0x004A0013 File Offset: 0x0049E213
		public bool CanPlace(Rectangle area, int padding = 0)
		{
			return this.CanPlace(area, TileID.Sets.GeneralPlacementTiles, padding);
		}

		/// <summary>
		/// Similar to <see cref="M:Terraria.WorldBuilding.StructureMap.CanPlace(Microsoft.Xna.Framework.Rectangle,System.Int32)" />, except instead of using <see cref="F:Terraria.ID.TileID.Sets.GeneralPlacementTiles" /> the set of valid tiles to overwrite are passed in. Typically this is used with a copy of <c>GeneralPlacementTiles</c> that is changed slightly to allow or disallow some specific tiles.
		/// </summary>
		// Token: 0x0600140E RID: 5134 RVA: 0x004A0024 File Offset: 0x0049E224
		public unsafe bool CanPlace(Rectangle area, bool[] validTiles, int padding = 0)
		{
			object @lock = this._lock;
			bool result;
			lock (@lock)
			{
				if (area.X < 0 || area.Y < 0 || area.X + area.Width > Main.maxTilesX - 1 || area.Y + area.Height > Main.maxTilesY - 1)
				{
					result = false;
				}
				else
				{
					Rectangle rectangle;
					rectangle..ctor(area.X - padding, area.Y - padding, area.Width + padding * 2, area.Height + padding * 2);
					for (int i = 0; i < this._protectedStructures.Count; i++)
					{
						if (rectangle.Intersects(this._protectedStructures[i]))
						{
							return false;
						}
					}
					for (int j = rectangle.X; j < rectangle.X + rectangle.Width; j++)
					{
						for (int k = rectangle.Y; k < rectangle.Y + rectangle.Height; k++)
						{
							if (Main.tile[j, k].active())
							{
								ushort type = *Main.tile[j, k].type;
								if (!validTiles[(int)type])
								{
									return false;
								}
							}
						}
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x004A0194 File Offset: 0x0049E394
		public Rectangle GetBoundingBox()
		{
			object @lock = this._lock;
			Rectangle result;
			lock (@lock)
			{
				if (this._structures.Count == 0)
				{
					result = Rectangle.Empty;
				}
				else
				{
					Point point;
					point..ctor(this._structures.Min((Rectangle rect) => rect.Left), this._structures.Min((Rectangle rect) => rect.Top));
					Point point2;
					point2..ctor(this._structures.Max((Rectangle rect) => rect.Right), this._structures.Max((Rectangle rect) => rect.Bottom));
					result = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
				}
			}
			return result;
		}

		/// <summary> Unused, use <see cref="M:Terraria.WorldBuilding.StructureMap.AddProtectedStructure(Microsoft.Xna.Framework.Rectangle,System.Int32)" /> instead. </summary>
		// Token: 0x06001410 RID: 5136 RVA: 0x004A02D8 File Offset: 0x0049E4D8
		public void AddStructure(Rectangle area, int padding = 0)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				area.Inflate(padding, padding);
				this._structures.Add(area);
			}
		}

		/// <summary>
		/// Adds an area of the world to the protected structures listing to be protected from other world generation code. Use this after placing a structure if you do not wish other world generation code to overlap with it.
		/// </summary>
		// Token: 0x06001411 RID: 5137 RVA: 0x004A0328 File Offset: 0x0049E528
		public void AddProtectedStructure(Rectangle area, int padding = 0)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				area.Inflate(padding, padding);
				this._structures.Add(area);
				this._protectedStructures.Add(area);
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x004A0384 File Offset: 0x0049E584
		public void Reset()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._protectedStructures.Clear();
			}
		}

		// Token: 0x04001093 RID: 4243
		private readonly List<Rectangle> _structures = new List<Rectangle>(2048);

		// Token: 0x04001094 RID: 4244
		private readonly List<Rectangle> _protectedStructures = new List<Rectangle>(2048);

		// Token: 0x04001095 RID: 4245
		private readonly object _lock = new object();
	}
}
