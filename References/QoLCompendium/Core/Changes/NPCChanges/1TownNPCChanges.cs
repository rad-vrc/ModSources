// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.ImprovedTownNPCSpawns
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class ImprovedTownNPCSpawns : ModSystem
{
  private static bool _isExtraUpdate;
  private static HashSet<int> _activeTownNPCs = new HashSet<int>();
  private static BestiaryUnlockProgressReport _cachedReport = new BestiaryUnlockProgressReport();

  public virtual void Load()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_Main.UpdateTime_SpawnTownNPCs += ImprovedTownNPCSpawns.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (ImprovedTownNPCSpawns.\u003C\u003Ec.\u003C\u003E9__3_0 = new On_Main.hook_UpdateTime_SpawnTownNPCs((object) ImprovedTownNPCSpawns.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CLoad\u003Eb__3_0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_WorldGen.QuickFindHome += ImprovedTownNPCSpawns.\u003C\u003Ec.\u003C\u003E9__3_1 ?? (ImprovedTownNPCSpawns.\u003C\u003Ec.\u003C\u003E9__3_1 = new On_WorldGen.hook_QuickFindHome((object) ImprovedTownNPCSpawns.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CLoad\u003Eb__3_1)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_Main.GetBestiaryProgressReport += ImprovedTownNPCSpawns.\u003C\u003Ec.\u003C\u003E9__3_2 ?? (ImprovedTownNPCSpawns.\u003C\u003Ec.\u003C\u003E9__3_2 = new On_Main.hook_GetBestiaryProgressReport((object) ImprovedTownNPCSpawns.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CLoad\u003Eb__3_2)));
  }

  public virtual void PreUpdateWorld()
  {
    if (QoLCompendium.QoLCompendium.mainConfig.TownNPCSpawnImprovements)
    {
      NPC.savedAngler = true;
      NPC.savedGolfer = true;
      NPC.savedStylist = true;
      if (NPC.downedGoblins)
        NPC.savedGoblin = true;
      if (NPC.downedBoss2)
        NPC.savedBartender = true;
      if (NPC.downedBoss3)
        NPC.savedMech = true;
      if (Main.hardMode)
      {
        NPC.savedWizard = true;
        NPC.savedTaxCollector = true;
      }
    }
    if (!ImprovedTownNPCSpawns.HasEnoughMoneyForMerchant() || !QoLCompendium.QoLCompendium.mainConfig.AutoMoneyQuickStack)
      return;
    ImprovedTownNPCSpawns.TrySetNPCSpawn(17);
  }

  private static void TrySetNPCSpawn(
    On_Main.orig_UpdateTime_SpawnTownNPCs orig,
    double worldUpdateRate)
  {
    orig.Invoke();
    if (Main.netMode == 1 || worldUpdateRate <= 0.0 || Main.checkForSpawns != 0)
      return;
    ImprovedTownNPCSpawns.SetupActiveTownNPCList();
    if (!ImprovedTownNPCSpawns.HasEnoughMoneyForMerchant())
      return;
    ImprovedTownNPCSpawns.TrySetNPCSpawn(17);
  }

  public static bool HasEnoughMoneyForMerchant()
  {
    int num = 0;
    for (int index1 = 0; index1 < (int) byte.MaxValue; ++index1)
    {
      Player player = Main.player[index1];
      if (((Entity) player).active)
      {
        for (int index2 = 0; index2 < 40; ++index2)
        {
          if (player.bank.item[index2] != null && player.bank.item[index2].stack > 0)
          {
            Item obj = player.bank.item[index2];
            switch (obj.type)
            {
              case 71:
                num += obj.stack;
                break;
              case 72:
                num += obj.stack * 100;
                break;
              case 73:
                return true;
              case 74:
                return true;
            }
            if (num >= 5000)
              return true;
          }
        }
      }
    }
    return false;
  }

  public static void TrySetNPCSpawn(int npcId)
  {
    if (ImprovedTownNPCSpawns._activeTownNPCs.Contains(npcId))
      return;
    Main.townNPCCanSpawn[npcId] = true;
    if (WorldGen.prioritizedTownNPCType != 0)
      return;
    WorldGen.prioritizedTownNPCType = npcId;
  }

  private static void SetupActiveTownNPCList()
  {
    ImprovedTownNPCSpawns._activeTownNPCs = new HashSet<int>();
    for (int index = 0; index < Main.maxNPCs; ++index)
    {
      NPC npc = Main.npc[index];
      if (((Entity) npc).active && npc.townNPC && npc.friendly)
        ImprovedTownNPCSpawns._activeTownNPCs.Add(npc.type);
    }
  }
}
