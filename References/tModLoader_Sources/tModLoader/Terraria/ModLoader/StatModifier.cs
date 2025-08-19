using System;

namespace Terraria.ModLoader
{
	// Token: 0x020001FC RID: 508
	public struct StatModifier
	{
		/// <summary>
		/// The combination of all additive multipliers. Starts at 1
		/// </summary>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x00502283 File Offset: 0x00500483
		// (set) Token: 0x0600271D RID: 10013 RVA: 0x0050228B File Offset: 0x0050048B
		public float Additive { readonly get; private set; }

		/// <summary>
		/// The combination of all multiplicative multipliers. Starts at 1. Applies 'after' all additive bonuses have been accumulated.
		/// </summary>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x00502294 File Offset: 0x00500494
		// (set) Token: 0x0600271F RID: 10015 RVA: 0x0050229C File Offset: 0x0050049C
		public float Multiplicative { readonly get; private set; }

		// Token: 0x06002720 RID: 10016 RVA: 0x005022A5 File Offset: 0x005004A5
		public StatModifier()
		{
			this.Base = 0f;
			this.Additive = 1f;
			this.Multiplicative = 1f;
			this.Flat = 0f;
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x005022D4 File Offset: 0x005004D4
		public StatModifier(float additive, float multiplicative, float flat = 0f, float @base = 0f)
		{
			this.Base = 0f;
			this.Additive = 1f;
			this.Multiplicative = 1f;
			this.Flat = 0f;
			this.Additive = additive;
			this.Multiplicative = multiplicative;
			this.Flat = flat;
			this.Base = @base;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0050232C File Offset: 0x0050052C
		public override bool Equals(object obj)
		{
			if (obj is StatModifier)
			{
				StatModifier i = (StatModifier)obj;
				return this == i;
			}
			return false;
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00502358 File Offset: 0x00500558
		public override int GetHashCode()
		{
			return (((1713062080 * -1521134295 + this.Additive.GetHashCode()) * -1521134295 + this.Multiplicative.GetHashCode()) * -1521134295 + this.Flat.GetHashCode()) * -1521134295 + this.Base.GetHashCode();
		}

		/// <summary>
		/// By using the add operator, the supplied additive modifier is combined with the existing modifiers. For example, adding 0.12f would be equivalent to a typical 12% damage boost. For 99% of effects used in the game, this approach is used.
		/// </summary>
		/// <param name="m"></param>
		/// <param name="add">The additive modifier to add, where 0.01f is equivalent to 1%</param>
		/// <returns></returns>
		// Token: 0x06002724 RID: 10020 RVA: 0x005023B8 File Offset: 0x005005B8
		public static StatModifier operator +(StatModifier m, float add)
		{
			return new StatModifier(m.Additive + add, m.Multiplicative, m.Flat, m.Base);
		}

		/// <summary>
		/// By using the subtract operator, the supplied subtractive modifier is combined with the existing modifiers. For example, subtracting 0.12f would be equivalent to a typical 12% damage decrease. For 99% of effects used in the game, this approach is used.
		/// </summary>
		/// <param name="m"></param>
		/// <param name="sub">The additive modifier to subtract, where 0.01f is equivalent to 1%</param>
		/// <returns></returns>
		// Token: 0x06002725 RID: 10021 RVA: 0x005023DB File Offset: 0x005005DB
		public static StatModifier operator -(StatModifier m, float sub)
		{
			return new StatModifier(m.Additive - sub, m.Multiplicative, m.Flat, m.Base);
		}

		/// <summary>
		/// The multiply operator applies a multiplicative effect to the resulting multiplicative modifier. This effect is very rarely used, typical effects use the add operator.
		/// </summary>
		/// <param name="m"></param>
		/// <param name="mul">The factor by which the multiplicative modifier is scaled</param>
		/// <returns></returns>
		// Token: 0x06002726 RID: 10022 RVA: 0x005023FE File Offset: 0x005005FE
		public static StatModifier operator *(StatModifier m, float mul)
		{
			return new StatModifier(m.Additive, m.Multiplicative * mul, m.Flat, m.Base);
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x00502421 File Offset: 0x00500621
		public static StatModifier operator /(StatModifier m, float div)
		{
			return new StatModifier(m.Additive, m.Multiplicative / div, m.Flat, m.Base);
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x00502444 File Offset: 0x00500644
		public static StatModifier operator +(float add, StatModifier m)
		{
			return m + add;
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x0050244D File Offset: 0x0050064D
		public static StatModifier operator *(float mul, StatModifier m)
		{
			return m * mul;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x00502456 File Offset: 0x00500656
		public static bool operator ==(StatModifier m1, StatModifier m2)
		{
			return m1.Additive == m2.Additive && m1.Multiplicative == m2.Multiplicative && m1.Flat == m2.Flat && m1.Base == m2.Base;
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x00502498 File Offset: 0x00500698
		public static bool operator !=(StatModifier m1, StatModifier m2)
		{
			return m1.Additive != m2.Additive || m1.Multiplicative != m2.Multiplicative || m1.Flat != m2.Flat || m1.Base != m2.Base;
		}

		/// <summary>
		/// Use this to apply the modifiers of this <see cref="T:Terraria.ModLoader.StatModifier" /> to the <paramref name="baseValue" />. You should assign
		/// the value passed in to the return result. For example:
		/// <para><br><c>damage = CritDamage.ApplyTo(damage)</c></br></para>
		/// <br></br>could be used to apply a crit damage modifier to a base damage value
		/// <para /> Note that when using this to calculate the final damage of a <see cref="T:Terraria.ModLoader.DamageClass" /> make sure to use <see cref="M:Terraria.Player.GetTotalDamage(Terraria.ModLoader.DamageClass)" /> not <see cref="M:Terraria.Player.GetDamage(Terraria.ModLoader.DamageClass)" /> to account for inherited damage modifiers such as Generic damage.
		/// </summary>
		/// <remarks>For help understanding the meanings of the applied values please make note of documentation for:
		/// <list type="bullet">
		/// <item><description><see cref="F:Terraria.ModLoader.StatModifier.Base" /></description></item>
		/// <item><description><see cref="P:Terraria.ModLoader.StatModifier.Additive" /></description></item>
		/// <item><description><see cref="P:Terraria.ModLoader.StatModifier.Multiplicative" /></description></item>
		/// <item><description><see cref="F:Terraria.ModLoader.StatModifier.Flat" /></description></item>
		/// </list>
		/// The order of operations of the modifiers are as follows:
		/// <list type="number">
		/// <item><description>The <paramref name="baseValue" /> is added to <see cref="F:Terraria.ModLoader.StatModifier.Base" /></description></item>
		/// <item><description>That result is multiplied by <see cref="P:Terraria.ModLoader.StatModifier.Additive" /></description></item>
		/// <item><description>The previous result is then multiplied by <see cref="P:Terraria.ModLoader.StatModifier.Multiplicative" /></description></item>
		/// <item><description>Finally, <see cref="F:Terraria.ModLoader.StatModifier.Flat" /> as added to the result of all previous calculations</description></item>
		/// </list>
		/// </remarks>
		/// <param name="baseValue">The starting value to apply modifiers to</param>
		/// <returns>The result of <paramref name="baseValue" /> after all modifiers are applied</returns>
		// Token: 0x0600272C RID: 10028 RVA: 0x005024E6 File Offset: 0x005006E6
		public float ApplyTo(float baseValue)
		{
			return (baseValue + this.Base) * this.Additive * this.Multiplicative + this.Flat;
		}

		/// <summary>
		/// Combines the components of two StatModifiers. Typically used to apply the effects of ammo-specific StatModifier to the DamageClass StatModifier values.
		/// </summary>
		// Token: 0x0600272D RID: 10029 RVA: 0x00502508 File Offset: 0x00500708
		public StatModifier CombineWith(StatModifier m)
		{
			return new StatModifier(this.Additive + m.Additive - 1f, this.Multiplicative * m.Multiplicative, this.Flat + m.Flat, this.Base + m.Base);
		}

		/// <summary>
		/// Scales all components of this StatModifier for the purposes of applying damage class modifier inheritance.<para />This is <b>NOT</b> intended for typical modding usage, if you are looking to increase this stat by some percentage, use the addition (<c>+</c>) operator.
		/// </summary>
		// Token: 0x0600272E RID: 10030 RVA: 0x00502556 File Offset: 0x00500756
		public StatModifier Scale(float scale)
		{
			return new StatModifier(1f + (this.Additive - 1f) * scale, 1f + (this.Multiplicative - 1f) * scale, this.Flat * scale, this.Base * scale);
		}

		// Token: 0x040018BE RID: 6334
		public static readonly StatModifier Default = new StatModifier();

		/// <summary>
		/// An offset to the base value of the stat. Directly applied to the base stat before multipliers are applied.
		/// </summary>
		// Token: 0x040018BF RID: 6335
		public float Base;

		/// <summary>
		/// Increase to the final value of the stat. Directly added to the stat after multipliers are applied.
		/// </summary>
		// Token: 0x040018C2 RID: 6338
		public float Flat;
	}
}
