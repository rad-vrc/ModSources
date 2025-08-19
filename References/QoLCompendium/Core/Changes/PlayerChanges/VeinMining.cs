// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.PlayerChanges.VeinMiningPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.PlayerChanges;

public class VeinMiningPlayer : ModPlayer
{
  public int ctr;
  private bool _canMine;
  private int cd;
  private int mcd;
  public static int MiningSpeed = QoLCompendium.QoLCompendium.mainConfig.VeinMinerSpeed;
  private readonly PriorityQueue<Point16, double> picks = new PriorityQueue<Point16, double>();
  public int pickPower;

  public virtual void Initialize() => this.CanMine = true;

  public bool CanMine
  {
    get => this._canMine;
    set
    {
      this.cd = 60;
      this._canMine = value;
    }
  }

  public virtual void PreUpdate()
  {
    VeinMiningPlayer.MiningSpeed = QoLCompendium.QoLCompendium.mainConfig.VeinMinerSpeed;
    MethodInfo method = typeof (Player).GetMethod("GetPickaxeDamage", (BindingFlags) 36);
    --this.cd;
    --this.mcd;
    if (this.cd == 0)
      this.CanMine = true;
    Point16 point16;
    double num1;
    if (this.mcd > 0 || !this.picks.TryDequeue(ref point16, ref num1))
      return;
    short x = point16.X;
    short y = point16.Y;
    int num2 = (int) ((MethodBase) method).Invoke((object) this.Player, new object[5]
    {
      (object) x,
      (object) y,
      (object) this.pickPower,
      (object) 0,
      (object) ((Tilemap) ref Main.tile)[(int) x, (int) y]
    });
    if (!WorldGen.CanKillTile((int) x, (int) y))
      num2 = 0;
    if (num2 == 0)
      return;
    WorldGen.KillTile((int) point16.X, (int) point16.Y, false, false, false);
    if (Main.netMode == 1)
      NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) point16.X, (float) point16.Y, 0.0f, 0, 0, 0);
    this.mcd = VeinMiningPlayer.MiningSpeed;
  }

  public void QueueTile(int x, int y)
  {
    float num = ((Entity) this.Player).Distance(new Vector2((float) (x * 16 /*0x10*/), (float) (y * 16 /*0x10*/)));
    this.picks.Enqueue(new Point16(x, y), (double) num);
  }

  public bool NotInQueue(int x, int y)
  {
    return !VeinMiningPlayer.Contains<Point16, double>(this.picks, new Point16(x, y));
  }

  private static bool Contains<T, U>(PriorityQueue<T, U> priorityQueue, T item)
  {
    return ((IEnumerable<(T, U)>) priorityQueue.UnorderedItems).Any<(T, U)>((Func<(T, U), bool>) (el => el.Element.Equals((object) (T) item)));
  }
}
