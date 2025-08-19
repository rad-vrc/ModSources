// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.WorldChanges.DisableCredits
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

#nullable enable
namespace QoLCompendium.Core.Changes.WorldChanges;

public class DisableCredits : ModSystem
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
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    IL_NPC.OnGameEventClearedForTheFirstTime += DisableCredits.\u003C\u003EO.\u003C0\u003E__NoCredits ?? (DisableCredits.\u003C\u003EO.\u003C0\u003E__NoCredits = new ILContext.Manipulator((object) null, __methodptr(NoCredits)));
  }

  public virtual void Unload()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    IL_NPC.OnGameEventClearedForTheFirstTime -= DisableCredits.\u003C\u003EO.\u003C0\u003E__NoCredits ?? (DisableCredits.\u003C\u003EO.\u003C0\u003E__NoCredits = new ILContext.Manipulator((object) null, __methodptr(NoCredits)));
  }

  public static void NoCredits(ILContext il)
  {
    ILCursor ilCursor = new ILCursor(il);
    ilCursor.GotoNext((MoveType) 0, new Func<Instruction, bool>[1]
    {
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchCall<CreditsRollEvent>(i, "TryStartingCreditsRoll"))
    });
    ilCursor.Remove();
  }
}
