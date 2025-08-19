using System;
using System.IO;
using ReLogic.Content;
using Terraria.GameContent;

namespace Terraria.IO
{
	// Token: 0x020000D2 RID: 210
	public class ResourcePackContentValidator
	{
		// Token: 0x0600149B RID: 5275 RVA: 0x004ADBD0 File Offset: 0x004ABDD0
		public void ValidateResourePack(ResourcePack pack)
		{
			if ((AssetReaderCollection)Main.instance.Services.GetService(typeof(AssetReaderCollection)) == null)
			{
				return;
			}
			pack.GetContentSource().GetAllAssetsStartingWith("Images" + Path.DirectorySeparatorChar.ToString());
			VanillaContentValidator.Instance.GetValidImageFilePaths();
		}
	}
}
