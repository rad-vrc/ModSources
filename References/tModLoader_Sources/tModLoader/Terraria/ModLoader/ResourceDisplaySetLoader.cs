using System;
using System.Collections.Generic;

namespace Terraria.ModLoader
{
	// Token: 0x020001F3 RID: 499
	public static class ResourceDisplaySetLoader
	{
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x00501B79 File Offset: 0x004FFD79
		public static int DisplaySetCount
		{
			get
			{
				return ResourceDisplaySetLoader.moddedDisplaySets.Count;
			}
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x00501B85 File Offset: 0x004FFD85
		internal static int Add(ModResourceDisplaySet displaySet)
		{
			ResourceDisplaySetLoader.moddedDisplaySets.Add(displaySet);
			return ResourceDisplaySetLoader.DisplaySetCount - 1;
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x00501B99 File Offset: 0x004FFD99
		public static ModResourceDisplaySet GetDisplaySet(int type)
		{
			if (type < 0 || type >= ResourceDisplaySetLoader.DisplaySetCount)
			{
				return null;
			}
			return ResourceDisplaySetLoader.moddedDisplaySets[type];
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x00501BB4 File Offset: 0x004FFDB4
		internal static void Unload()
		{
			ResourceDisplaySetLoader.moddedDisplaySets.Clear();
			Main.ResourceSetsManager.ResetToVanilla();
		}

		// Token: 0x0400189E RID: 6302
		internal static readonly IList<ModResourceDisplaySet> moddedDisplaySets = new List<ModResourceDisplaySet>();
	}
}
