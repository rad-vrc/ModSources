// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.CheckTravelingNPCS
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class CheckTravelingNPCS : GlobalNPC
{
  public virtual void OnChatButtonClicked(NPC npc, bool firstButton)
  {
    if (npc.type == 368)
      ModConditions.talkedToTravelingMerchant = true;
    if (npc.type != 453)
      return;
    ModConditions.talkedToSkeletonMerchant = true;
  }
}
