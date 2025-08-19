using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x0200075D RID: 1885
	public abstract class ASoundEffectBasedAudioTrack : IAudioTrack, IDisposable
	{
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06004C24 RID: 19492 RVA: 0x0066CFB8 File Offset: 0x0066B1B8
		public bool IsPlaying
		{
			get
			{
				return this._soundEffectInstance.State == 0;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06004C25 RID: 19493 RVA: 0x0066CFC8 File Offset: 0x0066B1C8
		public bool IsStopped
		{
			get
			{
				return this._soundEffectInstance.State == 2;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06004C26 RID: 19494 RVA: 0x0066CFD8 File Offset: 0x0066B1D8
		public bool IsPaused
		{
			get
			{
				return this._soundEffectInstance.State == 1;
			}
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0066CFE8 File Offset: 0x0066B1E8
		public ASoundEffectBasedAudioTrack()
		{
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0066D010 File Offset: 0x0066B210
		protected void CreateSoundEffect(int sampleRate, AudioChannels channels)
		{
			this._sampleRate = sampleRate;
			this._channels = channels;
			this._soundEffectInstance = new DynamicSoundEffectInstance(this._sampleRate, this._channels);
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x0066D037 File Offset: 0x0066B237
		private void _soundEffectInstance_BufferNeeded(object sender, EventArgs e)
		{
			this.PrepareBuffer();
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x0066D03F File Offset: 0x0066B23F
		public void Update()
		{
			if (this.IsPlaying && this._soundEffectInstance.PendingBufferCount < 8)
			{
				this.PrepareBuffer();
			}
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x0066D060 File Offset: 0x0066B260
		protected void ResetBuffer()
		{
			for (int i = 0; i < this._bufferToSubmit.Length; i++)
			{
				this._bufferToSubmit[i] = 0;
			}
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x0066D08C File Offset: 0x0066B28C
		protected void PrepareBuffer()
		{
			for (int i = 0; i < 2; i++)
			{
				this.ReadAheadPutAChunkIntoTheBuffer();
			}
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x0066D0AB File Offset: 0x0066B2AB
		public void Stop(AudioStopOptions options)
		{
			this._soundEffectInstance.Stop(options == 1);
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x0066D0BC File Offset: 0x0066B2BC
		public void Play()
		{
			this.PrepareToPlay();
			this._soundEffectInstance.Play();
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x0066D0CF File Offset: 0x0066B2CF
		public void Pause()
		{
			this._soundEffectInstance.Pause();
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x0066D0DC File Offset: 0x0066B2DC
		public void SetVariable(string variableName, float value)
		{
			if (variableName == "Volume")
			{
				float volume = this.ReMapVolumeToMatchXact(value);
				this._soundEffectInstance.Volume = volume;
				return;
			}
			if (variableName == "VolumeDirect")
			{
				this._soundEffectInstance.Volume = value;
				return;
			}
			if (variableName == "Pitch")
			{
				this._soundEffectInstance.Pitch = value;
				return;
			}
			if (!(variableName == "Pan"))
			{
				return;
			}
			this._soundEffectInstance.Pan = value;
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x0066D15C File Offset: 0x0066B35C
		private float ReMapVolumeToMatchXact(float musicVolume)
		{
			double num = 31.0 * (double)musicVolume - 25.0 - 11.94;
			return (float)Math.Pow(10.0, num / 20.0);
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x0066D1A4 File Offset: 0x0066B3A4
		public void Resume()
		{
			this._soundEffectInstance.Resume();
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x0066D1B1 File Offset: 0x0066B3B1
		protected virtual void PrepareToPlay()
		{
			this.ResetBuffer();
		}

		// Token: 0x06004C34 RID: 19508
		public abstract void Reuse();

		// Token: 0x06004C35 RID: 19509 RVA: 0x0066D1B9 File Offset: 0x0066B3B9
		protected virtual void ReadAheadPutAChunkIntoTheBuffer()
		{
		}

		// Token: 0x06004C36 RID: 19510
		public abstract void Dispose();

		// Token: 0x040060AE RID: 24750
		protected const int bufferLength = 4096;

		// Token: 0x040060AF RID: 24751
		protected const int bufferCountPerSubmit = 2;

		// Token: 0x040060B0 RID: 24752
		protected const int buffersToCoverFor = 8;

		// Token: 0x040060B1 RID: 24753
		protected byte[] _bufferToSubmit = new byte[4096];

		// Token: 0x040060B2 RID: 24754
		protected float[] _temporaryBuffer = new float[2048];

		// Token: 0x040060B3 RID: 24755
		private int _sampleRate;

		// Token: 0x040060B4 RID: 24756
		private AudioChannels _channels;

		// Token: 0x040060B5 RID: 24757
		protected DynamicSoundEffectInstance _soundEffectInstance;
	}
}
