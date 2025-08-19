﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
	// Token: 0x020002E5 RID: 741
	public class JungleHouseBuilder : HouseBuilder
	{
		// Token: 0x06002352 RID: 9042 RVA: 0x0054E03C File Offset: 0x0054C23C
		public JungleHouseBuilder(IEnumerable<Rectangle> rooms) : base(HouseType.Jungle, rooms)
		{
			base.TileType = 158;
			base.WallType = 42;
			base.BeamType = 575;
			base.PlatformStyle = 2;
			base.DoorStyle = 2;
			base.TableStyle = 2;
			base.WorkbenchStyle = 2;
			base.PianoStyle = 2;
			base.BookcaseStyle = 12;
			base.ChairStyle = 3;
			base.ChestStyle = 8;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x0054E0A8 File Offset: 0x0054C2A8
		protected override void AgeRoom(Rectangle room)
		{
			WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.6),
				new Modifiers.Blotches(2, 0.6),
				new Modifiers.OnlyTiles(new ushort[]
				{
					base.TileType
				}),
				new Actions.SetTileKeepWall(60, true, true),
				new Modifiers.Dither(0.8),
				new Actions.SetTileKeepWall(59, true, true)
			}));
			WorldUtils.Gen(new Point(room.X + 1, room.Y), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.5),
				new Modifiers.OnlyTiles(new ushort[]
				{
					60
				}),
				new Modifiers.Offset(0, 1),
				new Modifiers.IsEmpty(),
				new ActionVines(3, room.Height, 62)
			}));
			WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.5),
				new Modifiers.OnlyTiles(new ushort[]
				{
					60
				}),
				new Modifiers.Offset(0, 1),
				new Modifiers.IsEmpty(),
				new ActionVines(3, room.Height, 62)
			}));
			WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.85),
				new Modifiers.Blotches(2, 0.3),
				new Actions.PlaceWall(64, true)
			}));
		}
	}
}
