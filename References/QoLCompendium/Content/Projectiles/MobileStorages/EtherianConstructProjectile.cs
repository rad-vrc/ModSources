// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.MobileStorages.EtherianConstructProjectile
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.MobileStorages;
using QoLCompendium.Core.Changes.PlayerChanges;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.MobileStorages;

public class EtherianConstructProjectile : ModProjectile
{
  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 22;
    ((Entity) this.Projectile).height = 36;
    this.Projectile.aiStyle = 97;
    this.Projectile.tileCollide = false;
    this.Projectile.timeLeft = 10800;
  }

  public virtual void DrawBehind(
    int index,
    List<int> drawCacheProjsBehindNPCsAndTiles,
    List<int> drawCacheProjsBehindNPCs,
    List<int> drawCacheProjsBehindProjectiles,
    List<int> drawCacheProjsOverPlayers,
    List<int> drawCacheProjsOverWiresUI)
  {
    drawCacheProjsOverWiresUI.Add(((Entity) this.Projectile).whoAmI);
  }

  public virtual void PostAI()
  {
    if (Main.netMode == 2)
      return;
    Player player = Main.player[Main.myPlayer];
    BankPlayer modPlayer = player.GetModPlayer<BankPlayer>();
    PortableBankAI.BankAI(this.Projectile, ModContent.ItemType<EtherianConstruct>(), -4, ref modPlayer.defenders, player, modPlayer);
  }
}
