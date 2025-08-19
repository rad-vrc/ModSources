using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000650 RID: 1616
	public class CampsiteBiome : MicroBiome
	{
		// Token: 0x060046AA RID: 18090 RVA: 0x00632700 File Offset: 0x00630900
		public unsafe override bool Place(Point origin, StructureMap structures)
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
						int type2 = (int)(*Main.tile[i, j].type);
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
			Point result;
			if (!WorldUtils.Find(origin, Searches.Chain(new Searches.Down(10), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out result))
			{
				return false;
			}
			int num3 = result.Y - 1;
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
			int num7 = (num - 3) * ((!flag) ? 1 : -1);
			if (GenBase._random.Next() % 10 != 0)
			{
				WorldGen.PlaceTile(origin.X + num7, num3, 186, false, false, -1, 0);
			}
			if (GenBase._random.Next() % 10 != 0)
			{
				WorldGen.PlaceTile(origin.X, num3, 215, true, false, -1, 0);
				if (GenBase._tiles[origin.X, num3].active() && *GenBase._tiles[origin.X, num3].type == 215)
				{
					ref short frameY = ref GenBase._tiles[origin.X, num3].frameY;
					frameY += 36;
					ref short frameY2 = ref GenBase._tiles[origin.X - 1, num3].frameY;
					frameY2 += 36;
					ref short frameY3 = ref GenBase._tiles[origin.X + 1, num3].frameY;
					frameY3 += 36;
					ref short frameY4 = ref GenBase._tiles[origin.X, num3 - 1].frameY;
					frameY4 += 36;
					ref short frameY5 = ref GenBase._tiles[origin.X - 1, num3 - 1].frameY;
					frameY5 += 36;
					ref short frameY6 = ref GenBase._tiles[origin.X + 1, num3 - 1].frameY;
					frameY6 += 36;
				}
			}
			structures.AddProtectedStructure(new Rectangle(origin.X - num, origin.Y - num, num * 2, num * 2), 4);
			return true;
		}
	}
}
