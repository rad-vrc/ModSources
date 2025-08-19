using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Terraria.Localization;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000616 RID: 1558
	[NullableContext(1)]
	[Nullable(0)]
	public class SimpleItemDropRuleCondition : IItemDropRuleCondition, IProvideItemConditionDescription, IEquatable<SimpleItemDropRuleCondition>
	{
		// Token: 0x06004492 RID: 17554 RVA: 0x0060AC85 File Offset: 0x00608E85
		public SimpleItemDropRuleCondition(LocalizedText Description, Func<bool> Predicate, ShowItemDropInUI ShowItemDropInUI, bool ShowConditionInUI = true)
		{
			this.Description = Description;
			this.Predicate = Predicate;
			this.ShowItemDropInUI = ShowItemDropInUI;
			this.ShowConditionInUI = ShowConditionInUI;
			base..ctor();
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06004493 RID: 17555 RVA: 0x0060ACAA File Offset: 0x00608EAA
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SimpleItemDropRuleCondition);
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x0060ACB6 File Offset: 0x00608EB6
		// (set) Token: 0x06004495 RID: 17557 RVA: 0x0060ACBE File Offset: 0x00608EBE
		public LocalizedText Description { get; set; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x0060ACC7 File Offset: 0x00608EC7
		// (set) Token: 0x06004497 RID: 17559 RVA: 0x0060ACCF File Offset: 0x00608ECF
		public Func<bool> Predicate { get; set; }

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x0060ACD8 File Offset: 0x00608ED8
		// (set) Token: 0x06004499 RID: 17561 RVA: 0x0060ACE0 File Offset: 0x00608EE0
		public ShowItemDropInUI ShowItemDropInUI { get; set; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x0060ACE9 File Offset: 0x00608EE9
		// (set) Token: 0x0600449B RID: 17563 RVA: 0x0060ACF1 File Offset: 0x00608EF1
		public bool ShowConditionInUI { get; set; }

		// Token: 0x0600449C RID: 17564 RVA: 0x0060ACFA File Offset: 0x00608EFA
		public bool CanDrop(DropAttemptInfo info)
		{
			return this.Predicate();
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x0060AD08 File Offset: 0x00608F08
		public bool CanShowItemDropInUI()
		{
			bool result;
			switch (this.ShowItemDropInUI)
			{
			case ShowItemDropInUI.Always:
				result = true;
				break;
			case ShowItemDropInUI.Never:
				result = false;
				break;
			case ShowItemDropInUI.WhenConditionSatisfied:
				result = this.Predicate();
				break;
			default:
				throw new NotImplementedException();
			}
			return result;
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x0060AD4D File Offset: 0x00608F4D
		[NullableContext(2)]
		public string GetConditionDescription()
		{
			if (!this.ShowConditionInUI)
			{
				return null;
			}
			return this.Description.Value;
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x0060AD64 File Offset: 0x00608F64
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SimpleItemDropRuleCondition");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x0060ADB0 File Offset: 0x00608FB0
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Description = ");
			builder.Append(this.Description);
			builder.Append(", Predicate = ");
			builder.Append(this.Predicate);
			builder.Append(", ShowItemDropInUI = ");
			builder.Append(this.ShowItemDropInUI.ToString());
			builder.Append(", ShowConditionInUI = ");
			builder.Append(this.ShowConditionInUI.ToString());
			return true;
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x0060AE43 File Offset: 0x00609043
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SimpleItemDropRuleCondition left, SimpleItemDropRuleCondition right)
		{
			return !(left == right);
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x0060AE4F File Offset: 0x0060904F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SimpleItemDropRuleCondition left, SimpleItemDropRuleCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x0060AE64 File Offset: 0x00609064
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<Func<bool>>.Default.GetHashCode(this.<Predicate>k__BackingField)) * -1521134295 + EqualityComparer<ShowItemDropInUI>.Default.GetHashCode(this.<ShowItemDropInUI>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShowConditionInUI>k__BackingField);
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x0060AEDD File Offset: 0x006090DD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SimpleItemDropRuleCondition);
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x0060AEEC File Offset: 0x006090EC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SimpleItemDropRuleCondition other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<LocalizedText>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<Func<bool>>.Default.Equals(this.<Predicate>k__BackingField, other.<Predicate>k__BackingField) && EqualityComparer<ShowItemDropInUI>.Default.Equals(this.<ShowItemDropInUI>k__BackingField, other.<ShowItemDropInUI>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ShowConditionInUI>k__BackingField, other.<ShowConditionInUI>k__BackingField));
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x0060AF7D File Offset: 0x0060917D
		[CompilerGenerated]
		protected SimpleItemDropRuleCondition(SimpleItemDropRuleCondition original)
		{
			this.Description = original.<Description>k__BackingField;
			this.Predicate = original.<Predicate>k__BackingField;
			this.ShowItemDropInUI = original.<ShowItemDropInUI>k__BackingField;
			this.ShowConditionInUI = original.<ShowConditionInUI>k__BackingField;
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x0060AFB5 File Offset: 0x006091B5
		[CompilerGenerated]
		public void Deconstruct(out LocalizedText Description, out Func<bool> Predicate, out ShowItemDropInUI ShowItemDropInUI, out bool ShowConditionInUI)
		{
			Description = this.Description;
			Predicate = this.Predicate;
			ShowItemDropInUI = this.ShowItemDropInUI;
			ShowConditionInUI = this.ShowConditionInUI;
		}
	}
}
