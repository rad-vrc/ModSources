using System;
using Microsoft.Xna.Framework.Audio;
using Terraria.Utilities;

namespace Terraria.Audio
{
	// Token: 0x0200048A RID: 1162
	public abstract class SoundStyle
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x005C3820 File Offset: 0x005C1A20
		public float Volume
		{
			get
			{
				return this._volume;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x005C3828 File Offset: 0x005C1A28
		public float PitchVariance
		{
			get
			{
				return this._pitchVariance;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x005C3830 File Offset: 0x005C1A30
		public SoundType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06002E3D RID: 11837
		public abstract bool IsTrackable { get; }

		// Token: 0x06002E3E RID: 11838 RVA: 0x005C3838 File Offset: 0x005C1A38
		public SoundStyle(float volume, float pitchVariance, SoundType type = SoundType.Sound)
		{
			this._volume = volume;
			this._pitchVariance = pitchVariance;
			this._type = type;
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x005C3855 File Offset: 0x005C1A55
		public SoundStyle(SoundType type = SoundType.Sound)
		{
			this._volume = 1f;
			this._pitchVariance = 0f;
			this._type = type;
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x005C387A File Offset: 0x005C1A7A
		public float GetRandomPitch()
		{
			return SoundStyle._random.NextFloat() * this.PitchVariance - this.PitchVariance * 0.5f;
		}

		// Token: 0x06002E41 RID: 11841
		public abstract SoundEffect GetRandomSound();

		// Token: 0x040051C1 RID: 20929
		private static UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040051C2 RID: 20930
		private float _volume;

		// Token: 0x040051C3 RID: 20931
		private float _pitchVariance;

		// Token: 0x040051C4 RID: 20932
		private SoundType _type;
	}
}
