using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as a place for you to program behaviors of equipment textures. This is useful for equipment slots that do not have any item associated with them (for example, the Werewolf buff). Note that this class is purely for visual effects.
	/// </summary>
	// Token: 0x0200015C RID: 348
	public class EquipTexture
	{
		/// <summary>
		/// The name and folders of the texture file used by this equipment texture.
		/// </summary>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x004D25E8 File Offset: 0x004D07E8
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x004D25F0 File Offset: 0x004D07F0
		public string Texture { get; internal set; }

		/// <summary>
		/// The internal name of this equipment texture.
		/// </summary>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x004D25F9 File Offset: 0x004D07F9
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x004D2601 File Offset: 0x004D0801
		public string Name { get; internal set; }

		/// <summary>
		/// The type of equipment that this equipment texture is used as.
		/// </summary>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x004D260A File Offset: 0x004D080A
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x004D2612 File Offset: 0x004D0812
		public EquipType Type { get; internal set; }

		/// <summary>
		/// The slot (internal ID) of this equipment texture.
		/// </summary>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x004D261B File Offset: 0x004D081B
		// (set) Token: 0x06001C05 RID: 7173 RVA: 0x004D2623 File Offset: 0x004D0823
		public int Slot { get; internal set; }

		/// <summary>
		/// The item that is associated with this equipment texture. Null if no item is associated with this.
		/// </summary>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x004D262C File Offset: 0x004D082C
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x004D2634 File Offset: 0x004D0834
		public ModItem Item { get; internal set; }

		/// <summary>
		/// Allows you to create special effects (such as dust) when this equipment texture is displayed on the player under the given equipment type. By default this will call the associated ModItem's UpdateVanity if there is an associated ModItem.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="type"></param>
		// Token: 0x06001C08 RID: 7176 RVA: 0x004D263D File Offset: 0x004D083D
		public virtual void FrameEffects(Player player, EquipType type)
		{
			if (this.Item != null)
			{
				this.Item.EquipFrameEffects(player, type);
			}
		}

		/// <summary>
		/// Returns whether or not the head armor, body armor, and leg armor textures make up a set. This hook is used for the PreUpdateVanitySet, UpdateVanitySet, and ArmorSetShadows hooks. By default this will return the same thing as the associated ModItem's IsVanitySet, or false if no ModItem is associated.
		/// </summary>
		/// <param name="head"></param>
		/// <param name="body"></param>
		/// <param name="legs"></param>
		/// <returns></returns>
		// Token: 0x06001C09 RID: 7177 RVA: 0x004D2654 File Offset: 0x004D0854
		public virtual bool IsVanitySet(int head, int body, int legs)
		{
			return this.Item != null && this.Item.IsVanitySet(head, body, legs);
		}

		/// <summary>
		/// Allows you to create special effects (such as the necro armor's hurt noise) when the player wears this equipment texture's vanity set. This hook is called regardless of whether the player is frozen in any way. By default this will call the associated ModItem's PreUpdateVanitySet if there is an associated ModItem.
		/// </summary>
		/// <param name="player"></param>
		// Token: 0x06001C0A RID: 7178 RVA: 0x004D266E File Offset: 0x004D086E
		public virtual void PreUpdateVanitySet(Player player)
		{
			if (this.Item != null)
			{
				this.Item.PreUpdateVanitySet(player);
			}
		}

		/// <summary>
		/// Allows you to create special effects (such as dust) when the player wears this equipment texture's vanity set. This hook will only be called if the player is not frozen in any way. By default this will call the associated ModItem's UpdateVanitySet if there is an associated ModItem.
		/// </summary>
		/// <param name="player"></param>
		// Token: 0x06001C0B RID: 7179 RVA: 0x004D2684 File Offset: 0x004D0884
		public virtual void UpdateVanitySet(Player player)
		{
			if (this.Item != null)
			{
				this.Item.UpdateVanitySet(player);
			}
		}

		/// <summary>
		/// Allows you to determine special visual effects this vanity set has on the player without having to code them yourself. By default this will call the associated ModItem's ArmorSetShadows if there is an associated ModItem.
		/// </summary>
		/// <param name="player"></param>
		// Token: 0x06001C0C RID: 7180 RVA: 0x004D269A File Offset: 0x004D089A
		public virtual void ArmorSetShadows(Player player)
		{
			if (this.Item != null)
			{
				this.Item.ArmorSetShadows(player);
			}
		}

		/// <summary>
		/// Allows you to modify the equipment that the player appears to be wearing. This is most commonly used to add legs to robes and for swapping to female variant textures if <paramref name="male" /> is false for head and leg armor. This hook will only be called for head, body, and leg textures. Note that equipSlot is not the same as the item type of the armor the player will appear to be wearing. Worn equipment has a separate set of IDs. You can find the vanilla equipment IDs by looking at the headSlot, bodySlot, and legSlot fields for items, and modded equipment IDs by looking at EquipLoader.
		/// <para /> If this hook is called on body armor, equipSlot allows you to modify the leg armor the player appears to be wearing. If you modify it, make sure to set robes to true. If this hook is called on leg armor, equipSlot allows you to modify the leg armor the player appears to be wearing, and the robes parameter is useless. The same is true for head armor.
		/// <para /> By default, if there is an associated ModItem, this will call that ModItem's SetMatch.
		/// </summary>
		/// <param name="male"></param>
		/// <param name="equipSlot"></param>
		/// <param name="robes"></param>
		// Token: 0x06001C0D RID: 7181 RVA: 0x004D26B0 File Offset: 0x004D08B0
		public virtual void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			if (this.Item != null)
			{
				this.Item.SetMatch(male, ref equipSlot, ref robes);
			}
		}

		/// <summary>
		/// Allows you to modify the colors in which this armor texture and surrounding accessories are drawn, in addition to which glow mask and in what color is drawn. By default this will call the associated ModItem's DrawArmorColor if there is an associated ModItem.
		/// </summary>
		/// <param name="drawPlayer"></param>
		/// <param name="shadow"></param>
		/// <param name="color"></param>
		/// <param name="glowMask"></param>
		/// <param name="glowMaskColor"></param>
		// Token: 0x06001C0E RID: 7182 RVA: 0x004D26C8 File Offset: 0x004D08C8
		public virtual void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
		{
			if (this.Item != null)
			{
				this.Item.DrawArmorColor(drawPlayer, shadow, ref color, ref glowMask, ref glowMaskColor);
			}
		}

		/// <summary>
		/// Allows you to modify which glow mask and in what color is drawn on the player's arms. Note that this is only called for body equipment textures. By default this will call the associated ModItem's ArmorArmGlowMask if there is an associated ModItem.
		/// </summary>
		/// <param name="drawPlayer"></param>
		/// <param name="shadow"></param>
		/// <param name="glowMask"></param>
		/// <param name="color"></param>
		// Token: 0x06001C0F RID: 7183 RVA: 0x004D26E4 File Offset: 0x004D08E4
		public virtual void ArmorArmGlowMask(Player drawPlayer, float shadow, ref int glowMask, ref Color color)
		{
			if (this.Item != null)
			{
				this.Item.ArmorArmGlowMask(drawPlayer, shadow, ref glowMask, ref color);
			}
		}

		/// <summary>
		/// Allows you to modify vertical wing speeds.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="ascentWhenFalling"></param>
		/// <param name="ascentWhenRising"></param>
		/// <param name="maxCanAscendMultiplier"></param>
		/// <param name="maxAscentMultiplier"></param>
		/// <param name="constantAscend"></param>
		// Token: 0x06001C10 RID: 7184 RVA: 0x004D26FE File Offset: 0x004D08FE
		public virtual void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			if (this.Item != null)
			{
				this.Item.VerticalWingSpeeds(player, ref ascentWhenFalling, ref ascentWhenRising, ref maxCanAscendMultiplier, ref maxAscentMultiplier, ref constantAscend);
			}
		}

		/// <summary>
		/// Allows you to modify horizontal wing speeds.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="speed"></param>
		/// <param name="acceleration"></param>
		// Token: 0x06001C11 RID: 7185 RVA: 0x004D271C File Offset: 0x004D091C
		public virtual void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			if (this.Item != null)
			{
				this.Item.HorizontalWingSpeeds(player, ref speed, ref acceleration);
			}
		}

		/// <summary>
		/// Allows for wing textures to do various things while in use. "inUse" is whether or not the jump button is currently pressed. Called when this wing texture visually appears on the player. Use to animate wings, create dusts, invoke sounds, and create lights. By default this will call the associated ModItem's WingUpdate if there is an associated ModItem.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="inUse"></param>
		/// <returns></returns>
		// Token: 0x06001C12 RID: 7186 RVA: 0x004D2734 File Offset: 0x004D0934
		public virtual bool WingUpdate(Player player, bool inUse)
		{
			ModItem item = this.Item;
			return item != null && item.WingUpdate(player, inUse);
		}
	}
}
