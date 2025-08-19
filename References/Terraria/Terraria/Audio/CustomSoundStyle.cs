using System;
using Microsoft.Xna.Framework.Audio;
using Terraria.Utilities;

namespace Terraria.Audio
{
	// Token: 0x0200047D RID: 1149
	public class CustomSoundStyle : SoundStyle
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x0003266D File Offset: 0x0003086D
		public override bool IsTrackable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x005BF3B5 File Offset: 0x005BD5B5
		public CustomSoundStyle(SoundEffect soundEffect, SoundType type = SoundType.Sound, float volume = 1f, float pitchVariance = 0f) : base(volume, pitchVariance, type)
		{
			this._soundEffects = new SoundEffect[]
			{
				soundEffect
			};
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x005BF3D1 File Offset: 0x005BD5D1
		public CustomSoundStyle(SoundEffect[] soundEffects, SoundType type = SoundType.Sound, float volume = 1f, float pitchVariance = 0f) : base(volume, pitchVariance, type)
		{
			this._soundEffects = soundEffects;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x005BF3E4 File Offset: 0x005BD5E4
		public override SoundEffect GetRandomSound()
		{
			return this._soundEffects[CustomSoundStyle.Random.Next(this._soundEffects.Length)];
		}

		// Token: 0x04005158 RID: 20824
		private static readonly UnifiedRandom Random = new UnifiedRandom();

		// Token: 0x04005159 RID: 20825
		private readonly SoundEffect[] _soundEffects;
	}
}
