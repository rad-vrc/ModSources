using System;
using System.IO;
using System.Runtime.CompilerServices;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000355 RID: 853
	internal static class ContentConverters
	{
		// Token: 0x06002F95 RID: 12181 RVA: 0x00536410 File Offset: 0x00534610
		internal static bool Convert(ref string resourceName, FileStream src, MemoryStream dst)
		{
			if (!(Path.GetExtension(resourceName).ToLower() == ".png"))
			{
				return false;
			}
			if (resourceName != "icon.png" && ImageIO.ToRaw(src, dst))
			{
				resourceName = Path.ChangeExtension(resourceName, "rawimg");
				return true;
			}
			src.Position = 0L;
			return false;
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x00536468 File Offset: 0x00534668
		internal static bool Reverse(ref string resourceName, out Action<Stream, Stream> converter)
		{
			if (resourceName == "Info")
			{
				resourceName = "build.txt";
				Action<Stream, Stream> action;
				if ((action = ContentConverters.<>O.<0>__InfoToBuildTxt) == null)
				{
					action = (ContentConverters.<>O.<0>__InfoToBuildTxt = new Action<Stream, Stream>(BuildProperties.InfoToBuildTxt));
				}
				converter = action;
				return true;
			}
			if (Path.GetExtension(resourceName).ToLower() == ".rawimg")
			{
				resourceName = Path.ChangeExtension(resourceName, "png");
				Action<Stream, Stream> action2;
				if ((action2 = ContentConverters.<>O.<1>__RawToPng) == null)
				{
					action2 = (ContentConverters.<>O.<1>__RawToPng = new Action<Stream, Stream>(ImageIO.RawToPng));
				}
				converter = action2;
				return true;
			}
			converter = null;
			return false;
		}

		// Token: 0x02000AA8 RID: 2728
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006DC1 RID: 28097
			public static Action<Stream, Stream> <0>__InfoToBuildTxt;

			// Token: 0x04006DC2 RID: 28098
			public static Action<Stream, Stream> <1>__RawToPng;
		}
	}
}
