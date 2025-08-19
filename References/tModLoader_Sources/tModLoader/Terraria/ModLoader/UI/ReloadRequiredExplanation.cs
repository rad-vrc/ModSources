using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader.UI
{
	/// <summary>
	/// <paramref name="typeOrder" /> dictates the order specific explanations are shown:
	/// <br /> 1: Download, 2: Switch Version, 3: Enable, 4: Disable, 5: Config Change
	/// <para /> <paramref name="mod" /> is internal name, <paramref name="localMod" /> might be null for mods that need to be downloaded.
	/// </summary>
	// Token: 0x0200025F RID: 607
	internal class ReloadRequiredExplanation : IEquatable<ReloadRequiredExplanation>
	{
		/// <summary>
		/// <paramref name="typeOrder" /> dictates the order specific explanations are shown:
		/// <br /> 1: Download, 2: Switch Version, 3: Enable, 4: Disable, 5: Config Change
		/// <para /> <paramref name="mod" /> is internal name, <paramref name="localMod" /> might be null for mods that need to be downloaded.
		/// </summary>
		// Token: 0x06002AA2 RID: 10914 RVA: 0x0051D7F1 File Offset: 0x0051B9F1
		public ReloadRequiredExplanation(int typeOrder, string mod, LocalMod localMod, string reason)
		{
			this.typeOrder = typeOrder;
			this.mod = mod;
			this.localMod = localMod;
			this.reason = reason;
			base..ctor();
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x0051D816 File Offset: 0x0051BA16
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ReloadRequiredExplanation);
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x0051D822 File Offset: 0x0051BA22
		// (set) Token: 0x06002AA5 RID: 10917 RVA: 0x0051D82A File Offset: 0x0051BA2A
		public int typeOrder { get; set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x0051D833 File Offset: 0x0051BA33
		// (set) Token: 0x06002AA7 RID: 10919 RVA: 0x0051D83B File Offset: 0x0051BA3B
		public string mod { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x0051D844 File Offset: 0x0051BA44
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x0051D84C File Offset: 0x0051BA4C
		public LocalMod localMod { get; set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x0051D855 File Offset: 0x0051BA55
		// (set) Token: 0x06002AAB RID: 10923 RVA: 0x0051D85D File Offset: 0x0051BA5D
		public string reason { get; set; }

		// Token: 0x06002AAC RID: 10924 RVA: 0x0051D868 File Offset: 0x0051BA68
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ReloadRequiredExplanation");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x0051D8B4 File Offset: 0x0051BAB4
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("typeOrder = ");
			builder.Append(this.typeOrder.ToString());
			builder.Append(", mod = ");
			builder.Append(this.mod);
			builder.Append(", localMod = ");
			builder.Append(this.localMod);
			builder.Append(", reason = ");
			builder.Append(this.reason);
			return true;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x0051D939 File Offset: 0x0051BB39
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ReloadRequiredExplanation left, ReloadRequiredExplanation right)
		{
			return !(left == right);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x0051D945 File Offset: 0x0051BB45
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ReloadRequiredExplanation left, ReloadRequiredExplanation right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0051D95C File Offset: 0x0051BB5C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<typeOrder>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<mod>k__BackingField)) * -1521134295 + EqualityComparer<LocalMod>.Default.GetHashCode(this.<localMod>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<reason>k__BackingField);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x0051D9D5 File Offset: 0x0051BBD5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ReloadRequiredExplanation);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x0051D9E4 File Offset: 0x0051BBE4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ReloadRequiredExplanation other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<typeOrder>k__BackingField, other.<typeOrder>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<mod>k__BackingField, other.<mod>k__BackingField) && EqualityComparer<LocalMod>.Default.Equals(this.<localMod>k__BackingField, other.<localMod>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<reason>k__BackingField, other.<reason>k__BackingField));
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x0051DA75 File Offset: 0x0051BC75
		[CompilerGenerated]
		protected ReloadRequiredExplanation([Nullable(1)] ReloadRequiredExplanation original)
		{
			this.typeOrder = original.<typeOrder>k__BackingField;
			this.mod = original.<mod>k__BackingField;
			this.localMod = original.<localMod>k__BackingField;
			this.reason = original.<reason>k__BackingField;
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x0051DAAD File Offset: 0x0051BCAD
		[CompilerGenerated]
		public void Deconstruct(out int typeOrder, out string mod, out LocalMod localMod, out string reason)
		{
			typeOrder = this.typeOrder;
			mod = this.mod;
			localMod = this.localMod;
			reason = this.reason;
		}
	}
}
