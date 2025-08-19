using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria.Audio
{
	/// <summary>
	/// This data type describes in detail how a sound should be played.
	/// <br /> Passable to the <see cref="M:Terraria.Audio.SoundEngine.PlaySound(Terraria.Audio.SoundStyle@,System.Nullable{Microsoft.Xna.Framework.Vector2},Terraria.Audio.SoundUpdateCallback)" /> method.
	/// </summary>
	// Token: 0x0200076E RID: 1902
	public struct SoundStyle : IEquatable<SoundStyle>
	{
		/// <summary> The sound effect to play. </summary>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06004CCB RID: 19659 RVA: 0x00671400 File Offset: 0x0066F600
		// (set) Token: 0x06004CCC RID: 19660 RVA: 0x00671408 File Offset: 0x0066F608
		[Nullable(1)]
		public string SoundPath { [NullableContext(1)] readonly get; [NullableContext(1)] set; }

		/// <summary>
		/// Controls which volume setting will this be affected by.
		/// <br /> Ambience sounds also don't play when the game is out of focus.
		/// </summary>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06004CCD RID: 19661 RVA: 0x00671411 File Offset: 0x0066F611
		// (set) Token: 0x06004CCE RID: 19662 RVA: 0x00671419 File Offset: 0x0066F619
		public SoundType Type { readonly get; set; }

		/// <summary> If defined, this string will be the only thing used to determine which styles should instances be shared with. </summary>
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06004CCF RID: 19663 RVA: 0x00671422 File Offset: 0x0066F622
		// (set) Token: 0x06004CD0 RID: 19664 RVA: 0x0067142A File Offset: 0x0066F62A
		[Nullable(2)]
		public string Identifier { [NullableContext(2)] readonly get; [NullableContext(2)] set; }

		/// <summary>
		/// The max amount of sound instances that this style will allow creating, before stopping a playing sound or refusing to play a new one.
		/// <br /><br /> If using variants, use <see cref="P:Terraria.Audio.SoundStyle.LimitsArePerVariant" /> to allow <see cref="P:Terraria.Audio.SoundStyle.MaxInstances" /> to apply to each variant individually rather than to all variants as a group.
		/// <br /><br /> Set to 0 for no limits.
		/// </summary>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x00671433 File Offset: 0x0066F633
		// (set) Token: 0x06004CD2 RID: 19666 RVA: 0x0067143B File Offset: 0x0066F63B
		public int MaxInstances { readonly get; set; }

		/// <summary>
		/// Determines what the action taken when the max amount of sound instances is reached.
		/// <br /><br /> Defaults to <see cref="F:Terraria.Audio.SoundLimitBehavior.ReplaceOldest" />, which means a currently playing sound will be stopped and a new sound instance will be started.
		/// </summary>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06004CD3 RID: 19667 RVA: 0x00671444 File Offset: 0x0066F644
		// (set) Token: 0x06004CD4 RID: 19668 RVA: 0x0067144C File Offset: 0x0066F64C
		public SoundLimitBehavior SoundLimitBehavior { readonly get; set; }

		/// <summary>
		/// How many additional times to attempt to find a variant that is not currently playing before applying the SoundLimitBehavior. Only has effect if LimitsArePerVariant is true. Defaults to 0.
		/// </summary>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x00671455 File Offset: 0x0066F655
		// (set) Token: 0x06004CD6 RID: 19670 RVA: 0x0067145D File Offset: 0x0066F65D
		public int RerollAttempts
		{
			get
			{
				return this.rerollAttempts;
			}
			set
			{
				this.rerollAttempts = Math.Max(0, value);
			}
		}

		/// <summary>
		/// If true, then variants are treated as different sounds for the purposes of <see cref="P:Terraria.Audio.SoundStyle.SoundLimitBehavior" /> and <see cref="P:Terraria.Audio.SoundStyle.MaxInstances" />. Defaults to false, meaning that all variants share the same sound instance limitations.
		/// </summary>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x0067146C File Offset: 0x0066F66C
		// (set) Token: 0x06004CD8 RID: 19672 RVA: 0x00671474 File Offset: 0x0066F674
		public bool LimitsArePerVariant { readonly get; set; }

		/// <summary> If true, this sound won't play if the game's window isn't selected. </summary>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06004CD9 RID: 19673 RVA: 0x0067147D File Offset: 0x0066F67D
		// (set) Token: 0x06004CDA RID: 19674 RVA: 0x00671485 File Offset: 0x0066F685
		public bool PlayOnlyIfFocused { readonly get; set; }

		/// <summary>
		/// Determines how the sound will be affected when the game is paused (or unfocused) and subsequently resumed. Long-running sounds might benefit from changing this value.
		/// <br /><br /> Defaults to <see cref="F:Terraria.Audio.PauseBehavior.KeepPlaying" />, which means the sound will continue playing while the game is paused.
		/// </summary>
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06004CDB RID: 19675 RVA: 0x0067148E File Offset: 0x0066F68E
		// (set) Token: 0x06004CDC RID: 19676 RVA: 0x00671496 File Offset: 0x0066F696
		public PauseBehavior PauseBehavior { readonly get; set; }

		/// <summary> Whether or not to loop played sounds. </summary>
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06004CDD RID: 19677 RVA: 0x0067149F File Offset: 0x0066F69F
		// (set) Token: 0x06004CDE RID: 19678 RVA: 0x006714A7 File Offset: 0x0066F6A7
		public bool IsLooped { readonly get; set; }

		/// <summary>
		/// Whether or not this sound obeys the <see cref="F:Terraria.Main.musicPitch" /> field to decide its pitch.<br />
		/// Defaults to false. Used in vanilla by the sounds for the Bell, the (Magical) Harp, and The Axe.<br />
		/// Could prove useful, but is kept internal for the moment.
		/// </summary>
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06004CDF RID: 19679 RVA: 0x006714B0 File Offset: 0x0066F6B0
		// (set) Token: 0x06004CE0 RID: 19680 RVA: 0x006714B8 File Offset: 0x0066F6B8
		internal bool UsesMusicPitch { readonly get; set; }

		/// <summary>
		/// An array of possible suffixes to randomly append to after <see cref="P:Terraria.Audio.SoundStyle.SoundPath" />.
		/// <br /> Setting this property resets <see cref="P:Terraria.Audio.SoundStyle.VariantsWeights" />.
		/// </summary>
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06004CE1 RID: 19681 RVA: 0x006714C1 File Offset: 0x0066F6C1
		// (set) Token: 0x06004CE2 RID: 19682 RVA: 0x006714CE File Offset: 0x0066F6CE
		public ReadOnlySpan<int> Variants
		{
			get
			{
				return this.variants;
			}
			set
			{
				this.variantsWeights = null;
				this.totalVariantWeight = null;
				if (value.IsEmpty)
				{
					this.variants = null;
					return;
				}
				this.variants = value.ToArray();
			}
		}

		/// <summary>
		/// An array of randomization weights to optionally go with <see cref="P:Terraria.Audio.SoundStyle.Variants" />.
		/// <br /> Set this last, if at all, as the <see cref="P:Terraria.Audio.SoundStyle.Variants" />'s setter resets all weights data.
		/// </summary>
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06004CE3 RID: 19683 RVA: 0x00671501 File Offset: 0x0066F701
		// (set) Token: 0x06004CE4 RID: 19684 RVA: 0x00671510 File Offset: 0x0066F710
		public ReadOnlySpan<float> VariantsWeights
		{
			get
			{
				return this.variantsWeights;
			}
			set
			{
				if (value.Length == 0)
				{
					this.variantsWeights = null;
					this.totalVariantWeight = null;
					return;
				}
				if (this.variants == null)
				{
					throw new ArgumentException("Variants weights must be set after variants.");
				}
				if (value.Length != this.variants.Length)
				{
					throw new ArgumentException("Variants and their weights must have the same length.");
				}
				this.variantsWeights = value.ToArray();
				this.totalVariantWeight = null;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06004CE5 RID: 19685 RVA: 0x00671582 File Offset: 0x0066F782
		// (set) Token: 0x06004CE6 RID: 19686 RVA: 0x0067158A File Offset: 0x0066F78A
		internal int? SelectedVariant { readonly get; set; }

		/// <summary> The volume multiplier to play sounds with. </summary>
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06004CE7 RID: 19687 RVA: 0x00671593 File Offset: 0x0066F793
		// (set) Token: 0x06004CE8 RID: 19688 RVA: 0x0067159B File Offset: 0x0066F79B
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = MathHelper.Clamp(value, 0f, 1f);
			}
		}

		/// <summary>
		/// The pitch <b>offset</b> to play sounds with.
		/// <para />In XNA and FNA, Pitch ranges from -1.0f (down one octave) to 1.0f (up one octave). 0.0f is unity (normal) pitch.
		/// </summary>
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06004CE9 RID: 19689 RVA: 0x006715B3 File Offset: 0x0066F7B3
		// (set) Token: 0x06004CEA RID: 19690 RVA: 0x006715BB File Offset: 0x0066F7BB
		public float Pitch
		{
			get
			{
				return this.pitch;
			}
			set
			{
				this.pitch = value;
			}
		}

		/// <summary>
		/// The pitch offset randomness value. Cannot be negative.
		/// <br />With Pitch at 0.0, and PitchVariance at 1.0, used pitch will range from -0.5 to 0.5. 
		/// <para />In XNA and FNA, Pitch ranges from -1.0f (down one octave) to 1.0f (up one octave). 0.0f is unity (normal) pitch.
		/// </summary>
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x006715C4 File Offset: 0x0066F7C4
		// (set) Token: 0x06004CEC RID: 19692 RVA: 0x006715CC File Offset: 0x0066F7CC
		public float PitchVariance
		{
			get
			{
				return this.pitchVariance;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentException("Pitch variance cannot be negative.", "value");
				}
				this.pitchVariance = value;
			}
		}

		/// <summary>
		/// A helper property for controlling both Pitch and PitchVariance at once.
		/// <para />In XNA and FNA, Pitch ranges from -1.0f (down one octave) to 1.0f (up one octave). 0.0f is unity (normal) pitch.
		/// </summary>
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06004CED RID: 19693 RVA: 0x006715F0 File Offset: 0x0066F7F0
		// (set) Token: 0x06004CEE RID: 19694 RVA: 0x00671624 File Offset: 0x0066F824
		[TupleElementNames(new string[]
		{
			"minPitch",
			"maxPitch"
		})]
		public ValueTuple<float, float> PitchRange
		{
			[return: TupleElementNames(new string[]
			{
				"minPitch",
				"maxPitch"
			})]
			get
			{
				float halfVariance = this.PitchVariance / 2f;
				float item = this.Pitch - halfVariance;
				float maxPitch = this.Pitch + halfVariance;
				return new ValueTuple<float, float>(item, maxPitch);
			}
			[param: TupleElementNames(new string[]
			{
				"minPitch",
				"maxPitch"
			})]
			set
			{
				if (value.Item1 > value.Item2)
				{
					throw new ArgumentException("Min pitch cannot be greater than max pitch.", "value");
				}
				this.Pitch = (value.Item1 + value.Item2) * 0.5f;
				this.PitchVariance = value.Item2 - value.Item1;
			}
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x0067167C File Offset: 0x0066F87C
		[NullableContext(1)]
		public SoundStyle(string soundPath, SoundType type = SoundType.Sound)
		{
			this.SelectedVariant = null;
			this.variantsWeights = null;
			this.totalVariantWeight = null;
			this.volume = 1f;
			this.pitch = 0f;
			this.pitchVariance = 0f;
			this.effectCache = null;
			this.variantsEffectCache = null;
			this.rerollAttempts = 0;
			this.Identifier = null;
			this.MaxInstances = 1;
			this.SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest;
			this.LimitsArePerVariant = false;
			this.PlayOnlyIfFocused = false;
			this.PauseBehavior = PauseBehavior.KeepPlaying;
			this.IsLooped = false;
			this.UsesMusicPitch = false;
			this.SoundPath = soundPath;
			this.Type = type;
			this.variants = null;
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x0067172B File Offset: 0x0066F92B
		[NullableContext(1)]
		public SoundStyle(string soundPath, int numVariants, SoundType type = SoundType.Sound)
		{
			this = new SoundStyle(soundPath, type);
			if (numVariants > 1)
			{
				this.variants = SoundStyle.CreateVariants(1, numVariants);
			}
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x00671746 File Offset: 0x0066F946
		[NullableContext(1)]
		public SoundStyle(string soundPath, int variantSuffixesStart, int numVariants, SoundType type = SoundType.Sound)
		{
			this = new SoundStyle(soundPath, type);
			if (numVariants > 1)
			{
				this.variants = SoundStyle.CreateVariants(variantSuffixesStart, numVariants);
			}
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x00671762 File Offset: 0x0066F962
		public SoundStyle([Nullable(1)] string soundPath, ReadOnlySpan<int> variants, SoundType type = SoundType.Sound)
		{
			this = new SoundStyle(soundPath, type);
			this.variants = (variants.IsEmpty ? null : variants.ToArray());
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x00671788 File Offset: 0x0066F988
		public unsafe SoundStyle([Nullable(1)] string soundPath, [TupleElementNames(new string[]
		{
			"variant",
			"weight"
		})] ReadOnlySpan<ValueTuple<int, float>> weightedVariants, SoundType type = SoundType.Sound)
		{
			this = new SoundStyle(soundPath, type);
			if (weightedVariants.IsEmpty)
			{
				this.variants = null;
				return;
			}
			this.variants = new int[weightedVariants.Length];
			this.variantsWeights = new float[weightedVariants.Length];
			for (int i = 0; i < weightedVariants.Length; i++)
			{
				ValueTuple<int, float> valueTuple = *weightedVariants[i];
				int variant = valueTuple.Item1;
				float weight = valueTuple.Item2;
				this.variants[i] = variant;
				this.variantsWeights[i] = weight;
			}
		}

		/// <summary>
		/// Checks if this SoundStyle is the same as another SoundStyle. This method takes into account differences in chosen variants if <see cref="P:Terraria.Audio.SoundStyle.LimitsArePerVariant" /> is true.
		/// </summary>
		// Token: 0x06004CF4 RID: 19700 RVA: 0x00671810 File Offset: 0x0066FA10
		public bool IsTheSameAs(SoundStyle style)
		{
			if (this.LimitsArePerVariant)
			{
				int? selectedVariant = this.SelectedVariant;
				int? selectedVariant2 = style.SelectedVariant;
				if (!(selectedVariant.GetValueOrDefault() == selectedVariant2.GetValueOrDefault() & selectedVariant != null == (selectedVariant2 != null)))
				{
					return false;
				}
			}
			if (this.Identifier != null || style.Identifier != null)
			{
				return this.Identifier == style.Identifier;
			}
			return this.SoundPath == style.SoundPath;
		}

		/// <summary>
		/// Same as <see cref="M:Terraria.Audio.SoundStyle.IsTheSameAs(Terraria.Audio.SoundStyle)" /> except it doesn't take into account differences in chosen variants.
		/// </summary>
		// Token: 0x06004CF5 RID: 19701 RVA: 0x00671894 File Offset: 0x0066FA94
		public bool IsVariantOf(SoundStyle style)
		{
			if (this.Identifier != null || style.Identifier != null)
			{
				return this.Identifier == style.Identifier;
			}
			return this.SoundPath == style.SoundPath;
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x006718D1 File Offset: 0x0066FAD1
		[NullableContext(1)]
		[Obsolete("Renamed to GetSoundEffect")]
		public SoundEffect GetRandomSound()
		{
			return this.GetSoundEffect();
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x006718DC File Offset: 0x0066FADC
		[NullableContext(1)]
		public SoundEffect GetSoundEffect()
		{
			Asset<SoundEffect> asset;
			if (this.variants == null || this.variants.Length == 0)
			{
				Asset<SoundEffect> asset2;
				if ((asset2 = this.effectCache) == null)
				{
					asset2 = (this.effectCache = ModContent.Request<SoundEffect>(this.SoundPath, 1));
				}
				asset = asset2;
			}
			else
			{
				int variantIndex = this.SelectedVariant ?? this.GetRandomVariantIndex();
				int variant = this.variants[variantIndex];
				Array.Resize<Asset<SoundEffect>>(ref this.variantsEffectCache, this.variants.Length);
				ref Asset<SoundEffect> ptr = ref this.variantsEffectCache[variantIndex];
				Asset<SoundEffect> asset3;
				if ((asset3 = ptr) == null)
				{
					Asset<SoundEffect> asset4;
					ptr = (asset4 = ModContent.Request<SoundEffect>(this.SoundPath + variant.ToString(), 1));
					asset3 = asset4;
				}
				asset = asset3;
			}
			return asset.Value;
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x00671993 File Offset: 0x0066FB93
		public float GetRandomPitch()
		{
			return this.Pitch + (SoundStyle.Random.NextFloat() - 0.5f) * this.PitchVariance;
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x006719B4 File Offset: 0x0066FBB4
		internal SoundStyle WithVolume(float volume)
		{
			SoundStyle result = this;
			result.Volume = volume;
			return result;
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x006719D4 File Offset: 0x0066FBD4
		internal SoundStyle WithPitchVariance(float pitchVariance)
		{
			SoundStyle result = this;
			result.PitchVariance = pitchVariance;
			return result;
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x006719F4 File Offset: 0x0066FBF4
		public SoundStyle WithVolumeScale(float scale)
		{
			SoundStyle result = this;
			result.Volume = this.Volume * scale;
			return result;
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x00671A18 File Offset: 0x0066FC18
		public SoundStyle WithPitchOffset(float offset)
		{
			SoundStyle result = this;
			result.Pitch = this.Pitch + offset;
			return result;
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x00671A3C File Offset: 0x0066FC3C
		internal SoundStyle WithSelectedVariant(int? random = null)
		{
			if (this.variants == null || this.variants.Length == 0)
			{
				return this;
			}
			SoundStyle result = this;
			result.SelectedVariant = new int?(random ?? this.GetRandomVariantIndex());
			return result;
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x00671A90 File Offset: 0x0066FC90
		private int GetRandomVariantIndex()
		{
			if (this.variantsWeights == null)
			{
				return SoundStyle.Random.Next(this.variants.Length);
			}
			float value = this.totalVariantWeight.GetValueOrDefault();
			if (this.totalVariantWeight == null)
			{
				value = this.variantsWeights.Sum();
				this.totalVariantWeight = new float?(value);
			}
			float random = (float)SoundStyle.Random.NextDouble() * this.totalVariantWeight.Value;
			float accumulatedWeight = 0f;
			for (int i = 0; i < this.variantsWeights.Length; i++)
			{
				accumulatedWeight += this.variantsWeights[i];
				if (random < accumulatedWeight)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x00671B2C File Offset: 0x0066FD2C
		[NullableContext(1)]
		private static int[] CreateVariants(int start, int count)
		{
			if (count <= 1)
			{
				return Array.Empty<int>();
			}
			int[] result = new int[count];
			for (int i = 0; i < count; i++)
			{
				result[i] = start + i;
			}
			return result;
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x00671B60 File Offset: 0x0066FD60
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SoundStyle");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x00671BAC File Offset: 0x0066FDAC
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			builder.Append("SoundPath = ");
			builder.Append(this.SoundPath);
			builder.Append(", Type = ");
			builder.Append(this.Type.ToString());
			builder.Append(", Identifier = ");
			builder.Append(this.Identifier);
			builder.Append(", MaxInstances = ");
			builder.Append(this.MaxInstances.ToString());
			builder.Append(", SoundLimitBehavior = ");
			builder.Append(this.SoundLimitBehavior.ToString());
			builder.Append(", RerollAttempts = ");
			builder.Append(this.RerollAttempts.ToString());
			builder.Append(", LimitsArePerVariant = ");
			builder.Append(this.LimitsArePerVariant.ToString());
			builder.Append(", PlayOnlyIfFocused = ");
			builder.Append(this.PlayOnlyIfFocused.ToString());
			builder.Append(", PauseBehavior = ");
			builder.Append(this.PauseBehavior.ToString());
			builder.Append(", IsLooped = ");
			builder.Append(this.IsLooped.ToString());
			builder.Append(", Variants = ");
			builder.Append(this.Variants.ToString());
			builder.Append(", VariantsWeights = ");
			builder.Append(this.VariantsWeights.ToString());
			builder.Append(", Volume = ");
			builder.Append(this.Volume.ToString());
			builder.Append(", Pitch = ");
			builder.Append(this.Pitch.ToString());
			builder.Append(", PitchVariance = ");
			builder.Append(this.PitchVariance.ToString());
			builder.Append(", PitchRange = ");
			builder.Append(this.PitchRange.ToString());
			return true;
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x00671E15 File Offset: 0x00670015
		[CompilerGenerated]
		public static bool operator !=(SoundStyle left, SoundStyle right)
		{
			return !(left == right);
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x00671E21 File Offset: 0x00670021
		[CompilerGenerated]
		public static bool operator ==(SoundStyle left, SoundStyle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x00671E2C File Offset: 0x0067002C
		[CompilerGenerated]
		public override readonly int GetHashCode()
		{
			return ((((((((((((((((((EqualityComparer<int[]>.Default.GetHashCode(this.variants) * -1521134295 + EqualityComparer<float[]>.Default.GetHashCode(this.variantsWeights)) * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this.totalVariantWeight)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.volume)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.pitch)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.pitchVariance)) * -1521134295 + EqualityComparer<Asset<SoundEffect>>.Default.GetHashCode(this.effectCache)) * -1521134295 + EqualityComparer<Asset<SoundEffect>[]>.Default.GetHashCode(this.variantsEffectCache)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.rerollAttempts)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SoundPath>k__BackingField)) * -1521134295 + EqualityComparer<SoundType>.Default.GetHashCode(this.<Type>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Identifier>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxInstances>k__BackingField)) * -1521134295 + EqualityComparer<SoundLimitBehavior>.Default.GetHashCode(this.<SoundLimitBehavior>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<LimitsArePerVariant>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<PlayOnlyIfFocused>k__BackingField)) * -1521134295 + EqualityComparer<PauseBehavior>.Default.GetHashCode(this.<PauseBehavior>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsLooped>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<UsesMusicPitch>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<SelectedVariant>k__BackingField);
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x00671FFE File Offset: 0x006701FE
		[CompilerGenerated]
		public override readonly bool Equals(object obj)
		{
			return obj is SoundStyle && this.Equals((SoundStyle)obj);
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x00672018 File Offset: 0x00670218
		[CompilerGenerated]
		public readonly bool Equals(SoundStyle other)
		{
			return EqualityComparer<int[]>.Default.Equals(this.variants, other.variants) && EqualityComparer<float[]>.Default.Equals(this.variantsWeights, other.variantsWeights) && EqualityComparer<float?>.Default.Equals(this.totalVariantWeight, other.totalVariantWeight) && EqualityComparer<float>.Default.Equals(this.volume, other.volume) && EqualityComparer<float>.Default.Equals(this.pitch, other.pitch) && EqualityComparer<float>.Default.Equals(this.pitchVariance, other.pitchVariance) && EqualityComparer<Asset<SoundEffect>>.Default.Equals(this.effectCache, other.effectCache) && EqualityComparer<Asset<SoundEffect>[]>.Default.Equals(this.variantsEffectCache, other.variantsEffectCache) && EqualityComparer<int>.Default.Equals(this.rerollAttempts, other.rerollAttempts) && EqualityComparer<string>.Default.Equals(this.<SoundPath>k__BackingField, other.<SoundPath>k__BackingField) && EqualityComparer<SoundType>.Default.Equals(this.<Type>k__BackingField, other.<Type>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Identifier>k__BackingField, other.<Identifier>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxInstances>k__BackingField, other.<MaxInstances>k__BackingField) && EqualityComparer<SoundLimitBehavior>.Default.Equals(this.<SoundLimitBehavior>k__BackingField, other.<SoundLimitBehavior>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<LimitsArePerVariant>k__BackingField, other.<LimitsArePerVariant>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<PlayOnlyIfFocused>k__BackingField, other.<PlayOnlyIfFocused>k__BackingField) && EqualityComparer<PauseBehavior>.Default.Equals(this.<PauseBehavior>k__BackingField, other.<PauseBehavior>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsLooped>k__BackingField, other.<IsLooped>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<UsesMusicPitch>k__BackingField, other.<UsesMusicPitch>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<SelectedVariant>k__BackingField, other.<SelectedVariant>k__BackingField);
		}

		// Token: 0x04006123 RID: 24867
		[Nullable(1)]
		private static readonly UnifiedRandom Random = new UnifiedRandom();

		// Token: 0x04006124 RID: 24868
		[Nullable(2)]
		private int[] variants;

		// Token: 0x04006125 RID: 24869
		[Nullable(2)]
		private float[] variantsWeights;

		// Token: 0x04006126 RID: 24870
		private float? totalVariantWeight;

		// Token: 0x04006127 RID: 24871
		private float volume;

		// Token: 0x04006128 RID: 24872
		private float pitch;

		// Token: 0x04006129 RID: 24873
		private float pitchVariance;

		// Token: 0x0400612A RID: 24874
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private Asset<SoundEffect> effectCache;

		// Token: 0x0400612B RID: 24875
		[Nullable(new byte[]
		{
			2,
			2,
			1
		})]
		private Asset<SoundEffect>[] variantsEffectCache;

		// Token: 0x0400612C RID: 24876
		private int rerollAttempts;
	}
}
