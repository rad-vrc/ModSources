// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.PlayerChanges.UnlockAllHairstyles
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.PlayerChanges;

public class UnlockAllHairstyles : ModSystem
{
  private static bool _rebuilt;

  public virtual void Load()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_HairstyleUnlocksHelper.ListWarrantsRemake += UnlockAllHairstyles.\u003C\u003EO.\u003C0\u003E__RebuildPatch ?? (UnlockAllHairstyles.\u003C\u003EO.\u003C0\u003E__RebuildPatch = new On_HairstyleUnlocksHelper.hook_ListWarrantsRemake((object) null, __methodptr(RebuildPatch)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_HairstyleUnlocksHelper.RebuildList += UnlockAllHairstyles.\u003C\u003EO.\u003C1\u003E__UnlockPatch ?? (UnlockAllHairstyles.\u003C\u003EO.\u003C1\u003E__UnlockPatch = new On_HairstyleUnlocksHelper.hook_RebuildList((object) null, __methodptr(UnlockPatch)));
  }

  public virtual void Unload()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_HairstyleUnlocksHelper.ListWarrantsRemake -= UnlockAllHairstyles.\u003C\u003EO.\u003C0\u003E__RebuildPatch ?? (UnlockAllHairstyles.\u003C\u003EO.\u003C0\u003E__RebuildPatch = new On_HairstyleUnlocksHelper.hook_ListWarrantsRemake((object) null, __methodptr(RebuildPatch)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_HairstyleUnlocksHelper.RebuildList -= UnlockAllHairstyles.\u003C\u003EO.\u003C1\u003E__UnlockPatch ?? (UnlockAllHairstyles.\u003C\u003EO.\u003C1\u003E__UnlockPatch = new On_HairstyleUnlocksHelper.hook_RebuildList((object) null, __methodptr(UnlockPatch)));
  }

  private static bool RebuildPatch(
    On_HairstyleUnlocksHelper.orig_ListWarrantsRemake orig,
    HairstyleUnlocksHelper self)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.AllHairStylesAvailable || UnlockAllHairstyles._rebuilt)
      return false;
    UnlockAllHairstyles._rebuilt = true;
    return true;
  }

  private static void UnlockPatch(
    On_HairstyleUnlocksHelper.orig_RebuildList orig,
    HairstyleUnlocksHelper self)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.AllHairStylesAvailable)
      return;
    self.AvailableHairstyles.Clear();
    for (int index = 0; index < TextureAssets.PlayerHair.Length; ++index)
      self.AvailableHairstyles.Add(index);
  }
}
