using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using ReLogic.Content;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000354 RID: 852
	public static class Cloning
	{
		// Token: 0x06002F8C RID: 12172 RVA: 0x005361AC File Offset: 0x005343AC
		public static bool IsCloneable<T>(T t, Expression<Func<T, Delegate>> cloneMethod)
		{
			return Cloning.IsCloneable<T, Delegate>(t, cloneMethod);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x005361B8 File Offset: 0x005343B8
		public static bool IsCloneable<T, F>(T t, Expression<Func<T, F>> cloneMethod) where F : Delegate
		{
			Type type = t.GetType();
			Cloning.TypeCloningInfo typeInfo;
			if (!Cloning.typeInfos.TryGetValue(type, out typeInfo))
			{
				typeInfo = Cloning.ComputeInfo(type, cloneMethod.ToOverrideQuery<T, F>().Binder(t).Method.DeclaringType);
			}
			return typeInfo.IsCloneable;
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x0053620C File Offset: 0x0053440C
		private static Cloning.TypeCloningInfo ComputeInfo(Type type, Type cloneableAncestor)
		{
			Cloning.TypeCloningInfo info = new Cloning.TypeCloningInfo
			{
				type = type,
				overridesClone = (type == cloneableAncestor)
			};
			if (!info.overridesClone)
			{
				info.fieldsWhichMightNeedDeepCloning = (from f in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where f.DeclaringType == type && !Cloning.IsCloneByReference(f)
				select f).ToArray<FieldInfo>();
				Cloning.TypeCloningInfo typeInfo;
				info.baseTypeInfo = (Cloning.typeInfos.TryGetValue(type.BaseType, out typeInfo) ? typeInfo : Cloning.ComputeInfo(type.BaseType, cloneableAncestor));
			}
			Cloning.typeInfos[type] = info;
			return info;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x005362C0 File Offset: 0x005344C0
		private static bool IsCloneByReference(FieldInfo f)
		{
			return f.GetCustomAttribute<CloneByReference>() != null || Cloning.IsCloneByReference(f.FieldType);
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x005362D7 File Offset: 0x005344D7
		private static bool IsCloneByReference(Type type)
		{
			return type.IsValueType || type.GetCustomAttribute<CloneByReference>() != null || Cloning.IsImmutable(type);
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x005362F4 File Offset: 0x005344F4
		public static bool IsImmutable(Type type)
		{
			if (type.IsGenericType && !type.IsGenericTypeDefinition && Cloning.IsImmutable(type.GetGenericTypeDefinition()))
			{
				return true;
			}
			ConditionalWeakTable<Type, object> obj = Cloning.immutableTypes;
			bool result;
			lock (obj)
			{
				object obj2;
				result = Cloning.immutableTypes.TryGetValue(type, out obj2);
			}
			return result;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x0053635C File Offset: 0x0053455C
		public static void AddImmutableType(Type type)
		{
			ConditionalWeakTable<Type, object> obj = Cloning.immutableTypes;
			lock (obj)
			{
				Cloning.immutableTypes.AddOrUpdate(type, null);
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x005363A4 File Offset: 0x005345A4
		public static void WarnNotCloneable(Type type)
		{
			Cloning.typeInfos[type].Warn();
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x005363B8 File Offset: 0x005345B8
		static Cloning()
		{
			TypeCaching.OnClear += Cloning.typeInfos.Clear;
			Cloning.AddImmutableType(typeof(string));
			Cloning.AddImmutableType(typeof(Asset));
		}

		// Token: 0x04001CB1 RID: 7345
		private static Dictionary<Type, Cloning.TypeCloningInfo> typeInfos = new Dictionary<Type, Cloning.TypeCloningInfo>();

		// Token: 0x04001CB2 RID: 7346
		private static ConditionalWeakTable<Type, object> immutableTypes = new ConditionalWeakTable<Type, object>();

		// Token: 0x02000AA6 RID: 2726
		private class TypeCloningInfo
		{
			// Token: 0x1700091B RID: 2331
			// (get) Token: 0x060059B8 RID: 22968 RVA: 0x006A27C4 File Offset: 0x006A09C4
			public bool IsCloneable
			{
				get
				{
					return this.overridesClone || (this.fieldsWhichMightNeedDeepCloning.Length == 0 && this.baseTypeInfo.IsCloneable);
				}
			}

			// Token: 0x060059B9 RID: 22969 RVA: 0x006A27E8 File Offset: 0x006A09E8
			public void Warn()
			{
				if (this.warnCheckDone)
				{
					return;
				}
				if (!this.IsCloneable)
				{
					if (this.fieldsWhichMightNeedDeepCloning.Length == 0)
					{
						this.baseTypeInfo.Warn();
					}
					else
					{
						IEnumerable<FieldInfo> fields = this.fieldsWhichMightNeedDeepCloning;
						Cloning.TypeCloningInfo b = this.baseTypeInfo;
						while (!b.overridesClone)
						{
							fields = fields.Concat(b.fieldsWhichMightNeedDeepCloning);
							b = b.baseTypeInfo;
						}
						string[] array = new string[6];
						array[0] = this.type.FullName;
						array[1] = " has reference fields (";
						array[2] = string.Join(", ", from f in fields
						select f.Name);
						array[3] = ") that may not be safe to share between clones.";
						array[4] = Environment.NewLine;
						array[5] = "For deep-cloning, add a custom Clone override and make proper copies of these fields. If shallow (memberwise) cloning is acceptable, mark the fields with [CloneByReference] or properties with [field: CloneByReference]";
						string msg = string.Concat(array);
						Logging.tML.Warn(msg);
					}
				}
				this.warnCheckDone = true;
			}

			// Token: 0x04006DBB RID: 28091
			public Type type;

			// Token: 0x04006DBC RID: 28092
			public bool overridesClone;

			// Token: 0x04006DBD RID: 28093
			public FieldInfo[] fieldsWhichMightNeedDeepCloning;

			// Token: 0x04006DBE RID: 28094
			public Cloning.TypeCloningInfo baseTypeInfo;

			// Token: 0x04006DBF RID: 28095
			public bool warnCheckDone;
		}
	}
}
