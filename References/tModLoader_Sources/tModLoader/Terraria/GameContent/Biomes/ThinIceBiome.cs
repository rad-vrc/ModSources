using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x0200065F RID: 1631
	public class ThinIceBiome : MicroBiome
	{
		// Token: 0x0600470D RID: 18189 RVA: 0x00639468 File Offset: 0x00637668
		public override bool Place(Point origin, StructureMap structures)
		{
			Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
			WorldUtils.Gen(new Point(origin.X - 25, origin.Y - 25), new Shapes.Rectangle(50, 50), new Actions.TileScanner(new ushort[]
			{
				0,
				59,
				147,
				1,
				225
			}).Output(dictionary));
			int num = dictionary[0] + dictionary[1];
			int num2 = dictionary[59];
			int num3 = dictionary[147];
			if (dictionary[225] > 0)
			{
				return false;
			}
			if (num3 <= num2 || num3 <= num)
			{
				return false;
			}
			int num4 = 0;
			for (int num5 = GenBase._random.Next(10, 15); num5 > 5; num5--)
			{
				int num6 = GenBase._random.Next(-5, 5);
				WorldUtils.Gen(new Point(origin.X + num6, origin.Y + num4), new Shapes.Circle(num5), Actions.Chain(new GenAction[]
				{
					new Modifiers.Blotches(4, 0.3),
					new Modifiers.OnlyTiles(new ushort[]
					{
						147,
						161,
						224,
						0,
						1
					}),
					new Actions.SetTile(162, true, true)
				}));
				WorldUtils.Gen(new Point(origin.X + num6, origin.Y + num4), new Shapes.Circle(num5), Actions.Chain(new GenAction[]
				{
					new Modifiers.Blotches(4, 0.3),
					new Modifiers.HasLiquid(-1, -1),
					new Modifiers.SkipTiles(new ushort[]
					{
						21,
						467,
						226,
						237
					}),
					new Actions.SetTile(162, true, true),
					new Actions.SetLiquid(0, 0)
				}));
				num4 += num5 - 2;
			}
			structures.AddStructure(new Rectangle(origin.X - 25, origin.Y - 25, 50, 50), 8);
			return true;
		}
	}
}
