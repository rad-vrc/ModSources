using System;
using System.Reflection;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003B7 RID: 951
	public class PropertyFieldWrapper
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x00544CDF File Offset: 0x00542EDF
		public bool IsField
		{
			get
			{
				return this.fieldInfo != null;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x00544CED File Offset: 0x00542EED
		public bool IsProperty
		{
			get
			{
				return this.propertyInfo != null;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x00544CFB File Offset: 0x00542EFB
		public MemberInfo MemberInfo
		{
			get
			{
				return this.fieldInfo ?? this.propertyInfo;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x00544D0D File Offset: 0x00542F0D
		public string Name
		{
			get
			{
				FieldInfo fieldInfo = this.fieldInfo;
				return ((fieldInfo != null) ? fieldInfo.Name : null) ?? this.propertyInfo.Name;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x00544D30 File Offset: 0x00542F30
		public Type Type
		{
			get
			{
				FieldInfo fieldInfo = this.fieldInfo;
				return ((fieldInfo != null) ? fieldInfo.FieldType : null) ?? this.propertyInfo.PropertyType;
			}
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x00544D53 File Offset: 0x00542F53
		public PropertyFieldWrapper(FieldInfo fieldInfo)
		{
			this.fieldInfo = fieldInfo;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x00544D62 File Offset: 0x00542F62
		public PropertyFieldWrapper(PropertyInfo propertyInfo)
		{
			this.propertyInfo = propertyInfo;
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x00544D71 File Offset: 0x00542F71
		public object GetValue(object obj)
		{
			if (this.fieldInfo != null)
			{
				return this.fieldInfo.GetValue(obj);
			}
			return this.propertyInfo.GetValue(obj, null);
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x00544D9B File Offset: 0x00542F9B
		public void SetValue(object obj, object value)
		{
			if (this.fieldInfo != null)
			{
				this.fieldInfo.SetValue(obj, value);
				return;
			}
			if (this.propertyInfo.CanWrite)
			{
				this.propertyInfo.SetValue(obj, value, null);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x00544DD4 File Offset: 0x00542FD4
		public bool CanWrite
		{
			get
			{
				return this.fieldInfo != null || this.propertyInfo.CanWrite;
			}
		}

		// Token: 0x04001DB0 RID: 7600
		private readonly FieldInfo fieldInfo;

		// Token: 0x04001DB1 RID: 7601
		private readonly PropertyInfo propertyInfo;
	}
}
