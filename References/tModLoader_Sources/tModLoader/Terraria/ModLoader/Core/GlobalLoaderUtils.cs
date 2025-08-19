using System;
using System.Buffers;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.Core
{
	// Token: 0x0200035B RID: 859
	public static class GlobalLoaderUtils<TGlobal, TEntity> where TGlobal : GlobalType<TEntity, TGlobal> where TEntity : IEntityWithGlobals<TGlobal>
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x005369FF File Offset: 0x00534BFF
		private static TGlobal[] Globals
		{
			get
			{
				return GlobalList<TGlobal>.Globals;
			}
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x00536A06 File Offset: 0x00534C06
		static GlobalLoaderUtils()
		{
			TypeCaching.ResetStaticMembersOnClear(typeof(GlobalTypeLookups<TGlobal>));
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x00536A18 File Offset: 0x00534C18
		public static void SetDefaults(TEntity entity, ref TGlobal[] entityGlobals, Action<TEntity> setModEntityDefaults)
		{
			if (entity.Type == 0)
			{
				return;
			}
			int initialType = entity.Type;
			entityGlobals = new TGlobal[GlobalList<TGlobal>.SlotsPerEntity];
			if (!GlobalTypeLookups<TGlobal>.Initialized)
			{
				GlobalLoaderUtils<TGlobal, TEntity>.SetDefaultsBeforeLookupsAreBuilt(entity, entityGlobals, setModEntityDefaults);
				return;
			}
			foreach (TGlobal g in GlobalLoaderUtils<TGlobal, TEntity>.SlotPerEntityGlobals[entity.Type])
			{
				short slot = g.PerEntityIndex;
				entityGlobals[(int)slot] = (g.InstancePerEntity ? g.NewInstance(entity) : g);
			}
			setModEntityDefaults(entity);
			foreach (TGlobal tglobal in new EntityGlobalsEnumerator<TGlobal>(GlobalLoaderUtils<TGlobal, TEntity>.HookSetDefaultsEarly[entity.Type], entity))
			{
				tglobal.SetDefaults(entity);
			}
			foreach (TGlobal tglobal2 in new EntityGlobalsEnumerator<TGlobal>(GlobalLoaderUtils<TGlobal, TEntity>.HookSetDefaultsLate[entity.Type], entity))
			{
				tglobal2.SetDefaults(entity);
			}
			if (entity.Type != initialType)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(55, 4);
				defaultInterpolatedStringHandler.AppendLiteral("A mod attempted to ");
				defaultInterpolatedStringHandler.AppendFormatted(typeof(TEntity).Name);
				defaultInterpolatedStringHandler.AppendLiteral(".type from ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(initialType);
				defaultInterpolatedStringHandler.AppendLiteral(" to ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(entity.Type);
				defaultInterpolatedStringHandler.AppendLiteral(" during SetDefaults. ");
				defaultInterpolatedStringHandler.AppendFormatted<TEntity>(entity);
				throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x00536BE4 File Offset: 0x00534DE4
		private static void SetDefaultsBeforeLookupsAreBuilt(TEntity entity, TGlobal[] entityGlobals, Action<TEntity> setModEntityDefaults)
		{
			GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime[] instTimes = ArrayPool<GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime>.Shared.Rent(GlobalLoaderUtils<TGlobal, TEntity>.Globals.Length);
			try
			{
				GlobalLoaderUtils<TGlobal, TEntity>.SetDefaultsBeforeLookupsAreBuilt(entity, entityGlobals, setModEntityDefaults, ref instTimes);
				GlobalLoaderUtils<TGlobal, TEntity>.TUpdateGlobalTypeData updateGlobalTypeData = GlobalLoaderUtils<TGlobal, TEntity>.UpdateGlobalTypeData;
				if (updateGlobalTypeData != null)
				{
					updateGlobalTypeData(entity.Type, instTimes.AsSpan<GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime>().Slice(0, GlobalLoaderUtils<TGlobal, TEntity>.Globals.Length));
				}
			}
			finally
			{
				ArrayPool<GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime>.Shared.Return(instTimes, true);
			}
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x00536C64 File Offset: 0x00534E64
		private static void SetDefaultsBeforeLookupsAreBuilt(TEntity entity, TGlobal[] entityGlobals, Action<TEntity> setModEntityDefaults, ref GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime[] instTimes)
		{
			foreach (TGlobal g in GlobalLoaderUtils<TGlobal, TEntity>.Globals)
			{
				if (!g.ConditionallyAppliesToEntities || g.AppliesToEntity(entity, false))
				{
					if (g.PerEntityIndex >= 0)
					{
						entityGlobals[(int)g.PerEntityIndex] = (g.InstancePerEntity ? g.NewInstance(entity) : g);
					}
					instTimes[(int)g.StaticIndex] = GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime.Pass1;
				}
			}
			setModEntityDefaults(entity);
			foreach (TGlobal g2 in GlobalLoaderUtils<TGlobal, TEntity>.Globals)
			{
				if (instTimes[(int)g2.StaticIndex] == GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime.Pass1)
				{
					TGlobal tglobal = (g2.PerEntityIndex >= 0) ? entityGlobals[(int)g2.PerEntityIndex] : g2;
					if (tglobal != null)
					{
						tglobal.SetDefaults(entity);
					}
				}
			}
			foreach (TGlobal g3 in GlobalLoaderUtils<TGlobal, TEntity>.Globals)
			{
				if (instTimes[(int)g3.StaticIndex] != GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime.Pass1 && (!g3.ConditionallyAppliesToEntities || g3.AppliesToEntity(entity, true)))
				{
					if (g3.PerEntityIndex >= 0)
					{
						entityGlobals[(int)g3.PerEntityIndex] = (g3.InstancePerEntity ? g3.NewInstance(entity) : g3);
					}
					instTimes[(int)g3.StaticIndex] = GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime.Pass2;
				}
			}
			foreach (TGlobal g4 in GlobalLoaderUtils<TGlobal, TEntity>.Globals)
			{
				if (instTimes[(int)g4.StaticIndex] == GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime.Pass2)
				{
					TGlobal tglobal2 = (g4.PerEntityIndex >= 0) ? entityGlobals[(int)g4.PerEntityIndex] : g4;
					if (tglobal2 != null)
					{
						tglobal2.SetDefaults(entity);
					}
				}
			}
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x00536E64 File Offset: 0x00535064
		public static void BuildTypeLookups(Action<int> setDefaults)
		{
			try
			{
				TGlobal g2;
				TGlobal[] hookSetDefaults = GlobalLoaderUtils<TGlobal, TEntity>.Globals.WhereMethodIsOverridden((TGlobal g) => (Action<TEntity>)methodof(GlobalType<TEntity, TGlobal>.SetDefaults(TEntity)).CreateDelegate(typeof(Action<TEntity>), g2)).ToArray<TGlobal>();
				int typeCount = GlobalList<TGlobal>.EntityTypeCount;
				Array.Fill<TGlobal[]>(GlobalLoaderUtils<TGlobal, TEntity>.HookSetDefaultsEarly = new TGlobal[typeCount][], Array.Empty<TGlobal>());
				Array.Fill<TGlobal[]>(GlobalLoaderUtils<TGlobal, TEntity>.HookSetDefaultsLate = new TGlobal[typeCount][], Array.Empty<TGlobal>());
				TGlobal[][] globalsForType = new TGlobal[typeCount][];
				Array.Fill<TGlobal[]>(globalsForType, Array.Empty<TGlobal>());
				GlobalTypeLookups<TGlobal>.AppliesToTypeSet[] appliesToTypeCache = new GlobalTypeLookups<TGlobal>.AppliesToTypeSet[GlobalLoaderUtils<TGlobal, TEntity>.Globals.Length];
				GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime[] instTimes = new GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime[GlobalLoaderUtils<TGlobal, TEntity>.Globals.Length];
				Predicate<TGlobal> <>9__3;
				Predicate<TGlobal> <>9__4;
				Predicate<TGlobal> <>9__5;
				for (int setDefaultsType = 0; setDefaultsType < typeCount; setDefaultsType++)
				{
					int finalType = 0;
					GlobalLoaderUtils<TGlobal, TEntity>.UpdateGlobalTypeData = delegate(int type, ReadOnlySpan<GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime> data)
					{
						if (type == 0)
						{
							return;
						}
						finalType = type;
						data.CopyTo(instTimes);
					};
					setDefaults(setDefaultsType);
					if (finalType != 0)
					{
						TGlobal[][] array = globalsForType;
						int finalType4 = finalType;
						TGlobal[] globals = GlobalLoaderUtils<TGlobal, TEntity>.Globals;
						Predicate<TGlobal> filter;
						if ((filter = <>9__3) == null)
						{
							filter = (<>9__3 = ((TGlobal g) => instTimes[(int)g.StaticIndex] > GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime.NotApplied));
						}
						array[finalType4] = GlobalTypeLookups<TGlobal>.CachedFilter(globals, filter);
						TGlobal[][] hookSetDefaultsEarly = GlobalLoaderUtils<TGlobal, TEntity>.HookSetDefaultsEarly;
						int finalType2 = finalType;
						TGlobal[] arr = hookSetDefaults;
						Predicate<TGlobal> filter2;
						if ((filter2 = <>9__4) == null)
						{
							filter2 = (<>9__4 = ((TGlobal g) => instTimes[(int)g.StaticIndex] == GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime.Pass1));
						}
						hookSetDefaultsEarly[finalType2] = GlobalTypeLookups<TGlobal>.CachedFilter(arr, filter2);
						TGlobal[][] hookSetDefaultsLate = GlobalLoaderUtils<TGlobal, TEntity>.HookSetDefaultsLate;
						int finalType3 = finalType;
						TGlobal[] arr2 = hookSetDefaults;
						Predicate<TGlobal> filter3;
						if ((filter3 = <>9__5) == null)
						{
							filter3 = (<>9__5 = ((TGlobal g) => instTimes[(int)g.StaticIndex] == GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime.Pass2));
						}
						hookSetDefaultsLate[finalType3] = GlobalTypeLookups<TGlobal>.CachedFilter(arr2, filter3);
						foreach (TGlobal g2 in GlobalLoaderUtils<TGlobal, TEntity>.Globals)
						{
							if (g2.ConditionallyAppliesToEntities && instTimes[(int)g2.StaticIndex] > GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime.NotApplied)
							{
								appliesToTypeCache[(int)g2.StaticIndex].Add(finalType);
							}
						}
					}
				}
				GlobalTypeLookups<TGlobal>.Init(globalsForType, appliesToTypeCache);
				GlobalLoaderUtils<TGlobal, TEntity>.SlotPerEntityGlobals = GlobalTypeLookups<TGlobal>.BuildPerTypeGlobalLists((from g in GlobalLoaderUtils<TGlobal, TEntity>.Globals
				where g.SlotPerEntity
				select g).ToArray<TGlobal>());
			}
			finally
			{
				GlobalLoaderUtils<TGlobal, TEntity>.UpdateGlobalTypeData = null;
			}
		}

		// Token: 0x04001CC6 RID: 7366
		private static TGlobal[][] SlotPerEntityGlobals;

		// Token: 0x04001CC7 RID: 7367
		private static TGlobal[][] HookSetDefaultsEarly;

		// Token: 0x04001CC8 RID: 7368
		private static TGlobal[][] HookSetDefaultsLate;

		// Token: 0x04001CC9 RID: 7369
		[ThreadStatic]
		private static GlobalLoaderUtils<TGlobal, TEntity>.TUpdateGlobalTypeData UpdateGlobalTypeData;

		// Token: 0x02000AA9 RID: 2729
		private enum InstantiationTime
		{
			// Token: 0x04006DC4 RID: 28100
			NotApplied,
			// Token: 0x04006DC5 RID: 28101
			Pass1,
			// Token: 0x04006DC6 RID: 28102
			Pass2
		}

		// Token: 0x02000AAA RID: 2730
		// (Invoke) Token: 0x060059BE RID: 22974
		private delegate void TUpdateGlobalTypeData(int type, ReadOnlySpan<GlobalLoaderUtils<TGlobal, TEntity>.InstantiationTime> data);
	}
}
