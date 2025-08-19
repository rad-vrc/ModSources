// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.BestiaryUnlock
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class BestiaryUnlock : GlobalNPC
{
  public virtual void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.OneKillForBestiaryEntries)
      return;
    bestiaryEntry.UIInfoProvider = (IBestiaryUICollectionInfoProvider) new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[npc.type], true);
  }
}
