using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x020002CE RID: 718
	public class CaveHouseBiome : MicroBiome
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x00548245 File Offset: 0x00546445
		// (set) Token: 0x060022C2 RID: 8898 RVA: 0x0054824D File Offset: 0x0054644D
		[JsonProperty]
		public double IceChestChance { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00548256 File Offset: 0x00546456
		// (set) Token: 0x060022C4 RID: 8900 RVA: 0x0054825E File Offset: 0x0054645E
		[JsonProperty]
		public double JungleChestChance { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x00548267 File Offset: 0x00546467
		// (set) Token: 0x060022C6 RID: 8902 RVA: 0x0054826F File Offset: 0x0054646F
		[JsonProperty]
		public double GoldChestChance { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x00548278 File Offset: 0x00546478
		// (set) Token: 0x060022C8 RID: 8904 RVA: 0x00548280 File Offset: 0x00546480
		[JsonProperty]
		public double GraniteChestChance { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x00548289 File Offset: 0x00546489
		// (set) Token: 0x060022CA RID: 8906 RVA: 0x00548291 File Offset: 0x00546491
		[JsonProperty]
		public double MarbleChestChance { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x0054829A File Offset: 0x0054649A
		// (set) Token: 0x060022CC RID: 8908 RVA: 0x005482A2 File Offset: 0x005464A2
		[JsonProperty]
		public double MushroomChestChance { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x005482AB File Offset: 0x005464AB
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x005482B3 File Offset: 0x005464B3
		[JsonProperty]
		public double DesertChestChance { get; set; }

		// Token: 0x060022CF RID: 8911 RVA: 0x005482BC File Offset: 0x005464BC
		public override bool Place(Point origin, StructureMap structures)
		{
			if (!WorldGen.InWorld(origin.X, origin.Y, 10))
			{
				return false;
			}
			int num = 25;
			for (int i = origin.X - num; i <= origin.X + num; i++)
			{
				for (int j = origin.Y - num; j <= origin.Y + num; j++)
				{
					if (Main.tile[i, j].wire())
					{
						return false;
					}
					if (TileID.Sets.BasicChest[(int)Main.tile[i, j].type])
					{
						return false;
					}
				}
			}
			HouseBuilder houseBuilder = HouseUtils.CreateBuilder(origin, structures);
			if (!houseBuilder.IsValid)
			{
				return false;
			}
			this.ApplyConfigurationToBuilder(houseBuilder);
			houseBuilder.Place(this._builderContext, structures);
			return true;
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00548370 File Offset: 0x00546570
		private void ApplyConfigurationToBuilder(HouseBuilder builder)
		{
			switch (builder.Type)
			{
			case HouseType.Wood:
				builder.ChestChance = this.GoldChestChance;
				return;
			case HouseType.Ice:
				builder.ChestChance = this.IceChestChance;
				return;
			case HouseType.Desert:
				builder.ChestChance = this.DesertChestChance;
				return;
			case HouseType.Jungle:
				builder.ChestChance = this.JungleChestChance;
				return;
			case HouseType.Mushroom:
				builder.ChestChance = this.MushroomChestChance;
				return;
			case HouseType.Granite:
				builder.ChestChance = this.GraniteChestChance;
				return;
			case HouseType.Marble:
				builder.ChestChance = this.MarbleChestChance;
				return;
			default:
				return;
			}
		}

		// Token: 0x040047EE RID: 18414
		private readonly HouseBuilderContext _builderContext = new HouseBuilderContext();
	}
}
