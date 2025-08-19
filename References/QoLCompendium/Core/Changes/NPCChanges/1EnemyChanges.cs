// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.SpawnRateEdits
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class SpawnRateEdits : GlobalNPC
{
  public static int boss = -1;

  public virtual void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
  {
    if (SpawnRateEdits.AnyBossAlive() && (double) ((Entity) player).Distance(((Entity) Main.npc[SpawnRateEdits.boss]).Center) < 6000.0 && QoLCompendium.QoLCompendium.mainConfig.NoSpawnsDuringBosses)
      maxSpawns = 0;
    if (player.GetModPlayer<QoLCPlayer>().noSpawns)
      maxSpawns = 0;
    if (player.GetModPlayer<QoLCPlayer>().increasedSpawns)
    {
      spawnRate = (int) ((double) spawnRate * 0.1);
      maxSpawns = (int) ((double) maxSpawns * 10.0);
    }
    if (!player.GetModPlayer<QoLCPlayer>().decreasedSpawns)
      return;
    spawnRate = (int) ((double) spawnRate * 2.5);
    maxSpawns = (int) ((double) maxSpawns * 0.30000001192092896);
  }

  public virtual bool PreAI(NPC npc)
  {
    if (npc.boss)
      SpawnRateEdits.boss = ((Entity) npc).whoAmI;
    return true;
  }

  public static bool AnyBossAlive()
  {
    if (SpawnRateEdits.boss == -1)
      return false;
    NPC npc = Main.npc[SpawnRateEdits.boss];
    if (((Entity) npc).active && npc.type != 395 && (npc.boss || npc.type == 13))
      return true;
    SpawnRateEdits.boss = -1;
    return false;
  }
}
