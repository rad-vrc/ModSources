using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x020001C6 RID: 454
	public abstract class ModRarity : ModType
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x004E8F2C File Offset: 0x004E712C
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x004E8F34 File Offset: 0x004E7134
		public int Type { get; internal set; }

		// Token: 0x060023AF RID: 9135 RVA: 0x004E8F3D File Offset: 0x004E713D
		protected sealed override void Register()
		{
			ModTypeLookup<ModRarity>.Register(this);
			this.Type = RarityLoader.Add(this);
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x004E8F51 File Offset: 0x004E7151
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Your ModRarity's color.
		/// Returns White by default.
		/// </summary>
		/// <returns></returns>
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x004E8F59 File Offset: 0x004E7159
		public virtual Color RarityColor
		{
			get
			{
				return Color.White;
			}
		}

		/// <summary>
		/// Allows you to modify which rarities will come before and after this when a modifier is applied (since modifiers can affect rarity)
		/// </summary>
		/// <param name="offset">The amount by which the rarity would be offset in vanilla. -2 is the most it can go down, and +2 is the most it can go up by.</param>
		/// <param name="valueMult">The combined stat and prefix value scale. Can be used to implement super high or low value rarity adjustments outside normal vanilla ranges</param>
		/// <returns>The adjusted rarity type. Return <code>Type</code> for no change.</returns>
		// Token: 0x060023B2 RID: 9138 RVA: 0x004E8F60 File Offset: 0x004E7160
		public virtual int GetPrefixedRarity(int offset, float valueMult)
		{
			return this.Type;
		}
	}
}
