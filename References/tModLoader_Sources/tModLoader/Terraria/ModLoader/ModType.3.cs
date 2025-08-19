using System;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	// Token: 0x020001D2 RID: 466
	public abstract class ModType<TEntity, TModType> : ModType<TEntity> where TModType : ModType<TEntity, TModType>
	{
		/// <summary>
		/// Whether or not this type is cloneable. Cloning is supported if<br />
		/// all reference typed fields in each sub-class which doesn't override Clone are marked with [CloneByReference]
		/// </summary>
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x004E9E78 File Offset: 0x004E8078
		public virtual bool IsCloneable
		{
			get
			{
				bool flag = this._isCloneable.GetValueOrDefault();
				if (this._isCloneable == null)
				{
					flag = Cloning.IsCloneable<ModType<TEntity, TModType>>(this, (ModType<TEntity, TModType> m) => (Func<TEntity, TModType>)methodof(ModType<TEntity, TModType>.Clone(TEntity)).CreateDelegate(typeof(Func<TEntity, TModType>), m));
					this._isCloneable = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		/// <summary>
		/// Whether to create new instances of this mod type via <see cref="M:Terraria.ModLoader.ModType`2.Clone(`0)" /> or via the default constructor
		/// Defaults to false (default constructor).
		/// </summary>
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x004E9F47 File Offset: 0x004E8147
		protected virtual bool CloneNewInstances
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Create a copy of this instanced global. Called when an entity is cloned.
		/// </summary>
		/// <param name="newEntity">The new clone of the entity</param>
		/// <returns>A clone of this mod type</returns>
		// Token: 0x060024A2 RID: 9378 RVA: 0x004E9F4A File Offset: 0x004E814A
		public virtual TModType Clone(TEntity newEntity)
		{
			if (!this.IsCloneable)
			{
				Cloning.WarnNotCloneable(base.GetType());
			}
			TModType tmodType = (TModType)((object)base.MemberwiseClone());
			tmodType.Entity = newEntity;
			return tmodType;
		}

		/// <summary>
		/// Create a new instance of this ModType for a specific entity
		/// </summary>
		/// <param name="entity">The entity instance the mod type is being instantiated for</param>
		/// <returns></returns>
		// Token: 0x060024A3 RID: 9379 RVA: 0x004E9F76 File Offset: 0x004E8176
		public virtual TModType NewInstance(TEntity entity)
		{
			if (this.CloneNewInstances)
			{
				return this.Clone(entity);
			}
			TModType tmodType = (TModType)((object)Activator.CreateInstance(base.GetType(), true));
			tmodType.Mod = base.Mod;
			tmodType.Entity = entity;
			return tmodType;
		}

		// Token: 0x04001738 RID: 5944
		private bool? _isCloneable;
	}
}
