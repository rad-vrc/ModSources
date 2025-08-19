using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x02000480 RID: 1152
	public class CueAudioTrack : IAudioTrack, IDisposable
	{
		// Token: 0x06002DE0 RID: 11744 RVA: 0x005BF40B File Offset: 0x005BD60B
		public CueAudioTrack(SoundBank bank, string cueName)
		{
			this._soundBank = bank;
			this._cueName = cueName;
			this.Reuse();
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x005BF427 File Offset: 0x005BD627
		public bool IsPlaying
		{
			get
			{
				return this._cue.IsPlaying;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x005BF434 File Offset: 0x005BD634
		public bool IsStopped
		{
			get
			{
				return this._cue.IsStopped;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x005BF441 File Offset: 0x005BD641
		public bool IsPaused
		{
			get
			{
				return this._cue.IsPaused;
			}
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x005BF44E File Offset: 0x005BD64E
		public void Stop(AudioStopOptions options)
		{
			this._cue.Stop(options);
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x005BF45C File Offset: 0x005BD65C
		public void Play()
		{
			this._cue.Play();
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x005BF469 File Offset: 0x005BD669
		public void SetVariable(string variableName, float value)
		{
			this._cue.SetVariable(variableName, value);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x005BF478 File Offset: 0x005BD678
		public void Resume()
		{
			this._cue.Resume();
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x005BF485 File Offset: 0x005BD685
		public void Reuse()
		{
			if (this._cue != null)
			{
				this.Stop(AudioStopOptions.Immediate);
			}
			this._cue = this._soundBank.GetCue(this._cueName);
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x005BF4AD File Offset: 0x005BD6AD
		public void Pause()
		{
			this._cue.Pause();
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Dispose()
		{
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Update()
		{
		}

		// Token: 0x0400515A RID: 20826
		private Cue _cue;

		// Token: 0x0400515B RID: 20827
		private string _cueName;

		// Token: 0x0400515C RID: 20828
		private SoundBank _soundBank;
	}
}
