// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.AnglerReset
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.IO;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class AnglerReset : GlobalNPC
{
  public virtual void OnChatButtonClicked(NPC npc, bool firstButton)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.AnglerQuestInstantReset || !Main.anglerQuestFinished)
      return;
    if (Main.netMode == 0)
    {
      Main.AnglerQuestSwap();
    }
    else
    {
      if (Main.netMode != 1)
        return;
      ModPacket packet = ((ModType) this).Mod.GetPacket(256 /*0x0100*/);
      ((BinaryWriter) packet).Write((byte) 3);
      packet.Send(-1, -1);
    }
  }
}
