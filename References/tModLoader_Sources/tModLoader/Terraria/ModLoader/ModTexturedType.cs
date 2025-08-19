using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// The base type for most modded things with textures.
	/// </summary>
	// Token: 0x020001CD RID: 461
	public abstract class ModTexturedType : ModType
	{
		/// <summary>
		/// The file name of this type's texture file in the mod loader's file space.
		/// </summary>
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x004E9537 File Offset: 0x004E7737
		public virtual string Texture
		{
			get
			{
				return (base.GetType().Namespace + "." + this.Name).Replace('.', '/');
			}
		}
	}
}
