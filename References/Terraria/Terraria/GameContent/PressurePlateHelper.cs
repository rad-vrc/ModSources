using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x02000202 RID: 514
	public class PressurePlateHelper
	{
		// Token: 0x06001D52 RID: 7506 RVA: 0x00501DC0 File Offset: 0x004FFFC0
		public static void Update()
		{
			if (!PressurePlateHelper.NeedsFirstUpdate)
			{
				return;
			}
			foreach (Point location in PressurePlateHelper.PressurePlatesPressed.Keys)
			{
				PressurePlateHelper.PokeLocation(location);
			}
			PressurePlateHelper.PressurePlatesPressed.Clear();
			PressurePlateHelper.NeedsFirstUpdate = false;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00501E2C File Offset: 0x0050002C
		public static void Reset()
		{
			PressurePlateHelper.PressurePlatesPressed.Clear();
			for (int i = 0; i < PressurePlateHelper.PlayerLastPosition.Length; i++)
			{
				PressurePlateHelper.PlayerLastPosition[i] = Vector2.Zero;
			}
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00501E68 File Offset: 0x00500068
		public static void ResetPlayer(int player)
		{
			Point[] array = PressurePlateHelper.PressurePlatesPressed.Keys.ToArray<Point>();
			for (int i = 0; i < array.Length; i++)
			{
				PressurePlateHelper.MoveAwayFrom(array[i], player);
			}
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00501EA0 File Offset: 0x005000A0
		public static void UpdatePlayerPosition(Player player)
		{
			Point point = new Point(1, 1);
			Vector2 vector = point.ToVector2();
			List<Point> tilesIn = Collision.GetTilesIn(PressurePlateHelper.PlayerLastPosition[player.whoAmI] + vector, PressurePlateHelper.PlayerLastPosition[player.whoAmI] + player.Size - vector * 2f);
			List<Point> tilesIn2 = Collision.GetTilesIn(player.TopLeft + vector, player.BottomRight - vector * 2f);
			Rectangle hitbox = player.Hitbox;
			Rectangle hitbox2 = player.Hitbox;
			hitbox.Inflate(-point.X, -point.Y);
			hitbox2.Inflate(-point.X, -point.Y);
			hitbox2.X = (int)PressurePlateHelper.PlayerLastPosition[player.whoAmI].X;
			hitbox2.Y = (int)PressurePlateHelper.PlayerLastPosition[player.whoAmI].Y;
			for (int i = 0; i < tilesIn.Count; i++)
			{
				Point point2 = tilesIn[i];
				Tile tile = Main.tile[point2.X, point2.Y];
				if (tile.active() && tile.type == 428)
				{
					PressurePlateHelper.pressurePlateBounds.X = point2.X * 16;
					PressurePlateHelper.pressurePlateBounds.Y = point2.Y * 16 + 16 - PressurePlateHelper.pressurePlateBounds.Height;
					if (!hitbox.Intersects(PressurePlateHelper.pressurePlateBounds) && !tilesIn2.Contains(point2))
					{
						PressurePlateHelper.MoveAwayFrom(point2, player.whoAmI);
					}
				}
			}
			for (int j = 0; j < tilesIn2.Count; j++)
			{
				Point point3 = tilesIn2[j];
				Tile tile2 = Main.tile[point3.X, point3.Y];
				if (tile2.active() && tile2.type == 428)
				{
					PressurePlateHelper.pressurePlateBounds.X = point3.X * 16;
					PressurePlateHelper.pressurePlateBounds.Y = point3.Y * 16 + 16 - PressurePlateHelper.pressurePlateBounds.Height;
					if (hitbox.Intersects(PressurePlateHelper.pressurePlateBounds) && (!tilesIn.Contains(point3) || !hitbox2.Intersects(PressurePlateHelper.pressurePlateBounds)))
					{
						PressurePlateHelper.MoveInto(point3, player.whoAmI);
					}
				}
			}
			PressurePlateHelper.PlayerLastPosition[player.whoAmI] = player.position;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x00502128 File Offset: 0x00500328
		public static void DestroyPlate(Point location)
		{
			bool[] array;
			if (PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out array))
			{
				PressurePlateHelper.PressurePlatesPressed.Remove(location);
				PressurePlateHelper.PokeLocation(location);
			}
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00502156 File Offset: 0x00500356
		private static void UpdatePlatePosition(Point location, int player, bool onIt)
		{
			if (onIt)
			{
				PressurePlateHelper.MoveInto(location, player);
				return;
			}
			PressurePlateHelper.MoveAwayFrom(location, player);
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0050216C File Offset: 0x0050036C
		private static void MoveInto(Point location, int player)
		{
			bool[] array;
			if (PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out array))
			{
				array[player] = true;
				return;
			}
			object entityCreationLock = PressurePlateHelper.EntityCreationLock;
			lock (entityCreationLock)
			{
				PressurePlateHelper.PressurePlatesPressed[location] = new bool[255];
			}
			PressurePlateHelper.PressurePlatesPressed[location][player] = true;
			PressurePlateHelper.PokeLocation(location);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x005021E4 File Offset: 0x005003E4
		private static void MoveAwayFrom(Point location, int player)
		{
			bool[] array;
			if (PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out array))
			{
				array[player] = false;
				bool flag = false;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					object entityCreationLock = PressurePlateHelper.EntityCreationLock;
					lock (entityCreationLock)
					{
						PressurePlateHelper.PressurePlatesPressed.Remove(location);
					}
					PressurePlateHelper.PokeLocation(location);
				}
			}
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00502260 File Offset: 0x00500460
		private static void PokeLocation(Point location)
		{
			if (Main.netMode != 1)
			{
				Wiring.blockPlayerTeleportationForOneIteration = true;
				Wiring.HitSwitch(location.X, location.Y);
				NetMessage.SendData(59, -1, -1, null, location.X, (float)location.Y, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0400453B RID: 17723
		public static object EntityCreationLock = new object();

		// Token: 0x0400453C RID: 17724
		public static Dictionary<Point, bool[]> PressurePlatesPressed = new Dictionary<Point, bool[]>();

		// Token: 0x0400453D RID: 17725
		public static bool NeedsFirstUpdate;

		// Token: 0x0400453E RID: 17726
		private static Vector2[] PlayerLastPosition = new Vector2[255];

		// Token: 0x0400453F RID: 17727
		private static Rectangle pressurePlateBounds = new Rectangle(0, 0, 16, 10);
	}
}
