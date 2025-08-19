using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000675 RID: 1653
	public class BestiaryEntry
	{
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060047AA RID: 18346 RVA: 0x006460B0 File Offset: 0x006442B0
		// (set) Token: 0x060047AB RID: 18347 RVA: 0x006460B8 File Offset: 0x006442B8
		public List<IBestiaryInfoElement> Info { get; private set; }

		// Token: 0x060047AC RID: 18348 RVA: 0x006460C1 File Offset: 0x006442C1
		public BestiaryEntry()
		{
			this.Info = new List<IBestiaryInfoElement>();
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x006460D4 File Offset: 0x006442D4
		public static BestiaryEntry Enemy(int npcNetId)
		{
			NPC nPC = ContentSamples.NpcsByNetId[npcNetId];
			List<IBestiaryInfoElement> list = new List<IBestiaryInfoElement>
			{
				new NPCNetIdBestiaryInfoElement(npcNetId),
				new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
				new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
				new NPCKillCounterInfoElement(npcNetId)
			};
			list.Add(new NPCStatsReportInfoElement(npcNetId));
			if (nPC.rarity != 0)
			{
				list.Add(new RareSpawnBestiaryInfoElement(nPC.rarity));
			}
			IBestiaryUICollectionInfoProvider uIInfoProvider;
			if (nPC.boss || NPCID.Sets.ShouldBeCountedAsBoss[nPC.type])
			{
				list.Add(new BossBestiaryInfoElement());
				uIInfoProvider = new CommonEnemyUICollectionInfoProvider(nPC.GetBestiaryCreditId(), true);
			}
			else
			{
				uIInfoProvider = new CommonEnemyUICollectionInfoProvider(nPC.GetBestiaryCreditId(), false);
			}
			string key = Lang.GetNPCName(nPC.netID).Key;
			key = key.Replace("NPCName.", "");
			string text = "Bestiary_FlavorText.npc_" + key;
			if (Language.Exists(text))
			{
				list.Add(new FlavorTextBestiaryInfoElement(text));
			}
			return new BestiaryEntry
			{
				Icon = new UnlockableNPCEntryIcon(npcNetId, 0f, 0f, 0f, 0f, null),
				Info = list,
				UIInfoProvider = uIInfoProvider
			};
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x00646218 File Offset: 0x00644418
		public static BestiaryEntry TownNPC(int npcNetId)
		{
			NPC nPC = ContentSamples.NpcsByNetId[npcNetId];
			List<IBestiaryInfoElement> list = new List<IBestiaryInfoElement>
			{
				new NPCNetIdBestiaryInfoElement(npcNetId),
				new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
				new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
				new NPCKillCounterInfoElement(npcNetId)
			};
			string key = Lang.GetNPCName(nPC.netID).Key;
			key = key.Replace("NPCName.", "");
			string text = "Bestiary_FlavorText.npc_" + key;
			if (Language.Exists(text))
			{
				list.Add(new FlavorTextBestiaryInfoElement(text));
			}
			return new BestiaryEntry
			{
				Icon = new UnlockableNPCEntryIcon(npcNetId, 0f, 0f, 0f, 0f, null),
				Info = list,
				UIInfoProvider = new TownNPCUICollectionInfoProvider(nPC.GetBestiaryCreditId())
			};
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x00646304 File Offset: 0x00644504
		public static BestiaryEntry Critter(int npcNetId)
		{
			NPC nPC = ContentSamples.NpcsByNetId[npcNetId];
			List<IBestiaryInfoElement> list = new List<IBestiaryInfoElement>
			{
				new NPCNetIdBestiaryInfoElement(npcNetId),
				new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
				new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
				new NPCKillCounterInfoElement(npcNetId)
			};
			string key = Lang.GetNPCName(nPC.netID).Key;
			key = key.Replace("NPCName.", "");
			string text = "Bestiary_FlavorText.npc_" + key;
			if (Language.Exists(text))
			{
				list.Add(new FlavorTextBestiaryInfoElement(text));
			}
			return new BestiaryEntry
			{
				Icon = new UnlockableNPCEntryIcon(npcNetId, 0f, 0f, 0f, 0f, null),
				Info = list,
				UIInfoProvider = new CritterUICollectionInfoProvider(nPC.GetBestiaryCreditId())
			};
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x006463ED File Offset: 0x006445ED
		public static BestiaryEntry Biome(string nameLanguageKey, string texturePath, Func<bool> unlockCondition)
		{
			return new BestiaryEntry
			{
				Icon = new CustomEntryIcon(nameLanguageKey, texturePath, unlockCondition),
				Info = new List<IBestiaryInfoElement>()
			};
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x0064640D File Offset: 0x0064460D
		public void AddTags(params IBestiaryInfoElement[] elements)
		{
			this.Info.AddRange(elements);
		}

		// Token: 0x04005BE4 RID: 23524
		public IEntryIcon Icon;

		// Token: 0x04005BE5 RID: 23525
		public IBestiaryUICollectionInfoProvider UIInfoProvider;
	}
}
