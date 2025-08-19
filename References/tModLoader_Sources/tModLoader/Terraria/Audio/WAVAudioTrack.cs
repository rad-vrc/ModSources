using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x02000770 RID: 1904
	public class WAVAudioTrack : ASoundEffectBasedAudioTrack
	{
		// Token: 0x06004D08 RID: 19720 RVA: 0x0067223C File Offset: 0x0067043C
		public WAVAudioTrack(Stream stream)
		{
			this._stream = stream;
			BinaryReader binaryReader = new BinaryReader(stream);
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			AudioChannels channels = 1;
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
					channels = binaryReader.ReadUInt16();
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

		// Token: 0x06004D09 RID: 19721 RVA: 0x00672314 File Offset: 0x00670514
		private static void SkipJunk(BinaryReader reader, int chunkSize)
		{
			int num = chunkSize;
			if (num % 2 != 0)
			{
				num++;
			}
			reader.ReadBytes(num);
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x00672334 File Offset: 0x00670534
		protected override void ReadAheadPutAChunkIntoTheBuffer()
		{
			byte[] bufferToSubmit = this._bufferToSubmit;
			if (this._stream.Read(bufferToSubmit, 0, bufferToSubmit.Length) < 1)
			{
				base.Stop(1);
				return;
			}
			this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x00672374 File Offset: 0x00670574
		public override void Reuse()
		{
			this._stream.Position = this._streamContentStartIndex;
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x00672387 File Offset: 0x00670587
		public override void Dispose()
		{
			this._soundEffectInstance.Dispose();
			this._stream.Dispose();
		}

		// Token: 0x0400613C RID: 24892
		private long _streamContentStartIndex = -1L;

		// Token: 0x0400613D RID: 24893
		private Stream _stream;

		// Token: 0x0400613E RID: 24894
		private const uint JUNK = 1263424842U;

		// Token: 0x0400613F RID: 24895
		private const uint FMT = 544501094U;
	}
}
