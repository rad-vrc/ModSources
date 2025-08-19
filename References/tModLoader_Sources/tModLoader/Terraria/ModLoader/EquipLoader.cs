using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Reflection;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as a central place to store equipment slots and their corresponding textures. You will use this to obtain the IDs for your equipment textures.
	/// </summary>
	// Token: 0x0200015B RID: 347
	public static class EquipLoader
	{
		// Token: 0x06001BEF RID: 7151 RVA: 0x004D1B00 File Offset: 0x004CFD00
		static EquipLoader()
		{
			foreach (EquipType type in EquipLoader.EquipTypes)
			{
				EquipLoader.nextEquip[type] = EquipLoader.GetNumVanilla(type);
				EquipLoader.equipTextures[type] = new Dictionary<int, EquipTexture>();
			}
			EquipLoader.slotToId[EquipType.Head] = new Dictionary<int, int>();
			EquipLoader.slotToId[EquipType.Body] = new Dictionary<int, int>();
			EquipLoader.slotToId[EquipType.Legs] = new Dictionary<int, int>();
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x004D1BB8 File Offset: 0x004CFDB8
		internal static int ReserveEquipID(EquipType type)
		{
			Dictionary<EquipType, int> dictionary = EquipLoader.nextEquip;
			int num = dictionary[type];
			dictionary[type] = num + 1;
			return num;
		}

		/// <summary>
		/// Gets the equipment texture for the specified equipment type and ID.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="slot"></param>
		/// <returns></returns>
		// Token: 0x06001BF1 RID: 7153 RVA: 0x004D1BE0 File Offset: 0x004CFDE0
		public static EquipTexture GetEquipTexture(EquipType type, int slot)
		{
			EquipTexture texture;
			if (!EquipLoader.equipTextures[type].TryGetValue(slot, out texture))
			{
				return null;
			}
			return texture;
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x004D1C08 File Offset: 0x004CFE08
		internal static void ResizeAndFillArrays()
		{
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.ArmorHead, EquipLoader.nextEquip[EquipType.Head]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.ArmorBody, EquipLoader.nextEquip[EquipType.Body]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.ArmorBodyComposite, EquipLoader.nextEquip[EquipType.Body]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.FemaleBody, EquipLoader.nextEquip[EquipType.Body]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.ArmorArm, EquipLoader.nextEquip[EquipType.Body]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.ArmorLeg, EquipLoader.nextEquip[EquipType.Legs]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccHandsOn, EquipLoader.nextEquip[EquipType.HandsOn]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccHandsOnComposite, EquipLoader.nextEquip[EquipType.HandsOn]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccHandsOff, EquipLoader.nextEquip[EquipType.HandsOff]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccHandsOffComposite, EquipLoader.nextEquip[EquipType.HandsOff]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccBack, EquipLoader.nextEquip[EquipType.Back]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccFront, EquipLoader.nextEquip[EquipType.Front]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccShoes, EquipLoader.nextEquip[EquipType.Shoes]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccWaist, EquipLoader.nextEquip[EquipType.Waist]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Wings, EquipLoader.nextEquip[EquipType.Wings]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccShield, EquipLoader.nextEquip[EquipType.Shield]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccNeck, EquipLoader.nextEquip[EquipType.Neck]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccFace, EquipLoader.nextEquip[EquipType.Face]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccBeard, EquipLoader.nextEquip[EquipType.Beard]);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.AccBalloon, EquipLoader.nextEquip[EquipType.Balloon]);
			LoaderUtils.ResetStaticMembers(typeof(ArmorIDs), true);
			WingStatsInitializer.Load();
			foreach (EquipType type in EquipLoader.EquipTypes)
			{
				foreach (KeyValuePair<int, EquipTexture> entry in EquipLoader.equipTextures[type])
				{
					int slot = entry.Key;
					string texture = entry.Value.Texture;
					EquipLoader.GetTextureArray(type)[slot] = ModContent.Request<Texture2D>(texture, 2);
					if (type == EquipType.Body)
					{
						ArmorIDs.Body.Sets.UsesNewFramingCode[slot] = true;
					}
					else if (type == EquipType.HandsOn)
					{
						ArmorIDs.HandOn.Sets.UsesNewFramingCode[slot] = true;
					}
					else if (type == EquipType.HandsOff)
					{
						ArmorIDs.HandOff.Sets.UsesNewFramingCode[slot] = true;
					}
				}
			}
			EquipLoader.<ResizeAndFillArrays>g__ResizeAndRegisterType|8_0(EquipType.Head, ref Item.headType);
			EquipLoader.<ResizeAndFillArrays>g__ResizeAndRegisterType|8_0(EquipType.Body, ref Item.bodyType);
			EquipLoader.<ResizeAndFillArrays>g__ResizeAndRegisterType|8_0(EquipType.Legs, ref Item.legType);
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x004D1EAC File Offset: 0x004D00AC
		internal static void Unload()
		{
			foreach (EquipType type in EquipLoader.EquipTypes)
			{
				EquipLoader.nextEquip[type] = EquipLoader.GetNumVanilla(type);
				EquipLoader.equipTextures[type].Clear();
			}
			EquipLoader.idToSlot.Clear();
			EquipLoader.slotToId[EquipType.Head].Clear();
			EquipLoader.slotToId[EquipType.Body].Clear();
			EquipLoader.slotToId[EquipType.Legs].Clear();
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x004D1F2C File Offset: 0x004D012C
		internal static int GetNumVanilla(EquipType type)
		{
			int result;
			switch (type)
			{
			case EquipType.Head:
				result = ArmorIDs.Head.Count;
				break;
			case EquipType.Body:
				result = ArmorIDs.Body.Count;
				break;
			case EquipType.Legs:
				result = ArmorIDs.Legs.Count;
				break;
			case EquipType.HandsOn:
				result = ArmorIDs.HandOn.Count;
				break;
			case EquipType.HandsOff:
				result = ArmorIDs.HandOff.Count;
				break;
			case EquipType.Back:
				result = ArmorIDs.Back.Count;
				break;
			case EquipType.Front:
				result = ArmorIDs.Front.Count;
				break;
			case EquipType.Shoes:
				result = ArmorIDs.Shoe.Count;
				break;
			case EquipType.Waist:
				result = ArmorIDs.Waist.Count;
				break;
			case EquipType.Wings:
				result = ArmorIDs.Wing.Count;
				break;
			case EquipType.Shield:
				result = ArmorIDs.Shield.Count;
				break;
			case EquipType.Neck:
				result = ArmorIDs.Neck.Count;
				break;
			case EquipType.Face:
				result = (int)ArmorIDs.Face.Count;
				break;
			case EquipType.Balloon:
				result = ArmorIDs.Balloon.Count;
				break;
			case EquipType.Beard:
				result = (int)ArmorIDs.Beard.Count;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x004D1FF8 File Offset: 0x004D01F8
		internal static IdDictionary GetSearch(EquipType type)
		{
			IdDictionary search;
			switch (type)
			{
			case EquipType.Head:
				search = ArmorIDs.Head.Search;
				break;
			case EquipType.Body:
				search = ArmorIDs.Body.Search;
				break;
			case EquipType.Legs:
				search = ArmorIDs.Legs.Search;
				break;
			case EquipType.HandsOn:
				search = ArmorIDs.HandOn.Search;
				break;
			case EquipType.HandsOff:
				search = ArmorIDs.HandOff.Search;
				break;
			case EquipType.Back:
				search = ArmorIDs.Back.Search;
				break;
			case EquipType.Front:
				search = ArmorIDs.Front.Search;
				break;
			case EquipType.Shoes:
				search = ArmorIDs.Shoe.Search;
				break;
			case EquipType.Waist:
				search = ArmorIDs.Waist.Search;
				break;
			case EquipType.Wings:
				search = ArmorIDs.Wing.Search;
				break;
			case EquipType.Shield:
				search = ArmorIDs.Shield.Search;
				break;
			case EquipType.Neck:
				search = ArmorIDs.Neck.Search;
				break;
			case EquipType.Face:
				search = ArmorIDs.Face.Search;
				break;
			case EquipType.Balloon:
				search = ArmorIDs.Balloon.Search;
				break;
			case EquipType.Beard:
				search = ArmorIDs.Beard.Search;
				break;
			default:
				throw new ArgumentOutOfRangeException("type");
			}
			return search;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x004D20D0 File Offset: 0x004D02D0
		internal static Asset<Texture2D>[] GetTextureArray(EquipType type)
		{
			Asset<Texture2D>[] result;
			switch (type)
			{
			case EquipType.Head:
				result = TextureAssets.ArmorHead;
				break;
			case EquipType.Body:
				result = TextureAssets.ArmorBodyComposite;
				break;
			case EquipType.Legs:
				result = TextureAssets.ArmorLeg;
				break;
			case EquipType.HandsOn:
				result = TextureAssets.AccHandsOnComposite;
				break;
			case EquipType.HandsOff:
				result = TextureAssets.AccHandsOffComposite;
				break;
			case EquipType.Back:
				result = TextureAssets.AccBack;
				break;
			case EquipType.Front:
				result = TextureAssets.AccFront;
				break;
			case EquipType.Shoes:
				result = TextureAssets.AccShoes;
				break;
			case EquipType.Waist:
				result = TextureAssets.AccWaist;
				break;
			case EquipType.Wings:
				result = TextureAssets.Wings;
				break;
			case EquipType.Shield:
				result = TextureAssets.AccShield;
				break;
			case EquipType.Neck:
				result = TextureAssets.AccNeck;
				break;
			case EquipType.Face:
				result = TextureAssets.AccFace;
				break;
			case EquipType.Balloon:
				result = TextureAssets.AccBalloon;
				break;
			case EquipType.Beard:
				result = TextureAssets.AccBeard;
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x004D219C File Offset: 0x004D039C
		internal static void SetSlot(Item item)
		{
			Dictionary<EquipType, int> slots;
			if (!EquipLoader.idToSlot.TryGetValue(item.type, out slots))
			{
				return;
			}
			foreach (KeyValuePair<EquipType, int> entry in slots)
			{
				int slot = entry.Value;
				switch (entry.Key)
				{
				case EquipType.Head:
					item.headSlot = slot;
					break;
				case EquipType.Body:
					item.bodySlot = slot;
					break;
				case EquipType.Legs:
					item.legSlot = slot;
					break;
				case EquipType.HandsOn:
					item.handOnSlot = slot;
					break;
				case EquipType.HandsOff:
					item.handOffSlot = slot;
					break;
				case EquipType.Back:
					item.backSlot = slot;
					break;
				case EquipType.Front:
					item.frontSlot = slot;
					break;
				case EquipType.Shoes:
					item.shoeSlot = slot;
					break;
				case EquipType.Waist:
					item.waistSlot = slot;
					break;
				case EquipType.Wings:
					item.wingSlot = slot;
					break;
				case EquipType.Shield:
					item.shieldSlot = slot;
					break;
				case EquipType.Neck:
					item.neckSlot = slot;
					break;
				case EquipType.Face:
					item.faceSlot = slot;
					break;
				case EquipType.Balloon:
					item.balloonSlot = slot;
					break;
				case EquipType.Beard:
					item.beardSlot = slot;
					break;
				}
			}
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x004D22DC File Offset: 0x004D04DC
		internal static int GetPlayerEquip(Player player, EquipType type)
		{
			int result;
			switch (type)
			{
			case EquipType.Head:
				result = player.head;
				break;
			case EquipType.Body:
				result = player.body;
				break;
			case EquipType.Legs:
				result = player.legs;
				break;
			case EquipType.HandsOn:
				result = player.handon;
				break;
			case EquipType.HandsOff:
				result = player.handoff;
				break;
			case EquipType.Back:
				result = player.back;
				break;
			case EquipType.Front:
				result = player.front;
				break;
			case EquipType.Shoes:
				result = player.shoe;
				break;
			case EquipType.Waist:
				result = player.waist;
				break;
			case EquipType.Wings:
				result = player.wings;
				break;
			case EquipType.Shield:
				result = player.shield;
				break;
			case EquipType.Neck:
				result = player.neck;
				break;
			case EquipType.Face:
				result = player.face;
				break;
			case EquipType.Balloon:
				result = player.balloon;
				break;
			case EquipType.Beard:
				result = player.beard;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}

		/// <summary>
		/// Adds an equipment texture of the specified type, internal name, and/or associated item to your mod.<br />
		/// If no internal name is provided, the associated item's name will be used instead.<br />
		/// You can then get the ID for your texture by calling EquipLoader.GetEquipTexture, and using the EquipTexture's Slot property.<br />
		/// If you need to override EquipmentTexture's hooks, you can specify the class of the equipment texture class.
		/// </summary>
		/// <remarks>
		/// If both an internal name and associated item are provided, the EquipTexture's name will be set to the internal name, alongside the keys for the equipTexture dictionary.<br />
		/// Additionally, if multiple EquipTextures of the same type are registered for the same item, the first one to be added will be the one automatically displayed on the player and mannequins.
		/// </remarks>
		/// <param name="mod">The mod the equipment texture is from.</param>
		/// <param name="equipTexture">The equip texture.</param>
		/// <param name="item">The item.</param>
		/// <param name="name">The internal name.</param>
		/// <param name="type">The type.</param>
		/// <param name="texture">The texture.</param>
		/// <returns>the ID / slot that is assigned to the equipment texture.</returns>
		// Token: 0x06001BF9 RID: 7161 RVA: 0x004D23C0 File Offset: 0x004D05C0
		public static int AddEquipTexture(Mod mod, string texture, EquipType type, ModItem item = null, string name = null, EquipTexture equipTexture = null)
		{
			if (!mod.loading)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorNotLoading"));
			}
			if (name == null && item == null)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorEquipTextureMissingParameters"));
			}
			if (equipTexture == null)
			{
				equipTexture = new EquipTexture();
			}
			ModContent.Request<Texture2D>(texture, 2);
			equipTexture.Texture = texture;
			equipTexture.Name = (name ?? item.Name);
			equipTexture.Type = type;
			equipTexture.Item = item;
			int slot = equipTexture.Slot = EquipLoader.ReserveEquipID(type);
			EquipLoader.GetSearch(type).Add(mod.Name + "/" + equipTexture.Name, slot);
			EquipLoader.equipTextures[type][slot] = equipTexture;
			mod.equipTextures[Tuple.Create<string, EquipType>(equipTexture.Name, type)] = equipTexture;
			if (item != null)
			{
				Dictionary<EquipType, int> slots;
				if (!EquipLoader.idToSlot.TryGetValue(item.Type, out slots))
				{
					slots = (EquipLoader.idToSlot[item.Type] = new Dictionary<EquipType, int>());
				}
				slots[type] = slot;
				if (type == EquipType.Head || type == EquipType.Body || type == EquipType.Legs)
				{
					EquipLoader.slotToId[type][slot] = item.Type;
				}
			}
			return slot;
		}

		/// <summary>
		/// Gets the EquipTexture instance corresponding to the name and EquipType. Returns null if no EquipTexture with the given name and EquipType is found.
		/// </summary>
		/// <param name="mod">The mod the equipment texture is from.</param>
		/// <param name="name">The name.</param>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		// Token: 0x06001BFA RID: 7162 RVA: 0x004D24F4 File Offset: 0x004D06F4
		public static EquipTexture GetEquipTexture(Mod mod, string name, EquipType type)
		{
			EquipTexture texture;
			if (!mod.equipTextures.TryGetValue(Tuple.Create<string, EquipType>(name, type), out texture))
			{
				return null;
			}
			return texture;
		}

		/// <summary>
		/// Gets the slot/ID of the equipment texture corresponding to the given name. Returns -1 if no EquipTexture with the given name is found.
		/// </summary>
		/// <param name="mod">The mod the equipment texture is from.</param>
		/// <param name="name">The name.</param>
		/// <param name="type"></param>
		/// <returns></returns>
		// Token: 0x06001BFB RID: 7163 RVA: 0x004D251A File Offset: 0x004D071A
		public static int GetEquipSlot(Mod mod, string name, EquipType type)
		{
			EquipTexture equipTexture = EquipLoader.GetEquipTexture(mod, name, type);
			if (equipTexture == null)
			{
				return -1;
			}
			return equipTexture.Slot;
		}

		/// <summary>
		/// Hook Player.PlayerFrame
		/// Calls each of the item's equipment texture's FrameEffects hook.
		/// </summary>
		// Token: 0x06001BFC RID: 7164 RVA: 0x004D2530 File Offset: 0x004D0730
		public static void EquipFrameEffects(Player player)
		{
			foreach (EquipType type in EquipLoader.EquipTypes)
			{
				int slot = EquipLoader.GetPlayerEquip(player, type);
				EquipTexture equipTexture = EquipLoader.GetEquipTexture(type, slot);
				if (equipTexture != null)
				{
					equipTexture.FrameEffects(player, type);
				}
			}
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x004D2574 File Offset: 0x004D0774
		[CompilerGenerated]
		internal static void <ResizeAndFillArrays>g__ResizeAndRegisterType|8_0(EquipType equipType, ref int[] typeArray)
		{
			Array.Resize<int>(ref typeArray, EquipLoader.nextEquip[equipType]);
			foreach (KeyValuePair<int, int> entry in EquipLoader.slotToId[equipType])
			{
				typeArray[entry.Key] = entry.Value;
			}
		}

		// Token: 0x040014F1 RID: 5361
		internal static readonly Dictionary<EquipType, int> nextEquip = new Dictionary<EquipType, int>();

		// Token: 0x040014F2 RID: 5362
		internal static readonly Dictionary<EquipType, Dictionary<int, EquipTexture>> equipTextures = new Dictionary<EquipType, Dictionary<int, EquipTexture>>();

		// Token: 0x040014F3 RID: 5363
		internal static readonly Dictionary<int, Dictionary<EquipType, int>> idToSlot = new Dictionary<int, Dictionary<EquipType, int>>();

		// Token: 0x040014F4 RID: 5364
		internal static readonly Dictionary<EquipType, Dictionary<int, int>> slotToId = new Dictionary<EquipType, Dictionary<int, int>>();

		// Token: 0x040014F5 RID: 5365
		public static readonly EquipType[] EquipTypes = (EquipType[])Enum.GetValues(typeof(EquipType));
	}
}
