using System;
using System.Linq;
using System.Reflection;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003BB RID: 955
	internal class SetElementWrapper<V> : ISetElementWrapper
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x00545519 File Offset: 0x00543719
		// (set) Token: 0x060032C1 RID: 12993 RVA: 0x00545524 File Offset: 0x00543724
		public V Value
		{
			get
			{
				return this._value;
			}
			set
			{
				MethodInfo removeMethod = this.set.GetType().GetMethods().FirstOrDefault((MethodInfo m) => m.Name == "Remove");
				MethodInfo addMethod = this.set.GetType().GetMethods().FirstOrDefault((MethodInfo m) => m.Name == "Add");
				if (!(bool)this.set.GetType().GetMethods().FirstOrDefault((MethodInfo m) => m.Name == "Contains").Invoke(this.set, new object[]
				{
					value
				}))
				{
					removeMethod.Invoke(this.set, new object[]
					{
						this._value
					});
					this._value = value;
					addMethod.Invoke(this.set, new object[]
					{
						this._value
					});
				}
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x00545639 File Offset: 0x00543839
		object ISetElementWrapper.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x00545646 File Offset: 0x00543846
		public SetElementWrapper(V value, object set)
		{
			this.set = set;
			this._value = value;
		}

		// Token: 0x04001DBB RID: 7611
		private readonly object set;

		// Token: 0x04001DBC RID: 7612
		private V _value;
	}
}
