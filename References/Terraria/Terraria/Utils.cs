using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using ReLogic.Graphics;
using ReLogic.OS;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.Utilities;
using Terraria.Utilities.Terraria.Utilities;

namespace Terraria
{
	// Token: 0x0200004B RID: 75
	public static class Utils
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x0003EEAA File Offset: 0x0003D0AA
		public static Color ColorLerp_BlackToWhite(float percent)
		{
			return Color.Lerp(Color.Black, Color.White, percent);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x003ED815 File Offset: 0x003EBA15
		public static double Lerp(double value1, double value2, double amount)
		{
			return value1 + (value2 - value1) * amount;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x003ED81E File Offset: 0x003EBA1E
		public static Vector2 Round(Vector2 input)
		{
			return new Vector2((float)Math.Round((double)input.X), (float)Math.Round((double)input.Y));
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x003ED83F File Offset: 0x003EBA3F
		public static bool IsPowerOfTwo(int x)
		{
			return x != 0 && (x & x - 1) == 0;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x003ED84E File Offset: 0x003EBA4E
		public static float SmoothStep(float min, float max, float x)
		{
			return MathHelper.Clamp((x - min) / (max - min), 0f, 1f);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x003ED866 File Offset: 0x003EBA66
		public static double SmoothStep(double min, double max, double x)
		{
			return Utils.Clamp<double>((x - min) / (max - min), 0.0, 1.0);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x003ED886 File Offset: 0x003EBA86
		public static float UnclampedSmoothStep(float min, float max, float x)
		{
			return (x - min) / (max - min);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x003ED886 File Offset: 0x003EBA86
		public static double UnclampedSmoothStep(double min, double max, double x)
		{
			return (x - min) / (max - min);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x003ED890 File Offset: 0x003EBA90
		public static Dictionary<string, string> ParseArguements(string[] args)
		{
			string text = null;
			string text2 = "";
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].Length != 0)
				{
					if (args[i][0] == '-' || args[i][0] == '+')
					{
						if (text != null)
						{
							dictionary.Add(text.ToLower(), text2);
						}
						text = args[i];
						text2 = "";
					}
					else
					{
						if (text2 != "")
						{
							text2 += " ";
						}
						text2 += args[i];
					}
				}
			}
			if (text != null)
			{
				dictionary.Add(text.ToLower(), text2);
			}
			return dictionary;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x003ED93C File Offset: 0x003EBB3C
		public static void Swap<T>(ref T t1, ref T t2)
		{
			T t3 = t1;
			t1 = t2;
			t2 = t3;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x003ED963 File Offset: 0x003EBB63
		public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
		{
			if (value.CompareTo(max) > 0)
			{
				return max;
			}
			if (value.CompareTo(min) < 0)
			{
				return min;
			}
			return value;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x003ED98C File Offset: 0x003EBB8C
		public static float Turn01ToCyclic010(float value)
		{
			return 1f - ((float)Math.Cos((double)(value * 6.2831855f)) * 0.5f + 0.5f);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x003ED9AE File Offset: 0x003EBBAE
		public static float PingPongFrom01To010(float value)
		{
			value %= 1f;
			if (value < 0f)
			{
				value += 1f;
			}
			if (value >= 0.5f)
			{
				return 2f - value * 2f;
			}
			return value * 2f;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x003ED9E8 File Offset: 0x003EBBE8
		public static float MultiLerp(float percent, params float[] floats)
		{
			float num = 1f / ((float)floats.Length - 1f);
			float num2 = num;
			int num3 = 0;
			while (percent / num2 > 1f && num3 < floats.Length - 2)
			{
				num2 += num;
				num3++;
			}
			return MathHelper.Lerp(floats[num3], floats[num3 + 1], (percent - num * (float)num3) / num);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x003EDA3C File Offset: 0x003EBC3C
		public static float WrappedLerp(float value1, float value2, float percent)
		{
			float num = percent * 2f;
			if (num > 1f)
			{
				num = 2f - num;
			}
			return MathHelper.Lerp(value1, value2, num);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x003EDA69 File Offset: 0x003EBC69
		public static float GetLerpValue(float from, float to, float t, bool clamped = false)
		{
			if (clamped)
			{
				if (from < to)
				{
					if (t < from)
					{
						return 0f;
					}
					if (t > to)
					{
						return 1f;
					}
				}
				else
				{
					if (t < to)
					{
						return 1f;
					}
					if (t > from)
					{
						return 0f;
					}
				}
			}
			return (t - from) / (to - from);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x003EDAA1 File Offset: 0x003EBCA1
		public static float Remap(float fromValue, float fromMin, float fromMax, float toMin, float toMax, bool clamped = true)
		{
			return MathHelper.Lerp(toMin, toMax, Utils.GetLerpValue(fromMin, fromMax, fromValue, clamped));
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x003EDAB8 File Offset: 0x003EBCB8
		public static void ClampWithinWorld(ref int minX, ref int minY, ref int maxX, ref int maxY, bool lastValuesInclusiveToIteration = false, int fluffX = 0, int fluffY = 0)
		{
			int num = lastValuesInclusiveToIteration ? 1 : 0;
			minX = Utils.Clamp<int>(minX, fluffX, Main.maxTilesX - num - fluffX);
			maxX = Utils.Clamp<int>(maxX, fluffX, Main.maxTilesX - num - fluffX);
			minY = Utils.Clamp<int>(minY, fluffY, Main.maxTilesY - num - fluffY);
			maxY = Utils.Clamp<int>(maxY, fluffY, Main.maxTilesY - num - fluffY);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x003EDB24 File Offset: 0x003EBD24
		public static Utils.ChaseResults GetChaseResults(Vector2 chaserPosition, float chaserSpeed, Vector2 runnerPosition, Vector2 runnerVelocity)
		{
			Utils.ChaseResults chaseResults = default(Utils.ChaseResults);
			if (chaserPosition == runnerPosition)
			{
				return new Utils.ChaseResults
				{
					InterceptionHappens = true,
					InterceptionPosition = chaserPosition,
					InterceptionTime = 0f,
					ChaserVelocity = Vector2.Zero
				};
			}
			if (chaserSpeed <= 0f)
			{
				return default(Utils.ChaseResults);
			}
			Vector2 value = chaserPosition - runnerPosition;
			float num = value.Length();
			float num2 = runnerVelocity.Length();
			if (num2 == 0f)
			{
				chaseResults.InterceptionTime = num / chaserSpeed;
				chaseResults.InterceptionPosition = runnerPosition;
			}
			else
			{
				float a = chaserSpeed * chaserSpeed - num2 * num2;
				float b = 2f * Vector2.Dot(value, runnerVelocity);
				float c = -num * num;
				float num3;
				float num4;
				if (!Utils.SolveQuadratic(a, b, c, out num3, out num4))
				{
					return default(Utils.ChaseResults);
				}
				if (num3 < 0f && num4 < 0f)
				{
					return default(Utils.ChaseResults);
				}
				if (num3 > 0f && num4 > 0f)
				{
					chaseResults.InterceptionTime = Math.Min(num3, num4);
				}
				else
				{
					chaseResults.InterceptionTime = Math.Max(num3, num4);
				}
				chaseResults.InterceptionPosition = runnerPosition + runnerVelocity * chaseResults.InterceptionTime;
			}
			chaseResults.ChaserVelocity = (chaseResults.InterceptionPosition - chaserPosition) / chaseResults.InterceptionTime;
			chaseResults.InterceptionHappens = true;
			return chaseResults;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x003EDC88 File Offset: 0x003EBE88
		public static Vector2 FactorAcceleration(Vector2 currentVelocity, float timeToInterception, Vector2 descendOfProjectile, int framesOfLenience)
		{
			float num = Math.Max(0f, timeToInterception - (float)framesOfLenience);
			Vector2 value = descendOfProjectile * (num * num) / 2f / timeToInterception;
			return currentVelocity - value;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x003EDCC8 File Offset: 0x003EBEC8
		public static bool SolveQuadratic(float a, float b, float c, out float result1, out float result2)
		{
			float num = b * b - 4f * a * c;
			result1 = 0f;
			result2 = 0f;
			if (num > 0f)
			{
				result1 = (-b + (float)Math.Sqrt((double)num)) / (2f * a);
				result2 = (-b - (float)Math.Sqrt((double)num)) / (2f * a);
				return true;
			}
			if (num < 0f)
			{
				return false;
			}
			result1 = (result2 = (-b + (float)Math.Sqrt((double)num)) / (2f * a));
			return true;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x003EDD50 File Offset: 0x003EBF50
		public static double GetLerpValue(double from, double to, double t, bool clamped = false)
		{
			if (clamped)
			{
				if (from < to)
				{
					if (t < from)
					{
						return 0.0;
					}
					if (t > to)
					{
						return 1.0;
					}
				}
				else
				{
					if (t < to)
					{
						return 1.0;
					}
					if (t > from)
					{
						return 0.0;
					}
				}
			}
			return (t - from) / (to - from);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x003EDDA4 File Offset: 0x003EBFA4
		public static float GetDayTimeAs24FloatStartingFromMidnight()
		{
			if (Main.dayTime)
			{
				return 4.5f + (float)(Main.time / 54000.0) * 15f;
			}
			return 19.5f + (float)(Main.time / 32400.0) * 9f;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x003EDDF1 File Offset: 0x003EBFF1
		public static Vector2 GetDayTimeAsDirectionIn24HClock()
		{
			return Utils.GetDayTimeAsDirectionIn24HClock(Utils.GetDayTimeAs24FloatStartingFromMidnight());
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x003EDE00 File Offset: 0x003EC000
		public static Vector2 GetDayTimeAsDirectionIn24HClock(float timeFrom0To24)
		{
			return new Vector2(0f, -1f).RotatedBy((double)(timeFrom0To24 / 24f * 6.2831855f), default(Vector2));
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x003EDE38 File Offset: 0x003EC038
		public static string[] ConvertMonoArgsToDotNet(string[] brokenArgs)
		{
			ArrayList arrayList = new ArrayList();
			string text = "";
			for (int i = 0; i < brokenArgs.Length; i++)
			{
				if (brokenArgs[i].StartsWith("-"))
				{
					if (text != "")
					{
						arrayList.Add(text);
						text = "";
					}
					else
					{
						arrayList.Add("");
					}
					arrayList.Add(brokenArgs[i]);
				}
				else
				{
					if (text != "")
					{
						text += " ";
					}
					text += brokenArgs[i];
				}
			}
			arrayList.Add(text);
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x003EDEE4 File Offset: 0x003EC0E4
		public static T Max<T>(params T[] args) where T : IComparable
		{
			T result = args[0];
			for (int i = 1; i < args.Length; i++)
			{
				if (result.CompareTo(args[i]) < 0)
				{
					result = args[i];
				}
			}
			return result;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x003EDF2C File Offset: 0x003EC12C
		public static float LineRectangleDistance(Rectangle rect, Vector2 lineStart, Vector2 lineEnd)
		{
			Vector2 vector = rect.TopLeft();
			Vector2 vector2 = rect.TopRight();
			Vector2 vector3 = rect.BottomLeft();
			Vector2 vector4 = rect.BottomRight();
			if (lineStart.Between(vector, vector4) || lineEnd.Between(vector, vector4))
			{
				return 0f;
			}
			float value = vector.Distance(vector.ClosestPointOnLine(lineStart, lineEnd));
			float value2 = vector2.Distance(vector2.ClosestPointOnLine(lineStart, lineEnd));
			float value3 = vector3.Distance(vector3.ClosestPointOnLine(lineStart, lineEnd));
			float value4 = vector4.Distance(vector4.ClosestPointOnLine(lineStart, lineEnd));
			return MathHelper.Min(value, MathHelper.Min(value2, MathHelper.Min(value3, value4)));
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x003EDFC4 File Offset: 0x003EC1C4
		public static List<List<TextSnippet>> WordwrapStringSmart(string text, Color c, DynamicSpriteFont font, int maxWidth, int maxLines)
		{
			TextSnippet[] array = ChatManager.ParseMessage(text, c).ToArray();
			List<List<TextSnippet>> list = new List<List<TextSnippet>>();
			List<TextSnippet> list2 = new List<TextSnippet>();
			foreach (TextSnippet textSnippet in array)
			{
				string[] array2 = textSnippet.Text.Split(new char[]
				{
					'\n'
				});
				for (int j = 0; j < array2.Length - 1; j++)
				{
					list2.Add(textSnippet.CopyMorph(array2[j]));
					list.Add(list2);
					list2 = new List<TextSnippet>();
				}
				list2.Add(textSnippet.CopyMorph(array2[array2.Length - 1]));
			}
			list.Add(list2);
			if (maxWidth != -1)
			{
				for (int k = 0; k < list.Count; k++)
				{
					List<TextSnippet> list3 = list[k];
					float num = 0f;
					for (int l = 0; l < list3.Count; l++)
					{
						float stringLength = list3[l].GetStringLength(font);
						if (stringLength + num > (float)maxWidth)
						{
							int num2 = maxWidth - (int)num;
							if (num > 0f)
							{
								num2 -= 16;
							}
							int num3 = Math.Min(list3[l].Text.Length, num2 / 8);
							if (num3 < 0)
							{
								num3 = 0;
							}
							string[] array3 = list3[l].Text.Split(new char[]
							{
								' '
							});
							int num4 = num3;
							if (array3.Length > 1)
							{
								num4 = 0;
								for (int m = 0; m < array3.Length; m++)
								{
									bool flag = num4 == 0;
									if (num4 + array3[m].Length > num3 && !flag)
									{
										break;
									}
									num4 += array3[m].Length + 1;
								}
								if (num4 > num3)
								{
									num4 = num3;
								}
							}
							string newText = list3[l].Text.Substring(0, num4);
							string newText2 = list3[l].Text.Substring(num4);
							list2 = new List<TextSnippet>
							{
								list3[l].CopyMorph(newText2)
							};
							for (int n = l + 1; n < list3.Count; n++)
							{
								list2.Add(list3[n]);
							}
							list3[l] = list3[l].CopyMorph(newText);
							list[k] = list[k].Take(l + 1).ToList<TextSnippet>();
							list.Insert(k + 1, list2);
							break;
						}
						num += stringLength;
					}
				}
			}
			if (maxLines != -1)
			{
				while (list.Count > maxLines)
				{
					list.RemoveAt(maxLines);
				}
			}
			return list;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x003EE26C File Offset: 0x003EC46C
		public static string[] WordwrapString(string text, DynamicSpriteFont font, int maxWidth, int maxLines, out int lineAmount)
		{
			string[] array = new string[maxLines];
			int num = 0;
			List<string> list = new List<string>(text.Split(new char[]
			{
				'\n'
			}));
			List<string> list2 = new List<string>(list[0].Split(new char[]
			{
				' '
			}));
			int num2 = 1;
			while (num2 < list.Count && num2 < maxLines)
			{
				list2.Add("\n");
				list2.AddRange(list[num2].Split(new char[]
				{
					' '
				}));
				num2++;
			}
			bool flag = true;
			while (list2.Count > 0)
			{
				string text2 = list2[0];
				string str = " ";
				if (list2.Count == 1)
				{
					str = "";
				}
				if (text2 == "\n")
				{
					string[] array2 = array;
					int num3 = num++;
					array2[num3] += text2;
					flag = true;
					if (num >= maxLines)
					{
						break;
					}
					list2.RemoveAt(0);
				}
				else if (flag)
				{
					if (font.MeasureString(text2).X > (float)maxWidth)
					{
						string str2 = text2[0].ToString() ?? "";
						int num4 = 1;
						while (font.MeasureString(str2 + text2[num4].ToString() + "-").X <= (float)maxWidth)
						{
							str2 += text2[num4++].ToString();
						}
						str2 += "-";
						array[num++] = str2 + " ";
						if (num >= maxLines)
						{
							break;
						}
						list2.RemoveAt(0);
						list2.Insert(0, text2.Substring(num4));
					}
					else
					{
						string[] array3 = array;
						int num5 = num;
						array3[num5] = array3[num5] + text2 + str;
						flag = false;
						list2.RemoveAt(0);
					}
				}
				else if (font.MeasureString(array[num] + text2).X > (float)maxWidth)
				{
					num++;
					if (num >= maxLines)
					{
						break;
					}
					flag = true;
				}
				else
				{
					string[] array4 = array;
					int num6 = num;
					array4[num6] = array4[num6] + text2 + str;
					flag = false;
					list2.RemoveAt(0);
				}
			}
			lineAmount = num;
			if (lineAmount == maxLines)
			{
				lineAmount--;
			}
			return array;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x003EE4A5 File Offset: 0x003EC6A5
		public static Rectangle CenteredRectangle(Vector2 center, Vector2 size)
		{
			return new Rectangle((int)(center.X - size.X / 2f), (int)(center.Y - size.Y / 2f), (int)size.X, (int)size.Y);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x003EE4E4 File Offset: 0x003EC6E4
		public static Vector2 Vector2FromElipse(Vector2 angleVector, Vector2 elipseSizes)
		{
			if (elipseSizes == Vector2.Zero)
			{
				return Vector2.Zero;
			}
			if (angleVector == Vector2.Zero)
			{
				return Vector2.Zero;
			}
			angleVector.Normalize();
			Vector2 value = Vector2.Normalize(elipseSizes);
			value = Vector2.One / value;
			angleVector *= value;
			angleVector.Normalize();
			return angleVector * elipseSizes / 2f;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x003EE552 File Offset: 0x003EC752
		public static bool FloatIntersect(float r1StartX, float r1StartY, float r1Width, float r1Height, float r2StartX, float r2StartY, float r2Width, float r2Height)
		{
			return r1StartX <= r2StartX + r2Width && r1StartY <= r2StartY + r2Height && r1StartX + r1Width >= r2StartX && r1StartY + r1Height >= r2StartY;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x003EE578 File Offset: 0x003EC778
		public static long CoinsCount(out bool overFlowing, Item[] inv, params int[] ignoreSlots)
		{
			List<int> list = new List<int>(ignoreSlots);
			long num = 0L;
			for (int i = 0; i < inv.Length; i++)
			{
				if (!list.Contains(i))
				{
					switch (inv[i].type)
					{
					case 71:
						num += (long)inv[i].stack;
						break;
					case 72:
						num += (long)inv[i].stack * 100L;
						break;
					case 73:
						num += (long)inv[i].stack * 10000L;
						break;
					case 74:
						num += (long)inv[i].stack * 1000000L;
						break;
					}
				}
			}
			overFlowing = false;
			return num;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x003EE618 File Offset: 0x003EC818
		public static int[] CoinsSplit(long count)
		{
			int[] array = new int[4];
			long num = 0L;
			long num2 = 1000000L;
			for (int i = 3; i >= 0; i--)
			{
				array[i] = (int)((count - num) / num2);
				num += (long)array[i] * num2;
				num2 /= 100L;
			}
			return array;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x003EE65C File Offset: 0x003EC85C
		public static long CoinsCombineStacks(out bool overFlowing, params long[] coinCounts)
		{
			long num = 0L;
			foreach (long num2 in coinCounts)
			{
				num += num2;
				if (num >= 999999999L)
				{
					overFlowing = true;
					return 999999999L;
				}
			}
			overFlowing = false;
			return num;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x003EE69C File Offset: 0x003EC89C
		public static void PoofOfSmoke(Vector2 position)
		{
			int num = Main.rand.Next(3, 7);
			for (int i = 0; i < num; i++)
			{
				int num2 = Gore.NewGore(position, (Main.rand.NextFloat() * 6.2831855f).ToRotationVector2() * new Vector2(2f, 0.7f) * 0.7f, Main.rand.Next(11, 14), 1f);
				Main.gore[num2].scale = 0.7f;
				Main.gore[num2].velocity *= 0.5f;
			}
			for (int j = 0; j < 10; j++)
			{
				Dust dust = Main.dust[Dust.NewDust(position, 14, 14, 16, 0f, 0f, 100, default(Color), 1.5f)];
				dust.position += new Vector2(5f);
				dust.velocity = (Main.rand.NextFloat() * 6.2831855f).ToRotationVector2() * new Vector2(2f, 0.7f) * 0.7f * (0.5f + 0.5f * Main.rand.NextFloat());
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x003EE7ED File Offset: 0x003EC9ED
		public static Vector2 ToScreenPosition(this Vector2 worldPosition)
		{
			return Vector2.Transform(worldPosition - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix) / Main.UIScale;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x003EE814 File Offset: 0x003ECA14
		public static string PrettifyPercentDisplay(float percent, string originalFormat)
		{
			return percent.ToString(originalFormat, CultureInfo.InvariantCulture).TrimEnd(new char[]
			{
				'0',
				'%',
				' '
			}).TrimEnd(new char[]
			{
				'.',
				' '
			}).TrimStart(new char[]
			{
				'0',
				' '
			}) + "%";
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x003EE878 File Offset: 0x003ECA78
		public static void TrimTextIfNeeded(ref string text, DynamicSpriteFont font, float scale, float maxWidth)
		{
			int num = 0;
			Vector2 vector = font.MeasureString(text) * scale;
			while (vector.X > maxWidth)
			{
				text = text.Substring(0, text.Length - 1);
				num++;
				vector = font.MeasureString(text) * scale;
			}
			if (num > 0)
			{
				text = text.Substring(0, text.Length - 1) + "…";
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x003EE8E8 File Offset: 0x003ECAE8
		public static string FormatWith(string original, object obj)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			return Utils._substitutionRegex.Replace(original, delegate(Match match)
			{
				if (match.Groups[1].Length != 0)
				{
					return "";
				}
				string name = match.Groups[2].ToString();
				PropertyDescriptor propertyDescriptor = properties.Find(name, false);
				if (propertyDescriptor == null)
				{
					return "";
				}
				return (propertyDescriptor.GetValue(obj) ?? "").ToString();
			});
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x003EE930 File Offset: 0x003ECB30
		public static bool TryCreatingDirectory(string folderPath)
		{
			if (Directory.Exists(folderPath))
			{
				return true;
			}
			bool result;
			try
			{
				Directory.CreateDirectory(folderPath);
				result = true;
			}
			catch (Exception exception)
			{
				FancyErrorPrinter.ShowDirectoryCreationFailError(exception, folderPath);
				result = false;
			}
			return result;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x003EE970 File Offset: 0x003ECB70
		public static void OpenFolder(string folderPath)
		{
			if (!Utils.TryCreatingDirectory(folderPath))
			{
				return;
			}
			if (Platform.IsLinux)
			{
				Process.Start(new ProcessStartInfo(folderPath)
				{
					FileName = "open-folder",
					Arguments = folderPath,
					UseShellExecute = true,
					CreateNoWindow = true
				});
				return;
			}
			Process.Start(folderPath);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x003EE9C4 File Offset: 0x003ECBC4
		public static byte[] ToByteArray(this string str)
		{
			byte[] array = new byte[str.Length * 2];
			Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x003EE9F1 File Offset: 0x003ECBF1
		public static float NextFloat(this UnifiedRandom r)
		{
			return (float)r.NextDouble();
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x003EE9FA File Offset: 0x003ECBFA
		public static float NextFloatDirection(this UnifiedRandom r)
		{
			return (float)r.NextDouble() * 2f - 1f;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x003EEA0F File Offset: 0x003ECC0F
		public static float NextFloat(this UnifiedRandom random, FloatRange range)
		{
			return random.NextFloat() * (range.Maximum - range.Minimum) + range.Minimum;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x003EEA2C File Offset: 0x003ECC2C
		public static T NextFromList<T>(this UnifiedRandom random, params T[] objs)
		{
			return objs[random.Next(objs.Length)];
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x003EEA3D File Offset: 0x003ECC3D
		public static T NextFromCollection<T>(this UnifiedRandom random, List<T> objs)
		{
			return objs[random.Next(objs.Count)];
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x003EEA51 File Offset: 0x003ECC51
		public static int Next(this UnifiedRandom random, IntRange range)
		{
			return random.Next(range.Minimum, range.Maximum + 1);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x003EEA67 File Offset: 0x003ECC67
		public static Vector2 NextVector2Square(this UnifiedRandom r, float min, float max)
		{
			return new Vector2((max - min) * (float)r.NextDouble() + min, (max - min) * (float)r.NextDouble() + min);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x003EEA88 File Offset: 0x003ECC88
		public static Vector2 NextVector2FromRectangle(this UnifiedRandom r, Rectangle rect)
		{
			return new Vector2((float)rect.X + r.NextFloat() * (float)rect.Width, (float)rect.Y + r.NextFloat() * (float)rect.Height);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x003EEABB File Offset: 0x003ECCBB
		public static Vector2 NextVector2Unit(this UnifiedRandom r, float startRotation = 0f, float rotationRange = 6.2831855f)
		{
			return (startRotation + rotationRange * r.NextFloat()).ToRotationVector2();
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x003EEACC File Offset: 0x003ECCCC
		public static Vector2 NextVector2Circular(this UnifiedRandom r, float circleHalfWidth, float circleHalfHeight)
		{
			return r.NextVector2Unit(0f, 6.2831855f) * new Vector2(circleHalfWidth, circleHalfHeight) * r.NextFloat();
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x003EEAF5 File Offset: 0x003ECCF5
		public static Vector2 NextVector2CircularEdge(this UnifiedRandom r, float circleHalfWidth, float circleHalfHeight)
		{
			return r.NextVector2Unit(0f, 6.2831855f) * new Vector2(circleHalfWidth, circleHalfHeight);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x003EEB13 File Offset: 0x003ECD13
		public static int Width(this Asset<Texture2D> asset)
		{
			if (!asset.IsLoaded)
			{
				return 0;
			}
			return asset.Value.Width;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x003EEB2A File Offset: 0x003ECD2A
		public static int Height(this Asset<Texture2D> asset)
		{
			if (!asset.IsLoaded)
			{
				return 0;
			}
			return asset.Value.Height;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x003EEB41 File Offset: 0x003ECD41
		public static Rectangle Frame(this Asset<Texture2D> tex, int horizontalFrames = 1, int verticalFrames = 1, int frameX = 0, int frameY = 0, int sizeOffsetX = 0, int sizeOffsetY = 0)
		{
			if (!tex.IsLoaded)
			{
				return Rectangle.Empty;
			}
			return tex.Value.Frame(horizontalFrames, verticalFrames, frameX, frameY, sizeOffsetX, sizeOffsetY);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x003EEB65 File Offset: 0x003ECD65
		public static Rectangle OffsetSize(this Rectangle rect, int xSize, int ySize)
		{
			rect.Width += xSize;
			rect.Height += ySize;
			return rect;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x003EEB80 File Offset: 0x003ECD80
		public static Vector2 Size(this Asset<Texture2D> tex)
		{
			if (!tex.IsLoaded)
			{
				return Vector2.Zero;
			}
			return tex.Value.Size();
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x003EEB9C File Offset: 0x003ECD9C
		public static Rectangle Frame(this Texture2D tex, int horizontalFrames = 1, int verticalFrames = 1, int frameX = 0, int frameY = 0, int sizeOffsetX = 0, int sizeOffsetY = 0)
		{
			int num = tex.Width / horizontalFrames;
			int num2 = tex.Height / verticalFrames;
			return new Rectangle(num * frameX, num2 * frameY, num + sizeOffsetX, num2 + sizeOffsetY);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x003EEBD0 File Offset: 0x003ECDD0
		public static Vector2 OriginFlip(this Rectangle rect, Vector2 origin, SpriteEffects effects)
		{
			if (effects.HasFlag(SpriteEffects.FlipHorizontally))
			{
				origin.X = (float)rect.Width - origin.X;
			}
			if (effects.HasFlag(SpriteEffects.FlipVertically))
			{
				origin.Y = (float)rect.Height - origin.Y;
			}
			return origin;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x003EEC2E File Offset: 0x003ECE2E
		public static Vector2 Size(this Texture2D tex)
		{
			return new Vector2((float)tex.Width, (float)tex.Height);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x003EEC43 File Offset: 0x003ECE43
		public static void WriteRGB(this BinaryWriter bb, Color c)
		{
			bb.Write(c.R);
			bb.Write(c.G);
			bb.Write(c.B);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x003EEC6C File Offset: 0x003ECE6C
		public static void WriteVector2(this BinaryWriter bb, Vector2 v)
		{
			bb.Write(v.X);
			bb.Write(v.Y);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x003EEC88 File Offset: 0x003ECE88
		public static void WritePackedVector2(this BinaryWriter bb, Vector2 v)
		{
			HalfVector2 halfVector = new HalfVector2(v.X, v.Y);
			bb.Write(halfVector.PackedValue);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x003EECB5 File Offset: 0x003ECEB5
		public static Color ReadRGB(this BinaryReader bb)
		{
			return new Color((int)bb.ReadByte(), (int)bb.ReadByte(), (int)bb.ReadByte());
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x003EECCE File Offset: 0x003ECECE
		public static Vector2 ReadVector2(this BinaryReader bb)
		{
			return new Vector2(bb.ReadSingle(), bb.ReadSingle());
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x003EECE4 File Offset: 0x003ECEE4
		public static Vector2 ReadPackedVector2(this BinaryReader bb)
		{
			HalfVector2 halfVector = default(HalfVector2);
			halfVector.PackedValue = bb.ReadUInt32();
			return halfVector.ToVector2();
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x003EED0D File Offset: 0x003ECF0D
		public static Vector2 Left(this Rectangle r)
		{
			return new Vector2((float)r.X, (float)(r.Y + r.Height / 2));
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x003EED2B File Offset: 0x003ECF2B
		public static Vector2 Right(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width), (float)(r.Y + r.Height / 2));
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x003EED50 File Offset: 0x003ECF50
		public static Vector2 Top(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width / 2), (float)r.Y);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x003EED6E File Offset: 0x003ECF6E
		public static Vector2 Bottom(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width / 2), (float)(r.Y + r.Height));
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x003EED93 File Offset: 0x003ECF93
		public static Vector2 TopLeft(this Rectangle r)
		{
			return new Vector2((float)r.X, (float)r.Y);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x003EEDA8 File Offset: 0x003ECFA8
		public static Vector2 TopRight(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width), (float)r.Y);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x003EEDC4 File Offset: 0x003ECFC4
		public static Vector2 BottomLeft(this Rectangle r)
		{
			return new Vector2((float)r.X, (float)(r.Y + r.Height));
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x003EEDE0 File Offset: 0x003ECFE0
		public static Vector2 BottomRight(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width), (float)(r.Y + r.Height));
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x003EEE03 File Offset: 0x003ED003
		public static Vector2 Center(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width / 2), (float)(r.Y + r.Height / 2));
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x003EEE2A File Offset: 0x003ED02A
		public static Vector2 Size(this Rectangle r)
		{
			return new Vector2((float)r.Width, (float)r.Height);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x003EEE40 File Offset: 0x003ED040
		public static float Distance(this Rectangle r, Vector2 point)
		{
			if (Utils.FloatIntersect((float)r.Left, (float)r.Top, (float)r.Width, (float)r.Height, point.X, point.Y, 0f, 0f))
			{
				return 0f;
			}
			if (point.X >= (float)r.Left && point.X <= (float)r.Right)
			{
				if (point.Y < (float)r.Top)
				{
					return (float)r.Top - point.Y;
				}
				return point.Y - (float)r.Bottom;
			}
			else if (point.Y >= (float)r.Top && point.Y <= (float)r.Bottom)
			{
				if (point.X < (float)r.Left)
				{
					return (float)r.Left - point.X;
				}
				return point.X - (float)r.Right;
			}
			else if (point.X < (float)r.Left)
			{
				if (point.Y < (float)r.Top)
				{
					return Vector2.Distance(point, r.TopLeft());
				}
				return Vector2.Distance(point, r.BottomLeft());
			}
			else
			{
				if (point.Y < (float)r.Top)
				{
					return Vector2.Distance(point, r.TopRight());
				}
				return Vector2.Distance(point, r.BottomRight());
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x003EEF94 File Offset: 0x003ED194
		public static Vector2 ClosestPointInRect(this Rectangle r, Vector2 point)
		{
			Vector2 vector = point;
			if (vector.X < (float)r.Left)
			{
				vector.X = (float)r.Left;
			}
			if (vector.X > (float)r.Right)
			{
				vector.X = (float)r.Right;
			}
			if (vector.Y < (float)r.Top)
			{
				vector.Y = (float)r.Top;
			}
			if (vector.Y > (float)r.Bottom)
			{
				vector.Y = (float)r.Bottom;
			}
			return vector;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x003EF020 File Offset: 0x003ED220
		public static Rectangle Modified(this Rectangle r, int x, int y, int w, int h)
		{
			return new Rectangle(r.X + x, r.Y + y, r.Width + w, r.Height + h);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x003EF048 File Offset: 0x003ED248
		public static bool IntersectsConeFastInaccurate(this Rectangle targetRect, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
		{
			Vector2 point = coneCenter + coneRotation.ToRotationVector2() * coneLength;
			Vector2 spinningpoint = targetRect.ClosestPointInRect(point) - coneCenter;
			float num = spinningpoint.RotatedBy((double)(-(double)coneRotation), default(Vector2)).ToRotation();
			return num >= -maximumAngle && num <= maximumAngle && spinningpoint.Length() < coneLength;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x003EF0A8 File Offset: 0x003ED2A8
		public static bool IntersectsConeSlowMoreAccurate(this Rectangle targetRect, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
		{
			Vector2 point = coneCenter + coneRotation.ToRotationVector2() * coneLength;
			return Utils.DoesFitInCone(targetRect.ClosestPointInRect(point), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.TopLeft(), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.TopRight(), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.BottomLeft(), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.BottomRight(), coneCenter, coneLength, coneRotation, maximumAngle);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x003EF130 File Offset: 0x003ED330
		public static bool DoesFitInCone(Vector2 point, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
		{
			Vector2 spinningpoint = point - coneCenter;
			float num = spinningpoint.RotatedBy((double)(-(double)coneRotation), default(Vector2)).ToRotation();
			return num >= -maximumAngle && num <= maximumAngle && spinningpoint.Length() < coneLength;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x003EF174 File Offset: 0x003ED374
		public static float ToRotation(this Vector2 v)
		{
			return (float)Math.Atan2((double)v.Y, (double)v.X);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x003EF18A File Offset: 0x003ED38A
		public static Vector2 ToRotationVector2(this float f)
		{
			return new Vector2((float)Math.Cos((double)f), (float)Math.Sin((double)f));
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x003EF1A4 File Offset: 0x003ED3A4
		public static Vector2 RotatedBy(this Vector2 spinningpoint, double radians, Vector2 center = default(Vector2))
		{
			float num = (float)Math.Cos(radians);
			float num2 = (float)Math.Sin(radians);
			Vector2 vector = spinningpoint - center;
			Vector2 result = center;
			result.X += vector.X * num - vector.Y * num2;
			result.Y += vector.X * num2 + vector.Y * num;
			return result;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x003EF204 File Offset: 0x003ED404
		public static Vector2D RotatedBy(this Vector2D spinningpoint, double radians, Vector2D center = default(Vector2D))
		{
			double num = Math.Cos(radians);
			double num2 = Math.Sin(radians);
			Vector2D vector2D = spinningpoint - center;
			Vector2D result = center;
			result.X += vector2D.X * num - vector2D.Y * num2;
			result.Y += vector2D.X * num2 + vector2D.Y * num;
			return result;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x003EF264 File Offset: 0x003ED464
		public static Vector2 RotatedByRandom(this Vector2 spinninpoint, double maxRadians)
		{
			return spinninpoint.RotatedBy(Main.rand.NextDouble() * maxRadians - Main.rand.NextDouble() * maxRadians, default(Vector2));
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x003EF299 File Offset: 0x003ED499
		public static Vector2 Floor(this Vector2 vec)
		{
			vec.X = (float)((int)vec.X);
			vec.Y = (float)((int)vec.Y);
			return vec;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x003EF2BA File Offset: 0x003ED4BA
		public static bool HasNaNs(this Vector2 vec)
		{
			return float.IsNaN(vec.X) || float.IsNaN(vec.Y);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x003EF2D6 File Offset: 0x003ED4D6
		public static bool Between(this Vector2 vec, Vector2 minimum, Vector2 maximum)
		{
			return vec.X >= minimum.X && vec.X <= maximum.X && vec.Y >= minimum.Y && vec.Y <= maximum.Y;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x003EF315 File Offset: 0x003ED515
		public static Vector2 ToVector2(this Point p)
		{
			return new Vector2((float)p.X, (float)p.Y);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x003EF32A File Offset: 0x003ED52A
		public static Vector2 ToVector2(this Point16 p)
		{
			return new Vector2((float)p.X, (float)p.Y);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x003EF33F File Offset: 0x003ED53F
		public static Vector2D ToVector2D(this Point p)
		{
			return new Vector2D((double)p.X, (double)p.Y);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x003EF354 File Offset: 0x003ED554
		public static Vector2D ToVector2D(this Point16 p)
		{
			return new Vector2D((double)p.X, (double)p.Y);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x003EF369 File Offset: 0x003ED569
		public static Vector2 ToWorldCoordinates(this Point p, float autoAddX = 8f, float autoAddY = 8f)
		{
			return p.ToVector2() * 16f + new Vector2(autoAddX, autoAddY);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x003EF387 File Offset: 0x003ED587
		public static Vector2 ToWorldCoordinates(this Point16 p, float autoAddX = 8f, float autoAddY = 8f)
		{
			return p.ToVector2() * 16f + new Vector2(autoAddX, autoAddY);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x003EF3A8 File Offset: 0x003ED5A8
		public static Vector2 MoveTowards(this Vector2 currentPosition, Vector2 targetPosition, float maxAmountAllowedToMove)
		{
			Vector2 v = targetPosition - currentPosition;
			if (v.Length() < maxAmountAllowedToMove)
			{
				return targetPosition;
			}
			return currentPosition + v.SafeNormalize(Vector2.Zero) * maxAmountAllowedToMove;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x003EF3E0 File Offset: 0x003ED5E0
		public static Point16 ToTileCoordinates16(this Vector2 vec)
		{
			return new Point16((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x003EF3F9 File Offset: 0x003ED5F9
		public static Point16 ToTileCoordinates16(this Vector2D vec)
		{
			return new Point16((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x003EF412 File Offset: 0x003ED612
		public static Point ToTileCoordinates(this Vector2 vec)
		{
			return new Point((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x003EF42B File Offset: 0x003ED62B
		public static Point ToTileCoordinates(this Vector2D vec)
		{
			return new Point((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x003EF444 File Offset: 0x003ED644
		public static Point ToPoint(this Vector2 v)
		{
			return new Point((int)v.X, (int)v.Y);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x003EF459 File Offset: 0x003ED659
		public static Point ToPoint(this Vector2D v)
		{
			return new Point((int)v.X, (int)v.Y);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x003EF46E File Offset: 0x003ED66E
		public static Vector2D ToVector2D(this Vector2 v)
		{
			return new Vector2D((double)v.X, (double)v.Y);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x003EF483 File Offset: 0x003ED683
		public static Vector2 SafeNormalize(this Vector2 v, Vector2 defaultValue)
		{
			if (v == Vector2.Zero || v.HasNaNs())
			{
				return defaultValue;
			}
			return Vector2.Normalize(v);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x003EF4A4 File Offset: 0x003ED6A4
		public static Vector2 ClosestPointOnLine(this Vector2 P, Vector2 A, Vector2 B)
		{
			Vector2 value = P - A;
			Vector2 vector = B - A;
			float num = vector.LengthSquared();
			float num2 = Vector2.Dot(value, vector) / num;
			if (num2 < 0f)
			{
				return A;
			}
			if (num2 > 1f)
			{
				return B;
			}
			return A + vector * num2;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x003EF4F4 File Offset: 0x003ED6F4
		public static bool RectangleLineCollision(Vector2 rectTopLeft, Vector2 rectBottomRight, Vector2 lineStart, Vector2 lineEnd)
		{
			if (lineStart.Between(rectTopLeft, rectBottomRight) || lineEnd.Between(rectTopLeft, rectBottomRight))
			{
				return true;
			}
			Vector2 p = new Vector2(rectBottomRight.X, rectTopLeft.Y);
			Vector2 vector = new Vector2(rectTopLeft.X, rectBottomRight.Y);
			Vector2[] array = new Vector2[]
			{
				rectTopLeft.ClosestPointOnLine(lineStart, lineEnd),
				p.ClosestPointOnLine(lineStart, lineEnd),
				vector.ClosestPointOnLine(lineStart, lineEnd),
				rectBottomRight.ClosestPointOnLine(lineStart, lineEnd)
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[0].Between(rectTopLeft, vector))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x003EF5A4 File Offset: 0x003ED7A4
		public static Vector2 RotateRandom(this Vector2 spinninpoint, double maxRadians)
		{
			return spinninpoint.RotatedBy(Main.rand.NextDouble() * maxRadians - Main.rand.NextDouble() * maxRadians, default(Vector2));
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x003EF5D9 File Offset: 0x003ED7D9
		public static float AngleTo(this Vector2 Origin, Vector2 Target)
		{
			return (float)Math.Atan2((double)(Target.Y - Origin.Y), (double)(Target.X - Origin.X));
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x003EF5FD File Offset: 0x003ED7FD
		public static float AngleFrom(this Vector2 Origin, Vector2 Target)
		{
			return (float)Math.Atan2((double)(Origin.Y - Target.Y), (double)(Origin.X - Target.X));
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x003EF624 File Offset: 0x003ED824
		public static Vector2 rotateTowards(Vector2 currentPosition, Vector2 currentVelocity, Vector2 targetPosition, float maxChange)
		{
			float scaleFactor = currentVelocity.Length();
			float targetAngle = currentPosition.AngleTo(targetPosition);
			return currentVelocity.ToRotation().AngleTowards(targetAngle, 0.017453292f).ToRotationVector2() * scaleFactor;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x003EF65D File Offset: 0x003ED85D
		public static float Distance(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Distance(Origin, Target);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x003EF666 File Offset: 0x003ED866
		public static float DistanceSQ(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.DistanceSquared(Origin, Target);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x003EF66F File Offset: 0x003ED86F
		public static Vector2 DirectionTo(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Normalize(Target - Origin);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x003EF67D File Offset: 0x003ED87D
		public static Vector2 DirectionFrom(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Normalize(Origin - Target);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x003EF68B File Offset: 0x003ED88B
		public static bool WithinRange(this Vector2 Origin, Vector2 Target, float MaxRange)
		{
			return Vector2.DistanceSquared(Origin, Target) <= MaxRange * MaxRange;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x003EF69C File Offset: 0x003ED89C
		public static Vector2 XY(this Vector4 vec)
		{
			return new Vector2(vec.X, vec.Y);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x003EF6AF File Offset: 0x003ED8AF
		public static Vector2 ZW(this Vector4 vec)
		{
			return new Vector2(vec.Z, vec.W);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x003EF6C2 File Offset: 0x003ED8C2
		public static Vector3 XZW(this Vector4 vec)
		{
			return new Vector3(vec.X, vec.Z, vec.W);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x003EF6DB File Offset: 0x003ED8DB
		public static Vector3 YZW(this Vector4 vec)
		{
			return new Vector3(vec.Y, vec.Z, vec.W);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x003EF6F4 File Offset: 0x003ED8F4
		public static Color MultiplyRGB(this Color firstColor, Color secondColor)
		{
			return new Color((int)((byte)((float)(firstColor.R * secondColor.R) / 255f)), (int)((byte)((float)(firstColor.G * secondColor.G) / 255f)), (int)((byte)((float)(firstColor.B * secondColor.B) / 255f)));
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x003EF74C File Offset: 0x003ED94C
		public static Color MultiplyRGBA(this Color firstColor, Color secondColor)
		{
			return new Color((int)((byte)((float)(firstColor.R * secondColor.R) / 255f)), (int)((byte)((float)(firstColor.G * secondColor.G) / 255f)), (int)((byte)((float)(firstColor.B * secondColor.B) / 255f)), (int)((byte)((float)(firstColor.A * secondColor.A) / 255f)));
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x003EF7BC File Offset: 0x003ED9BC
		public static string Hex3(this Color color)
		{
			return (color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2")).ToLower();
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x003EF810 File Offset: 0x003EDA10
		public static string Hex4(this Color color)
		{
			return (color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2") + color.A.ToString("X2")).ToLower();
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x003EF877 File Offset: 0x003EDA77
		public static int ToDirectionInt(this bool value)
		{
			if (!value)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x003EF87F File Offset: 0x003EDA7F
		public static int ToInt(this bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x003EF887 File Offset: 0x003EDA87
		public static int ModulusPositive(this int myInteger, int modulusNumber)
		{
			return (myInteger % modulusNumber + modulusNumber) % modulusNumber;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x003EF890 File Offset: 0x003EDA90
		public static float AngleLerp(this float curAngle, float targetAngle, float amount)
		{
			float angle;
			if (targetAngle < curAngle)
			{
				float num = targetAngle + 6.2831855f;
				angle = ((num - curAngle > curAngle - targetAngle) ? MathHelper.Lerp(curAngle, targetAngle, amount) : MathHelper.Lerp(curAngle, num, amount));
			}
			else
			{
				if (targetAngle <= curAngle)
				{
					return curAngle;
				}
				float num = targetAngle - 6.2831855f;
				angle = ((targetAngle - curAngle > curAngle - num) ? MathHelper.Lerp(curAngle, num, amount) : MathHelper.Lerp(curAngle, targetAngle, amount));
			}
			return MathHelper.WrapAngle(angle);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x003EF8F8 File Offset: 0x003EDAF8
		public static float AngleTowards(this float curAngle, float targetAngle, float maxChange)
		{
			curAngle = MathHelper.WrapAngle(curAngle);
			targetAngle = MathHelper.WrapAngle(targetAngle);
			if (curAngle < targetAngle)
			{
				if (targetAngle - curAngle > 3.1415927f)
				{
					curAngle += 6.2831855f;
				}
			}
			else if (curAngle - targetAngle > 3.1415927f)
			{
				curAngle -= 6.2831855f;
			}
			curAngle += MathHelper.Clamp(targetAngle - curAngle, -maxChange, maxChange);
			return MathHelper.WrapAngle(curAngle);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x003EF958 File Offset: 0x003EDB58
		public static bool deepCompare(this int[] firstArray, int[] secondArray)
		{
			if (firstArray == null && secondArray == null)
			{
				return true;
			}
			if (firstArray == null || secondArray == null)
			{
				return false;
			}
			if (firstArray.Length != secondArray.Length)
			{
				return false;
			}
			for (int i = 0; i < firstArray.Length; i++)
			{
				if (firstArray[i] != secondArray[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x003EF998 File Offset: 0x003EDB98
		public static List<int> GetTrueIndexes(this bool[] array)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					list.Add(i);
				}
			}
			return list;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x003EF9C8 File Offset: 0x003EDBC8
		public static List<int> GetTrueIndexes(params bool[][] arrays)
		{
			List<int> list = new List<int>();
			foreach (bool[] array in arrays)
			{
				list.AddRange(array.GetTrueIndexes());
			}
			return list.Distinct<int>().ToList<int>();
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x003EFA08 File Offset: 0x003EDC08
		public static int Count<T>(this T[] arr, T value)
		{
			int num = 0;
			foreach (T x in arr)
			{
				if (EqualityComparer<T>.Default.Equals(x, value))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x003EFA42 File Offset: 0x003EDC42
		public static bool PressingShift(this KeyboardState kb)
		{
			return kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x003EFA60 File Offset: 0x003EDC60
		public static bool PressingControl(this KeyboardState kb)
		{
			return kb.IsKeyDown(Keys.LeftControl) || kb.IsKeyDown(Keys.RightControl);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x003EFA80 File Offset: 0x003EDC80
		public static R[] MapArray<T, R>(T[] array, Func<T, R> mapper)
		{
			R[] array2 = new R[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = mapper(array[i]);
			}
			return array2;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x003EFAB9 File Offset: 0x003EDCB9
		public static bool PlotLine(Point16 p0, Point16 p1, Utils.TileActionAttempt plot, bool jump = true)
		{
			return Utils.PlotLine((int)p0.X, (int)p0.Y, (int)p1.X, (int)p1.Y, plot, jump);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x003EFADA File Offset: 0x003EDCDA
		public static bool PlotLine(Point p0, Point p1, Utils.TileActionAttempt plot, bool jump = true)
		{
			return Utils.PlotLine(p0.X, p0.Y, p1.X, p1.Y, plot, jump);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x003EFAFC File Offset: 0x003EDCFC
		private static bool PlotLine(int x0, int y0, int x1, int y1, Utils.TileActionAttempt plot, bool jump = true)
		{
			if (x0 == x1 && y0 == y1)
			{
				return plot(x0, y0);
			}
			bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if (flag)
			{
				Utils.Swap<int>(ref x0, ref y0);
				Utils.Swap<int>(ref x1, ref y1);
			}
			int num = Math.Abs(x1 - x0);
			int num2 = Math.Abs(y1 - y0);
			int num3 = num / 2;
			int num4 = y0;
			int num5 = (x0 < x1) ? 1 : -1;
			int num6 = (y0 < y1) ? 1 : -1;
			for (int num7 = x0; num7 != x1; num7 += num5)
			{
				if (flag)
				{
					if (!plot(num4, num7))
					{
						return false;
					}
				}
				else if (!plot(num7, num4))
				{
					return false;
				}
				num3 -= num2;
				if (num3 < 0)
				{
					num4 += num6;
					if (!jump)
					{
						if (flag)
						{
							if (!plot(num4, num7))
							{
								return false;
							}
						}
						else if (!plot(num7, num4))
						{
							return false;
						}
					}
					num3 += num;
				}
			}
			return true;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x003EFBDB File Offset: 0x003EDDDB
		public static int RandomNext(ref ulong seed, int bits)
		{
			seed = Utils.RandomNextSeed(seed);
			return (int)(seed >> 48 - bits);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x003EFBF1 File Offset: 0x003EDDF1
		public static ulong RandomNextSeed(ulong seed)
		{
			return seed * 25214903917UL + 11UL & 281474976710655UL;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x003EFC0C File Offset: 0x003EDE0C
		public static float RandomFloat(ref ulong seed)
		{
			return (float)Utils.RandomNext(ref seed, 24) / 16777216f;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x003EFC20 File Offset: 0x003EDE20
		public static int RandomInt(ref ulong seed, int max)
		{
			if ((max & -max) == max)
			{
				return (int)((long)max * (long)Utils.RandomNext(ref seed, 31) >> 31);
			}
			int num;
			int num2;
			do
			{
				num = Utils.RandomNext(ref seed, 31);
				num2 = num % max;
			}
			while (num - num2 + (max - 1) < 0);
			return num2;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x003EFC5D File Offset: 0x003EDE5D
		public static int RandomInt(ref ulong seed, int min, int max)
		{
			return Utils.RandomInt(ref seed, max - min) + min;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x003EFC6A File Offset: 0x003EDE6A
		public static bool PlotTileLine(Vector2 start, Vector2 end, float width, Utils.TileActionAttempt plot)
		{
			return Utils.PlotTileLine(start.ToVector2D(), end.ToVector2D(), (double)width, plot);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x003EFC80 File Offset: 0x003EDE80
		public static bool PlotTileLine(Vector2D start, Vector2D end, double width, Utils.TileActionAttempt plot)
		{
			double num = width / 2.0;
			Vector2D vector2D = end - start;
			Vector2D vector2D2 = vector2D / vector2D.Length();
			Vector2D vector2D3 = new Vector2D(-vector2D2.Y, vector2D2.X) * num;
			Point point = (start - vector2D3).ToTileCoordinates();
			Point point2 = (start + vector2D3).ToTileCoordinates();
			Point point3 = start.ToTileCoordinates();
			Point point4 = end.ToTileCoordinates();
			Point lineMinOffset = new Point(point.X - point3.X, point.Y - point3.Y);
			Point lineMaxOffset = new Point(point2.X - point3.X, point2.Y - point3.Y);
			return Utils.PlotLine(point3.X, point3.Y, point4.X, point4.Y, (int x, int y) => Utils.PlotLine(x + lineMinOffset.X, y + lineMinOffset.Y, x + lineMaxOffset.X, y + lineMaxOffset.Y, plot, false), true);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x003EFD84 File Offset: 0x003EDF84
		public static bool PlotTileTale(Vector2D start, Vector2D end, double width, Utils.TileActionAttempt plot)
		{
			double halfWidth = width / 2.0;
			Vector2D vector2D = end - start;
			Vector2D vector2D2 = vector2D / vector2D.Length();
			Vector2D perpOffset = new Vector2D(-vector2D2.Y, vector2D2.X);
			Point pointStart = start.ToTileCoordinates();
			Point point = end.ToTileCoordinates();
			int length = 0;
			Utils.PlotLine(pointStart.X, pointStart.Y, point.X, point.Y, delegate(int <p0>, int <p1>)
			{
				int length2 = length;
				length = length2 + 1;
				return true;
			}, true);
			int length3 = length;
			length = length3 - 1;
			int curLength = 0;
			return Utils.PlotLine(pointStart.X, pointStart.Y, point.X, point.Y, delegate(int x, int y)
			{
				int curLength;
				double num = 1.0 - (double)curLength / (double)length;
				curLength = curLength;
				curLength++;
				Point point2 = (start - perpOffset * halfWidth * num).ToTileCoordinates();
				Point point3 = (start + perpOffset * halfWidth * num).ToTileCoordinates();
				Point point4 = new Point(point2.X - pointStart.X, point2.Y - pointStart.Y);
				Point point5 = new Point(point3.X - pointStart.X, point3.Y - pointStart.Y);
				return Utils.PlotLine(x + point4.X, y + point4.Y, x + point5.X, y + point5.Y, plot, false);
			}, true);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x003EFE8C File Offset: 0x003EE08C
		public static bool PlotTileArea(int x, int y, Utils.TileActionAttempt plot)
		{
			if (!WorldGen.InWorld(x, y, 0))
			{
				return false;
			}
			List<Point> list = new List<Point>();
			List<Point> list2 = new List<Point>();
			HashSet<Point> hashSet = new HashSet<Point>();
			list2.Add(new Point(x, y));
			while (list2.Count > 0)
			{
				list.Clear();
				list.AddRange(list2);
				list2.Clear();
				while (list.Count > 0)
				{
					Point point = list[0];
					if (!WorldGen.InWorld(point.X, point.Y, 1))
					{
						list.Remove(point);
					}
					else
					{
						hashSet.Add(point);
						list.Remove(point);
						if (plot(point.X, point.Y))
						{
							Point item = new Point(point.X - 1, point.Y);
							if (!hashSet.Contains(item))
							{
								list2.Add(item);
							}
							item = new Point(point.X + 1, point.Y);
							if (!hashSet.Contains(item))
							{
								list2.Add(item);
							}
							item = new Point(point.X, point.Y - 1);
							if (!hashSet.Contains(item))
							{
								list2.Add(item);
							}
							item = new Point(point.X, point.Y + 1);
							if (!hashSet.Contains(item))
							{
								list2.Add(item);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x003EFFE6 File Offset: 0x003EE1E6
		public static int RandomConsecutive(double random, int odds)
		{
			return (int)Math.Log(1.0 - random, 1.0 / (double)odds);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x003EEA67 File Offset: 0x003ECC67
		public static Vector2 RandomVector2(UnifiedRandom random, float min, float max)
		{
			return new Vector2((max - min) * (float)random.NextDouble() + min, (max - min) * (float)random.NextDouble() + min);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x003F0005 File Offset: 0x003EE205
		public static Vector2D RandomVector2D(UnifiedRandom random, double min, double max)
		{
			return new Vector2D((max - min) * random.NextDouble() + min, (max - min) * random.NextDouble() + min);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x003F0024 File Offset: 0x003EE224
		public static bool IndexInRange<T>(this T[] t, int index)
		{
			return index >= 0 && index < t.Length;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x003F0032 File Offset: 0x003EE232
		public static bool IndexInRange<T>(this List<T> t, int index)
		{
			return index >= 0 && index < t.Count;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x003F0043 File Offset: 0x003EE243
		public static T SelectRandom<T>(UnifiedRandom random, params T[] choices)
		{
			return choices[random.Next(choices.Length)];
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x003F0054 File Offset: 0x003EE254
		public static void DrawBorderStringFourWay(SpriteBatch sb, DynamicSpriteFont font, string text, float x, float y, Color textColor, Color borderColor, Vector2 origin, float scale = 1f)
		{
			Color color = borderColor;
			Vector2 zero = Vector2.Zero;
			int i = 0;
			while (i < 5)
			{
				switch (i)
				{
				case 0:
					zero.X = x - 2f;
					zero.Y = y;
					break;
				case 1:
					zero.X = x + 2f;
					zero.Y = y;
					break;
				case 2:
					zero.X = x;
					zero.Y = y - 2f;
					break;
				case 3:
					zero.X = x;
					zero.Y = y + 2f;
					break;
				case 4:
					goto IL_90;
				default:
					goto IL_90;
				}
				IL_A4:
				DynamicSpriteFontExtensionMethods.DrawString(sb, font, text, zero, color, 0f, origin, scale, SpriteEffects.None, 0f);
				i++;
				continue;
				IL_90:
				zero.X = x;
				zero.Y = y;
				color = textColor;
				goto IL_A4;
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x003F012C File Offset: 0x003EE32C
		public static Vector2 DrawBorderString(SpriteBatch sb, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int maxCharactersDisplayed = -1)
		{
			if (maxCharactersDisplayed != -1 && text.Length > maxCharactersDisplayed)
			{
				text.Substring(0, maxCharactersDisplayed);
			}
			DynamicSpriteFont value = FontAssets.MouseText.Value;
			Vector2 vector = value.MeasureString(text);
			ChatManager.DrawColorCodedStringWithShadow(sb, value, text, pos, color, 0f, new Vector2(anchorx, anchory) * vector, new Vector2(scale), -1f, 1.5f);
			return vector * scale;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x003F01A0 File Offset: 0x003EE3A0
		public static Vector2 DrawBorderStringBig(SpriteBatch spriteBatch, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int maxCharactersDisplayed = -1)
		{
			if (maxCharactersDisplayed != -1 && text.Length > maxCharactersDisplayed)
			{
				text.Substring(0, maxCharactersDisplayed);
			}
			DynamicSpriteFont value = FontAssets.DeathText.Value;
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, value, text, pos + new Vector2((float)i, (float)j), Color.Black, 0f, new Vector2(anchorx, anchory) * value.MeasureString(text), scale, SpriteEffects.None, 0f);
				}
			}
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, value, text, pos, color, 0f, new Vector2(anchorx, anchory) * value.MeasureString(text), scale, SpriteEffects.None, 0f);
			return value.MeasureString(text) * scale;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x003F0261 File Offset: 0x003EE461
		public static void DrawInvBG(SpriteBatch sb, Rectangle R, Color c = default(Color))
		{
			Utils.DrawInvBG(sb, R.X, R.Y, R.Width, R.Height, c);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x003F0282 File Offset: 0x003EE482
		public static void DrawInvBG(SpriteBatch sb, float x, float y, float w, float h, Color c = default(Color))
		{
			Utils.DrawInvBG(sb, (int)x, (int)y, (int)w, (int)h, c);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x003F0298 File Offset: 0x003EE498
		public static void DrawInvBG(SpriteBatch sb, int x, int y, int w, int h, Color c = default(Color))
		{
			if (c == default(Color))
			{
				c = new Color(63, 65, 151, 255) * 0.785f;
			}
			Texture2D value = TextureAssets.InventoryBack13.Value;
			if (w < 20)
			{
				w = 20;
			}
			if (h < 20)
			{
				h = 20;
			}
			sb.Draw(value, new Rectangle(x, y, 10, 10), new Rectangle?(new Rectangle(0, 0, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + 10, y, w - 20, 10), new Rectangle?(new Rectangle(10, 0, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + w - 10, y, 10, 10), new Rectangle?(new Rectangle(value.Width - 10, 0, 10, 10)), c);
			sb.Draw(value, new Rectangle(x, y + 10, 10, h - 20), new Rectangle?(new Rectangle(0, 10, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + 10, y + 10, w - 20, h - 20), new Rectangle?(new Rectangle(10, 10, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + w - 10, y + 10, 10, h - 20), new Rectangle?(new Rectangle(value.Width - 10, 10, 10, 10)), c);
			sb.Draw(value, new Rectangle(x, y + h - 10, 10, 10), new Rectangle?(new Rectangle(0, value.Height - 10, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + 10, y + h - 10, w - 20, 10), new Rectangle?(new Rectangle(10, value.Height - 10, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + w - 10, y + h - 10, 10, 10), new Rectangle?(new Rectangle(value.Width - 10, value.Height - 10, 10, 10)), c);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x003F04B0 File Offset: 0x003EE6B0
		public static string ReadEmbeddedResource(string path)
		{
			string result;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x003F050C File Offset: 0x003EE70C
		public static void DrawSplicedPanel(SpriteBatch sb, Texture2D texture, int x, int y, int w, int h, int leftEnd, int rightEnd, int topEnd, int bottomEnd, Color c)
		{
			if (w < leftEnd + rightEnd)
			{
				w = leftEnd + rightEnd;
			}
			if (h < topEnd + bottomEnd)
			{
				h = topEnd + bottomEnd;
			}
			sb.Draw(texture, new Rectangle(x, y, leftEnd, topEnd), new Rectangle?(new Rectangle(0, 0, leftEnd, topEnd)), c);
			sb.Draw(texture, new Rectangle(x + leftEnd, y, w - leftEnd - rightEnd, topEnd), new Rectangle?(new Rectangle(leftEnd, 0, texture.Width - leftEnd - rightEnd, topEnd)), c);
			sb.Draw(texture, new Rectangle(x + w - rightEnd, y, topEnd, rightEnd), new Rectangle?(new Rectangle(texture.Width - rightEnd, 0, rightEnd, topEnd)), c);
			sb.Draw(texture, new Rectangle(x, y + topEnd, leftEnd, h - topEnd - bottomEnd), new Rectangle?(new Rectangle(0, topEnd, leftEnd, texture.Height - topEnd - bottomEnd)), c);
			sb.Draw(texture, new Rectangle(x + leftEnd, y + topEnd, w - leftEnd - rightEnd, h - topEnd - bottomEnd), new Rectangle?(new Rectangle(leftEnd, topEnd, texture.Width - leftEnd - rightEnd, texture.Height - topEnd - bottomEnd)), c);
			sb.Draw(texture, new Rectangle(x + w - rightEnd, y + topEnd, rightEnd, h - topEnd - bottomEnd), new Rectangle?(new Rectangle(texture.Width - rightEnd, topEnd, rightEnd, texture.Height - topEnd - bottomEnd)), c);
			sb.Draw(texture, new Rectangle(x, y + h - bottomEnd, leftEnd, bottomEnd), new Rectangle?(new Rectangle(0, texture.Height - bottomEnd, leftEnd, bottomEnd)), c);
			sb.Draw(texture, new Rectangle(x + leftEnd, y + h - bottomEnd, w - leftEnd - rightEnd, bottomEnd), new Rectangle?(new Rectangle(leftEnd, texture.Height - bottomEnd, texture.Width - leftEnd - rightEnd, bottomEnd)), c);
			sb.Draw(texture, new Rectangle(x + w - rightEnd, y + h - bottomEnd, rightEnd, bottomEnd), new Rectangle?(new Rectangle(texture.Width - rightEnd, texture.Height - bottomEnd, rightEnd, bottomEnd)), c);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x003F0749 File Offset: 0x003EE949
		public static void DrawSettingsPanel(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			Utils.DrawPanel(TextureAssets.SettingsPanel.Value, 2, 0, spriteBatch, position, width, color);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x003F0749 File Offset: 0x003EE949
		public static void DrawSettings2Panel(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			Utils.DrawPanel(TextureAssets.SettingsPanel.Value, 2, 0, spriteBatch, position, width, color);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x003F0760 File Offset: 0x003EE960
		public static void DrawPanel(Texture2D texture, int edgeWidth, int edgeShove, SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			spriteBatch.Draw(texture, position, new Rectangle?(new Rectangle(0, 0, edgeWidth, texture.Height)), color);
			spriteBatch.Draw(texture, new Vector2(position.X + (float)edgeWidth, position.Y), new Rectangle?(new Rectangle(edgeWidth + edgeShove, 0, texture.Width - (edgeWidth + edgeShove) * 2, texture.Height)), color, 0f, Vector2.Zero, new Vector2((width - (float)(edgeWidth * 2)) / (float)(texture.Width - (edgeWidth + edgeShove) * 2), 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, new Vector2(position.X + width - (float)edgeWidth, position.Y), new Rectangle?(new Rectangle(texture.Width - edgeWidth, 0, edgeWidth, texture.Height)), color);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x003F0838 File Offset: 0x003EEA38
		public static void DrawRectangle(SpriteBatch sb, Vector2 start, Vector2 end, Color colorStart, Color colorEnd, float width)
		{
			Utils.DrawLine(sb, start, new Vector2(start.X, end.Y), colorStart, colorEnd, width);
			Utils.DrawLine(sb, start, new Vector2(end.X, start.Y), colorStart, colorEnd, width);
			Utils.DrawLine(sb, end, new Vector2(start.X, end.Y), colorStart, colorEnd, width);
			Utils.DrawLine(sb, end, new Vector2(end.X, start.Y), colorStart, colorEnd, width);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x003F08BC File Offset: 0x003EEABC
		public static void DrawLaser(SpriteBatch sb, Texture2D tex, Vector2 start, Vector2 end, Vector2 scale, Utils.LaserLineFraming framing)
		{
			Vector2 vector = Vector2.Normalize(end - start);
			float num = (end - start).Length();
			float rotation = vector.ToRotation() - 1.5707964f;
			if (vector.HasNaNs())
			{
				return;
			}
			float num2;
			Rectangle rectangle;
			Vector2 vector2;
			Color color;
			framing(0, start, num, default(Rectangle), out num2, out rectangle, out vector2, out color);
			sb.Draw(tex, start, new Rectangle?(rectangle), color, rotation, rectangle.Size() / 2f, scale, SpriteEffects.None, 0f);
			num -= num2 * scale.Y;
			Vector2 vector3 = start + vector * ((float)rectangle.Height - vector2.Y) * scale.Y;
			if (num > 0f)
			{
				float num3 = 0f;
				while (num3 + 1f < num)
				{
					framing(1, vector3, num - num3, rectangle, out num2, out rectangle, out vector2, out color);
					if (num - num3 < (float)rectangle.Height)
					{
						num2 *= (num - num3) / (float)rectangle.Height;
						rectangle.Height = (int)(num - num3);
					}
					sb.Draw(tex, vector3, new Rectangle?(rectangle), color, rotation, vector2, scale, SpriteEffects.None, 0f);
					num3 += num2 * scale.Y;
					vector3 += vector * num2 * scale.Y;
				}
			}
			framing(2, vector3, num, default(Rectangle), out num2, out rectangle, out vector2, out color);
			sb.Draw(tex, vector3, new Rectangle?(rectangle), color, rotation, vector2, scale, SpriteEffects.None, 0f);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x003F0A62 File Offset: 0x003EEC62
		public static void DrawLine(SpriteBatch spriteBatch, Point start, Point end, Color color)
		{
			Utils.DrawLine(spriteBatch, new Vector2((float)(start.X << 4), (float)(start.Y << 4)), new Vector2((float)(end.X << 4), (float)(end.Y << 4)), color);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x003F0A9C File Offset: 0x003EEC9C
		public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
		{
			float num = Vector2.Distance(start, end);
			Vector2 vector = (end - start) / num;
			Vector2 value = start;
			Vector2 screenPosition = Main.screenPosition;
			float rotation = vector.ToRotation();
			for (float num2 = 0f; num2 <= num; num2 += 4f)
			{
				float num3 = num2 / num;
				spriteBatch.Draw(TextureAssets.BlackTile.Value, value - screenPosition, null, new Color(new Vector4(num3, num3, num3, 1f) * color.ToVector4()), rotation, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
				value = start + num2 * vector;
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x003F0B50 File Offset: 0x003EED50
		public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color colorStart, Color colorEnd, float width)
		{
			float num = Vector2.Distance(start, end);
			Vector2 vector = (end - start) / num;
			Vector2 value = start;
			Vector2 screenPosition = Main.screenPosition;
			float rotation = vector.ToRotation();
			float scale = width / 16f;
			for (float num2 = 0f; num2 <= num; num2 += width)
			{
				float amount = num2 / num;
				spriteBatch.Draw(TextureAssets.BlackTile.Value, value - screenPosition, null, Color.Lerp(colorStart, colorEnd, amount), rotation, Vector2.Zero, scale, SpriteEffects.None, 0f);
				value = start + num2 * vector;
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x003F0BF1 File Offset: 0x003EEDF1
		public static void DrawRectForTilesInWorld(SpriteBatch spriteBatch, Rectangle rect, Color color)
		{
			Utils.DrawRectForTilesInWorld(spriteBatch, new Point(rect.X, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height), color);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x003F0C2A File Offset: 0x003EEE2A
		public static void DrawRectForTilesInWorld(SpriteBatch spriteBatch, Point start, Point end, Color color)
		{
			Utils.DrawRect(spriteBatch, new Vector2((float)(start.X << 4), (float)(start.Y << 4)), new Vector2((float)((end.X << 4) - 4), (float)((end.Y << 4) - 4)), color);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x003F0C65 File Offset: 0x003EEE65
		public static void DrawRect(SpriteBatch spriteBatch, Rectangle rect, Color color)
		{
			Utils.DrawRect(spriteBatch, new Vector2((float)rect.X, (float)rect.Y), new Vector2((float)(rect.X + rect.Width), (float)(rect.Y + rect.Height)), color);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x003F0CA4 File Offset: 0x003EEEA4
		public static void DrawRect(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
		{
			Utils.DrawLine(spriteBatch, start, new Vector2(start.X, end.Y), color);
			Utils.DrawLine(spriteBatch, start, new Vector2(end.X, start.Y), color);
			Utils.DrawLine(spriteBatch, end, new Vector2(start.X, end.Y), color);
			Utils.DrawLine(spriteBatch, end, new Vector2(end.X, start.Y), color);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x003F0D15 File Offset: 0x003EEF15
		public static void DrawRect(SpriteBatch spriteBatch, Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft, Color color)
		{
			Utils.DrawLine(spriteBatch, topLeft, topRight, color);
			Utils.DrawLine(spriteBatch, topRight, bottomRight, color);
			Utils.DrawLine(spriteBatch, bottomRight, bottomLeft, color);
			Utils.DrawLine(spriteBatch, bottomLeft, topLeft, color);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x003F0D44 File Offset: 0x003EEF44
		public static void DrawCursorSingle(SpriteBatch sb, Color color, float rot = float.NaN, float scale = 1f, Vector2 manualPosition = default(Vector2), int cursorSlot = 0, int specialMode = 0)
		{
			bool flag = false;
			bool flag2 = true;
			bool flag3 = true;
			Vector2 zero = Vector2.Zero;
			Vector2 value = new Vector2((float)Main.mouseX, (float)Main.mouseY);
			if (manualPosition != Vector2.Zero)
			{
				value = manualPosition;
			}
			if (float.IsNaN(rot))
			{
				rot = 0f;
			}
			else
			{
				flag = true;
				rot -= 2.3561945f;
			}
			if (cursorSlot == 4 || cursorSlot == 5)
			{
				flag2 = false;
				zero = new Vector2(8f);
				if (flag && specialMode == 0)
				{
					float num = rot;
					if (num < 0f)
					{
						num += 6.2831855f;
					}
					for (float num2 = 0f; num2 < 4f; num2 += 1f)
					{
						if (Math.Abs(num - 1.5707964f * num2) <= 0.7853982f)
						{
							rot = 1.5707964f * num2;
							break;
						}
					}
				}
			}
			Vector2 value2 = Vector2.One;
			if ((Main.ThickMouse && cursorSlot == 0) || cursorSlot == 1)
			{
				value2 = Main.DrawThickCursor(cursorSlot == 1);
			}
			if (flag2)
			{
				sb.Draw(TextureAssets.Cursors[cursorSlot].Value, value + value2 + Vector2.One, null, color.MultiplyRGB(new Color(0.2f, 0.2f, 0.2f, 0.5f)), rot, zero, scale * 1.1f, SpriteEffects.None, 0f);
			}
			if (flag3)
			{
				sb.Draw(TextureAssets.Cursors[cursorSlot].Value, value + value2, null, color, rot, zero, scale, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x04000DAA RID: 3498
		public const long MaxCoins = 999999999L;

		// Token: 0x04000DAB RID: 3499
		public static Dictionary<DynamicSpriteFont, float[]> charLengths = new Dictionary<DynamicSpriteFont, float[]>();

		// Token: 0x04000DAC RID: 3500
		private static Regex _substitutionRegex = new Regex("{(\\?(?:!)?)?([a-zA-Z][\\w\\.]*)}", RegexOptions.Compiled);

		// Token: 0x04000DAD RID: 3501
		private const ulong RANDOM_MULTIPLIER = 25214903917UL;

		// Token: 0x04000DAE RID: 3502
		private const ulong RANDOM_ADD = 11UL;

		// Token: 0x04000DAF RID: 3503
		private const ulong RANDOM_MASK = 281474976710655UL;

		// Token: 0x020004DC RID: 1244
		// (Invoke) Token: 0x06002F73 RID: 12147
		public delegate bool TileActionAttempt(int x, int y);

		// Token: 0x020004DD RID: 1245
		// (Invoke) Token: 0x06002F77 RID: 12151
		public delegate void LaserLineFraming(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distanceCovered, out Rectangle frame, out Vector2 origin, out Color color);

		// Token: 0x020004DE RID: 1246
		// (Invoke) Token: 0x06002F7B RID: 12155
		public delegate Color ColorLerpMethod(float percent);

		// Token: 0x020004DF RID: 1247
		public struct ChaseResults
		{
			// Token: 0x0400570A RID: 22282
			public bool InterceptionHappens;

			// Token: 0x0400570B RID: 22283
			public Vector2 InterceptionPosition;

			// Token: 0x0400570C RID: 22284
			public float InterceptionTime;

			// Token: 0x0400570D RID: 22285
			public Vector2 ChaserVelocity;
		}
	}
}
