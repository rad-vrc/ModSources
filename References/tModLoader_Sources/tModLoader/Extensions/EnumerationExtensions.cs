using System;

namespace Extensions
{
	// Token: 0x0200001C RID: 28
	public static class EnumerationExtensions
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00005960 File Offset: 0x00003B60
		public static T Include<T>(this Enum value, T append)
		{
			Type type = value.GetType();
			object obj = value;
			EnumerationExtensions._Value value2 = new EnumerationExtensions._Value(append, type);
			if (value2.Signed is long)
			{
				obj = (Convert.ToInt64(value) | value2.Signed.Value);
			}
			else if (value2.Unsigned is ulong)
			{
				obj = (Convert.ToUInt64(value) | value2.Unsigned.Value);
			}
			return (T)((object)Enum.Parse(type, obj.ToString()));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000059EC File Offset: 0x00003BEC
		public static T Remove<T>(this Enum value, T remove)
		{
			Type type = value.GetType();
			object obj = value;
			EnumerationExtensions._Value value2 = new EnumerationExtensions._Value(remove, type);
			if (value2.Signed is long)
			{
				obj = (Convert.ToInt64(value) & ~value2.Signed.Value);
			}
			else if (value2.Unsigned is ulong)
			{
				obj = (Convert.ToUInt64(value) & ~value2.Unsigned.Value);
			}
			return (T)((object)Enum.Parse(type, obj.ToString()));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005A78 File Offset: 0x00003C78
		public static bool Has<T>(this Enum value, T check)
		{
			Type type = value.GetType();
			EnumerationExtensions._Value value2 = new EnumerationExtensions._Value(check, type);
			if (value2.Signed is long)
			{
				return (Convert.ToInt64(value) & value2.Signed.Value) == value2.Signed.Value;
			}
			return value2.Unsigned is ulong && (Convert.ToUInt64(value) & value2.Unsigned.Value) == value2.Unsigned.Value;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005AFE File Offset: 0x00003CFE
		public static bool Missing<T>(this Enum obj, T value)
		{
			return !obj.Has(value);
		}

		// Token: 0x02000786 RID: 1926
		private class _Value
		{
			// Token: 0x06004D9A RID: 19866 RVA: 0x006734A4 File Offset: 0x006716A4
			public _Value(object value, Type type)
			{
				if (!type.IsEnum)
				{
					throw new ArgumentException("Value provided is not an enumerated type!");
				}
				Type underlyingType = Enum.GetUnderlyingType(type);
				if (underlyingType.Equals(EnumerationExtensions._Value._UInt32) || underlyingType.Equals(EnumerationExtensions._Value._UInt64))
				{
					this.Unsigned = new ulong?(Convert.ToUInt64(value));
					return;
				}
				this.Signed = new long?(Convert.ToInt64(value));
			}

			// Token: 0x04006595 RID: 26005
			private static Type _UInt64 = typeof(ulong);

			// Token: 0x04006596 RID: 26006
			private static Type _UInt32 = typeof(long);

			// Token: 0x04006597 RID: 26007
			public long? Signed;

			// Token: 0x04006598 RID: 26008
			public ulong? Unsigned;
		}
	}
}
