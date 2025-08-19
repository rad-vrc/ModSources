using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000074 RID: 116
	public class StructureMap
	{
		// Token: 0x06001169 RID: 4457 RVA: 0x0048D1D8 File Offset: 0x0048B3D8
		public bool CanPlace(Rectangle area, int padding = 0)
		{
			return this.CanPlace(area, TileID.Sets.GeneralPlacementTiles, padding);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0048D1E8 File Offset: 0x0048B3E8
		public bool CanPlace(Rectangle area, bool[] validTiles, int padding = 0)
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
					Rectangle rectangle = new Rectangle(area.X - padding, area.Y - padding, area.Width + padding * 2, area.Height + padding * 2);
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
								ushort type = Main.tile[j, k].type;
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

		// Token: 0x0600116B RID: 4459 RVA: 0x0048D350 File Offset: 0x0048B550
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
					Point point = new Point(this._structures.Min((Rectangle rect) => rect.Left), this._structures.Min((Rectangle rect) => rect.Top));
					Point point2 = new Point(this._structures.Max((Rectangle rect) => rect.Right), this._structures.Max((Rectangle rect) => rect.Bottom));
					result = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
				}
			}
			return result;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0048D494 File Offset: 0x0048B694
		public void AddStructure(Rectangle area, int padding = 0)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				area.Inflate(padding, padding);
				this._structures.Add(area);
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0048D4E4 File Offset: 0x0048B6E4
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

		// Token: 0x0600116E RID: 4462 RVA: 0x0048D540 File Offset: 0x0048B740
		public void Reset()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._protectedStructures.Clear();
			}
		}

		// Token: 0x04000FB6 RID: 4022
		private readonly List<Rectangle> _structures = new List<Rectangle>(2048);

		// Token: 0x04000FB7 RID: 4023
		private readonly List<Rectangle> _protectedStructures = new List<Rectangle>(2048);

		// Token: 0x04000FB8 RID: 4024
		private readonly object _lock = new object();
	}
}
