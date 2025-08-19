using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This attribute annotates a <see cref="T:Terraria.ModLoader.ModItem" /> class to indicate that the game should autoload the specified equipment texture or textures and assign it to this item. The equipment texture is the texture that appears on the player itself when the item is worn and visible. Armor and most accessories will set at least one <see cref="T:Terraria.ModLoader.EquipType" />, but not every accessory needs on-player visuals. 
	/// <para /> The equipment texture will be loaded from the path made from appending "_EquipTypeNameHere" to <see cref="P:Terraria.ModLoader.ModItem.Texture" />. An error will be thrown during mod loading if the texture can't be found. For example, a helmet item named "ExampleHelmet" annotated with <c>[AutoloadEquip(EquipType.Head)]</c> will need both a "ExampleHelmet.png" and "ExampleHelmet_Head.png" to work. Note that equipment textures must follow specific layouts, see <see href="https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod/Content/Items/Armor">ExampleMod examples</see> of similar items to use as a guide.
	/// </summary>
	// Token: 0x02000140 RID: 320
	[AttributeUsage(AttributeTargets.Class)]
	public class AutoloadEquip : Attribute
	{
		// Token: 0x06001ABB RID: 6843 RVA: 0x004CD18A File Offset: 0x004CB38A
		public AutoloadEquip(params EquipType[] equipTypes)
		{
			this.equipTypes = equipTypes;
		}

		// Token: 0x04001478 RID: 5240
		public readonly EquipType[] equipTypes;
	}
}
