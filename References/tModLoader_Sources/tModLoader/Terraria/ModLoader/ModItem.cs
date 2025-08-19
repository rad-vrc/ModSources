using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves as a place for you to place all your properties and hooks for each item.
	/// <br /> To use it, simply create a new class deriving from this one. Implementations will be registered automatically.
	/// <para /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Item">Basic Item Guide</see> teaches the basics of making a modded item.
	/// </summary>
	// Token: 0x020001B2 RID: 434
	public abstract class ModItem : ModType<Item, ModItem>, ILocalizedModType, IModType
	{
		/// <summary>
		/// The item object that this ModItem controls.
		/// </summary>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x004E41E7 File Offset: 0x004E23E7
		public Item Item
		{
			get
			{
				return base.Entity;
			}
		}

		/// <summary>
		/// Shorthand for <c>Item.type</c>.
		/// </summary>
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x004E41EF File Offset: 0x004E23EF
		public int Type
		{
			get
			{
				return this.Item.type;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x004E41FC File Offset: 0x004E23FC
		public virtual string LocalizationCategory
		{
			get
			{
				return "Items";
			}
		}

		/// <summary>
		/// The translations for the display name of this item.
		/// </summary>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x004E4203 File Offset: 0x004E2403
		public virtual LocalizedText DisplayName
		{
			get
			{
				return this.GetLocalization("DisplayName", new Func<string>(base.PrettyPrintName));
			}
		}

		/// <summary>
		/// The translations for the tooltip of this item.
		/// </summary>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x004E421C File Offset: 0x004E241C
		public virtual LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		/// <summary>
		/// The file name of this type's texture file in the mod loader's file space. <br />
		/// The resulting  Asset&lt;Texture2D&gt; can be retrieved using <see cref="F:Terraria.GameContent.TextureAssets.Item" /> indexed by <see cref="P:Terraria.ModLoader.ModItem.Type" /> if needed. <br />
		/// You can use a vanilla texture by returning <c>$"Terraria/Images/Item_{ItemID.ItemNameHere}"</c> <br />
		/// </summary>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x004E4248 File Offset: 0x004E2448
		public virtual string Texture
		{
			get
			{
				return (base.GetType().Namespace + "." + this.Name).Replace('.', '/');
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x004E426E File Offset: 0x004E246E
		protected override Item CreateTemplateEntity()
		{
			return new Item
			{
				ModItem = this
			};
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x004E427C File Offset: 0x004E247C
		protected override void ValidateType()
		{
			base.ValidateType();
			if (!this.IsCloneable)
			{
				Cloning.WarnNotCloneable(base.GetType());
			}
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x004E4298 File Offset: 0x004E2498
		protected sealed override void Register()
		{
			ModTypeLookup<ModItem>.Register(this);
			this.Item.ResetStats(ItemLoader.Register(this));
			this.Item.ModItem = this;
			AutoloadEquip autoloadEquip = AttributeUtilities.GetAttribute<AutoloadEquip>(base.GetType());
			if (autoloadEquip != null)
			{
				foreach (EquipType equip in autoloadEquip.equipTypes)
				{
					Mod mod = base.Mod;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
					defaultInterpolatedStringHandler.AppendFormatted(this.Texture);
					defaultInterpolatedStringHandler.AppendLiteral("_");
					defaultInterpolatedStringHandler.AppendFormatted<EquipType>(equip);
					EquipLoader.AddEquipTexture(mod, defaultInterpolatedStringHandler.ToStringAndClear(), equip, this, null, null);
				}
			}
			this.OnCreated(new InitializationItemCreationContext());
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x004E433C File Offset: 0x004E253C
		public sealed override void SetupContent()
		{
			ItemLoader.SetDefaults(this.Item, false);
			this.AutoStaticDefaults();
			this.SetStaticDefaults();
			ItemID.Search.Add(base.FullName, this.Type);
		}

		/// <summary>
		/// This is where you set all your item's properties, such as width, damage, shootSpeed, defense, etc. There are many properties that must be set for an item to do anything at all so it is best to consult examples, <see href="https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod/Content/Items">such as those in ExampleMod</see>, to get an idea of what is required for the type of item you are making.
		/// <para /> There are many useful methods such as <see cref="M:Terraria.Item.DefaultToPlaceableTile(System.Int32,System.Int32)" /> and <see cref="M:Terraria.Item.DefaultToRangedWeapon(System.Int32,System.Int32,System.Int32,System.Single,System.Boolean)" /> to easily set the variables needed for that type of item all at once.
		/// <para /> The <see cref="M:Terraria.Item.CloneDefaults(System.Int32)" /> method can be used to clone the item defaults of an item.
		/// </summary>
		// Token: 0x060020EF RID: 8431 RVA: 0x004E436C File Offset: 0x004E256C
		public virtual void SetDefaults()
		{
		}

		/// <summary>
		/// Gets called when your item spawns in world.
		/// <para /> Called on the local client or the server where Item.NewItem is called.
		/// </summary>
		// Token: 0x060020F0 RID: 8432 RVA: 0x004E436E File Offset: 0x004E256E
		public virtual void OnSpawn(IEntitySource source)
		{
		}

		/// <summary>
		/// Called when this item is created. The <paramref name="context" /> parameter indicates the context of the item creation and can be used in logic for the desired effect.
		/// <para /> Called on the local client only, except during mod loading on clients and the server where it is called once for every ModItem.
		/// <para /> Known <see cref="T:Terraria.DataStructures.ItemCreationContext" /> include: <see cref="T:Terraria.ModLoader.InitializationItemCreationContext" />, <see cref="T:Terraria.DataStructures.BuyItemCreationContext" />, <see cref="T:Terraria.DataStructures.JourneyDuplicationItemCreationContext" />, and <see cref="T:Terraria.DataStructures.RecipeItemCreationContext" />. Some of these provide additional context such as how <see cref="T:Terraria.DataStructures.RecipeItemCreationContext" /> includes the items consumed to craft this created item.
		/// </summary>
		// Token: 0x060020F1 RID: 8433 RVA: 0x004E4370 File Offset: 0x004E2570
		public virtual void OnCreated(ItemCreationContext context)
		{
		}

		/// <summary>
		/// Automatically sets certain defaults. Override this if you do not want the properties to be set for you.
		/// </summary>
		// Token: 0x060020F2 RID: 8434 RVA: 0x004E4372 File Offset: 0x004E2572
		public virtual void AutoDefaults()
		{
			EquipLoader.SetSlot(this.Item);
		}

		/// <summary>
		/// Automatically sets certain static defaults. Override this if you do not want the properties to be set for you.
		/// </summary>
		// Token: 0x060020F3 RID: 8435 RVA: 0x004E4380 File Offset: 0x004E2580
		public virtual void AutoStaticDefaults()
		{
			TextureAssets.Item[this.Item.type] = ModContent.Request<Texture2D>(this.Texture, 2);
			Asset<Texture2D> flameTexture;
			if (ModContent.RequestIfExists<Texture2D>(this.Texture + "_Flame", out flameTexture, 2))
			{
				TextureAssets.ItemFlame[this.Item.type] = flameTexture;
			}
			this.Item.ResearchUnlockCount = 1;
		}

		/// <summary>
		/// Allows you to manually choose what prefix an item will get.
		/// </summary>
		/// <returns>The ID of the prefix to give the item, -1 to use default vanilla behavior</returns>
		// Token: 0x060020F4 RID: 8436 RVA: 0x004E43E2 File Offset: 0x004E25E2
		public virtual int ChoosePrefix(UnifiedRandom rand)
		{
			return -1;
		}

		/// <summary>
		/// Allows you to change whether or not a weapon receives melee prefixes. Return true if the item should receive melee prefixes and false if it should not.
		/// </summary>
		// Token: 0x060020F5 RID: 8437 RVA: 0x004E43E5 File Offset: 0x004E25E5
		public virtual bool MeleePrefix()
		{
			return this.Item.DamageType.GetsPrefixesFor(DamageClass.Melee) && !this.Item.noUseGraphic;
		}

		/// <summary>
		/// Allows you to change whether or not a weapon receives generic prefixes. Return true if the item should receive generic prefixes and false if it should only receive them from another category.
		/// </summary>
		// Token: 0x060020F6 RID: 8438 RVA: 0x004E440E File Offset: 0x004E260E
		public virtual bool WeaponPrefix()
		{
			return (this.Item.DamageType.GetsPrefixesFor(DamageClass.Melee) && this.Item.noUseGraphic) || this.Item.DamageType.GetsPrefixesFor(DamageClass.Generic);
		}

		/// <summary>
		/// Allows you to change whether or not a weapon receives ranged prefixes. Return true if the item should receive ranged prefixes and false if it should not.
		/// </summary>
		// Token: 0x060020F7 RID: 8439 RVA: 0x004E444B File Offset: 0x004E264B
		public virtual bool RangedPrefix()
		{
			return this.Item.DamageType.GetsPrefixesFor(DamageClass.Ranged);
		}

		/// <summary>
		/// Allows you to change whether or not a weapon receives magic prefixes. Return true if the item should receive magic prefixes and false if it should not.
		/// </summary>
		// Token: 0x060020F8 RID: 8440 RVA: 0x004E4462 File Offset: 0x004E2662
		public virtual bool MagicPrefix()
		{
			return this.Item.DamageType.GetsPrefixesFor(DamageClass.Magic);
		}

		/// <summary>
		/// To prevent putting the item in the tinkerer slot, return false when pre is -3.
		/// To prevent rolling of a prefix on spawn, return false when pre is -1.
		/// To force rolling of a prefix on spawn, return true when pre is -1.
		///
		/// To reduce the probability of a prefix on spawn (pre == -1) to X%, return false 100-4X % of the time.
		/// To increase the probability of a prefix on spawn (pre == -1) to X%, return true (4X-100)/3 % of the time.
		///
		/// To delete a prefix from an item when the item is loaded, return false when pre is the prefix you want to delete.
		/// Use AllowPrefix to prevent rolling of a certain prefix.
		/// </summary>
		/// <param name="pre">The prefix being applied to the item, or the roll mode. -1 is when the item is naturally generated in a chest, crafted, purchased from an NPC, looted from a grab bag (excluding presents), or dropped by a slain enemy (if it's spawned with prefixGiven: -1). -2 is when the item is rolled in the tinkerer. -3 determines if the item can be placed in the tinkerer slot.</param>
		/// <param name="rand">The random number generator class to be used in random choices</param>
		/// <returns></returns>
		// Token: 0x060020F9 RID: 8441 RVA: 0x004E447C File Offset: 0x004E267C
		public virtual bool? PrefixChance(int pre, UnifiedRandom rand)
		{
			return null;
		}

		/// <summary>
		/// Force a re-roll of a prefix by returning false.
		/// </summary>
		// Token: 0x060020FA RID: 8442 RVA: 0x004E4492 File Offset: 0x004E2692
		public virtual bool AllowPrefix(int pre)
		{
			return true;
		}

		/// <summary>
		/// Returns whether or not this item can be used. By default returns true.
		/// <para /> Called on local, server, and remote clients.
		/// <br /><br /> The item may or not be used after this method is called, so logic in this method should have no side effects such as consuming items or resources.
		/// </summary>
		/// <param name="player">The player using the item.</param>
		// Token: 0x060020FB RID: 8443 RVA: 0x004E4495 File Offset: 0x004E2695
		public virtual bool CanUseItem(Player player)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the autoswing (auto-reuse) behavior of this item without having to mess with Item.autoReuse.
		/// <para /> Useful to create effects like the Feral Claws which makes melee weapons and whips auto-reusable.
		/// <para /> Return true to enable autoswing (if not already enabled through autoReuse), return false to prevent autoswing. Returns null by default, which applies vanilla behavior.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player"> The player. </param>
		// Token: 0x060020FC RID: 8444 RVA: 0x004E4498 File Offset: 0x004E2698
		public virtual bool? CanAutoReuseItem(Player player)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the location and rotation of this item in its use animation.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player"> The player. </param>
		/// <param name="heldItemFrame"> The source rectangle for the held item's texture. </param>
		// Token: 0x060020FD RID: 8445 RVA: 0x004E44AE File Offset: 0x004E26AE
		public virtual void UseStyle(Player player, Rectangle heldItemFrame)
		{
		}

		/// <summary>
		/// Allows you to modify the location and rotation of this item when the player is holding it.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player"> The player. </param>
		/// <param name="heldItemFrame"> The source rectangle for the held item's texture. </param>
		// Token: 0x060020FE RID: 8446 RVA: 0x004E44B0 File Offset: 0x004E26B0
		public virtual void HoldStyle(Player player, Rectangle heldItemFrame)
		{
		}

		/// <summary>
		/// Allows you to make things happen when the player is holding this item (for example, torches make light and water candles increase spawn rate).
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x060020FF RID: 8447 RVA: 0x004E44B2 File Offset: 0x004E26B2
		public virtual void HoldItem(Player player)
		{
		}

		/// <summary>
		/// Allows you to change the effective useTime of an item.
		/// <para /> Note that this hook may cause items' actions to run less or more times than they should per a single use.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <returns> The multiplier on the usage time. 1f by default. Values greater than 1 increase the item use's length. </returns>
		// Token: 0x06002100 RID: 8448 RVA: 0x004E44B4 File Offset: 0x004E26B4
		public virtual float UseTimeMultiplier(Player player)
		{
			return 1f;
		}

		/// <summary>
		/// Allows you to change the effective useAnimation of an item.
		/// <para /> Note that this hook may cause items' actions to run less or more times than they should per a single use.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <returns>The multiplier on the animation time. 1f by default. Values greater than 1 increase the item animation's length. </returns>
		// Token: 0x06002101 RID: 8449 RVA: 0x004E44BB File Offset: 0x004E26BB
		public virtual float UseAnimationMultiplier(Player player)
		{
			return 1f;
		}

		/// <summary>
		/// Allows you to safely change both useTime and useAnimation while keeping the values relative to each other.
		/// <para /> Useful for status effects.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <returns> The multiplier on the use speed. 1f by default. Values greater than 1 increase the overall item speed. </returns>
		// Token: 0x06002102 RID: 8450 RVA: 0x004E44C2 File Offset: 0x004E26C2
		public virtual float UseSpeedMultiplier(Player player)
		{
			return 1f;
		}

		/// <summary>
		/// Allows you to temporarily modify the amount of life a life healing item will heal for, based on player buffs, accessories, etc. This is only called for items with a <see cref="F:Terraria.Item.healLife" /> value.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player using the item.</param>
		/// <param name="quickHeal">Whether the item is being used through quick heal or not.</param>
		/// <param name="healValue">The amount of life being healed.</param>
		// Token: 0x06002103 RID: 8451 RVA: 0x004E44C9 File Offset: 0x004E26C9
		public virtual void GetHealLife(Player player, bool quickHeal, ref int healValue)
		{
		}

		/// <summary>
		/// Allows you to temporarily modify the amount of mana a mana healing item will heal for, based on player buffs, accessories, etc. This is only called for items with a <see cref="F:Terraria.Item.healMana" /> value.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player using the item.</param>
		/// <param name="quickHeal">Whether the item is being used through quick heal or not.</param>
		/// <param name="healValue">The amount of mana being healed.</param>
		// Token: 0x06002104 RID: 8452 RVA: 0x004E44CB File Offset: 0x004E26CB
		public virtual void GetHealMana(Player player, bool quickHeal, ref int healValue)
		{
		}

		/// <summary>
		/// Allows you to temporarily modify the amount of mana this item will consume on use, based on player buffs, accessories, etc. This is only called for items with a mana value.
		/// <para /> <b>Do not</b> modify <see cref="F:Terraria.Item.mana" />, modify the <paramref name="reduce" /> and <paramref name="mult" /> parameters.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player using the item.</param>
		/// <param name="reduce">Used for decreasingly stacking buffs (most common). Only ever use -= on this field.</param>
		/// <param name="mult">Use to directly multiply the item's effective mana cost. Good for debuffs, or things which should stack separately (eg meteor armor set bonus).</param>
		// Token: 0x06002105 RID: 8453 RVA: 0x004E44CD File Offset: 0x004E26CD
		public virtual void ModifyManaCost(Player player, ref float reduce, ref float mult)
		{
		}

		/// <summary>
		/// Allows you to make stuff happen when a player doesn't have enough mana for the item they are trying to use.
		/// If the player has high enough mana after this hook runs, mana consumption will happen normally.
		/// Only runs once per item use.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player using the item.</param>
		/// <param name="neededMana">The mana needed to use the item.</param>
		// Token: 0x06002106 RID: 8454 RVA: 0x004E44CF File Offset: 0x004E26CF
		public virtual void OnMissingMana(Player player, int neededMana)
		{
		}

		/// <summary>
		/// Allows you to make stuff happen when a player consumes mana on use of this item.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player using the item.</param>
		/// <param name="manaConsumed">The mana consumed from the player.</param>
		// Token: 0x06002107 RID: 8455 RVA: 0x004E44D1 File Offset: 0x004E26D1
		public virtual void OnConsumeMana(Player player, int manaConsumed)
		{
		}

		/// <summary>
		/// Allows you to dynamically modify a weapon's damage based on player and item conditions.
		/// Can be utilized to modify damage beyond the tools that DamageClass has to offer.
		/// <para /> <b>Do not</b> modify <see cref="F:Terraria.Item.damage" />, modify the <paramref name="damage" /> parameter.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player using the item.</param>
		/// <param name="damage">The StatModifier object representing the totality of the various modifiers to be applied to the item's base damage.</param>
		// Token: 0x06002108 RID: 8456 RVA: 0x004E44D3 File Offset: 0x004E26D3
		public virtual void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
		}

		/// <summary>
		/// Allows you to set an item's sorting group in Journey Mode's duplication menu. This is useful for setting custom item types that group well together, or whenever the default vanilla sorting doesn't sort the way you want it.
		/// <para /> Note that this affects the order of the item in the listing, not which filters the item satisfies.
		/// </summary>
		/// <param name="itemGroup">The item group this item is being assigned to</param>
		// Token: 0x06002109 RID: 8457 RVA: 0x004E44D5 File Offset: 0x004E26D5
		public virtual void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
		{
		}

		/// <summary>
		/// Choose if this item will be consumed or not when used as bait. return null for vanilla behavior.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The Player that owns the bait</param>
		// Token: 0x0600210A RID: 8458 RVA: 0x004E44D8 File Offset: 0x004E26D8
		public virtual bool? CanConsumeBait(Player player)
		{
			return null;
		}

		/// <summary>
		/// Allows you to prevent an item from being researched by returning false. True is the default behavior.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x0600210B RID: 8459 RVA: 0x004E44EE File Offset: 0x004E26EE
		public virtual bool CanResearch()
		{
			return true;
		}

		/// <summary>
		/// Allows you to create custom behavior when an item is accepted by the Research function
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="fullyResearched">True if the item was completely researched, and is ready to be duplicated, false if only partially researched.</param>
		// Token: 0x0600210C RID: 8460 RVA: 0x004E44F1 File Offset: 0x004E26F1
		public virtual void OnResearched(bool fullyResearched)
		{
		}

		/// <summary>
		/// Allows you to dynamically modify a weapon's knockback based on player and item conditions.
		/// Can be utilized to modify damage beyond the tools that DamageClass has to offer.
		/// <para /> <b>Do not</b> modify <see cref="F:Terraria.Item.knockBack" />, modify the <paramref name="knockback" /> parameter.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player using the item.</param>
		/// <param name="knockback">The StatModifier object representing the totality of the various modifiers to be applied to the item's base knockback.</param>
		// Token: 0x0600210D RID: 8461 RVA: 0x004E44F3 File Offset: 0x004E26F3
		public virtual void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
		{
		}

		/// <summary>
		/// Allows you to dynamically modify a weapon's crit chance based on player and item conditions.
		/// Can be utilized to modify damage beyond the tools that DamageClass has to offer.
		/// <para /> <b>Do not</b> modify <see cref="F:Terraria.Item.crit" />, modify the <paramref name="crit" /> parameter.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player using the item.</param>
		/// <param name="crit">The total crit chance of the item after all normal crit chance calculations.</param>
		// Token: 0x0600210E RID: 8462 RVA: 0x004E44F5 File Offset: 0x004E26F5
		public virtual void ModifyWeaponCrit(Player player, ref float crit)
		{
		}

		/// <summary>
		/// Whether or not having no ammo prevents an item that uses ammo from shooting.
		/// Return false to allow shooting with no ammo in the inventory, in which case this item will act as if the default ammo for it is being used.
		/// Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x0600210F RID: 8463 RVA: 0x004E44F7 File Offset: 0x004E26F7
		public virtual bool NeedsAmmo(Player player)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify various properties of the projectile created by a weapon based on the ammo it is using. This hook is called on the ammo.
		/// <para /> Called on local and remote clients when a player picking ammo but only on the local client when held projectiles are picking ammo.
		/// </summary>
		/// <param name="weapon">The item that is using this ammo.</param>
		/// <param name="player">The player using the item.</param>
		/// <param name="type">The ID of the fired projectile.</param>
		/// <param name="speed">The speed of the fired projectile.</param>
		/// <param name="damage">
		/// The damage modifier for the projectile.<br></br>
		/// Total weapon damage is included as Flat damage.<br></br>
		/// Be careful not to apply flat or base damage bonuses which are already applied to the weapon.
		/// </param>
		/// <param name="knockback">The knockback of the fired projectile.</param>
		// Token: 0x06002110 RID: 8464 RVA: 0x004E44FA File Offset: 0x004E26FA
		public virtual void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
		{
		}

		/// <summary>
		/// Whether or not the given ammo item is valid for this weapon. If this, or <see cref="M:Terraria.ModLoader.ModItem.CanBeChosenAsAmmo(Terraria.Item,Terraria.Player)" /> on the ammo, returns false, then the ammo will not be valid for this weapon.
		/// <para /> By default, returns null and allows <see cref="F:Terraria.Item.useAmmo" /> and <see cref="F:Terraria.Item.ammo" /> to decide. Return true to make the ammo valid regardless of these fields, and return false to make it invalid.
		/// <para /> If false is returned, the <see cref="M:Terraria.ModLoader.ModItem.CanConsumeAmmo(Terraria.Item,Terraria.Player)" />, <see cref="M:Terraria.ModLoader.ModItem.CanBeConsumedAsAmmo(Terraria.Item,Terraria.Player)" />, <see cref="M:Terraria.ModLoader.ModItem.OnConsumeAmmo(Terraria.Item,Terraria.Player)" />, and <see cref="M:Terraria.ModLoader.ModItem.OnConsumedAsAmmo(Terraria.Item,Terraria.Player)" /> hooks are never called.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="ammo">The ammo that the weapon is attempting to select.</param>
		/// <param name="player">The player which this weapon and the potential ammo belong to.</param>
		/// <returns></returns>
		// Token: 0x06002111 RID: 8465 RVA: 0x004E44FC File Offset: 0x004E26FC
		public virtual bool? CanChooseAmmo(Item ammo, Player player)
		{
			return null;
		}

		/// <summary>
		/// Whether or not this ammo item is valid for the given weapon. If this, or <see cref="M:Terraria.ModLoader.ModItem.CanChooseAmmo(Terraria.Item,Terraria.Player)" /> on the weapon, returns false, then the ammo will not be valid for this weapon.
		/// <para /> By default, returns null and allows <see cref="F:Terraria.Item.useAmmo" /> and <see cref="F:Terraria.Item.ammo" /> to decide. Return true to make the ammo valid regardless of these fields, and return false to make it invalid.
		/// <para /> If false is returned, the <see cref="M:Terraria.ModLoader.ModItem.CanConsumeAmmo(Terraria.Item,Terraria.Player)" />, <see cref="M:Terraria.ModLoader.ModItem.CanBeConsumedAsAmmo(Terraria.Item,Terraria.Player)" />, <see cref="M:Terraria.ModLoader.ModItem.OnConsumeAmmo(Terraria.Item,Terraria.Player)" />, and <see cref="M:Terraria.ModLoader.ModItem.OnConsumedAsAmmo(Terraria.Item,Terraria.Player)" /> hooks are never called.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="weapon">The weapon attempting to select the ammo.</param>
		/// <param name="player">The player which the weapon and this potential ammo belong to.</param>
		/// <returns></returns>
		// Token: 0x06002112 RID: 8466 RVA: 0x004E4514 File Offset: 0x004E2714
		public virtual bool? CanBeChosenAsAmmo(Item weapon, Player player)
		{
			return null;
		}

		/// <summary>
		/// Whether or not the given ammo item will be consumed by this weapon.
		/// <para /> By default, returns true; return false to prevent ammo consumption.
		/// <para /> If false is returned, the <see cref="M:Terraria.ModLoader.ModItem.OnConsumeAmmo(Terraria.Item,Terraria.Player)" /> and <see cref="M:Terraria.ModLoader.ModItem.OnConsumedAsAmmo(Terraria.Item,Terraria.Player)" /> hooks are never called.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="ammo">The ammo that the weapon is attempting to consume.</param>
		/// <param name="player">The player which this weapon and the ammo belong to.</param>
		/// <returns></returns>
		// Token: 0x06002113 RID: 8467 RVA: 0x004E452A File Offset: 0x004E272A
		public virtual bool CanConsumeAmmo(Item ammo, Player player)
		{
			return true;
		}

		/// <summary>
		/// Whether or not this ammo item will be consumed by the given weapon.
		/// <para /> By default, returns true; return false to prevent ammo consumption.
		/// <para /> If false is returned, the <see cref="M:Terraria.ModLoader.ModItem.OnConsumeAmmo(Terraria.Item,Terraria.Player)" /> and <see cref="M:Terraria.ModLoader.ModItem.OnConsumedAsAmmo(Terraria.Item,Terraria.Player)" /> hooks are never called.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="weapon">The weapon attempting to consume the ammo.</param>
		/// <param name="player">The player which the weapon and this ammo belong to.</param>
		/// <returns></returns>
		// Token: 0x06002114 RID: 8468 RVA: 0x004E452D File Offset: 0x004E272D
		public virtual bool CanBeConsumedAsAmmo(Item weapon, Player player)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make things happen when the given ammo is consumed by this weapon.
		/// <para /> Called before the ammo stack is reduced, and is never called if the ammo isn't consumed in the first place.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="ammo">The ammo that this weapon is currently using.</param>
		/// <param name="player">The player which this weapon and the ammo belong to.</param>
		// Token: 0x06002115 RID: 8469 RVA: 0x004E4530 File Offset: 0x004E2730
		public virtual void OnConsumeAmmo(Item ammo, Player player)
		{
		}

		/// <summary>
		/// Allows you to make things happen when this ammo is consumed by the given weapon.
		/// <para /> Called before the ammo stack is reduced, and is never called if the ammo isn't consumed in the first place.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="weapon">The weapon that is currently using this ammo.</param>
		/// <param name="player">The player which the weapon and this ammo belong to.</param>
		// Token: 0x06002116 RID: 8470 RVA: 0x004E4532 File Offset: 0x004E2732
		public virtual void OnConsumedAsAmmo(Item weapon, Player player)
		{
		}

		/// <summary>
		/// Allows you to prevent this item from shooting a projectile on use. Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player"> The player using the item. </param>
		/// <returns></returns>
		// Token: 0x06002117 RID: 8471 RVA: 0x004E4534 File Offset: 0x004E2734
		public virtual bool CanShoot(Player player)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the position, velocity, type, damage and/or knockback of a projectile being shot by this item.
		/// <para /> These parameters will be provided to <see cref="M:Terraria.ModLoader.ModItem.Shoot(Terraria.Player,Terraria.DataStructures.EntitySource_ItemUse_WithAmmo,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32,System.Single)" /> where the projectile will actually be spawned.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player"> The player using the item. </param>
		/// <param name="position"> The center position of the projectile. </param>
		/// <param name="velocity"> The velocity of the projectile. </param>
		/// <param name="type"> The ID of the projectile. </param>
		/// <param name="damage"> The damage of the projectile. </param>
		/// <param name="knockback"> The knockback of the projectile. </param>
		// Token: 0x06002118 RID: 8472 RVA: 0x004E4537 File Offset: 0x004E2737
		public virtual void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
		}

		/// <summary>
		/// Allows you to modify this item's shooting mechanism. Return false to prevent vanilla's shooting code from running. Returns true by default.
		/// <para /> This method is called after the <see cref="M:Terraria.ModLoader.ModItem.ModifyShootStats(Terraria.Player,Microsoft.Xna.Framework.Vector2@,Microsoft.Xna.Framework.Vector2@,System.Int32@,System.Int32@,System.Single@)" /> hook has had a chance to adjust the spawn parameters.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player"> The player using the item. </param>
		/// <param name="source"> The projectile source's information. </param>
		/// <param name="position"> The center position of the projectile. </param>
		/// <param name="velocity"> The velocity of the projectile. </param>
		/// <param name="type"> The ID of the projectile. </param>
		/// <param name="damage"> The damage of the projectile. </param>
		/// <param name="knockback"> The knockback of the projectile. </param>
		/// <returns></returns>
		// Token: 0x06002119 RID: 8473 RVA: 0x004E4539 File Offset: 0x004E2739
		public virtual bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			return true;
		}

		/// <summary>
		/// Changes the hitbox of this melee weapon when it is used.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="hitbox">The hitbox.</param>
		/// <param name="noHitbox">if set to <c>true</c> [no hitbox].</param>
		// Token: 0x0600211A RID: 8474 RVA: 0x004E453C File Offset: 0x004E273C
		public virtual void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
		}

		/// <summary>
		/// Allows you to give this melee weapon special effects, such as creating light or dust.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="hitbox">The hitbox.</param>
		// Token: 0x0600211B RID: 8475 RVA: 0x004E453E File Offset: 0x004E273E
		public virtual void MeleeEffects(Player player, Rectangle hitbox)
		{
		}

		/// <summary>
		/// Allows you to determine whether this item can catch the given NPC.
		/// <para /> Return true or false to say the given NPC can or cannot be caught, respectively, regardless of vanilla rules.
		/// <para /> Returns null by default, which allows vanilla's NPC catching rules to decide the target's fate.
		/// <para /> If this returns false, <see cref="M:Terraria.ModLoader.CombinedHooks.OnCatchNPC(Terraria.Player,Terraria.NPC,Terraria.Item,System.Boolean)" /> is never called.
		/// <para /> NOTE: this does not classify the given item as an NPC-catching tool, which is necessary for catching NPCs in the first place. To do that, you will need to use <see cref="F:Terraria.ID.ItemID.Sets.CatchingTool" />.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="target">The NPC the player is trying to catch.</param>
		/// <param name="player">The player attempting to catch the NPC.</param>
		/// <returns></returns>
		// Token: 0x0600211C RID: 8476 RVA: 0x004E4540 File Offset: 0x004E2740
		public virtual bool? CanCatchNPC(NPC target, Player player)
		{
			return null;
		}

		/// <summary>
		/// Allows you to make things happen when this item attempts to catch the given NPC.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc">The NPC which the player attempted to catch.</param>
		/// <param name="player">The player attempting to catch the given NPC.</param>
		/// <param name="failed">Whether or not the given NPC has been successfully caught.</param>
		// Token: 0x0600211D RID: 8477 RVA: 0x004E4556 File Offset: 0x004E2756
		public virtual void OnCatchNPC(NPC npc, Player player, bool failed)
		{
		}

		/// <summary>
		/// Allows you to dynamically modify this item's size for the given player, similarly to the effect of the Titan Glove.
		/// <para /> <b>Do not</b> modify <see cref="F:Terraria.Item.scale" />, modify the <paramref name="scale" /> parameter.
		/// <para /> Called on local and remote clients
		/// </summary>
		/// <param name="player">The player wielding this item.</param>
		/// <param name="scale">
		/// The scale multiplier to be applied to this item.<br></br>
		/// Will be 1.1 if the Titan Glove is equipped, and 1 otherwise.
		/// </param>
		// Token: 0x0600211E RID: 8478 RVA: 0x004E4558 File Offset: 0x004E2758
		public virtual void ModifyItemScale(Player player, ref float scale)
		{
		}

		/// <summary>
		/// Allows you to determine whether this melee weapon can hit the given NPC when swung. Return true to allow hitting the target, return false to block this weapon from hitting the target, and return null to use the vanilla code for whether the target can be hit. Returns null by default.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="target">The target.</param>
		/// <returns></returns>
		// Token: 0x0600211F RID: 8479 RVA: 0x004E455C File Offset: 0x004E275C
		public virtual bool? CanHitNPC(Player player, NPC target)
		{
			return null;
		}

		/// <summary>
		/// Allows you to determine whether a melee weapon can collide with the given NPC when swung.
		/// <para /> Use <see cref="M:Terraria.ModLoader.ModItem.CanHitNPC(Terraria.Player,Terraria.NPC)" /> instead for Flymeal-type effects.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="meleeAttackHitbox">Hitbox of melee attack.</param>
		/// <param name="player">The player wielding this item.</param>
		/// <param name="target">The target npc.</param>
		/// <returns>
		/// Return true to allow colliding with target, return false to block the weapon from colliding with target, and return null to use the vanilla code for whether the target can be colliding. Returns null by default.
		/// </returns>
		// Token: 0x06002120 RID: 8480 RVA: 0x004E4574 File Offset: 0x004E2774
		public virtual bool? CanMeleeAttackCollideWithNPC(Rectangle meleeAttackHitbox, Player player, NPC target)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that this melee weapon does to an NPC.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="target">The target.</param>
		/// <param name="modifiers">The strike.</param>
		// Token: 0x06002121 RID: 8481 RVA: 0x004E458A File Offset: 0x004E278A
		public virtual void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this melee weapon hits an NPC (for example how the Pumpkin Sword creates pumpkin heads).
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="target">The target.</param>
		/// <param name="hit">The strike.</param>
		/// <param name="damageDone">The actual damage dealt to/taken by the NPC.</param>
		// Token: 0x06002122 RID: 8482 RVA: 0x004E458C File Offset: 0x004E278C
		public virtual void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to determine whether this melee weapon can hit the given opponent player when swung. Return false to block this weapon from hitting the target. Returns true by default.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="target">The target.</param>
		/// <returns>
		///   <c>true</c> if this instance [can hit PVP] the specified player; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06002123 RID: 8483 RVA: 0x004E458E File Offset: 0x004E278E
		public virtual bool CanHitPvp(Player player, Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, etc., that this melee weapon does to a player.
		/// <para /> Called on the client taking damage.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="target">The target.</param>
		/// <param name="modifiers"></param>
		// Token: 0x06002124 RID: 8484 RVA: 0x004E4591 File Offset: 0x004E2791
		public virtual void ModifyHitPvp(Player player, Player target, ref Player.HurtModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this melee weapon hits a player.
		/// <para /> Called on the client taking damage.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="target">The target.</param>
		/// <param name="hurtInfo"></param>
		// Token: 0x06002125 RID: 8485 RVA: 0x004E4593 File Offset: 0x004E2793
		public virtual void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
		{
		}

		/// <summary>
		/// Allows you to make things happen when this item is used. The return value controls whether or not ApplyItemTime will be called for the player.
		/// <para /> Return true if the item actually did something, to force itemTime.
		/// <para /> Return false to keep itemTime at 0.
		/// <para /> Return null for vanilla behavior.
		/// <para /> Called on local, server, and remote clients.
		/// <br /><br /> Note the for right-click actions, this is currently only called on the local client.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <returns></returns>
		// Token: 0x06002126 RID: 8486 RVA: 0x004E4598 File Offset: 0x004E2798
		public virtual bool? UseItem(Player player)
		{
			return null;
		}

		/// <summary>
		/// Allows you to make things happen when this item's use animation starts.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player"> The player. </param>
		// Token: 0x06002127 RID: 8487 RVA: 0x004E45AE File Offset: 0x004E27AE
		public virtual void UseAnimation(Player player)
		{
		}

		/// <summary>
		/// If this item is consumable and this returns true, then this item will be consumed upon usage. Returns true by default.
		/// If false is returned, the OnConsumeItem hook is never called.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <returns></returns>
		// Token: 0x06002128 RID: 8488 RVA: 0x004E45B0 File Offset: 0x004E27B0
		public virtual bool ConsumeItem(Player player)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make things happen when this item is consumed.
		/// Called before the item stack is reduced.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x06002129 RID: 8489 RVA: 0x004E45B3 File Offset: 0x004E27B3
		public virtual void OnConsumeItem(Player player)
		{
		}

		/// <summary>
		/// Allows you to modify the player's animation when this item is being used.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x0600212A RID: 8490 RVA: 0x004E45B5 File Offset: 0x004E27B5
		public virtual void UseItemFrame(Player player)
		{
		}

		/// <summary>
		/// Allows you to modify the player's animation when the player is holding this item.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x0600212B RID: 8491 RVA: 0x004E45B7 File Offset: 0x004E27B7
		public virtual void HoldItemFrame(Player player)
		{
		}

		/// <summary>
		/// Allows you to make this item usable by right-clicking. When this item is used by right-clicking, <see cref="F:Terraria.Player.altFunctionUse" /> will be set to 2. Check the value of altFunctionUse in <see cref="M:Terraria.ModLoader.ModItem.UseItem(Terraria.Player)" /> to apply right-click specific logic. For auto-reusing through right clicking, see also <see cref="F:Terraria.ID.ItemID.Sets.ItemsThatAllowRepeatedRightClick" />.
		/// <para /> Returns false by default.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <returns></returns>
		// Token: 0x0600212C RID: 8492 RVA: 0x004E45B9 File Offset: 0x004E27B9
		public virtual bool AltFunctionUse(Player player)
		{
			return false;
		}

		/// <summary>
		/// Allows you to make things happen when this item is in the player's inventory. This should NOT be used for information accessories;
		/// use <seealso cref="M:Terraria.ModLoader.ModItem.UpdateInfoAccessory(Terraria.Player)" /> for those instead.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x0600212D RID: 8493 RVA: 0x004E45BC File Offset: 0x004E27BC
		public virtual void UpdateInventory(Player player)
		{
		}

		/// <summary>
		/// Allows you to set information accessory fields with the passed in player argument. This hook should only be used for information
		/// accessory fields such as the Radar, Lifeform Analyzer, and others. Using it for other fields will likely cause weird side-effects.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player"> The player to be affected the information accessory. </param>
		// Token: 0x0600212E RID: 8494 RVA: 0x004E45BE File Offset: 0x004E27BE
		public virtual void UpdateInfoAccessory(Player player)
		{
		}

		/// <summary>
		/// Allows you to give effects to this armor or accessory, such as increased damage.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x0600212F RID: 8495 RVA: 0x004E45C0 File Offset: 0x004E27C0
		public virtual void UpdateEquip(Player player)
		{
		}

		/// <summary>
		/// Allows you to give effects to this accessory. The hideVisual parameter is whether the player has marked the accessory slot to be hidden from being drawn on the player.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="hideVisual">if set to <c>true</c> the accessory is hidden.</param>
		// Token: 0x06002130 RID: 8496 RVA: 0x004E45C2 File Offset: 0x004E27C2
		public virtual void UpdateAccessory(Player player, bool hideVisual)
		{
		}

		/// <summary>
		/// Allows you to give effects to this accessory when equipped in a vanity slot. Vanilla uses this for boot effects, wings and merman/werewolf visual flags
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x06002131 RID: 8497 RVA: 0x004E45C4 File Offset: 0x004E27C4
		public virtual void UpdateVanity(Player player)
		{
		}

		/// <summary>
		/// Allows you to set custom draw flags for this accessory that can be checked in a <see cref="T:Terraria.ModLoader.PlayerDrawLayer" /> or other drawcode. Not required if using pre-existing layers (e.g. face, back).
		/// <para /> <paramref name="hideVisual" /> indicates if the accessory is hidden (in a non-vanity accessory slot that is set to hidden). It sounds counterintuitive for this method to be called on hidden accessories, but this can be used for effects where the visuals of an accessory should be forced despite the player hiding the accessory. For example, wings will always show while in the air and the Shield of Cthulhu will always show while its dash is active even while hidden.
		/// </summary>
		// Token: 0x06002132 RID: 8498 RVA: 0x004E45C6 File Offset: 0x004E27C6
		public virtual void UpdateVisibleAccessory(Player player, bool hideVisual)
		{
		}

		/// <summary>
		/// Allows tracking custom shader values corresponding to specific items or custom player layers for equipped accessories. <paramref name="dye" /> is the <see cref="F:Terraria.Item.dye" /> of the item in the dye slot. <paramref name="hideVisual" /> indicates if this item is in a non-vanity accessory slot that is set to hidden. Most implementations will not assign shaders if the accessory is hidden, but there are rare cases where it is desired to assign the shader regardless of accessory visibility. One example is Hand Of Creation, the player can disable visibility of the accessory to prevent the backpack visuals from showing, but the stool will still be properly dyed by the corresponding dye item when visible.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="dye"></param>
		/// <param name="hideVisual"></param>
		// Token: 0x06002133 RID: 8499 RVA: 0x004E45C8 File Offset: 0x004E27C8
		public virtual void UpdateItemDye(Player player, int dye, bool hideVisual)
		{
		}

		/// <summary>
		/// Allows you to create special effects (such as dust) when this item's equipment texture of the given equipment type is displayed on the player. Note that this hook is only ever called through this item's associated equipment texture.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="type">The type.</param>
		// Token: 0x06002134 RID: 8500 RVA: 0x004E45CA File Offset: 0x004E27CA
		public virtual void EquipFrameEffects(Player player, EquipType type)
		{
		}

		/// <summary>
		/// Returns whether or not the head armor, body armor, and leg armor make up a set. If this returns true, then this item's UpdateArmorSet method will be called. Returns false by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="head">The head.</param>
		/// <param name="body">The body.</param>
		/// <param name="legs">The legs.</param>
		// Token: 0x06002135 RID: 8501 RVA: 0x004E45CC File Offset: 0x004E27CC
		public virtual bool IsArmorSet(Item head, Item body, Item legs)
		{
			return false;
		}

		/// <summary>
		/// Allows you to give set bonuses to the armor set that this armor is in. Set player.setBonus to a string for the bonus description.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x06002136 RID: 8502 RVA: 0x004E45CF File Offset: 0x004E27CF
		public virtual void UpdateArmorSet(Player player)
		{
		}

		/// <summary>
		/// Returns whether or not the head armor, body armor, and leg armor textures make up a set. This hook is used for the PreUpdateVanitySet, UpdateVanitySet, and ArmorSetShadows hooks. By default, this will return the same value as the IsArmorSet hook (passing the equipment textures' associated items as parameters), so you will not have to use this hook unless you want vanity effects to be entirely separate from armor sets. Note that this hook is only ever called through this item's associated equipment texture.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="head">The head.</param>
		/// <param name="body">The body.</param>
		/// <param name="legs">The legs.</param>
		// Token: 0x06002137 RID: 8503 RVA: 0x004E45D4 File Offset: 0x004E27D4
		public virtual bool IsVanitySet(int head, int body, int legs)
		{
			int headItemType = 0;
			if (head >= 0)
			{
				headItemType = Item.headType[head];
			}
			Item headItem = ContentSamples.ItemsByType[headItemType];
			int bodyItemType = 0;
			if (body >= 0)
			{
				bodyItemType = Item.bodyType[body];
			}
			Item bodyItem = ContentSamples.ItemsByType[bodyItemType];
			int legsItemType = 0;
			if (legs >= 0)
			{
				legsItemType = Item.legType[legs];
			}
			Item legItem = ContentSamples.ItemsByType[legsItemType];
			return this.IsArmorSet(headItem, bodyItem, legItem);
		}

		/// <summary>
		/// Allows you to create special effects (such as the necro armor's hurt noise) when the player wears this item's vanity set. This hook is called regardless of whether the player is frozen in any way. Note that this hook is only ever called through this item's associated equipment texture.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x06002138 RID: 8504 RVA: 0x004E463D File Offset: 0x004E283D
		public virtual void PreUpdateVanitySet(Player player)
		{
		}

		/// <summary>
		/// Allows you to create special effects (such as dust) when the player wears this item's vanity set. This hook will only be called if the player is not frozen in any way. Note that this hook is only ever called through this item's associated equipment texture.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x06002139 RID: 8505 RVA: 0x004E463F File Offset: 0x004E283F
		public virtual void UpdateVanitySet(Player player)
		{
		}

		/// <summary>
		/// Allows you to determine special visual effects this vanity set has on the player without having to code them yourself. Note that this hook is only ever called through this item's associated equipment texture. Use the player.armorEffectDraw bools to activate the desired effects.
		/// <para /> Called on local, server, and remote clients.
		/// <example><code>player.armorEffectDrawShadow = true;</code></example>
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x0600213A RID: 8506 RVA: 0x004E4641 File Offset: 0x004E2841
		public virtual void ArmorSetShadows(Player player)
		{
		}

		/// <summary>
		/// Allows you to modify the equipment that the player appears to be wearing. This is most commonly used to add legs to robes and for swapping to female variant textures if <paramref name="male" /> is false for head and leg armor. This hook will only be called for head armor, body armor, and leg armor. Note that equipSlot is not the same as the item type of the armor the player will appear to be wearing. Worn equipment has a separate set of IDs. You can find the vanilla equipment IDs by looking at the headSlot, bodySlot, and legSlot fields for items, and modded equipment IDs by looking at EquipLoader.
		/// <para /> If this hook is called on body armor, equipSlot allows you to modify the leg armor the player appears to be wearing. If you modify it, make sure to set robes to true. If this hook is called on leg armor, equipSlot allows you to modify the leg armor the player appears to be wearing, and the robes parameter is useless. The same is true for head armor.
		/// <para /> Note that this hook is only ever called through this item's associated equipment texture.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="male">if set to <c>true</c> [male].</param>
		/// <param name="equipSlot">The equip slot.</param>
		/// <param name="robes">if set to <c>true</c> [robes].</param>
		// Token: 0x0600213B RID: 8507 RVA: 0x004E4643 File Offset: 0x004E2843
		public virtual void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
		}

		/// <summary>
		/// Returns whether or not this item does something when it is right-clicked in the inventory. Returns false by default.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x0600213C RID: 8508 RVA: 0x004E4645 File Offset: 0x004E2845
		public virtual bool CanRightClick()
		{
			return false;
		}

		/// <summary>
		/// Allows you to make things happen when this item is right-clicked in the inventory. By default this will consume the item by 1 stack, so return false in <see cref="M:Terraria.ModLoader.ModItem.ConsumeItem(Terraria.Player)" /> if that behavior is undesired.
		/// <para /> This is only called if the item can be right-clicked, meaning <see cref="F:Terraria.ID.ItemID.Sets.OpenableBag" /> is true for the item type or either <see cref="M:Terraria.ModLoader.ModItem.CanRightClick" /> or <see cref="M:Terraria.ModLoader.GlobalItem.CanRightClick(Terraria.Item)" /> return true.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x0600213D RID: 8509 RVA: 0x004E4648 File Offset: 0x004E2848
		public virtual void RightClick(Player player)
		{
		}

		/// <summary>
		/// Allows you to add and modify the loot items that spawn from bag items when opened.
		/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4">Basic NPC Drops and Loot 1.4 Guide</see> explains how to use the <see cref="M:Terraria.ModLoader.ModNPC.ModifyNPCLoot(Terraria.ModLoader.NPCLoot)" /> hook to modify NPC loot as well as this hook. A common usage is to use this hook and <see cref="M:Terraria.ModLoader.ModNPC.ModifyNPCLoot(Terraria.ModLoader.NPCLoot)" /> to edit non-expert exclusive drops for bosses.
		/// <br /> This hook only runs once during mod loading, any dynamic behavior must be contained in the rules themselves.
		/// </summary>
		/// <param name="itemLoot">A reference to the item drop database for this item type</param>
		// Token: 0x0600213E RID: 8510 RVA: 0x004E464A File Offset: 0x004E284A
		public virtual void ModifyItemLoot(ItemLoot itemLoot)
		{
		}

		/// <summary>
		/// Allows you to decide if this item is allowed to stack with another of its type.
		/// <para /> This is only called when attempting to stack with an item of the same type.
		/// <para /> This is not called for coins in inventory/UI.
		/// <para /> This covers all scenarios, if you just need to change in-world stacking behavior, use <see cref="M:Terraria.ModLoader.ModItem.CanStackInWorld(Terraria.Item)" />.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="source">The item instance being stacked onto this item</param>
		/// <returns>Whether or not the item is allowed to stack</returns>
		// Token: 0x0600213F RID: 8511 RVA: 0x004E464C File Offset: 0x004E284C
		public virtual bool CanStack(Item source)
		{
			return true;
		}

		/// <summary>
		/// Allows you to decide if this item is allowed to stack with another of its type in the world.
		/// <para /> This is only called when attempting to stack with an item of the same type.
		/// <para /> Called on the local client or server, depending on who the item is reserved for.
		/// </summary>
		/// <param name="source">The item instance being stacked onto this item</param>
		/// <returns>Whether or not the item is allowed to stack</returns>
		// Token: 0x06002140 RID: 8512 RVA: 0x004E464F File Offset: 0x004E284F
		public virtual bool CanStackInWorld(Item source)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make things happen when items stack together.
		/// <para /> This hook is called on item being stacked onto from <paramref name="source" /> and before the items are transferred
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="source">The item instance being stacked onto this item</param>
		/// <param name="numToTransfer">The quantity of <paramref name="source" /> that will be transferred to this item</param>
		// Token: 0x06002141 RID: 8513 RVA: 0x004E4652 File Offset: 0x004E2852
		public virtual void OnStack(Item source, int numToTransfer)
		{
		}

		/// <summary>
		/// Allows you to make things happen when an item stack is split. This hook is called before the stack values are modified.
		/// <para /> This item is the item clone being stacked onto from <paramref name="source" /> and always has a stack of zero.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="source">The original item that will have it's stack reduced.</param>
		/// <param name="numToTransfer">The quantity of <paramref name="source" /> that will be transferred to this item</param>
		// Token: 0x06002142 RID: 8514 RVA: 0x004E4654 File Offset: 0x004E2854
		public virtual void SplitStack(Item source, int numToTransfer)
		{
		}

		/// <summary>
		/// Returns if the normal reforge pricing is applied.
		/// If true or false is returned and the price is altered, the price will equal the altered price.
		/// The passed reforge price equals the Item.value. Vanilla pricing will apply 20% discount if applicable and then price the reforge at a third of that value.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x06002143 RID: 8515 RVA: 0x004E4656 File Offset: 0x004E2856
		public virtual bool ReforgePrice(ref int reforgePrice, ref bool canApplyDiscount)
		{
			return true;
		}

		/// <summary>
		/// This hook gets called when the player clicks on the reforge button and can afford the reforge.
		/// Returns whether the reforge will take place. If false is returned by this or any GlobalItem, the item will not be reforged, the cost to reforge will not be paid, and PreReforge and PostReforge hooks will not be called.
		/// Reforging preserves modded data on the item.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x06002144 RID: 8516 RVA: 0x004E4659 File Offset: 0x004E2859
		public virtual bool CanReforge()
		{
			return true;
		}

		/// <summary>
		/// This hook gets called immediately before an item gets reforged by the Goblin Tinkerer.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x06002145 RID: 8517 RVA: 0x004E465C File Offset: 0x004E285C
		public virtual void PreReforge()
		{
		}

		/// <summary>
		/// This hook gets called immediately after an item gets reforged by the Goblin Tinkerer.
		/// Useful for modifying modded data based on the reforge result.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x06002146 RID: 8518 RVA: 0x004E465E File Offset: 0x004E285E
		public virtual void PostReforge()
		{
		}

		/// <summary>
		/// Allows you to modify the colors in which this armor and surrounding accessories are drawn, in addition to which glow mask and in what color is drawn. Note that this hook is only ever called through this item's associated equipment texture.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="drawPlayer">The draw player.</param>
		/// <param name="shadow">The shadow.</param>
		/// <param name="color">The color.</param>
		/// <param name="glowMask">The glow mask.</param>
		/// <param name="glowMaskColor">Color of the glow mask.</param>
		// Token: 0x06002147 RID: 8519 RVA: 0x004E4660 File Offset: 0x004E2860
		public virtual void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
		{
		}

		/// <summary>
		/// Allows you to modify which glow mask and in what color is drawn on the player's arms. Note that this is only called for body armor. Also note that this hook is only ever called through this item's associated equipment texture.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="drawPlayer">The draw player.</param>
		/// <param name="shadow">The shadow.</param>
		/// <param name="glowMask">The glow mask.</param>
		/// <param name="color">The color.</param>
		// Token: 0x06002148 RID: 8520 RVA: 0x004E4662 File Offset: 0x004E2862
		public virtual void ArmorArmGlowMask(Player drawPlayer, float shadow, ref int glowMask, ref Color color)
		{
		}

		/// <summary>
		/// Allows you to modify the speeds at which you rise and fall when these wings are equipped.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="ascentWhenFalling">The ascent when falling.</param>
		/// <param name="ascentWhenRising">The ascent when rising.</param>
		/// <param name="maxCanAscendMultiplier">The maximum can ascend multiplier.</param>
		/// <param name="maxAscentMultiplier">The maximum ascent multiplier.</param>
		/// <param name="constantAscend">The constant ascend.</param>
		// Token: 0x06002149 RID: 8521 RVA: 0x004E4664 File Offset: 0x004E2864
		public virtual void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
		}

		/// <summary>
		/// Allows you to modify these wing's horizontal flight speed and acceleration.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="speed">The speed.</param>
		/// <param name="acceleration">The acceleration.</param>
		// Token: 0x0600214A RID: 8522 RVA: 0x004E4666 File Offset: 0x004E2866
		public virtual void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
		}

		/// <summary>
		/// Allows for Wings to do various things while in use. "inUse" is whether or not the jump button is currently pressed. Called when these wings visually appear on the player. Use to animate wings, create dusts, invoke sounds, and create lights. Note that this hook is only ever called through this item's associated equipment texture. False will keep everything the same. True, you need to handle all animations in your own code.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="inUse">if set to <c>true</c> [in use].</param>
		/// <returns></returns>
		// Token: 0x0600214B RID: 8523 RVA: 0x004E4668 File Offset: 0x004E2868
		public virtual bool WingUpdate(Player player, bool inUse)
		{
			return false;
		}

		/// <summary>
		/// Allows you to customize this item's movement when lying in the world. Note that this will not be called if this item is currently being grabbed by a player.
		/// <para /> Called on all clients and the server.
		/// </summary>
		/// <param name="gravity">The gravity.</param>
		/// <param name="maxFallSpeed">The maximum fall speed.</param>
		// Token: 0x0600214C RID: 8524 RVA: 0x004E466B File Offset: 0x004E286B
		public virtual void Update(ref float gravity, ref float maxFallSpeed)
		{
		}

		/// <summary>
		/// Allows you to make things happen when this item is lying in the world. This will always be called, even when it is being grabbed by a player. This hook should be used for adding light, or for increasing the age of less valuable items.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x0600214D RID: 8525 RVA: 0x004E466D File Offset: 0x004E286D
		public virtual void PostUpdate()
		{
		}

		/// <summary>
		/// Allows you to modify how close this item must be to the player in order to move towards the player.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="grabRange">The grab range.</param>
		// Token: 0x0600214E RID: 8526 RVA: 0x004E466F File Offset: 0x004E286F
		public virtual void GrabRange(Player player, ref int grabRange)
		{
		}

		/// <summary>
		/// Allows you to modify the way this item moves towards the player. Return true if you override this hook; returning false will allow the vanilla grab style to take place. Returns false by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <returns></returns>
		// Token: 0x0600214F RID: 8527 RVA: 0x004E4671 File Offset: 0x004E2871
		public virtual bool GrabStyle(Player player)
		{
			return false;
		}

		/// <summary>
		/// Allows you to determine whether or not the item can be picked up
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		// Token: 0x06002150 RID: 8528 RVA: 0x004E4674 File Offset: 0x004E2874
		public virtual bool CanPickup(Player player)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make special things happen when the player picks up this item. Return false to stop the item from being added to the player's inventory; returns true by default.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <returns></returns>
		// Token: 0x06002151 RID: 8529 RVA: 0x004E4677 File Offset: 0x004E2877
		public virtual bool OnPickup(Player player)
		{
			return true;
		}

		/// <summary>
		/// Return true to specify that the item can be picked up despite not having enough room in inventory. Useful for something like hearts or experience items. Use in conjunction with OnPickup to actually consume the item and handle it.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <returns></returns>
		// Token: 0x06002152 RID: 8530 RVA: 0x004E467A File Offset: 0x004E287A
		public virtual bool ItemSpace(Player player)
		{
			return false;
		}

		/// <summary>
		/// Allows you to determine the color and transparency in which this item is drawn. Return null to use the default color (normally light color). Returns null by default.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="lightColor">Color of the light.</param>
		/// <returns></returns>
		// Token: 0x06002153 RID: 8531 RVA: 0x004E4680 File Offset: 0x004E2880
		public virtual Color? GetAlpha(Color lightColor)
		{
			return null;
		}

		/// <summary>
		/// Allows you to draw things behind this item, or to modify the way this item is drawn in the world. Return false to stop the game from drawing the item (useful if you're manually drawing the item).
		/// <para /> Note that items in the world are drawn centered horizontally sitting at the bottom of the item hitbox, not in the center of the hitbox. To replicate the normal drawing calculations, use the following and then use <see cref="M:Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(Microsoft.Xna.Framework.Graphics.SpriteFont,System.String,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color,System.Single,Microsoft.Xna.Framework.Vector2,System.Single,Microsoft.Xna.Framework.Graphics.SpriteEffects,System.Single)" />:
		/// <para /> Called on all clients.
		/// <code>
		/// Main.GetItemDrawFrame(Item.type, out var itemTexture, out var itemFrame);
		/// Vector2 drawOrigin = itemFrame.Size() / 2f;
		/// Vector2 drawPosition = Item.Bottom - Main.screenPosition - new Vector2(0, drawOrigin.Y);
		/// </code>
		/// <para /> Returns true by default.
		/// </summary>
		/// <param name="spriteBatch">The sprite batch.</param>
		/// <param name="lightColor">Color of the light.</param>
		/// <param name="alphaColor">Color of the alpha.</param>
		/// <param name="rotation">The item rotation. Items rotate slightly as they are thrown.</param>
		/// <param name="scale">The draw scale. Items are usually drawn in the world at a scale of 1f but some effects like pulsing Soul items change this.</param>
		/// <param name="whoAmI">The <see cref="F:Terraria.Entity.whoAmI" />.</param>
		/// <returns></returns>
		// Token: 0x06002154 RID: 8532 RVA: 0x004E4696 File Offset: 0x004E2896
		public virtual bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this item. This method is called even if PreDrawInWorld returns false.
		/// <para /> Note that items in the world are drawn centered horizontally sitting at the bottom of the item hitbox, not in the center of the hitbox. To replicate the normal drawing calculations, use the following and then use <see cref="M:Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(Microsoft.Xna.Framework.Graphics.SpriteFont,System.String,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color,System.Single,Microsoft.Xna.Framework.Vector2,System.Single,Microsoft.Xna.Framework.Graphics.SpriteEffects,System.Single)" />:
		/// <para /> Called on all clients.
		/// <code>
		/// Main.GetItemDrawFrame(Item.type, out var itemTexture, out var itemFrame);
		/// Vector2 drawOrigin = itemFrame.Size() / 2f;
		/// Vector2 drawPosition = Item.Bottom - Main.screenPosition - new Vector2(0, drawOrigin.Y);
		/// </code>
		/// </summary>
		/// <param name="spriteBatch">The sprite batch.</param>
		/// <param name="lightColor">Color of the light.</param>
		/// <param name="alphaColor">Color of the alpha.</param>
		/// <param name="rotation">The item rotation. Items rotate slightly as they are thrown.</param>
		/// <param name="scale">The draw scale. Items are usually drawn in the world at a scale of 1f but some effects like pulsing Soul items change this.</param>
		/// <param name="whoAmI">The <see cref="F:Terraria.Entity.whoAmI" />.</param>
		// Token: 0x06002155 RID: 8533 RVA: 0x004E4699 File Offset: 0x004E2899
		public virtual void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
		}

		/// <summary>
		/// Allows you to draw things behind this item in the inventory. Return false to stop the game from drawing the item (useful if you're manually drawing the item).
		/// <para /> Note that <paramref name="position" /> is the center of the inventory slot and <paramref name="origin" /> is the center of the texture <paramref name="frame" /> to be drawn, so the provided parameters can be passed into <see cref="M:Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(Microsoft.Xna.Framework.Graphics.SpriteFont,System.String,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color,System.Single,Microsoft.Xna.Framework.Vector2,System.Single,Microsoft.Xna.Framework.Graphics.SpriteEffects,System.Single)" /> to draw a texture in the typical manner.
		/// <para /> Called on the local client only.
		/// <para /> Returns true by default.
		/// </summary>
		/// <param name="spriteBatch">The sprite batch.</param>
		/// <param name="position">The screen position of the center of the inventory slot.</param>
		/// <param name="frame">The frame of the item texture to be drawn.</param>
		/// <param name="drawColor">Color of the draw.</param>
		/// <param name="itemColor">Color of the item.</param>
		/// <param name="origin">The draw origin, the center of the frame to be drawn.</param>
		/// <param name="scale">The scale the item has been calculated to draw in to fit in the inventory slot.</param>
		/// <returns></returns>
		// Token: 0x06002156 RID: 8534 RVA: 0x004E469B File Offset: 0x004E289B
		public virtual bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this item in the inventory. This method is called even if PreDrawInInventory returns false.
		/// <para /> Note that <paramref name="position" /> is the center of the inventory slot and <paramref name="origin" /> is the center of the texture <paramref name="frame" /> to be drawn, so the provided parameters can be passed into <see cref="M:Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(Microsoft.Xna.Framework.Graphics.SpriteFont,System.String,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color,System.Single,Microsoft.Xna.Framework.Vector2,System.Single,Microsoft.Xna.Framework.Graphics.SpriteEffects,System.Single)" /> to draw a texture in the typical manner.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="spriteBatch">The sprite batch.</param>
		/// <param name="position">The screen position of the center of the inventory slot.</param>
		/// <param name="frame">The frame of the item texture to be drawn.</param>
		/// <param name="drawColor">Color of the draw.</param>
		/// <param name="itemColor">Color of the item.</param>
		/// <param name="origin">The draw origin, the center of the frame to be drawn.</param>
		/// <param name="scale">The scale of the item drawing to to fit in the inventory slot.</param>
		// Token: 0x06002157 RID: 8535 RVA: 0x004E469E File Offset: 0x004E289E
		public virtual void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
		}

		/// <summary>
		/// Allows you to determine the offset of this item's sprite when used by the player. This is only used for items with a useStyle of 5 that aren't staves. Return null to use the vanilla holdout offset; returns null by default.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06002158 RID: 8536 RVA: 0x004E46A0 File Offset: 0x004E28A0
		public virtual Vector2? HoldoutOffset()
		{
			return null;
		}

		/// <summary>
		/// Allows you to determine the point on this item's sprite that the player holds onto when using this item. The origin is from the bottom left corner of the sprite. This is only used for staves with a useStyle of 5. Return null to use the vanilla holdout origin (zero); returns null by default.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06002159 RID: 8537 RVA: 0x004E46B8 File Offset: 0x004E28B8
		public virtual Vector2? HoldoutOrigin()
		{
			return null;
		}

		/// <summary>
		/// Allows you to disallow the player from equipping this accessory. Return false to disallow equipping this accessory.
		/// <para /> Do not use this to check for mutually exclusive accessories being equipped, that check is only possible via <see cref="M:Terraria.ModLoader.ModItem.CanAccessoryBeEquippedWith(Terraria.Item,Terraria.Item,Terraria.Player)" />
		/// <para /> Returns <see langword="true" /> by default.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="slot">The inventory slot that the item is attempting to occupy.</param>
		/// <param name="modded">If the inventory slot index is for modded slots.</param>
		// Token: 0x0600215A RID: 8538 RVA: 0x004E46CE File Offset: 0x004E28CE
		public virtual bool CanEquipAccessory(Player player, int slot, bool modded)
		{
			return true;
		}

		/// <summary>
		/// Allows you to prevent similar accessories from being equipped multiple times. For example, vanilla Wings.
		/// Return false to have the currently equipped item swapped with the incoming item - ie both can't be equipped at same time.
		/// <para /> This method exists because manually checking <see cref="F:Terraria.Player.armor" /> in <see cref="M:Terraria.ModLoader.ModItem.CanEquipAccessory(Terraria.Player,System.Int32,System.Boolean)" /> will not correctly account for modded accessory slots.
		/// </summary>
		// Token: 0x0600215B RID: 8539 RVA: 0x004E46D1 File Offset: 0x004E28D1
		public virtual bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify what item, and in what quantity, is obtained when any item belonging to the extractinator type corresponding to this item is fed into the Extractinator. Use <see cref="F:Terraria.ID.ItemID.Sets.ExtractinatorMode" /> to allow an item to be fed into the Extractinator.
		/// <para /> This method is only called if <c>ItemID.Sets.ExtractinatorMode[Item.type] = Item.type;</c> in used in SetStaticDefaults. Other items belonging to the same extractinator group should use <c>ItemID.Sets.ExtractinatorMode[Item.type] = ModContent.ItemType&lt;IconicItemForThisExtractinatorType&gt;();</c> to indicate that they share the same extractinator output pool and to avoid code duplication.
		/// <para /> By default the parameters will be set to the output of feeding Silt/Slush into the Extractinator.
		/// <para /> Use <paramref name="extractinatorBlockType" /> to provide different behavior for <see cref="F:Terraria.ID.TileID.ChlorophyteExtractinator" /> if desired.
		/// <para /> If the Chlorophyte Extractinator item swapping behavior is desired, see the example in <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Common/GlobalItems/TorchExtractinatorGlobalItem.cs">TorchExtractinatorGlobalItem.cs</see>.
		/// <para /> This method is not instanced.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="extractinatorBlockType">Which Extractinator tile is being used, <see cref="F:Terraria.ID.TileID.Extractinator" /> or <see cref="F:Terraria.ID.TileID.ChlorophyteExtractinator" />.</param>
		/// <param name="resultType">Type of the result.</param>
		/// <param name="resultStack">The result stack.</param>
		// Token: 0x0600215C RID: 8540 RVA: 0x004E46D4 File Offset: 0x004E28D4
		public virtual void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
		{
		}

		/// <summary>
		/// If this item is a fishing pole, allows you to modify the origin and color of its fishing line.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="bobber">The bobber projectile</param>
		/// <param name="lineOriginOffset"> The offset of the fishing line's origin from the player's center. </param>
		/// <param name="lineColor"> The fishing line's color, before being overridden by string color accessories. </param>
		// Token: 0x0600215D RID: 8541 RVA: 0x004E46D6 File Offset: 0x004E28D6
		public virtual void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
		{
		}

		/// <summary>
		/// Allows you to determine how many of this item a player obtains when the player fishes this item.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="stack">The stack.</param>
		// Token: 0x0600215E RID: 8542 RVA: 0x004E46D8 File Offset: 0x004E28D8
		public virtual void CaughtFishStack(ref int stack)
		{
		}

		/// <summary>
		/// Whether or not the Angler can ever randomly request this type of item for his daily quest. Returns false by default.
		/// </summary>
		// Token: 0x0600215F RID: 8543 RVA: 0x004E46DA File Offset: 0x004E28DA
		public virtual bool IsQuestFish()
		{
			return false;
		}

		/// <summary>
		/// Whether or not specific conditions have been satisfied for the Angler to be able to request this item. (For example, Hardmode.) Returns true by default.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		// Token: 0x06002160 RID: 8544 RVA: 0x004E46DD File Offset: 0x004E28DD
		public virtual bool IsAnglerQuestAvailable()
		{
			return true;
		}

		/// <summary>
		/// Allows you to set what the Angler says when he requests for this item. The description parameter is his dialogue, and catchLocation should be set to "\n(Caught at [location])".
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="description">The description.</param>
		/// <param name="catchLocation">The catch location.</param>
		// Token: 0x06002161 RID: 8545 RVA: 0x004E46E0 File Offset: 0x004E28E0
		public virtual void AnglerQuestChat(ref string description, ref string catchLocation)
		{
		}

		/// <summary>
		/// Allows you to save custom data for this item.
		/// <br />
		/// <br /><b>NOTE:</b> The provided tag is always empty by default, and is provided as an argument only for the sake of convenience and optimization.
		/// <br /><b>NOTE:</b> Try to only save data that isn't default values.
		/// </summary>
		/// <param name="tag"> The TagCompound to save data into. Note that this is always empty by default, and is provided as an argument only for the sake of convenience and optimization. </param>
		// Token: 0x06002162 RID: 8546 RVA: 0x004E46E2 File Offset: 0x004E28E2
		public virtual void SaveData(TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to load custom data that you have saved for this item.
		/// <br /><b>Try to write defensive loading code that won't crash if something's missing.</b>
		/// </summary>
		/// <param name="tag"> The TagCompound to load data from. </param>
		// Token: 0x06002163 RID: 8547 RVA: 0x004E46E4 File Offset: 0x004E28E4
		public virtual void LoadData(TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to send custom data for this item between client and server, which will be handled in NetReceive.
		/// <br />Called whenever an item container syncs its contents (various MessageIDs and sources), or <see cref="F:Terraria.ID.MessageID.SyncItem" /> and <see cref="F:Terraria.ID.MessageID.InstancedItem" /> are successfully sent, for example when the item is dropped into the world.
		/// <br />Can be called on both server and client.
		/// </summary>
		/// <param name="writer">The writer.</param>
		// Token: 0x06002164 RID: 8548 RVA: 0x004E46E6 File Offset: 0x004E28E6
		public virtual void NetSend(BinaryWriter writer)
		{
		}

		/// <summary>
		/// Receives the custom data sent in NetSend.
		/// <br />Called whenever an item container syncs its contents (various MessageIDs and sources), or <see cref="F:Terraria.ID.MessageID.SyncItem" /> and <see cref="F:Terraria.ID.MessageID.InstancedItem" /> are successfully received.
		/// <br />Can be called on both server and client.
		/// </summary>
		/// <param name="reader">The reader.</param>
		// Token: 0x06002165 RID: 8549 RVA: 0x004E46E8 File Offset: 0x004E28E8
		public virtual void NetReceive(BinaryReader reader)
		{
		}

		/// <summary>
		/// Override this method to add <see cref="T:Terraria.Recipe" />s to the game.<br />
		/// Do note that this will be called for every instance of the overriding ModItem class that is added to the game.<br />
		/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Recipes">Basic Recipes Guide</see> teaches how to add new recipes to the game and how to manipulate existing recipes.<br />
		/// To create a recipe resulting in this item, use <see cref="M:Terraria.ModLoader.ModItem.CreateRecipe(System.Int32)" />.<br />
		/// To create a recipe using this item as an ingredient, use <see cref="M:Terraria.Recipe.Create(System.Int32,System.Int32)" /> and then pass in <c>this</c> or <c>Type</c> into <see cref="M:Terraria.Recipe.AddIngredient(Terraria.ModLoader.ModItem,System.Int32)" /> or <see cref="M:Terraria.Recipe.AddIngredient(System.Int32,System.Int32)" />
		/// </summary>
		// Token: 0x06002166 RID: 8550 RVA: 0x004E46EA File Offset: 0x004E28EA
		public virtual void AddRecipes()
		{
		}

		/// <summary>
		/// Allows you to make anything happen when the player crafts this item using the given recipe.
		/// </summary>
		/// <param name="recipe">The recipe that was used to craft this item.</param>
		// Token: 0x06002167 RID: 8551 RVA: 0x004E46EC File Offset: 0x004E28EC
		[Obsolete("Use OnCreate and check if context is RecipeItemCreationContext", true)]
		public virtual void OnCraft(Recipe recipe)
		{
		}

		/// <summary>
		/// Allows you to do things before this item's tooltip is drawn.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="lines">The tooltip lines for this item</param>
		/// <param name="x">The top X position for this tooltip. It is where the first line starts drawing</param>
		/// <param name="y">The top Y position for this tooltip. It is where the first line starts drawing</param>
		/// <returns>Whether or not to draw this tooltip</returns>
		// Token: 0x06002168 RID: 8552 RVA: 0x004E46EE File Offset: 0x004E28EE
		public virtual bool PreDrawTooltip(ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y)
		{
			return true;
		}

		/// <summary>
		/// Allows you to do things after this item's tooltip is drawn. The lines contain draw information as this is ran after drawing the tooltip.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="lines">The tooltip lines for this item</param>
		// Token: 0x06002169 RID: 8553 RVA: 0x004E46F1 File Offset: 0x004E28F1
		public virtual void PostDrawTooltip(ReadOnlyCollection<DrawableTooltipLine> lines)
		{
		}

		/// <summary>
		/// Allows you to do things before a tooltip line of this item is drawn. The line contains draw info.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="line">The line that would be drawn</param>
		/// <param name="yOffset">The Y offset added for next tooltip lines</param>
		/// <returns>Whether or not to draw this tooltip line</returns>
		// Token: 0x0600216A RID: 8554 RVA: 0x004E46F3 File Offset: 0x004E28F3
		public virtual bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			return true;
		}

		/// <summary>
		/// Allows you to do things after a tooltip line of this item is drawn. The line contains draw info.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="line">The line that was drawn</param>
		// Token: 0x0600216B RID: 8555 RVA: 0x004E46F6 File Offset: 0x004E28F6
		public virtual void PostDrawTooltipLine(DrawableTooltipLine line)
		{
		}

		/// <summary>
		/// Allows you to modify all the tooltips that display for this item. See here for information about TooltipLine. To hide tooltips, please use <see cref="M:Terraria.ModLoader.TooltipLine.Hide" /> and defensive coding.
		/// <para /> Called on a clone of the item, not the original. Modifying instanced fields will have no effect.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="tooltips">The tooltips.</param>
		// Token: 0x0600216C RID: 8556 RVA: 0x004E46F8 File Offset: 0x004E28F8
		public virtual void ModifyTooltips(List<TooltipLine> tooltips)
		{
		}

		/// <summary>
		/// Creates a recipe resulting this ModItem. The <paramref name="amount" /> dictates the resulting stack. This method only creates the recipe, it does not register it into the game. Call this at the very beginning when creating a new recipe.
		/// </summary>
		/// <param name="amount">The stack -&gt; how many result items given when the recipe is crafted. (eg. 1 wood -&gt; 4 wood platform)</param>
		/// <returns></returns>
		// Token: 0x0600216D RID: 8557 RVA: 0x004E46FA File Offset: 0x004E28FA
		public Recipe CreateRecipe(int amount = 1)
		{
			return Recipe.Create(this.Type, amount);
		}
	}
}
