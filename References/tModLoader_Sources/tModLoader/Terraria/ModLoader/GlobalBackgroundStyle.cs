using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves to collect functions that operate on any kind of background style, without being specific to one single background style.
	/// </summary>
	// Token: 0x0200019D RID: 413
	public abstract class GlobalBackgroundStyle : ModType
	{
		// Token: 0x06001FDD RID: 8157 RVA: 0x004E26D6 File Offset: 0x004E08D6
		protected sealed override void Register()
		{
			ModTypeLookup<GlobalBackgroundStyle>.Register(this);
			GlobalBackgroundStyleLoader.globalBackgroundStyles.Add(this);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x004E26E9 File Offset: 0x004E08E9
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to change which underground background style is being used.
		/// </summary>
		// Token: 0x06001FDF RID: 8159 RVA: 0x004E26F1 File Offset: 0x004E08F1
		public virtual void ChooseUndergroundBackgroundStyle(ref int style)
		{
		}

		/// <summary>
		/// Allows you to change which surface background style is being used.
		/// </summary>
		// Token: 0x06001FE0 RID: 8160 RVA: 0x004E26F3 File Offset: 0x004E08F3
		public virtual void ChooseSurfaceBackgroundStyle(ref int style)
		{
		}

		/// <summary>
		/// Allows you to change which textures make up the underground background by assigning their background slots/IDs to the given array. Index 0 is the texture on the border of the ground and sky layers. Index 1 is the texture drawn between rock and ground layers. Index 2 is the texture on the border of ground and rock layers. Index 3 is the texture drawn in the rock layer. The border images are 160x16 pixels, and the others are 160x96, but it seems like the right 32 pixels of each is a duplicate of the far left 32 pixels.
		/// </summary>
		// Token: 0x06001FE1 RID: 8161 RVA: 0x004E26F5 File Offset: 0x004E08F5
		public virtual void FillUndergroundTextureArray(int style, int[] textureSlots)
		{
		}

		/// <summary>
		/// Allows you to modify the transparency of all background styles that exist. The style parameter is the current style that is being used.
		/// </summary>
		// Token: 0x06001FE2 RID: 8162 RVA: 0x004E26F7 File Offset: 0x004E08F7
		public virtual void ModifyFarSurfaceFades(int style, float[] fades, float transitionSpeed)
		{
		}
	}
}
