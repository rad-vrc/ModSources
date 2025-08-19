using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.PlayerChanges
{
	// Token: 0x02000233 RID: 563
	public class VeinMiningPlayer : ModPlayer
	{
		// Token: 0x06000D80 RID: 3456 RVA: 0x00068C77 File Offset: 0x00066E77
		public override void Initialize()
		{
			this.CanMine = true;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00068C80 File Offset: 0x00066E80
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x00068C88 File Offset: 0x00066E88
		public bool CanMine
		{
			get
			{
				return this._canMine;
			}
			set
			{
				this.cd = 60;
				this._canMine = value;
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00068C9C File Offset: 0x00066E9C
		public override void PreUpdate()
		{
			VeinMiningPlayer.MiningSpeed = QoLCompendium.mainConfig.VeinMinerSpeed;
			MethodInfo GetPickaxeDamage = typeof(Player).GetMethod("GetPickaxeDamage", BindingFlags.Instance | BindingFlags.NonPublic);
			this.cd--;
			this.mcd--;
			if (this.cd == 0)
			{
				this.CanMine = true;
			}
			Point16 tile;
			double num;
			if (this.mcd <= 0 && this.picks.TryDequeue(ref tile, ref num))
			{
				short x = tile.X;
				short y = tile.Y;
				int dmg = (int)GetPickaxeDamage.Invoke(base.Player, new object[]
				{
					x,
					y,
					this.pickPower,
					0,
					Main.tile[(int)x, (int)y]
				});
				if (!WorldGen.CanKillTile((int)x, (int)y))
				{
					dmg = 0;
				}
				if (dmg != 0)
				{
					WorldGen.KillTile((int)tile.X, (int)tile.Y, false, false, false);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(17, -1, -1, null, 0, (float)tile.X, (float)tile.Y, 0f, 0, 0, 0);
					}
					this.mcd = VeinMiningPlayer.MiningSpeed;
				}
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00068DDC File Offset: 0x00066FDC
		public void QueueTile(int x, int y)
		{
			float prio = base.Player.Distance(new Vector2((float)(x * 16), (float)(y * 16)));
			this.picks.Enqueue(new Point16(x, y), (double)prio);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00068E18 File Offset: 0x00067018
		public bool NotInQueue(int x, int y)
		{
			return !VeinMiningPlayer.Contains<Point16, double>(this.picks, new Point16(x, y));
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00068E30 File Offset: 0x00067030
		private static bool Contains<T, U>(PriorityQueue<T, U> priorityQueue, T item)
		{
			return priorityQueue.UnorderedItems.Any(([TupleElementNames(new string[]
			{
				"Element",
				"Priority"
			})] ValueTuple<T, U> el) => el.Item1.Equals(item));
		}

		// Token: 0x0400059F RID: 1439
		public int ctr;

		// Token: 0x040005A0 RID: 1440
		private bool _canMine;

		// Token: 0x040005A1 RID: 1441
		private int cd;

		// Token: 0x040005A2 RID: 1442
		private int mcd;

		// Token: 0x040005A3 RID: 1443
		public static int MiningSpeed = QoLCompendium.mainConfig.VeinMinerSpeed;

		// Token: 0x040005A4 RID: 1444
		private readonly PriorityQueue<Point16, double> picks = new PriorityQueue<Point16, double>();

		// Token: 0x040005A5 RID: 1445
		public int pickPower;
	}
}
