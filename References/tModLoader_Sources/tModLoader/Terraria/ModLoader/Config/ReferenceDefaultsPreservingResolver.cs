using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Custom ContractResolver for facilitating reference type defaults.
	/// The ShouldSerialize code enables unchanged-by-user reference type defaults to properly not serialize.
	/// The ValueProvider code helps during deserialization to not
	/// </summary>
	// Token: 0x02000387 RID: 903
	[NullableContext(1)]
	[Nullable(0)]
	internal class ReferenceDefaultsPreservingResolver : DefaultContractResolver
	{
		// Token: 0x06003106 RID: 12550 RVA: 0x0053E9A0 File Offset: 0x0053CBA0
		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
			if (!type.IsClass)
			{
				return props;
			}
			ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
			if (ctor == null)
			{
				return props;
			}
			object referenceInstance = ctor.Invoke(null);
			using (IEnumerator<JsonProperty> enumerator = (from p in props
			where p.Readable
			select p).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonProperty prop = enumerator.Current;
					if (!(prop.PropertyType == null) && !prop.PropertyType.IsValueType)
					{
						if (prop.Writable)
						{
							if (prop.PropertyType.GetConstructor(Type.EmptyTypes) != null)
							{
								Func<object> defaultValueCreator = () => prop.ValueProvider.GetValue(ctor.Invoke(null));
								prop.ValueProvider = new ReferenceDefaultsPreservingResolver.NullToDefaultValueProvider(prop.ValueProvider, defaultValueCreator);
							}
							else if (prop.PropertyType.IsArray)
							{
								Func<object> defaultValueCreator2 = () => prop.ValueProvider.GetValue(ctor.Invoke(null));
								prop.ValueProvider = new ReferenceDefaultsPreservingResolver.NullToDefaultValueProvider(prop.ValueProvider, defaultValueCreator2);
							}
						}
						JsonProperty prop2 = prop;
						if (prop2.ShouldSerialize == null)
						{
							prop2.ShouldSerialize = delegate(object instance)
							{
								object value = prop.ValueProvider.GetValue(instance);
								object refVal = prop.ValueProvider.GetValue(referenceInstance);
								return !ConfigManager.ObjectEquals(value, refVal);
							};
						}
					}
				}
			}
			return props;
		}

		// Token: 0x02000AEF RID: 2799
		[Nullable(0)]
		public abstract class ValueProviderDecorator : IValueProvider
		{
			// Token: 0x06005ADB RID: 23259 RVA: 0x006A479B File Offset: 0x006A299B
			public ValueProviderDecorator(IValueProvider baseProvider)
			{
				if (baseProvider == null)
				{
					throw new ArgumentNullException();
				}
				this.baseProvider = baseProvider;
			}

			// Token: 0x06005ADC RID: 23260 RVA: 0x006A47B4 File Offset: 0x006A29B4
			[return: Nullable(2)]
			public virtual object GetValue(object target)
			{
				return this.baseProvider.GetValue(target);
			}

			// Token: 0x06005ADD RID: 23261 RVA: 0x006A47C2 File Offset: 0x006A29C2
			public virtual void SetValue(object target, [Nullable(2)] object value)
			{
				this.baseProvider.SetValue(target, value);
			}

			// Token: 0x04006E90 RID: 28304
			private readonly IValueProvider baseProvider;
		}

		// Token: 0x02000AF0 RID: 2800
		[Nullable(0)]
		private class NullToDefaultValueProvider : ReferenceDefaultsPreservingResolver.ValueProviderDecorator
		{
			// Token: 0x06005ADE RID: 23262 RVA: 0x006A47D1 File Offset: 0x006A29D1
			public NullToDefaultValueProvider(IValueProvider baseProvider, [Nullable(new byte[]
			{
				1,
				2
			})] Func<object> defaultValueGenerator) : base(baseProvider)
			{
				this.defaultValueGenerator = defaultValueGenerator;
			}

			// Token: 0x06005ADF RID: 23263 RVA: 0x006A47E1 File Offset: 0x006A29E1
			public override void SetValue(object target, [Nullable(2)] object value)
			{
				base.SetValue(target, value ?? this.defaultValueGenerator());
			}

			// Token: 0x04006E91 RID: 28305
			[Nullable(new byte[]
			{
				1,
				2
			})]
			private readonly Func<object> defaultValueGenerator;
		}
	}
}
