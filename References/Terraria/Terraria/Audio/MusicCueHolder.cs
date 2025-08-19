using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x02000485 RID: 1157
	public class MusicCueHolder
	{
		// Token: 0x06002E18 RID: 11800 RVA: 0x005C30B0 File Offset: 0x005C12B0
		public MusicCueHolder(SoundBank soundBank, string cueName)
		{
			this._soundBank = soundBank;
			this._cueName = cueName;
			this._loadedCue = null;
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x005C30D0 File Offset: 0x005C12D0
		public void Pause()
		{
			if (this._loadedCue == null)
			{
				return;
			}
			if (this._loadedCue.IsPaused)
			{
				return;
			}
			if (!this._loadedCue.IsPlaying)
			{
				return;
			}
			try
			{
				this._loadedCue.Pause();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x005C3124 File Offset: 0x005C1324
		public void Resume()
		{
			if (this._loadedCue == null)
			{
				return;
			}
			if (!this._loadedCue.IsPaused)
			{
				return;
			}
			try
			{
				this._loadedCue.Resume();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x005C316C File Offset: 0x005C136C
		public void Stop()
		{
			if (this._loadedCue == null)
			{
				return;
			}
			this.SetVolume(0f);
			this._loadedCue.Stop(AudioStopOptions.Immediate);
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x005C318E File Offset: 0x005C138E
		public bool IsPlaying
		{
			get
			{
				return this._loadedCue != null && this._loadedCue.IsPlaying;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x005C31A5 File Offset: 0x005C13A5
		public bool IsOngoing
		{
			get
			{
				return this._loadedCue != null && (this._loadedCue.IsPlaying || !this._loadedCue.IsStopped);
			}
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x005C31CE File Offset: 0x005C13CE
		public void RestartAndTryPlaying(float volume)
		{
			this.PurgeCue();
			this.TryPlaying(volume);
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x005C31DD File Offset: 0x005C13DD
		private void PurgeCue()
		{
			if (this._loadedCue == null)
			{
				return;
			}
			this._loadedCue.Stop(AudioStopOptions.Immediate);
			this._loadedCue.Dispose();
			this._loadedCue = null;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x005C3206 File Offset: 0x005C1406
		public void Play(float volume)
		{
			this.LoadTrack(false);
			this.SetVolume(volume);
			this._loadedCue.Play();
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x005C3221 File Offset: 0x005C1421
		public void TryPlaying(float volume)
		{
			this.LoadTrack(false);
			if (!this._loadedCue.IsPrepared)
			{
				return;
			}
			this.SetVolume(volume);
			if (!this._loadedCue.IsPlaying)
			{
				this._loadedCue.Play();
			}
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x005C3257 File Offset: 0x005C1457
		public void SetVolume(float volume)
		{
			this._lastSetVolume = volume;
			if (this._loadedCue != null)
			{
				this._loadedCue.SetVariable("Volume", this._lastSetVolume);
			}
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x005C327E File Offset: 0x005C147E
		private void LoadTrack(bool forceReload)
		{
			if (forceReload || this._loadedCue == null)
			{
				this._loadedCue = this._soundBank.GetCue(this._cueName);
			}
		}

		// Token: 0x040051B4 RID: 20916
		private SoundBank _soundBank;

		// Token: 0x040051B5 RID: 20917
		private string _cueName;

		// Token: 0x040051B6 RID: 20918
		private Cue _loadedCue;

		// Token: 0x040051B7 RID: 20919
		private float _lastSetVolume;
	}
}
