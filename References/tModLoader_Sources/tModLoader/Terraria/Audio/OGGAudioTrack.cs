using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using NVorbis;

namespace Terraria.Audio
{
	// Token: 0x02000767 RID: 1895
	public class OGGAudioTrack : ASoundEffectBasedAudioTrack
	{
		// Token: 0x06004C9B RID: 19611 RVA: 0x00670697 File Offset: 0x0066E897
		public OGGAudioTrack(Stream streamToRead)
		{
			this._vorbisReader = new VorbisReader(streamToRead, true);
			this.FindLoops();
			base.CreateSoundEffect(this._vorbisReader.SampleRate, this._vorbisReader.Channels);
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x006706CE File Offset: 0x0066E8CE
		protected override void ReadAheadPutAChunkIntoTheBuffer()
		{
			this.PrepareBufferToSubmit();
			this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x006706E8 File Offset: 0x0066E8E8
		private void PrepareBufferToSubmit()
		{
			byte[] bufferToSubmit = this._bufferToSubmit;
			float[] temporaryBuffer = this._temporaryBuffer;
			VorbisReader vorbisReader = this._vorbisReader;
			int num = vorbisReader.ReadSamples(temporaryBuffer, 0, temporaryBuffer.Length);
			bool flag2 = this._loopEnd > 0 && vorbisReader.DecodedPosition >= (long)this._loopEnd;
			bool flag = num < temporaryBuffer.Length;
			if (flag2 || flag)
			{
				vorbisReader.DecodedPosition = (long)this._loopStart;
				vorbisReader.ReadSamples(temporaryBuffer, num, temporaryBuffer.Length - num);
			}
			OGGAudioTrack.ApplyTemporaryBufferTo(temporaryBuffer, bufferToSubmit);
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x00670764 File Offset: 0x0066E964
		private static void ApplyTemporaryBufferTo(float[] temporaryBuffer, byte[] samplesBuffer)
		{
			for (int i = 0; i < temporaryBuffer.Length; i++)
			{
				short num = (short)(temporaryBuffer[i] * 32767f);
				samplesBuffer[i * 2] = (byte)num;
				samplesBuffer[i * 2 + 1] = (byte)(num >> 8);
			}
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x0067079C File Offset: 0x0066E99C
		public override void Reuse()
		{
			this._vorbisReader.SeekTo(0L, SeekOrigin.Begin);
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x006707AC File Offset: 0x0066E9AC
		private void FindLoops()
		{
			OGGAudioTrack.<>c__DisplayClass8_0 CS$<>8__locals1;
			CS$<>8__locals1.tags = this._vorbisReader.Tags.All;
			OGGAudioTrack.<FindLoops>g__TryReadInteger|8_0("LOOPSTART", ref this._loopStart, ref CS$<>8__locals1);
			OGGAudioTrack.<FindLoops>g__TryReadInteger|8_0("LOOPEND", ref this._loopEnd, ref CS$<>8__locals1);
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x006707F4 File Offset: 0x0066E9F4
		public override void Dispose()
		{
			this._soundEffectInstance.Dispose();
			this._vorbisReader.Dispose();
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x0067080C File Offset: 0x0066EA0C
		[CompilerGenerated]
		internal static void <FindLoops>g__TryReadInteger|8_0(string entryName, ref int result, ref OGGAudioTrack.<>c__DisplayClass8_0 A_2)
		{
			IList<string> values;
			int potentialResult;
			if (A_2.tags.TryGetValue(entryName, out values) && values.Count > 0 && int.TryParse(values[0], out potentialResult))
			{
				result = potentialResult;
			}
		}

		// Token: 0x04006111 RID: 24849
		private VorbisReader _vorbisReader;

		// Token: 0x04006112 RID: 24850
		private int _loopStart;

		// Token: 0x04006113 RID: 24851
		private int _loopEnd;
	}
}
