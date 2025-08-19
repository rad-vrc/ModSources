using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;

namespace Terraria.ModLoader.Core
{
	// Token: 0x0200035C RID: 860
	public static class GlobalTypeLookups<TGlobal> where TGlobal : GlobalType<TGlobal>
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x00537178 File Offset: 0x00535378
		// (set) Token: 0x06002FC6 RID: 12230 RVA: 0x0053717F File Offset: 0x0053537F
		public static bool Initialized { get; private set; } = false;

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x00537187 File Offset: 0x00535387
		private static int EntityTypeCount
		{
			get
			{
				return GlobalList<TGlobal>.EntityTypeCount;
			}
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x0053718E File Offset: 0x0053538E
		static GlobalTypeLookups()
		{
			TypeCaching.ResetStaticMembersOnClear(typeof(GlobalTypeLookups<TGlobal>));
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x005371C0 File Offset: 0x005353C0
		public static void Init(TGlobal[][] globalsForType, GlobalTypeLookups<TGlobal>.AppliesToTypeSet[] appliesToTypeCache)
		{
			if (GlobalTypeLookups<TGlobal>.Initialized)
			{
				throw new Exception("Init already called");
			}
			GlobalTypeLookups<TGlobal>.Initialized = true;
			GlobalTypeLookups<TGlobal>._globalsForType = globalsForType;
			GlobalTypeLookups<TGlobal>._appliesToType = appliesToTypeCache;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x005371E6 File Offset: 0x005353E6
		public static TGlobal[] GetGlobalsForType(int type)
		{
			if (type == 0)
			{
				return Array.Empty<TGlobal>();
			}
			if (GlobalTypeLookups<TGlobal>._globalsForType == null)
			{
				throw new Exception("Cannot lookup globals by type until after PostSetupContent, consider moving the calling code to [Post]AddRecipes or later instead");
			}
			return GlobalTypeLookups<TGlobal>._globalsForType[type];
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x0053720A File Offset: 0x0053540A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AppliesToType(TGlobal global, int type)
		{
			return type > 0 && (!global.ConditionallyAppliesToEntities || GlobalTypeLookups<TGlobal>.AppliesToTypeCacheLookup((int)global.StaticIndex, type));
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x00537232 File Offset: 0x00535432
		private static bool AppliesToTypeCacheLookup(int index, int type)
		{
			if (GlobalTypeLookups<TGlobal>._appliesToType == null)
			{
				throw new Exception("Cannot access conditional globals on an entity until after PostSetupContent, consider moving the calling code to [Post]AddRecipes or later, or accessing the global definition directly");
			}
			return GlobalTypeLookups<TGlobal>._appliesToType[index][type];
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x00537258 File Offset: 0x00535458
		public static TGlobal[] CachedFilter(TGlobal[] arr, Predicate<TGlobal> filter)
		{
			TGlobal[] buf = ArrayPool<TGlobal>.Shared.Rent(arr.Length);
			TGlobal[] array;
			try
			{
				int i = 0;
				foreach (TGlobal g in arr)
				{
					if (filter(g))
					{
						buf[i++] = g;
					}
				}
				if (i == arr.Length)
				{
					array = arr;
				}
				else
				{
					SortedDictionary<Memory<TGlobal>, TGlobal[]> cache = GlobalTypeLookups<TGlobal>._cache;
					lock (cache)
					{
						Memory<TGlobal> filtered = buf.AsMemory<TGlobal>().Slice(0, i);
						TGlobal[] cached;
						if (GlobalTypeLookups<TGlobal>._cache.TryGetValue(filtered, out cached))
						{
							array = cached;
						}
						else
						{
							TGlobal[] result = filtered.ToArray();
							GlobalTypeLookups<TGlobal>._cache.Add(result, result);
							array = result;
						}
					}
				}
			}
			finally
			{
				ArrayPool<TGlobal>.Shared.Return(buf, true);
			}
			return array;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x00537344 File Offset: 0x00535544
		public static TGlobal[][] BuildPerTypeGlobalLists(TGlobal[] arr)
		{
			Dictionary<TGlobal[], TGlobal[]> dict = new Dictionary<TGlobal[], TGlobal[]>();
			TGlobal[][] lookup = new TGlobal[GlobalTypeLookups<TGlobal>.EntityTypeCount][];
			int type;
			int type2;
			for (type = 0; type < lookup.Length; type = type2 + 1)
			{
				TGlobal[] typeProfile = GlobalTypeLookups<TGlobal>.GetGlobalsForType(type);
				TGlobal[] v;
				if (!dict.TryGetValue(typeProfile, out v))
				{
					v = (dict[typeProfile] = GlobalTypeLookups<TGlobal>.CachedFilter(arr, (TGlobal g) => GlobalTypeLookups<TGlobal>.AppliesToType(g, type)));
				}
				lookup[type] = v;
				type2 = type;
			}
			return lookup;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x005373CC File Offset: 0x005355CC
		public static void LogStats()
		{
			TGlobal[] globals = GlobalList<TGlobal>.Globals;
			int instanced = globals.Count((TGlobal g) => g.InstancePerEntity);
			int conditionalWithSlot = globals.Count((TGlobal g) => g.ConditionallyAppliesToEntities && g.SlotPerEntity);
			int conditionalByType = globals.Count((TGlobal g) => g.ConditionallyAppliesToEntities && !g.SlotPerEntity);
			int appliesToSingleType = GlobalTypeLookups<TGlobal>._appliesToType.Count((GlobalTypeLookups<TGlobal>.AppliesToTypeSet s) => s.SingleType > 0);
			int cacheEntries = GlobalTypeLookups<TGlobal>._cache.Count;
			int cacheSize = GlobalTypeLookups<TGlobal>._cache.Values.Sum((TGlobal[] e) => e.Length * 8 + 16);
			ILog tML = Logging.tML;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(188, 9);
			defaultInterpolatedStringHandler.AppendFormatted(typeof(TGlobal).Name);
			defaultInterpolatedStringHandler.AppendLiteral(" registration stats. Count: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(globals.Length);
			defaultInterpolatedStringHandler.AppendLiteral(", Slots per Entity: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(GlobalList<TGlobal>.SlotsPerEntity);
			defaultInterpolatedStringHandler.AppendLiteral("\n\t");
			defaultInterpolatedStringHandler.AppendLiteral("Instanced: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(instanced);
			defaultInterpolatedStringHandler.AppendLiteral(", Conditional with slot: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(conditionalWithSlot);
			defaultInterpolatedStringHandler.AppendLiteral(", Conditional by type: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(conditionalByType);
			defaultInterpolatedStringHandler.AppendLiteral(", Applies to single type: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(appliesToSingleType);
			defaultInterpolatedStringHandler.AppendLiteral("\n\t");
			defaultInterpolatedStringHandler.AppendLiteral("List Permutations: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(cacheEntries);
			defaultInterpolatedStringHandler.AppendLiteral(", Est Memory Consumption: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(cacheSize);
			defaultInterpolatedStringHandler.AppendLiteral(" bytes");
			tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x04001CCB RID: 7371
		private static TGlobal[][] _globalsForType = null;

		// Token: 0x04001CCC RID: 7372
		private static GlobalTypeLookups<TGlobal>.AppliesToTypeSet[] _appliesToType = null;

		// Token: 0x04001CCD RID: 7373
		private static SortedDictionary<Memory<TGlobal>, TGlobal[]> _cache = new SortedDictionary<Memory<TGlobal>, TGlobal[]>(new GlobalTypeLookups<TGlobal>.CachedArrayComparer());

		// Token: 0x02000AAE RID: 2734
		private class CachedArrayComparer : IComparer<Memory<TGlobal>>
		{
			// Token: 0x060059CA RID: 22986 RVA: 0x006A2998 File Offset: 0x006A0B98
			public unsafe int Compare(Memory<TGlobal> m1, Memory<TGlobal> m2)
			{
				int c = m1.Length.CompareTo(m2.Length);
				if (c != 0)
				{
					return c;
				}
				Span<TGlobal> s = m1.Span;
				Span<TGlobal> s2 = m2.Span;
				for (int i = 0; i < s.Length; i++)
				{
					TGlobal g = *s[i];
					TGlobal g2 = *s2[i];
					int c2 = g.StaticIndex.CompareTo(g2.StaticIndex);
					if (c2 != 0)
					{
						return c2;
					}
					if (g != g2)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(82, 2);
						defaultInterpolatedStringHandler.AppendLiteral("Two globals with the same static index in the cache! Is one of them instanced? (");
						defaultInterpolatedStringHandler.AppendFormatted<TGlobal>(g);
						defaultInterpolatedStringHandler.AppendLiteral(",");
						defaultInterpolatedStringHandler.AppendFormatted<TGlobal>(g2);
						defaultInterpolatedStringHandler.AppendLiteral(")");
						throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
					}
				}
				return 0;
			}
		}

		// Token: 0x02000AAF RID: 2735
		public struct AppliesToTypeSet
		{
			// Token: 0x1700091C RID: 2332
			// (get) Token: 0x060059CC RID: 22988 RVA: 0x006A2A9D File Offset: 0x006A0C9D
			// (set) Token: 0x060059CD RID: 22989 RVA: 0x006A2AA5 File Offset: 0x006A0CA5
			public int SingleType { readonly get; private set; }

			// Token: 0x1700091D RID: 2333
			public bool this[int type]
			{
				get
				{
					return type == this.SingleType || this._bitset[type];
				}
			}

			// Token: 0x060059CF RID: 22991 RVA: 0x006A2AC8 File Offset: 0x006A0CC8
			public void Add(int type)
			{
				if (this._bitset.IsEmpty && this.SingleType == 0)
				{
					this.SingleType = type;
					return;
				}
				if (this.SingleType > 0)
				{
					this._bitset.Set(this.SingleType);
					this.SingleType = 0;
				}
				this._bitset.Set(type);
			}

			// Token: 0x04006DD0 RID: 28112
			private GlobalTypeLookups<TGlobal>.AppliesToTypeSet.BitSet _bitset;

			// Token: 0x02000E2F RID: 3631
			private struct BitSet
			{
				// Token: 0x170009AF RID: 2479
				public bool this[int i]
				{
					get
					{
						return this.arr != null && (this.arr[i >> 6] & 1L << i) != 0L;
					}
				}

				// Token: 0x170009B0 RID: 2480
				// (get) Token: 0x06006579 RID: 25977 RVA: 0x006DF309 File Offset: 0x006DD509
				public bool IsEmpty
				{
					get
					{
						return this.arr == null;
					}
				}

				// Token: 0x0600657A RID: 25978 RVA: 0x006DF314 File Offset: 0x006DD514
				public void Set(int i)
				{
					if (this.arr == null)
					{
						this.arr = new long[GlobalTypeLookups<TGlobal>.EntityTypeCount + 63 >> 6];
					}
					this.arr[i >> 6] |= 1L << i;
				}

				// Token: 0x04007C64 RID: 31844
				private long[] arr;
			}
		}
	}
}
