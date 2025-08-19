// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.NoTownSlimes
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class NoTownSlimes : GlobalNPC
{
  public virtual void AI(NPC npc)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.NoTownSlimes || !((Entity) npc).active || !Common.TownSlimeIDs.Contains(npc.type))
      return;
    npc.timeLeft = 0;
    ((Entity) npc).active = false;
  }
}
