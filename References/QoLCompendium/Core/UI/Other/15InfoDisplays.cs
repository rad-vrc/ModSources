// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Other.InfoPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System;
using System.Collections.Generic;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.UI.Other;

public class InfoPlayer : ModPlayer
{
  public bool battalionLog;
  public bool harmInducer;
  public bool headCounter;
  public bool kettlebell;
  public bool luckyDie;
  public bool metallicClover;
  public bool plateCracker;
  public bool regenerator;
  public bool reinforcedPanel;
  public bool replenisher;
  public bool trackingDevice;
  public bool wingTimer;
  public bool anglerRadar;
  public bool deteriorationDisplay;
  public bool skullWatch;
  public float armorPenetrationStat;
  public float critChanceStat;
  public float damageStat;
  private List<string> classString = new List<string>()
  {
    "Automatic"
  };
  public int classIndex;

  public virtual void ResetEffects() => this.Reset();

  public virtual void UpdateDead() => this.Reset();

  public virtual void Initialize()
  {
    for (int index = 1; index < DamageClassLoader.DamageClassCount; ++index)
    {
      string[] strArray1 = $"{DamageClassLoader.GetDamageClass(index)}".Split(".", StringSplitOptions.None);
      List<string> classString = this.classString;
      string[] strArray2 = strArray1;
      string str = strArray2[strArray2.Length - 1].Replace("DamageClass", "") + (strArray1[0] != "Terraria" ? $"({strArray1[0]})" : "");
      classString.Add(str);
    }
  }

  public virtual void PostUpdate()
  {
    this.armorPenetrationStat = this.Player.GetArmorPenetration(DamageClass.Generic);
    this.critChanceStat = this.Player.GetCritChance(DamageClass.Generic);
    this.damageStat = ((StatModifier) ref this.Player.GetDamage(DamageClass.Generic)).ApplyTo(1f);
    if (this.classIndex != 0 && this.classIndex == 1)
      return;
    int num1 = this.classIndex == 0 ? this.Player.HeldItem.DamageType.Type : this.classIndex;
    int num2 = num1 - (num1 == 3 || num1 == 7 ? 1 : 0);
    this.damageStat += ((StatModifier) ref this.Player.GetDamage(DamageClassLoader.GetDamageClass(num2))).ApplyTo(1f) - 1f;
    this.critChanceStat += this.Player.GetCritChance(DamageClassLoader.GetDamageClass(num2));
    this.armorPenetrationStat += this.Player.GetArmorPenetration(DamageClassLoader.GetDamageClass(num2));
  }

  public void Reset()
  {
    this.battalionLog = false;
    this.harmInducer = false;
    this.headCounter = false;
    this.kettlebell = false;
    this.luckyDie = false;
    this.metallicClover = false;
    this.plateCracker = false;
    this.regenerator = false;
    this.reinforcedPanel = false;
    this.replenisher = false;
    this.trackingDevice = false;
    this.wingTimer = false;
    this.anglerRadar = false;
    this.deteriorationDisplay = false;
    this.skullWatch = false;
  }
}
