// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.UnbreakableQueenBeeLarva
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.TileChanges;

public class UnbreakableQueenBeeLarva : ModSystem
{
  private static bool oldBreak;

  public virtual void Load()
  {
    UnbreakableQueenBeeLarva.oldBreak = Main.tileCut[231];
    if (!QoLCompendium.QoLCompendium.mainConfig.NoLarvaBreak)
      return;
    Main.tileCut[231] = false;
  }

  public virtual void Unload() => Main.tileCut[231] = UnbreakableQueenBeeLarva.oldBreak;
}
