using System;

namespace Terraria
{
	// Token: 0x0200005C RID: 92
	internal static class TileData
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x003FD0A4 File Offset: 0x003FB2A4
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x003FD0AB File Offset: 0x003FB2AB
		internal static uint Length { get; private set; }

		// Token: 0x06000FB1 RID: 4017 RVA: 0x003FD0B3 File Offset: 0x003FB2B3
		internal static void SetLength(uint len)
		{
			TileData.Length = len;
			Action<uint> onSetLength = TileData.OnSetLength;
			if (onSetLength == null)
			{
				return;
			}
			onSetLength(len);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x003FD0CB File Offset: 0x003FB2CB
		internal static void ClearEverything()
		{
			Action onClearEverything = TileData.OnClearEverything;
			if (onClearEverything == null)
			{
				return;
			}
			onClearEverything();
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x003FD0DC File Offset: 0x003FB2DC
		internal static void ClearSingle(uint index)
		{
			Action<uint> onClearSingle = TileData.OnClearSingle;
			if (onClearSingle == null)
			{
				return;
			}
			onClearSingle(index);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x003FD0EE File Offset: 0x003FB2EE
		internal static void CopySingle(uint sourceIndex, uint destinationIndex)
		{
			Action<uint, uint> onCopySingle = TileData.OnCopySingle;
			if (onCopySingle == null)
			{
				return;
			}
			onCopySingle(sourceIndex, destinationIndex);
		}

		// Token: 0x04000ECD RID: 3789
		internal static Action OnClearEverything;

		// Token: 0x04000ECE RID: 3790
		internal static Action<uint> OnSetLength;

		// Token: 0x04000ECF RID: 3791
		internal static Action<uint> OnClearSingle;

		// Token: 0x04000ED0 RID: 3792
		internal static Action<uint, uint> OnCopySingle;
	}
}
