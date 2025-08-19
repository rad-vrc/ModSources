using System;
using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x0200020E RID: 526
	public class TileDestroyedCondition : AchievementCondition
	{
		// Token: 0x06001DE0 RID: 7648 RVA: 0x005072F8 File Offset: 0x005054F8
		private TileDestroyedCondition(ushort[] tileIds) : base("TILE_DESTROYED_" + tileIds[0])
		{
			this._tileIds = tileIds;
			TileDestroyedCondition.ListenForDestruction(this);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00507320 File Offset: 0x00505520
		private static void ListenForDestruction(TileDestroyedCondition condition)
		{
			if (!TileDestroyedCondition._isListenerHooked)
			{
				AchievementsHelper.OnTileDestroyed += TileDestroyedCondition.TileDestroyedListener;
				TileDestroyedCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._tileIds.Length; i++)
			{
				if (!TileDestroyedCondition._listeners.ContainsKey(condition._tileIds[i]))
				{
					TileDestroyedCondition._listeners[condition._tileIds[i]] = new List<TileDestroyedCondition>();
				}
				TileDestroyedCondition._listeners[condition._tileIds[i]].Add(condition);
			}
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x005073A4 File Offset: 0x005055A4
		private static void TileDestroyedListener(Player player, ushort tileId)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			if (TileDestroyedCondition._listeners.ContainsKey(tileId))
			{
				foreach (TileDestroyedCondition tileDestroyedCondition in TileDestroyedCondition._listeners[tileId])
				{
					tileDestroyedCondition.Complete();
				}
			}
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00507414 File Offset: 0x00505614
		public static AchievementCondition Create(params ushort[] tileIds)
		{
			return new TileDestroyedCondition(tileIds);
		}

		// Token: 0x0400457E RID: 17790
		private const string Identifier = "TILE_DESTROYED";

		// Token: 0x0400457F RID: 17791
		private static Dictionary<ushort, List<TileDestroyedCondition>> _listeners = new Dictionary<ushort, List<TileDestroyedCondition>>();

		// Token: 0x04004580 RID: 17792
		private static bool _isListenerHooked;

		// Token: 0x04004581 RID: 17793
		private ushort[] _tileIds;
	}
}
