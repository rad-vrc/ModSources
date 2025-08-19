using System;
using System.ComponentModel;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Defines the default key value to be added when using the ModConfig UI to add elements to a Dictionary. Works the same as System.ComponentModel.DefaultValueAttribute, but can't inherit from it because it would break when deserializing any data structure annotated with it. This attribute compliments DefaultListValueAttribute when used annotating a Dictionary.
	/// </summary>
	// Token: 0x02000379 RID: 889
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class DefaultDictionaryKeyValueAttribute : Attribute
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x0053D203 File Offset: 0x0053B403
		public virtual object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x0053D20C File Offset: 0x0053B40C
		public DefaultDictionaryKeyValueAttribute(Type type, string value)
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

		// Token: 0x060030B0 RID: 12464 RVA: 0x0053D280 File Offset: 0x0053B480
		public DefaultDictionaryKeyValueAttribute(char value)
		{
			this.value = value;
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x0053D294 File Offset: 0x0053B494
		public DefaultDictionaryKeyValueAttribute(byte value)
		{
			this.value = value;
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0053D2A8 File Offset: 0x0053B4A8
		public DefaultDictionaryKeyValueAttribute(short value)
		{
			this.value = value;
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0053D2BC File Offset: 0x0053B4BC
		public DefaultDictionaryKeyValueAttribute(int value)
		{
			this.value = value;
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x0053D2D0 File Offset: 0x0053B4D0
		public DefaultDictionaryKeyValueAttribute(long value)
		{
			this.value = value;
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x0053D2E4 File Offset: 0x0053B4E4
		public DefaultDictionaryKeyValueAttribute(float value)
		{
			this.value = value;
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x0053D2F8 File Offset: 0x0053B4F8
		public DefaultDictionaryKeyValueAttribute(double value)
		{
			this.value = value;
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0053D30C File Offset: 0x0053B50C
		public DefaultDictionaryKeyValueAttribute(bool value)
		{
			this.value = value;
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x0053D320 File Offset: 0x0053B520
		public DefaultDictionaryKeyValueAttribute(string value)
		{
			this.value = value;
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x0053D32F File Offset: 0x0053B52F
		public DefaultDictionaryKeyValueAttribute(object value)
		{
			this.value = value;
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x0053D340 File Offset: 0x0053B540
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DefaultDictionaryKeyValueAttribute other = obj as DefaultDictionaryKeyValueAttribute;
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

		// Token: 0x060030BB RID: 12475 RVA: 0x0053D382 File Offset: 0x0053B582
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x0053D38A File Offset: 0x0053B58A
		protected void SetValue(object value)
		{
			this.value = value;
		}

		// Token: 0x04001D21 RID: 7457
		private object value;
	}
}
