using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content;
using ReLogic.Content.Readers;

namespace Terraria.ModLoader.Assets
{
	// Token: 0x020003C9 RID: 969
	public class WavReader : IAssetReader
	{
		// Token: 0x06003327 RID: 13095 RVA: 0x0054927F File Offset: 0x0054747F
		T IAssetReader.FromStream<T>(Stream stream)
		{
			if (typeof(T) != typeof(SoundEffect))
			{
				throw AssetLoadException.FromInvalidReader<WavReader, T>();
			}
			return SoundEffect.FromStream(stream) as T;
		}
	}
}
