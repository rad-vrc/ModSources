using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Terraria.ModLoader.Core
{
	// Token: 0x0200035D RID: 861
	public class HookList<T> where T : class
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x005375BA File Offset: 0x005357BA
		public LoaderUtils.MethodOverrideQuery<T> HookOverrideQuery { get; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x005375C2 File Offset: 0x005357C2
		public MethodInfo Method
		{
			get
			{
				return this.HookOverrideQuery.Method;
			}
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x005375CF File Offset: 0x005357CF
		public HookList(LoaderUtils.MethodOverrideQuery<T> hook)
		{
			this.HookOverrideQuery = hook;
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x005375F4 File Offset: 0x005357F4
		[Obsolete("Use HookList.Create instead", true)]
		public HookList(MethodInfo method) : this(method.ToBindingExpression<T>().ToOverrideQuery<T, Delegate>())
		{
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x00537607 File Offset: 0x00535807
		public FilteredArrayEnumerator<T> Enumerate(T[] instances)
		{
			return new FilteredArrayEnumerator<T>(instances, this.indices);
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x00537615 File Offset: 0x00535815
		public FilteredSpanEnumerator<T> Enumerate(ReadOnlySpan<T> instances)
		{
			return new FilteredSpanEnumerator<T>(instances, this.indices);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x00537623 File Offset: 0x00535823
		public FilteredSpanEnumerator<T> Enumerate(IEntityWithInstances<T> entity)
		{
			return this.Enumerate(entity.Instances);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x00537636 File Offset: 0x00535836
		public IEnumerable<T> EnumerateSlow(IReadOnlyList<T> instances)
		{
			foreach (int i in this.indices)
			{
				yield return instances[i];
			}
			int[] array = null;
			yield break;
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x0053764D File Offset: 0x0053584D
		public ReadOnlySpan<T> Enumerate()
		{
			return this.defaultInstances;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x0053765C File Offset: 0x0053585C
		public void Update(IReadOnlyList<T> allDefaultInstances)
		{
			IReadOnlyList<IIndexed> indexed = allDefaultInstances as IReadOnlyList<IIndexed>;
			if (indexed != null && !HookList<T>.Validate(indexed))
			{
				throw new ArgumentException("allDefaultInstances elements have missing or duplicate IIndexed.Index");
			}
			List<T> list = new List<T>(allDefaultInstances.Count);
			List<int> inds = new List<int>();
			for (int i = 0; i < allDefaultInstances.Count; i++)
			{
				T inst = allDefaultInstances[i];
				if (this.HookOverrideQuery.HasOverride(inst))
				{
					list.Add(inst);
					inds.Add(i);
				}
			}
			this.defaultInstances = list.ToArray();
			this.indices = inds.ToArray();
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x005376E8 File Offset: 0x005358E8
		private static bool Validate(IReadOnlyList<IIndexed> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if ((int)list[i].Index != i)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.ModLoader.Core.LoaderUtils.ToOverrideQuery``2(System.Linq.Expressions.Expression{System.Func{``0,``1}})" />
		/// </summary>
		// Token: 0x06002FDB RID: 12251 RVA: 0x00537718 File Offset: 0x00535918
		public static HookList<T> Create(Expression<Func<T, Delegate>> expr)
		{
			return HookList<T>.Create<Delegate>(expr);
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.ModLoader.Core.LoaderUtils.ToOverrideQuery``2(System.Linq.Expressions.Expression{System.Func{``0,``1}})" />
		/// </summary>
		// Token: 0x06002FDC RID: 12252 RVA: 0x00537720 File Offset: 0x00535920
		public static HookList<T> Create<F>(Expression<Func<T, F>> expr) where F : Delegate
		{
			return new HookList<T>(expr.ToOverrideQuery<T, F>());
		}

		// Token: 0x04001CCF RID: 7375
		private int[] indices = Array.Empty<int>();

		// Token: 0x04001CD0 RID: 7376
		private T[] defaultInstances = Array.Empty<T>();
	}
}
