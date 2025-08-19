using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	// Token: 0x0200016C RID: 364
	public static class GlobalList<TGlobal> where TGlobal : GlobalType<TGlobal>
	{
		/// <summary>
		/// All registered globals. Empty until all globals have loaded
		/// </summary>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x004D3CC3 File Offset: 0x004D1EC3
		// (set) Token: 0x06001D05 RID: 7429 RVA: 0x004D3CCA File Offset: 0x004D1ECA
		public static TGlobal[] Globals { get; private set; } = Array.Empty<TGlobal>();

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x004D3CD2 File Offset: 0x004D1ED2
		// (set) Token: 0x06001D07 RID: 7431 RVA: 0x004D3CD9 File Offset: 0x004D1ED9
		public static int SlotsPerEntity { get; private set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x004D3CE1 File Offset: 0x004D1EE1
		// (set) Token: 0x06001D09 RID: 7433 RVA: 0x004D3CE8 File Offset: 0x004D1EE8
		public static int EntityTypeCount { get; private set; }

		// Token: 0x06001D0A RID: 7434 RVA: 0x004D3CF0 File Offset: 0x004D1EF0
		[return: TupleElementNames(new string[]
		{
			"index",
			"perEntityIndex"
		})]
		internal static ValueTuple<short, short> Register(TGlobal global)
		{
			if (GlobalList<TGlobal>.loadingFinished)
			{
				throw new Exception("Loading has finished. Cannot add more globals");
			}
			short item = (short)GlobalList<TGlobal>._globals.Count;
			short perEntityIndex = (short)(global.SlotPerEntity ? GlobalList<TGlobal>.SlotsPerEntity++ : -1);
			GlobalList<TGlobal>._globals.Add(global);
			return new ValueTuple<short, short>(item, perEntityIndex);
		}

		/// <summary>
		/// Call during <see cref="M:Terraria.ModLoader.ILoader.ResizeArrays" />. Which runs after all <see cref="M:Terraria.ModLoader.ILoadable.Load(Terraria.ModLoader.Mod)" /> calls, but before any <see cref="M:Terraria.ModLoader.ModType.SetupContent" /> calls
		/// </summary>
		// Token: 0x06001D0B RID: 7435 RVA: 0x004D3D4A File Offset: 0x004D1F4A
		public static void FinishLoading(int typeCount)
		{
			if (GlobalList<TGlobal>.loadingFinished)
			{
				throw new Exception("FinishLoading already called");
			}
			GlobalList<TGlobal>.loadingFinished = true;
			GlobalList<TGlobal>.Globals = GlobalList<TGlobal>._globals.ToArray();
			GlobalList<TGlobal>.EntityTypeCount = typeCount;
		}

		/// <summary>
		/// Call during unloading, to clear the globals list
		/// </summary>
		// Token: 0x06001D0C RID: 7436 RVA: 0x004D3D79 File Offset: 0x004D1F79
		public static void Reset()
		{
			LoaderUtils.ResetStaticMembers(typeof(GlobalList<TGlobal>), true);
		}

		// Token: 0x040015B1 RID: 5553
		private static bool loadingFinished = false;

		// Token: 0x040015B2 RID: 5554
		private static List<TGlobal> _globals = new List<TGlobal>();
	}
}
