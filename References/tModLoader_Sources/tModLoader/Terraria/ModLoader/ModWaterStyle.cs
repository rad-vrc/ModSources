using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.GameContent.Liquid;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Represents a style of water that gets drawn, based on factors such as the background. This is used to determine the color of the water, as well as other things as determined by the hooks below.
	/// </summary>
	// Token: 0x020001D5 RID: 469
	[Autoload(true, Side = ModSide.Client)]
	public abstract class ModWaterStyle : ModTexturedType
	{
		/// <summary>
		/// The ID of the water style.
		/// </summary>
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x004EA2E5 File Offset: 0x004E84E5
		// (set) Token: 0x060024BA RID: 9402 RVA: 0x004EA2ED File Offset: 0x004E84ED
		public int Slot { get; internal set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x004EA2F6 File Offset: 0x004E84F6
		public virtual string BlockTexture
		{
			get
			{
				return this.Texture + "_Block";
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x004EA308 File Offset: 0x004E8508
		public virtual string SlopeTexture
		{
			get
			{
				return this.Texture + "_Slope";
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x004EA31A File Offset: 0x004E851A
		protected sealed override void Register()
		{
			this.Slot = LoaderManager.Get<WaterStylesLoader>().Register(this);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x004EA330 File Offset: 0x004E8530
		public sealed override void SetupContent()
		{
			LiquidRenderer.Instance._liquidTextures[this.Slot] = ModContent.Request<Texture2D>(this.Texture, 2);
			this.SetStaticDefaults();
			TextureAssets.Liquid[this.Slot] = ModContent.Request<Texture2D>(this.BlockTexture, 2);
			if (base.Mod.TModLoaderVersion < new Version(2023, 6, 24))
			{
				TextureAssets.LiquidSlope[this.Slot] = ModContent.Request<Texture2D>(this.BlockTexture, 2);
				return;
			}
			TextureAssets.LiquidSlope[this.Slot] = ModContent.Request<Texture2D>(this.SlopeTexture, 2);
		}

		/// <summary>
		/// The ID of the waterfall style the game should use when this water style is in use.
		/// </summary>
		// Token: 0x060024BF RID: 9407
		public abstract int ChooseWaterfallStyle();

		/// <summary>
		/// The ID of the dust that is created when anything splashes in water.
		/// </summary>
		// Token: 0x060024C0 RID: 9408
		public abstract int GetSplashDust();

		/// <summary>
		/// The ID of the gore that represents droplets of water falling down from a block. Return <see cref="F:Terraria.ID.GoreID.WaterDrip" /> (or another existing droplet gore) or make a custom ModGore that uses <see cref="F:Terraria.ID.GoreID.Sets.LiquidDroplet" />.
		/// </summary>
		// Token: 0x060024C1 RID: 9409
		public abstract int GetDropletGore();

		/// <summary>
		/// Allows you to modify the light levels of the tiles behind the water. The light color components will be multiplied by the parameters.
		/// </summary>
		// Token: 0x060024C2 RID: 9410 RVA: 0x004EA3C8 File Offset: 0x004E85C8
		public virtual void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 0.88f;
			g = 0.96f;
			b = 1.015f;
		}

		/// <summary>
		/// Allows you to change the hair color resulting from the biome hair dye when this water style is in use.
		/// </summary>
		// Token: 0x060024C3 RID: 9411 RVA: 0x004EA3DF File Offset: 0x004E85DF
		public virtual Color BiomeHairColor()
		{
			return new Color(28, 216, 94);
		}

		/// <summary>
		/// Returns the texture to be used when drawing rain of this water type.
		/// <br />Default uses the vanilla rain texture.
		/// </summary>
		// Token: 0x060024C4 RID: 9412 RVA: 0x004EA3EF File Offset: 0x004E85EF
		public virtual Asset<Texture2D> GetRainTexture()
		{
			return TextureAssets.Rain;
		}

		/// <summary>
		/// Return the variant of rain used. Equal to the offset in the rain texture divided by four.
		/// <br />Vanilla rain has three variants per biome, and so vanilla variants range from 0 to 3 * Main.maxLiquidTextures.
		/// <br />Default is a random number from 0 to 2, which creates normal vanilla forest biome rain.
		/// </summary>
		// Token: 0x060024C5 RID: 9413 RVA: 0x004EA3F6 File Offset: 0x004E85F6
		public virtual byte GetRainVariant()
		{
			return (byte)Main.rand.Next(3);
		}
	}
}
