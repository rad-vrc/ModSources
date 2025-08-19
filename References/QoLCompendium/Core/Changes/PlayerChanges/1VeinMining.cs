// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.PlayerChanges.VeinMiningSystem
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using VanillaQoL.Config;

#nullable enable
namespace QoLCompendium.Core.Changes.PlayerChanges;

public class VeinMiningSystem : ModSystem
{
  public static int threshold = QoLCompendium.QoLCompendium.mainConfig.VeinMinerTileLimit;

  [JITWhenModsEnabled(new string[] {"VanillaQoL"})]
  public static bool VanillaQoLVeinminer => QoLConfig.Instance.veinMining;

  public virtual void Load()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    IL_Player.PickTile += VeinMiningSystem.\u003C\u003EO.\u003C0\u003E__PickTilePatch ?? (VeinMiningSystem.\u003C\u003EO.\u003C0\u003E__PickTilePatch = new ILContext.Manipulator((object) null, __methodptr(PickTilePatch)));
  }

  public virtual void Unload()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    IL_Player.PickTile -= VeinMiningSystem.\u003C\u003EO.\u003C0\u003E__PickTilePatch ?? (VeinMiningSystem.\u003C\u003EO.\u003C0\u003E__PickTilePatch = new ILContext.Manipulator((object) null, __methodptr(PickTilePatch)));
  }

  public virtual void PreUpdateWorld()
  {
    VeinMiningSystem.threshold = QoLCompendium.QoLCompendium.mainConfig.VeinMinerTileLimit;
  }

  public static void PickTilePatch(
  #nullable disable
  ILContext il)
  {
    ILCursor ilCursor = new ILCursor(il);
    if (!ilCursor.TryGotoNext((MoveType) 0, new Func<Instruction, bool>[6]
    {
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchLdarg1(i)),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchLdarg2(i)),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchLdcI4(i, 0)),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchLdcI4(i, 0)),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchLdcI4(i, 0)),
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchCall<WorldGen>(i, "KillTile"))
    }))
      return;
    ilCursor.EmitLdarg0();
    ilCursor.EmitCall<VeinMiningSystem>("ClearCD");
    ilCursor.EmitLdarg0();
    ilCursor.EmitLdarg1();
    ilCursor.EmitLdarg2();
    ilCursor.EmitLdarg3();
    ilCursor.EmitCall<VeinMiningSystem>("VeinMine");
  }

  public static void ClearCD(Player player) => player.GetModPlayer<VeinMiningPlayer>().ctr = 0;

  public static void VeinMine(Player player, int x, int y, int pickPower)
  {
    Tile tileSafely1 = Framing.GetTileSafely(x, y);
    VeinMiningPlayer modPlayer = player.GetModPlayer<VeinMiningPlayer>();
    if (!((Tile) ref tileSafely1).HasTile || !VeinMiningSystem.CanVeinMine(tileSafely1) || !modPlayer.CanMine)
      return;
    modPlayer.pickPower = pickPower;
    foreach ((int x, int y) tuple in VeinMiningSystem.TileRot(x, y))
    {
      Tile tileSafely2 = Framing.GetTileSafely(tuple.x, tuple.y);
      bool flag = modPlayer.NotInQueue(tuple.x, tuple.y);
      if (((!((Tile) ref tileSafely2).HasTile ? 0 : (VeinMiningSystem.CanVeinMine(tileSafely2) ? 1 : 0)) & (flag ? 1 : 0)) != 0)
      {
        ++modPlayer.ctr;
        if (modPlayer.ctr > VeinMiningSystem.threshold)
        {
          modPlayer.ctr = 0;
          modPlayer.CanMine = false;
        }
        modPlayer.QueueTile(tuple.x, tuple.y);
        VeinMiningSystem.VeinMine(player, tuple.x, tuple.y, pickPower);
      }
    }
  }

  public static bool CanVeinMine(Tile tile)
  {
    if (VeinMiningSystem.VanillaQoLLoaded() || !QoLCompendium.QoLCompendium.mainConfig.VeinMiner || QoLCompendium.QoLCompendium.mainConfig.VeinMinerTileWhitelist == null)
      return false;
    string fullNameById = Common.GetFullNameById((int) ((Tile) ref tile).TileType);
    return fullNameById != null && QoLCompendium.QoLCompendium.mainConfig.VeinMinerTileWhitelist.Contains(fullNameById);
  }

  public static IEnumerable<(int x, int y)> TileRot(int x, int y)
  {
    for (int i = x - 1; i <= x + 1; ++i)
    {
      for (int j = y - 1; j <= y + 1; ++j)
      {
        if (i != x || j != y)
          yield return (i, j);
      }
    }
  }

  public static bool VanillaQoLLoaded()
  {
    return Terraria.ModLoader.ModLoader.HasMod("VanillaQoL") && VeinMiningSystem.VanillaQoLVeinminer;
  }
}
