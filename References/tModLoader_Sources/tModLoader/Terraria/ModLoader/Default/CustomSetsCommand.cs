using System;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002BE RID: 702
	internal class CustomSetsCommand : ModCommand
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x0052E110 File Offset: 0x0052C310
		public override string Command
		{
			get
			{
				return "customsets";
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x0052E117 File Offset: 0x0052C317
		public override CommandType Type
		{
			get
			{
				return CommandType.Chat | CommandType.Console;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x0052E11A File Offset: 0x0052C31A
		public override string Description
		{
			get
			{
				return Language.GetTextValue("tModLoader.CommandCustomSetsDescription");
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x0052E126 File Offset: 0x0052C326
		public override string Usage
		{
			get
			{
				return Language.GetTextValue("tModLoader.CommandCustomSetsUsage");
			}
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x0052E134 File Offset: 0x0052C334
		public override void Action(CommandCaller caller, string input, string[] args)
		{
			bool printValues = false;
			bool openFile = false;
			string searchTerm = null;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == "-h")
				{
					caller.Reply(this.Usage, default(Color));
					return;
				}
				if (args[i] == "-v")
				{
					printValues = true;
				}
				else if (args[i] == "-o")
				{
					openFile = true;
				}
				else
				{
					searchTerm = args[i];
				}
			}
			StringBuilder sb = new StringBuilder();
			if (searchTerm != null)
			{
				StringBuilder stringBuilder = sb;
				StringBuilder stringBuilder2 = stringBuilder;
				StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(53, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("Outputting all named ID sets from the search term '");
				appendInterpolatedStringHandler.AppendFormatted(searchTerm);
				appendInterpolatedStringHandler.AppendLiteral("':");
				stringBuilder2.AppendLine(ref appendInterpolatedStringHandler);
			}
			foreach (SetFactory setFactory in SetFactory.SetFactories)
			{
				string metadata = setFactory.CustomMetadataInfo(searchTerm, printValues);
				sb.Append(metadata);
			}
			string outputText = sb.ToString();
			caller.Reply(outputText, default(Color));
			string outputPath = Path.Combine(Logging.LogDir, "CustomSets.txt");
			File.WriteAllText(outputPath, outputText);
			if (openFile)
			{
				Utils.OpenFolder(Logging.LogDir);
			}
			caller.Reply("Data written to '" + outputPath + "'", default(Color));
		}
	}
}
