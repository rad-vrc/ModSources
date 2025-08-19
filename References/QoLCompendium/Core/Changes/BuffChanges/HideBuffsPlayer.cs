// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.BuffChanges.HideBuffsPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Collections.Generic;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.BuffChanges;

public class HideBuffsPlayer : ModPlayer
{
  public virtual void PostUpdateBuffs()
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.HideBuffs)
      return;
    HashSet<int> intSet1 = new HashSet<int>();
    HashSet<int> intSet2 = new HashSet<int>();
    for (int index = 0; index < this.Player.CountBuffs(); ++index)
    {
      if (!this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Contains(this.Player.buffType[index]))
        intSet2.Add(this.Player.buffType[index]);
      if (this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Contains(this.Player.buffType[index]))
        intSet1.Add(this.Player.buffType[index]);
    }
    foreach (int num in intSet1)
    {
      this.Player.ClearBuff(num);
      this.Player.AddBuff(num, 2, true, false);
    }
  }
}
