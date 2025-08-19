using System;
using Microsoft.Xna.Framework.Audio;
using Terraria.Utilities;

namespace Terraria.Audio
{
	// Token: 0x02000483 RID: 1155
	public class LegacySoundStyle : SoundStyle
	{
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x005C2E60 File Offset: 0x005C1060
		public int Style
		{
			get
			{
				if (this.Variations != 1)
				{
					return LegacySoundStyle.Random.Next(this._style, this._style + this.Variations);
				}
				return this._style;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x005C2E8F File Offset: 0x005C108F
		public override bool IsTrackable
		{
			get
			{
				return this.SoundId == 42;
			}
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x005C2E9B File Offset: 0x005C109B
		public LegacySoundStyle(int soundId, int style, SoundType type = SoundType.Sound) : base(type)
		{
			this._style = style;
			this.Variations = 1;
			this.SoundId = soundId;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x005C2EB9 File Offset: 0x005C10B9
		public LegacySoundStyle(int soundId, int style, int variations, SoundType type = SoundType.Sound) : base(type)
		{
			this._style = style;
			this.Variations = variations;
			this.SoundId = soundId;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x005C2ED8 File Offset: 0x005C10D8
		private LegacySoundStyle(int soundId, int style, int variations, SoundType type, float volume, float pitchVariance) : base(volume, pitchVariance, type)
		{
			this._style = style;
			this.Variations = variations;
			this.SoundId = soundId;
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x005C2EFB File Offset: 0x005C10FB
		public LegacySoundStyle WithVolume(float volume)
		{
			return new LegacySoundStyle(this.SoundId, this._style, this.Variations, base.Type, volume, base.PitchVariance);
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x005C2F21 File Offset: 0x005C1121
		public LegacySoundStyle WithPitchVariance(float pitchVariance)
		{
			return new LegacySoundStyle(this.SoundId, this._style, this.Variations, base.Type, base.Volume, pitchVariance);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x005C2F47 File Offset: 0x005C1147
		public LegacySoundStyle AsMusic()
		{
			return new LegacySoundStyle(this.SoundId, this._style, this.Variations, SoundType.Music, base.Volume, base.PitchVariance);
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x005C2F6D File Offset: 0x005C116D
		public LegacySoundStyle AsAmbient()
		{
			return new LegacySoundStyle(this.SoundId, this._style, this.Variations, SoundType.Ambient, base.Volume, base.PitchVariance);
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x005C2F93 File Offset: 0x005C1193
		public LegacySoundStyle AsSound()
		{
			return new LegacySoundStyle(this.SoundId, this._style, this.Variations, SoundType.Sound, base.Volume, base.PitchVariance);
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x005C2FB9 File Offset: 0x005C11B9
		public bool Includes(int soundId, int style)
		{
			return this.SoundId == soundId && style >= this._style && style < this._style + this.Variations;
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x005C2FDF File Offset: 0x005C11DF
		public override SoundEffect GetRandomSound()
		{
			if (this.IsTrackable)
			{
				return SoundEngine.GetTrackableSoundByStyleId(this.Style);
			}
			return null;
		}

		// Token: 0x040051AE RID: 20910
		private static readonly UnifiedRandom Random = new UnifiedRandom();

		// Token: 0x040051AF RID: 20911
		private readonly int _style;

		// Token: 0x040051B0 RID: 20912
		public readonly int Variations;

		// Token: 0x040051B1 RID: 20913
		public readonly int SoundId;
	}
}
