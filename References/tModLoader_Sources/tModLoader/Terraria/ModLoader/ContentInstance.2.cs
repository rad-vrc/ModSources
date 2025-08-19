using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.ModLoader
{
	// Token: 0x02000154 RID: 340
	public static class ContentInstance<T> where T : class
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x004D0CD4 File Offset: 0x004CEED4
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x004D0CDB File Offset: 0x004CEEDB
		public static T Instance { get; private set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x004D0CE3 File Offset: 0x004CEEE3
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x004D0CEA File Offset: 0x004CEEEA
		public static IReadOnlyList<T> Instances { get; private set; }

		// Token: 0x06001B99 RID: 7065 RVA: 0x004D0CF2 File Offset: 0x004CEEF2
		static ContentInstance()
		{
			ContentInstance.Link(typeof(T), new Action<object, IEnumerable>(ContentInstance<T>.Update));
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x004D0D0F File Offset: 0x004CEF0F
		private static void Update(object instance, IEnumerable instances)
		{
			ContentInstance<T>.Instance = (T)((object)instance);
			IReadOnlyList<T> instances2;
			if (instances == null)
			{
				instances2 = null;
			}
			else
			{
				IEnumerable<T> enumerable = instances.Cast<T>();
				instances2 = ((enumerable != null) ? enumerable.ToArray<T>() : null);
			}
			ContentInstance<T>.Instances = instances2;
		}
	}
}
