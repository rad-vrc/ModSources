using System;
using Newtonsoft.Json.Linq;

namespace Terraria.IO
{
	// Token: 0x020003DF RID: 991
	public class GameConfiguration
	{
		// Token: 0x060033FD RID: 13309 RVA: 0x005563D5 File Offset: 0x005545D5
		public GameConfiguration(JObject configurationRoot)
		{
			this._root = configurationRoot;
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x005563E4 File Offset: 0x005545E4
		public T Get<T>(string entry)
		{
			return this._root[entry].ToObject<T>();
		}

		// Token: 0x04001E72 RID: 7794
		private readonly JObject _root;
	}
}
