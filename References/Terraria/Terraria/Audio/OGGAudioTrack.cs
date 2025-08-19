using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using NVorbis;

namespace Terraria.Audio
{
	// Token: 0x02000486 RID: 1158
	public class OGGAudioTrack : ASoundEffectBasedAudioTrack
	{
		// Token: 0x06002E24 RID: 11812 RVA: 0x005C32A2 File Offset: 0x005C14A2
		public OGGAudioTrack(Stream streamToRead)
		{
			this._vorbisReader = new VorbisReader(streamToRead, true);
			this.FindLoops();
			base.CreateSoundEffect(this._vorbisReader.SampleRate, (AudioChannels)this._vorbisReader.Channels);
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x005C32D9 File Offset: 0x005C14D9
		protected override void ReadAheadPutAChunkIntoTheBuffer()
		{
			this.PrepareBufferToSubmit();
			this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x005C32F4 File Offset: 0x005C14F4
		private void PrepareBufferToSubmit()
		{
			byte[] bufferToSubmit = this._bufferToSubmit;
			float[] temporaryBuffer = this._temporaryBuffer;
			VorbisReader vorbisReader = this._vorbisReader;
			int num = vorbisReader.ReadSamples(temporaryBuffer, 0, temporaryBuffer.Length);
			bool flag = this._loopEnd > 0 && vorbisReader.DecodedPosition >= (long)this._loopEnd;
			bool flag2 = num < temporaryBuffer.Length;
			if (flag || flag2)
			{
				vorbisReader.DecodedPosition = (long)this._loopStart;
				vorbisReader.ReadSamples(temporaryBuffer, num, temporaryBuffer.Length - num);
			}
			OGGAudioTrack.ApplyTemporaryBufferTo(temporaryBuffer, bufferToSubmit);
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x005C3370 File Offset: 0x005C1570
		private static void ApplyTemporaryBufferTo(float[] temporaryBuffer, byte[] samplesBuffer)
		{
			for (int i = 0; i < temporaryBuffer.Length; i++)
			{
				short num = (short)(temporaryBuffer[i] * 32767f);
				samplesBuffer[i * 2] = (byte)num;
				samplesBuffer[i * 2 + 1] = (byte)(num >> 8);
			}
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x005C33A8 File Offset: 0x005C15A8
		public override void Reuse()
		{
			this._vorbisReader.SeekTo(0L, SeekOrigin.Begin);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x005C33B8 File Offset: 0x005C15B8
		private void FindLoops()
		{
			IDictionary<string, IList<string>> all = this._vorbisReader.Tags.All;
			this.TryReadingTag(all, "LOOPSTART", ref this._loopStart);
			this.TryReadingTag(all, "LOOPEND", ref this._loopEnd);
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x005C33FC File Offset: 0x005C15FC
		private void TryReadingTag(IDictionary<string, IList<string>> tags, string entryName, ref int result)
		{
			IList<string> list;
			int num;
			if (tags.TryGetValue(entryName, out list) && list.Count > 0 && int.TryParse(list[0], out num))
			{
				result = num;
			}
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x005C3430 File Offset: 0x005C1630
		public override void Dispose()
		{
			this._soundEffectInstance.Dispose();
			this._vorbisReader.Dispose();
		}

		// Token: 0x040051B8 RID: 20920
		private VorbisReader _vorbisReader;

		// Token: 0x040051B9 RID: 20921
		private int _loopStart;

		// Token: 0x040051BA RID: 20922
		private int _loopEnd;
	}
}
