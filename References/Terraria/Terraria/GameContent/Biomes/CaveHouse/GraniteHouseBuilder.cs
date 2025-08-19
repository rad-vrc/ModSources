using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
	// Token: 0x020002E0 RID: 736
	public class GraniteHouseBuilder : HouseBuilder
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x0054C430 File Offset: 0x0054A630
		public GraniteHouseBuilder(IEnumerable<Rectangle> rooms) : base(HouseType.Granite, rooms)
		{
			base.TileType = 369;
			base.WallType = 181;
			base.BeamType = 576;
			base.PlatformStyle = 28;
			base.DoorStyle = 34;
			base.TableStyle = 33;
			base.WorkbenchStyle = 29;
			base.PianoStyle = 28;
			base.BookcaseStyle = 30;
			base.ChairStyle = 34;
			base.ChestStyle = 50;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x0054C4A8 File Offset: 0x0054A6A8
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
				new Actions.SetTileKeepWall(368, true, true)
			}));
			WorldUtils.Gen(new Point(room.X + 1, room.Y), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.8),
				new Modifiers.OnlyTiles(new ushort[]
				{
					368
				}),
				new Modifiers.Offset(0, 1),
				new ActionStalagtite()
			}));
			WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.8),
				new Modifiers.OnlyTiles(new ushort[]
				{
					368
				}),
				new Modifiers.Offset(0, 1),
				new ActionStalagtite()
			}));
			WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.85),
				new Modifiers.Blotches(2, 0.3),
				new Actions.PlaceWall(180, true)
			}));
		}
	}
}
