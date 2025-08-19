using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves as a way to store information about a line of tooltip for an item. You will create and manipulate objects of this class if you use the ModifyTooltips hook.
	/// </summary>
	// Token: 0x020001FF RID: 511
	public class TooltipLine
	{
		/// <summary>
		/// =&gt; $"{Mod}/{Name}"
		/// </summary>
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x00508E09 File Offset: 0x00507009
		public string FullName
		{
			get
			{
				return this.Mod + "/" + this.Name;
			}
		}

		/// <summary>
		/// Creates a tooltip line object with the given mod, identifier name, and text.<para />
		/// These are the names of the vanilla tooltip lines, in the order in which they appear, along with their functions. All of them will have a mod name of "Terraria". Remember that most of these tooltip lines will not exist depending on the item.<para />
		/// <list type="bullet">
		/// <item><description>"ItemName" - The name of the item.</description></item>
		/// <item><description>"Favorite" - Tells if the item is favorited.</description></item>
		/// <item><description>"FavoriteDesc" - Tells what it means when an item is favorited.</description></item>
		/// <item><description>"NoTransfer" - Warning that this item cannot be placed inside itself, used by Money Trough and Void Bag/Vault.</description></item>
		/// <item><description>"Social" - Tells if the item is in a social slot.</description></item>
		/// <item><description>"SocialDesc" - Tells what it means for an item to be in a social slot.</description></item>
		/// <item><description>"Damage" - The damage value and type of the weapon.</description></item>
		/// <item><description>"CritChance" - The critical strike chance of the weapon.</description></item>
		/// <item><description>"Speed" - The use speed of the weapon.</description></item>
		/// <item><description>"NoSpeedScaling" - Whether this item does not scale with attack speed, added by tModLoader.</description></item>
		/// <item><description>"SpecialSpeedScaling" - The multiplier this item applies to attack speed bonuses, added by tModLoader.</description></item>
		/// <item><description>"Knockback" - The knockback of the weapon.</description></item>
		/// <item><description>"FishingPower" - Tells the fishing power of the fishing pole.</description></item>
		/// <item><description>"NeedsBait" - Tells that a fishing pole requires bait.</description></item>
		/// <item><description>"BaitPower" - The bait power of the bait.</description></item>
		/// <item><description>"Equipable" - Tells that an item is equipable.</description></item>
		/// <item><description>"WandConsumes" - Tells what item a tile wand consumes.</description></item>
		/// <item><description>"Quest" - Tells that this is a quest item.</description></item>
		/// <item><description>"Vanity" - Tells that this is a vanity item.</description></item>
		/// <item><description>"Defense" - Tells how much defense the item gives when equipped.</description></item>
		/// <item><description>"PickPower" - The item's pickaxe power.</description></item>
		/// <item><description>"AxePower" - The item's axe power.</description></item>
		/// <item><description>"HammerPower" - The item's hammer power.</description></item>
		/// <item><description>"TileBoost" - How much farther the item can reach than normal items.</description></item>
		/// <item><description>"HealLife" - How much health the item recovers when used.</description></item>
		/// <item><description>"HealMana" - How much mana the item recovers when used.</description></item>
		/// <item><description>"UseMana" - Tells how much mana the item consumes upon usage.</description></item>
		/// <item><description>"Placeable" - Tells if the item is placeable.</description></item>
		/// <item><description>"Ammo" - Tells if the item is ammo.</description></item>
		/// <item><description>"Consumable" - Tells if the item is consumable.</description></item>
		/// <item><description>"Material" - Tells if the item can be used to craft something.</description></item>
		/// <item><description>"Tooltip#" - A tooltip line of the item. # will be 0 for the first line, 1 for the second, etc.</description></item>
		/// <item><description>"EtherianManaWarning" - Warning about how the item can't be used without Etherian Mana until the Eternia Crystal has been defeated.</description></item>
		/// <item><description>"WellFedExpert" - In expert mode, tells that food increases life regeneration.</description></item>
		/// <item><description>"BuffTime" - Tells how long the item's buff lasts.</description></item>
		/// <item><description>"OneDropLogo" - The One Drop logo for yoyos.This is a specially-marked tooltip line that has no text.</description></item>
		/// <item><description>"PrefixDamage" - The damage modifier of the prefix.</description></item>
		/// <item><description>"PrefixSpeed" - The usage speed modifier of the prefix.</description></item>
		/// <item><description>"PrefixCritChance" - The critical strike chance modifier of the prefix.</description></item>
		/// <item><description>"PrefixUseMana" - The mana consumption modifier of the prefix.</description></item>
		/// <item><description>"PrefixSize" - The melee size modifier of the prefix.</description></item>
		/// <item><description>"PrefixShootSpeed" - The shootSpeed modifier of the prefix.</description></item>
		/// <item><description>"PrefixKnockback" - The knockback modifier of the prefix.</description></item>
		/// <item><description>"PrefixAccDefense" - The defense modifier of the accessory prefix.</description></item>
		/// <item><description>"PrefixAccMaxMana" - The maximum mana modifier of the accessory prefix.</description></item>
		/// <item><description>"PrefixAccCritChance" - The critical strike chance modifier of the accessory prefix.</description></item>
		/// <item><description>"PrefixAccDamage" - The damage modifier of the accessory prefix.</description></item>
		/// <item><description>"PrefixAccMoveSpeed" - The movement speed modifier of the accessory prefix.</description></item>
		/// <item><description>"PrefixAccMeleeSpeed" - The melee speed modifier of the accessory prefix.</description></item>
		/// <item><description>"SetBonus" - The set bonus description of the armor set.</description></item>
		/// <item><description>"Expert" - Tells whether the item is from expert-mode.</description></item>
		/// <item><description>"Master" - Whether the item is exclusive to Master Mode.</description></item>
		/// <item><description>"JourneyResearch" - How many more items need to be researched to unlock duplication in Journey Mode.</description></item>
		/// <item><description>"ModifiedByMods" - Whether the item has been modified by any mods and what mods when holding shift, added by tModLoader.</description></item>
		/// <item><description>"BestiaryNotes" - Any bestiary notes, used when hovering items in the bestiary.</description></item>
		/// <item><description>"SpecialPrice" - Tells the alternate currency price of an item.</description></item>
		/// <item><description>"Price" - Tells the price of an item.</description></item>
		/// </list>
		/// </summary>
		/// <param name="mod">The mod instance</param>
		/// <param name="name">The name of the tooltip</param>
		/// <param name="text">The content of the tooltip</param>
		// Token: 0x060027C6 RID: 10182 RVA: 0x00508E21 File Offset: 0x00507021
		public TooltipLine(Mod mod, string name, string text)
		{
			this.Mod = mod.Name;
			this.Name = name;
			this.Text = text;
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x00508E4A File Offset: 0x0050704A
		internal TooltipLine(string mod, string name, string text)
		{
			this.Mod = mod;
			this.Name = name;
			this.Text = text;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x00508E6E File Offset: 0x0050706E
		internal TooltipLine(string name, string text)
		{
			this.Mod = "Terraria";
			this.Name = name;
			this.Text = text;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x00508E96 File Offset: 0x00507096
		// (set) Token: 0x060027CA RID: 10186 RVA: 0x00508E9E File Offset: 0x0050709E
		public bool Visible { get; private set; } = true;

		// Token: 0x060027CB RID: 10187 RVA: 0x00508EA7 File Offset: 0x005070A7
		public void Hide()
		{
			this.Visible = false;
		}

		/// <summary>
		/// The name of the mod adding this tooltip line. This will be "Terraria" for all vanilla tooltip lines.
		/// </summary>
		// Token: 0x0400192B RID: 6443
		public readonly string Mod;

		/// <summary>
		/// The name of the tooltip, used to help you identify its function.
		/// </summary>
		// Token: 0x0400192C RID: 6444
		public readonly string Name;

		/// <summary>
		/// The actual text that this tooltip displays.
		/// </summary>
		// Token: 0x0400192D RID: 6445
		public string Text;

		/// <summary>
		/// Whether or not this tooltip gives prefix information. This will make it so that the tooltip is colored either green or red.
		/// </summary>
		// Token: 0x0400192E RID: 6446
		public bool IsModifier;

		/// <summary>
		/// If isModifier is true, this determines whether the tooltip is colored green or red.
		/// </summary>
		// Token: 0x0400192F RID: 6447
		public bool IsModifierBad;

		/// <summary>
		/// This completely overrides the color the tooltip is drawn in. If it is set to null (the default value) then the tooltip's color will not be overridden.
		/// </summary>
		// Token: 0x04001930 RID: 6448
		public Color? OverrideColor;

		// Token: 0x04001931 RID: 6449
		internal bool OneDropLogo;
	}
}
