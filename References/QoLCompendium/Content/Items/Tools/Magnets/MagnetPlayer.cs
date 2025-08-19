// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Magnets.MagnetPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Magnets;

public class MagnetPlayer : ModPlayer
{
  public bool BaseMagnet;
  public bool HellstoneMagnet;
  public bool SoulMagnet;
  public bool SpectreMagnet;
  public bool LunarMagnet;

  public virtual void Initialize() => this.Reset();

  public virtual void ResetEffects() => this.Reset();

  public virtual void UpdateDead() => this.Reset();

  private void Reset()
  {
    this.BaseMagnet = false;
    this.HellstoneMagnet = false;
    this.SoulMagnet = false;
    this.SpectreMagnet = false;
    this.LunarMagnet = false;
  }
}
