using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005B0 RID: 1456
	public class AllPersonalitiesModifier : IShopPersonalityTrait
	{
		// Token: 0x06004290 RID: 17040 RVA: 0x005F93C9 File Offset: 0x005F75C9
		public void ModifyShopPrice(HelperInfo info, ShopHelper shopHelperInstance)
		{
			NPCHappiness happiness = info.npc.Happiness;
			AllPersonalitiesModifier.ModifyShopPrice_Relationships(info, shopHelperInstance);
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x005F93E0 File Offset: 0x005F75E0
		internal static void RegisterVanillaNpcRelationships(NPC npc)
		{
			NPCHappiness npcHappiness = npc.Happiness;
			AllPersonalitiesModifier.RegisterNpcToNpcRelationships(new HelperInfo
			{
				npc = npc
			}, npcHappiness);
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x005F940C File Offset: 0x005F760C
		private static void ModifyShopPrice_Relationships(HelperInfo info, ShopHelper shopHelperInstance)
		{
			bool[] nearbyNPCsByType = info.nearbyNPCsByType;
			if (info.npc.type == 663)
			{
				List<int> list = new List<int>();
				for (int i = 0; i < nearbyNPCsByType.Length; i++)
				{
					if (nearbyNPCsByType[i])
					{
						list.Add(i);
					}
				}
				int j = 0;
				while (j < 3 && list.Count > 0)
				{
					int index = Main.rand.Next(list.Count);
					int npcType = list[index];
					list.RemoveAt(index);
					shopHelperInstance.LoveNPCByTypeName(npcType);
					j++;
				}
			}
			if (info.npc.type != 663 && nearbyNPCsByType[663])
			{
				shopHelperInstance.LikePrincess();
			}
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x005F94B4 File Offset: 0x005F76B4
		private static void RegisterNpcToNpcRelationships(HelperInfo info, NPCHappiness npcHappiness)
		{
			AllPersonalitiesModifier.NearbyNpcsDummy nearbyNPCsByType = default(AllPersonalitiesModifier.NearbyNpcsDummy);
			AllPersonalitiesModifier.ShopHelperDummy shopHelperInstance = new AllPersonalitiesModifier.ShopHelperDummy(npcHappiness);
			int type = info.npc.type;
			if (type <= 160)
			{
				if (type <= 107)
				{
					if (type <= 38)
					{
						switch (type)
						{
						case 17:
							if (nearbyNPCsByType[588])
							{
								shopHelperInstance.LikeNPC(588);
							}
							if (nearbyNPCsByType[18])
							{
								shopHelperInstance.LikeNPC(18);
							}
							if (nearbyNPCsByType[441])
							{
								shopHelperInstance.DislikeNPC(441);
							}
							if (nearbyNPCsByType[369])
							{
								shopHelperInstance.HateNPC(369);
								return;
							}
							break;
						case 18:
							if (nearbyNPCsByType[19])
							{
								shopHelperInstance.LoveNPC(19);
							}
							if (nearbyNPCsByType[108])
							{
								shopHelperInstance.LikeNPC(108);
							}
							if (nearbyNPCsByType[208])
							{
								shopHelperInstance.DislikeNPC(208);
							}
							if (nearbyNPCsByType[20])
							{
								shopHelperInstance.DislikeNPC(20);
							}
							if (nearbyNPCsByType[633])
							{
								shopHelperInstance.HateNPC(633);
								return;
							}
							break;
						case 19:
							if (nearbyNPCsByType[18])
							{
								shopHelperInstance.LoveNPC(18);
							}
							if (nearbyNPCsByType[178])
							{
								shopHelperInstance.LikeNPC(178);
							}
							if (nearbyNPCsByType[588])
							{
								shopHelperInstance.DislikeNPC(588);
							}
							if (nearbyNPCsByType[38])
							{
								shopHelperInstance.HateNPC(38);
								return;
							}
							break;
						case 20:
							if (nearbyNPCsByType[228])
							{
								shopHelperInstance.LikeNPC(228);
							}
							if (nearbyNPCsByType[160])
							{
								shopHelperInstance.LikeNPC(160);
							}
							if (nearbyNPCsByType[369])
							{
								shopHelperInstance.DislikeNPC(369);
							}
							if (nearbyNPCsByType[588])
							{
								shopHelperInstance.HateNPC(588);
								return;
							}
							break;
						case 21:
							break;
						case 22:
							if (nearbyNPCsByType[54])
							{
								shopHelperInstance.LikeNPC(54);
							}
							if (nearbyNPCsByType[178])
							{
								shopHelperInstance.DislikeNPC(178);
							}
							if (nearbyNPCsByType[227])
							{
								shopHelperInstance.HateNPC(227);
							}
							if (nearbyNPCsByType[633])
							{
								shopHelperInstance.LikeNPC(633);
								return;
							}
							break;
						default:
							if (type != 38)
							{
								return;
							}
							if (nearbyNPCsByType[550])
							{
								shopHelperInstance.LoveNPC(550);
							}
							if (nearbyNPCsByType[124])
							{
								shopHelperInstance.LikeNPC(124);
							}
							if (nearbyNPCsByType[107])
							{
								shopHelperInstance.DislikeNPC(107);
							}
							if (nearbyNPCsByType[19])
							{
								shopHelperInstance.DislikeNPC(19);
								return;
							}
							break;
						}
					}
					else if (type != 54)
					{
						if (type != 107)
						{
							return;
						}
						if (nearbyNPCsByType[124])
						{
							shopHelperInstance.LoveNPC(124);
						}
						if (nearbyNPCsByType[207])
						{
							shopHelperInstance.LikeNPC(207);
						}
						if (nearbyNPCsByType[54])
						{
							shopHelperInstance.DislikeNPC(54);
						}
						if (nearbyNPCsByType[353])
						{
							shopHelperInstance.HateNPC(353);
							return;
						}
					}
					else
					{
						if (nearbyNPCsByType[160])
						{
							shopHelperInstance.LoveNPC(160);
						}
						if (nearbyNPCsByType[441])
						{
							shopHelperInstance.LikeNPC(441);
						}
						if (nearbyNPCsByType[18])
						{
							shopHelperInstance.DislikeNPC(18);
						}
						if (nearbyNPCsByType[124])
						{
							shopHelperInstance.HateNPC(124);
							return;
						}
					}
				}
				else if (type <= 124)
				{
					if (type != 108)
					{
						if (type != 124)
						{
							return;
						}
						if (nearbyNPCsByType[107])
						{
							shopHelperInstance.LoveNPC(107);
						}
						if (nearbyNPCsByType[209])
						{
							shopHelperInstance.LikeNPC(209);
						}
						if (nearbyNPCsByType[19])
						{
							shopHelperInstance.DislikeNPC(19);
						}
						if (nearbyNPCsByType[54])
						{
							shopHelperInstance.HateNPC(54);
							return;
						}
					}
					else
					{
						if (nearbyNPCsByType[588])
						{
							shopHelperInstance.LoveNPC(588);
						}
						if (nearbyNPCsByType[17])
						{
							shopHelperInstance.LikeNPC(17);
						}
						if (nearbyNPCsByType[228])
						{
							shopHelperInstance.DislikeNPC(228);
						}
						if (nearbyNPCsByType[209])
						{
							shopHelperInstance.HateNPC(209);
							return;
						}
					}
				}
				else if (type != 142)
				{
					if (type != 160)
					{
						return;
					}
					if (nearbyNPCsByType[22])
					{
						shopHelperInstance.LoveNPC(22);
					}
					if (nearbyNPCsByType[20])
					{
						shopHelperInstance.LikeNPC(20);
					}
					if (nearbyNPCsByType[54])
					{
						shopHelperInstance.DislikeNPC(54);
					}
					if (nearbyNPCsByType[228])
					{
						shopHelperInstance.HateNPC(228);
						return;
					}
				}
				else if (nearbyNPCsByType[441])
				{
					shopHelperInstance.HateNPC(441);
					return;
				}
			}
			else if (type <= 353)
			{
				if (type <= 209)
				{
					if (type != 178)
					{
						switch (type)
						{
						case 207:
							if (nearbyNPCsByType[19])
							{
								shopHelperInstance.LikeNPC(19);
							}
							if (nearbyNPCsByType[227])
							{
								shopHelperInstance.LikeNPC(227);
							}
							if (nearbyNPCsByType[178])
							{
								shopHelperInstance.DislikeNPC(178);
							}
							if (nearbyNPCsByType[229])
							{
								shopHelperInstance.HateNPC(229);
								return;
							}
							break;
						case 208:
							if (nearbyNPCsByType[108])
							{
								shopHelperInstance.LoveNPC(108);
							}
							if (nearbyNPCsByType[353])
							{
								shopHelperInstance.LikeNPC(353);
							}
							if (nearbyNPCsByType[17])
							{
								shopHelperInstance.DislikeNPC(17);
							}
							if (nearbyNPCsByType[441])
							{
								shopHelperInstance.HateNPC(441);
							}
							if (nearbyNPCsByType[633])
							{
								shopHelperInstance.LoveNPC(633);
								return;
							}
							break;
						case 209:
							if (nearbyNPCsByType[353])
							{
								shopHelperInstance.LikeNPC(353);
							}
							if (nearbyNPCsByType[229])
							{
								shopHelperInstance.LikeNPC(229);
							}
							if (nearbyNPCsByType[178])
							{
								shopHelperInstance.LikeNPC(178);
							}
							if (nearbyNPCsByType[108])
							{
								shopHelperInstance.HateNPC(108);
							}
							if (nearbyNPCsByType[633])
							{
								shopHelperInstance.DislikeNPC(633);
								return;
							}
							break;
						default:
							return;
						}
					}
					else
					{
						if (nearbyNPCsByType[209])
						{
							shopHelperInstance.LoveNPC(209);
						}
						if (nearbyNPCsByType[227])
						{
							shopHelperInstance.LikeNPC(227);
						}
						if (nearbyNPCsByType[208])
						{
							shopHelperInstance.DislikeNPC(208);
						}
						if (nearbyNPCsByType[108])
						{
							shopHelperInstance.DislikeNPC(108);
						}
						if (nearbyNPCsByType[20])
						{
							shopHelperInstance.DislikeNPC(20);
							return;
						}
					}
				}
				else
				{
					switch (type)
					{
					case 227:
						if (nearbyNPCsByType[20])
						{
							shopHelperInstance.LoveNPC(20);
						}
						if (nearbyNPCsByType[208])
						{
							shopHelperInstance.LikeNPC(208);
						}
						if (nearbyNPCsByType[209])
						{
							shopHelperInstance.DislikeNPC(209);
						}
						if (nearbyNPCsByType[160])
						{
							shopHelperInstance.DislikeNPC(160);
							return;
						}
						break;
					case 228:
						if (nearbyNPCsByType[20])
						{
							shopHelperInstance.LikeNPC(20);
						}
						if (nearbyNPCsByType[22])
						{
							shopHelperInstance.LikeNPC(22);
						}
						if (nearbyNPCsByType[18])
						{
							shopHelperInstance.DislikeNPC(18);
						}
						if (nearbyNPCsByType[160])
						{
							shopHelperInstance.HateNPC(160);
							return;
						}
						break;
					case 229:
						if (nearbyNPCsByType[369])
						{
							shopHelperInstance.LoveNPC(369);
						}
						if (nearbyNPCsByType[550])
						{
							shopHelperInstance.LikeNPC(550);
						}
						if (nearbyNPCsByType[353])
						{
							shopHelperInstance.DislikeNPC(353);
						}
						if (nearbyNPCsByType[22])
						{
							shopHelperInstance.HateNPC(22);
							return;
						}
						break;
					default:
						if (type != 353)
						{
							return;
						}
						if (nearbyNPCsByType[207])
						{
							shopHelperInstance.LoveNPC(207);
						}
						if (nearbyNPCsByType[229])
						{
							shopHelperInstance.LikeNPC(229);
						}
						if (nearbyNPCsByType[550])
						{
							shopHelperInstance.DislikeNPC(550);
						}
						if (nearbyNPCsByType[107])
						{
							shopHelperInstance.HateNPC(107);
							return;
						}
						break;
					}
				}
			}
			else if (type <= 441)
			{
				if (type != 369)
				{
					if (type != 441)
					{
						return;
					}
					if (nearbyNPCsByType[17])
					{
						shopHelperInstance.LoveNPC(17);
					}
					if (nearbyNPCsByType[208])
					{
						shopHelperInstance.LikeNPC(208);
					}
					if (nearbyNPCsByType[38])
					{
						shopHelperInstance.DislikeNPC(38);
					}
					if (nearbyNPCsByType[124])
					{
						shopHelperInstance.DislikeNPC(124);
					}
					if (nearbyNPCsByType[142])
					{
						shopHelperInstance.HateNPC(142);
						return;
					}
				}
				else
				{
					if (nearbyNPCsByType[208])
					{
						shopHelperInstance.LikeNPC(208);
					}
					if (nearbyNPCsByType[38])
					{
						shopHelperInstance.LikeNPC(38);
					}
					if (nearbyNPCsByType[441])
					{
						shopHelperInstance.LikeNPC(441);
					}
					if (nearbyNPCsByType[550])
					{
						shopHelperInstance.HateNPC(550);
						return;
					}
				}
			}
			else if (type != 550)
			{
				if (type != 588)
				{
					if (type != 633)
					{
						return;
					}
					if (nearbyNPCsByType[369])
					{
						shopHelperInstance.DislikeNPC(369);
					}
					if (nearbyNPCsByType[19])
					{
						shopHelperInstance.HateNPC(19);
					}
					if (nearbyNPCsByType[228])
					{
						shopHelperInstance.LoveNPC(228);
					}
					if (nearbyNPCsByType[588])
					{
						shopHelperInstance.LikeNPC(588);
					}
				}
				else
				{
					if (nearbyNPCsByType[227])
					{
						shopHelperInstance.LikeNPC(227);
					}
					if (nearbyNPCsByType[369])
					{
						shopHelperInstance.LoveNPC(369);
					}
					if (nearbyNPCsByType[17])
					{
						shopHelperInstance.HateNPC(17);
					}
					if (nearbyNPCsByType[229])
					{
						shopHelperInstance.DislikeNPC(229);
					}
					if (nearbyNPCsByType[633])
					{
						shopHelperInstance.LikeNPC(633);
						return;
					}
				}
			}
			else
			{
				if (nearbyNPCsByType[38])
				{
					shopHelperInstance.LoveNPC(38);
				}
				if (nearbyNPCsByType[107])
				{
					shopHelperInstance.LikeNPC(107);
				}
				if (nearbyNPCsByType[22])
				{
					shopHelperInstance.DislikeNPC(22);
				}
				if (nearbyNPCsByType[207])
				{
					shopHelperInstance.HateNPC(207);
					return;
				}
			}
		}

		// Token: 0x02000C63 RID: 3171
		private struct NearbyNpcsDummy
		{
			// Token: 0x17000965 RID: 2405
			public bool this[int index]
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x02000C64 RID: 3172
		private readonly struct ShopHelperDummy
		{
			// Token: 0x06005FE4 RID: 24548 RVA: 0x006D19E6 File Offset: 0x006CFBE6
			public ShopHelperDummy(NPCHappiness happiness)
			{
				this.Happiness = happiness;
			}

			// Token: 0x06005FE5 RID: 24549 RVA: 0x006D19EF File Offset: 0x006CFBEF
			public void LoveNPC(int npcId)
			{
				this.Happiness.SetNPCAffection(npcId, AffectionLevel.Love);
			}

			// Token: 0x06005FE6 RID: 24550 RVA: 0x006D1A00 File Offset: 0x006CFC00
			public void LikeNPC(int npcId)
			{
				this.Happiness.SetNPCAffection(npcId, AffectionLevel.Like);
			}

			// Token: 0x06005FE7 RID: 24551 RVA: 0x006D1A11 File Offset: 0x006CFC11
			public void DislikeNPC(int npcId)
			{
				this.Happiness.SetNPCAffection(npcId, AffectionLevel.Dislike);
			}

			// Token: 0x06005FE8 RID: 24552 RVA: 0x006D1A22 File Offset: 0x006CFC22
			public void HateNPC(int npcId)
			{
				this.Happiness.SetNPCAffection(npcId, AffectionLevel.Hate);
			}

			// Token: 0x04007981 RID: 31105
			public readonly NPCHappiness Happiness;
		}
	}
}
