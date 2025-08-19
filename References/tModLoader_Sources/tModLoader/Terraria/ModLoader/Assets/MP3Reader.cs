using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content;
using ReLogic.Content.Readers;
using XPT.Core.Audio.MP3Sharp;

namespace Terraria.ModLoader.Assets
{
	// Token: 0x020003C6 RID: 966
	public class MP3Reader : IAssetReader
	{
		// Token: 0x06003320 RID: 13088 RVA: 0x00549098 File Offset: 0x00547298
		T IAssetReader.FromStream<T>(Stream stream)
		{
			if (typeof(T) != typeof(SoundEffect))
			{
				throw AssetLoadException.FromInvalidReader<MP3Reader, T>();
			}
			T result;
			using (MP3Stream mp3Stream = new MP3Stream(stream))
			{
				using (MemoryStream ms = new MemoryStream())
				{
					mp3Stream.CopyTo(ms);
					result = (new SoundEffect(ms.ToArray(), mp3Stream.Frequency, 2) as T);
				}
			}
			return result;
		}
	}
}
