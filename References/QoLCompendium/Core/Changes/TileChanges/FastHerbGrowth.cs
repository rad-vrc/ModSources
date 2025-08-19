// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.FastHerbGrowth
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

#nullable enable
namespace QoLCompendium.Core.Changes.TileChanges;

public class FastHerbGrowth : ModSystem
{
  public virtual void Load()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    IL_WorldGen.GrowAlch += FastHerbGrowth.\u003C\u003EO.\u003C0\u003E__WorldGen_GrowAlch ?? (FastHerbGrowth.\u003C\u003EO.\u003C0\u003E__WorldGen_GrowAlch = new ILContext.Manipulator((object) null, __methodptr(WorldGen_GrowAlch)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_TileDrawing.IsAlchemyPlantHarvestable += FastHerbGrowth.\u003C\u003EO.\u003C1\u003E__TileDrawing_IsAlchemyPlantHarvestable ?? (FastHerbGrowth.\u003C\u003EO.\u003C1\u003E__TileDrawing_IsAlchemyPlantHarvestable = new On_TileDrawing.hook_IsAlchemyPlantHarvestable((object) null, __methodptr(TileDrawing_IsAlchemyPlantHarvestable)));
  }

  public virtual void Unload()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    IL_WorldGen.GrowAlch -= FastHerbGrowth.\u003C\u003EO.\u003C0\u003E__WorldGen_GrowAlch ?? (FastHerbGrowth.\u003C\u003EO.\u003C0\u003E__WorldGen_GrowAlch = new ILContext.Manipulator((object) null, __methodptr(WorldGen_GrowAlch)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_TileDrawing.IsAlchemyPlantHarvestable -= FastHerbGrowth.\u003C\u003EO.\u003C1\u003E__TileDrawing_IsAlchemyPlantHarvestable ?? (FastHerbGrowth.\u003C\u003EO.\u003C1\u003E__TileDrawing_IsAlchemyPlantHarvestable = new On_TileDrawing.hook_IsAlchemyPlantHarvestable((object) null, __methodptr(TileDrawing_IsAlchemyPlantHarvestable)));
  }

  public static void WorldGen_GrowAlch(
  #nullable disable
  ILContext il)
  {
    ILCursor ilCursor = new ILCursor(il);
    if (!ilCursor.TryGotoNext((MoveType) 2, new Func<Instruction, bool>[2]
    {
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.Match(i, OpCodes.Call)),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.Match(i, OpCodes.Ldc_I4_S))
    }))
      return;
    ilCursor.EmitDelegate<Func<int, int>>((Func<int, int>) (num => !QoLCompendium.QoLCompendium.mainConfig.FastHerbGrowth ? num : 1));
  }

  public static bool TileDrawing_IsAlchemyPlantHarvestable(
    On_TileDrawing.orig_IsAlchemyPlantHarvestable orig,
    TileDrawing self,
    int style)
  {
    return QoLCompendium.QoLCompendium.mainConfig.FastHerbGrowth || orig.Invoke(self, style);
  }
}
