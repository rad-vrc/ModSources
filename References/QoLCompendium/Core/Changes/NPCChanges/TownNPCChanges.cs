// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.InvincibleTownNPCs
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class InvincibleTownNPCs : GlobalNPC
{
  public virtual bool InstancePerEntity => true;

  public virtual void SetDefaults(NPC npc)
  {
    bool damageFromHostiles = npc.dontTakeDamageFromHostiles;
    if (npc.type == 548 && QoLCompendium.QoLCompendium.mainConfig.TownNPCsDontDie)
    {
      npc.dontTakeDamageFromHostiles = false;
    }
    else
    {
      npc.dontTakeDamageFromHostiles = damageFromHostiles;
      if (npc.friendly && QoLCompendium.QoLCompendium.mainConfig.TownNPCsDontDie)
        npc.dontTakeDamageFromHostiles = true;
      else
        npc.dontTakeDamageFromHostiles = damageFromHostiles;
    }
  }
}
