﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
	// Token: 0x020002DF RID: 735
	public class DesertHouseBuilder : HouseBuilder
	{
		// Token: 0x06002315 RID: 8981 RVA: 0x0054C198 File Offset: 0x0054A398
		public DesertHouseBuilder(IEnumerable<Rectangle> rooms) : base(HouseType.Desert, rooms)
		{
			base.TileType = 396;
			base.WallType = 187;
			base.BeamType = 577;
			base.PlatformStyle = 42;
			base.DoorStyle = 43;
			base.TableStyle = 7;
			base.UsesTables2 = true;
			base.WorkbenchStyle = 39;
			base.PianoStyle = 38;
			base.BookcaseStyle = 39;
			base.ChairStyle = 43;
			base.ChestStyle = 10;
			base.UsesContainers2 = true;
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x0054C21C File Offset: 0x0054A41C
		protected override void AgeRoom(Rectangle room)
		{
			WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.8),
				new Modifiers.Blotches(2, 0.2),
				new Modifiers.OnlyTiles(new ushort[]
				{
					base.TileType
				}),
				new Actions.SetTileKeepWall(396, true, true),
				new Modifiers.Dither(0.5),
				new Actions.SetTileKeepWall(397, true, true)
			}));
			WorldUtils.Gen(new Point(room.X + 1, room.Y), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.5),
				new Modifiers.OnlyTiles(new ushort[]
				{
					397,
					396
				}),
				new Modifiers.Offset(0, 1),
				new ActionStalagtite()
			}));
			WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.5),
				new Modifiers.OnlyTiles(new ushort[]
				{
					397,
					396
				}),
				new Modifiers.Offset(0, 1),
				new ActionStalagtite()
			}));
			WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.8),
				new Modifiers.Blotches(2, 0.3),
				new Modifiers.OnlyWalls(new ushort[]
				{
					base.WallType
				}),
				new Actions.PlaceWall(216, true)
			}));
		}
	}
}
