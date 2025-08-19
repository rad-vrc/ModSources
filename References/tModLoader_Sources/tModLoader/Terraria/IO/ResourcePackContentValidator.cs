using System;
using System.IO;
using ReLogic.Content;
using Terraria.GameContent;

namespace Terraria.IO
{
	// Token: 0x020003E3 RID: 995
	public class ResourcePackContentValidator
	{
		// Token: 0x06003436 RID: 13366 RVA: 0x00557258 File Offset: 0x00555458
		public void ValidateResourePack(ResourcePack pack)
		{
			if ((AssetReaderCollection)Main.instance.Services.GetService(typeof(AssetReaderCollection)) != null)
			{
				pack.GetContentSource().GetAllAssetsStartingWith("Images" + Path.DirectorySeparatorChar.ToString(), false);
				VanillaContentValidator.Instance.GetValidImageFilePaths();
			}
		}
	}
}
