using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000651 RID: 1617
	public class CaveHouseBiome : MicroBiome
	{
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060046AC RID: 18092 RVA: 0x00632BE1 File Offset: 0x00630DE1
		// (set) Token: 0x060046AD RID: 18093 RVA: 0x00632BE9 File Offset: 0x00630DE9
		[JsonProperty]
		public double IceChestChance { get; set; }

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060046AE RID: 18094 RVA: 0x00632BF2 File Offset: 0x00630DF2
		// (set) Token: 0x060046AF RID: 18095 RVA: 0x00632BFA File Offset: 0x00630DFA
		[JsonProperty]
		public double JungleChestChance { get; set; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060046B0 RID: 18096 RVA: 0x00632C03 File Offset: 0x00630E03
		// (set) Token: 0x060046B1 RID: 18097 RVA: 0x00632C0B File Offset: 0x00630E0B
		[JsonProperty]
		public double GoldChestChance { get; set; }

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060046B2 RID: 18098 RVA: 0x00632C14 File Offset: 0x00630E14
		// (set) Token: 0x060046B3 RID: 18099 RVA: 0x00632C1C File Offset: 0x00630E1C
		[JsonProperty]
		public double GraniteChestChance { get; set; }

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060046B4 RID: 18100 RVA: 0x00632C25 File Offset: 0x00630E25
		// (set) Token: 0x060046B5 RID: 18101 RVA: 0x00632C2D File Offset: 0x00630E2D
		[JsonProperty]
		public double MarbleChestChance { get; set; }

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060046B6 RID: 18102 RVA: 0x00632C36 File Offset: 0x00630E36
		// (set) Token: 0x060046B7 RID: 18103 RVA: 0x00632C3E File Offset: 0x00630E3E
		[JsonProperty]
		public double MushroomChestChance { get; set; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060046B8 RID: 18104 RVA: 0x00632C47 File Offset: 0x00630E47
		// (set) Token: 0x060046B9 RID: 18105 RVA: 0x00632C4F File Offset: 0x00630E4F
		[JsonProperty]
		public double DesertChestChance { get; set; }

		// Token: 0x060046BA RID: 18106 RVA: 0x00632C58 File Offset: 0x00630E58
		public unsafe override bool Place(Point origin, StructureMap structures)
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
					if (TileID.Sets.BasicChest[(int)(*Main.tile[i, j].type)])
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

		// Token: 0x060046BB RID: 18107 RVA: 0x00632D14 File Offset: 0x00630F14
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

		// Token: 0x04005B90 RID: 23440
		private readonly HouseBuilderContext _builderContext = new HouseBuilderContext();
	}
}
