// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.WorldChanges.TimeRateModifiers
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.WorldChanges;

public class TimeRateModifiers : ModSystem
{
  public virtual void ModifyTimeRate(
    ref double timeRate,
    ref double tileUpdateRate,
    ref double eventUpdateRate)
  {
    QoLCPlayer qoLcPlayer;
    if (!Main.LocalPlayer.TryGetModPlayer<QoLCPlayer>(ref qoLcPlayer) || !qoLcPlayer.pausePedestal || !((Entity) Main.LocalPlayer).active || Main.dedServ || Main.gameMenu || ((CreativePowers.ASharedTogglePower) CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>()).Enabled)
      return;
    timeRate = 0.0;
  }

  public virtual void PreUpdateTime()
  {
    if (Main.LocalPlayer.GetModPlayer<QoLCPlayer>().sunPedestal && !Main.dayTime)
    {
      Main.dayTime = true;
      Main.time = 0.0;
    }
    if (Main.LocalPlayer.GetModPlayer<QoLCPlayer>().moonPedestal && Main.dayTime)
    {
      Main.dayTime = false;
      Main.time = 0.0;
    }
    if (!Main.dayTime && Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bloodMoonPedestal)
      Main.bloodMoon = true;
    if (!Main.dayTime || !Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eclipsePedestal)
      return;
    Main.eclipse = true;
  }
}
