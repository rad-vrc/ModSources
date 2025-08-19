using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x020002CD RID: 717
	public class CampsiteBiome : MicroBiome
	{
		// Token: 0x060022BF RID: 8895 RVA: 0x00547D84 File Offset: 0x00545F84
		public override bool Place(Point origin, StructureMap structures)
		{
			Ref<int> @ref = new Ref<int>(0);
			Ref<int> ref2 = new Ref<int>(0);
			WorldUtils.Gen(origin, new Shapes.Circle(10), Actions.Chain(new GenAction[]
			{
				new Actions.Scanner(ref2),
				new Modifiers.IsSolid(),
				new Actions.Scanner(@ref)
			}));
			if (@ref.Value < ref2.Value - 5)
			{
				return false;
			}
			int num = GenBase._random.Next(6, 10);
			int num2 = GenBase._random.Next(1, 5);
			if (!structures.CanPlace(new Rectangle(origin.X - num, origin.Y - num, num * 2, num * 2), 0))
			{
				return false;
			}
			ushort type = (ushort)((byte)(196 + WorldGen.genRand.Next(4)));
			for (int i = origin.X - num; i <= origin.X + num; i++)
			{
				for (int j = origin.Y - num; j <= origin.Y + num; j++)
				{
					if (Main.tile[i, j].active())
					{
						int type2 = (int)Main.tile[i, j].type;
						if (type2 == 53 || type2 == 396 || type2 == 397 || type2 == 404)
						{
							type = 171;
						}
						if (type2 == 161 || type2 == 147)
						{
							type = 40;
						}
						if (type2 == 60)
						{
							type = (ushort)((byte)(204 + WorldGen.genRand.Next(4)));
						}
						if (type2 == 367)
						{
							type = 178;
						}
						if (type2 == 368)
						{
							type = 180;
						}
					}
				}
			}
			ShapeData data = new ShapeData();
			WorldUtils.Gen(origin, new Shapes.Slime(num), Actions.Chain(new GenAction[]
			{
				new Modifiers.Blotches(num2, num2, num2, 1, 1.0).Output(data),
				new Modifiers.Offset(0, -2),
				new Modifiers.OnlyTiles(new ushort[]
				{
					53
				}),
				new Actions.SetTile(397, true, true),
				new Modifiers.OnlyWalls(new ushort[1]),
				new Actions.PlaceWall(type, true)
			}));
			WorldUtils.Gen(origin, new ModShapes.All(data), Actions.Chain(new GenAction[]
			{
				new Actions.ClearTile(false),
				new Actions.SetLiquid(0, 0),
				new Actions.SetFrames(true),
				new Modifiers.OnlyWalls(new ushort[1]),
				new Actions.PlaceWall(type, true)
			}));
			Point point;
			if (!WorldUtils.Find(origin, Searches.Chain(new Searches.Down(10), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out point))
			{
				return false;
			}
			int num3 = point.Y - 1;
			bool flag = GenBase._random.Next() % 2 == 0;
			if (GenBase._random.Next() % 10 != 0)
			{
				int num4 = GenBase._random.Next(1, 4);
				int num5 = flag ? 4 : (-(num >> 1));
				for (int k = 0; k < num4; k++)
				{
					int num6 = GenBase._random.Next(1, 3);
					for (int l = 0; l < num6; l++)
					{
						WorldGen.PlaceTile(origin.X + num5 - k, num3 - l, 332, true, false, -1, 0);
					}
				}
			}
			int num7 = (num - 3) * (flag ? -1 : 1);
			if (GenBase._random.Next() % 10 != 0)
			{
				WorldGen.PlaceTile(origin.X + num7, num3, 186, false, false, -1, 0);
			}
			if (GenBase._random.Next() % 10 != 0)
			{
				WorldGen.PlaceTile(origin.X, num3, 215, true, false, -1, 0);
				if (GenBase._tiles[origin.X, num3].active() && GenBase._tiles[origin.X, num3].type == 215)
				{
					Tile tile = GenBase._tiles[origin.X, num3];
					tile.frameY += 36;
					Tile tile2 = GenBase._tiles[origin.X - 1, num3];
					tile2.frameY += 36;
					Tile tile3 = GenBase._tiles[origin.X + 1, num3];
					tile3.frameY += 36;
					Tile tile4 = GenBase._tiles[origin.X, num3 - 1];
					tile4.frameY += 36;
					Tile tile5 = GenBase._tiles[origin.X - 1, num3 - 1];
					tile5.frameY += 36;
					Tile tile6 = GenBase._tiles[origin.X + 1, num3 - 1];
					tile6.frameY += 36;
				}
			}
			structures.AddProtectedStructure(new Rectangle(origin.X - num, origin.Y - num, num * 2, num * 2), 4);
			return true;
		}
	}
}
