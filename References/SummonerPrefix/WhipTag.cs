// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.WhipTag
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

#nullable disable
namespace SummonerPrefix;

public class WhipTag
{
  public string SpecialType = "";
  public int TimeLeft;
  public string ID = "";
  public float CritAdd;
  public int TagDamage;
  public float TagDamageMult = 1f;

  public WhipTag(string id, int tagDmg, float crit = 0.0f, float dmgMult = 1f, int time = 300)
  {
    this.ID = id;
    this.TagDamage = tagDmg;
    this.CritAdd = crit;
    this.TagDamageMult = dmgMult;
    this.TimeLeft = time;
  }

  public WhipTag Clone()
  {
    return new WhipTag(this.ID, this.TagDamage, this.CritAdd, this.TagDamageMult, this.TimeLeft)
    {
      SpecialType = this.SpecialType
    };
  }
}
