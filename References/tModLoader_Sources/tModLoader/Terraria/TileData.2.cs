using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Terraria
{
	// Token: 0x0200005D RID: 93
	internal static class TileData<[IsUnmanaged] T> where T : struct, ValueType, ITileData
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x003FD101 File Offset: 0x003FB301
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x003FD108 File Offset: 0x003FB308
		public static T[] data { get; private set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x003FD110 File Offset: 0x003FB310
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x003FD117 File Offset: 0x003FB317
		public unsafe static T* ptr { get; private set; }

		// Token: 0x06000FB9 RID: 4025 RVA: 0x003FD120 File Offset: 0x003FB320
		static TileData()
		{
			TileData.OnSetLength = (Action<uint>)Delegate.Combine(TileData.OnSetLength, new Action<uint>(TileData<T>.SetLength));
			TileData.OnClearEverything = (Action)Delegate.Combine(TileData.OnClearEverything, new Action(TileData<T>.ClearEverything));
			TileData.OnCopySingle = (Action<uint, uint>)Delegate.Combine(TileData.OnCopySingle, new Action<uint, uint>(TileData<T>.CopySingle));
			TileData.OnClearSingle = (Action<uint>)Delegate.Combine(TileData.OnClearSingle, new Action<uint>(TileData<T>.ClearSingle));
			AssemblyLoadContext.GetLoadContext(typeof(T).Assembly).Unloading += delegate(AssemblyLoadContext _)
			{
				TileData<T>.Unload();
			};
			TileData<T>.SetLength(TileData.Length);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x003FD1E0 File Offset: 0x003FB3E0
		private static void Unload()
		{
			Delegate onSetLength = TileData.OnSetLength;
			Action<uint> value;
			if ((value = TileData<T>.<>O.<0>__SetLength) == null)
			{
				value = (TileData<T>.<>O.<0>__SetLength = new Action<uint>(TileData<T>.SetLength));
			}
			TileData.OnSetLength = (Action<uint>)Delegate.Remove(onSetLength, value);
			Delegate onClearEverything = TileData.OnClearEverything;
			Action value2;
			if ((value2 = TileData<T>.<>O.<1>__ClearEverything) == null)
			{
				value2 = (TileData<T>.<>O.<1>__ClearEverything = new Action(TileData<T>.ClearEverything));
			}
			TileData.OnClearEverything = (Action)Delegate.Remove(onClearEverything, value2);
			Delegate onCopySingle = TileData.OnCopySingle;
			Action<uint, uint> value3;
			if ((value3 = TileData<T>.<>O.<2>__CopySingle) == null)
			{
				value3 = (TileData<T>.<>O.<2>__CopySingle = new Action<uint, uint>(TileData<T>.CopySingle));
			}
			TileData.OnCopySingle = (Action<uint, uint>)Delegate.Remove(onCopySingle, value3);
			Delegate onClearSingle = TileData.OnClearSingle;
			Action<uint> value4;
			if ((value4 = TileData<T>.<>O.<3>__ClearSingle) == null)
			{
				value4 = (TileData<T>.<>O.<3>__ClearSingle = new Action<uint>(TileData<T>.ClearSingle));
			}
			TileData.OnClearSingle = (Action<uint>)Delegate.Remove(onClearSingle, value4);
			if (TileData<T>.data != null)
			{
				TileData<T>.handle.Free();
				TileData<T>.data = null;
			}
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x003FD2C0 File Offset: 0x003FB4C0
		public static void ClearEverything()
		{
			Array.Clear(TileData<T>.data);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x003FD2CC File Offset: 0x003FB4CC
		private unsafe static void SetLength(uint len)
		{
			if (TileData<T>.data != null)
			{
				TileData<T>.handle.Free();
			}
			TileData<T>.data = new T[len];
			TileData<T>.handle = GCHandle.Alloc(TileData<T>.data, GCHandleType.Pinned);
			TileData<T>.ptr = (T*)TileData<T>.handle.AddrOfPinnedObject().ToPointer();
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x003FD31C File Offset: 0x003FB51C
		private unsafe static void ClearSingle(uint index)
		{
			TileData<T>.ptr[(ulong)index * (ulong)((long)sizeof(T)) / (ulong)sizeof(T)] = default(T);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x003FD335 File Offset: 0x003FB535
		private unsafe static void CopySingle(uint sourceIndex, uint destinationIndex)
		{
			TileData<T>.ptr[(ulong)destinationIndex * (ulong)((long)sizeof(T)) / (ulong)sizeof(T)] = TileData<T>.ptr[(ulong)sourceIndex * (ulong)((long)sizeof(T)) / (ulong)sizeof(T)];
		}

		// Token: 0x04000ED4 RID: 3796
		private static GCHandle handle;

		// Token: 0x020007F0 RID: 2032
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040067A4 RID: 26532
			public static Action<uint> <0>__SetLength;

			// Token: 0x040067A5 RID: 26533
			public static Action <1>__ClearEverything;

			// Token: 0x040067A6 RID: 26534
			public static Action<uint, uint> <2>__CopySingle;

			// Token: 0x040067A7 RID: 26535
			public static Action<uint> <3>__ClearSingle;
		}
	}
}
