using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent
{
	// Token: 0x020004C3 RID: 1219
	public class VanillaContentValidator : IContentValidator
	{
		// Token: 0x06003A5B RID: 14939 RVA: 0x005A7E3C File Offset: 0x005A603C
		public VanillaContentValidator(string infoFilePath)
		{
			foreach (string text in Regex.Split(Utils.ReadEmbeddedResource(infoFilePath), "\r\n|\r|\n"))
			{
				if (!text.StartsWith("//"))
				{
					string[] array2 = text.Split('\t', StringSplitOptions.None);
					int result;
					int result2;
					if (array2.Length >= 3 && int.TryParse(array2[1], out result) && int.TryParse(array2[2], out result2))
					{
						string key = array2[0].Replace('/', '\\');
						this._info[key] = new VanillaContentValidator.TextureMetaData
						{
							Width = result,
							Height = result2
						};
					}
				}
			}
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x005A7EEC File Offset: 0x005A60EC
		public bool AssetIsValid<T>(T content, string contentPath, out IRejectionReason rejectReason) where T : class
		{
			Texture2D texture2D = content as Texture2D;
			rejectReason = null;
			VanillaContentValidator.TextureMetaData value;
			return texture2D == null || !this._info.TryGetValue(contentPath, out value) || value.Matches(texture2D, out rejectReason);
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x005A7F28 File Offset: 0x005A6128
		public HashSet<string> GetValidImageFilePaths()
		{
			return new HashSet<string>(from x in this._info
			select x.Key);
		}

		// Token: 0x040053FA RID: 21498
		public static VanillaContentValidator Instance;

		// Token: 0x040053FB RID: 21499
		private Dictionary<string, VanillaContentValidator.TextureMetaData> _info = new Dictionary<string, VanillaContentValidator.TextureMetaData>();

		// Token: 0x02000BCC RID: 3020
		private struct TextureMetaData
		{
			// Token: 0x06005DDA RID: 24026 RVA: 0x006C9938 File Offset: 0x006C7B38
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

			// Token: 0x04007729 RID: 30505
			public int Width;

			// Token: 0x0400772A RID: 30506
			public int Height;
		}
	}
}
