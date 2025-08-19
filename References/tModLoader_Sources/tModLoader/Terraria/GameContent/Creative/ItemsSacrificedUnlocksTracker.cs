using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.ID;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200064D RID: 1613
	public class ItemsSacrificedUnlocksTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600469C RID: 18076 RVA: 0x00632352 File Offset: 0x00630552
		// (set) Token: 0x0600469D RID: 18077 RVA: 0x0063235A File Offset: 0x0063055A
		public int LastEditId { get; private set; }

		// Token: 0x0600469E RID: 18078 RVA: 0x00632363 File Offset: 0x00630563
		public ItemsSacrificedUnlocksTracker()
		{
			this._sacrificeCountByItemPersistentId = new Dictionary<string, int>();
			this._sacrificesCountByItemIdCache = new Dictionary<int, int>();
			this.LastEditId = 0;
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x00632388 File Offset: 0x00630588
		public int GetSacrificeCount(int itemId)
		{
			int value;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out value))
			{
				itemId = value;
			}
			int value2;
			this._sacrificesCountByItemIdCache.TryGetValue(itemId, out value2);
			return value2;
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x006323B8 File Offset: 0x006305B8
		public void FillListOfItemsThatCanBeObtainedInfinitely(List<int> toObtainInfinitely)
		{
			foreach (KeyValuePair<int, int> item in this._sacrificesCountByItemIdCache)
			{
				int num;
				int amountNeededTotal;
				if (this.TryGetSacrificeNumbers(item.Key, out num, out amountNeededTotal) && item.Value >= amountNeededTotal)
				{
					toObtainInfinitely.Add(item.Key);
				}
			}
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x00632430 File Offset: 0x00630630
		public bool TryGetSacrificeNumbers(int itemId, out int amountWeHave, out int amountNeededTotal)
		{
			int value;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out value))
			{
				itemId = value;
			}
			amountWeHave = (amountNeededTotal = 0);
			if (!CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(itemId, out amountNeededTotal))
			{
				return false;
			}
			this._sacrificesCountByItemIdCache.TryGetValue(itemId, out amountWeHave);
			return true;
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x00632478 File Offset: 0x00630678
		public void RegisterItemSacrifice(int itemId, int amount)
		{
			int value;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out value))
			{
				itemId = value;
			}
			string value2;
			if (ContentSamples.ItemPersistentIdsByNetIds.TryGetValue(itemId, out value2))
			{
				int value3;
				this._sacrificeCountByItemPersistentId.TryGetValue(value2, out value3);
				value3 += amount;
				int value4 = Utils.Clamp<int>(value3, 0, 9999);
				this._sacrificeCountByItemPersistentId[value2] = value4;
				this._sacrificesCountByItemIdCache[itemId] = value4;
				this.MarkContentsDirty();
			}
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x006324E8 File Offset: 0x006306E8
		public void SetSacrificeCountDirectly(string persistentId, int sacrificeCount)
		{
			int value = Utils.Clamp<int>(sacrificeCount, 0, 9999);
			this._sacrificeCountByItemPersistentId[persistentId] = value;
			int value2;
			if (ContentSamples.ItemNetIdsByPersistentIds.TryGetValue(persistentId, out value2))
			{
				this._sacrificesCountByItemIdCache[value2] = value;
				this.MarkContentsDirty();
			}
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x00632534 File Offset: 0x00630734
		public void Save(BinaryWriter writer)
		{
			Dictionary<string, int> dictionary = this._sacrificeCountByItemPersistentId.Where(delegate(KeyValuePair<string, int> pair)
			{
				int netID;
				Item realItem;
				return ContentSamples.ItemNetIdsByPersistentIds.TryGetValue(pair.Key, out netID) && ContentSamples.ItemsByType.TryGetValue(netID, out realItem) && realItem.ModItem == null;
			}).ToDictionary((KeyValuePair<string, int> pair) => pair.Key, (KeyValuePair<string, int> pair) => pair.Value);
			writer.Write(dictionary.Count);
			foreach (KeyValuePair<string, int> item in dictionary)
			{
				writer.Write(item.Key);
				writer.Write(item.Value);
			}
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x00632610 File Offset: 0x00630810
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string key = reader.ReadString();
				int value = reader.ReadInt32();
				int value2;
				if (ContentSamples.ItemNetIdsByPersistentIds.TryGetValue(key, out value2))
				{
					int value3;
					if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(value2, out value3))
					{
						value2 = value3;
					}
					this._sacrificesCountByItemIdCache[value2] = value;
					string value4;
					if (ContentSamples.ItemPersistentIdsByNetIds.TryGetValue(value2, out value4))
					{
						key = value4;
					}
				}
				this._sacrificeCountByItemPersistentId[key] = value;
			}
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x00632690 File Offset: 0x00630890
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
				reader.ReadInt32();
			}
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x006326BE File Offset: 0x006308BE
		public void Reset()
		{
			this._sacrificeCountByItemPersistentId.Clear();
			this._sacrificesCountByItemIdCache.Clear();
			this.MarkContentsDirty();
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x006326DC File Offset: 0x006308DC
		public void OnPlayerJoining(int playerIndex)
		{
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x006326E0 File Offset: 0x006308E0
		public void MarkContentsDirty()
		{
			int lastEditId = this.LastEditId;
			this.LastEditId = lastEditId + 1;
		}

		// Token: 0x04005B88 RID: 23432
		public const int POSITIVE_SACRIFICE_COUNT_CAP = 9999;

		// Token: 0x04005B89 RID: 23433
		internal Dictionary<string, int> _sacrificeCountByItemPersistentId;

		// Token: 0x04005B8A RID: 23434
		internal Dictionary<int, int> _sacrificesCountByItemIdCache;
	}
}
