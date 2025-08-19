using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Each background style determines in its own way how exactly the background is drawn. This class serves as a collection of functions for above-ground backgrounds.
	/// </summary>
	// Token: 0x0200019C RID: 412
	public abstract class ModSurfaceBackgroundStyle : ModBackgroundStyle
	{
		// Token: 0x06001FD5 RID: 8149 RVA: 0x004E26A7 File Offset: 0x004E08A7
		protected sealed override void Register()
		{
			base.Slot = LoaderManager.Get<SurfaceBackgroundStylesLoader>().Register(this);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x004E26BA File Offset: 0x004E08BA
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to modify the transparency of all background styles that exist. In general, you should move the index equal to this style's slot closer to 1, and all other indexes closer to 0. The transitionSpeed parameter is what you should add/subtract to each element of the fades parameter. See the ExampleMod for an example.
		/// </summary>
		// Token: 0x06001FD7 RID: 8151
		public abstract void ModifyFarFades(float[] fades, float transitionSpeed);

		/// <summary>
		/// Allows you to determine which texture is drawn in the very back of the background. BackgroundTextureLoader.GetBackgroundSlot may be useful here, as well as for the other texture-choosing hooks.
		/// </summary>
		// Token: 0x06001FD8 RID: 8152 RVA: 0x004E26C2 File Offset: 0x004E08C2
		public virtual int ChooseFarTexture()
		{
			return -1;
		}

		/// <summary>
		/// Allows you to determine which texture is drawn in the middle of the background.
		/// </summary>
		// Token: 0x06001FD9 RID: 8153 RVA: 0x004E26C5 File Offset: 0x004E08C5
		public virtual int ChooseMiddleTexture()
		{
			return -1;
		}

		/// <summary>
		/// Gives you complete freedom over how the closest part of the background is drawn. Return true for ChooseCloseTexture to have an effect; return false to disable tModLoader's own code for drawing the close background.
		/// </summary>
		// Token: 0x06001FDA RID: 8154 RVA: 0x004E26C8 File Offset: 0x004E08C8
		public virtual bool PreDrawCloseBackground(SpriteBatch spriteBatch)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine which texture is drawn in the closest part of the background. This also lets you modify the scale and parallax (as well as two unfortunately-unknown parameters).
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="parallax">The parallax value.</param>
		/// <param name="a">a?</param>
		/// <param name="b">b?</param>
		/// <returns></returns>
		// Token: 0x06001FDB RID: 8155 RVA: 0x004E26CB File Offset: 0x004E08CB
		public virtual int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
		{
			return -1;
		}
	}
}
