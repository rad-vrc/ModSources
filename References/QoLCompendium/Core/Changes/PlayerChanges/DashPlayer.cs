// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.PlayerChanges.DashPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.PlayerChanges;

public class DashPlayer : ModPlayer
{
  public int latestXDirPressed;
  public int latestXDirReleased;
  public bool LeftLastPressed;
  public bool RightLastPressed;

  public virtual void ProcessTriggers(TriggersSet triggersSet)
  {
    if (Main.LocalPlayer.controlLeft && !this.LeftLastPressed)
      this.latestXDirPressed = -1;
    if (Main.LocalPlayer.controlRight && !this.RightLastPressed)
      this.latestXDirPressed = 1;
    if (!Main.LocalPlayer.controlLeft && !Main.LocalPlayer.releaseLeft)
      this.latestXDirReleased = -1;
    if (!Main.LocalPlayer.controlRight && !Main.LocalPlayer.releaseRight)
      this.latestXDirReleased = 1;
    this.LeftLastPressed = Main.LocalPlayer.controlLeft;
    this.RightLastPressed = Main.LocalPlayer.controlRight;
  }
}
