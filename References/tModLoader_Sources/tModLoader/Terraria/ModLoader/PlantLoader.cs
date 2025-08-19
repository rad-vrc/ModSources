using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using log4net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Enums;

namespace Terraria.ModLoader
{
	// Token: 0x020001BD RID: 445
	public static class PlantLoader
	{
		// Token: 0x06002287 RID: 8839 RVA: 0x004E7D74 File Offset: 0x004E5F74
		internal static void FinishSetup()
		{
			foreach (IPlant plant in PlantLoader.plantList)
			{
				plant.SetStaticDefaults();
				for (int i = 0; i < plant.GrowsOnTileId.Length; i++)
				{
					Vector2 id;
					id..ctor((float)plant.PlantTileId, (float)plant.GrowsOnTileId[i]);
					IPlant existing;
					if (PlantLoader.plantLookup.TryGetValue(id, out existing))
					{
						ILog tML = Logging.tML;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(70, 2);
						defaultInterpolatedStringHandler.AppendLiteral("The new plant ");
						defaultInterpolatedStringHandler.AppendFormatted<Type>(plant.GetType());
						defaultInterpolatedStringHandler.AppendLiteral(" conflicts with the existing plant ");
						defaultInterpolatedStringHandler.AppendFormatted<Type>(existing.GetType());
						defaultInterpolatedStringHandler.AppendLiteral(". New plant not added");
						tML.Error(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					else
					{
						if (!PlantLoader.plantIdToStyleLimit.ContainsKey((int)id.X))
						{
							PlantLoader.plantIdToStyleLimit.Add((int)id.X, plant.VanillaCount);
						}
						PlantLoader.plantLookup.Add(id, plant);
					}
				}
			}
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x004E7E9C File Offset: 0x004E609C
		internal static void UnloadPlants()
		{
			PlantLoader.plantList.Clear();
			PlantLoader.plantLookup.Clear();
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x004E7EB4 File Offset: 0x004E60B4
		public static T Get<T>(int plantTileID, int growsOnTileID) where T : IPlant
		{
			IPlant plant;
			if (!PlantLoader.plantLookup.TryGetValue(new Vector2((float)plantTileID, (float)growsOnTileID), out plant))
			{
				return default(T);
			}
			return (T)((object)plant);
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x004E7EE8 File Offset: 0x004E60E8
		public static bool Exists(int plantTileID, int growsOnTileID)
		{
			return PlantLoader.plantLookup.ContainsKey(new Vector2((float)plantTileID, (float)growsOnTileID));
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x004E7F00 File Offset: 0x004E6100
		public static Asset<Texture2D> GetCactusFruitTexture(int type)
		{
			ModCactus tree = PlantLoader.Get<ModCactus>(80, type);
			if (tree == null)
			{
				return null;
			}
			return tree.GetFruitTexture();
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x004E7F24 File Offset: 0x004E6124
		public static Asset<Texture2D> GetTexture(int plantId, int tileType)
		{
			IPlant plant = PlantLoader.Get<IPlant>(plantId, tileType);
			if (plant == null)
			{
				return null;
			}
			return plant.GetTexture();
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x004E7F44 File Offset: 0x004E6144
		public static ITree GetTree(int type)
		{
			ModTree tree = PlantLoader.Get<ModTree>(5, type);
			if (tree != null)
			{
				return tree;
			}
			ModPalmTree palm = PlantLoader.Get<ModPalmTree>(323, type);
			if (palm != null)
			{
				return palm;
			}
			return null;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x004E7F70 File Offset: 0x004E6170
		public static TreeTypes GetModTreeType(int type)
		{
			ITree tree = PlantLoader.GetTree(type);
			if (tree == null)
			{
				return TreeTypes.None;
			}
			return tree.CountsAsTreeType;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x004E7F83 File Offset: 0x004E6183
		public static bool ShakeTree(int x, int y, int type, ref bool createLeaves)
		{
			ITree tree = PlantLoader.GetTree(type);
			return tree == null || tree.Shake(x, y, ref createLeaves);
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x004E7F9C File Offset: 0x004E619C
		public static void GetTreeLeaf(int type, ref int leafGoreType)
		{
			ITree tree = PlantLoader.GetTree(type);
			if (tree != null)
			{
				leafGoreType = tree.TreeLeaf();
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x004E7FBC File Offset: 0x004E61BC
		public unsafe static void CheckAndInjectModSapling(int x, int y, ref int tileToCreate, ref int previewPlaceStyle)
		{
			if (tileToCreate == 20)
			{
				Tile soil = Main.tile[x, y + 1];
				if (soil.active())
				{
					TileLoader.SaplingGrowthType((int)(*soil.type), ref tileToCreate, ref previewPlaceStyle);
				}
			}
		}

		// Token: 0x0400170D RID: 5901
		internal static Dictionary<Vector2, IPlant> plantLookup = new Dictionary<Vector2, IPlant>();

		// Token: 0x0400170E RID: 5902
		internal static List<IPlant> plantList = new List<IPlant>();

		// Token: 0x0400170F RID: 5903
		internal static Dictionary<int, int> plantIdToStyleLimit = new Dictionary<int, int>();
	}
}
