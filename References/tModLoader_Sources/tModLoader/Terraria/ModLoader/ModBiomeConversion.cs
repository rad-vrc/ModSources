using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class allows you to define a modded conversion to work with <see cref="M:Terraria.ModLoader.ModBlockType.Convert(System.Int32,System.Int32,System.Int32)" /> and <see cref="M:Terraria.ModLoader.TileLoader.RegisterConversion(System.Int32,System.Int32,Terraria.ModLoader.TileLoader.ConvertTile)" /> / <see cref="M:Terraria.ModLoader.WallLoader.RegisterConversion(System.Int32,System.Int32,Terraria.ModLoader.WallLoader.ConvertWall)" /> <br />
	/// For the moment, this class does not provide any functional hooks, but it can still be used by mods in combination with the aforementionned hooks to create custom conversion behavior
	/// </summary>
	// Token: 0x020001A0 RID: 416
	public abstract class ModBiomeConversion : ModType
	{
		/// <summary>
		/// The ID of the biome conversion
		/// </summary>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x004E2A66 File Offset: 0x004E0C66
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x004E2A6E File Offset: 0x004E0C6E
		public int Type { get; internal set; }

		// Token: 0x06002003 RID: 8195 RVA: 0x004E2A77 File Offset: 0x004E0C77
		protected sealed override void Register()
		{
			ModTypeLookup<ModBiomeConversion>.Register(this);
			this.Type = BiomeConversionLoader.Register(this);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x004E2A8B File Offset: 0x004E0C8B
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to run code after the mod's content has been setup (ID sets have been filled, etc). <br />
		/// Use <see cref="M:Terraria.ModLoader.TileLoader.RegisterConversion(System.Int32,System.Int32,Terraria.ModLoader.TileLoader.ConvertTile)" /> and <see cref="M:Terraria.ModLoader.WallLoader.RegisterConversion(System.Int32,System.Int32,Terraria.ModLoader.WallLoader.ConvertWall)" /> in this hook if you want to implement conversions that rely on <see cref="T:Terraria.ID.TileID.Sets.Conversion" /> and <see cref="T:Terraria.ID.WallID.Sets.Conversion" /> sets being fully populated.
		/// </summary>
		// Token: 0x06002005 RID: 8197 RVA: 0x004E2A93 File Offset: 0x004E0C93
		public virtual void PostSetupContent()
		{
		}
	}
}
