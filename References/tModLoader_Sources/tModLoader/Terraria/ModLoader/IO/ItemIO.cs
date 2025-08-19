using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using log4net;
using Terraria.ID;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.Exceptions;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000282 RID: 642
	public static class ItemIO
	{
		// Token: 0x06002BBF RID: 11199 RVA: 0x0052392C File Offset: 0x00521B2C
		internal static void WriteVanillaID(Item item, BinaryWriter writer)
		{
			writer.Write((item.ModItem != null) ? 0 : item.netID);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00523945 File Offset: 0x00521B45
		internal static void WriteShortVanillaID(Item item, BinaryWriter writer)
		{
			ItemIO.WriteShortVanillaID(item.netID, writer);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00523953 File Offset: 0x00521B53
		internal static void WriteShortVanillaID(int id, BinaryWriter writer)
		{
			writer.Write((short)((id >= (int)ItemID.Count) ? 0 : id));
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x00523968 File Offset: 0x00521B68
		internal static void WriteShortVanillaStack(Item item, BinaryWriter writer)
		{
			ItemIO.WriteShortVanillaStack(item.stack, writer);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x00523976 File Offset: 0x00521B76
		internal static void WriteShortVanillaStack(int stack, BinaryWriter writer)
		{
			writer.Write((short)((stack > 32767) ? 32767 : stack));
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x0052398F File Offset: 0x00521B8F
		internal static void WriteByteVanillaPrefix(Item item, BinaryWriter writer)
		{
			ItemIO.WriteByteVanillaPrefix(item.prefix, writer);
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x0052399D File Offset: 0x00521B9D
		internal static void WriteByteVanillaPrefix(int prefix, BinaryWriter writer)
		{
			writer.Write((byte)((prefix >= PrefixID.Count) ? 0 : prefix));
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x005239B2 File Offset: 0x00521BB2
		public static TagCompound Save(Item item)
		{
			return ItemIO.Save(item, ItemIO.SaveGlobals(item));
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x005239C0 File Offset: 0x00521BC0
		public static TagCompound Save(Item item, List<TagCompound> globalData)
		{
			TagCompound tag = new TagCompound();
			if (item.type <= 0)
			{
				return tag;
			}
			if (item.ModItem == null)
			{
				tag.Set("mod", "Terraria", false);
				tag.Set("id", item.netID, false);
			}
			else
			{
				tag.Set("mod", item.ModItem.Mod.Name, false);
				tag.Set("name", item.ModItem.Name, false);
				TagCompound saveData = new TagCompound();
				item.ModItem.SaveData(saveData);
				if (saveData.Count > 0)
				{
					tag.Set("data", saveData, false);
				}
			}
			ModPrefix modPrefix = PrefixLoader.GetPrefix(item.prefix);
			if (modPrefix != null)
			{
				if (modPrefix is UnloadedPrefix)
				{
					UnloadedGlobalItem unloadedGlobalItem = item.GetGlobalItem<UnloadedGlobalItem>();
					tag.Set("modPrefixMod", unloadedGlobalItem.ModPrefixMod, false);
					tag.Set("modPrefixName", unloadedGlobalItem.ModPrefixName, false);
				}
				else
				{
					tag.Set("modPrefixMod", modPrefix.Mod.Name, false);
					tag.Set("modPrefixName", modPrefix.Name, false);
				}
			}
			else if (item.prefix != 0 && item.prefix < PrefixID.Count)
			{
				tag.Set("prefix", (byte)item.prefix, false);
			}
			if (item.stack > 1)
			{
				tag.Set("stack", item.stack, false);
			}
			if (item.favorited)
			{
				tag.Set("fav", true, false);
			}
			tag.Set("globalData", globalData, false);
			return tag;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x00523B50 File Offset: 0x00521D50
		public static void Load(Item item, TagCompound tag)
		{
			string modName = tag.GetString("mod");
			if (modName == "")
			{
				item.netDefaults(0);
				return;
			}
			ModItem modItem;
			if (modName == "Terraria")
			{
				item.netDefaults(tag.GetInt("id"));
			}
			else if (ModContent.TryFind<ModItem>(modName, tag.GetString("name"), out modItem))
			{
				item.SetDefaults(modItem.Type);
				item.ModItem.LoadData(tag.GetCompound("data"));
			}
			else
			{
				item.SetDefaults(ModContent.ItemType<UnloadedItem>());
				((UnloadedItem)item.ModItem).Setup(tag);
			}
			ItemIO.LoadModdedPrefix(item, tag);
			item.stack = tag.Get<int?>("stack").GetValueOrDefault(1);
			item.favorited = tag.GetBool("fav");
			if (!(item.ModItem is UnloadedItem))
			{
				ItemIO.LoadGlobals(item, tag.GetList<TagCompound>("globalData"));
			}
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x00523C44 File Offset: 0x00521E44
		internal static void LoadModdedPrefix(Item item, TagCompound tag)
		{
			if (!tag.ContainsKey("modPrefixMod") || !tag.ContainsKey("modPrefixName"))
			{
				if (tag.ContainsKey("prefix"))
				{
					item.Prefix((int)tag.GetByte("prefix"));
				}
				return;
			}
			string modPrefixMod = tag.GetString("modPrefixMod");
			string modPrefixName = tag.GetString("modPrefixName");
			ModPrefix prefix;
			if (ModContent.TryFind<ModPrefix>(modPrefixMod, modPrefixName, out prefix))
			{
				item.Prefix(prefix.Type);
				return;
			}
			item.Prefix(ModContent.PrefixType<UnloadedPrefix>());
			UnloadedGlobalItem globalItem = item.GetGlobalItem<UnloadedGlobalItem>();
			globalItem.ModPrefixMod = modPrefixMod;
			globalItem.ModPrefixName = modPrefixName;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x00523CDB File Offset: 0x00521EDB
		public static Item Load(TagCompound tag)
		{
			Item item = new Item();
			ItemIO.Load(item, tag);
			return item;
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x00523CEC File Offset: 0x00521EEC
		internal static List<TagCompound> SaveGlobals(Item item)
		{
			if (item.ModItem is UnloadedItem)
			{
				return null;
			}
			List<TagCompound> list = new List<TagCompound>();
			TagCompound saveData = new TagCompound();
			foreach (GlobalItem g in ItemLoader.HookSaveData.Enumerate(item))
			{
				UnloadedGlobalItem unloadedGlobalItem = g as UnloadedGlobalItem;
				if (unloadedGlobalItem != null)
				{
					list.AddRange(unloadedGlobalItem.data);
				}
				else
				{
					g.SaveData(item, saveData);
					if (saveData.Count != 0)
					{
						List<TagCompound> list2 = list;
						TagCompound tagCompound = new TagCompound();
						tagCompound["mod"] = g.Mod.Name;
						tagCompound["name"] = g.Name;
						tagCompound["data"] = saveData;
						list2.Add(tagCompound);
						saveData = new TagCompound();
					}
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x00523DC0 File Offset: 0x00521FC0
		internal static void LoadGlobals(Item item, IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				GlobalItem globalItemBase;
				GlobalItem globalItem;
				if (ModContent.TryFind<GlobalItem>(tag.GetString("mod"), tag.GetString("name"), out globalItemBase) && item.TryGetGlobalItem<GlobalItem>(globalItemBase, out globalItem))
				{
					try
					{
						globalItem.LoadData(item, tag.GetCompound("data"));
						continue;
					}
					catch (Exception e)
					{
						throw new CustomModDataException(globalItem.Mod, "Error in reading custom player data for " + globalItem.FullName, e);
					}
				}
				item.GetGlobalItem<UnloadedGlobalItem>().data.Add(tag);
			}
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x00523E7C File Offset: 0x0052207C
		public static void Send(Item item, BinaryWriter writer, bool writeStack = false, bool writeFavorite = false)
		{
			writer.Write7BitEncodedInt(item.netID);
			writer.Write7BitEncodedInt(item.prefix);
			if (writeStack)
			{
				writer.Write7BitEncodedInt(item.stack);
			}
			if (writeFavorite)
			{
				writer.Write(item.favorited);
			}
			ItemIO.SendModData(item, writer);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x00523EBB File Offset: 0x005220BB
		public static void Receive(Item item, BinaryReader reader, bool readStack = false, bool readFavorite = false)
		{
			item.netDefaults(reader.Read7BitEncodedInt());
			item.Prefix(reader.Read7BitEncodedInt());
			if (readStack)
			{
				item.stack = reader.Read7BitEncodedInt();
			}
			if (readFavorite)
			{
				item.favorited = reader.ReadBoolean();
			}
			ItemIO.ReceiveModData(item, reader);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x00523EFB File Offset: 0x005220FB
		public static Item Receive(BinaryReader reader, bool readStack = false, bool readFavorite = false)
		{
			Item item = new Item();
			ItemIO.Receive(item, reader, readStack, readFavorite);
			return item;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x00523F0C File Offset: 0x0052210C
		public static void SendModData(Item item, BinaryWriter writer)
		{
			if (item.IsAir)
			{
				return;
			}
			writer.SafeWrite(delegate(BinaryWriter w)
			{
				ModItem modItem = item.ModItem;
				if (modItem == null)
				{
					return;
				}
				modItem.NetSend(w);
			});
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookNetSend.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				GlobalItem g = enumerator.Current;
				writer.SafeWrite(delegate(BinaryWriter w)
				{
					g.NetSend(item, w);
				});
			}
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x00523F98 File Offset: 0x00522198
		public static void ReceiveModData(Item item, BinaryReader reader)
		{
			if (item.IsAir)
			{
				return;
			}
			try
			{
				reader.SafeRead(delegate(BinaryReader r)
				{
					ModItem modItem = item.ModItem;
					if (modItem == null)
					{
						return;
					}
					modItem.NetReceive(r);
				});
			}
			catch (IOException e)
			{
				Logging.tML.Error(e.ToString());
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Above IOException error caused by ");
				defaultInterpolatedStringHandler.AppendFormatted(item.ModItem.Name);
				defaultInterpolatedStringHandler.AppendLiteral(" from the ");
				defaultInterpolatedStringHandler.AppendFormatted(item.ModItem.Mod.Name);
				defaultInterpolatedStringHandler.AppendLiteral(" mod.");
				tML.Error(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookNetReceive.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				GlobalItem g = enumerator.Current;
				try
				{
					reader.SafeRead(delegate(BinaryReader r)
					{
						g.NetReceive(item, r);
					});
				}
				catch (IOException e2)
				{
					Logging.tML.Error(e2.ToString());
					ILog tML2 = Logging.tML;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(64, 3);
					defaultInterpolatedStringHandler.AppendLiteral("Above IOException error caused by ");
					defaultInterpolatedStringHandler.AppendFormatted(g.Name);
					defaultInterpolatedStringHandler.AppendLiteral(" from the ");
					defaultInterpolatedStringHandler.AppendFormatted(g.Mod.Name);
					defaultInterpolatedStringHandler.AppendLiteral(" mod while reading ");
					defaultInterpolatedStringHandler.AppendFormatted(item.Name);
					defaultInterpolatedStringHandler.AppendLiteral(".");
					tML2.Error(defaultInterpolatedStringHandler.ToStringAndClear());
				}
			}
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x0052417C File Offset: 0x0052237C
		internal static byte[] LegacyModData(int type, BinaryReader reader, bool hasGlobalSaving = true)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(memoryStream))
				{
					if (type >= (int)ItemID.Count)
					{
						ushort length = reader.ReadUInt16();
						writer.Write(length);
						writer.Write(reader.ReadBytes((int)length));
					}
					if (hasGlobalSaving)
					{
						ushort count = reader.ReadUInt16();
						writer.Write(count);
						for (int i = 0; i < (int)count; i++)
						{
							writer.Write(reader.ReadString());
							writer.Write(reader.ReadString());
							ushort length2 = reader.ReadUInt16();
							writer.Write(length2);
							writer.Write(reader.ReadBytes((int)length2));
						}
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x00524250 File Offset: 0x00522450
		public static string ToBase64(Item item)
		{
			MemoryStream ms = new MemoryStream();
			TagIO.ToStream(ItemIO.Save(item), ms, true);
			return Convert.ToBase64String(ms.ToArray());
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x0052427B File Offset: 0x0052247B
		public static Item FromBase64(string base64)
		{
			return ItemIO.Load(TagIO.FromStream(Convert.FromBase64String(base64).ToMemoryStream(false), true));
		}
	}
}
