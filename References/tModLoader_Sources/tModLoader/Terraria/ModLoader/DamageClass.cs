using System;
using ReLogic.Reflection;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// <see cref="T:Terraria.ModLoader.DamageClass" /> is used to determine the application of item effects, damage/stat scaling, and class bonuses.
	/// </summary>
	/// <remarks>
	/// New classes can be created and can be set to inherit these applications from other classes. 
	/// <para>For a more in-depth explanation and demonstration refer to <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/DamageClasses/ExampleDamageClass.cs">ExampleMod's ExampleDamageClass.cs</see>.</para>
	/// </remarks>
	// Token: 0x02000155 RID: 341
	public abstract class DamageClass : ModType, ILocalizedModType, IModType
	{
		/// <summary>
		/// Default damage class for non-classed weapons and items, does not benefit from Generic bonuses
		/// </summary>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x004D0D39 File Offset: 0x004CEF39
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x004D0D40 File Offset: 0x004CEF40
		public static DamageClass Default { get; private set; } = new DefaultDamageClass();

		/// <summary>
		/// Base damage class for all weapons. All vanilla damage classes inherit bonuses applied to this class.
		/// Accessories which benefit all classes provide bonuses via the Generic class
		/// </summary>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x004D0D48 File Offset: 0x004CEF48
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x004D0D4F File Offset: 0x004CEF4F
		public static DamageClass Generic { get; private set; } = new GenericDamageClass();

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x004D0D57 File Offset: 0x004CEF57
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x004D0D5E File Offset: 0x004CEF5E
		public static DamageClass Melee { get; private set; } = new MeleeDamageClass();

		/// <summary>
		/// This is a damage class used by various projectile-only vanilla melee weapons. Attack speed has no effect on items with this damage class.
		/// </summary>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x004D0D66 File Offset: 0x004CEF66
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x004D0D6D File Offset: 0x004CEF6D
		public static DamageClass MeleeNoSpeed { get; private set; } = new MeleeNoSpeedDamageClass();

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x004D0D75 File Offset: 0x004CEF75
		// (set) Token: 0x06001BA4 RID: 7076 RVA: 0x004D0D7C File Offset: 0x004CEF7C
		public static DamageClass Ranged { get; private set; } = new RangedDamageClass();

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x004D0D84 File Offset: 0x004CEF84
		// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x004D0D8B File Offset: 0x004CEF8B
		public static DamageClass Magic { get; private set; } = new MagicDamageClass();

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x004D0D93 File Offset: 0x004CEF93
		// (set) Token: 0x06001BA8 RID: 7080 RVA: 0x004D0D9A File Offset: 0x004CEF9A
		public static DamageClass Summon { get; private set; } = new SummonDamageClass();

		/// <summary>
		/// This is a damage class used solely by vanilla whips. It benefits from melee attackSpeed bonuses.
		/// </summary>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x004D0DA2 File Offset: 0x004CEFA2
		// (set) Token: 0x06001BAA RID: 7082 RVA: 0x004D0DA9 File Offset: 0x004CEFA9
		public static DamageClass SummonMeleeSpeed { get; private set; } = new SummonMeleeSpeedDamageClass();

		/// <summary>
		/// This is a damage class used solely by vanilla forbidden storm. It scales with both magic and summon damage modifiers.
		/// </summary>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x004D0DB1 File Offset: 0x004CEFB1
		// (set) Token: 0x06001BAC RID: 7084 RVA: 0x004D0DB8 File Offset: 0x004CEFB8
		public static DamageClass MagicSummonHybrid { get; private set; } = new MagicSummonHybridDamageClass();

		/// <summary>
		/// Class provided for modders who want to coordinate throwing accessories and items. Not used by any vanilla items.
		/// </summary>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x004D0DC0 File Offset: 0x004CEFC0
		// (set) Token: 0x06001BAE RID: 7086 RVA: 0x004D0DC7 File Offset: 0x004CEFC7
		public static DamageClass Throwing { get; private set; } = new ThrowingDamageClass();

		/// <summary>
		/// The internal ID of this <see cref="T:Terraria.ModLoader.DamageClass" />.
		/// </summary>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x004D0DCF File Offset: 0x004CEFCF
		// (set) Token: 0x06001BB0 RID: 7088 RVA: 0x004D0DD7 File Offset: 0x004CEFD7
		public int Type { get; internal set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x004D0DE0 File Offset: 0x004CEFE0
		public virtual string LocalizationCategory
		{
			get
			{
				return "DamageClasses";
			}
		}

		/// <summary>
		/// This is the name that will show up when an item tooltip displays 'X [ClassName]'.
		/// </summary>
		/// <remarks>
		/// This should include the 'damage' part.
		/// Note that vanilla entries all start with a space that will need to be trimmed if used in other contexts.
		/// </remarks>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x004D0DE7 File Offset: 0x004CEFE7
		public virtual LocalizedText DisplayName
		{
			get
			{
				return this.GetLocalization("DisplayName", new Func<string>(base.PrettyPrintName));
			}
		}

		/// <summary>
		/// This lets you define the classes that this <see cref="T:Terraria.ModLoader.DamageClass" /> will benefit from (other than itself) for the purposes of stat bonuses, such as damage and crit chance.
		/// This is used to allow extensive specifications for what your damage class can and can't benefit from in terms of other classes' stat bonuses.
		/// </summary>
		/// <param name="damageClass">The <see cref="T:Terraria.ModLoader.DamageClass" /> which you want this <see cref="T:Terraria.ModLoader.DamageClass" /> to benefit from statistically.</param>
		/// <returns>By default this will return <see cref="F:Terraria.ModLoader.StatInheritanceData.Full" /> for <see cref="P:Terraria.ModLoader.DamageClass.Generic" /> and <see cref="F:Terraria.ModLoader.StatInheritanceData.None" /> for any other.</returns>
		// Token: 0x06001BB3 RID: 7091 RVA: 0x004D0E00 File Offset: 0x004CF000
		public virtual StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			if (damageClass != DamageClass.Generic)
			{
				return StatInheritanceData.None;
			}
			return StatInheritanceData.Full;
		}

		/// <summary> 
		/// This lets you define the classes that this <see cref="T:Terraria.ModLoader.DamageClass" /> will count as (other than itself) for the purpose of armor and accessory effects, such as Spectre armor's bolts on magic attacks, or Magma Stone's Hellfire debuff on melee attacks.<br />
		/// For a more in-depth explanation and demonstration, see <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/DamageClasses/ExampleDamageClass.cs">ExampleMod's ExampleDamageClass.cs</see>
		/// This method is only meant to be overridden. Modders should call <see cref="M:Terraria.ModLoader.DamageClass.CountsAsClass(Terraria.ModLoader.DamageClass)" /> to query effect inheritance.
		/// </summary>
		/// <remarks>Return <see langword="true" /> for each <see cref="T:Terraria.ModLoader.DamageClass" /> you want to inherit from</remarks>
		/// <param name="damageClass">The <see cref="T:Terraria.ModLoader.DamageClass" /> you want to inherit effects from.</param>
		/// <returns><see langword="false" /> by default - which does not let any other classes' effects trigger on this <see cref="T:Terraria.ModLoader.DamageClass" />.</returns>
		// Token: 0x06001BB4 RID: 7092 RVA: 0x004D0E15 File Offset: 0x004CF015
		public virtual bool GetEffectInheritance(DamageClass damageClass)
		{
			return false;
		}

		/// <summary> 
		/// This lets you define the classes that this <see cref="T:Terraria.ModLoader.DamageClass" /> will count as (other than itself) for the purpose of prefixes.<br />
		/// This method is only meant to be overridden. Modders should call <see cref="M:Terraria.ModLoader.DamageClass.GetsPrefixesFor(Terraria.ModLoader.DamageClass)" /> to query prefix inheritance.
		/// </summary>
		/// <remarks>Return <see langword="true" /> for each <see cref="T:Terraria.ModLoader.DamageClass" /> you want to inherit from</remarks>
		/// <param name="damageClass">The <see cref="T:Terraria.ModLoader.DamageClass" /> you want to inherit prefixes from.</param>
		/// <returns><see cref="M:Terraria.ModLoader.DamageClass.GetEffectInheritance(Terraria.ModLoader.DamageClass)" /> by default - which lets the prefixes of any class this class inherits effects from roll and remain on items of this <see cref="T:Terraria.ModLoader.DamageClass" />.</returns>
		// Token: 0x06001BB5 RID: 7093 RVA: 0x004D0E18 File Offset: 0x004CF018
		public virtual bool GetPrefixInheritance(DamageClass damageClass)
		{
			return this.GetEffectInheritance(damageClass);
		}

		/// <summary> 
		/// This lets you define default stat modifiers for all items of this class (e.g. base crit chance).
		/// </summary>
		/// <param name="player">The player to apply stat modifications to</param>
		// Token: 0x06001BB6 RID: 7094 RVA: 0x004D0E21 File Offset: 0x004CF021
		public virtual void SetDefaultStats(Player player)
		{
		}

		/// <summary>
		/// This lets you decide whether or not your damage class will use standard crit chance calculations.
		/// Setting this to <see langword="false" /> will also hide the critical strike chance line in the tooltip of any item that uses this <see cref="T:Terraria.ModLoader.DamageClass" />.
		/// </summary>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x004D0E23 File Offset: 0x004CF023
		public virtual bool UseStandardCritCalcs
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Overriding this lets you disable standard statistical tooltip lines displayed on items associated with this <see cref="T:Terraria.ModLoader.DamageClass" />. All tooltip lines are enabled by default.
		/// </summary>
		/// <remarks>To disable tooltip lines you should return <see langword="false" /> for each of those cases.</remarks>
		/// <param name="player">The player to apply tooltip changes to</param>
		/// <param name="lineName">The tooltip line to change visibility for. Usable values are: "Damage", "CritChance", "Speed", and "Knockback"</param>
		// Token: 0x06001BB8 RID: 7096 RVA: 0x004D0E26 File Offset: 0x004CF026
		public virtual bool ShowStatTooltipLine(Player player, string lineName)
		{
			return true;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x004D0E29 File Offset: 0x004CF029
		protected sealed override void Register()
		{
			ModTypeLookup<DamageClass>.Register(this);
			this.Type = DamageClassLoader.Add(this);
			DamageClass.Search.Add(base.FullName, this.Type);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x004D0E53 File Offset: 0x004CF053
		public sealed override void SetupContent()
		{
			LocalizedText displayName = this.DisplayName;
			this.SetStaticDefaults();
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.DamageClass.CountsAsClass(Terraria.ModLoader.DamageClass)" />
		// Token: 0x06001BBB RID: 7099 RVA: 0x004D0E62 File Offset: 0x004CF062
		public bool CountsAsClass<T>() where T : DamageClass
		{
			return this.CountsAsClass(ModContent.GetInstance<T>());
		}

		/// <summary>
		/// This is used to check if this <see cref="T:Terraria.ModLoader.DamageClass" /> has been set to inherit effects from the provided <see cref="T:Terraria.ModLoader.DamageClass" />, as dictated by <see cref="M:Terraria.ModLoader.DamageClass.GetEffectInheritance(Terraria.ModLoader.DamageClass)" />
		/// </summary>
		/// <param name="damageClass">The DamageClass you want to check if effects are inherited by this DamageClass.</param>
		/// <returns><see langword="true" /> if this damage class is inheriting effects from <paramref name="damageClass" />, <see langword="false" /> otherwise</returns>
		// Token: 0x06001BBC RID: 7100 RVA: 0x004D0E74 File Offset: 0x004CF074
		public bool CountsAsClass(DamageClass damageClass)
		{
			return DamageClassLoader.effectInheritanceCache[this.Type, damageClass.Type];
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.DamageClass.GetsPrefixesFor(Terraria.ModLoader.DamageClass)" />
		// Token: 0x06001BBD RID: 7101 RVA: 0x004D0E8C File Offset: 0x004CF08C
		public bool GetsPrefixesFor<T>() where T : DamageClass
		{
			return this.GetsPrefixesFor(ModContent.GetInstance<T>());
		}

		/// <summary>
		/// This is used to check if this <see cref="T:Terraria.ModLoader.DamageClass" /> has been set to inherit prefixes from the provided <see cref="T:Terraria.ModLoader.DamageClass" />, as dictated by <see cref="M:Terraria.ModLoader.DamageClass.GetPrefixInheritance(Terraria.ModLoader.DamageClass)" />
		/// </summary>
		/// <param name="damageClass">The DamageClass you want to check if prefixes are inherited by this DamageClass.</param>
		/// <returns><see langword="true" /> if this damage class inherits prefixes from <paramref name="damageClass" />, <see langword="false" /> otherwise</returns>
		// Token: 0x06001BBE RID: 7102 RVA: 0x004D0E9E File Offset: 0x004CF09E
		public bool GetsPrefixesFor(DamageClass damageClass)
		{
			return this == damageClass || this.GetPrefixInheritance(damageClass);
		}

		// Token: 0x040014C7 RID: 5319
		public static IdDictionary Search = IdDictionary.Create<DamageClass, int>();

		// Token: 0x020008C8 RID: 2248
		public class Sets
		{
			/// <summary>
			/// Used for creating sets indexed by DamageClass type (<see cref="P:Terraria.ModLoader.DamageClass.Type" />).
			/// <para /> <inheritdoc cref="T:Terraria.ID.SetFactory" />
			/// </summary>
			// Token: 0x04006A69 RID: 27241
			public static SetFactory Factory = new SetFactory(DamageClassLoader.DamageClassCount, "DamageClass", DamageClass.Search);
		}
	}
}
