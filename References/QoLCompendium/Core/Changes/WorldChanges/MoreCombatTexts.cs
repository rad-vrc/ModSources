// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.WorldChanges.MoreCombatTexts
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable enable
namespace QoLCompendium.Core.Changes.WorldChanges;

public class MoreCombatTexts : ModSystem
{
  public virtual void Load()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    MoreCombatTexts.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new MoreCombatTexts.\u003C\u003Ec__DisplayClass0_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.maximumTexts = QoLCompendium.QoLCompendium.mainClientConfig.CombatTextLimit;
    // ISSUE: reference to a compiler-generated field
    Array.Resize<CombatText>(ref Main.combatText, cDisplayClass00.maximumTexts);
    // ISSUE: reference to a compiler-generated field
    for (int index = 0; index < cDisplayClass00.maximumTexts; ++index)
      Main.combatText[index] = new CombatText();
    // ISSUE: method pointer
    On_CombatText.UpdateCombatText += new On_CombatText.hook_UpdateCombatText((object) cDisplayClass00, __methodptr(\u003CLoad\u003Eb__0));
    // ISSUE: method pointer
    On_CombatText.clearAll += new On_CombatText.hook_clearAll((object) cDisplayClass00, __methodptr(\u003CLoad\u003Eb__1));
    // ISSUE: method pointer
    IL_CombatText.NewText_Rectangle_Color_string_bool_bool += new ILContext.Manipulator((object) cDisplayClass00, __methodptr(\u003CLoad\u003Eb__2));
    // ISSUE: method pointer
    IL_Main.DoDraw += new ILContext.Manipulator((object) cDisplayClass00, __methodptr(\u003CLoad\u003Eb__3));
  }
}
