// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.TownNPCsCanLiveInEvil
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

#nullable enable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class TownNPCsCanLiveInEvil : ModSystem
{
  public virtual bool IsLoadingEnabled(
  #nullable disable
  Mod mod)
  {
    Mod mod1;
    Terraria.ModLoader.ModLoader.TryGetMod("VanillaQoL", ref mod1);
    return mod1 == null;
  }

  public virtual void Load()
  {
    // ISSUE: method pointer
    IL_WorldGen.ScoreRoom += new ILContext.Manipulator((object) this, __methodptr(LiveInCorrupt));
  }

  public virtual void Unload()
  {
    // ISSUE: method pointer
    IL_WorldGen.ScoreRoom -= new ILContext.Manipulator((object) this, __methodptr(LiveInCorrupt));
  }

  private void LiveInCorrupt(ILContext il)
  {
    ILCursor ilCursor = new ILCursor(il);
    if (!ilCursor.TryGotoNext((MoveType) 2, new Func<Instruction, bool>[5]
    {
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchCall(i, (MethodBase) typeof (WorldGen).GetMethod("GetTileTypeCountByCategory"))),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.Match(i, OpCodes.Neg)),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.Match(i, OpCodes.Stloc_S)),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.Match(i, OpCodes.Ldloc_S)),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.Match(i, OpCodes.Ldc_I4_S))
    }))
      return;
    ilCursor.EmitDelegate<Func<int, int>>((Func<int, int>) (returnValue => !QoLCompendium.QoLCompendium.mainConfig.TownNPCsLiveInEvil ? returnValue : 114514));
  }
}
