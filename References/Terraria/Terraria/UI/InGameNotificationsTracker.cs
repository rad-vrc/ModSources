using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Achievements;
using Terraria.GameInput;
using Terraria.Social;
using Terraria.Social.Base;

namespace Terraria.UI
{
	// Token: 0x0200008E RID: 142
	public class InGameNotificationsTracker
	{
		// Token: 0x0600122D RID: 4653 RVA: 0x00491464 File Offset: 0x0048F664
		public static void Initialize()
		{
			Main.Achievements.OnAchievementCompleted += InGameNotificationsTracker.AddCompleted;
			SocialAPI.JoinRequests.OnRequestAdded += InGameNotificationsTracker.JoinRequests_OnRequestAdded;
			SocialAPI.JoinRequests.OnRequestRemoved += InGameNotificationsTracker.JoinRequests_OnRequestRemoved;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x004914B3 File Offset: 0x0048F6B3
		private static void JoinRequests_OnRequestAdded(UserJoinToServerRequest request)
		{
			InGameNotificationsTracker.AddJoinRequest(request);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x004914BC File Offset: 0x0048F6BC
		private static void JoinRequests_OnRequestRemoved(UserJoinToServerRequest request)
		{
			for (int i = InGameNotificationsTracker._notifications.Count - 1; i >= 0; i--)
			{
				if (InGameNotificationsTracker._notifications[i].CreationObject == request)
				{
					InGameNotificationsTracker._notifications.RemoveAt(i);
				}
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00491500 File Offset: 0x0048F700
		public static void DrawInGame(SpriteBatch sb)
		{
			float num = (float)(Main.screenHeight - 40);
			if (PlayerInput.UsingGamepad)
			{
				num -= 25f;
			}
			Vector2 vector = new Vector2((float)(Main.screenWidth / 2), num);
			foreach (IInGameNotification inGameNotification in InGameNotificationsTracker._notifications)
			{
				inGameNotification.DrawInGame(sb, vector);
				inGameNotification.PushAnchor(ref vector);
				if (vector.Y < -100f)
				{
					break;
				}
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00491594 File Offset: 0x0048F794
		public static void DrawInIngameOptions(SpriteBatch spriteBatch, Rectangle area, ref int gamepadPointIdLocalIndexToUse)
		{
			int num = 4;
			int num2 = area.Height / 5 - num;
			Rectangle area2 = new Rectangle(area.X, area.Y, area.Width - 6, num2);
			int num3 = 0;
			foreach (IInGameNotification inGameNotification in InGameNotificationsTracker._notifications)
			{
				inGameNotification.DrawInNotificationsArea(spriteBatch, area2, ref gamepadPointIdLocalIndexToUse);
				area2.Y += num2 + num;
				num3++;
				if (num3 >= 5)
				{
					break;
				}
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0049162C File Offset: 0x0048F82C
		public static void AddCompleted(Achievement achievement)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			InGameNotificationsTracker._notifications.Add(new InGamePopups.AchievementUnlockedPopup(achievement));
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00491647 File Offset: 0x0048F847
		public static void AddJoinRequest(UserJoinToServerRequest request)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			InGameNotificationsTracker._notifications.Add(new InGamePopups.PlayerWantsToJoinGamePopup(request));
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00491662 File Offset: 0x0048F862
		public static void Clear()
		{
			InGameNotificationsTracker._notifications.Clear();
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00491670 File Offset: 0x0048F870
		public static void Update()
		{
			for (int i = 0; i < InGameNotificationsTracker._notifications.Count; i++)
			{
				InGameNotificationsTracker._notifications[i].Update();
				if (InGameNotificationsTracker._notifications[i].ShouldBeRemoved)
				{
					InGameNotificationsTracker._notifications.Remove(InGameNotificationsTracker._notifications[i]);
					i--;
				}
			}
		}

		// Token: 0x04001012 RID: 4114
		private static List<IInGameNotification> _notifications = new List<IInGameNotification>();
	}
}
