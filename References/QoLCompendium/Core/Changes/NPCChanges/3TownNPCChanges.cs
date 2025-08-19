// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.DisableTownNPCHappiness
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable enable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class DisableTownNPCHappiness : ModSystem
{
  private 
  #nullable disable
  Hook hook;
  private Hook hook2;

  public virtual void Load()
  {
    // ISSUE: method pointer
    IL_Condition.cctor += new ILContext.Manipulator((object) this, __methodptr(\u003CLoad\u003Eb__2_0));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    IL_Main.DrawNPCChatButtons += DisableTownNPCHappiness.\u003C\u003Ec.\u003C\u003E9__2_1 ?? (DisableTownNPCHappiness.\u003C\u003Ec.\u003C\u003E9__2_1 = new ILContext.Manipulator((object) DisableTownNPCHappiness.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CLoad\u003Eb__2_1)));
  }

  public virtual void Unload()
  {
    this.hook?.Dispose();
    this.hook = (Hook) null;
    this.hook2?.Dispose();
    this.hook2 = (Hook) null;
  }

  private static bool IgnoreIfUnhappy(Func<object, bool> orig, object self)
  {
    return orig(self) || QoLCompendium.QoLCompendium.mainConfig.OverridePylonSales;
  }
}
