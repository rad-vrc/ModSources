using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.IO;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000077 RID: 119
	public class WorldGenConfiguration : GameConfiguration
	{
		// Token: 0x0600117A RID: 4474 RVA: 0x0048E074 File Offset: 0x0048C274
		public WorldGenConfiguration(JObject configurationRoot) : base(configurationRoot)
		{
			this._biomeRoot = (((JObject)configurationRoot["Biomes"]) ?? new JObject());
			this._passRoot = (((JObject)configurationRoot["Passes"]) ?? new JObject());
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0048E0C6 File Offset: 0x0048C2C6
		public T CreateBiome<T>() where T : MicroBiome, new()
		{
			return this.CreateBiome<T>(typeof(T).Name);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0048E0E0 File Offset: 0x0048C2E0
		public T CreateBiome<T>(string name) where T : MicroBiome, new()
		{
			JToken jtoken;
			if (this._biomeRoot.TryGetValue(name, ref jtoken))
			{
				return jtoken.ToObject<T>();
			}
			return Activator.CreateInstance<T>();
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0048E10C File Offset: 0x0048C30C
		public GameConfiguration GetPassConfiguration(string name)
		{
			JToken jtoken;
			if (this._passRoot.TryGetValue(name, ref jtoken))
			{
				return new GameConfiguration((JObject)jtoken);
			}
			return new GameConfiguration(new JObject());
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0048E140 File Offset: 0x0048C340
		public static WorldGenConfiguration FromEmbeddedPath(string path)
		{
			WorldGenConfiguration result;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					result = new WorldGenConfiguration(JsonConvert.DeserializeObject<JObject>(streamReader.ReadToEnd()));
				}
			}
			return result;
		}

		// Token: 0x04000FBF RID: 4031
		private readonly JObject _biomeRoot;

		// Token: 0x04000FC0 RID: 4032
		private readonly JObject _passRoot;
	}
}
