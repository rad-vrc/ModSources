using System;
using Newtonsoft.Json.Linq;

namespace Terraria.IO
{
	// Token: 0x020000DD RID: 221
	public class GameConfiguration
	{
		// Token: 0x0600151D RID: 5405 RVA: 0x004AFA9B File Offset: 0x004ADC9B
		public GameConfiguration(JObject configurationRoot)
		{
			this._root = configurationRoot;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x004AFAAA File Offset: 0x004ADCAA
		public T Get<T>(string entry)
		{
			return this._root[entry].ToObject<T>();
		}

		// Token: 0x04001299 RID: 4761
		private readonly JObject _root;
	}
}
