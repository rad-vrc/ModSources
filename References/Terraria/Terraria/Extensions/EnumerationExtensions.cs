using System;

namespace Extensions
{
	// Token: 0x02000011 RID: 17
	public static class EnumerationExtensions
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00004D7C File Offset: 0x00002F7C
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

		// Token: 0x0600009B RID: 155 RVA: 0x00004E08 File Offset: 0x00003008
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

		// Token: 0x0600009C RID: 156 RVA: 0x00004E94 File Offset: 0x00003094
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

		// Token: 0x0600009D RID: 157 RVA: 0x00004F1A File Offset: 0x0000311A
		public static bool Missing<T>(this Enum obj, T value)
		{
			return !obj.Has(value);
		}

		// Token: 0x0200049A RID: 1178
		private class _Value
		{
			// Token: 0x06002EA7 RID: 11943 RVA: 0x005C49EC File Offset: 0x005C2BEC
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

			// Token: 0x040055BA RID: 21946
			private static Type _UInt64 = typeof(ulong);

			// Token: 0x040055BB RID: 21947
			private static Type _UInt32 = typeof(long);

			// Token: 0x040055BC RID: 21948
			public long? Signed;

			// Token: 0x040055BD RID: 21949
			public ulong? Unsigned;
		}
	}
}
