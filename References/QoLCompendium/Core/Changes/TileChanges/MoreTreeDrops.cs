// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.MoreTreeDrops
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.Utilities;

#nullable disable
namespace QoLCompendium.Core.Changes.TileChanges;

public class MoreTreeDrops : ModSystem
{
  public virtual void Load()
  {
    // ISSUE: method pointer
    On_WorldGen.SetGemTreeDrops += new On_WorldGen.hook_SetGemTreeDrops((object) this, __methodptr(GemAlways));
    MoreTreeDrops.ShakeTreeTweak.Load();
  }

  public virtual void Unload()
  {
    // ISSUE: method pointer
    On_WorldGen.SetGemTreeDrops -= new On_WorldGen.hook_SetGemTreeDrops((object) this, __methodptr(GemAlways));
  }

  private void GemAlways(
    On_WorldGen.orig_SetGemTreeDrops orig,
    int gemType,
    int seedType,
    Tile tileCache,
    ref int dropItem,
    ref int secondaryItem)
  {
    if (QoLCompendium.QoLCompendium.mainConfig.TreesDropMoreWhenShook)
    {
      dropItem = gemType;
      secondaryItem = seedType;
    }
    else
      orig.Invoke(gemType, seedType, tileCache, ref dropItem, ref secondaryItem);
  }

  public static int GetShakeTreeFruit(TreeTypes treeType)
  {
    switch (treeType - 1)
    {
      case 0:
        WeightedRandom<short> weightedRandom = new WeightedRandom<short>();
        weightedRandom.Add((short) 4009, 1.0);
        weightedRandom.Add((short) 4282, 1.0);
        weightedRandom.Add((short) 4293, 1.0);
        weightedRandom.Add((short) 4290, 1.0);
        weightedRandom.Add((short) 4291, 1.0);
        return (int) weightedRandom.Get();
      case 1:
        return Utils.NextBool(WorldGen.genRand, 2) ? 4284 : 4289;
      case 3:
        return Utils.NextBool(WorldGen.genRand, 2) ? 4296 : 4285;
      case 4:
        return Utils.NextBool(WorldGen.genRand, 2) ? 4294 : 4292;
      case 5:
        return Utils.NextBool(WorldGen.genRand, 2) ? 4286 : 4295;
      case 6:
        return Utils.NextBool(WorldGen.genRand, 2) ? 4297 : 4288;
      case 7:
      case 8:
      case 9:
      case 10:
        return Utils.NextBool(WorldGen.genRand, 2) ? 4283 : 4287;
      case 11:
        return Utils.NextBool(WorldGen.genRand, 2) ? 5278 : 5277;
      default:
        return -1;
    }
  }

  private class ShakeTreeTweak
  {
    private static bool _isShakingTree;
    private static bool _hasItemDropped;

    public static void Load()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      On_WorldGen.ShakeTree += new On_WorldGen.hook_ShakeTree((object) new MoreTreeDrops.ShakeTreeTweak.\u003C\u003Ec__DisplayClass3_0()
      {
        numShakes = typeof (WorldGen).GetField("numTreeShakes", (BindingFlags) 40),
        maxShakes = typeof (WorldGen).GetField("maxTreeShakes", (BindingFlags) 40),
        shakeX = typeof (WorldGen).GetField("treeShakeX", (BindingFlags) 40),
        shakeY = typeof (WorldGen).GetField("treeShakeY", (BindingFlags) 40)
      }, __methodptr(\u003CLoad\u003Eb__0));
    }

    private class ShakeTreeItem : GlobalItem
    {
      public virtual void OnSpawn(Item item, IEntitySource source)
      {
        if (!MoreTreeDrops.ShakeTreeTweak._isShakingTree || !(source is EntitySource_ShakeTree))
          return;
        MoreTreeDrops.ShakeTreeTweak._hasItemDropped = true;
      }
    }
  }
}
