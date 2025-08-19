using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// This TypeConverter facilitates converting to and from the string Type. This is necessary for Objects that are to be used as Dictionary keys, since the JSON for keys needs to be a string. Classes annotated with this TypeConverter need to implement a static FromString method that returns T.
	/// </summary>
	/// <typeparam name="T">The Type that implements the static FromString method that returns Type T.</typeparam>
	// Token: 0x02000391 RID: 913
	public class ToFromStringConverter<T> : TypeConverter
	{
		// Token: 0x0600314A RID: 12618 RVA: 0x0053F384 File Offset: 0x0053D584
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType != typeof(string);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x0053F396 File Offset: 0x0053D596
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x0053F3B4 File Offset: 0x0053D5B4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			MethodInfo parse = typeof(T).GetMethod("FromString", new Type[]
			{
				typeof(string)
			});
			if (parse != null && parse.IsStatic && parse.ReturnType == typeof(T))
			{
				return parse.Invoke(null, new object[]
				{
					value
				});
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(82, 2);
			defaultInterpolatedStringHandler.AppendLiteral("The ");
			defaultInterpolatedStringHandler.AppendFormatted(typeof(T).Name);
			defaultInterpolatedStringHandler.AppendLiteral(" type does not have a public static FromString(string) method that returns a ");
			defaultInterpolatedStringHandler.AppendFormatted(typeof(T).Name);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			throw new JsonException(defaultInterpolatedStringHandler.ToStringAndClear());
		}
	}
}
