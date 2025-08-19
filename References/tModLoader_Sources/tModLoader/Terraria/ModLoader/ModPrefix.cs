using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Represents a modded prefix (or modifier). The <see href="https://terraria.wiki.gg/wiki/Modifiers">Modifiers page on the Terraria wiki</see> is a good resource for vanilla prefixes.
	/// </summary>
	// Token: 0x020001C3 RID: 451
	public abstract class ModPrefix : ModType, ILocalizedModType, IModType
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x004E8544 File Offset: 0x004E6744
		// (set) Token: 0x06002349 RID: 9033 RVA: 0x004E854C File Offset: 0x004E674C
		public int Type { get; internal set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x004E8555 File Offset: 0x004E6755
		public virtual string LocalizationCategory
		{
			get
			{
				return "Prefixes";
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x004E855C File Offset: 0x004E675C
		public virtual LocalizedText DisplayName
		{
			get
			{
				return this.GetLocalization("DisplayName", new Func<string>(base.PrettyPrintName));
			}
		}

		/// <summary>
		/// The category your prefix belongs to, PrefixCategory.Custom by default
		/// </summary>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x004E8575 File Offset: 0x004E6775
		public virtual PrefixCategory Category
		{
			get
			{
				return PrefixCategory.Custom;
			}
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x004E8578 File Offset: 0x004E6778
		protected sealed override void Register()
		{
			ModTypeLookup<ModPrefix>.Register(this);
			this.Type = PrefixLoader.ReservePrefixID();
			PrefixLoader.RegisterPrefix(this);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x004E8591 File Offset: 0x004E6791
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
			PrefixID.Search.Add(base.FullName, this.Type);
		}

		/// <summary>
		/// The roll chance of your prefix relative to a vanilla prefix, 1f by default.
		/// </summary>
		// Token: 0x0600234F RID: 9039 RVA: 0x004E85AF File Offset: 0x004E67AF
		public virtual float RollChance(Item item)
		{
			return 1f;
		}

		/// <summary>
		/// Returns if your ModPrefix can roll on the given item
		/// By default returns RollChance(item) &gt; 0
		/// </summary>
		// Token: 0x06002350 RID: 9040 RVA: 0x004E85B6 File Offset: 0x004E67B6
		public virtual bool CanRoll(Item item)
		{
			return this.RollChance(item) > 0f;
		}

		/// <summary>
		/// Sets the stat changes for this prefix. If data is not already pre-stored, it is best to store custom data changes to some static variables.
		/// <br /><br /> All parameters default to 1f, except <paramref name="critBonus" /> which defaults to 0.
		/// <br /><br /> It is important to remember that a prefix will only be applied to an item if every stat it affects would be changed after multiplication and rounding to the nearest integer. If a ModPrefix is not being applied to items, a multiplier might be too insignificant. The <see href="https://terraria.wiki.gg/wiki/Modifiers#Notes">Modifiers page on the Terraria wiki</see> has more details.
		/// <br /><br /> Use <see cref="M:Terraria.ModLoader.ModPrefix.AllStatChangesHaveEffectOn(Terraria.Item)" /> to implement that same restriction for modded stats. 
		/// </summary>
		// Token: 0x06002351 RID: 9041 RVA: 0x004E85C6 File Offset: 0x004E67C6
		public virtual void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
		{
		}

		/// <summary>
		/// Use this to check whether modifiers to custom stats would be too small to have an effect after rounding, and prevent the prefix from being applied to the given item if there would be no change.
		/// <para />Vanilla stat changes (<seealso cref="M:Terraria.ModLoader.ModPrefix.SetStats(System.Single@,System.Single@,System.Single@,System.Single@,System.Single@,System.Single@,System.Int32@)" />) are checked automatically, so there is no need to override this method to check them
		/// </summary>
		/// <returns>false to prevent the prefix from being applied</returns>
		// Token: 0x06002352 RID: 9042 RVA: 0x004E85C8 File Offset: 0x004E67C8
		public virtual bool AllStatChangesHaveEffectOn(Item item)
		{
			return true;
		}

		/// <summary>
		/// Applies the custom data stats set in SetStats to the given item.
		/// </summary>
		// Token: 0x06002353 RID: 9043 RVA: 0x004E85CB File Offset: 0x004E67CB
		public virtual void Apply(Item item)
		{
		}

		/// <summary>
		/// Allows you to modify the sell price of the item based on the prefix or changes in custom data stats. This also influences the item's rarity.
		/// </summary>
		// Token: 0x06002354 RID: 9044 RVA: 0x004E85CD File Offset: 0x004E67CD
		public virtual void ModifyValue(ref float valueMult)
		{
		}

		/// <summary>
		/// Use this to modify player stats (or any other applicable data) based on this ModPrefix.
		/// </summary>
		/// <param name="player"> The player gaining the benefits of this accessory. </param>
		// Token: 0x06002355 RID: 9045 RVA: 0x004E85CF File Offset: 0x004E67CF
		public virtual void ApplyAccessoryEffects(Player player)
		{
		}

		/// <summary>
		/// Use this to add tooltips to any item with this prefix applied. Note that the stat bonuses applied via <see cref="M:Terraria.ModLoader.ModPrefix.SetStats(System.Single@,System.Single@,System.Single@,System.Single@,System.Single@,System.Single@,System.Int32@)" /> will automatically generate tooltips. (such as damage, use speed, crit chance, mana cost, scale, shoot speed, and knockback)<br />
		/// </summary>
		// Token: 0x06002356 RID: 9046 RVA: 0x004E85D1 File Offset: 0x004E67D1
		public virtual IEnumerable<TooltipLine> GetTooltipLines(Item item)
		{
			return null;
		}
	}
}
