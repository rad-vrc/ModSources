using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Represents a style of waterfalls that gets drawn. This is mostly used to determine the color of the waterfall.
	/// </summary>
	// Token: 0x020001D6 RID: 470
	[Autoload(true, Side = ModSide.Client)]
	public abstract class ModWaterfallStyle : ModTexturedType
	{
		/// <summary>
		/// The ID of this waterfall style.
		/// </summary>
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x004EA40C File Offset: 0x004E860C
		// (set) Token: 0x060024C8 RID: 9416 RVA: 0x004EA414 File Offset: 0x004E8614
		public int Slot { get; internal set; }

		// Token: 0x060024C9 RID: 9417 RVA: 0x004EA41D File Offset: 0x004E861D
		protected sealed override void Register()
		{
			this.Slot = LoaderManager.Get<WaterFallStylesLoader>().Register(this);
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x004EA430 File Offset: 0x004E8630
		public sealed override void SetupContent()
		{
			Main.instance.waterfallManager.waterfallTexture[this.Slot] = ModContent.Request<Texture2D>(this.Texture, 2);
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to create light at a tile occupied by a waterfall of this style.
		/// </summary>
		// Token: 0x060024CB RID: 9419 RVA: 0x004EA45A File Offset: 0x004E865A
		public virtual void AddLight(int i, int j)
		{
		}

		/// <summary>
		/// Allows you to determine the color multiplier acting on waterfalls of this style. Useful for waterfalls whose colors change over time.
		/// </summary>
		// Token: 0x060024CC RID: 9420 RVA: 0x004EA45C File Offset: 0x004E865C
		public virtual void ColorMultiplier(ref float r, ref float g, ref float b, float a)
		{
		}
	}
}
