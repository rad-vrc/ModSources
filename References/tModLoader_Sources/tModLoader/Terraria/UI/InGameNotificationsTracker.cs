using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Achievements;
using Terraria.GameInput;
using Terraria.Social;
using Terraria.Social.Base;

namespace Terraria.UI
{
	// Token: 0x020000A7 RID: 167
	public class InGameNotificationsTracker
	{
		// Token: 0x06001508 RID: 5384 RVA: 0x004A70D0 File Offset: 0x004A52D0
		public static void Initialize()
		{
			AchievementManager achievements = Main.Achievements;
			Achievement.AchievementCompleted value;
			if ((value = InGameNotificationsTracker.<>O.<0>__AddCompleted) == null)
			{
				value = (InGameNotificationsTracker.<>O.<0>__AddCompleted = new Achievement.AchievementCompleted(InGameNotificationsTracker.AddCompleted));
			}
			achievements.OnAchievementCompleted += value;
			ServerJoinRequestsManager joinRequests = SocialAPI.JoinRequests;
			ServerJoinRequestEvent value2;
			if ((value2 = InGameNotificationsTracker.<>O.<1>__JoinRequests_OnRequestAdded) == null)
			{
				value2 = (InGameNotificationsTracker.<>O.<1>__JoinRequests_OnRequestAdded = new ServerJoinRequestEvent(InGameNotificationsTracker.JoinRequests_OnRequestAdded));
			}
			joinRequests.OnRequestAdded += value2;
			ServerJoinRequestsManager joinRequests2 = SocialAPI.JoinRequests;
			ServerJoinRequestEvent value3;
			if ((value3 = InGameNotificationsTracker.<>O.<2>__JoinRequests_OnRequestRemoved) == null)
			{
				value3 = (InGameNotificationsTracker.<>O.<2>__JoinRequests_OnRequestRemoved = new ServerJoinRequestEvent(InGameNotificationsTracker.JoinRequests_OnRequestRemoved));
			}
			joinRequests2.OnRequestRemoved += value3;
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x004A714C File Offset: 0x004A534C
		private static void JoinRequests_OnRequestAdded(UserJoinToServerRequest request)
		{
			InGameNotificationsTracker.AddJoinRequest(request);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x004A7154 File Offset: 0x004A5354
		private static void JoinRequests_OnRequestRemoved(UserJoinToServerRequest request)
		{
			for (int num = InGameNotificationsTracker._notifications.Count - 1; num >= 0; num--)
			{
				InGamePopups.PlayerWantsToJoinGamePopup joinReq = InGameNotificationsTracker._notifications[num] as InGamePopups.PlayerWantsToJoinGamePopup;
				if (joinReq != null && joinReq.CreationObject == request)
				{
					InGameNotificationsTracker._notifications.RemoveAt(num);
				}
			}
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x004A71A0 File Offset: 0x004A53A0
		public static void DrawInGame(SpriteBatch sb)
		{
			float num = (float)(Main.screenHeight - 40);
			if (PlayerInput.UsingGamepad)
			{
				num -= 25f;
			}
			Vector2 positionAnchorBottom;
			positionAnchorBottom..ctor((float)(Main.screenWidth / 2), num);
			foreach (IInGameNotification inGameNotification in InGameNotificationsTracker._notifications)
			{
				inGameNotification.DrawInGame(sb, positionAnchorBottom);
				inGameNotification.PushAnchor(ref positionAnchorBottom);
				if (positionAnchorBottom.Y < -100f)
				{
					break;
				}
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x004A7234 File Offset: 0x004A5434
		public static void AddCompleted(Achievement achievement)
		{
			if (Main.netMode != 2)
			{
				InGameNotificationsTracker._notifications.Add(new InGamePopups.AchievementUnlockedPopup(achievement));
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x004A724E File Offset: 0x004A544E
		public static void AddJoinRequest(UserJoinToServerRequest request)
		{
			if (Main.netMode != 2)
			{
				InGameNotificationsTracker._notifications.Add(new InGamePopups.PlayerWantsToJoinGamePopup(request));
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x004A7268 File Offset: 0x004A5468
		public static void Clear()
		{
			InGameNotificationsTracker._notifications.Clear();
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x004A7274 File Offset: 0x004A5474
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

		/// <summary>
		/// Adds an in-game notification to the tracker.
		/// </summary>
		/// <param name="notification">The notification to add.</param>
		// Token: 0x06001510 RID: 5392 RVA: 0x004A72D2 File Offset: 0x004A54D2
		public static void AddNotification(IInGameNotification notification)
		{
			InGameNotificationsTracker._notifications.Add(notification);
		}

		// Token: 0x040010E3 RID: 4323
		private static List<IInGameNotification> _notifications = new List<IInGameNotification>();

		// Token: 0x02000866 RID: 2150
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400691C RID: 26908
			public static Achievement.AchievementCompleted <0>__AddCompleted;

			// Token: 0x0400691D RID: 26909
			public static ServerJoinRequestEvent <1>__JoinRequests_OnRequestAdded;

			// Token: 0x0400691E RID: 26910
			public static ServerJoinRequestEvent <2>__JoinRequests_OnRequestRemoved;
		}
	}
}
