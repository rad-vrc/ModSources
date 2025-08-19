using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000352 RID: 850
	internal static class AssemblyResolving
	{
		// Token: 0x06002F7D RID: 12157 RVA: 0x00534E86 File Offset: 0x00533086
		public static void Init()
		{
			if (AssemblyResolving.init)
			{
				return;
			}
			AssemblyResolving.init = true;
			AssemblyResolving.AssemblyResolveEarly(delegate(object _, [Nullable(1)] ResolveEventArgs args)
			{
				Logging.tML.DebugFormat("Assembly Resolve: {0} -> {1}", args.RequestingAssembly, args.Name);
				return null;
			});
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x00534EBC File Offset: 0x005330BC
		internal static void AssemblyResolveEarly(ResolveEventHandler handler)
		{
			FieldInfo fieldInfo = typeof(AppDomain).GetFields((BindingFlags)(-1)).First((FieldInfo f) => f.Name.EndsWith("AssemblyResolve"));
			ResolveEventHandler a = (ResolveEventHandler)fieldInfo.GetValue(AppDomain.CurrentDomain);
			fieldInfo.SetValue(AppDomain.CurrentDomain, null);
			AppDomain.CurrentDomain.AssemblyResolve += handler;
			AppDomain.CurrentDomain.AssemblyResolve += a;
		}

		// Token: 0x04001C9A RID: 7322
		private static bool init;
	}
}
