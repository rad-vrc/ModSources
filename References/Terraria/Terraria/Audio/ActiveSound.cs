using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x02000479 RID: 1145
	public class ActiveSound
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x005BEE1F File Offset: 0x005BD01F
		// (set) Token: 0x06002D91 RID: 11665 RVA: 0x005BEE27 File Offset: 0x005BD027
		public SoundEffectInstance Sound { get; private set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x005BEE30 File Offset: 0x005BD030
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x005BEE38 File Offset: 0x005BD038
		public SoundStyle Style { get; private set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x005BEE41 File Offset: 0x005BD041
		public bool IsPlaying
		{
			get
			{
				return this.Sound.State == SoundState.Playing;
			}
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x005BEE51 File Offset: 0x005BD051
		public ActiveSound(SoundStyle style, Vector2 position)
		{
			this.Position = position;
			this.Volume = 1f;
			this.Pitch = style.PitchVariance;
			this.IsGlobal = false;
			this.Style = style;
			this.Play();
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x005BEE8B File Offset: 0x005BD08B
		public ActiveSound(SoundStyle style)
		{
			this.Position = Vector2.Zero;
			this.Volume = 1f;
			this.Pitch = style.PitchVariance;
			this.IsGlobal = true;
			this.Style = style;
			this.Play();
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x005BEEC9 File Offset: 0x005BD0C9
		public ActiveSound(SoundStyle style, Vector2 position, ActiveSound.LoopedPlayCondition condition)
		{
			this.Position = position;
			this.Volume = 1f;
			this.Pitch = style.PitchVariance;
			this.IsGlobal = false;
			this.Style = style;
			this.PlayLooped(condition);
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x005BEF04 File Offset: 0x005BD104
		private void Play()
		{
			SoundEffectInstance soundEffectInstance = this.Style.GetRandomSound().CreateInstance();
			soundEffectInstance.Pitch += this.Style.GetRandomPitch();
			this.Pitch = soundEffectInstance.Pitch;
			soundEffectInstance.Play();
			SoundInstanceGarbageCollector.Track(soundEffectInstance);
			this.Sound = soundEffectInstance;
			this.Update();
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x005BEF60 File Offset: 0x005BD160
		private void PlayLooped(ActiveSound.LoopedPlayCondition condition)
		{
			SoundEffectInstance soundEffectInstance = this.Style.GetRandomSound().CreateInstance();
			soundEffectInstance.Pitch += this.Style.GetRandomPitch();
			this.Pitch = soundEffectInstance.Pitch;
			soundEffectInstance.IsLooped = true;
			this.Condition = condition;
			soundEffectInstance.Play();
			SoundInstanceGarbageCollector.Track(soundEffectInstance);
			this.Sound = soundEffectInstance;
			this.Update();
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x005BEFC9 File Offset: 0x005BD1C9
		public void Stop()
		{
			if (this.Sound != null)
			{
				this.Sound.Stop();
			}
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x005BEFDE File Offset: 0x005BD1DE
		public void Pause()
		{
			if (this.Sound != null && this.Sound.State == SoundState.Playing)
			{
				this.Sound.Pause();
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x005BF000 File Offset: 0x005BD200
		public void Resume()
		{
			if (this.Sound != null && this.Sound.State == SoundState.Paused)
			{
				this.Sound.Resume();
			}
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x005BF024 File Offset: 0x005BD224
		public void Update()
		{
			if (this.Sound == null)
			{
				return;
			}
			if (this.Condition != null && !this.Condition())
			{
				this.Sound.Stop(true);
				return;
			}
			Vector2 vector = Main.screenPosition + new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2));
			float num = 1f;
			if (!this.IsGlobal)
			{
				float num2 = (this.Position.X - vector.X) / ((float)Main.screenWidth * 0.5f);
				num2 = MathHelper.Clamp(num2, -1f, 1f);
				this.Sound.Pan = num2;
				float num3 = Vector2.Distance(this.Position, vector);
				num = 1f - num3 / ((float)Main.screenWidth * 1.5f);
			}
			num *= this.Style.Volume * this.Volume;
			switch (this.Style.Type)
			{
			case SoundType.Sound:
				num *= Main.soundVolume;
				break;
			case SoundType.Ambient:
				num *= Main.ambientVolume;
				break;
			case SoundType.Music:
				num *= Main.musicVolume;
				break;
			}
			num = MathHelper.Clamp(num, 0f, 1f);
			this.Sound.Volume = num;
			this.Sound.Pitch = this.Pitch;
		}

		// Token: 0x04005148 RID: 20808
		public readonly bool IsGlobal;

		// Token: 0x04005149 RID: 20809
		public Vector2 Position;

		// Token: 0x0400514A RID: 20810
		public float Volume;

		// Token: 0x0400514B RID: 20811
		public float Pitch;

		// Token: 0x0400514D RID: 20813
		public ActiveSound.LoopedPlayCondition Condition;

		// Token: 0x02000782 RID: 1922
		// (Invoke) Token: 0x06003929 RID: 14633
		public delegate bool LoopedPlayCondition();
	}
}
