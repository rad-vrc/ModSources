using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x02000766 RID: 1894
	public class MusicCueHolder
	{
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06004C8F RID: 19599 RVA: 0x006704AC File Offset: 0x0066E6AC
		public bool IsPlaying
		{
			get
			{
				return this._loadedCue != null && this._loadedCue.IsPlaying;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x006704C3 File Offset: 0x0066E6C3
		public bool IsOngoing
		{
			get
			{
				return this._loadedCue != null && (this._loadedCue.IsPlaying || !this._loadedCue.IsStopped);
			}
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x006704EC File Offset: 0x0066E6EC
		public MusicCueHolder(SoundBank soundBank, string cueName)
		{
			this._soundBank = soundBank;
			this._cueName = cueName;
			this._loadedCue = null;
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x0067050C File Offset: 0x0066E70C
		public void Pause()
		{
			if (this._loadedCue == null || this._loadedCue.IsPaused || !this._loadedCue.IsPlaying)
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

		// Token: 0x06004C93 RID: 19603 RVA: 0x00670560 File Offset: 0x0066E760
		public void Resume()
		{
			if (this._loadedCue == null || !this._loadedCue.IsPaused)
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

		// Token: 0x06004C94 RID: 19604 RVA: 0x006705A4 File Offset: 0x0066E7A4
		public void Stop()
		{
			if (this._loadedCue != null)
			{
				this.SetVolume(0f);
				this._loadedCue.Stop(1);
			}
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x006705C5 File Offset: 0x0066E7C5
		public void RestartAndTryPlaying(float volume)
		{
			this.PurgeCue();
			this.TryPlaying(volume);
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x006705D4 File Offset: 0x0066E7D4
		private void PurgeCue()
		{
			if (this._loadedCue != null)
			{
				this._loadedCue.Stop(1);
				this._loadedCue.Dispose();
				this._loadedCue = null;
			}
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x006705FC File Offset: 0x0066E7FC
		public void Play(float volume)
		{
			this.LoadTrack(false);
			this.SetVolume(volume);
			this._loadedCue.Play();
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x00670617 File Offset: 0x0066E817
		public void TryPlaying(float volume)
		{
			this.LoadTrack(false);
			if (this._loadedCue.IsPrepared)
			{
				this.SetVolume(volume);
				if (!this._loadedCue.IsPlaying)
				{
					this._loadedCue.Play();
				}
			}
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x0067064C File Offset: 0x0066E84C
		public void SetVolume(float volume)
		{
			this._lastSetVolume = volume;
			if (this._loadedCue != null)
			{
				this._loadedCue.SetVariable("Volume", this._lastSetVolume);
			}
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x00670673 File Offset: 0x0066E873
		private void LoadTrack(bool forceReload)
		{
			if (forceReload || this._loadedCue == null)
			{
				this._loadedCue = this._soundBank.GetCue(this._cueName);
			}
		}

		// Token: 0x0400610D RID: 24845
		private SoundBank _soundBank;

		// Token: 0x0400610E RID: 24846
		private string _cueName;

		// Token: 0x0400610F RID: 24847
		private Cue _loadedCue;

		// Token: 0x04006110 RID: 24848
		private float _lastSetVolume;
	}
}
