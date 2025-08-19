using System;
using System.IO;
using System.Linq;
using ReLogic.Content.Readers;
using ReLogic.Content.Sources;
using Terraria.Initializers;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader.Assets
{
	// Token: 0x020003CA RID: 970
	public class TModContentSource : ContentSource
	{
		// Token: 0x06003329 RID: 13097 RVA: 0x005492BC File Offset: 0x005474BC
		public TModContentSource(TmodFile file)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			this.file = file;
			if (Main.dedServ)
			{
				return;
			}
			base.SetAssetNames((from fileEntry in file
			select fileEntry.Name).Where(delegate(string name)
			{
				IAssetReader assetReader;
				return AssetInitializer.assetReaderCollection.TryGetReader(Path.GetExtension(name), ref assetReader);
			}));
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x0054933C File Offset: 0x0054753C
		public override Stream OpenStream(string assetName)
		{
			return this.file.GetStream(assetName, true);
		}

		// Token: 0x04001DF6 RID: 7670
		private readonly TmodFile file;
	}
}
