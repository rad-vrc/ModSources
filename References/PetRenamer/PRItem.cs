using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace PetRenamer
{
	// Token: 0x02000006 RID: 6
	public class PRItem : GlobalItem
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000027F1 File Offset: 0x000009F1
		public override bool AppliesToEntity(Item entity, bool lateInstantiation)
		{
			return lateInstantiation && PetRenamer.IsPetItem(entity);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000027FE File Offset: 0x000009FE
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002805 File Offset: 0x00000A05
		public static LocalizedText PetNameText { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000280D File Offset: 0x00000A0D
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002814 File Offset: 0x00000A14
		public static LocalizedText PetOwnerText { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000281C File Offset: 0x00000A1C
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002823 File Offset: 0x00000A23
		public static LocalizedText PetRenameText { get; private set; }

		// Token: 0x06000031 RID: 49 RVA: 0x0000282C File Offset: 0x00000A2C
		public override void Load()
		{
			string category = "Items.PetItems.";
			if (PRItem.PetNameText == null)
			{
				PRItem.PetNameText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "PetName"), null);
			}
			if (PRItem.PetOwnerText == null)
			{
				PRItem.PetOwnerText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "PetOwner"), null);
			}
			if (PRItem.PetRenameText == null)
			{
				PRItem.PetRenameText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "PetRename"), null);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028BD File Offset: 0x00000ABD
		public PRItem()
		{
			this.petName = string.Empty;
			this.petOwner = string.Empty;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002050 File Offset: 0x00000250
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028DB File Offset: 0x00000ADB
		public override GlobalItem Clone(Item item, Item itemClone)
		{
			PRItem pritem = (PRItem)base.Clone(item, itemClone);
			pritem.petName = this.petName;
			pritem.petOwner = this.petOwner;
			return pritem;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002904 File Offset: 0x00000B04
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			Color color = Color.Lerp(Color.White, Color.Orange, 0.4f);
			if (this.petName.Length > 0)
			{
				tooltips.Add(new TooltipLine(base.Mod, "PetName", PRItem.PetNameText.Format(new object[]
				{
					this.petName
				}))
				{
					OverrideColor = new Color?(color)
				});
				tooltips.Add(new TooltipLine(base.Mod, "PetOwner", PRItem.PetOwnerText.Format(new object[]
				{
					this.petOwner
				}))
				{
					OverrideColor = new Color?(color)
				});
				return;
			}
			List<string> keys = PetRenamer.RenamePetUIHotkey.GetAssignedKeys(InputMode.Keyboard);
			string key;
			if (keys.Count == 0)
			{
				key = PetRenamer.UnboundText.ToString();
			}
			else
			{
				key = keys[0].ToString();
			}
			tooltips.Add(new TooltipLine(base.Mod, "PetRename", PRItem.PetRenameText.Format(new object[]
			{
				key
			}))
			{
				OverrideColor = new Color?(color)
			});
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A0F File Offset: 0x00000C0F
		public override void LoadData(Item item, TagCompound tag)
		{
			this.petName = tag.GetString("petName");
			this.petOwner = tag.GetString("petOwner");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A33 File Offset: 0x00000C33
		public override void SaveData(Item item, TagCompound tag)
		{
			if (this.petName.Length == 0)
			{
				return;
			}
			tag.Add("petName", this.petName);
			tag.Add("petOwner", this.petOwner);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A65 File Offset: 0x00000C65
		public override void NetSend(Item item, BinaryWriter writer)
		{
			writer.Write(this.petName);
			writer.Write(this.petOwner);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A7F File Offset: 0x00000C7F
		public override void NetReceive(Item item, BinaryReader reader)
		{
			this.petName = reader.ReadString();
			this.petOwner = reader.ReadString();
		}

		// Token: 0x04000019 RID: 25
		public string petName;

		// Token: 0x0400001A RID: 26
		public string petOwner;
	}
}
