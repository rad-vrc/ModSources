using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using XPT.Core.Audio.MP3Sharp;

namespace Terraria.Audio
{
	// Token: 0x02000484 RID: 1156
	public class MP3AudioTrack : ASoundEffectBasedAudioTrack
	{
		// Token: 0x06002E14 RID: 11796 RVA: 0x005C3004 File Offset: 0x005C1204
		public MP3AudioTrack(Stream stream)
		{
			this._stream = stream;
			MP3Stream mp3Stream = new MP3Stream(stream);
			int frequency = mp3Stream.Frequency;
			this._mp3Stream = mp3Stream;
			base.CreateSoundEffect(frequency, AudioChannels.Stereo);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x005C303B File Offset: 0x005C123B
		public override void Reuse()
		{
			this._mp3Stream.Position = 0L;
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x005C304A File Offset: 0x005C124A
		public override void Dispose()
		{
			this._soundEffectInstance.Dispose();
			this._mp3Stream.Dispose();
			this._stream.Dispose();
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x005C3070 File Offset: 0x005C1270
		protected override void ReadAheadPutAChunkIntoTheBuffer()
		{
			byte[] bufferToSubmit = this._bufferToSubmit;
			if (this._mp3Stream.Read(bufferToSubmit, 0, bufferToSubmit.Length) < 1)
			{
				base.Stop(AudioStopOptions.Immediate);
				return;
			}
			this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
		}

		// Token: 0x040051B2 RID: 20914
		private Stream _stream;

		// Token: 0x040051B3 RID: 20915
		private MP3Stream _mp3Stream;
	}
}
