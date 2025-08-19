using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Tiles.Dedicated
{
	// Token: 0x02000012 RID: 18
	public class BurgerTile : ModTile
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003508 File Offset: 0x00001708
		public override void SetStaticDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = false;
			Main.tileLighted[(int)base.Type] = false;
			base.DustType = 2;
			base.AddMapEntry(new Color(188, 145, 73), null);
		}
	}
}
