using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x0200075E RID: 1886
	public class CueAudioTrack : IAudioTrack, IDisposable
	{
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x0066D1BB File Offset: 0x0066B3BB
		public bool IsPlaying
		{
			get
			{
				return this._cue.IsPlaying;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x0066D1C8 File Offset: 0x0066B3C8
		public bool IsStopped
		{
			get
			{
				return this._cue.IsStopped;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06004C39 RID: 19513 RVA: 0x0066D1D5 File Offset: 0x0066B3D5
		public bool IsPaused
		{
			get
			{
				return this._cue.IsPaused;
			}
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x0066D1E2 File Offset: 0x0066B3E2
		public CueAudioTrack(SoundBank bank, string cueName)
		{
			this._soundBank = bank;
			this._cueName = cueName;
			this.Reuse();
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x0066D1FE File Offset: 0x0066B3FE
		public void Stop(AudioStopOptions options)
		{
			this._cue.Stop(options);
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x0066D20C File Offset: 0x0066B40C
		public void Play()
		{
			this._cue.Play();
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x0066D219 File Offset: 0x0066B419
		public void SetVariable(string variableName, float value)
		{
			this._cue.SetVariable(variableName, value);
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x0066D228 File Offset: 0x0066B428
		public void Resume()
		{
			this._cue.Resume();
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x0066D235 File Offset: 0x0066B435
		public void Reuse()
		{
			if (this._cue != null)
			{
				this.Stop(1);
			}
			this._cue = this._soundBank.GetCue(this._cueName);
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x0066D25D File Offset: 0x0066B45D
		public void Pause()
		{
			this._cue.Pause();
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0066D26A File Offset: 0x0066B46A
		public void Dispose()
		{
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0066D26C File Offset: 0x0066B46C
		public void Update()
		{
		}

		// Token: 0x040060B6 RID: 24758
		private Cue _cue;

		// Token: 0x040060B7 RID: 24759
		private string _cueName;

		// Token: 0x040060B8 RID: 24760
		private SoundBank _soundBank;
	}
}
