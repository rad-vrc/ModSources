using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.GameContent.Biomes.Desert;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000654 RID: 1620
	public class DesertBiome : MicroBiome
	{
		// Token: 0x060046D3 RID: 18131 RVA: 0x00634050 File Offset: 0x00632250
		public override bool Place(Point origin, StructureMap structures)
		{
			DesertDescription desertDescription = DesertDescription.CreateFromPlacement(origin);
			if (!desertDescription.IsValid)
			{
				return false;
			}
			DesertBiome.ExportDescriptionToEngine(desertDescription);
			SandMound.Place(desertDescription);
			desertDescription.UpdateSurfaceMap();
			if (!Main.tenthAnniversaryWorld && GenBase._random.NextDouble() <= this.ChanceOfEntrance)
			{
				switch (GenBase._random.Next(4))
				{
				case 0:
					ChambersEntrance.Place(desertDescription);
					break;
				case 1:
					AnthillEntrance.Place(desertDescription);
					break;
				case 2:
					LarvaHoleEntrance.Place(desertDescription);
					break;
				case 3:
					PitEntrance.Place(desertDescription);
					break;
				}
			}
			DesertHive.Place(desertDescription);
			DesertBiome.CleanupArea(desertDescription.Hive);
			Rectangle area;
			area..ctor(desertDescription.CombinedArea.X, 50, desertDescription.CombinedArea.Width, desertDescription.CombinedArea.Bottom - 20);
			structures.AddStructure(area, 10);
			return true;
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x00634126 File Offset: 0x00632326
		private static void ExportDescriptionToEngine(DesertDescription description)
		{
			GenVars.UndergroundDesertLocation = description.CombinedArea;
			GenVars.UndergroundDesertLocation.Inflate(10, 10);
			GenVars.UndergroundDesertHiveLocation = description.Hive;
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x0063414C File Offset: 0x0063234C
		private static void CleanupArea(Rectangle area)
		{
			for (int i = -20 + area.Left; i < area.Right + 20; i++)
			{
				for (int j = -20 + area.Top; j < area.Bottom + 20; j++)
				{
					if (i > 0 && i < Main.maxTilesX - 1 && j > 0 && j < Main.maxTilesY - 1)
					{
						WorldGen.SquareWallFrame(i, j, true);
						WorldUtils.TileFrame(i, j, true);
					}
				}
			}
		}

		// Token: 0x04005BA0 RID: 23456
		[JsonProperty("ChanceOfEntrance")]
		public double ChanceOfEntrance = 0.3333;
	}
}
