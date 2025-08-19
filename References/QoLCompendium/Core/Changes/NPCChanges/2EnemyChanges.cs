// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.TowerShieldHealth
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class TowerShieldHealth : ModSystem
{
  public virtual void PreUpdateWorld()
  {
    if (NPC.ShieldStrengthTowerVortex > QoLCompendium.QoLCompendium.mainConfig.LunarPillarShieldHeath)
      NPC.ShieldStrengthTowerVortex = QoLCompendium.QoLCompendium.mainConfig.LunarPillarShieldHeath;
    if (NPC.ShieldStrengthTowerSolar > QoLCompendium.QoLCompendium.mainConfig.LunarPillarShieldHeath)
      NPC.ShieldStrengthTowerSolar = QoLCompendium.QoLCompendium.mainConfig.LunarPillarShieldHeath;
    if (NPC.ShieldStrengthTowerNebula > QoLCompendium.QoLCompendium.mainConfig.LunarPillarShieldHeath)
      NPC.ShieldStrengthTowerNebula = QoLCompendium.QoLCompendium.mainConfig.LunarPillarShieldHeath;
    if (NPC.ShieldStrengthTowerStardust <= QoLCompendium.QoLCompendium.mainConfig.LunarPillarShieldHeath)
      return;
    NPC.ShieldStrengthTowerStardust = QoLCompendium.QoLCompendium.mainConfig.LunarPillarShieldHeath;
  }
}
