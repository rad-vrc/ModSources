using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
	// Token: 0x02000670 RID: 1648
	public class MarbleHouseBuilder : HouseBuilder
	{
		// Token: 0x06004780 RID: 18304 RVA: 0x0063DC7C File Offset: 0x0063BE7C
		public MarbleHouseBuilder(IEnumerable<Rectangle> rooms) : base(HouseType.Marble, rooms)
		{
			base.TileType = 357;
			base.WallType = 179;
			base.BeamType = 561;
			base.PlatformStyle = 29;
			base.DoorStyle = 35;
			base.TableStyle = 34;
			base.WorkbenchStyle = 30;
			base.PianoStyle = 29;
			base.BookcaseStyle = 31;
			base.ChairStyle = 35;
			base.ChestStyle = 51;
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x0063DCF4 File Offset: 0x0063BEF4
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
				new Actions.SetTileKeepWall(367, true, true)
			}));
			WorldUtils.Gen(new Point(room.X + 1, room.Y), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.8),
				new Modifiers.OnlyTiles(new ushort[]
				{
					367
				}),
				new Modifiers.Offset(0, 1),
				new ActionStalagtite()
			}));
			WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.8),
				new Modifiers.OnlyTiles(new ushort[]
				{
					367
				}),
				new Modifiers.Offset(0, 1),
				new ActionStalagtite()
			}));
			WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
			{
				new Modifiers.Dither(0.85),
				new Modifiers.Blotches(2, 0.3),
				new Actions.PlaceWall(178, true)
			}));
		}
	}
}
