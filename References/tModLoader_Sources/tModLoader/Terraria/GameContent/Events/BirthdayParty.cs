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
	// Token: 0x0200062A RID: 1578
	public class BirthdayParty
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x0060E6F5 File Offset: 0x0060C8F5
		public static bool PartyIsUp
		{
			get
			{
				return BirthdayParty.GenuineParty || BirthdayParty.ManualParty;
			}
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x0060E705 File Offset: 0x0060C905
		public static void CheckMorning()
		{
			BirthdayParty.NaturalAttempt();
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x0060E70C File Offset: 0x0060C90C
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
				Color color;
				color..ctor(255, 0, 160);
				WorldGen.BroadcastText(NetworkText.FromKey(Lang.misc[99].Key, Array.Empty<object>()), color);
			}
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x0060E778 File Offset: 0x0060C978
		private static bool CanNPCParty(NPC n)
		{
			return n.active && n.townNPC && n.aiStyle != 0 && n.type != 37 && n.type != 453 && n.type != 441 && !NPCID.Sets.IsTownPet[n.type];
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x0060E7D4 File Offset: 0x0060C9D4
		private static void NaturalAttempt()
		{
			if (Main.netMode == 1 || !NPC.AnyNPCs(208))
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
				NPC nPC = Main.npc[l];
				if (BirthdayParty.CanNPCParty(nPC))
				{
					list.Add(nPC);
				}
			}
			if (list.Count >= 5)
			{
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
				Color color;
				color..ctor(255, 0, 160);
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
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x0060EA64 File Offset: 0x0060CC64
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

		// Token: 0x06004515 RID: 17685 RVA: 0x0060EADA File Offset: 0x0060CCDA
		private static void CheckForAchievement()
		{
			if (BirthdayParty.PartyIsUp)
			{
				AchievementsHelper.NotifyProgressionEvent(25);
			}
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x0060EAEA File Offset: 0x0060CCEA
		public static void WorldClear()
		{
			BirthdayParty.ManualParty = false;
			BirthdayParty.GenuineParty = false;
			BirthdayParty.PartyDaysOnCooldown = 0;
			BirthdayParty.CelebratingNPCs.Clear();
			BirthdayParty._wasCelebrating = false;
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x0060EB10 File Offset: 0x0060CD10
		public static void UpdateTime()
		{
			if (BirthdayParty._wasCelebrating != BirthdayParty.PartyIsUp)
			{
				if (Main.netMode != 2)
				{
					if (BirthdayParty.PartyIsUp)
					{
						SkyManager.Instance.Activate("Party", default(Vector2), Array.Empty<object>());
					}
					else
					{
						SkyManager.Instance.Deactivate("Party", Array.Empty<object>());
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
							Color color;
							color..ctor(255, 0, 160);
							WorldGen.BroadcastText(NetworkText.FromKey(Lang.misc[99].Key, Array.Empty<object>()), color);
							NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
			BirthdayParty._wasCelebrating = BirthdayParty.PartyIsUp;
		}

		// Token: 0x04005AC9 RID: 23241
		public static bool ManualParty;

		// Token: 0x04005ACA RID: 23242
		public static bool GenuineParty;

		// Token: 0x04005ACB RID: 23243
		public static int PartyDaysOnCooldown;

		// Token: 0x04005ACC RID: 23244
		public static List<int> CelebratingNPCs = new List<int>();

		// Token: 0x04005ACD RID: 23245
		private static bool _wasCelebrating;
	}
}
