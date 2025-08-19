// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.BuffChanges.SliceOfCakeLastsUntilDeath
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.BuffChanges;

public class SliceOfCakeLastsUntilDeath : GlobalBuff
{
  private readonly bool _defaultTimeLeft = BuffID.Sets.TimeLeftDoesNotDecrease[192 /*0xC0*/];
  private readonly bool _defaultTimeDisplay = Main.buffNoTimeDisplay[192 /*0xC0*/];
  private readonly bool _defaultNoSave = Main.buffNoSave[192 /*0xC0*/];

  public virtual void SetStaticDefaults()
  {
    bool infiniteSliceOfCake = QoLCompendium.QoLCompendium.mainConfig.InfiniteSliceOfCake;
    BuffID.Sets.TimeLeftDoesNotDecrease[192 /*0xC0*/] = infiniteSliceOfCake || this._defaultTimeLeft;
    Main.buffNoTimeDisplay[192 /*0xC0*/] = infiniteSliceOfCake || this._defaultTimeDisplay;
    Main.buffNoSave[192 /*0xC0*/] = infiniteSliceOfCake || this._defaultNoSave;
  }
}
