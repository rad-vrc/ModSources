using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Utilities
{
	// Token: 0x02000091 RID: 145
	public static class PlatformUtilities
	{
		// Token: 0x06001487 RID: 5255 RVA: 0x004A277C File Offset: 0x004A097C
		public unsafe static void SavePng(Stream stream, int width, int height, int imgWidth, int imgHeight, byte[] data)
		{
			if (width * height * 4 > data.Length)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 3);
				defaultInterpolatedStringHandler.AppendLiteral("Data length ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(data.Length);
				defaultInterpolatedStringHandler.AppendLiteral(" must be >= width(");
				defaultInterpolatedStringHandler.AppendFormatted<int>(width);
				defaultInterpolatedStringHandler.AppendLiteral(")*height(");
				defaultInterpolatedStringHandler.AppendFormatted<int>(height);
				defaultInterpolatedStringHandler.AppendLiteral(")*4");
				throw new IndexOutOfRangeException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			fixed (byte[] array = data)
			{
				byte* ptr;
				if (data == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				FNA3D.WritePNGStream(stream, width, height, imgWidth, imgHeight, ptr);
			}
		}
	}
}
