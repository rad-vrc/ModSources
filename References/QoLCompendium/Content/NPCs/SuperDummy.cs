// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.NPCs.SuperDummy
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core.Changes.NPCChanges;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.NPCs;

public class SuperDummy : ModNPC
{
  public virtual void SetDefaults()
  {
    this.NPC.CloneDefaults(488);
    this.NPC.lifeMax = int.MaxValue;
    this.NPC.aiStyle = -1;
    ((Entity) this.NPC).width = 28;
    ((Entity) this.NPC).height = 50;
    this.NPC.immortal = false;
    this.NPC.npcSlots = 0.0f;
    this.NPC.dontCountMe = true;
    this.NPC.noGravity = true;
  }

  public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
  {
    return new bool?(false);
  }

  public virtual void OnSpawn(IEntitySource source)
  {
    this.NPC.life = this.NPC.lifeMax = int.MaxValue;
  }

  public virtual void AI()
  {
    this.NPC.life = this.NPC.lifeMax = int.MaxValue;
    if (!SpawnRateEdits.AnyBossAlive())
      return;
    this.NPC.life = 0;
    this.NPC.HitEffect(0, 10.0, new bool?());
    this.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
  }

  public virtual bool CheckDead() => false;
}
