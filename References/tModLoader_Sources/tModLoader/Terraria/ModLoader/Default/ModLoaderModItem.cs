using System;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002C8 RID: 712
	public abstract class ModLoaderModItem : ModItem
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x0052F8FB File Offset: 0x0052DAFB
		public override string Texture
		{
			get
			{
				return "ModLoader/" + base.Texture.Substring("Terraria.ModLoader.Default.".Length).Replace('/', '.');
			}
		}
	}
}
