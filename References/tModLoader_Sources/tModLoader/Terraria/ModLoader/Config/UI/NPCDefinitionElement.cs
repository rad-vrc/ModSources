using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003AD RID: 941
	internal class NPCDefinitionElement : DefinitionElement<NPCDefinition>
	{
		// Token: 0x06003268 RID: 12904 RVA: 0x005437FD File Offset: 0x005419FD
		protected override DefinitionOptionElement<NPCDefinition> CreateDefinitionOptionElement()
		{
			return new NPCDefinitionOptionElement(0, this.Value, 0.5f);
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x00543810 File Offset: 0x00541A10
		protected override List<DefinitionOptionElement<NPCDefinition>> CreateDefinitionOptionElementList()
		{
			base.OptionScale = 0.8f;
			List<DefinitionOptionElement<NPCDefinition>> options = new List<DefinitionOptionElement<NPCDefinition>>();
			IEnumerable<int> enumerable = Enumerable.Range(0, (int)NPCID.Count).Concat(Enumerable.Range(-65, 65).Reverse<int>()).Concat(Enumerable.Range((int)NPCID.Count, NPCLoader.NPCCount - (int)NPCID.Count));
			int order = 0;
			foreach (int i in enumerable)
			{
				NPCDefinitionOptionElement optionElement = new NPCDefinitionOptionElement(order, new NPCDefinition(i), base.OptionScale);
				optionElement.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					this.Value = optionElement.Definition;
					this.UpdateNeeded = true;
					this.SelectionExpanded = false;
				};
				options.Add(optionElement);
				order++;
			}
			return options;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x005438F0 File Offset: 0x00541AF0
		protected override List<DefinitionOptionElement<NPCDefinition>> GetPassedOptionElements()
		{
			List<DefinitionOptionElement<NPCDefinition>> passed = new List<DefinitionOptionElement<NPCDefinition>>();
			foreach (DefinitionOptionElement<NPCDefinition> option in base.Options)
			{
				if (Lang.GetNPCName(option.Type).Value.IndexOf(base.ChooserFilter.CurrentString, StringComparison.OrdinalIgnoreCase) != -1)
				{
					string modname = option.Definition.Mod;
					if (option.Type >= (int)NPCID.Count)
					{
						modname = NPCLoader.GetNPC(option.Type).Mod.DisplayNameClean;
					}
					if (modname.IndexOf(base.ChooserFilterMod.CurrentString, StringComparison.OrdinalIgnoreCase) != -1)
					{
						passed.Add(option);
					}
				}
			}
			return passed;
		}
	}
}
