// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.NoLavaFromSlimes
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class NoLavaFromSlimes : GlobalNPC
{
  public virtual void HitEffect(NPC npc, NPC.HitInfo hit)
  {
    if (npc.type != 59 || Main.netMode == 1)
      return;
    if (npc.life > 0)
      return;
    try
    {
      if (!QoLCompendium.QoLCompendium.mainConfig.LavaSlimesDontDropLava)
        return;
      int num1 = (int) ((double) ((Entity) npc).Center.X / 16.0);
      int num2 = (int) ((double) ((Entity) npc).Center.Y / 16.0);
      if (WorldGen.SolidTile(num1, num2, false))
        return;
      Tile tile1 = ((Tilemap) ref Main.tile)[num1, num2];
      if (!((Tile) ref tile1).CheckingLiquid)
        return;
      Tile tile2 = ((Tilemap) ref Main.tile)[num1, num2];
      ((Tile) ref tile2).LiquidAmount = (byte) 0;
      WorldGen.SquareTileFrame(num1, num2, true);
    }
    catch
    {
    }
  }
}
