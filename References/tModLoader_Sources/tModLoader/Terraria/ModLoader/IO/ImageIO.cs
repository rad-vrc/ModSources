using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000281 RID: 641
	public class ImageIO
	{
		// Token: 0x06002BBB RID: 11195 RVA: 0x0052379C File Offset: 0x0052199C
		public unsafe static bool ToRaw(Stream src, Stream dst)
		{
			int width;
			int height;
			int len;
			IntPtr img = FNA3D.ReadImageStream(src, ref width, ref height, ref len, -1, -1, false);
			if (img == IntPtr.Zero)
			{
				return false;
			}
			byte* colors = (byte*)img.ToPointer();
			bool result;
			using (BinaryWriter w = new BinaryWriter(dst))
			{
				w.Write(1);
				w.Write(width);
				w.Write(height);
				for (int i = 0; i < len; i += 4)
				{
					if (colors[i + 3] == 0)
					{
						w.Write(0);
					}
					else
					{
						w.Write(colors[i]);
						w.Write(colors[i + 1]);
						w.Write(colors[i + 2]);
						w.Write(colors[i + 3]);
					}
				}
				FNA3D.FNA3D_Image_Free(img);
				result = true;
			}
			return result;
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x00523874 File Offset: 0x00521A74
		public unsafe static void RawToPng(Stream src, Stream dst)
		{
			int width;
			int height;
			byte[] array;
			byte* pixels;
			if ((array = ImageIO.ReadRaw(src, out width, out height)) == null || array.Length == 0)
			{
				pixels = null;
			}
			else
			{
				pixels = &array[0];
			}
			FNA3D.WritePNGStream(dst, width, height, width, height, pixels);
			array = null;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x005238B0 File Offset: 0x00521AB0
		public static byte[] ReadRaw(Stream stream, out int width, out int height)
		{
			byte[] result;
			using (BinaryReader r = new BinaryReader(stream))
			{
				int v = r.ReadInt32();
				if (v != 1)
				{
					throw new Exception("Unknown RawImg Format Version: " + v.ToString());
				}
				width = r.ReadInt32();
				height = r.ReadInt32();
				result = r.ReadBytes(width * height * 4);
			}
			return result;
		}

		// Token: 0x04001BF0 RID: 7152
		public const int VERSION = 1;
	}
}
