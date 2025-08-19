// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.DisplayLoadedSupportedMods
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core;

public class DisplayLoadedSupportedMods : ModCommand
{
  public virtual CommandType Type => (CommandType) 1;

  public virtual string Command => "displayLoadedMods";

  public virtual string Description
  {
    get => "Displays loaded mods that Quality of Life Compendium supports";
  }

  public virtual void Action(CommandCaller caller, string input, string[] args)
  {
    foreach (KeyValuePair<string, Mod> loadedMod in LoadModSupport.LoadedMods)
      Main.NewText(loadedMod.Value.DisplayName + " is loaded", byte.MaxValue, byte.MaxValue, byte.MaxValue);
  }
}
