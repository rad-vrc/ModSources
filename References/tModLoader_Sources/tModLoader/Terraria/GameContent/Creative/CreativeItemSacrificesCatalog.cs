using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria.ID;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200063F RID: 1599
	public class CreativeItemSacrificesCatalog
	{
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06004646 RID: 17990 RVA: 0x00630DAF File Offset: 0x0062EFAF
		public Dictionary<int, int> SacrificeCountNeededByItemId
		{
			get
			{
				return this._sacrificeCountNeededByItemId;
			}
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x00630DB8 File Offset: 0x0062EFB8
		public void Initialize()
		{
			this._sacrificeCountNeededByItemId.Clear();
			foreach (string text in Regex.Split(Utils.ReadEmbeddedResource("Terraria.GameContent.Creative.Content.Sacrifices.tsv"), "\r\n|\r|\n"))
			{
				if (!text.StartsWith("//"))
				{
					string[] array2 = text.Split('\t', StringSplitOptions.None);
					int id;
					if (array2.Length >= 3 && ItemID.Search.TryGetId(array2[0], ref id))
					{
						int value = 0;
						bool flag = false;
						string text2 = array2[1].ToLower();
						if (text2 != null)
						{
							int length = text2.Length;
							if (length == 0)
							{
								goto IL_E0;
							}
							if (length != 1)
							{
								goto IL_13B;
							}
							switch (text2[0])
							{
							case 'a':
								goto IL_E0;
							case 'b':
								value = 25;
								break;
							case 'c':
								value = 5;
								break;
							case 'd':
								value = 1;
								break;
							case 'e':
								flag = true;
								break;
							case 'f':
								value = 2;
								break;
							case 'g':
								value = 3;
								break;
							case 'h':
								value = 10;
								break;
							case 'i':
								value = 15;
								break;
							case 'j':
								value = 30;
								break;
							case 'k':
								value = 99;
								break;
							case 'l':
								value = 100;
								break;
							case 'm':
								value = 200;
								break;
							case 'n':
								value = 20;
								break;
							case 'o':
								value = 400;
								break;
							default:
								goto IL_13B;
							}
							IL_155:
							if (!flag)
							{
								this._sacrificeCountNeededByItemId[id] = value;
								goto IL_168;
							}
							goto IL_168;
							IL_E0:
							value = 50;
							goto IL_155;
						}
						IL_13B:
						throw new Exception("There is no category for this item: " + array2[0] + ", category: " + text2);
					}
				}
				IL_168:;
			}
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x00630F3C File Offset: 0x0062F13C
		public bool TryGetSacrificeCountCapToUnlockInfiniteItems(int itemId, out int amountNeeded)
		{
			int value;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out value))
			{
				itemId = value;
			}
			return this._sacrificeCountNeededByItemId.TryGetValue(itemId, out amountNeeded);
		}

		// Token: 0x04005B69 RID: 23401
		public static CreativeItemSacrificesCatalog Instance = new CreativeItemSacrificesCatalog();

		// Token: 0x04005B6A RID: 23402
		private Dictionary<int, int> _sacrificeCountNeededByItemId = new Dictionary<int, int>();
	}
}
