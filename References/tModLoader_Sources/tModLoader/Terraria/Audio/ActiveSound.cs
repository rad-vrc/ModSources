using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x0200075C RID: 1884
	[NullableContext(2)]
	[Nullable(0)]
	public class ActiveSound
	{
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06004C17 RID: 19479 RVA: 0x0066CC03 File Offset: 0x0066AE03
		// (set) Token: 0x06004C18 RID: 19480 RVA: 0x0066CC0B File Offset: 0x0066AE0B
		public SoundEffectInstance Sound { get; private set; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06004C19 RID: 19481 RVA: 0x0066CC14 File Offset: 0x0066AE14
		// (set) Token: 0x06004C1A RID: 19482 RVA: 0x0066CC1C File Offset: 0x0066AE1C
		public SoundStyle Style { get; private set; }

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x0066CC25 File Offset: 0x0066AE25
		public bool IsPlaying
		{
			get
			{
				SoundEffectInstance sound = this.Sound;
				return sound != null && !sound.IsDisposed && this.Sound.State == 0;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x0066CC4E File Offset: 0x0066AE4E
		public bool IsPlayingOrPaused
		{
			get
			{
				SoundEffectInstance sound = this.Sound;
				return sound != null && !sound.IsDisposed && this.Sound.State != 2;
			}
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x0066CC7C File Offset: 0x0066AE7C
		public ActiveSound(SoundStyle style, Vector2? position = null, SoundUpdateCallback updateCallback = null)
		{
			this.Position = position;
			this.Volume = 1f;
			this.Pitch = style.PitchVariance;
			this.Style = style.WithSelectedVariant(style.SelectedVariant);
			this.Callback = updateCallback;
			this.Play();
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x0066CCD0 File Offset: 0x0066AED0
		private void Play()
		{
			if (!Program.IsMainThread)
			{
				ActiveSound.RunOnMainThreadAndWait(new Action(this.Play));
				return;
			}
			SoundEffectInstance soundEffectInstance = this.Style.GetSoundEffect().CreateInstance();
			soundEffectInstance.Pitch += this.Style.GetRandomPitch();
			this.Pitch = soundEffectInstance.Pitch;
			soundEffectInstance.IsLooped = this.Style.IsLooped;
			soundEffectInstance.Play();
			SoundInstanceGarbageCollector.Track(soundEffectInstance);
			this.Sound = soundEffectInstance;
			this.Update();
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x0066CD5E File Offset: 0x0066AF5E
		public void Stop()
		{
			if (!Program.IsMainThread)
			{
				ActiveSound.RunOnMainThreadAndWait(new Action(this.Stop));
				return;
			}
			if (this.Sound != null)
			{
				this.Sound.Stop();
			}
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x0066CD8C File Offset: 0x0066AF8C
		public void Pause()
		{
			if (!Program.IsMainThread)
			{
				ActiveSound.RunOnMainThreadAndWait(new Action(this.Pause));
				return;
			}
			if (this.Sound != null && this.Sound.State == null)
			{
				this.Sound.Pause();
			}
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x0066CDC7 File Offset: 0x0066AFC7
		public void Resume()
		{
			if (!Program.IsMainThread)
			{
				ActiveSound.RunOnMainThreadAndWait(new Action(this.Resume));
				return;
			}
			if (this.Sound != null && this.Sound.State == 1)
			{
				this.Sound.Resume();
			}
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x0066CE04 File Offset: 0x0066B004
		public void Update()
		{
			if (this.Sound == null)
			{
				return;
			}
			if (!Program.IsMainThread)
			{
				ActiveSound.RunOnMainThreadAndWait(new Action(this.Update));
				return;
			}
			if (this.Sound.IsDisposed)
			{
				return;
			}
			SoundUpdateCallback callback = this.Callback;
			if (callback != null && !callback(this))
			{
				this.Sound.Stop(true);
				return;
			}
			Vector2 value = Main.screenPosition + new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2));
			float num = 1f;
			if (this.Position != null)
			{
				float value2 = (this.Position.Value.X - value.X) / ((float)Main.screenWidth * 0.5f);
				value2 = MathHelper.Clamp(value2, -1f, 1f);
				this.Sound.Pan = value2;
				float num2 = Vector2.Distance(this.Position.Value, value);
				num = 1f - num2 / ((float)Main.screenWidth * 1.5f);
			}
			num *= this.Style.Volume * this.Volume;
			switch (this.Style.Type)
			{
			case SoundType.Sound:
				num *= Main.soundVolume;
				break;
			case SoundType.Ambient:
				num *= Main.ambientVolume;
				if (Main.gameInactive)
				{
					num = 0f;
				}
				break;
			case SoundType.Music:
				num *= Main.musicVolume;
				break;
			}
			num = MathHelper.Clamp(num, 0f, 1f);
			this.Sound.Volume = num;
			this.Sound.Pitch = this.Pitch;
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x0066CF98 File Offset: 0x0066B198
		[NullableContext(1)]
		private static void RunOnMainThreadAndWait(Action action)
		{
			Main.RunOnMainThread(action).GetAwaiter().GetResult();
		}

		// Token: 0x040060A8 RID: 24744
		public Vector2? Position;

		// Token: 0x040060A9 RID: 24745
		public float Volume;

		// Token: 0x040060AA RID: 24746
		public float Pitch;

		// Token: 0x040060AB RID: 24747
		public SoundUpdateCallback Callback;
	}
}
