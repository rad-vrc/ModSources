using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002F4 RID: 756
	public class BestiaryEntry
	{
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x005574E0 File Offset: 0x005556E0
		// (set) Token: 0x060023A7 RID: 9127 RVA: 0x005574E8 File Offset: 0x005556E8
		public List<IBestiaryInfoElement> Info { get; private set; }

		// Token: 0x060023A8 RID: 9128 RVA: 0x005574F1 File Offset: 0x005556F1
		public BestiaryEntry()
		{
			this.Info = new List<IBestiaryInfoElement>();
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x00557504 File Offset: 0x00555704
		public static BestiaryEntry Enemy(int npcNetId)
		{
			NPC npc = ContentSamples.NpcsByNetId[npcNetId];
			List<IBestiaryInfoElement> list = new List<IBestiaryInfoElement>
			{
				new NPCNetIdBestiaryInfoElement(npcNetId),
				new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
				new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
				new NPCKillCounterInfoElement(npcNetId)
			};
			list.Add(new NPCStatsReportInfoElement(npcNetId));
			if (npc.rarity != 0)
			{
				list.Add(new RareSpawnBestiaryInfoElement(npc.rarity));
			}
			IBestiaryUICollectionInfoProvider uiinfoProvider;
			if (npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
			{
				list.Add(new BossBestiaryInfoElement());
				uiinfoProvider = new CommonEnemyUICollectionInfoProvider(npc.GetBestiaryCreditId(), true);
			}
			else
			{
				uiinfoProvider = new CommonEnemyUICollectionInfoProvider(npc.GetBestiaryCreditId(), false);
			}
			string text = Lang.GetNPCName(npc.netID).Key;
			text = text.Replace("NPCName.", "");
			string text2 = "Bestiary_FlavorText.npc_" + text;
			if (Language.Exists(text2))
			{
				list.Add(new FlavorTextBestiaryInfoElement(text2));
			}
			return new BestiaryEntry
			{
				Icon = new UnlockableNPCEntryIcon(npcNetId, 0f, 0f, 0f, 0f, null),
				Info = list,
				UIInfoProvider = uiinfoProvider
			};
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x00557648 File Offset: 0x00555848
		public static BestiaryEntry TownNPC(int npcNetId)
		{
			NPC npc = ContentSamples.NpcsByNetId[npcNetId];
			List<IBestiaryInfoElement> list = new List<IBestiaryInfoElement>
			{
				new NPCNetIdBestiaryInfoElement(npcNetId),
				new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
				new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
				new NPCKillCounterInfoElement(npcNetId)
			};
			string text = Lang.GetNPCName(npc.netID).Key;
			text = text.Replace("NPCName.", "");
			string text2 = "Bestiary_FlavorText.npc_" + text;
			if (Language.Exists(text2))
			{
				list.Add(new FlavorTextBestiaryInfoElement(text2));
			}
			return new BestiaryEntry
			{
				Icon = new UnlockableNPCEntryIcon(npcNetId, 0f, 0f, 0f, 0f, null),
				Info = list,
				UIInfoProvider = new TownNPCUICollectionInfoProvider(npc.GetBestiaryCreditId())
			};
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x00557734 File Offset: 0x00555934
		public static BestiaryEntry Critter(int npcNetId)
		{
			NPC npc = ContentSamples.NpcsByNetId[npcNetId];
			List<IBestiaryInfoElement> list = new List<IBestiaryInfoElement>
			{
				new NPCNetIdBestiaryInfoElement(npcNetId),
				new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
				new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
				new NPCKillCounterInfoElement(npcNetId)
			};
			string text = Lang.GetNPCName(npc.netID).Key;
			text = text.Replace("NPCName.", "");
			string text2 = "Bestiary_FlavorText.npc_" + text;
			if (Language.Exists(text2))
			{
				list.Add(new FlavorTextBestiaryInfoElement(text2));
			}
			return new BestiaryEntry
			{
				Icon = new UnlockableNPCEntryIcon(npcNetId, 0f, 0f, 0f, 0f, null),
				Info = list,
				UIInfoProvider = new CritterUICollectionInfoProvider(npc.GetBestiaryCreditId())
			};
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x0055781D File Offset: 0x00555A1D
		public static BestiaryEntry Biome(string nameLanguageKey, string texturePath, Func<bool> unlockCondition)
		{
			return new BestiaryEntry
			{
				Icon = new CustomEntryIcon(nameLanguageKey, texturePath, unlockCondition),
				Info = new List<IBestiaryInfoElement>()
			};
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x0055783D File Offset: 0x00555A3D
		public void AddTags(params IBestiaryInfoElement[] elements)
		{
			this.Info.AddRange(elements);
		}

		// Token: 0x04004837 RID: 18487
		public IEntryIcon Icon;

		// Token: 0x04004839 RID: 18489
		public IBestiaryUICollectionInfoProvider UIInfoProvider;
	}
}
