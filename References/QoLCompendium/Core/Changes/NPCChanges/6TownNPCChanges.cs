// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.NoTownSlimesSystem
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class NoTownSlimesSystem : ModSystem
{
  public virtual void PreUpdateWorld()
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.NoTownSlimes)
      return;
    NPC.unlockedSlimeBlueSpawn = false;
    NPC.unlockedSlimeCopperSpawn = false;
    NPC.unlockedSlimeGreenSpawn = false;
    NPC.unlockedSlimeOldSpawn = false;
    NPC.unlockedSlimePurpleSpawn = false;
    NPC.unlockedSlimeRainbowSpawn = false;
    NPC.unlockedSlimeRedSpawn = false;
    NPC.unlockedSlimeYellowSpawn = false;
    Main.townNPCCanSpawn[670] = false;
    Main.townNPCCanSpawn[678] = false;
    Main.townNPCCanSpawn[679] = false;
    Main.townNPCCanSpawn[680] = false;
    Main.townNPCCanSpawn[681] = false;
    Main.townNPCCanSpawn[682] = false;
    Main.townNPCCanSpawn[683] = false;
    Main.townNPCCanSpawn[684] = false;
  }
}
