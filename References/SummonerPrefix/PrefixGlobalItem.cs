// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.PrefixGlobalItem
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace SummonerPrefix;

public class PrefixGlobalItem : GlobalItem
{
  public float WhipRangeMult = 1f;
  public WhipTag wTag;
  public bool CanGiveTag;
  public float MinionSlotMult = 1f;
  public float MinionSpeedMult = 1f;
  public float MinionScaleMult = 1f;
  public float MinionKnockbackMult = 1f;
  public float MinionLifeSteal;
  public float MinionCritAdd;
  public bool electrified;

  public virtual bool InstancePerEntity => true;

  public virtual void NetSend(Item item, BinaryWriter writer) => writer.Write(this.WhipRangeMult);

  public virtual void NetReceive(Item item, BinaryReader reader)
  {
    this.WhipRangeMult = reader.ReadSingle();
  }

  public virtual void SetDefaults(Item entity)
  {
    this.CanGiveTag = false;
    this.wTag = (WhipTag) null;
    if (entity.shoot > 0 && ProjectileID.Sets.IsAWhip[entity.shoot])
      this.wTag = new WhipTag(entity.Name, 0);
    this.WhipRangeMult = 1f;
  }
}
