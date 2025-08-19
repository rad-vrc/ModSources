using System;
using System.IO;
using XPT.Core.Audio.MP3Sharp;

namespace Terraria.Audio
{
	// Token: 0x02000765 RID: 1893
	public class MP3AudioTrack : ASoundEffectBasedAudioTrack
	{
		// Token: 0x06004C8B RID: 19595 RVA: 0x00670400 File Offset: 0x0066E600
		public MP3AudioTrack(Stream stream)
		{
			this._stream = stream;
			MP3Stream mP3Stream = new MP3Stream(stream);
			int frequency = mP3Stream.Frequency;
			this._mp3Stream = mP3Stream;
			base.CreateSoundEffect(frequency, 2);
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x00670437 File Offset: 0x0066E637
		public override void Reuse()
		{
			this._mp3Stream.Position = 0L;
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x00670446 File Offset: 0x0066E646
		public override void Dispose()
		{
			this._soundEffectInstance.Dispose();
			this._mp3Stream.Dispose();
			this._stream.Dispose();
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x0067046C File Offset: 0x0066E66C
		protected override void ReadAheadPutAChunkIntoTheBuffer()
		{
			byte[] bufferToSubmit = this._bufferToSubmit;
			if (this._mp3Stream.Read(bufferToSubmit, 0, bufferToSubmit.Length) < 1)
			{
				base.Stop(1);
				return;
			}
			this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
		}

		// Token: 0x0400610B RID: 24843
		private Stream _stream;

		// Token: 0x0400610C RID: 24844
		private MP3Stream _mp3Stream;
	}
}
