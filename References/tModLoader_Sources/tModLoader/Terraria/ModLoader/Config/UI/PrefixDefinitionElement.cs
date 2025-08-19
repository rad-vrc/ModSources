using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003B3 RID: 947
	internal class PrefixDefinitionElement : DefinitionElement<PrefixDefinition>
	{
		// Token: 0x06003288 RID: 12936 RVA: 0x0054470C File Offset: 0x0054290C
		protected override DefinitionOptionElement<PrefixDefinition> CreateDefinitionOptionElement()
		{
			return new PrefixDefinitionOptionElement(this.Value, 0.8f);
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x0054471E File Offset: 0x0054291E
		protected override void TweakDefinitionOptionElement(DefinitionOptionElement<PrefixDefinition> optionElement)
		{
			optionElement.Top.Set(0f, 0f);
			optionElement.Left.Set(-124f, 1f);
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x0054474C File Offset: 0x0054294C
		protected override List<DefinitionOptionElement<PrefixDefinition>> CreateDefinitionOptionElementList()
		{
			base.OptionScale = 0.8f;
			List<DefinitionOptionElement<PrefixDefinition>> options = new List<DefinitionOptionElement<PrefixDefinition>>();
			for (int i = 0; i < PrefixLoader.PrefixCount; i++)
			{
				PrefixDefinitionOptionElement optionElement;
				if (i == 0)
				{
					optionElement = new PrefixDefinitionOptionElement(new PrefixDefinition("Terraria", "None"), base.OptionScale);
				}
				else
				{
					optionElement = new PrefixDefinitionOptionElement(new PrefixDefinition(i), base.OptionScale);
				}
				optionElement.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					this.Value = optionElement.Definition;
					this.UpdateNeeded = true;
					this.SelectionExpanded = false;
				};
				options.Add(optionElement);
			}
			return options;
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x005447E8 File Offset: 0x005429E8
		protected override List<DefinitionOptionElement<PrefixDefinition>> GetPassedOptionElements()
		{
			List<DefinitionOptionElement<PrefixDefinition>> passed = new List<DefinitionOptionElement<PrefixDefinition>>();
			foreach (DefinitionOptionElement<PrefixDefinition> option in base.Options)
			{
				if (option.Definition.DisplayName.IndexOf(base.ChooserFilter.CurrentString, StringComparison.OrdinalIgnoreCase) != -1)
				{
					string modname = option.Definition.Mod;
					if (option.Type >= PrefixID.Count)
					{
						modname = PrefixLoader.GetPrefix(option.Type).Mod.DisplayNameClean;
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
