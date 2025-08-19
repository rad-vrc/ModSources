using System;
using System.IO;

namespace Terraria.Utilities
{
	// Token: 0x02000145 RID: 325
	public static class PlatformUtilities
	{
		// Token: 0x060018E7 RID: 6375 RVA: 0x004DF627 File Offset: 0x004DD827
		public static void SavePng(Stream stream, int width, int height, int imgWidth, int imgHeight, byte[] data)
		{
			throw new NotSupportedException("Use Bitmap to save png images on windows");
		}
	}
}
