using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
	// Token: 0x02000672 RID: 1650
	public class WoodHouseBuilder : HouseBuilder
	{
		// Token: 0x06004784 RID: 18308 RVA: 0x0063E10C File Offset: 0x0063C30C
		public WoodHouseBuilder(IEnumerable<Rectangle> rooms) : base(HouseType.Wood, rooms)
		{
			base.TileType = 30;
			base.WallType = 27;
			base.BeamType = 124;
			if (Main.tenthAnniversaryWorld)
			{
				if (Main.getGoodWorld)
				{
					if (WorldGen.genRand.Next(7) == 0)
					{
						base.TileType = 160;
						base.WallType = 44;
					}
				}
				else if (WorldGen.genRand.Next(2) == 0)
				{
					base.TileType = 160;
					base.WallType = 44;
				}
			}
			base.PlatformStyle = 0;
			base.DoorStyle = 0;
			base.TableStyle = 0;
			base.WorkbenchStyle = 0;
			base.PianoStyle = 0;
			base.BookcaseStyle = 0;
			base.ChairStyle = 0;
			base.ChestStyle = 1;
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x0063E1C4 File Offset: 0x0063C3C4
		protected override void AgeRoom(Rectangle room)
		{
			for (int i = 0; i < room.Width * room.Height / 16; i++)
			{
				int num = WorldGen.genRand.Next(1, room.Width - 1) + room.X;
				int y = WorldGen.genRand.Next(1, room.Height - 1) + room.Y;
				WorldUtils.Gen(new Point(num, y), new Shapes.Rectangle(2, 2), Actions.Chain(new GenAction[]
				{
					new Modifiers.Dither(0.5),
					new Modifiers.Blotches(2, 2, 0.3),
					new Modifiers.IsEmpty(),
					new Actions.SetTile(51, true, true)
				}));
			}
			WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.85),
				new Modifiers.Blotches(2, 0.3),
				new Modifiers.OnlyWalls(new ushort[]
				{
					base.WallType
				}),
				new Modifiers.SkipTiles(this.SkipTilesDuringWallAging),
				((double)room.Y > Main.worldSurface) ? new Actions.ClearWall(true) : new Actions.PlaceWall(2, true)
			}));
			WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.95),
				new Modifiers.OnlyTiles(new ushort[]
				{
					30,
					321,
					158
				}),
				new Actions.ClearTile(true)
			}));
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x0063E37E File Offset: 0x0063C57E
		public override void Place(HouseBuilderContext context, StructureMap structures)
		{
			base.Place(context, structures);
			this.RainbowifyOnTenthAnniversaryWorlds();
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x0063E390 File Offset: 0x0063C590
		private void RainbowifyOnTenthAnniversaryWorlds()
		{
			if (!Main.tenthAnniversaryWorld || (base.TileType == 160 && WorldGen.genRand.Next(2) == 0))
			{
				return;
			}
			foreach (Rectangle room in base.Rooms)
			{
				WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), new Actions.SetTileAndWallRainbowPaint());
			}
		}
	}
}
