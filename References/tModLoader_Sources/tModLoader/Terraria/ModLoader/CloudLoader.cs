using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	// Token: 0x0200014F RID: 335
	public static class CloudLoader
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x004CFA25 File Offset: 0x004CDC25
		// (set) Token: 0x06001B48 RID: 6984 RVA: 0x004CFA2C File Offset: 0x004CDC2C
		public static int CloudCount { get; private set; } = CloudID.Count;

		/// <summary> Registers a new cloud with the provided texture, spawn chance, and indication if the cloud belongs to the "rare cloud" category. Typically used with <see cref="T:Terraria.ModLoader.SimpleModCloud" /> as TCloud for cloud with no additional logic.
		/// <para /> Use this to manually load a modded cloud rather than making a <see cref="T:Terraria.ModLoader.ModCloud" /> class or autoloading the cloud through <see cref="P:Terraria.ModLoader.Mod.CloudAutoloadingEnabled" /> logic. 
		/// </summary>
		// Token: 0x06001B49 RID: 6985 RVA: 0x004CFA34 File Offset: 0x004CDC34
		public static bool AddCloudFromTexture<TCloud>(Mod mod, string texture, float spawnChance = 1f, bool rareCloud = false) where TCloud : ModCloud, new()
		{
			if (mod == null)
			{
				throw new ArgumentNullException("mod");
			}
			if (texture == null)
			{
				throw new ArgumentNullException("texture");
			}
			if (!mod.loading)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorNotLoading"));
			}
			TCloud tcloud = Activator.CreateInstance<TCloud>();
			tcloud.nameOverride = Path.GetFileNameWithoutExtension(texture);
			tcloud.textureOverride = texture;
			tcloud.spawnChance = spawnChance;
			tcloud.rareCloud = rareCloud;
			return mod.AddContent(tcloud);
		}

		/// <summary> Registers a new cloud with the provided texture, spawn chance, and indication if the cloud belongs to the "rare cloud" category.
		/// <para /> Use this to manually load a modded cloud rather than making a <see cref="T:Terraria.ModLoader.ModCloud" /> class or autoloading the cloud through <see cref="P:Terraria.ModLoader.Mod.CloudAutoloadingEnabled" /> logic. 
		/// </summary>
		// Token: 0x06001B4A RID: 6986 RVA: 0x004CFABA File Offset: 0x004CDCBA
		public static bool AddCloudFromTexture(Mod mod, string texture, float spawnChance = 1f, bool rareCloud = false)
		{
			return CloudLoader.AddCloudFromTexture<SimpleModCloud>(mod, texture, spawnChance, rareCloud);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x004CFAC8 File Offset: 0x004CDCC8
		internal static void RegisterModCloud(ModCloud modCloud)
		{
			int id = CloudLoader.CloudCount++;
			modCloud.Type = id;
			CloudLoader.clouds[id] = modCloud;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x004CFAF8 File Offset: 0x004CDCF8
		internal static void AutoloadClouds(Mod mod)
		{
			foreach (string path in from t in mod.RootContentSource.EnumerateAssets()
			where t.Contains("Clouds/")
			select t)
			{
				string texturePath = Path.ChangeExtension(path, null);
				ModCloud modCloud;
				if (!mod.TryFind<ModCloud>(Path.GetFileName(texturePath), out modCloud))
				{
					string textureKey = mod.Name + "/" + texturePath;
					CloudLoader.AddCloudFromTexture<SimpleModCloud>(mod, textureKey, 1f, false);
				}
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x004CFBA0 File Offset: 0x004CDDA0
		public static int? ChooseCloud(float vanillaPool, bool rare)
		{
			if (!CloudLoader.cloudLoaded)
			{
				return new int?(0);
			}
			IDictionary<int, float> pool = new Dictionary<int, float>();
			pool[0] = vanillaPool;
			foreach (ModCloud cloud in CloudLoader.clouds.Values)
			{
				if (rare == cloud.RareCloud)
				{
					float weight = cloud.SpawnChance();
					if (weight > 0f)
					{
						pool[cloud.Type] = weight;
					}
				}
			}
			float totalWeight = 0f;
			foreach (int type in pool.Keys)
			{
				if (pool[type] < 0f)
				{
					pool[type] = 0f;
				}
				totalWeight += pool[type];
			}
			float choice = (float)Main.rand.NextDouble() * totalWeight;
			foreach (int type2 in pool.Keys)
			{
				float weight2 = pool[type2];
				if (choice < weight2)
				{
					return new int?(type2);
				}
				choice -= weight2;
			}
			return null;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x004CFD10 File Offset: 0x004CDF10
		internal static void ResizeAndFillArrays(bool unloading = false)
		{
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Cloud, CloudLoader.CloudCount);
			LoaderUtils.ResetStaticMembers(typeof(CloudID), true);
			foreach (KeyValuePair<int, ModCloud> pair in CloudLoader.clouds)
			{
				TextureAssets.Cloud[pair.Key] = ModContent.Request<Texture2D>(pair.Value.Texture, 2);
			}
			if (!unloading)
			{
				CloudLoader.cloudLoaded = true;
			}
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x004CFD9C File Offset: 0x004CDF9C
		internal static void Unload()
		{
			CloudLoader.cloudLoaded = false;
			CloudLoader.CloudCount = CloudID.Count;
			CloudLoader.clouds.Clear();
			Cloud.SwapOutModdedClouds();
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x004CFDC0 File Offset: 0x004CDFC0
		internal static ModCloud GetModCloud(int type)
		{
			ModCloud modCloud;
			CloudLoader.clouds.TryGetValue(type, out modCloud);
			return modCloud;
		}

		// Token: 0x040014BB RID: 5307
		internal static readonly IDictionary<int, ModCloud> clouds = new Dictionary<int, ModCloud>();

		// Token: 0x040014BD RID: 5309
		public static bool cloudLoaded = false;
	}
}
