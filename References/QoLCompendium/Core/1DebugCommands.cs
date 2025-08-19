// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.DisplayDownedBosses
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core;

public class DisplayDownedBosses : ModCommand
{
  public virtual CommandType Type => (CommandType) 1;

  public virtual string Command => "displayDownedBosses";

  public virtual string Description
  {
    get => "Displays all downed bosses from mods that Quality of Life Compendium supports";
  }

  public virtual void Action(CommandCaller caller, string input, string[] args)
  {
    for (int index = 0; index < ModConditions.DownedBoss.Length; ++index)
      Main.NewText($"Boss: {Enum.GetName(typeof (ModConditions.Downed), (object) index)} | Downed: {ModConditions.DownedBoss[index].ToString()} | Saved at: {index.ToString()}", byte.MaxValue, byte.MaxValue, byte.MaxValue);
  }
}
