using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
	// Token: 0x020002E4 RID: 740
	public class IceHouseBuilder : HouseBuilder
	{
		// Token: 0x06002350 RID: 9040 RVA: 0x0054DDB4 File Offset: 0x0054BFB4
		public IceHouseBuilder(IEnumerable<Rectangle> rooms) : base(HouseType.Ice, rooms)
		{
			base.TileType = 321;
			base.WallType = 149;
			base.BeamType = 574;
			base.DoorStyle = 30;
			base.PlatformStyle = 19;
			base.TableStyle = 28;
			base.WorkbenchStyle = 23;
			base.PianoStyle = 23;
			base.BookcaseStyle = 25;
			base.ChairStyle = 30;
			base.ChestStyle = 11;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0054DE2C File Offset: 0x0054C02C
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
				new Actions.SetTileKeepWall(161, true, true),
				new Modifiers.Dither(0.8),
				new Actions.SetTileKeepWall(147, true, true)
			}));
			WorldUtils.Gen(new Point(room.X + 1, room.Y), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.5),
				new Modifiers.OnlyTiles(new ushort[]
				{
					161
				}),
				new Modifiers.Offset(0, 1),
				new ActionStalagtite()
			}));
			WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.5),
				new Modifiers.OnlyTiles(new ushort[]
				{
					161
				}),
				new Modifiers.Offset(0, 1),
				new ActionStalagtite()
			}));
			WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.85),
				new Modifiers.Blotches(2, 0.8),
				new Modifiers.SkipTiles(this.SkipTilesDuringWallAging),
				((double)room.Y > Main.worldSurface) ? new Actions.ClearWall(true) : new Actions.PlaceWall(40, true)
			}));
		}
	}
}
