using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Terraria.ModLoader.Core
{
	// Token: 0x0200035A RID: 858
	public class GlobalHookList<TGlobal> where TGlobal : GlobalType<TGlobal>
	{
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x00536916 File Offset: 0x00534B16
		public LoaderUtils.MethodOverrideQuery<TGlobal> HookOverrideQuery { get; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x0053691E File Offset: 0x00534B1E
		public MethodInfo Method
		{
			get
			{
				return this.HookOverrideQuery.Method;
			}
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x0053692B File Offset: 0x00534B2B
		public GlobalHookList(LoaderUtils.MethodOverrideQuery<TGlobal> hook)
		{
			this.HookOverrideQuery = hook;
			this.Update();
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x00536940 File Offset: 0x00534B40
		[Obsolete("Use HookList.Create instead", true)]
		public GlobalHookList(MethodInfo method) : this(method.ToBindingExpression<TGlobal>().ToOverrideQuery<TGlobal, Delegate>())
		{
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x00536953 File Offset: 0x00534B53
		public ReadOnlySpan<TGlobal> Enumerate()
		{
			return this.hookGlobals;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x00536960 File Offset: 0x00534B60
		public ReadOnlySpan<TGlobal> Enumerate(int type)
		{
			return this.ForType(type);
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x0053696E File Offset: 0x00534B6E
		public EntityGlobalsEnumerator<TGlobal> Enumerate(IEntityWithGlobals<TGlobal> entity)
		{
			return new EntityGlobalsEnumerator<TGlobal>(this.ForType(entity.Type), entity);
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x00536982 File Offset: 0x00534B82
		private TGlobal[] ForType(int type)
		{
			if (this.hookGlobals.Length != 0)
			{
				return this.hookGlobalsByType[type];
			}
			return this.hookGlobals;
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x0053699C File Offset: 0x00534B9C
		public void Update()
		{
			this.hookGlobals = GlobalList<TGlobal>.Globals.Where(new Func<TGlobal, bool>(this.HookOverrideQuery.HasOverride)).ToArray<TGlobal>();
			this.hookGlobalsByType = (GlobalTypeLookups<TGlobal>.Initialized ? GlobalTypeLookups<TGlobal>.BuildPerTypeGlobalLists(this.hookGlobals) : null);
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.ModLoader.Core.LoaderUtils.ToOverrideQuery``2(System.Linq.Expressions.Expression{System.Func{``0,``1}})" />
		/// </summary>
		// Token: 0x06002FBD RID: 12221 RVA: 0x005369EA File Offset: 0x00534BEA
		public static GlobalHookList<TGlobal> Create(Expression<Func<TGlobal, Delegate>> expr)
		{
			return GlobalHookList<TGlobal>.Create<Delegate>(expr);
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.ModLoader.Core.LoaderUtils.ToOverrideQuery``2(System.Linq.Expressions.Expression{System.Func{``0,``1}})" />
		/// </summary>
		// Token: 0x06002FBE RID: 12222 RVA: 0x005369F2 File Offset: 0x00534BF2
		public static GlobalHookList<TGlobal> Create<F>(Expression<Func<TGlobal, F>> expr) where F : Delegate
		{
			return new GlobalHookList<TGlobal>(expr.ToOverrideQuery<TGlobal, F>());
		}

		// Token: 0x04001CC4 RID: 7364
		private TGlobal[] hookGlobals;

		// Token: 0x04001CC5 RID: 7365
		private TGlobal[][] hookGlobalsByType;
	}
}
