using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace Terraria.GameInput
{
	// Token: 0x02000481 RID: 1153
	public class KeyConfiguration
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x00589E50 File Offset: 0x00588050
		public bool DoGrappleAndInteractShareTheSameKey
		{
			get
			{
				return this.KeyStatus["Grapple"].Count > 0 && this.KeyStatus["MouseRight"].Count > 0 && this.KeyStatus["MouseRight"].Contains(this.KeyStatus["Grapple"][0]);
			}
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x00589EBC File Offset: 0x005880BC
		public void SetupKeys()
		{
			this.KeyStatus.Clear();
			foreach (string knownTrigger in PlayerInput.KnownTriggers)
			{
				this.KeyStatus.Add(knownTrigger, new List<string>());
			}
			foreach (ModKeybind current in KeybindLoader.Keybinds)
			{
				this.KeyStatus.Add(current.FullName, new List<string>());
			}
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x00589F70 File Offset: 0x00588170
		public void Processkey(TriggersSet set, string newKey, InputMode mode)
		{
			foreach (KeyValuePair<string, List<string>> item in this.KeyStatus)
			{
				if (item.Value.Contains(newKey))
				{
					set.KeyStatus[item.Key] = true;
					set.LatestInputMode[item.Key] = mode;
				}
			}
			if (set.Up || set.Down || set.Left || set.Right || set.HotbarPlus || set.HotbarMinus || ((Main.gameMenu || Main.ingameOptionsWindow) && (set.MenuUp || set.MenuDown || set.MenuLeft || set.MenuRight)))
			{
				set.UsedMovementKey = true;
			}
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x0058A058 File Offset: 0x00588258
		public void CopyKeyState(TriggersSet oldSet, TriggersSet newSet, string newKey)
		{
			foreach (KeyValuePair<string, List<string>> item in this.KeyStatus)
			{
				if (item.Value.Contains(newKey))
				{
					newSet.KeyStatus[item.Key] = oldSet.KeyStatus[item.Key];
				}
			}
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x0058A0D8 File Offset: 0x005882D8
		public void ReadPreferences(Dictionary<string, List<string>> dict)
		{
			this.UnloadedModKeyStatus.Clear();
			foreach (KeyValuePair<string, List<string>> item in dict)
			{
				if (this.KeyStatus.ContainsKey(item.Key))
				{
					this.KeyStatus[item.Key] = new List<string>(item.Value);
				}
				else if (item.Key.Contains('/'))
				{
					List<string> existing;
					if (!this.UnloadedModKeyStatus.TryGetValue(item.Key, out existing) || existing.Count < item.Value.Count)
					{
						this.UnloadedModKeyStatus[item.Key] = new List<string>(item.Value);
					}
				}
				else if (item.Key.Contains(": "))
				{
					string newKey = item.Key.Replace(": ", "/");
					if (!this.KeyStatus.ContainsKey(newKey) && !this.UnloadedModKeyStatus.ContainsKey(newKey))
					{
						this.UnloadedModKeyStatus[newKey] = new List<string>(item.Value);
					}
				}
			}
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x0058A22C File Offset: 0x0058842C
		public Dictionary<string, List<string>> WritePreferences()
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			foreach (KeyValuePair<string, List<string>> item in from x in this.KeyStatus
			where x.Value.Count > 0
			select x)
			{
				dictionary[item.Key] = item.Value.ToList<string>();
			}
			foreach (KeyValuePair<string, List<string>> item2 in from x in this.UnloadedModKeyStatus
			where x.Value.Count > 0
			select x)
			{
				dictionary[item2.Key] = item2.Value.ToList<string>();
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

		// Token: 0x0400515C RID: 20828
		public readonly Dictionary<string, List<string>> KeyStatus = new Dictionary<string, List<string>>();

		// Token: 0x0400515D RID: 20829
		public readonly Dictionary<string, List<string>> UnloadedModKeyStatus = new Dictionary<string, List<string>>();
	}
}
