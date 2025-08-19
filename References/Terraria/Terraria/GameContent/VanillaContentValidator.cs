using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent
{
	// Token: 0x020001F3 RID: 499
	public class VanillaContentValidator : IContentValidator
	{
		// Token: 0x06001CEF RID: 7407 RVA: 0x004FE780 File Offset: 0x004FC980
		public VanillaContentValidator(string infoFilePath)
		{
			foreach (string text in Regex.Split(Utils.ReadEmbeddedResource(infoFilePath), "\r\n|\r|\n"))
			{
				if (!text.StartsWith("//"))
				{
					string[] array2 = text.Split(new char[]
					{
						'\t'
					});
					int width;
					int height;
					if (array2.Length >= 3 && int.TryParse(array2[1], out width) && int.TryParse(array2[2], out height))
					{
						string key = array2[0].Replace('/', '\\');
						this._info[key] = new VanillaContentValidator.TextureMetaData
						{
							Width = width,
							Height = height
						};
					}
				}
			}
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x004FE838 File Offset: 0x004FCA38
		public bool AssetIsValid<T>(T content, string contentPath, out IRejectionReason rejectReason) where T : class
		{
			Texture2D texture2D = content as Texture2D;
			rejectReason = null;
			VanillaContentValidator.TextureMetaData textureMetaData;
			return texture2D == null || !this._info.TryGetValue(contentPath, out textureMetaData) || textureMetaData.Matches(texture2D, out rejectReason);
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x004FE874 File Offset: 0x004FCA74
		public HashSet<string> GetValidImageFilePaths()
		{
			return new HashSet<string>(from x in this._info
			select x.Key);
		}

		// Token: 0x040043E0 RID: 17376
		public static VanillaContentValidator Instance;

		// Token: 0x040043E1 RID: 17377
		private Dictionary<string, VanillaContentValidator.TextureMetaData> _info = new Dictionary<string, VanillaContentValidator.TextureMetaData>();

		// Token: 0x0200060F RID: 1551
		private struct TextureMetaData
		{
			// Token: 0x06003359 RID: 13145 RVA: 0x00605C2C File Offset: 0x00603E2C
			public bool Matches(Texture2D texture, out IRejectionReason rejectReason)
			{
				if (texture.Width != this.Width || texture.Height != this.Height)
				{
					rejectReason = new ContentRejectionFromSize(this.Width, this.Height, texture.Width, texture.Height);
					return false;
				}
				rejectReason = null;
				return true;
			}

			// Token: 0x04006069 RID: 24681
			public int Width;

			// Token: 0x0400606A RID: 24682
			public int Height;
		}
	}
}
