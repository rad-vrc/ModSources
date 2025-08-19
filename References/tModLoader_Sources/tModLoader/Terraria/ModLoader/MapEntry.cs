using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x02000193 RID: 403
	internal struct MapEntry
	{
		// Token: 0x06001F33 RID: 7987 RVA: 0x004E0925 File Offset: 0x004DEB25
		internal MapEntry(Color color, LocalizedText name = null)
		{
			if (name == null)
			{
				name = LocalizedText.Empty;
			}
			this.color = color;
			this.name = name;
			Func<string, int, int, string> func;
			if ((func = MapEntry.<>O.<0>__sameName) == null)
			{
				func = (MapEntry.<>O.<0>__sameName = new Func<string, int, int, string>(MapEntry.sameName));
			}
			this.getName = func;
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x004E0960 File Offset: 0x004DEB60
		internal MapEntry(Color color, LocalizedText name, Func<string, int, int, string> getName)
		{
			this.color = color;
			this.name = name;
			this.getName = getName;
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x004E0977 File Offset: 0x004DEB77
		private static string sameName(string name, int x, int y)
		{
			return name;
		}

		// Token: 0x0400166F RID: 5743
		internal Color color;

		// Token: 0x04001670 RID: 5744
		internal LocalizedText name;

		// Token: 0x04001671 RID: 5745
		internal Func<string, int, int, string> getName;

		// Token: 0x02000916 RID: 2326
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006ACB RID: 27339
			public static Func<string, int, int, string> <0>__sameName;
		}
	}
}
