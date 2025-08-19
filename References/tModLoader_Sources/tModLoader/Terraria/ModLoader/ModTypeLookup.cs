using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Enables content instances to register with <see cref="M:Terraria.ModLoader.ModTypeLookup`1.Register(`0)" /> towards retrieval via <see cref="M:Terraria.ModLoader.ModContent.Find``1(System.String)" /> and similar methods.
	/// </summary>
	// Token: 0x020001D3 RID: 467
	public static class ModTypeLookup<T> where T : IModType
	{
		// Token: 0x060024A5 RID: 9381 RVA: 0x004E9FC0 File Offset: 0x004E81C0
		static ModTypeLookup()
		{
			if (typeof(T).Assembly == typeof(ModTypeLookup<>).Assembly)
			{
				TypeCaching.OnClear += delegate()
				{
					ModTypeLookup<T>.dict.Clear();
					ModTypeLookup<T>.tieredDict.Clear();
				};
			}
		}

		/// <summary>
		/// Registers the instance towards lookup via <see cref="M:Terraria.ModLoader.ModContent.Find``1(System.String)" /> and similar methods.
		/// <br />Should only be called once per instance. Registers legacy names specified via <see cref="T:Terraria.ModLoader.LegacyNameAttribute" /> on the instance's type automatically.
		/// </summary>
		// Token: 0x060024A6 RID: 9382 RVA: 0x004EA01B File Offset: 0x004E821B
		public static void Register(T instance)
		{
			ModTypeLookup<T>.RegisterWithName(instance, instance.Name, instance.FullName);
			ModTypeLookup<T>.RegisterLegacyNames(instance, LegacyNameAttribute.GetLegacyNamesOfType(instance.GetType()).ToArray<string>());
		}

		/// <summary>
		/// Registers the instance towards lookup via <see cref="M:Terraria.ModLoader.ModContent.Find``1(System.String)" /> and similar methods using any number of specified <paramref name="legacyNames" />.
		/// <br />Also see <seealso cref="T:Terraria.ModLoader.LegacyNameAttribute" /> which may be more convenient.
		/// </summary>
		// Token: 0x060024A7 RID: 9383 RVA: 0x004EA05C File Offset: 0x004E825C
		public static void RegisterLegacyNames(T instance, params string[] legacyNames)
		{
			foreach (string legacyName in legacyNames)
			{
				T instance2 = instance;
				string name = legacyName;
				Mod mod = instance.Mod;
				ModTypeLookup<T>.RegisterWithName(instance2, name, (((mod != null) ? mod.Name : null) ?? "Terraria") + "/" + legacyName);
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x004EA0B4 File Offset: 0x004E82B4
		private static void RegisterWithName(T instance, string name, string fullName)
		{
			if (ModTypeLookup<T>.dict.ContainsKey(fullName))
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorDuplicateName", typeof(T).Name, fullName));
			}
			ModTypeLookup<T>.dict[fullName] = instance;
			Mod mod = instance.Mod;
			string modName = ((mod != null) ? mod.Name : null) ?? "Terraria";
			Dictionary<string, T> subDictionary;
			if (!ModTypeLookup<T>.tieredDict.TryGetValue(modName, out subDictionary))
			{
				subDictionary = (ModTypeLookup<T>.tieredDict[modName] = new Dictionary<string, T>());
			}
			subDictionary[name] = instance;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x004EA146 File Offset: 0x004E8346
		internal static T Get(string fullName)
		{
			return ModTypeLookup<T>.dict[fullName];
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x004EA153 File Offset: 0x004E8353
		internal static T Get(string modName, string contentName)
		{
			return ModTypeLookup<T>.tieredDict[modName][contentName];
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x004EA166 File Offset: 0x004E8366
		internal static bool TryGetValue(string fullName, out T value)
		{
			return ModTypeLookup<T>.dict.TryGetValue(fullName, out value);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x004EA174 File Offset: 0x004E8374
		internal static bool TryGetValue(string modName, string contentName, out T value)
		{
			Dictionary<string, T> subDictionary;
			if (!ModTypeLookup<T>.tieredDict.TryGetValue(modName, out subDictionary))
			{
				value = default(T);
				return false;
			}
			return subDictionary.TryGetValue(contentName, out value);
		}

		// Token: 0x04001739 RID: 5945
		private static readonly Dictionary<string, T> dict = new Dictionary<string, T>();

		// Token: 0x0400173A RID: 5946
		private static readonly Dictionary<string, Dictionary<string, T>> tieredDict = new Dictionary<string, Dictionary<string, T>>();
	}
}
