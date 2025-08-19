using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Terraria.ID;

namespace Terraria.GameContent.Metadata
{
	// Token: 0x020005DF RID: 1503
	public static class TileMaterials
	{
		// Token: 0x06004329 RID: 17193 RVA: 0x005FC6E4 File Offset: 0x005FA8E4
		static TileMaterials()
		{
			TileMaterials._materialsByName = TileMaterials.DeserializeEmbeddedResource<Dictionary<string, TileMaterial>>("Terraria.GameContent.Metadata.MaterialData.Materials.json");
			TileMaterial tileMaterial = TileMaterials._materialsByName["Default"];
			for (int i = 0; i < TileMaterials.MaterialsByTileId.Length; i++)
			{
				TileMaterials.MaterialsByTileId[i] = tileMaterial;
			}
			foreach (KeyValuePair<string, string> item in TileMaterials.DeserializeEmbeddedResource<Dictionary<string, string>>("Terraria.GameContent.Metadata.MaterialData.Tiles.json"))
			{
				string key = item.Key;
				string value = item.Value;
				TileMaterials.SetForTileId((ushort)TileID.Search.GetId(key), TileMaterials._materialsByName[value]);
			}
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x005FC7AC File Offset: 0x005FA9AC
		private static T DeserializeEmbeddedResource<T>(string path)
		{
			T result;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					result = JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
				}
			}
			return result;
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x005FC80C File Offset: 0x005FAA0C
		public static void SetForTileId(ushort tileId, TileMaterial material)
		{
			TileMaterials.MaterialsByTileId[(int)tileId] = material;
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x005FC816 File Offset: 0x005FAA16
		public static TileMaterial GetByTileId(ushort tileId)
		{
			return TileMaterials.MaterialsByTileId[(int)tileId];
		}

		// Token: 0x040059EB RID: 23019
		public static Dictionary<string, TileMaterial> _materialsByName;

		// Token: 0x040059EC RID: 23020
		internal static TileMaterial[] MaterialsByTileId = new TileMaterial[(int)TileID.Count];
	}
}
