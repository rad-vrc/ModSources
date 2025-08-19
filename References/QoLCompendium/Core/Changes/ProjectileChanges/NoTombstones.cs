// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ProjectileChanges.NoTombstones
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ProjectileChanges;

public class NoTombstones : ModSystem
{
  public virtual void Load()
  {
    // ISSUE: method pointer
    On_Player.DropTombstone += new On_Player.hook_DropTombstone((object) this, __methodptr(DontSpawnTombstones));
  }

  public virtual void Unload()
  {
    // ISSUE: method pointer
    On_Player.DropTombstone -= new On_Player.hook_DropTombstone((object) this, __methodptr(DontSpawnTombstones));
  }

  private void DontSpawnTombstones(
    On_Player.orig_DropTombstone orig,
    Player self,
    long coinsOwned,
    NetworkText deathText,
    int hitDirection)
  {
    if (QoLCompendium.QoLCompendium.mainConfig.NoTombstoneDrops)
      return;
    orig.Invoke(self, coinsOwned, deathText, hitDirection);
  }
}
