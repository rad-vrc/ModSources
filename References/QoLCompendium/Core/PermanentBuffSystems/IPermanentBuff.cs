// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.IPermanentBuff
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems;

public abstract class IPermanentBuff : IComparable
{
  public int CompareTo(object obj) => this.GetType().Name.CompareTo(obj.GetType().Name);

  internal abstract void ApplyEffect(PermanentBuffPlayer player);
}
