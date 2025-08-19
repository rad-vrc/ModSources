using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x0200048B RID: 1163
	public class WAVAudioTrack : ASoundEffectBasedAudioTrack
	{
		// Token: 0x06002E43 RID: 11843 RVA: 0x005C38A8 File Offset: 0x005C1AA8
		public WAVAudioTrack(Stream stream)
		{
			this._stream = stream;
			BinaryReader binaryReader = new BinaryReader(stream);
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			AudioChannels channels = AudioChannels.Mono;
			uint sampleRate = 0U;
			bool flag = false;
			int num = 0;
			while (!flag && num < 10)
			{
				uint num2 = binaryReader.ReadUInt32();
				int chunkSize = binaryReader.ReadInt32();
				if (num2 != 544501094U)
				{
					if (num2 == 1263424842U)
					{
						WAVAudioTrack.SkipJunk(binaryReader, chunkSize);
					}
				}
				else
				{
					binaryReader.ReadInt16();
					channels = (AudioChannels)binaryReader.ReadUInt16();
					sampleRate = binaryReader.ReadUInt32();
					binaryReader.ReadInt32();
					binaryReader.ReadInt16();
					binaryReader.ReadInt16();
					flag = true;
				}
				if (!flag)
				{
					num++;
				}
			}
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			this._streamContentStartIndex = stream.Position;
			base.CreateSoundEffect((int)sampleRate, channels);
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x005C3980 File Offset: 0x005C1B80
		private static void SkipJunk(BinaryReader reader, int chunkSize)
		{
			int num = chunkSize;
			if (num % 2 != 0)
			{
				num++;
			}
			reader.ReadBytes(num);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x005C39A0 File Offset: 0x005C1BA0
		protected override void ReadAheadPutAChunkIntoTheBuffer()
		{
			byte[] bufferToSubmit = this._bufferToSubmit;
			if (this._stream.Read(bufferToSubmit, 0, bufferToSubmit.Length) < 1)
			{
				base.Stop(AudioStopOptions.Immediate);
				return;
			}
			this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x005C39E0 File Offset: 0x005C1BE0
		public override void Reuse()
		{
			this._stream.Position = this._streamContentStartIndex;
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x005C39F3 File Offset: 0x005C1BF3
		public override void Dispose()
		{
			this._soundEffectInstance.Dispose();
			this._stream.Dispose();
		}

		// Token: 0x040051C5 RID: 20933
		private long _streamContentStartIndex = -1L;

		// Token: 0x040051C6 RID: 20934
		private Stream _stream;

		// Token: 0x040051C7 RID: 20935
		private const uint JUNK = 1263424842U;

		// Token: 0x040051C8 RID: 20936
		private const uint FMT = 544501094U;
	}
}
