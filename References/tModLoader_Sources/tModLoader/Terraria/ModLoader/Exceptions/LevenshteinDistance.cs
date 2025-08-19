using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002A4 RID: 676
	internal static class LevenshteinDistance
	{
		// Token: 0x06002CEB RID: 11499 RVA: 0x0052ADDC File Offset: 0x00528FDC
		internal static string FolderAwareEditDistance(string source, string[] targets)
		{
			if (targets.Length == 0)
			{
				return null;
			}
			char separator = '/';
			string[] sourceParts = source.Split(separator, StringSplitOptions.None);
			List<string> sourceFolders = sourceParts.Reverse<string>().Skip(1).ToList<string>();
			string sourceFile = sourceParts.Last<string>();
			int missingFolderPenalty = 4;
			int extraFolderPenalty = 3;
			var source2 = targets.Select(delegate(string target)
			{
				string[] targetParts = target.Split(separator, StringSplitOptions.None);
				List<string> targetFolders = targetParts.Reverse<string>().Skip(1).ToList<string>();
				string targetFile = targetParts.Last<string>();
				IEnumerable<string> commonFolders = from x in sourceFolders
				where targetFolders.Contains(x)
				select x;
				List<string> reducedSourceFolders = sourceFolders.Except(commonFolders).ToList<string>();
				List<string> reducedTargetFolders = targetFolders.Except(commonFolders).ToList<string>();
				int score = 0;
				int folderDiff = reducedSourceFolders.Count - reducedTargetFolders.Count;
				if (folderDiff > 0)
				{
					score += folderDiff * missingFolderPenalty;
				}
				else if (folderDiff < 0)
				{
					score += -folderDiff * extraFolderPenalty;
				}
				if (reducedSourceFolders.Count > 0 && reducedSourceFolders.Count >= reducedTargetFolders.Count)
				{
					using (List<string>.Enumerator enumerator = reducedTargetFolders.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string item = enumerator.Current;
							int min = int.MaxValue;
							foreach (string item2 in reducedSourceFolders)
							{
								min = Math.Min(min, LevenshteinDistance.Compute(item, item2));
							}
							score += min;
						}
						goto IL_1CB;
					}
				}
				if (reducedSourceFolders.Count > 0)
				{
					foreach (string item3 in reducedSourceFolders)
					{
						int min2 = int.MaxValue;
						foreach (string item4 in reducedTargetFolders)
						{
							min2 = Math.Min(min2, LevenshteinDistance.Compute(item3, item4));
						}
						score += min2;
					}
				}
				IL_1CB:
				score += LevenshteinDistance.Compute(targetFile, sourceFile);
				return new
				{
					Target = target,
					Score = score
				};
			});
			from x in source2
			orderby x.Score
			select x;
			return (from x in source2
			orderby x.Score
			select x).First().Target;
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x0052AEA4 File Offset: 0x005290A4
		public static int Compute(string s, string t)
		{
			int i = s.Length;
			int j = t.Length;
			int[,] d = new int[i + 1, j + 1];
			if (i == 0)
			{
				return j;
			}
			if (j == 0)
			{
				return i;
			}
			int k = 0;
			while (k <= i)
			{
				d[k, 0] = k++;
			}
			int l = 0;
			while (l <= j)
			{
				d[0, l] = l++;
			}
			for (int m = 1; m <= i; m++)
			{
				for (int n = 1; n <= j; n++)
				{
					int cost = (t[n - 1] == s[m - 1]) ? 0 : 2;
					d[m, n] = Math.Min(Math.Min(d[m - 1, n] + 2, d[m, n - 1] + 2), d[m - 1, n - 1] + cost);
				}
			}
			return d[i, j];
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x0052AF8C File Offset: 0x0052918C
		public static ValueTuple<string, string> ComputeColorTaggedString(string s, string t)
		{
			int i = s.Length;
			int j = t.Length;
			int[,] d = new int[i + 1, j + 1];
			if (i == 0)
			{
				return new ValueTuple<string, string>("", "");
			}
			if (j == 0)
			{
				return new ValueTuple<string, string>("", "");
			}
			int k = 0;
			while (k <= i)
			{
				d[k, 0] = k++;
			}
			int l = 0;
			while (l <= j)
			{
				d[0, l] = l++;
			}
			for (int m = 1; m <= i; m++)
			{
				for (int n = 1; n <= j; n++)
				{
					int cost = (t[n - 1] != s[m - 1]) ? 1 : 0;
					d[m, n] = Math.Min(Math.Min(d[m - 1, n] + 1, d[m, n - 1] + 1), d[m - 1, n - 1] + cost);
				}
			}
			int x = i;
			int y = j;
			Stack<ValueTuple<LevenshteinDistance.Edits, char>> editsFromStoT = new Stack<ValueTuple<LevenshteinDistance.Edits, char>>();
			Stack<ValueTuple<LevenshteinDistance.Edits, char>> editsFromTtoS = new Stack<ValueTuple<LevenshteinDistance.Edits, char>>();
			while (x != 0 || y != 0)
			{
				int cost2 = d[x, y];
				if (y - 1 < 0)
				{
					editsFromStoT.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Delete, s[x - 1]));
					editsFromTtoS.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Blank, ' '));
					x--;
				}
				else if (x - 1 < 0)
				{
					editsFromStoT.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Insert, t[y - 1]));
					editsFromTtoS.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Blank, ' '));
					y--;
				}
				else
				{
					int costLeft = d[x, y - 1];
					int costUp = d[x - 1, y];
					int costDiagonal = d[x - 1, y - 1];
					if (costDiagonal <= costLeft && costDiagonal <= costUp && (costDiagonal == cost2 - 1 || costDiagonal == cost2))
					{
						if (costDiagonal == cost2 - 1)
						{
							editsFromStoT.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Substitute, s[x - 1]));
							editsFromTtoS.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Substitute, t[y - 1]));
							x--;
							y--;
						}
						else
						{
							editsFromStoT.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Keep, s[x - 1]));
							editsFromTtoS.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Keep, t[y - 1]));
							x--;
							y--;
						}
					}
					else if (costLeft <= costDiagonal && costLeft == cost2 - 1)
					{
						editsFromStoT.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Insert, t[y - 1]));
						editsFromTtoS.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Blank, ' '));
						y--;
					}
					else
					{
						editsFromStoT.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Delete, s[x - 1]));
						editsFromTtoS.Push(new ValueTuple<LevenshteinDistance.Edits, char>(LevenshteinDistance.Edits.Blank, ' '));
						x--;
					}
				}
			}
			string item = LevenshteinDistance.<ComputeColorTaggedString>g__FinalizeText|3_0(editsFromStoT);
			string resultB = LevenshteinDistance.<ComputeColorTaggedString>g__FinalizeText|3_0(editsFromTtoS);
			return new ValueTuple<string, string>(item, resultB);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x0052B268 File Offset: 0x00529468
		[CompilerGenerated]
		internal static string <ComputeColorTaggedString>g__FinalizeText|3_0(Stack<ValueTuple<LevenshteinDistance.Edits, char>> results)
		{
			string result = "";
			LevenshteinDistance.Edits editCurrent = LevenshteinDistance.Edits.Keep;
			while (results.Count > 0)
			{
				ValueTuple<LevenshteinDistance.Edits, char> entry = results.Pop();
				LevenshteinDistance.Edits nextEdit = entry.Item1;
				if (editCurrent != nextEdit)
				{
					if (editCurrent != LevenshteinDistance.Edits.Keep && editCurrent != LevenshteinDistance.Edits.Blank)
					{
						result += "]";
					}
					if (nextEdit == LevenshteinDistance.Edits.Delete)
					{
						result += "[c/ff0000:";
					}
					else if (nextEdit == LevenshteinDistance.Edits.Insert)
					{
						result += "[c/00ff00:";
					}
					else if (nextEdit == LevenshteinDistance.Edits.Substitute)
					{
						result += "[c/ffff00:";
					}
				}
				result += entry.Item2.ToString();
				editCurrent = nextEdit;
			}
			if (editCurrent != LevenshteinDistance.Edits.Keep && editCurrent != LevenshteinDistance.Edits.Blank)
			{
				result += "]";
			}
			return result;
		}

		// Token: 0x02000A4D RID: 2637
		private enum Edits
		{
			// Token: 0x04006CD4 RID: 27860
			Keep,
			// Token: 0x04006CD5 RID: 27861
			Delete,
			// Token: 0x04006CD6 RID: 27862
			Insert,
			// Token: 0x04006CD7 RID: 27863
			Substitute,
			// Token: 0x04006CD8 RID: 27864
			Blank
		}
	}
}
