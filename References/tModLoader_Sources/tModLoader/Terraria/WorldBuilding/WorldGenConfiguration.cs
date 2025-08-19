using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.IO;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000085 RID: 133
	public class WorldGenConfiguration : GameConfiguration
	{
		// Token: 0x0600141B RID: 5147 RVA: 0x004A0D74 File Offset: 0x0049EF74
		public WorldGenConfiguration(JObject configurationRoot) : base(configurationRoot)
		{
			this._biomeRoot = (((JObject)configurationRoot["Biomes"]) ?? new JObject());
			this._passRoot = (((JObject)configurationRoot["Passes"]) ?? new JObject());
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x004A0DC6 File Offset: 0x0049EFC6
		public T CreateBiome<T>() where T : MicroBiome, new()
		{
			return this.CreateBiome<T>(typeof(T).Name);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x004A0DE0 File Offset: 0x0049EFE0
		public T CreateBiome<T>(string name) where T : MicroBiome, new()
		{
			JToken value;
			if (this._biomeRoot.TryGetValue(name, ref value))
			{
				return value.ToObject<T>();
			}
			return Activator.CreateInstance<T>();
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x004A0E0C File Offset: 0x0049F00C
		public GameConfiguration GetPassConfiguration(string name)
		{
			JToken value;
			if (this._passRoot.TryGetValue(name, ref value))
			{
				return new GameConfiguration((JObject)value);
			}
			return new GameConfiguration(new JObject());
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x004A0E40 File Offset: 0x0049F040
		public static WorldGenConfiguration FromEmbeddedPath(string path)
		{
			WorldGenConfiguration result;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					result = new WorldGenConfiguration(JsonConvert.DeserializeObject<JObject>(streamReader.ReadToEnd()));
				}
			}
			return result;
		}

		// Token: 0x04001097 RID: 4247
		private readonly JObject _biomeRoot;

		// Token: 0x04001098 RID: 4248
		private readonly JObject _passRoot;
	}
}
