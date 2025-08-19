// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.UIElementsAndLayers
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI;

public class UIElementsAndLayers : ModSystem
{
  public virtual void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
  {
    int index = layers.FindIndex((Predicate<GameInterfaceLayer>) (layer => layer.Name.Equals("Vanilla: Mouse Text")));
    if (index == -1)
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLCompendium: Locator Arrow", UIElementsAndLayers.\u003C\u003Ec.\u003C\u003E9__0_1 ?? (UIElementsAndLayers.\u003C\u003Ec.\u003C\u003E9__0_1 = new GameInterfaceDrawMethod((object) UIElementsAndLayers.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CModifyInterfaceLayers\u003Eb__0_1))), (InterfaceScaleType) 0));
  }

  internal static Dictionary<string, int> GetSentryNameToCount(out int totalCount, bool onlyCount = false)
  {
    totalCount = 0;
    Dictionary<string, int> sentryNameToCount = new Dictionary<string, int>();
    for (int index = 0; index < 1000; ++index)
    {
      Projectile projectile = Main.projectile[index];
      if (((Entity) projectile).active && projectile.sentry && projectile.owner == Main.myPlayer)
      {
        ++totalCount;
        if (!onlyCount)
        {
          string key = Lang.GetProjectileName(projectile.type).Value;
          if (string.IsNullOrEmpty(key))
            key = "Uncountable";
          if (sentryNameToCount.ContainsKey(key))
            sentryNameToCount[key]++;
          else
            sentryNameToCount.Add(key, 1);
        }
      }
    }
    return sentryNameToCount;
  }
}
