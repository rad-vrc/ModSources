// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.PlayerChanges.VeinMinerExtension
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Mono.Cecil.Cil;
using MonoMod.Cil;

#nullable disable
namespace QoLCompendium.Core.Changes.PlayerChanges;

public static class VeinMinerExtension
{
  public static ILCursor EmitCall<T>(this ILCursor ilCursor, string memberName)
  {
    return ilCursor.Emit<T>(OpCodes.Call, memberName);
  }
}
