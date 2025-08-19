using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Events
{
	// Token: 0x020002A8 RID: 680
	public class BirthdayParty
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x00520691 File Offset: 0x0051E891
		public static bool PartyIsUp
		{
			get
			{
				return BirthdayParty.GenuineParty || BirthdayParty.ManualParty;
			}
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x005206A1 File Offset: 0x0051E8A1
		public static void CheckMorning()
		{
			BirthdayParty.NaturalAttempt();
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x005206A8 File Offset: 0x0051E8A8
		public static void CheckNight()
		{
			bool flag = false;
			if (BirthdayParty.GenuineParty)
			{
				flag = true;
				BirthdayParty.GenuineParty = false;
				BirthdayParty.CelebratingNPCs.Clear();
			}
			if (BirthdayParty.ManualParty)
			{
				flag = true;
				BirthdayParty.ManualParty = false;
			}
			if (flag)
			{
				Color color = new Color(255, 0, 160);
				WorldGen.BroadcastText(NetworkText.FromKey(Lang.misc[99].Key, new object[0]), color);
			}
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x00520714 File Offset: 0x0051E914
		private static bool CanNPCParty(NPC n)
		{
			return n.active && n.townNPC && n.aiStyle != 0 && n.type != 37 && n.type != 453 && n.type != 441 && !NPCID.Sets.IsTownPet[n.type];
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x00520770 File Offset: 0x0051E970
		private static void NaturalAttempt()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			if (!NPC.AnyNPCs(208))
			{
				return;
			}
			if (BirthdayParty.PartyDaysOnCooldown > 0)
			{
				BirthdayParty.PartyDaysOnCooldown--;
				return;
			}
			int maxValue = 10;
			if (Main.tenthAnniversaryWorld)
			{
				maxValue = 7;
			}
			if (Main.rand.Next(maxValue) != 0)
			{
				return;
			}
			List<NPC> list = new List<NPC>();
			for (int l = 0; l < 200; l++)
			{
				NPC npc = Main.npc[l];
				if (BirthdayParty.CanNPCParty(npc))
				{
					list.Add(npc);
				}
			}
			if (list.Count < 5)
			{
				return;
			}
			BirthdayParty.GenuineParty = true;
			BirthdayParty.PartyDaysOnCooldown = Main.rand.Next(5, 11);
			NPC.freeCake = true;
			BirthdayParty.CelebratingNPCs.Clear();
			List<int> list2 = new List<int>();
			int num = 1;
			if (Main.rand.Next(5) == 0 && list.Count > 12)
			{
				num = 3;
			}
			else if (Main.rand.Next(3) == 0)
			{
				num = 2;
			}
			list = (from i in list
			orderby Main.rand.Next()
			select i).ToList<NPC>();
			for (int j = 0; j < num; j++)
			{
				list2.Add(j);
			}
			for (int k = 0; k < list2.Count; k++)
			{
				BirthdayParty.CelebratingNPCs.Add(list[list2[k]].whoAmI);
			}
			Color color = new Color(255, 0, 160);
			if (BirthdayParty.CelebratingNPCs.Count == 3)
			{
				WorldGen.BroadcastText(NetworkText.FromKey("Game.BirthdayParty_3", new object[]
				{
					Main.npc[BirthdayParty.CelebratingNPCs[0]].GetGivenOrTypeNetName(),
					Main.npc[BirthdayParty.CelebratingNPCs[1]].GetGivenOrTypeNetName(),
					Main.npc[BirthdayParty.CelebratingNPCs[2]].GetGivenOrTypeNetName()
				}), color);
			}
			else if (BirthdayParty.CelebratingNPCs.Count == 2)
			{
				WorldGen.BroadcastText(NetworkText.FromKey("Game.BirthdayParty_2", new object[]
				{
					Main.npc[BirthdayParty.CelebratingNPCs[0]].GetGivenOrTypeNetName(),
					Main.npc[BirthdayParty.CelebratingNPCs[1]].GetGivenOrTypeNetName()
				}), color);
			}
			else
			{
				WorldGen.BroadcastText(NetworkText.FromKey("Game.BirthdayParty_1", new object[]
				{
					Main.npc[BirthdayParty.CelebratingNPCs[0]].GetGivenOrTypeNetName()
				}), color);
			}
			NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			BirthdayParty.CheckForAchievement();
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x00520A04 File Offset: 0x0051EC04
		public static void ToggleManualParty()
		{
			bool partyIsUp = BirthdayParty.PartyIsUp;
			if (Main.netMode != 1)
			{
				BirthdayParty.ManualParty = !BirthdayParty.ManualParty;
			}
			else
			{
				NetMessage.SendData(111, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (partyIsUp != BirthdayParty.PartyIsUp)
			{
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				BirthdayParty.CheckForAchievement();
			}
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00520A7A File Offset: 0x0051EC7A
		private static void CheckForAchievement()
		{
			if (BirthdayParty.PartyIsUp)
			{
				AchievementsHelper.NotifyProgressionEvent(25);
			}
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x00520A8A File Offset: 0x0051EC8A
		public static void WorldClear()
		{
			BirthdayParty.ManualParty = false;
			BirthdayParty.GenuineParty = false;
			BirthdayParty.PartyDaysOnCooldown = 0;
			BirthdayParty.CelebratingNPCs.Clear();
			BirthdayParty._wasCelebrating = false;
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x00520AB0 File Offset: 0x0051ECB0
		public static void UpdateTime()
		{
			if (BirthdayParty._wasCelebrating != BirthdayParty.PartyIsUp)
			{
				if (Main.netMode != 2)
				{
					if (BirthdayParty.PartyIsUp)
					{
						SkyManager.Instance.Activate("Party", default(Vector2), new object[0]);
					}
					else
					{
						SkyManager.Instance.Deactivate("Party", new object[0]);
					}
				}
				if (Main.netMode != 1 && BirthdayParty.CelebratingNPCs.Count > 0)
				{
					for (int i = 0; i < BirthdayParty.CelebratingNPCs.Count; i++)
					{
						if (!BirthdayParty.CanNPCParty(Main.npc[BirthdayParty.CelebratingNPCs[i]]))
						{
							BirthdayParty.CelebratingNPCs.RemoveAt(i);
						}
					}
					if (BirthdayParty.CelebratingNPCs.Count == 0)
					{
						BirthdayParty.GenuineParty = false;
						if (!BirthdayParty.ManualParty)
						{
							Color color = new Color(255, 0, 160);
							WorldGen.BroadcastText(NetworkText.FromKey(Lang.misc[99].Key, new object[0]), color);
							NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
			BirthdayParty._wasCelebrating = BirthdayParty.PartyIsUp;
		}

		// Token: 0x0400471E RID: 18206
		public static bool ManualParty;

		// Token: 0x0400471F RID: 18207
		public static bool GenuineParty;

		// Token: 0x04004720 RID: 18208
		public static int PartyDaysOnCooldown;

		// Token: 0x04004721 RID: 18209
		public static List<int> CelebratingNPCs = new List<int>();

		// Token: 0x04004722 RID: 18210
		private static bool _wasCelebrating;
	}
}
