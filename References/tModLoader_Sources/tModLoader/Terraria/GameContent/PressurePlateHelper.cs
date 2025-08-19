using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x020004B0 RID: 1200
	public class PressurePlateHelper
	{
		// Token: 0x060039BB RID: 14779 RVA: 0x0059B63C File Offset: 0x0059983C
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

		// Token: 0x060039BC RID: 14780 RVA: 0x0059B6A8 File Offset: 0x005998A8
		public static void Reset()
		{
			PressurePlateHelper.PressurePlatesPressed.Clear();
			for (int i = 0; i < PressurePlateHelper.PlayerLastPosition.Length; i++)
			{
				PressurePlateHelper.PlayerLastPosition[i] = Vector2.Zero;
			}
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x0059B6E4 File Offset: 0x005998E4
		public static void ResetPlayer(int player)
		{
			Point[] array = PressurePlateHelper.PressurePlatesPressed.Keys.ToArray<Point>();
			for (int i = 0; i < array.Length; i++)
			{
				PressurePlateHelper.MoveAwayFrom(array[i], player);
			}
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x0059B71C File Offset: 0x0059991C
		public unsafe static void UpdatePlayerPosition(Player player)
		{
			Point p;
			p..ctor(1, 1);
			Vector2 vector = p.ToVector2();
			List<Point> tilesIn = Collision.GetTilesIn(PressurePlateHelper.PlayerLastPosition[player.whoAmI] + vector, PressurePlateHelper.PlayerLastPosition[player.whoAmI] + player.Size - vector * 2f);
			List<Point> tilesIn2 = Collision.GetTilesIn(player.TopLeft + vector, player.BottomRight - vector * 2f);
			Rectangle hitbox = player.Hitbox;
			Rectangle hitbox2 = player.Hitbox;
			hitbox.Inflate(-p.X, -p.Y);
			hitbox2.Inflate(-p.X, -p.Y);
			hitbox2.X = (int)PressurePlateHelper.PlayerLastPosition[player.whoAmI].X;
			hitbox2.Y = (int)PressurePlateHelper.PlayerLastPosition[player.whoAmI].Y;
			for (int i = 0; i < tilesIn.Count; i++)
			{
				Point point = tilesIn[i];
				Tile tile = Main.tile[point.X, point.Y];
				if (tile.active() && *tile.type == 428)
				{
					PressurePlateHelper.pressurePlateBounds.X = point.X * 16;
					PressurePlateHelper.pressurePlateBounds.Y = point.Y * 16 + 16 - PressurePlateHelper.pressurePlateBounds.Height;
					if (!hitbox.Intersects(PressurePlateHelper.pressurePlateBounds) && !tilesIn2.Contains(point))
					{
						PressurePlateHelper.MoveAwayFrom(point, player.whoAmI);
					}
				}
			}
			for (int j = 0; j < tilesIn2.Count; j++)
			{
				Point point2 = tilesIn2[j];
				Tile tile2 = Main.tile[point2.X, point2.Y];
				if (tile2.active() && *tile2.type == 428)
				{
					PressurePlateHelper.pressurePlateBounds.X = point2.X * 16;
					PressurePlateHelper.pressurePlateBounds.Y = point2.Y * 16 + 16 - PressurePlateHelper.pressurePlateBounds.Height;
					if (hitbox.Intersects(PressurePlateHelper.pressurePlateBounds) && (!tilesIn.Contains(point2) || !hitbox2.Intersects(PressurePlateHelper.pressurePlateBounds)))
					{
						PressurePlateHelper.MoveInto(point2, player.whoAmI);
					}
				}
			}
			PressurePlateHelper.PlayerLastPosition[player.whoAmI] = player.position;
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x0059B9A4 File Offset: 0x00599BA4
		public static void DestroyPlate(Point location)
		{
			bool[] array;
			if (PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out array))
			{
				PressurePlateHelper.PressurePlatesPressed.Remove(location);
				PressurePlateHelper.PokeLocation(location);
			}
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x0059B9D2 File Offset: 0x00599BD2
		private static void UpdatePlatePosition(Point location, int player, bool onIt)
		{
			if (onIt)
			{
				PressurePlateHelper.MoveInto(location, player);
				return;
			}
			PressurePlateHelper.MoveAwayFrom(location, player);
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x0059B9E8 File Offset: 0x00599BE8
		private static void MoveInto(Point location, int player)
		{
			bool[] value;
			if (PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out value))
			{
				value[player] = true;
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

		// Token: 0x060039C2 RID: 14786 RVA: 0x0059BA60 File Offset: 0x00599C60
		private static void MoveAwayFrom(Point location, int player)
		{
			bool[] value;
			if (!PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out value))
			{
				return;
			}
			value[player] = false;
			bool flag = false;
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i])
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

		// Token: 0x060039C3 RID: 14787 RVA: 0x0059BAE0 File Offset: 0x00599CE0
		private static void PokeLocation(Point location)
		{
			if (Main.netMode != 1)
			{
				Wiring.blockPlayerTeleportationForOneIteration = true;
				Wiring.HitSwitch(location.X, location.Y);
				NetMessage.SendData(59, -1, -1, null, location.X, (float)location.Y, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0400527D RID: 21117
		public static object EntityCreationLock = new object();

		// Token: 0x0400527E RID: 21118
		public static Dictionary<Point, bool[]> PressurePlatesPressed = new Dictionary<Point, bool[]>();

		// Token: 0x0400527F RID: 21119
		public static bool NeedsFirstUpdate;

		// Token: 0x04005280 RID: 21120
		private static Vector2[] PlayerLastPosition = new Vector2[255];

		// Token: 0x04005281 RID: 21121
		private static Rectangle pressurePlateBounds = new Rectangle(0, 0, 16, 10);
	}
}
