using System;
using System.Collections.Generic;
using System.IO;
using Terraria.ID;

namespace Terraria.GameContent.Creative
{
	// Token: 0x020002C5 RID: 709
	public class ItemsSacrificedUnlocksTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x00542DC9 File Offset: 0x00540FC9
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x00542DD1 File Offset: 0x00540FD1
		public int LastEditId { get; private set; }

		// Token: 0x06002272 RID: 8818 RVA: 0x00542DDA File Offset: 0x00540FDA
		public ItemsSacrificedUnlocksTracker()
		{
			this._sacrificeCountByItemPersistentId = new Dictionary<string, int>();
			this._sacrificesCountByItemIdCache = new Dictionary<int, int>();
			this.LastEditId = 0;
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00542E00 File Offset: 0x00541000
		public int GetSacrificeCount(int itemId)
		{
			int num;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out num))
			{
				itemId = num;
			}
			int result;
			this._sacrificesCountByItemIdCache.TryGetValue(itemId, out result);
			return result;
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x00542E30 File Offset: 0x00541030
		public void FillListOfItemsThatCanBeObtainedInfinitely(List<int> toObtainInfinitely)
		{
			foreach (KeyValuePair<int, int> keyValuePair in this._sacrificesCountByItemIdCache)
			{
				int num;
				int num2;
				if (this.TryGetSacrificeNumbers(keyValuePair.Key, out num, out num2) && keyValuePair.Value >= num2)
				{
					toObtainInfinitely.Add(keyValuePair.Key);
				}
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00542EA8 File Offset: 0x005410A8
		public bool TryGetSacrificeNumbers(int itemId, out int amountWeHave, out int amountNeededTotal)
		{
			int num;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out num))
			{
				itemId = num;
			}
			amountWeHave = (amountNeededTotal = 0);
			if (!CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(itemId, out amountNeededTotal))
			{
				return false;
			}
			this._sacrificesCountByItemIdCache.TryGetValue(itemId, out amountWeHave);
			return true;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x00542EF0 File Offset: 0x005410F0
		public void RegisterItemSacrifice(int itemId, int amount)
		{
			int num;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out num))
			{
				itemId = num;
			}
			string key;
			if (!ContentSamples.ItemPersistentIdsByNetIds.TryGetValue(itemId, out key))
			{
				return;
			}
			int num2;
			this._sacrificeCountByItemPersistentId.TryGetValue(key, out num2);
			num2 += amount;
			int value = Utils.Clamp<int>(num2, 0, 9999);
			this._sacrificeCountByItemPersistentId[key] = value;
			this._sacrificesCountByItemIdCache[itemId] = value;
			this.MarkContentsDirty();
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00542F60 File Offset: 0x00541160
		public void SetSacrificeCountDirectly(string persistentId, int sacrificeCount)
		{
			int value = Utils.Clamp<int>(sacrificeCount, 0, 9999);
			this._sacrificeCountByItemPersistentId[persistentId] = value;
			int key;
			if (!ContentSamples.ItemNetIdsByPersistentIds.TryGetValue(persistentId, out key))
			{
				return;
			}
			this._sacrificesCountByItemIdCache[key] = value;
			this.MarkContentsDirty();
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00542FAC File Offset: 0x005411AC
		public void Save(BinaryWriter writer)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(this._sacrificeCountByItemPersistentId);
			writer.Write(dictionary.Count);
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				writer.Write(keyValuePair.Key);
				writer.Write(keyValuePair.Value);
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x00543028 File Offset: 0x00541228
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string key = reader.ReadString();
				int value = reader.ReadInt32();
				int key2;
				if (ContentSamples.ItemNetIdsByPersistentIds.TryGetValue(key, out key2))
				{
					int num2;
					if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(key2, out num2))
					{
						key2 = num2;
					}
					this._sacrificesCountByItemIdCache[key2] = value;
					string text;
					if (ContentSamples.ItemPersistentIdsByNetIds.TryGetValue(key2, out text))
					{
						key = text;
					}
				}
				this._sacrificeCountByItemPersistentId[key] = value;
			}
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x005430A8 File Offset: 0x005412A8
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
				reader.ReadInt32();
			}
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x005430D6 File Offset: 0x005412D6
		public void Reset()
		{
			this._sacrificeCountByItemPersistentId.Clear();
			this._sacrificesCountByItemIdCache.Clear();
			this.MarkContentsDirty();
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void OnPlayerJoining(int playerIndex)
		{
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x005430F4 File Offset: 0x005412F4
		public void MarkContentsDirty()
		{
			int lastEditId = this.LastEditId;
			this.LastEditId = lastEditId + 1;
		}

		// Token: 0x040047D3 RID: 18387
		public const int POSITIVE_SACRIFICE_COUNT_CAP = 9999;

		// Token: 0x040047D4 RID: 18388
		private Dictionary<string, int> _sacrificeCountByItemPersistentId;

		// Token: 0x040047D5 RID: 18389
		private Dictionary<int, int> _sacrificesCountByItemIdCache;
	}
}
