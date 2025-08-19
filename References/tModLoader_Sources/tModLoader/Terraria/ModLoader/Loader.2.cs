using System;
using System.Collections.Generic;

namespace Terraria.ModLoader
{
	/// <summary> Serves as a highest-level base for loaders of mod types. </summary>
	// Token: 0x0200018E RID: 398
	public abstract class Loader<T> : Loader where T : ModType
	{
		// Token: 0x06001EED RID: 7917 RVA: 0x004DD346 File Offset: 0x004DB546
		public int Register(T obj)
		{
			int result = base.Reserve();
			ModTypeLookup<T>.Register(obj);
			this.list.Add(obj);
			return result;
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x004DD360 File Offset: 0x004DB560
		public T Get(int id)
		{
			if (id < base.VanillaCount || id >= base.TotalCount)
			{
				return default(T);
			}
			return this.list[id - base.VanillaCount];
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x004DD39C File Offset: 0x004DB59C
		internal override void Unload()
		{
			base.Unload();
			this.list.Clear();
		}

		// Token: 0x0400164D RID: 5709
		internal List<T> list = new List<T>();
	}
}
