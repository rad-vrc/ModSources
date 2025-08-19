// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.IPermanentModdedBuff
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems;

public abstract class IPermanentModdedBuff : IComparable
{
  public int index = 44;
  public ModBuff buffToApply;

  public int CompareTo(object obj) => this.GetType().Name.CompareTo(obj.GetType().Name);

  internal abstract void ApplyEffect(PermanentBuffPlayer player);
}
