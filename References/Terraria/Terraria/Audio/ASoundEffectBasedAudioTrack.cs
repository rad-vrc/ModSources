using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x0200047B RID: 1147
	public abstract class ASoundEffectBasedAudioTrack : IAudioTrack, IDisposable
	{
		// Token: 0x06002DA0 RID: 11680 RVA: 0x005BF1C7 File Offset: 0x005BD3C7
		public ASoundEffectBasedAudioTrack()
		{
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x005BF1EF File Offset: 0x005BD3EF
		protected void CreateSoundEffect(int sampleRate, AudioChannels channels)
		{
			this._sampleRate = sampleRate;
			this._channels = channels;
			this._soundEffectInstance = new DynamicSoundEffectInstance(this._sampleRate, this._channels);
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x005BF216 File Offset: 0x005BD416
		private void _soundEffectInstance_BufferNeeded(object sender, EventArgs e)
		{
			this.PrepareBuffer();
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x005BF21E File Offset: 0x005BD41E
		public void Update()
		{
			if (!this.IsPlaying || this._soundEffectInstance.PendingBufferCount >= 8)
			{
				return;
			}
			this.PrepareBuffer();
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x005BF240 File Offset: 0x005BD440
		protected void ResetBuffer()
		{
			for (int i = 0; i < this._bufferToSubmit.Length; i++)
			{
				this._bufferToSubmit[i] = 0;
			}
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x005BF26C File Offset: 0x005BD46C
		protected void PrepareBuffer()
		{
			for (int i = 0; i < 2; i++)
			{
				this.ReadAheadPutAChunkIntoTheBuffer();
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x005BF28B File Offset: 0x005BD48B
		public bool IsPlaying
		{
			get
			{
				return this._soundEffectInstance.State == SoundState.Playing;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x005BF29B File Offset: 0x005BD49B
		public bool IsStopped
		{
			get
			{
				return this._soundEffectInstance.State == SoundState.Stopped;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x005BF2AB File Offset: 0x005BD4AB
		public bool IsPaused
		{
			get
			{
				return this._soundEffectInstance.State == SoundState.Paused;
			}
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x005BF2BB File Offset: 0x005BD4BB
		public void Stop(AudioStopOptions options)
		{
			this._soundEffectInstance.Stop(options == AudioStopOptions.Immediate);
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x005BF2CC File Offset: 0x005BD4CC
		public void Play()
		{
			this.PrepareToPlay();
			this._soundEffectInstance.Play();
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x005BF2DF File Offset: 0x005BD4DF
		public void Pause()
		{
			this._soundEffectInstance.Pause();
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x005BF2EC File Offset: 0x005BD4EC
		public void SetVariable(string variableName, float value)
		{
			if (variableName == "Volume")
			{
				float volume = this.ReMapVolumeToMatchXact(value);
				this._soundEffectInstance.Volume = volume;
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

		// Token: 0x06002DAD RID: 11693 RVA: 0x005BF350 File Offset: 0x005BD550
		private float ReMapVolumeToMatchXact(float musicVolume)
		{
			double num = 31.0 * (double)musicVolume - 25.0 - 11.94;
			return (float)Math.Pow(10.0, num / 20.0);
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x005BF398 File Offset: 0x005BD598
		public void Resume()
		{
			this._soundEffectInstance.Resume();
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x005BF3A5 File Offset: 0x005BD5A5
		protected virtual void PrepareToPlay()
		{
			this.ResetBuffer();
		}

		// Token: 0x06002DB0 RID: 11696
		public abstract void Reuse();

		// Token: 0x06002DB1 RID: 11697 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void ReadAheadPutAChunkIntoTheBuffer()
		{
		}

		// Token: 0x06002DB2 RID: 11698
		public abstract void Dispose();

		// Token: 0x04005150 RID: 20816
		protected const int bufferLength = 4096;

		// Token: 0x04005151 RID: 20817
		protected const int bufferCountPerSubmit = 2;

		// Token: 0x04005152 RID: 20818
		protected const int buffersToCoverFor = 8;

		// Token: 0x04005153 RID: 20819
		protected byte[] _bufferToSubmit = new byte[4096];

		// Token: 0x04005154 RID: 20820
		protected float[] _temporaryBuffer = new float[2048];

		// Token: 0x04005155 RID: 20821
		private int _sampleRate;

		// Token: 0x04005156 RID: 20822
		private AudioChannels _channels;

		// Token: 0x04005157 RID: 20823
		protected DynamicSoundEffectInstance _soundEffectInstance;
	}
}
