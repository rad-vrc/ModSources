// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.WorldChanges.DisableWorldEvilSpread
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.WorldChanges;

public class DisableWorldEvilSpread : ModSystem
{
  public static bool CorruptionSpreadDisabled;

  public virtual void Load()
  {
    // ISSUE: method pointer
    IL_WorldGen.UpdateWorld_Inner += new ILContext.Manipulator((object) this, __methodptr(WorldGen_UpdateWorld_Inner));
  }

  public virtual void Unload()
  {
    // ISSUE: method pointer
    IL_WorldGen.UpdateWorld_Inner -= new ILContext.Manipulator((object) this, __methodptr(WorldGen_UpdateWorld_Inner));
  }

  private void WorldGen_UpdateWorld_Inner(ILContext il)
  {
    ILCursor ilCursor = new ILCursor(il);
    Func<Instruction, bool>[] funcArray = new Func<Instruction, bool>[2]
    {
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchLdcI4(i, 3)),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchStloc(i, 1))
    };
    ilCursor.GotoNext((MoveType) 0, funcArray);
    ilCursor.MoveAfterLabels();
    ilCursor.EmitDelegate<Action>((Action) (() =>
    {
      if (!DisableWorldEvilSpread.CorruptionSpreadDisabled)
        return;
      WorldGen.AllowedToSpreadInfections = false;
    }));
  }

  public virtual void ClearWorld()
  {
    DisableWorldEvilSpread.CorruptionSpreadDisabled = QoLCompendium.QoLCompendium.mainConfig.DisableEvilBiomeSpread;
  }
}
