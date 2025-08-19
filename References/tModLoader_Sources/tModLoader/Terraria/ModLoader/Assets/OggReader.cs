using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using NVorbis;
using ReLogic.Content;
using ReLogic.Content.Readers;

namespace Terraria.ModLoader.Assets
{
	// Token: 0x020003C7 RID: 967
	public class OggReader : IAssetReader
	{
		// Token: 0x06003322 RID: 13090 RVA: 0x00549134 File Offset: 0x00547334
		T IAssetReader.FromStream<T>(Stream stream)
		{
			if (typeof(T) != typeof(SoundEffect))
			{
				throw AssetLoadException.FromInvalidReader<OggReader, T>();
			}
			T result;
			using (VorbisReader reader = new VorbisReader(stream, true))
			{
				byte[] buffer = new byte[reader.TotalSamples * 2L * (long)reader.Channels];
				float[] floatBuf = new float[buffer.Length / 2];
				reader.ReadSamples(floatBuf, 0, floatBuf.Length);
				OggReader.Convert(floatBuf, buffer);
				result = (new SoundEffect(buffer, reader.SampleRate, reader.Channels) as T);
			}
			return result;
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x005491DC File Offset: 0x005473DC
		public static void Convert(float[] floatBuf, byte[] buffer)
		{
			for (int i = 0; i < floatBuf.Length; i++)
			{
				short val = (short)(floatBuf[i] * 32767f);
				buffer[i * 2] = (byte)val;
				buffer[i * 2 + 1] = (byte)(val >> 8);
			}
		}
	}
}
