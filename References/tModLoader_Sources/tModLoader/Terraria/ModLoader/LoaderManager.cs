using System;
using System.Collections.Generic;
using System.Reflection;

namespace Terraria.ModLoader
{
	// Token: 0x02000190 RID: 400
	public static class LoaderManager
	{
		// Token: 0x06001EF3 RID: 7923 RVA: 0x004DD3C8 File Offset: 0x004DB5C8
		internal static void AutoLoad()
		{
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (typeof(ILoader).IsAssignableFrom(type) && !type.IsAbstract && type.IsClass && AutoloadAttribute.GetValue(type).NeedsAutoloading)
				{
					LoaderManager.loadersByType.Add(type, (ILoader)Activator.CreateInstance(type, true));
				}
			}
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x004DD438 File Offset: 0x004DB638
		public static T Get<T>()
		{
			ILoader result;
			if (!LoaderManager.loadersByType.TryGetValue(typeof(T), out result))
			{
				return (T)((object)Activator.CreateInstance(typeof(T), true));
			}
			return (T)((object)result);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x004DD47C File Offset: 0x004DB67C
		internal static void Unload()
		{
			foreach (ILoader loader in LoaderManager.loadersByType.Values)
			{
				loader.Unload();
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x004DD4D0 File Offset: 0x004DB6D0
		internal static void ResizeArrays()
		{
			foreach (ILoader loader in LoaderManager.loadersByType.Values)
			{
				loader.ResizeArrays();
			}
		}

		// Token: 0x0400164E RID: 5710
		private static readonly Dictionary<Type, ILoader> loadersByType = new Dictionary<Type, ILoader>();
	}
}
