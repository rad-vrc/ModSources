using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.GameInput
{
	// Token: 0x02000135 RID: 309
	public class KeyConfiguration
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x004D5394 File Offset: 0x004D3594
		public bool DoGrappleAndInteractShareTheSameKey
		{
			get
			{
				return this.KeyStatus["Grapple"].Count > 0 && this.KeyStatus["MouseRight"].Count > 0 && this.KeyStatus["MouseRight"].Contains(this.KeyStatus["Grapple"][0]);
			}
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x004D5400 File Offset: 0x004D3600
		public void SetupKeys()
		{
			this.KeyStatus.Clear();
			foreach (string key in PlayerInput.KnownTriggers)
			{
				this.KeyStatus.Add(key, new List<string>());
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x004D5468 File Offset: 0x004D3668
		public void Processkey(TriggersSet set, string newKey, InputMode mode)
		{
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.KeyStatus)
			{
				if (keyValuePair.Value.Contains(newKey))
				{
					set.KeyStatus[keyValuePair.Key] = true;
					set.LatestInputMode[keyValuePair.Key] = mode;
				}
			}
			if (set.Up || set.Down || set.Left || set.Right || set.HotbarPlus || set.HotbarMinus || ((Main.gameMenu || Main.ingameOptionsWindow) && (set.MenuUp || set.MenuDown || set.MenuLeft || set.MenuRight)))
			{
				set.UsedMovementKey = true;
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x004D5550 File Offset: 0x004D3750
		public void CopyKeyState(TriggersSet oldSet, TriggersSet newSet, string newKey)
		{
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.KeyStatus)
			{
				if (keyValuePair.Value.Contains(newKey))
				{
					newSet.KeyStatus[keyValuePair.Key] = oldSet.KeyStatus[keyValuePair.Key];
				}
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x004D55D0 File Offset: 0x004D37D0
		public void ReadPreferences(Dictionary<string, List<string>> dict)
		{
			foreach (KeyValuePair<string, List<string>> keyValuePair in dict)
			{
				if (this.KeyStatus.ContainsKey(keyValuePair.Key))
				{
					this.KeyStatus[keyValuePair.Key].Clear();
					foreach (string item in keyValuePair.Value)
					{
						this.KeyStatus[keyValuePair.Key].Add(item);
					}
				}
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x004D569C File Offset: 0x004D389C
		public Dictionary<string, List<string>> WritePreferences()
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.KeyStatus)
			{
				if (keyValuePair.Value.Count > 0)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value.ToList<string>());
				}
			}
			if (!dictionary.ContainsKey("MouseLeft") || dictionary["MouseLeft"].Count == 0)
			{
				dictionary["MouseLeft"] = new List<string>
				{
					"Mouse1"
				};
			}
			if (!dictionary.ContainsKey("Inventory") || dictionary["Inventory"].Count == 0)
			{
				dictionary["Inventory"] = new List<string>
				{
					"Escape"
				};
			}
			return dictionary;
		}

		// Token: 0x04001464 RID: 5220
		public Dictionary<string, List<string>> KeyStatus = new Dictionary<string, List<string>>();
	}
}
