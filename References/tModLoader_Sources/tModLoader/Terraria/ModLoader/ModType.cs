using System;
using System.Text.RegularExpressions;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	/// <summary>
	/// The base type for most modded things.
	/// </summary>
	// Token: 0x020001D0 RID: 464
	public abstract class ModType : IModType, ILoadable
	{
		/// <summary>
		///  The mod this belongs to.
		///  </summary>
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x004E9DA3 File Offset: 0x004E7FA3
		// (set) Token: 0x0600248D RID: 9357 RVA: 0x004E9DAB File Offset: 0x004E7FAB
		public Mod Mod { get; internal set; }

		/// <summary>
		/// The internal name of this.
		/// </summary>
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x004E9DB4 File Offset: 0x004E7FB4
		public virtual string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		/// <summary>
		/// The internal name of this, including the mod it is from.
		/// </summary>
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x004E9DC1 File Offset: 0x004E7FC1
		public string FullName
		{
			get
			{
				Mod mod = this.Mod;
				return (((mod != null) ? mod.Name : null) ?? "Terraria") + "/" + this.Name;
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x004E9DEE File Offset: 0x004E7FEE
		void ILoadable.Load(Mod mod)
		{
			if (!LoaderUtils.IsValidated(base.GetType()))
			{
				this.ValidateType();
			}
			this.Mod = mod;
			this.InitTemplateInstance();
			this.Load();
			this.Register();
		}

		/// <summary>
		/// Allows you to perform one-time loading tasks. Beware that mod content has not finished loading here, things like ModContent lookup tables or ID Sets are not fully populated.
		/// <para>Use <see cref="M:Terraria.ModLoader.ModType.SetStaticDefaults" /> when you need to access content.</para>
		/// </summary>
		// Token: 0x06002491 RID: 9361 RVA: 0x004E9E1C File Offset: 0x004E801C
		public virtual void Load()
		{
		}

		/// <summary>
		/// Allows you to stop <see cref="M:Terraria.ModLoader.Mod.AddContent(Terraria.ModLoader.ILoadable)" /> from actually adding this content. Useful for items that can be disabled by a config.
		/// </summary>
		/// <param name="mod">The mod adding this content</param>
		// Token: 0x06002492 RID: 9362 RVA: 0x004E9E1E File Offset: 0x004E801E
		public virtual bool IsLoadingEnabled(Mod mod)
		{
			return true;
		}

		/// <summary>
		/// If you make a new ModType, seal this override.
		/// </summary>
		// Token: 0x06002493 RID: 9363
		protected abstract void Register();

		/// <summary>
		/// If you make a new ModType, seal this override, and call <see cref="M:Terraria.ModLoader.ModType.SetStaticDefaults" /> in it.
		/// </summary>
		// Token: 0x06002494 RID: 9364 RVA: 0x004E9E21 File Offset: 0x004E8021
		public virtual void SetupContent()
		{
		}

		/// <summary>
		/// Allows you to modify the properties after initial loading has completed.
		/// </summary>
		// Token: 0x06002495 RID: 9365 RVA: 0x004E9E23 File Offset: 0x004E8023
		public virtual void SetStaticDefaults()
		{
		}

		/// <summary>
		/// Allows you to safely unload things you added in <see cref="M:Terraria.ModLoader.ModType.Load" />.
		/// </summary>
		// Token: 0x06002496 RID: 9366 RVA: 0x004E9E25 File Offset: 0x004E8025
		public virtual void Unload()
		{
		}

		/// <summary>
		/// Create dummy objects for instanced mod-types
		/// </summary>
		// Token: 0x06002497 RID: 9367 RVA: 0x004E9E27 File Offset: 0x004E8027
		protected virtual void InitTemplateInstance()
		{
		}

		/// <summary>
		/// Check for the correct overrides of different hook methods and fields and properties
		/// </summary>
		// Token: 0x06002498 RID: 9368 RVA: 0x004E9E29 File Offset: 0x004E8029
		protected virtual void ValidateType()
		{
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x004E9E2B File Offset: 0x004E802B
		public string PrettyPrintName()
		{
			return Regex.Replace(this.Name, "([A-Z])", " $1").Trim();
		}
	}
}
