using System;
using System.Collections.Generic;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000368 RID: 872
	internal static class TypeCaching
	{
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06003081 RID: 12417 RVA: 0x0053CD90 File Offset: 0x0053AF90
		// (remove) Token: 0x06003082 RID: 12418 RVA: 0x0053CDC4 File Offset: 0x0053AFC4
		public static event Action OnClear;

		// Token: 0x06003083 RID: 12419 RVA: 0x0053CDF7 File Offset: 0x0053AFF7
		public static void Clear()
		{
			Action onClear = TypeCaching.OnClear;
			if (onClear == null)
			{
				return;
			}
			onClear();
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x0053CE08 File Offset: 0x0053B008
		internal static void ResetStaticMembersOnClear(Type type)
		{
			if (TypeCaching._resetsRegistered.Add(type))
			{
				TypeCaching.OnClear += delegate()
				{
					LoaderUtils.ResetStaticMembers(type, true);
				};
			}
		}

		// Token: 0x04001D13 RID: 7443
		private static HashSet<Type> _resetsRegistered = new HashSet<Type>();
	}
}
