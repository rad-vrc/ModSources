using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	// Token: 0x02000173 RID: 371
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class GlobalType<[Nullable(0)] TGlobal> : ModType where TGlobal : GlobalType<TGlobal>
	{
		/// <summary>
		/// Index of this global in the list of all globals of the same type, in registration order
		/// </summary>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x004D45CB File Offset: 0x004D27CB
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x004D45D3 File Offset: 0x004D27D3
		public short StaticIndex { get; internal set; }

		/// <summary>
		/// Index of this global in a <see cref="P:Terraria.ModLoader.IEntityWithGlobals`1.EntityGlobals" /> array <br />
		/// -1 if this global does not have a <see cref="P:Terraria.ModLoader.GlobalType`1.SlotPerEntity" />
		/// </summary>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x004D45DC File Offset: 0x004D27DC
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x004D45E4 File Offset: 0x004D27E4
		public short PerEntityIndex { get; internal set; }

		/// <summary>
		/// If true, the global will be assigned a <see cref="P:Terraria.ModLoader.GlobalType`1.PerEntityIndex" /> at load time, which can be used to access the instance in the <see cref="P:Terraria.ModLoader.IEntityWithGlobals`1.EntityGlobals" /> array. <br />
		/// If false, the global will be a singleton applying to all entities
		/// </summary>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x004D45ED File Offset: 0x004D27ED
		public virtual bool SlotPerEntity
		{
			get
			{
				return this.InstancePerEntity;
			}
		}

		/// <summary>
		/// Whether to create a new instance of this Global for every entity that exists.
		/// Useful for storing information on an entity. Defaults to false.
		/// Return true if you need to store information (have non-static fields).
		/// </summary>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x004D45F5 File Offset: 0x004D27F5
		public virtual bool InstancePerEntity
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Whether this global applies to some entities but not others
		/// </summary>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001DC8 RID: 7624
		public abstract bool ConditionallyAppliesToEntities { get; }

		// Token: 0x06001DC9 RID: 7625 RVA: 0x004D45F8 File Offset: 0x004D27F8
		protected override void ValidateType()
		{
			base.ValidateType();
			if (base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any((FieldInfo f) => f.DeclaringType.IsSubclassOf(typeof(GlobalType<TGlobal>))) && !this.InstancePerEntity)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(91, 3);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(base.GetType().FullName);
				defaultInterpolatedStringHandler.AppendLiteral(" instance fields but ");
				defaultInterpolatedStringHandler.AppendFormatted("InstancePerEntity");
				defaultInterpolatedStringHandler.AppendLiteral(" returns false. Either use static fields, or override ");
				defaultInterpolatedStringHandler.AppendFormatted("InstancePerEntity");
				defaultInterpolatedStringHandler.AppendLiteral(" to return true");
				throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x004D46B8 File Offset: 0x004D28B8
		protected override void Register()
		{
			ModTypeLookup<TGlobal>.Register((TGlobal)((object)this));
			ValueTuple<short, short> valueTuple = GlobalList<TGlobal>.Register((TGlobal)((object)this));
			this.StaticIndex = valueTuple.Item1;
			this.PerEntityIndex = valueTuple.Item2;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x004D46F4 File Offset: 0x004D28F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TResult GetGlobal<[Nullable(0)] TResult>(int entityType, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<TGlobal> entityGlobals, TResult baseInstance) where TResult : TGlobal
		{
			TResult result;
			if (!GlobalType<TGlobal>.TryGetGlobal<TResult>(entityType, entityGlobals, baseInstance, out result))
			{
				throw new KeyNotFoundException(baseInstance.FullName);
			}
			return result;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x004D4720 File Offset: 0x004D2920
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TResult GetGlobal<[Nullable(0)] TResult>(int entityType, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<TGlobal> entityGlobals) where TResult : TGlobal
		{
			TResult result;
			if (!GlobalType<TGlobal>.TryGetGlobal<TResult>(entityType, entityGlobals, out result))
			{
				throw new KeyNotFoundException(typeof(TResult).FullName);
			}
			return result;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x004D4750 File Offset: 0x004D2950
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool TryGetGlobal<[Nullable(0)] TResult>(int entityType, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<TGlobal> entityGlobals, TResult baseInstance, out TResult result) where TResult : TGlobal
		{
			short slot = baseInstance.PerEntityIndex;
			if (entityType > 0 && slot >= 0)
			{
				result = (TResult)((object)(*entityGlobals[(int)slot]));
				return result != null;
			}
			if (GlobalTypeLookups<TGlobal>.AppliesToType((TGlobal)((object)baseInstance), entityType))
			{
				result = baseInstance;
				return true;
			}
			result = default(TResult);
			return false;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x004D47C1 File Offset: 0x004D29C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGlobal<[Nullable(0)] TResult>(int entityType, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<TGlobal> entityGlobals, out TResult result) where TResult : TGlobal
		{
			return GlobalType<TGlobal>.TryGetGlobal<TResult>(entityType, entityGlobals, ModContent.GetInstance<TResult>(), out result);
		}
	}
}
