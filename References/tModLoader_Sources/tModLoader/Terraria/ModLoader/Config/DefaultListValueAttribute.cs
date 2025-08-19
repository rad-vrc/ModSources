using System;
using System.ComponentModel;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Defines the default value to be added when using the ModConfig UI to add elements to a Collection (List, Set, or Dictionary value). Works the same as System.ComponentModel.DefaultValueAttribute, but can't inherit from it because it would break when deserializing any data structure annotated with it.
	/// </summary>
	// Token: 0x02000378 RID: 888
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class DefaultListValueAttribute : Attribute
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x0053D071 File Offset: 0x0053B271
		public virtual object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x0053D07C File Offset: 0x0053B27C
		public DefaultListValueAttribute(Type type, string value)
		{
			try
			{
				this.value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
			}
			catch
			{
				Logging.tML.Error(string.Concat(new string[]
				{
					"Default value attribute of type ",
					type.FullName,
					" threw converting from the string '",
					value,
					"'."
				}));
			}
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x0053D0F0 File Offset: 0x0053B2F0
		public DefaultListValueAttribute(char value)
		{
			this.value = value;
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x0053D104 File Offset: 0x0053B304
		public DefaultListValueAttribute(byte value)
		{
			this.value = value;
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x0053D118 File Offset: 0x0053B318
		public DefaultListValueAttribute(short value)
		{
			this.value = value;
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x0053D12C File Offset: 0x0053B32C
		public DefaultListValueAttribute(int value)
		{
			this.value = value;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0053D140 File Offset: 0x0053B340
		public DefaultListValueAttribute(long value)
		{
			this.value = value;
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x0053D154 File Offset: 0x0053B354
		public DefaultListValueAttribute(float value)
		{
			this.value = value;
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0053D168 File Offset: 0x0053B368
		public DefaultListValueAttribute(double value)
		{
			this.value = value;
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x0053D17C File Offset: 0x0053B37C
		public DefaultListValueAttribute(bool value)
		{
			this.value = value;
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x0053D190 File Offset: 0x0053B390
		public DefaultListValueAttribute(string value)
		{
			this.value = value;
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x0053D19F File Offset: 0x0053B39F
		public DefaultListValueAttribute(object value)
		{
			this.value = value;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x0053D1B0 File Offset: 0x0053B3B0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DefaultListValueAttribute other = obj as DefaultListValueAttribute;
			if (other == null)
			{
				return false;
			}
			if (this.Value != null)
			{
				return this.Value.Equals(other.Value);
			}
			return other.Value == null;
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x0053D1F2 File Offset: 0x0053B3F2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x0053D1FA File Offset: 0x0053B3FA
		protected void SetValue(object value)
		{
			this.value = value;
		}

		// Token: 0x04001D20 RID: 7456
		private object value;
	}
}
