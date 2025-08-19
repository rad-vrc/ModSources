using System;
using System.Collections.Generic;
using ReLogic.Reflection;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	// Token: 0x02000157 RID: 343
	public static class DamageClassLoader
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x004D0F5C File Offset: 0x004CF15C
		public static int DamageClassCount
		{
			get
			{
				return DamageClassLoader.DamageClasses.Count;
			}
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x004D0F68 File Offset: 0x004CF168
		static DamageClassLoader()
		{
			DamageClassLoader.RegisterDefaultClasses();
			DamageClassLoader.RebuildEffectInheritanceCache();
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x004D1006 File Offset: 0x004CF206
		internal static int Add(DamageClass damageClass)
		{
			DamageClassLoader.DamageClasses.Add(damageClass);
			return DamageClassLoader.DamageClasses.Count - 1;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x004D101F File Offset: 0x004CF21F
		internal static void ResizeArrays()
		{
			LoaderUtils.ResetStaticMembers(typeof(DamageClass.Sets), true);
			DamageClassLoader.RebuildEffectInheritanceCache();
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x004D1038 File Offset: 0x004CF238
		internal static void Unload()
		{
			DamageClassLoader.DamageClasses.RemoveRange(DamageClassLoader.DefaultClassCount, DamageClassLoader.DamageClasses.Count - DamageClassLoader.DefaultClassCount);
			DamageClass.Search = IdDictionary.Create<DamageClass, int>();
			foreach (DamageClass damageClass in DamageClassLoader.DamageClasses)
			{
				DamageClass.Search.Add(damageClass.FullName, damageClass.Type);
			}
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x004D10C4 File Offset: 0x004CF2C4
		private static void RebuildEffectInheritanceCache()
		{
			DamageClassLoader.effectInheritanceCache = new bool[DamageClassLoader.DamageClassCount, DamageClassLoader.DamageClassCount];
			for (int i = 0; i < DamageClassLoader.DamageClasses.Count; i++)
			{
				for (int j = 0; j < DamageClassLoader.DamageClasses.Count; j++)
				{
					DamageClass damageClass = DamageClassLoader.DamageClasses[i];
					if (damageClass == DamageClassLoader.DamageClasses[j] || damageClass.GetEffectInheritance(DamageClassLoader.DamageClasses[j]))
					{
						DamageClassLoader.effectInheritanceCache[i, j] = true;
					}
				}
			}
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x004D114C File Offset: 0x004CF34C
		internal static void RegisterDefaultClasses()
		{
			int i = 0;
			foreach (DamageClass damageClass in DamageClassLoader.DamageClasses)
			{
				damageClass.Type = i++;
				ContentInstance.Register(damageClass);
				ModTypeLookup<DamageClass>.Register(damageClass);
			}
		}

		/// <summary>
		/// Gets the DamageClass instance corresponding to the specified type
		/// </summary>
		/// <param name="type">The <see cref="P:Terraria.ModLoader.DamageClass.Type" /> of the damage class</param>
		/// <returns>The DamageClass instance, null if not found.</returns>
		// Token: 0x06001BC9 RID: 7113 RVA: 0x004D11B0 File Offset: 0x004CF3B0
		public static DamageClass GetDamageClass(int type)
		{
			if (type >= DamageClassLoader.DamageClasses.Count)
			{
				return null;
			}
			return DamageClassLoader.DamageClasses[type];
		}

		// Token: 0x040014D8 RID: 5336
		internal static bool[,] effectInheritanceCache;

		// Token: 0x040014D9 RID: 5337
		internal static readonly List<DamageClass> DamageClasses = new List<DamageClass>
		{
			DamageClass.Default,
			DamageClass.Generic,
			DamageClass.Melee,
			DamageClass.MeleeNoSpeed,
			DamageClass.Ranged,
			DamageClass.Magic,
			DamageClass.Summon,
			DamageClass.SummonMeleeSpeed,
			DamageClass.MagicSummonHybrid,
			DamageClass.Throwing
		};

		// Token: 0x040014DA RID: 5338
		private static readonly int DefaultClassCount = DamageClassLoader.DamageClasses.Count;
	}
}
