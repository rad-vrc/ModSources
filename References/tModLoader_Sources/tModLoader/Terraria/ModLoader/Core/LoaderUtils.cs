using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using Terraria.ModLoader.Exceptions;

namespace Terraria.ModLoader.Core
{
	// Token: 0x0200035E RID: 862
	[NullableContext(1)]
	[Nullable(0)]
	public static class LoaderUtils
	{
		/// <summary> Calls static constructors on the provided type and, optionally, its nested types. </summary>
		// Token: 0x06002FDD RID: 12253 RVA: 0x00537730 File Offset: 0x00535930
		public static void ResetStaticMembers(Type type, bool recursive = true)
		{
			ConstructorInfo typeInitializer = type.TypeInitializer;
			if (typeInitializer != null)
			{
				object value = typeInitializer.GetType().GetProperty("Invoker", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(typeInitializer);
				FieldInfo field = value.GetType().GetField("_invocationFlags", BindingFlags.Instance | BindingFlags.NonPublic);
				object fieldObj = value;
				uint newFlagValue = 0U;
				uint oldFlagValue = (uint)field.GetValue(fieldObj);
				field.SetValue(fieldObj, newFlagValue);
				typeInitializer.Invoke(null, null);
				field.SetValue(fieldObj, oldFlagValue);
			}
			if (recursive)
			{
				Type[] nestedTypes = type.GetNestedTypes();
				for (int i = 0; i < nestedTypes.Length; i++)
				{
					LoaderUtils.ResetStaticMembers(nestedTypes[i], recursive);
				}
			}
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x005377D8 File Offset: 0x005359D8
		public static void ForEachAndAggregateExceptions<[Nullable(2)] T>(IEnumerable<T> enumerable, Action<T> action)
		{
			List<Exception> exceptions = new List<Exception>();
			foreach (T e in enumerable)
			{
				try
				{
					action(e);
				}
				catch (Exception ex)
				{
					ex.Data["contentType"] = ((e is Type) ? e : e.GetType());
					exceptions.Add(ex);
				}
			}
			LoaderUtils.RethrowAggregatedExceptions(exceptions);
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x00537878 File Offset: 0x00535A78
		public static void RethrowAggregatedExceptions(IReadOnlyCollection<Exception> exceptions)
		{
			if (exceptions.Count == 1)
			{
				ExceptionDispatchInfo.Capture(exceptions.Single<Exception>()).Throw();
			}
			if (exceptions.Count > 0)
			{
				throw new MultipleException(exceptions);
			}
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x005378A4 File Offset: 0x00535AA4
		[Obsolete("Poor performance. Use HasOverride instead", true)]
		public static bool HasMethod(Type type, Type declaringType, string method, params Type[] args)
		{
			MethodInfo methodInfo = type.GetMethod(method, args);
			return !(methodInfo == null) && methodInfo.DeclaringType != declaringType;
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.ModLoader.Core.LoaderUtils.MethodOverrideQuery`1.Create(System.Linq.Expressions.Expression{System.Func{`0,System.Delegate}})" />
		/// </summary>
		// Token: 0x06002FE1 RID: 12257 RVA: 0x005378D4 File Offset: 0x00535AD4
		public static MethodInfo ToMethodInfo<[Nullable(2)] T, [Nullable(0)] F>(this Expression<Func<T, F>> expr) where F : Delegate
		{
			MethodInfo method;
			try
			{
				UnaryExpression unaryExpression = expr.Body as UnaryExpression;
				MethodCallExpression methodCallExpression = ((unaryExpression != null) ? unaryExpression.Operand : null) as MethodCallExpression;
				ConstantExpression constantExpression = ((methodCallExpression != null) ? methodCallExpression.Object : null) as ConstantExpression;
				method = (((constantExpression != null) ? constantExpression.Value : null) as MethodInfo);
				if (method == null)
				{
					throw new NullReferenceException();
				}
			}
			catch (Exception e)
			{
				throw new ArgumentException("Invalid hook expression " + ((expr != null) ? expr.ToString() : null), e);
			}
			return method;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x00537964 File Offset: 0x00535B64
		[Obsolete("Exists to support other obsolete methods", true)]
		internal static Expression<Func<T, Delegate>> ToBindingExpression<[Nullable(2)] T>(this MethodInfo method)
		{
			Type[] paramTypes = (from p in method.GetParameters()
			select p.ParameterType).ToArray<Type>();
			Type delType = (method.ReturnType == typeof(void)) ? LoaderUtils._actions[paramTypes.Length] : LoaderUtils._funcs[paramTypes.Length];
			if (method.ReturnType != typeof(void))
			{
				paramTypes = paramTypes.Concat(new Type[]
				{
					method.ReturnType
				}).ToArray<Type>();
			}
			if (paramTypes.Length != 0)
			{
				delType = delType.MakeGenericType(paramTypes);
			}
			MethodInfo createDelegate = new Func<Type, object, Delegate>(method.CreateDelegate).Method;
			ParameterExpression param;
			return Expression.Lambda<Func<T, Delegate>>(Expression.Convert(Expression.Call(Expression.Constant(method), createDelegate, Expression.Constant(delType), param), delType), new ParameterExpression[]
			{
				param
			});
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.ModLoader.Core.LoaderUtils.MethodOverrideQuery`1.Create(System.Linq.Expressions.Expression{System.Func{`0,System.Delegate}})" />
		/// </summary>
		// Token: 0x06002FE3 RID: 12259 RVA: 0x00537A5B File Offset: 0x00535C5B
		public static LoaderUtils.MethodOverrideQuery<T> ToOverrideQuery<[Nullable(2)] T, [Nullable(0)] F>(this Expression<Func<T, F>> expr) where F : Delegate
		{
			return LoaderUtils.MethodOverrideQuery<T>.Create<F>(expr);
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x00537A64 File Offset: 0x00535C64
		[Obsolete("Poor performance. Use MethodOverrideQuery instead", true)]
		public static MethodInfo GetDerivedDefinition(Type t, MethodInfo baseMethod)
		{
			return t.GetMethods().Single((MethodInfo m) => m.GetBaseDefinition() == baseMethod);
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x00537A95 File Offset: 0x00535C95
		[Obsolete("Poor performance. Use the delegate expression version instead", true)]
		public static bool HasOverride(Type t, MethodInfo baseMethod)
		{
			if (!baseMethod.DeclaringType.IsInterface)
			{
				return LoaderUtils.GetDerivedDefinition(t, baseMethod).DeclaringType != baseMethod.DeclaringType;
			}
			return t.IsAssignableTo(baseMethod.DeclaringType);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x00537AC8 File Offset: 0x00535CC8
		public static bool HasOverride<[Nullable(2)] T>(T t, Expression<Func<T, Delegate>> expr)
		{
			return LoaderUtils.HasOverride<T, Delegate>(t, expr);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x00537AD1 File Offset: 0x00535CD1
		public static bool HasOverride<[Nullable(2)] T, [Nullable(0)] F>(T t, Expression<Func<T, F>> expr) where F : Delegate
		{
			return expr.ToOverrideQuery<T, F>().HasOverride(t);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x00537AE0 File Offset: 0x00535CE0
		[Obsolete("Poor performance. Use the delegate expression version instead", true)]
		public static IEnumerable<T> WhereMethodIsOverridden<[Nullable(2)] T>(this IEnumerable<T> providers, MethodInfo method)
		{
			if (!method.IsVirtual)
			{
				string str = "Non-virtual method: ";
				MethodInfo method2 = method;
				throw new ArgumentException(str + ((method2 != null) ? method2.ToString() : null));
			}
			return from p in providers
			where LoaderUtils.HasOverride(p.GetType(), method)
			select p;
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x00537B3B File Offset: 0x00535D3B
		public static IEnumerable<T> WhereMethodIsOverridden<[Nullable(2)] T>(this IEnumerable<T> providers, Expression<Func<T, Delegate>> expr)
		{
			return providers.WhereMethodIsOverridden(expr);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x00537B44 File Offset: 0x00535D44
		public static IEnumerable<T> WhereMethodIsOverridden<[Nullable(2)] T, [Nullable(0)] F>(this IEnumerable<T> providers, Expression<Func<T, F>> expr) where F : Delegate
		{
			return providers.Where(new Func<T, bool>(expr.ToOverrideQuery<T, F>().HasOverride));
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x00537B60 File Offset: 0x00535D60
		public static void MustOverrideTogether<[Nullable(2)] T>(T t, params Expression<Func<T, Delegate>>[] methods)
		{
			int c = methods.Count((Expression<Func<T, Delegate>> m) => LoaderUtils.HasOverride<T>(t, m));
			if (c > 0 && c < methods.Length)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 2);
				defaultInterpolatedStringHandler.AppendFormatted<Type>(t.GetType());
				defaultInterpolatedStringHandler.AppendLiteral(" must override all of (");
				defaultInterpolatedStringHandler.AppendFormatted(string.Join<string>('/', from m in methods
				select m.ToMethodInfo<T, Delegate>().Name));
				defaultInterpolatedStringHandler.AppendLiteral(") or none");
				throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x00537C14 File Offset: 0x00535E14
		internal static bool IsValidated(Type type)
		{
			return !LoaderUtils.validatedTypes.Add(type);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x00537C24 File Offset: 0x00535E24
		static LoaderUtils()
		{
			TypeCaching.OnClear += LoaderUtils.validatedTypes.Clear;
		}

		// Token: 0x04001CD1 RID: 7377
		private static Type[] _actions = new Type[]
		{
			typeof(Action),
			typeof(Action<>),
			typeof(Action<, >),
			typeof(Action<, , >),
			typeof(Action<, , , >),
			typeof(Action<, , , , >)
		};

		// Token: 0x04001CD2 RID: 7378
		private static Type[] _funcs = new Type[]
		{
			typeof(Func<>),
			typeof(Func<, >),
			typeof(Func<, , >),
			typeof(Func<, , , >),
			typeof(Func<, , , , >),
			typeof(Func<, , , , , >)
		};

		// Token: 0x04001CD3 RID: 7379
		private static readonly HashSet<Type> validatedTypes = new HashSet<Type>();

		// Token: 0x02000AB3 RID: 2739
		[Nullable(0)]
		public class MethodOverrideQuery<[Nullable(2)] T>
		{
			// Token: 0x17000920 RID: 2336
			// (get) Token: 0x060059E1 RID: 23009 RVA: 0x006A2CCF File Offset: 0x006A0ECF
			public MethodInfo Method { get; }

			// Token: 0x17000921 RID: 2337
			// (get) Token: 0x060059E2 RID: 23010 RVA: 0x006A2CD7 File Offset: 0x006A0ED7
			[Nullable(new byte[]
			{
				1,
				1,
				2
			})]
			public Func<T, Delegate> Binder { [return: Nullable(new byte[]
			{
				1,
				1,
				2
			})] get; }

			// Token: 0x060059E3 RID: 23011 RVA: 0x006A2CDF File Offset: 0x006A0EDF
			private MethodOverrideQuery(MethodInfo method, [Nullable(new byte[]
			{
				1,
				1,
				2
			})] Func<T, Delegate> binder)
			{
				this.Method = method;
				this.Binder = binder;
			}

			// Token: 0x060059E4 RID: 23012 RVA: 0x006A2CF8 File Offset: 0x006A0EF8
			public bool HasOverride(T t)
			{
				Delegate @delegate = this.Binder(t);
				if (@delegate != null)
				{
					MethodInfo impl = @delegate.Method;
					return impl != this.Method;
				}
				return false;
			}

			/// <summary>
			/// <inheritdoc cref="M:Terraria.ModLoader.Core.LoaderUtils.MethodOverrideQuery`1.Create``1(System.Linq.Expressions.Expression{System.Func{`0,``0}})" />
			/// </summary>
			// Token: 0x060059E5 RID: 23013 RVA: 0x006A2D2A File Offset: 0x006A0F2A
			public static LoaderUtils.MethodOverrideQuery<T> Create(Expression<Func<T, Delegate>> expr)
			{
				return LoaderUtils.MethodOverrideQuery<T>.Create<Delegate>(expr);
			}

			/// <summary>
			/// The <paramref name="expr" /> must take one of the following forms
			/// <code>e =&gt; e.Method</code>
			/// <code>e =&gt; (DelegateType)e.Method</code>
			/// <code>e =&gt; (e(Interface)).Method</code>
			/// </summary>
			// Token: 0x060059E6 RID: 23014 RVA: 0x006A2D34 File Offset: 0x006A0F34
			public static LoaderUtils.MethodOverrideQuery<T> Create<[Nullable(0)] F>(Expression<Func<T, F>> expr) where F : Delegate
			{
				MethodInfo method = expr.ToMethodInfo<T, F>();
				return LoaderUtils.MethodOverrideQuery<T>._cache.GetOrAdd(method, (MethodInfo _) => new LoaderUtils.MethodOverrideQuery<T>(method, LoaderUtils.MethodOverrideQuery<T>.AddTypeCheckIfNecessary<F>(expr).Compile()));
			}

			/// <summary>
			/// Converts <code>e =&gt; (e(Interface)).Method</code> to <code>e =&gt; e is Interface ? (e(Interface)).Method : null</code>
			/// </summary>
			/// <returns>The expression with type check if an interface cast was present, otherwise the original expression</returns>
			// Token: 0x060059E7 RID: 23015 RVA: 0x006A2D7C File Offset: 0x006A0F7C
			[return: Nullable(new byte[]
			{
				1,
				1,
				1,
				2
			})]
			private static Expression<Func<T, F>> AddTypeCheckIfNecessary<[Nullable(2)] F>(Expression<Func<T, F>> expr)
			{
				UnaryExpression unaryExpression = expr.Body as UnaryExpression;
				if (unaryExpression != null)
				{
					MethodCallExpression methodCallExpression = unaryExpression.Operand as MethodCallExpression;
					if (methodCallExpression != null)
					{
						ReadOnlyCollection<Expression> createDelegateArgs = methodCallExpression.Arguments;
						ConstantExpression constantExpression = methodCallExpression.Object as ConstantExpression;
						if (constantExpression != null && constantExpression.Value is MethodInfo)
						{
							if (createDelegateArgs.Count >= 2)
							{
								unaryExpression = (createDelegateArgs[1] as UnaryExpression);
								if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert)
								{
									Type type = unaryExpression.Type;
									Expression target = unaryExpression.Operand;
									return expr.Update(Expression.Condition(Expression.TypeIs(target, type), expr.Body, Expression.Constant(null, expr.Body.Type)), expr.Parameters);
								}
							}
							return expr;
						}
					}
				}
				return expr;
			}

			// Token: 0x04006DE2 RID: 28130
			private static readonly ConcurrentDictionary<MethodInfo, LoaderUtils.MethodOverrideQuery<T>> _cache = new ConcurrentDictionary<MethodInfo, LoaderUtils.MethodOverrideQuery<T>>();
		}
	}
}
