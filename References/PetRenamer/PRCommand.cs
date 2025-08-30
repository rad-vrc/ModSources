using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PetRenamer
{
	// Token: 0x02000005 RID: 5
	internal class PRCommand : ModCommand
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000244D File Offset: 0x0000064D
		public static string CommandStart
		{
			get
			{
				return "/renamepet ";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002050 File Offset: 0x00000250
		public override CommandType Type
		{
			get
			{
				return CommandType.Chat;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002454 File Offset: 0x00000654
		public override string Command
		{
			get
			{
				return "renamepet";
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000245B File Offset: 0x0000065B
		public override string Usage
		{
			get
			{
				return "/renamepet newName";
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002462 File Offset: 0x00000662
		public override string Description
		{
			get
			{
				return PRCommand.DescriptionText.ToString();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000246E File Offset: 0x0000066E
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002475 File Offset: 0x00000675
		public static LocalizedText DescriptionText { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000247D File Offset: 0x0000067D
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002484 File Offset: 0x00000684
		public static LocalizedText NicknameResetText { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002493 File Offset: 0x00000693
		public static LocalizedText NamedNewText { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000249B File Offset: 0x0000069B
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000024A2 File Offset: 0x000006A2
		public static LocalizedText NamedSameText { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000024AA File Offset: 0x000006AA
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000024B1 File Offset: 0x000006B1
		public static LocalizedText RenamedText { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000024B9 File Offset: 0x000006B9
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000024C0 File Offset: 0x000006C0
		public static LocalizedText NoItemText { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000024C8 File Offset: 0x000006C8
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000024CF File Offset: 0x000006CF
		public static LocalizedText InvalidItemText { get; private set; }

		// Token: 0x06000027 RID: 39 RVA: 0x000024D8 File Offset: 0x000006D8
		public override void Load()
		{
			string category = "Commands.PRCommand.";
			if (PRCommand.DescriptionText == null)
			{
				PRCommand.DescriptionText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "Description"), null).WithFormatArgs(new object[]
				{
					"newName",
					"reset"
				});
			}
			if (PRCommand.NicknameResetText == null)
			{
				PRCommand.NicknameResetText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "NicknameReset"), null);
			}
			if (PRCommand.NamedNewText == null)
			{
				PRCommand.NamedNewText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "NamedNew"), null);
			}
			if (PRCommand.NamedSameText == null)
			{
				PRCommand.NamedSameText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "NamedSame"), null);
			}
			if (PRCommand.RenamedText == null)
			{
				PRCommand.RenamedText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "Renamed"), null);
			}
			if (PRCommand.NoItemText == null)
			{
				PRCommand.NoItemText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "NoItem"), null);
			}
			if (PRCommand.InvalidItemText == null)
			{
				PRCommand.InvalidItemText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "InvalidItem"), null);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000262C File Offset: 0x0000082C
		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length < 1)
			{
				caller.Reply(this.Usage, default(Color));
				return;
			}
			Item item = Main.mouseItem;
			PRItem petItem;
			if (item.TryGetGlobalItem<PRItem>(out petItem))
			{
				string previousName = petItem.petName;
				string newName = string.Join(" ", args);
				if (previousName != string.Empty && newName == "reset")
				{
					petItem.petName = string.Empty;
					petItem.petOwner = string.Empty;
					caller.Reply(PRCommand.NicknameResetText.Format(new object[]
					{
						previousName
					}), Color.OrangeRed);
					return;
				}
				petItem.petName = newName;
				petItem.petOwner = caller.Player.name;
				if (previousName == string.Empty)
				{
					caller.Reply(PRCommand.NamedNewText.Format(new object[]
					{
						item.Name,
						petItem.petName
					}), Color.Orange);
				}
				else if (previousName == petItem.petName)
				{
					caller.Reply(PRCommand.NamedSameText.Format(new object[]
					{
						previousName
					}), Color.OrangeRed);
				}
				else
				{
					caller.Reply(PRCommand.RenamedText.Format(new object[]
					{
						item.Name,
						previousName,
						petItem.petName
					}), Color.Orange);
				}
				if (previousName != petItem.petName)
				{
					SoundEngine.PlaySound(SoundID.ResearchComplete, null, null);
					return;
				}
			}
			else
			{
				if (item.type == 0)
				{
					caller.Reply(PRCommand.NoItemText.ToString(), Color.OrangeRed);
					return;
				}
				caller.Reply(PRCommand.InvalidItemText.Format(new object[]
				{
					item.Name
				}), Color.OrangeRed);
			}
		}

		// Token: 0x0400000C RID: 12
		private const string COMMANDNAME = "renamepet";

		// Token: 0x0400000D RID: 13
		private const string ARGUMENT = "newName";

		// Token: 0x0400000E RID: 14
		private const string RESET = "reset";
	}
}
