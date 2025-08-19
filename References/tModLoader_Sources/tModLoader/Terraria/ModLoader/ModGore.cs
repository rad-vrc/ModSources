using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class allows you to customize the behavior of a custom gore.
	/// </summary>
	// Token: 0x020001B0 RID: 432
	[Autoload(true, Side = ModSide.Client)]
	public abstract class ModGore : ModTexturedType
	{
		/// <summary> Allows you to copy the Update behavior of a different type of gore. This defaults to 0, which means no behavior is copied. </summary>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x004E40A7 File Offset: 0x004E22A7
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x004E40AF File Offset: 0x004E22AF
		public int UpdateType { get; set; } = -1;

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x004E40B8 File Offset: 0x004E22B8
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x004E40C0 File Offset: 0x004E22C0
		public int Type { get; internal set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x004E40C9 File Offset: 0x004E22C9
		public override string Name
		{
			get
			{
				return this.nameOverride ?? base.Name;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x004E40DB File Offset: 0x004E22DB
		public override string Texture
		{
			get
			{
				return this.textureOverride ?? base.Texture;
			}
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x004E40ED File Offset: 0x004E22ED
		protected override void Register()
		{
			ModTypeLookup<ModGore>.Register(this);
			GoreLoader.RegisterModGore(this);
			GoreID.Search.Add(base.FullName, this.Type);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x004E4111 File Offset: 0x004E2311
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to modify a gore's fields when it is created.
		/// </summary>
		// Token: 0x060020D7 RID: 8407 RVA: 0x004E4119 File Offset: 0x004E2319
		public virtual void OnSpawn(Gore gore, IEntitySource source)
		{
		}

		/// <summary>
		/// Allows you to customize how you want this type of gore to behave. Return true to allow for vanilla gore updating to also take place; returns true by default.
		/// </summary>
		// Token: 0x060020D8 RID: 8408 RVA: 0x004E411B File Offset: 0x004E231B
		public virtual bool Update(Gore gore)
		{
			return true;
		}

		/// <summary>
		/// Allows you to override the color this gore will draw in. Return null to draw it in the normal light color; returns null by default.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060020D9 RID: 8409 RVA: 0x004E4120 File Offset: 0x004E2320
		public virtual Color? GetAlpha(Gore gore, Color lightColor)
		{
			return null;
		}

		// Token: 0x040016C1 RID: 5825
		internal string nameOverride;

		// Token: 0x040016C2 RID: 5826
		internal string textureOverride;
	}
}
