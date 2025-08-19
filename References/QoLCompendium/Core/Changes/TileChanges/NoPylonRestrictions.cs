// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.NoPylonRestrictions
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.TileChanges;

public class NoPylonRestrictions : GlobalPylon
{
  public virtual bool? ValidTeleportCheck_PreAnyDanger(TeleportPylonInfo pylonInfo)
  {
    return QoLCompendium.QoLCompendium.mainConfig.NoPylonTeleportRestrictions ? new bool?(true) : new bool?();
  }

  public virtual bool? ValidTeleportCheck_PreNPCCount(
    TeleportPylonInfo pylonInfo,
    ref int defaultNecessaryNPCCount)
  {
    return QoLCompendium.QoLCompendium.mainConfig.NoPylonTeleportRestrictions ? new bool?(true) : new bool?();
  }
}
