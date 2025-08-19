using System;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader.Default;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000299 RID: 665
	internal class TileEntry : ModBlockEntry
	{
		// Token: 0x06002C87 RID: 11399 RVA: 0x00527AF5 File Offset: 0x00525CF5
		public TileEntry(ModTile tile) : base(tile)
		{
			this.frameImportant = Main.tileFrameImportant[(int)tile.Type];
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x00527B10 File Offset: 0x00525D10
		public TileEntry(TagCompound tag) : base(tag)
		{
			this.frameImportant = tag.GetBool("framed");
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x00527B2A File Offset: 0x00525D2A
		public override ModBlockType DefaultUnloadedPlaceholder
		{
			get
			{
				return ModContent.GetInstance<UnloadedSolidTile>();
			}
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x00527B31 File Offset: 0x00525D31
		public override TagCompound SerializeData()
		{
			TagCompound tagCompound = base.SerializeData();
			tagCompound["framed"] = this.frameImportant;
			return tagCompound;
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x00527B50 File Offset: 0x00525D50
		protected override ModBlockType GetUnloadedPlaceholder(ushort type)
		{
			if (TileID.Sets.BasicChest[(int)type])
			{
				return ModContent.GetInstance<UnloadedChest>();
			}
			if (TileID.Sets.BasicDresser[(int)type])
			{
				return ModContent.GetInstance<UnloadedDresser>();
			}
			if (TileID.Sets.RoomNeeds.CountsAsChair.Contains((int)type) || TileID.Sets.RoomNeeds.CountsAsDoor.Contains((int)type) || TileID.Sets.RoomNeeds.CountsAsTable.Contains((int)type) || TileID.Sets.RoomNeeds.CountsAsTorch.Contains((int)type))
			{
				return ModContent.GetInstance<UnloadedSupremeFurniture>();
			}
			if (Main.tileSolidTop[(int)type])
			{
				return ModContent.GetInstance<UnloadedSemiSolidTile>();
			}
			if (!Main.tileSolid[(int)type])
			{
				return ModContent.GetInstance<UnloadedNonSolidTile>();
			}
			return this.DefaultUnloadedPlaceholder;
		}

		// Token: 0x04001C06 RID: 7174
		public static Func<TagCompound, TileEntry> DESERIALIZER = (TagCompound tag) => new TileEntry(tag);

		// Token: 0x04001C07 RID: 7175
		public bool frameImportant;
	}
}
