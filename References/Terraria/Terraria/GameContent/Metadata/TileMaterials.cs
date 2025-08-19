using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Terraria.ID;

namespace Terraria.GameContent.Metadata
{
	// Token: 0x02000211 RID: 529
	public static class TileMaterials
	{
		// Token: 0x06001DF3 RID: 7667 RVA: 0x00507490 File Offset: 0x00505690
		static TileMaterials()
		{
			TileMaterials._materialsByName = TileMaterials.DeserializeEmbeddedResource<Dictionary<string, TileMaterial>>("Terraria.GameContent.Metadata.MaterialData.Materials.json");
			TileMaterial tileMaterial = TileMaterials._materialsByName["Default"];
			for (int i = 0; i < TileMaterials.MaterialsByTileId.Length; i++)
			{
				TileMaterials.MaterialsByTileId[i] = tileMaterial;
			}
			foreach (KeyValuePair<string, string> keyValuePair in TileMaterials.DeserializeEmbeddedResource<Dictionary<string, string>>("Terraria.GameContent.Metadata.MaterialData.Tiles.json"))
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				TileMaterials.SetForTileId((ushort)TileID.Search.GetId(key), TileMaterials._materialsByName[value]);
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00507558 File Offset: 0x00505758
		private static T DeserializeEmbeddedResource<T>(string path)
		{
			T result;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					result = JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
				}
			}
			return result;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x005075B8 File Offset: 0x005057B8
		public static void SetForTileId(ushort tileId, TileMaterial material)
		{
			TileMaterials.MaterialsByTileId[(int)tileId] = material;
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x005075C2 File Offset: 0x005057C2
		public static TileMaterial GetByTileId(ushort tileId)
		{
			return TileMaterials.MaterialsByTileId[(int)tileId];
		}

		// Token: 0x04004588 RID: 17800
		private static Dictionary<string, TileMaterial> _materialsByName;

		// Token: 0x04004589 RID: 17801
		private static readonly TileMaterial[] MaterialsByTileId = new TileMaterial[(int)TileID.Count];
	}
}
