using System;
using System.Runtime.CompilerServices;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	// Token: 0x02000174 RID: 372
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public abstract class GlobalType<[Nullable(0)] TEntity, [Nullable(0)] TGlobal> : GlobalType<TGlobal> where TEntity : IEntityWithGlobals<TGlobal> where TGlobal : GlobalType<TEntity, TGlobal>
	{
		/// <summary>
		/// Whether or not this type is cloneable. Cloning is supported if<br />
		/// all reference typed fields in each sub-class which doesn't override Clone are marked with [CloneByReference]
		/// </summary>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x004D47D8 File Offset: 0x004D29D8
		public virtual bool IsCloneable
		{
			get
			{
				bool flag = this._isCloneable.GetValueOrDefault();
				if (this._isCloneable == null)
				{
					flag = Cloning.IsCloneable<GlobalType<TEntity, TGlobal>>(this, (GlobalType<TEntity, TGlobal> m) => (Func<TEntity, TEntity, TGlobal>)methodof(GlobalType<TEntity, TGlobal>.Clone(TEntity, TEntity)).CreateDelegate(typeof(Func<TEntity, TEntity, TGlobal>), m));
					this._isCloneable = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		/// <summary>
		/// Whether to create new instances of this mod type via <see cref="M:Terraria.ModLoader.GlobalType`2.Clone(`0,`0)" /> or via the default constructor
		/// Defaults to false (default constructor).
		/// </summary>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x004D48A7 File Offset: 0x004D2AA7
		protected virtual bool CloneNewInstances
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Whether this global applies to some entities but not others. <br />
		/// True if the type overrides <see cref="M:Terraria.ModLoader.GlobalType`2.AppliesToEntity(`0,System.Boolean)" />
		/// </summary>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x004D48AC File Offset: 0x004D2AAC
		public sealed override bool ConditionallyAppliesToEntities
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = this._conditionallyAppliesToEntities.GetValueOrDefault();
				if (this._conditionallyAppliesToEntities == null)
				{
					flag = LoaderUtils.HasOverride<GlobalType<TEntity, TGlobal>>(this, (GlobalType<TEntity, TGlobal> m) => (Func<TEntity, bool, bool>)methodof(GlobalType<TEntity, TGlobal>.AppliesToEntity(TEntity, bool)).CreateDelegate(typeof(Func<TEntity, bool, bool>), m));
					this._conditionallyAppliesToEntities = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		/// <summary>
		/// Use this to control whether or not this global should be run on the provided entity instance. <br />
		/// </summary>
		/// <param name="entity"> The entity for which the global instantiation is being checked. </param>
		/// <param name="lateInstantiation">
		/// Whether this check occurs before or after the ModX.SetDefaults call.
		/// <br /> If you're relying on entity values that can be changed by that call, you should likely prefix your return value with the following:
		/// <code> lateInstantiation &amp;&amp; ... </code>
		/// </param>
		// Token: 0x06001DD3 RID: 7635 RVA: 0x004D497B File Offset: 0x004D2B7B
		public virtual bool AppliesToEntity(TEntity entity, bool lateInstantiation)
		{
			return true;
		}

		/// <summary>
		/// Allows you to set the properties of any and every instance that gets created.
		/// </summary>
		// Token: 0x06001DD4 RID: 7636 RVA: 0x004D497E File Offset: 0x004D2B7E
		public virtual void SetDefaults(TEntity entity)
		{
		}

		/// <summary>
		/// Create a copy of this instanced global. Called when an entity is cloned.
		/// </summary>
		/// <param name="from">The entity being cloned. May be null if <see cref="P:Terraria.ModLoader.GlobalType`2.CloneNewInstances" /> is true (via call from <see cref="M:Terraria.ModLoader.GlobalType`2.NewInstance(`0)" />)</param>
		/// <param name="to">The new clone of the entity</param>
		/// <returns>A clone of this global</returns>
		// Token: 0x06001DD5 RID: 7637 RVA: 0x004D4980 File Offset: 0x004D2B80
		public virtual TGlobal Clone([Nullable(2)] TEntity from, TEntity to)
		{
			if (!this.IsCloneable)
			{
				Cloning.WarnNotCloneable(base.GetType());
			}
			return (TGlobal)((object)base.MemberwiseClone());
		}

		/// <summary>
		/// Only called if <see cref="P:Terraria.ModLoader.GlobalType`1.InstancePerEntity" /> and <see cref="M:Terraria.ModLoader.GlobalType`2.AppliesToEntity(`0,System.Boolean)" />(<paramref name="target" />, ...) are both true. <br />
		/// <br />
		/// Returning null is permitted but <b>not recommended</b> over <c>AppliesToEntity</c> for performance reasons. <br />
		/// Only return null when the global is disabled based on some runtime property (eg world seed).
		/// </summary>
		/// <param name="target">The entity instance the global is being instantiated for</param>
		/// <returns></returns>
		// Token: 0x06001DD6 RID: 7638 RVA: 0x004D49A0 File Offset: 0x004D2BA0
		[return: Nullable(2)]
		public virtual TGlobal NewInstance(TEntity target)
		{
			if (this.CloneNewInstances)
			{
				return this.Clone(default(TEntity), target);
			}
			TGlobal tglobal = (TGlobal)((object)Activator.CreateInstance(base.GetType(), true));
			tglobal.Mod = base.Mod;
			tglobal.StaticIndex = base.StaticIndex;
			tglobal.PerEntityIndex = base.PerEntityIndex;
			tglobal._isCloneable = this._isCloneable;
			tglobal._conditionallyAppliesToEntities = this._conditionallyAppliesToEntities;
			return tglobal;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x004D4A2C File Offset: 0x004D2C2C
		public TGlobal Instance(TEntity entity)
		{
			TGlobal result;
			GlobalType<TGlobal>.TryGetGlobal<TGlobal>(entity.Type, entity.EntityGlobals, (TGlobal)((object)this), out result);
			return result;
		}

		// Token: 0x040015B9 RID: 5561
		private bool? _isCloneable;

		// Token: 0x040015BA RID: 5562
		private bool? _conditionallyAppliesToEntities;
	}
}
