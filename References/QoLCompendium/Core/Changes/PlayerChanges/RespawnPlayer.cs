// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.PlayerChanges.RespawnPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.PlayerChanges;

public class RespawnPlayer : ModPlayer
{
  private (int type, int time)[] buffCache;

  public virtual void Kill(
    double damage,
    int hitDirection,
    bool pvp,
    PlayerDeathReason damageSource)
  {
    if (QoLCompendium.QoLCompendium.mainConfig.InstantRespawn && Main.netMode == 0)
    {
      for (int npc = 0; npc < Main.maxNPCs; ++npc)
      {
        if (!Main.npc[npc].friendly && ((Entity) Main.npc[npc]).active && !Common.LunarPillarIDs.Contains(Main.npc[npc].type))
          RespawnPlayer.DespawnNPC(npc);
        this.Player.respawnTimer = 60;
      }
    }
    if (!QoLCompendium.QoLCompendium.mainConfig.KeepBuffsOnDeath)
      return;
    if (this.buffCache == null)
      this.buffCache = new (int, int)[Player.MaxBuffs];
    for (int index = 0; index < Player.MaxBuffs; ++index)
      this.buffCache[index] = (this.Player.buffType[index], this.Player.buffTime[index]);
  }

  public virtual void OnRespawn()
  {
    if (QoLCompendium.QoLCompendium.mainConfig.FullHealthRespawn)
    {
      this.Player.statLife = this.Player.statLifeMax2;
      this.Player.statMana = this.Player.statManaMax2;
    }
    if (!QoLCompendium.QoLCompendium.mainConfig.KeepBuffsOnDeath)
      return;
    foreach ((int type, int time) in this.buffCache)
    {
      if ((QoLCompendium.QoLCompendium.mainConfig.KeepDebuffsOnDeath || !Main.debuff[type]) && type > 0 && !Main.persistentBuff[type] && time > 2)
      {
        int num = (int) (float) time;
        this.Player.AddBuff(type, num, false, false);
      }
    }
  }

  public static void DespawnNPC(int npc)
  {
    Main.npc[npc].life = 0;
    ((Entity) Main.npc[npc]).active = false;
    if (Main.netMode != 2)
      return;
    NetMessage.SendData(23, -1, -1, (NetworkText) null, npc, 0.0f, 0.0f, 0.0f, 0, 0, 0);
  }
}
