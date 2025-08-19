using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using ReLogic.Graphics;
using ReLogic.OS;
using ReLogic.Utilities;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.Utilities;
using Terraria.Utilities.Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000067 RID: 103
	public static class Utils
	{
		// Token: 0x06001017 RID: 4119 RVA: 0x00400165 File Offset: 0x003FE365
		public static Color ColorLerp_BlackToWhite(float percent)
		{
			return Color.Lerp(Color.Black, Color.White, percent);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00400177 File Offset: 0x003FE377
		public static double Lerp(double value1, double value2, double amount)
		{
			return value1 + (value2 - value1) * amount;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00400180 File Offset: 0x003FE380
		public static Vector2 Round(Vector2 input)
		{
			return new Vector2((float)Math.Round((double)input.X), (float)Math.Round((double)input.Y));
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x004001A1 File Offset: 0x003FE3A1
		public static bool IsPowerOfTwo(int x)
		{
			return x != 0 && (x & x - 1) == 0;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x004001B0 File Offset: 0x003FE3B0
		public static float SmoothStep(float min, float max, float x)
		{
			return MathHelper.Clamp((x - min) / (max - min), 0f, 1f);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x004001C8 File Offset: 0x003FE3C8
		public static double SmoothStep(double min, double max, double x)
		{
			return Utils.Clamp<double>((x - min) / (max - min), 0.0, 1.0);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x004001E8 File Offset: 0x003FE3E8
		public static float UnclampedSmoothStep(float min, float max, float x)
		{
			return (x - min) / (max - min);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x004001F1 File Offset: 0x003FE3F1
		public static double UnclampedSmoothStep(double min, double max, double x)
		{
			return (x - min) / (max - min);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x004001FC File Offset: 0x003FE3FC
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
						Utils.AddArgToDictionary(text, ref text2, ref dictionary);
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
			Utils.AddArgToDictionary(text, ref text2, ref dictionary);
			return dictionary;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00400290 File Offset: 0x003FE490
		public static void Swap<T>(ref T t1, ref T t2)
		{
			T val = t1;
			t1 = t2;
			t2 = val;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x004002B7 File Offset: 0x003FE4B7
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

		/// <summary>
		/// Remaps a provided value from 0-&gt;1 to a cosine curve. Provides a method of turning an input sawtooth pattern into a smooth cyclic curve. This is useful when using repeating timers to drive effects that should return smoothly to their initial value before repeating rather than reset abruptly.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		// Token: 0x06001022 RID: 4130 RVA: 0x004002E0 File Offset: 0x003FE4E0
		public static float Turn01ToCyclic010(float value)
		{
			return 1f - ((float)Math.Cos((double)(value * 6.2831855f)) * 0.5f + 0.5f);
		}

		/// <summary>
		/// Remaps a provided value from 0-&gt;1 to 0-&gt;1-&gt;0, essentially turning a sawtooth pattern into a triangle wave pattern:
		/// <code>
		///          Original:   Remap:    Range Values ↓    
		///                 /      / \          1        
		///              /        /   \        0.5      
		///           /          /     \        0        
		/// Input→    0 .5  1    0 .5  1
		/// Output→   0 .5  1    0  1  0
		/// </code>
		/// This is useful when using repeating timers to drive effects that should return linearly to their initial value before repeating rather than reset abruptly.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		// Token: 0x06001023 RID: 4131 RVA: 0x00400302 File Offset: 0x003FE502
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

		/// <summary>
		/// Linearly interpolates between several values denoting a continuous piecewise linear function (<see href="https://en.wikipedia.org/wiki/Piecewise_linear_function">Piecewise linear function wikipedia page</see>).
		/// <para /> This can be used to interpolate between several values, providing finer control over the transition between values.
		/// <code>
		/// floats parameter→ 0      4      1    Range Values ↓
		///                         /   \            3    
		///                       /        \         2   
		///                     /                    1
		///                   /                      0
		/// percent input→    0 .25 .5 .75  1 
		/// output→           0  2   4  3   2    
		/// </code>
		/// </summary>
		/// <param name="percent"></param>
		/// <param name="floats"></param>
		/// <returns></returns>
		// Token: 0x06001024 RID: 4132 RVA: 0x0040033C File Offset: 0x003FE53C
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

		// Token: 0x06001025 RID: 4133 RVA: 0x00400390 File Offset: 0x003FE590
		public static float WrappedLerp(float value1, float value2, float percent)
		{
			float num = percent * 2f;
			if (num > 1f)
			{
				num = 2f - num;
			}
			return MathHelper.Lerp(value1, value2, num);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x004003BD File Offset: 0x003FE5BD
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

		/// <summary>
		/// Remaps an input value (<paramref name="fromValue" />) in the range of <paramref name="fromMin" /> to <paramref name="fromMax" /> to the corresponding value proportionally along the range from <paramref name="toMin" /> to <paramref name="toMax" />.
		/// <para /> For example, an input of (1.5f, 1, 3, 20, 30) would result in 22.5 because 1.5 is 25% between 1 and 3 and 25% between 20 and 30 is 22.5.
		/// </summary>
		// Token: 0x06001027 RID: 4135 RVA: 0x004003F5 File Offset: 0x003FE5F5
		public static float Remap(float fromValue, float fromMin, float fromMax, float toMin, float toMax, bool clamped = true)
		{
			return MathHelper.Lerp(toMin, toMax, Utils.GetLerpValue(fromMin, fromMax, fromValue, clamped));
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0040040C File Offset: 0x003FE60C
		public static void ClampWithinWorld(ref int minX, ref int minY, ref int maxX, ref int maxY, bool lastValuesInclusiveToIteration = false, int fluffX = 0, int fluffY = 0)
		{
			int num = (lastValuesInclusiveToIteration > false) ? 1 : 0;
			minX = Utils.Clamp<int>(minX, fluffX, Main.maxTilesX - num - fluffX);
			maxX = Utils.Clamp<int>(maxX, fluffX, Main.maxTilesX - num - fluffX);
			minY = Utils.Clamp<int>(minY, fluffY, Main.maxTilesY - num - fluffY);
			maxY = Utils.Clamp<int>(maxY, fluffY, Main.maxTilesY - num - fluffY);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00400474 File Offset: 0x003FE674
		public static Utils.ChaseResults GetChaseResults(Vector2 chaserPosition, float chaserSpeed, Vector2 runnerPosition, Vector2 runnerVelocity)
		{
			Utils.ChaseResults result = default(Utils.ChaseResults);
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
				result.InterceptionTime = num / chaserSpeed;
				result.InterceptionPosition = runnerPosition;
			}
			else
			{
				float a = chaserSpeed * chaserSpeed - num2 * num2;
				float b = 2f * Vector2.Dot(value, runnerVelocity);
				float c = (0f - num) * num;
				float result2;
				float result3;
				if (!Utils.SolveQuadratic(a, b, c, out result2, out result3))
				{
					return default(Utils.ChaseResults);
				}
				if (result2 < 0f && result3 < 0f)
				{
					return default(Utils.ChaseResults);
				}
				if (result2 > 0f && result3 > 0f)
				{
					result.InterceptionTime = Math.Min(result2, result3);
				}
				else
				{
					result.InterceptionTime = Math.Max(result2, result3);
				}
				result.InterceptionPosition = runnerPosition + runnerVelocity * result.InterceptionTime;
			}
			result.ChaserVelocity = (result.InterceptionPosition - chaserPosition) / result.InterceptionTime;
			result.InterceptionHappens = true;
			return result;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x004005DC File Offset: 0x003FE7DC
		public static Vector2 FactorAcceleration(Vector2 currentVelocity, float timeToInterception, Vector2 descendOfProjectile, int framesOfLenience)
		{
			float num = Math.Max(0f, timeToInterception - (float)framesOfLenience);
			Vector2 vector = descendOfProjectile * (num * num) / 2f / timeToInterception;
			return currentVelocity - vector;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0040061C File Offset: 0x003FE81C
		public static bool SolveQuadratic(float a, float b, float c, out float result1, out float result2)
		{
			float num = b * b - 4f * a * c;
			result1 = 0f;
			result2 = 0f;
			if (num > 0f)
			{
				result1 = (0f - b + (float)Math.Sqrt((double)num)) / (2f * a);
				result2 = (0f - b - (float)Math.Sqrt((double)num)) / (2f * a);
				return true;
			}
			if (num < 0f)
			{
				return false;
			}
			result1 = (result2 = (0f - b + (float)Math.Sqrt((double)num)) / (2f * a));
			return true;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x004006B0 File Offset: 0x003FE8B0
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

		/// <summary>
		/// Returns the current world time in a 24 hour clock representation. This value is derived from <see cref="F:Terraria.Main.dayTime" /> and <see cref="F:Terraria.Main.time" />.
		/// <para /> For example <c>Utils.GetDayTimeAs24FloatStartingFromMidnight() &lt; 6.50f</c> would check if the time is before 6:30 AM.
		/// </summary>
		// Token: 0x0600102D RID: 4141 RVA: 0x00400704 File Offset: 0x003FE904
		public static float GetDayTimeAs24FloatStartingFromMidnight()
		{
			if (Main.dayTime)
			{
				return 4.5f + (float)(Main.time / 54000.0) * 15f;
			}
			return 19.5f + (float)(Main.time / 32400.0) * 9f;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00400751 File Offset: 0x003FE951
		public static Vector2 GetDayTimeAsDirectionIn24HClock()
		{
			return Utils.GetDayTimeAsDirectionIn24HClock(Utils.GetDayTimeAs24FloatStartingFromMidnight());
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00400760 File Offset: 0x003FE960
		public static Vector2 GetDayTimeAsDirectionIn24HClock(float timeFrom0To24)
		{
			return new Vector2(0f, -1f).RotatedBy((double)(timeFrom0To24 / 24f * 6.2831855f), default(Vector2));
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00400798 File Offset: 0x003FE998
		public static string[] ConvertMonoArgsToDotNet(string[] brokenArgs)
		{
			ArrayList arrayList = new ArrayList();
			string text = "";
			for (int i = 0; i < brokenArgs.Length; i++)
			{
				if (brokenArgs[i].StartsWith("-") || brokenArgs[i].StartsWith("+"))
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

		// Token: 0x06001031 RID: 4145 RVA: 0x00400854 File Offset: 0x003FEA54
		public static T Max<T>(params T[] args) where T : IComparable
		{
			T result = args[0];
			for (int i = 1; i < args.Length; i++)
			{
				ref T ptr = ref result;
				if (default(T) == null)
				{
					T t = result;
					ptr = ref t;
				}
				if (ptr.CompareTo(args[i]) < 0)
				{
					result = args[i];
				}
			}
			return result;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x004008B4 File Offset: 0x003FEAB4
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
			float num = vector.Distance(vector.ClosestPointOnLine(lineStart, lineEnd));
			float value2 = vector2.Distance(vector2.ClosestPointOnLine(lineStart, lineEnd));
			float value3 = vector3.Distance(vector3.ClosestPointOnLine(lineStart, lineEnd));
			float value4 = vector4.Distance(vector4.ClosestPointOnLine(lineStart, lineEnd));
			return MathHelper.Min(num, MathHelper.Min(value2, MathHelper.Min(value3, value4)));
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0040094C File Offset: 0x003FEB4C
		public static List<List<TextSnippet>> WordwrapStringSmart(string text, Color c, DynamicSpriteFont font, int maxWidth, int maxLines)
		{
			TextSnippet[] array4 = ChatManager.ParseMessage(text, c).ToArray();
			List<List<TextSnippet>> list = new List<List<TextSnippet>>();
			List<TextSnippet> list2 = new List<TextSnippet>();
			foreach (TextSnippet textSnippet in array4)
			{
				string[] array2 = textSnippet.Text.Split('\n', StringSplitOptions.None);
				for (int i = 0; i < array2.Length - 1; i++)
				{
					list2.Add(textSnippet.CopyMorph(array2[i]));
					list.Add(list2);
					list2 = new List<TextSnippet>();
				}
				list2.Add(textSnippet.CopyMorph(array2[array2.Length - 1]));
			}
			list.Add(list2);
			if (maxWidth != -1)
			{
				for (int j = 0; j < list.Count; j++)
				{
					List<TextSnippet> list3 = list[j];
					float num = 0f;
					for (int k = 0; k < list3.Count; k++)
					{
						float stringLength = list3[k].GetStringLength(font);
						if (stringLength + num > (float)maxWidth)
						{
							int num2 = maxWidth - (int)num;
							if (num > 0f)
							{
								num2 -= 16;
							}
							int num3 = Math.Min(list3[k].Text.Length, num2 / 8);
							for (int l = 0; l < list3[k].Text.Length; l++)
							{
								if (font.MeasureString(list3[k].Text.Substring(0, l)).X * list3[k].Scale < (float)num2)
								{
									num3 = l;
								}
							}
							if (num3 < 0)
							{
								num3 = 0;
							}
							string[] array3 = list3[k].Text.Split(' ', StringSplitOptions.None);
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
							string newText = list3[k].Text.Substring(0, num4);
							string newText2 = list3[k].Text.Substring(num4);
							list2 = new List<TextSnippet>
							{
								list3[k].CopyMorph(newText2)
							};
							for (int n = k + 1; n < list3.Count; n++)
							{
								list2.Add(list3[n]);
							}
							list3[k] = list3[k].CopyMorph(newText);
							list[j] = list[j].Take(k + 1).ToList<TextSnippet>();
							list.Insert(j + 1, list2);
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

		// Token: 0x06001034 RID: 4148 RVA: 0x00400C40 File Offset: 0x003FEE40
		public static string[] WordwrapString(string text, DynamicSpriteFont font, int maxWidth, int maxLines, out int lineAmount)
		{
			string[] array = new string[maxLines];
			int num = 0;
			List<string> list = new List<string>(text.Split('\n', StringSplitOptions.None));
			List<string> list2 = new List<string>(list[0].Split(' ', StringSplitOptions.None));
			int i = 1;
			while (i < list.Count && i < maxLines)
			{
				list2.Add("\n");
				list2.AddRange(list[i].Split(' ', StringSplitOptions.None));
				i++;
			}
			bool flag = true;
			while (list2.Count > 0)
			{
				string text2 = list2[0];
				string text3 = " ";
				if (list2.Count == 1)
				{
					text3 = "";
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
						string text4 = text2[0].ToString() ?? "";
						int num2 = 1;
						while (font.MeasureString(text4 + text2[num2].ToString() + "-").X <= (float)maxWidth)
						{
							text4 += text2[num2++].ToString();
						}
						text4 += "-";
						array[num++] = text4 + " ";
						if (num >= maxLines)
						{
							break;
						}
						list2.RemoveAt(0);
						list2.Insert(0, text2.Substring(num2));
					}
					else
					{
						string[] array3 = array;
						int num4 = num;
						array3[num4] = array3[num4] + text2 + text3;
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
					int num5 = num;
					array4[num5] = array4[num5] + text2 + text3;
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

		// Token: 0x06001035 RID: 4149 RVA: 0x00400E61 File Offset: 0x003FF061
		public static Rectangle CenteredRectangle(Vector2 center, Vector2 size)
		{
			return new Rectangle((int)(center.X - size.X / 2f), (int)(center.Y - size.Y / 2f), (int)size.X, (int)size.Y);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00400EA0 File Offset: 0x003FF0A0
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
			Vector2 vector = Vector2.Normalize(elipseSizes);
			vector = Vector2.One / vector;
			angleVector *= vector;
			angleVector.Normalize();
			return angleVector * elipseSizes / 2f;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00400F0E File Offset: 0x003FF10E
		public static bool FloatIntersect(float r1StartX, float r1StartY, float r1Width, float r1Height, float r2StartX, float r2StartY, float r2Width, float r2Height)
		{
			return r1StartX <= r2StartX + r2Width && r1StartY <= r2StartY + r2Height && r1StartX + r1Width >= r2StartX && r1StartY + r1Height >= r2StartY;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00400F34 File Offset: 0x003FF134
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

		/// <summary>
		/// Converts a coin value into an array of individual coin currency item counts. The result is a 4 element array containing <c>[Copper, Silver, Gold, Platinum]</c> coin counts totaling to the coin value provided.
		/// </summary>
		// Token: 0x06001039 RID: 4153 RVA: 0x00400FD4 File Offset: 0x003FF1D4
		public static int[] CoinsSplit(long count)
		{
			int[] array = new int[4];
			long num = 0L;
			long num2 = 1000000L;
			for (int num3 = 3; num3 >= 0; num3--)
			{
				array[num3] = (int)((count - num) / num2);
				num += (long)array[num3] * num2;
				num2 /= 100L;
			}
			return array;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00401018 File Offset: 0x003FF218
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

		// Token: 0x0600103B RID: 4155 RVA: 0x00401058 File Offset: 0x003FF258
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

		// Token: 0x0600103C RID: 4156 RVA: 0x004011A9 File Offset: 0x003FF3A9
		public static Vector2 ToScreenPosition(this Vector2 worldPosition)
		{
			return Vector2.Transform(worldPosition - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix) / Main.UIScale;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x004011D0 File Offset: 0x003FF3D0
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

		// Token: 0x0600103E RID: 4158 RVA: 0x00401234 File Offset: 0x003FF434
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

		// Token: 0x0600103F RID: 4159 RVA: 0x004012A4 File Offset: 0x003FF4A4
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
				if (propertyDescriptor != null)
				{
					return (propertyDescriptor.GetValue(obj) ?? "").ToString();
				}
				return "";
			});
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x004012E8 File Offset: 0x003FF4E8
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

		// Token: 0x06001041 RID: 4161 RVA: 0x00401328 File Offset: 0x003FF528
		private static void OpenFolderXdg(string folderPath)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "xdg-open";
			processStartInfo.Arguments = folderPath;
			processStartInfo.UseShellExecute = true;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.EnvironmentVariables["LD_LIBRARY_PATH"] = "/usr/lib:/lib";
			Process.Start(processStartInfo);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00401375 File Offset: 0x003FF575
		public static void OpenFolder(string folderPath)
		{
			if (Utils.TryCreatingDirectory(folderPath))
			{
				if (Platform.IsLinux)
				{
					Utils.OpenFolderXdg(folderPath);
					return;
				}
				Process.Start(new ProcessStartInfo(folderPath)
				{
					UseShellExecute = true
				});
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x004013A0 File Offset: 0x003FF5A0
		public static byte[] ToByteArray(this string str)
		{
			byte[] array = new byte[str.Length * 2];
			Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		/// <summary>
		/// Generates a random value between 0f (inclusive) and 1f (exclusive). <br />It will not return 1f.
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		// Token: 0x06001044 RID: 4164 RVA: 0x004013CD File Offset: 0x003FF5CD
		public static float NextFloat(this UnifiedRandom r)
		{
			return (float)r.NextDouble();
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x004013D6 File Offset: 0x003FF5D6
		public static float NextFloatDirection(this UnifiedRandom r)
		{
			return (float)r.NextDouble() * 2f - 1f;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x004013EB File Offset: 0x003FF5EB
		public static float NextFloat(this UnifiedRandom random, FloatRange range)
		{
			return random.NextFloat() * (range.Maximum - range.Minimum) + range.Minimum;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00401408 File Offset: 0x003FF608
		public static T NextFromList<T>(this UnifiedRandom random, params T[] objs)
		{
			return objs[random.Next(objs.Length)];
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00401419 File Offset: 0x003FF619
		public static T NextFromCollection<T>(this UnifiedRandom random, List<T> objs)
		{
			return objs[random.Next(objs.Count)];
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0040142D File Offset: 0x003FF62D
		public static int Next(this UnifiedRandom random, IntRange range)
		{
			return random.Next(range.Minimum, range.Maximum + 1);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00401443 File Offset: 0x003FF643
		public static Vector2 NextVector2Square(this UnifiedRandom r, float min, float max)
		{
			return new Vector2((max - min) * (float)r.NextDouble() + min, (max - min) * (float)r.NextDouble() + min);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00401464 File Offset: 0x003FF664
		public static Vector2 NextVector2FromRectangle(this UnifiedRandom r, Rectangle rect)
		{
			return new Vector2((float)rect.X + r.NextFloat() * (float)rect.Width, (float)rect.Y + r.NextFloat() * (float)rect.Height);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00401497 File Offset: 0x003FF697
		public static Vector2 NextVector2Unit(this UnifiedRandom r, float startRotation = 0f, float rotationRange = 6.2831855f)
		{
			return (startRotation + rotationRange * r.NextFloat()).ToRotationVector2();
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x004014A8 File Offset: 0x003FF6A8
		public static Vector2 NextVector2Circular(this UnifiedRandom r, float circleHalfWidth, float circleHalfHeight)
		{
			return r.NextVector2Unit(0f, 6.2831855f) * new Vector2(circleHalfWidth, circleHalfHeight) * r.NextFloat();
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x004014D1 File Offset: 0x003FF6D1
		public static Vector2 NextVector2CircularEdge(this UnifiedRandom r, float circleHalfWidth, float circleHalfHeight)
		{
			return r.NextVector2Unit(0f, 6.2831855f) * new Vector2(circleHalfWidth, circleHalfHeight);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x004014EF File Offset: 0x003FF6EF
		public static int Width(this Asset<Texture2D> asset)
		{
			if (!asset.IsLoaded)
			{
				return 0;
			}
			return asset.Value.Width;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00401506 File Offset: 0x003FF706
		public static int Height(this Asset<Texture2D> asset)
		{
			if (!asset.IsLoaded)
			{
				return 0;
			}
			return asset.Value.Height;
		}

		/// <inheritdoc cref="M:Terraria.Utils.Frame(Microsoft.Xna.Framework.Graphics.Texture2D,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" />
		// Token: 0x06001051 RID: 4177 RVA: 0x0040151D File Offset: 0x003FF71D
		public static Rectangle Frame(this Asset<Texture2D> tex, int horizontalFrames = 1, int verticalFrames = 1, int frameX = 0, int frameY = 0, int sizeOffsetX = 0, int sizeOffsetY = 0)
		{
			if (!tex.IsLoaded)
			{
				return Rectangle.Empty;
			}
			return tex.Value.Frame(horizontalFrames, verticalFrames, frameX, frameY, sizeOffsetX, sizeOffsetY);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00401541 File Offset: 0x003FF741
		public static Rectangle OffsetSize(this Rectangle rect, int xSize, int ySize)
		{
			rect.Width += xSize;
			rect.Height += ySize;
			return rect;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0040155C File Offset: 0x003FF75C
		public static Vector2 Size(this Asset<Texture2D> tex)
		{
			if (!tex.IsLoaded)
			{
				return Vector2.Zero;
			}
			return tex.Value.Size();
		}

		/// <summary>
		/// Returns a Rectangle region ("frame") of the Texture2D corresponding to the layout provided. Can be used to easily retrieve coordinates for drawing a specific frame of a texture.
		/// <para /> For example <c>texture.Frame(1, 3, 0, 2)</c> indicates that the texture has 1 column and 3 rows and returns the coordinates of the bottom (index #2, or 3rd) frame of the only column. 
		/// </summary>
		// Token: 0x06001054 RID: 4180 RVA: 0x00401578 File Offset: 0x003FF778
		public static Rectangle Frame(this Texture2D tex, int horizontalFrames = 1, int verticalFrames = 1, int frameX = 0, int frameY = 0, int sizeOffsetX = 0, int sizeOffsetY = 0)
		{
			int num = tex.Width / horizontalFrames;
			int num2 = tex.Height / verticalFrames;
			return new Rectangle(num * frameX, num2 * frameY, num + sizeOffsetX, num2 + sizeOffsetY);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x004015AC File Offset: 0x003FF7AC
		public static Vector2 OriginFlip(this Rectangle rect, Vector2 origin, SpriteEffects effects)
		{
			if (effects.HasFlag(1))
			{
				origin.X = (float)rect.Width - origin.X;
			}
			if (effects.HasFlag(2))
			{
				origin.Y = (float)rect.Height - origin.Y;
			}
			return origin;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0040160A File Offset: 0x003FF80A
		public static Vector2 Size(this Texture2D tex)
		{
			return new Vector2((float)tex.Width, (float)tex.Height);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0040161F File Offset: 0x003FF81F
		public static void WriteRGB(this BinaryWriter bb, Color c)
		{
			bb.Write(c.R);
			bb.Write(c.G);
			bb.Write(c.B);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00401648 File Offset: 0x003FF848
		public static void WriteVector2(this BinaryWriter bb, Vector2 v)
		{
			bb.Write(v.X);
			bb.Write(v.Y);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00401664 File Offset: 0x003FF864
		public static void WritePackedVector2(this BinaryWriter bb, Vector2 v)
		{
			bb.Write(new HalfVector2(v.X, v.Y).PackedValue);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00401690 File Offset: 0x003FF890
		public static Color ReadRGB(this BinaryReader bb)
		{
			return new Color((int)bb.ReadByte(), (int)bb.ReadByte(), (int)bb.ReadByte());
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x004016A9 File Offset: 0x003FF8A9
		public static Vector2 ReadVector2(this BinaryReader bb)
		{
			return new Vector2(bb.ReadSingle(), bb.ReadSingle());
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x004016BC File Offset: 0x003FF8BC
		public static Vector2 ReadPackedVector2(this BinaryReader bb)
		{
			HalfVector2 halfVector = default(HalfVector2);
			halfVector.PackedValue = bb.ReadUInt32();
			return halfVector.ToVector2();
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x004016E5 File Offset: 0x003FF8E5
		public static Vector2 Left(this Rectangle r)
		{
			return new Vector2((float)r.X, (float)(r.Y + r.Height / 2));
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00401703 File Offset: 0x003FF903
		public static Vector2 Right(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width), (float)(r.Y + r.Height / 2));
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00401728 File Offset: 0x003FF928
		public static Vector2 Top(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width / 2), (float)r.Y);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00401746 File Offset: 0x003FF946
		public static Vector2 Bottom(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width / 2), (float)(r.Y + r.Height));
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0040176B File Offset: 0x003FF96B
		public static Vector2 TopLeft(this Rectangle r)
		{
			return new Vector2((float)r.X, (float)r.Y);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00401780 File Offset: 0x003FF980
		public static Vector2 TopRight(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width), (float)r.Y);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0040179C File Offset: 0x003FF99C
		public static Vector2 BottomLeft(this Rectangle r)
		{
			return new Vector2((float)r.X, (float)(r.Y + r.Height));
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x004017B8 File Offset: 0x003FF9B8
		public static Vector2 BottomRight(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width), (float)(r.Y + r.Height));
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x004017DB File Offset: 0x003FF9DB
		public static Vector2 Center(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width / 2), (float)(r.Y + r.Height / 2));
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00401802 File Offset: 0x003FFA02
		public static Vector2 Size(this Rectangle r)
		{
			return new Vector2((float)r.Width, (float)r.Height);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00401818 File Offset: 0x003FFA18
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

		// Token: 0x06001068 RID: 4200 RVA: 0x0040196C File Offset: 0x003FFB6C
		public static Vector2 ClosestPointInRect(this Rectangle r, Vector2 point)
		{
			Vector2 result = point;
			if (result.X < (float)r.Left)
			{
				result.X = (float)r.Left;
			}
			if (result.X > (float)r.Right)
			{
				result.X = (float)r.Right;
			}
			if (result.Y < (float)r.Top)
			{
				result.Y = (float)r.Top;
			}
			if (result.Y > (float)r.Bottom)
			{
				result.Y = (float)r.Bottom;
			}
			return result;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x004019F8 File Offset: 0x003FFBF8
		public static Rectangle Modified(this Rectangle r, int x, int y, int w, int h)
		{
			return new Rectangle(r.X + x, r.Y + y, r.Width + w, r.Height + h);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00401A20 File Offset: 0x003FFC20
		public static bool IntersectsConeFastInaccurate(this Rectangle targetRect, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
		{
			Vector2 point = coneCenter + coneRotation.ToRotationVector2() * coneLength;
			Vector2 spinningpoint = targetRect.ClosestPointInRect(point) - coneCenter;
			float num = spinningpoint.RotatedBy((double)(0f - coneRotation), default(Vector2)).ToRotation();
			return num >= 0f - maximumAngle && num <= maximumAngle && spinningpoint.Length() < coneLength;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00401A88 File Offset: 0x003FFC88
		public static bool IntersectsConeSlowMoreAccurate(this Rectangle targetRect, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
		{
			Vector2 point = coneCenter + coneRotation.ToRotationVector2() * coneLength;
			return Utils.DoesFitInCone(targetRect.ClosestPointInRect(point), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.TopLeft(), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.TopRight(), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.BottomLeft(), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.BottomRight(), coneCenter, coneLength, coneRotation, maximumAngle);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00401B10 File Offset: 0x003FFD10
		public static bool DoesFitInCone(Vector2 point, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
		{
			Vector2 spinningpoint = point - coneCenter;
			float num = spinningpoint.RotatedBy((double)(0f - coneRotation), default(Vector2)).ToRotation();
			return num >= 0f - maximumAngle && num <= maximumAngle && spinningpoint.Length() < coneLength;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00401B5E File Offset: 0x003FFD5E
		public static float ToRotation(this Vector2 v)
		{
			return (float)Math.Atan2((double)v.Y, (double)v.X);
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00401B74 File Offset: 0x003FFD74
		public static Vector2 ToRotationVector2(this float f)
		{
			return new Vector2((float)Math.Cos((double)f), (float)Math.Sin((double)f));
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00401B8C File Offset: 0x003FFD8C
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

		// Token: 0x06001070 RID: 4208 RVA: 0x00401BEC File Offset: 0x003FFDEC
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

		// Token: 0x06001071 RID: 4209 RVA: 0x00401C4C File Offset: 0x003FFE4C
		public static Vector2 RotatedByRandom(this Vector2 spinninpoint, double maxRadians)
		{
			return spinninpoint.RotatedBy(Main.rand.NextDouble() * maxRadians - Main.rand.NextDouble() * maxRadians, default(Vector2));
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00401C81 File Offset: 0x003FFE81
		public static Vector2 Floor(this Vector2 vec)
		{
			vec.X = (float)((int)vec.X);
			vec.Y = (float)((int)vec.Y);
			return vec;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00401CA2 File Offset: 0x003FFEA2
		public static bool HasNaNs(this Vector2 vec)
		{
			return float.IsNaN(vec.X) || float.IsNaN(vec.Y);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00401CBE File Offset: 0x003FFEBE
		public static bool Between(this Vector2 vec, Vector2 minimum, Vector2 maximum)
		{
			return vec.X >= minimum.X && vec.X <= maximum.X && vec.Y >= minimum.Y && vec.Y <= maximum.Y;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00401CFD File Offset: 0x003FFEFD
		public static Vector2 ToVector2(this Point p)
		{
			return new Vector2((float)p.X, (float)p.Y);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00401D12 File Offset: 0x003FFF12
		public static Vector2 ToVector2(this Point16 p)
		{
			return new Vector2((float)p.X, (float)p.Y);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00401D27 File Offset: 0x003FFF27
		public static Vector2D ToVector2D(this Point p)
		{
			return new Vector2D((double)p.X, (double)p.Y);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00401D3C File Offset: 0x003FFF3C
		public static Vector2D ToVector2D(this Point16 p)
		{
			return new Vector2D((double)p.X, (double)p.Y);
		}

		/// <summary> <ToWorldCoordinates>
		/// 		Converts from Tile coordinates to World coordinates.
		/// 		<para /> If this overload has <c>autoAddX</c> and <c>autoAddY</c> parameters, those parameters default to 8, meaning that they can be omitted and the resulting world coordinate will be in the center of the tile. Zeros can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> If this overload has a <c>autoAddXY</c> parameter, that will be added to the resulting world coordinate. <c>new Vector2(8)</c> can be used to get thw world coordinate in the center of the tile. <c>Vector2.Zero</c> can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> <CoordinateConversionNote>
		/// 		<para /> By convention, Tile coordinates should be represented by represented using <see cref="T:Microsoft.Xna.Framework.Point" /> or <see cref="T:Terraria.DataStructures.Point16" /> and World coordinates should be represented using <see cref="T:Microsoft.Xna.Framework.Vector2" />. This convention is not always followed, so naming variables appropriately is highly recommended.
		/// 		<para /> Please read the <see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see> for more information.
		/// 	</CoordinateConversionNote> 
		/// 	</ToWorldCoordinates> </summary>
		// Token: 0x06001079 RID: 4217 RVA: 0x00401D51 File Offset: 0x003FFF51
		public static Vector2 ToWorldCoordinates(this Point p, float autoAddX = 8f, float autoAddY = 8f)
		{
			return p.ToVector2() * 16f + new Vector2(autoAddX, autoAddY);
		}

		/// <summary> <ToWorldCoordinates>
		/// 		Converts from Tile coordinates to World coordinates.
		/// 		<para /> If this overload has <c>autoAddX</c> and <c>autoAddY</c> parameters, those parameters default to 8, meaning that they can be omitted and the resulting world coordinate will be in the center of the tile. Zeros can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> If this overload has a <c>autoAddXY</c> parameter, that will be added to the resulting world coordinate. <c>new Vector2(8)</c> can be used to get thw world coordinate in the center of the tile. <c>Vector2.Zero</c> can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> <CoordinateConversionNote>
		/// 		<para /> By convention, Tile coordinates should be represented by represented using <see cref="T:Microsoft.Xna.Framework.Point" /> or <see cref="T:Terraria.DataStructures.Point16" /> and World coordinates should be represented using <see cref="T:Microsoft.Xna.Framework.Vector2" />. This convention is not always followed, so naming variables appropriately is highly recommended.
		/// 		<para /> Please read the <see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see> for more information.
		/// 	</CoordinateConversionNote> 
		/// 	</ToWorldCoordinates> </summary>
		// Token: 0x0600107A RID: 4218 RVA: 0x00401D6F File Offset: 0x003FFF6F
		public static Vector2 ToWorldCoordinates(this Point16 p, float autoAddX = 8f, float autoAddY = 8f)
		{
			return p.ToVector2() * 16f + new Vector2(autoAddX, autoAddY);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00401D90 File Offset: 0x003FFF90
		public static Vector2 MoveTowards(this Vector2 currentPosition, Vector2 targetPosition, float maxAmountAllowedToMove)
		{
			Vector2 v = targetPosition - currentPosition;
			if (v.Length() < maxAmountAllowedToMove)
			{
				return targetPosition;
			}
			return currentPosition + v.SafeNormalize(Vector2.Zero) * maxAmountAllowedToMove;
		}

		/// <summary> <ToTileCoordinates>
		/// 		Converts from World coordinates to Tile coordinates.
		/// 		<para /> The Tile coordinate returned will be the coordinate of the Tile that would contain the World Coordinate. Be sure to plan out which world coordinate to use when dealing with entities, sometimes the logic might be better suited to use the Center of the entity vs the Position.
		/// 		<para /> <CoordinateConversionNote>
		/// 		<para /> By convention, Tile coordinates should be represented by represented using <see cref="T:Microsoft.Xna.Framework.Point" /> or <see cref="T:Terraria.DataStructures.Point16" /> and World coordinates should be represented using <see cref="T:Microsoft.Xna.Framework.Vector2" />. This convention is not always followed, so naming variables appropriately is highly recommended.
		/// 		<para /> Please read the <see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see> for more information.
		/// 	</CoordinateConversionNote> 
		/// 	</ToTileCoordinates> </summary>
		// Token: 0x0600107C RID: 4220 RVA: 0x00401DC8 File Offset: 0x003FFFC8
		public static Point16 ToTileCoordinates16(this Vector2 vec)
		{
			return new Point16((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00401DE1 File Offset: 0x003FFFE1
		public static Point16 ToTileCoordinates16(this Vector2D vec)
		{
			return new Point16((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		/// <summary> <ToTileCoordinates>
		/// 		Converts from World coordinates to Tile coordinates.
		/// 		<para /> The Tile coordinate returned will be the coordinate of the Tile that would contain the World Coordinate. Be sure to plan out which world coordinate to use when dealing with entities, sometimes the logic might be better suited to use the Center of the entity vs the Position.
		/// 		<para /> <CoordinateConversionNote>
		/// 		<para /> By convention, Tile coordinates should be represented by represented using <see cref="T:Microsoft.Xna.Framework.Point" /> or <see cref="T:Terraria.DataStructures.Point16" /> and World coordinates should be represented using <see cref="T:Microsoft.Xna.Framework.Vector2" />. This convention is not always followed, so naming variables appropriately is highly recommended.
		/// 		<para /> Please read the <see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see> for more information.
		/// 	</CoordinateConversionNote> 
		/// 	</ToTileCoordinates> </summary>
		// Token: 0x0600107E RID: 4222 RVA: 0x00401DFA File Offset: 0x003FFFFA
		public static Point ToTileCoordinates(this Vector2 vec)
		{
			return new Point((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00401E13 File Offset: 0x00400013
		public static Point ToTileCoordinates(this Vector2D vec)
		{
			return new Point((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00401E2C File Offset: 0x0040002C
		public static Point ToPoint(this Vector2 v)
		{
			return new Point((int)v.X, (int)v.Y);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00401E41 File Offset: 0x00400041
		public static Point ToPoint(this Vector2D v)
		{
			return new Point((int)v.X, (int)v.Y);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00401E56 File Offset: 0x00400056
		public static Vector2D ToVector2D(this Vector2 v)
		{
			return new Vector2D((double)v.X, (double)v.Y);
		}

		/// <summary>
		/// <inheritdoc cref="M:Microsoft.Xna.Framework.Vector2.Normalize" />
		/// <para /> If this Vector2 can't be normalized, either because it is a 0 length vector or has not a number values, <paramref name="defaultValue" /> will be returned instead as a fallback. It will not be normalized so using <see cref="P:Microsoft.Xna.Framework.Vector2.UnitX" /> or <see cref="P:Microsoft.Xna.Framework.Vector2.UnitY" /> is appropriate. <see cref="P:Microsoft.Xna.Framework.Vector2.Zero" /> can also be appropriate if the surrounding logic expects it.
		/// <para /> Using SafeNormalize instead of Normalize avoids many difficult to anticipate issues that would cause bugs. 
		/// </summary>
		// Token: 0x06001083 RID: 4227 RVA: 0x00401E6B File Offset: 0x0040006B
		public static Vector2 SafeNormalize(this Vector2 v, Vector2 defaultValue)
		{
			if (v == Vector2.Zero || v.HasNaNs())
			{
				return defaultValue;
			}
			return Vector2.Normalize(v);
		}

		/// <summary> Returns the closest point on a line segment from <paramref name="A" /> to <paramref name="B" /> to another point <paramref name="P" />. The resulting point will be on the line and not extend past A or B. </summary>
		// Token: 0x06001084 RID: 4228 RVA: 0x00401E8C File Offset: 0x0040008C
		public static Vector2 ClosestPointOnLine(this Vector2 P, Vector2 A, Vector2 B)
		{
			Vector2 vector2 = P - A;
			Vector2 vector = B - A;
			float num = vector.LengthSquared();
			float num2 = Vector2.Dot(vector2, vector) / num;
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

		// Token: 0x06001085 RID: 4229 RVA: 0x00401EDC File Offset: 0x004000DC
		public static bool RectangleLineCollision(Vector2 rectTopLeft, Vector2 rectBottomRight, Vector2 lineStart, Vector2 lineEnd)
		{
			if (lineStart.Between(rectTopLeft, rectBottomRight) || lineEnd.Between(rectTopLeft, rectBottomRight))
			{
				return true;
			}
			Vector2 p;
			p..ctor(rectBottomRight.X, rectTopLeft.Y);
			Vector2 vector;
			vector..ctor(rectTopLeft.X, rectBottomRight.Y);
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

		// Token: 0x06001086 RID: 4230 RVA: 0x00401F8C File Offset: 0x0040018C
		public static Vector2 RotateRandom(this Vector2 spinninpoint, double maxRadians)
		{
			return spinninpoint.RotatedBy(Main.rand.NextDouble() * maxRadians - Main.rand.NextDouble() * maxRadians, default(Vector2));
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00401FC1 File Offset: 0x004001C1
		public static float AngleTo(this Vector2 Origin, Vector2 Target)
		{
			return (float)Math.Atan2((double)(Target.Y - Origin.Y), (double)(Target.X - Origin.X));
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00401FE5 File Offset: 0x004001E5
		public static float AngleFrom(this Vector2 Origin, Vector2 Target)
		{
			return (float)Math.Atan2((double)(Origin.Y - Target.Y), (double)(Origin.X - Target.X));
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0040200C File Offset: 0x0040020C
		public static Vector2 rotateTowards(Vector2 currentPosition, Vector2 currentVelocity, Vector2 targetPosition, float maxChange)
		{
			float num = currentVelocity.Length();
			float targetAngle = currentPosition.AngleTo(targetPosition);
			return currentVelocity.ToRotation().AngleTowards(targetAngle, 0.017453292f).ToRotationVector2() * num;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00402045 File Offset: 0x00400245
		public static float Distance(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Distance(Origin, Target);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0040204E File Offset: 0x0040024E
		public static float DistanceSQ(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.DistanceSquared(Origin, Target);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00402057 File Offset: 0x00400257
		public static Vector2 DirectionTo(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Normalize(Target - Origin);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00402065 File Offset: 0x00400265
		public static Vector2 DirectionFrom(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Normalize(Origin - Target);
		}

		/// <summary> Returns true if the <paramref name="Target" /> is within <paramref name="MaxRange" /> world coordinate units of <paramref name="Origin" />. </summary>
		// Token: 0x0600108E RID: 4238 RVA: 0x00402073 File Offset: 0x00400273
		public static bool WithinRange(this Vector2 Origin, Vector2 Target, float MaxRange)
		{
			return Vector2.DistanceSquared(Origin, Target) <= MaxRange * MaxRange;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00402084 File Offset: 0x00400284
		public static Vector2 XY(this Vector4 vec)
		{
			return new Vector2(vec.X, vec.Y);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00402097 File Offset: 0x00400297
		public static Vector2 ZW(this Vector4 vec)
		{
			return new Vector2(vec.Z, vec.W);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x004020AA File Offset: 0x004002AA
		public static Vector3 XZW(this Vector4 vec)
		{
			return new Vector3(vec.X, vec.Z, vec.W);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x004020C3 File Offset: 0x004002C3
		public static Vector3 YZW(this Vector4 vec)
		{
			return new Vector3(vec.Y, vec.Z, vec.W);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x004020DC File Offset: 0x004002DC
		public static Color MultiplyRGB(this Color firstColor, Color secondColor)
		{
			return new Color((int)((byte)((float)(firstColor.R * secondColor.R) / 255f)), (int)((byte)((float)(firstColor.G * secondColor.G) / 255f)), (int)((byte)((float)(firstColor.B * secondColor.B) / 255f)));
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00402134 File Offset: 0x00400334
		public static Color MultiplyRGBA(this Color firstColor, Color secondColor)
		{
			return new Color((int)((byte)((float)(firstColor.R * secondColor.R) / 255f)), (int)((byte)((float)(firstColor.G * secondColor.G) / 255f)), (int)((byte)((float)(firstColor.B * secondColor.B) / 255f)), (int)((byte)((float)(firstColor.A * secondColor.A) / 255f)));
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x004021A4 File Offset: 0x004003A4
		public static string Hex3(this Color color)
		{
			return (color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2")).ToLower();
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x004021F8 File Offset: 0x004003F8
		public static string Hex4(this Color color)
		{
			return (color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2") + color.A.ToString("X2")).ToLower();
		}

		/// <summary> Converts a bool to 1 if true, -1 if false. Suitable for working with <see cref="F:Terraria.Entity.direction" /> and <see cref="F:Terraria.Projectile.spriteDirection" />, or anything else related to geometry.</summary>
		// Token: 0x06001097 RID: 4247 RVA: 0x0040225F File Offset: 0x0040045F
		public static int ToDirectionInt(this bool value)
		{
			if (!value)
			{
				return -1;
			}
			return 1;
		}

		/// <summary> Converts a bool to 1 if true, 0 if false. </summary>
		// Token: 0x06001098 RID: 4248 RVA: 0x00402267 File Offset: 0x00400467
		public static int ToInt(this bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0040226F File Offset: 0x0040046F
		public static int ModulusPositive(this int myInteger, int modulusNumber)
		{
			return (myInteger % modulusNumber + modulusNumber) % modulusNumber;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00402278 File Offset: 0x00400478
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
				float num2 = targetAngle - 6.2831855f;
				angle = ((targetAngle - curAngle > curAngle - num2) ? MathHelper.Lerp(curAngle, num2, amount) : MathHelper.Lerp(curAngle, targetAngle, amount));
			}
			return MathHelper.WrapAngle(angle);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x004022E0 File Offset: 0x004004E0
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
			curAngle += MathHelper.Clamp(targetAngle - curAngle, 0f - maxChange, maxChange);
			return MathHelper.WrapAngle(curAngle);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00402344 File Offset: 0x00400544
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

		/// <summary> Returns a list of the indexes within this array that are <see langword="true" />. </summary>
		// Token: 0x0600109D RID: 4253 RVA: 0x00402384 File Offset: 0x00400584
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

		// Token: 0x0600109E RID: 4254 RVA: 0x004023B4 File Offset: 0x004005B4
		public static List<int> GetTrueIndexes(params bool[][] arrays)
		{
			List<int> list = new List<int>();
			foreach (bool[] array in arrays)
			{
				list.AddRange(array.GetTrueIndexes());
			}
			return list.Distinct<int>().ToList<int>();
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x004023F4 File Offset: 0x004005F4
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

		// Token: 0x060010A0 RID: 4256 RVA: 0x0040242E File Offset: 0x0040062E
		public static bool PressingShift(this KeyboardState kb)
		{
			return kb.IsKeyDown(160) || kb.IsKeyDown(161);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0040244C File Offset: 0x0040064C
		public static bool PressingControl(this KeyboardState kb)
		{
			return kb.IsKeyDown(162) || kb.IsKeyDown(163);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0040246C File Offset: 0x0040066C
		public static R[] MapArray<T, R>(T[] array, Func<T, R> mapper)
		{
			R[] array2 = new R[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = mapper(array[i]);
			}
			return array2;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x004024A5 File Offset: 0x004006A5
		public static bool PlotLine(Point16 p0, Point16 p1, Utils.TileActionAttempt plot, bool jump = true)
		{
			return Utils.PlotLine((int)p0.X, (int)p0.Y, (int)p1.X, (int)p1.Y, plot, jump);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x004024C6 File Offset: 0x004006C6
		public static bool PlotLine(Point p0, Point p1, Utils.TileActionAttempt plot, bool jump = true)
		{
			return Utils.PlotLine(p0.X, p0.Y, p1.X, p1.Y, plot, jump);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x004024E8 File Offset: 0x004006E8
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
			for (int i = x0; i != x1; i += num5)
			{
				if (flag)
				{
					if (!plot(num4, i))
					{
						return false;
					}
				}
				else if (!plot(i, num4))
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
							if (!plot(num4, i))
							{
								return false;
							}
						}
						else if (!plot(i, num4))
						{
							return false;
						}
					}
					num3 += num;
				}
			}
			return true;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x004025C7 File Offset: 0x004007C7
		public static int RandomNext(ref ulong seed, int bits)
		{
			seed = Utils.RandomNextSeed(seed);
			return (int)(seed >> 48 - bits);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x004025DD File Offset: 0x004007DD
		public static ulong RandomNextSeed(ulong seed)
		{
			return seed * 25214903917UL + 11UL & 281474976710655UL;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x004025F8 File Offset: 0x004007F8
		public static float RandomFloat(ref ulong seed)
		{
			return (float)Utils.RandomNext(ref seed, 24) / 16777216f;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0040260C File Offset: 0x0040080C
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

		// Token: 0x060010AA RID: 4266 RVA: 0x00402649 File Offset: 0x00400849
		public static int RandomInt(ref ulong seed, int min, int max)
		{
			return Utils.RandomInt(ref seed, max - min) + min;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00402656 File Offset: 0x00400856
		public static bool PlotTileLine(Vector2 start, Vector2 end, float width, Utils.TileActionAttempt plot)
		{
			return Utils.PlotTileLine(start.ToVector2D(), end.ToVector2D(), (double)width, plot);
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0040266C File Offset: 0x0040086C
		public static bool PlotTileLine(Vector2D start, Vector2D end, double width, Utils.TileActionAttempt plot)
		{
			double num = width / 2.0;
			Vector2D vector2D = end - start;
			Vector2D vector2D2 = vector2D / vector2D.Length();
			Vector2D vector2D3 = new Vector2D(0.0 - vector2D2.Y, vector2D2.X) * num;
			Point point = (start - vector2D3).ToTileCoordinates();
			Point point2 = (start + vector2D3).ToTileCoordinates();
			Point point3 = start.ToTileCoordinates();
			Point point4 = end.ToTileCoordinates();
			Point lineMinOffset = new Point(point.X - point3.X, point.Y - point3.Y);
			Point lineMaxOffset = new Point(point2.X - point3.X, point2.Y - point3.Y);
			return Utils.PlotLine(point3.X, point3.Y, point4.X, point4.Y, (int x, int y) => Utils.PlotLine(x + lineMinOffset.X, y + lineMinOffset.Y, x + lineMaxOffset.X, y + lineMaxOffset.Y, plot, false), true);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0040277C File Offset: 0x0040097C
		public static bool PlotTileTale(Vector2D start, Vector2D end, double width, Utils.TileActionAttempt plot)
		{
			double halfWidth = width / 2.0;
			Vector2D vector2D = end - start;
			Vector2D vector2D2 = vector2D / vector2D.Length();
			Vector2D perpOffset = new Vector2D(0.0 - vector2D2.Y, vector2D2.X);
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
				Point point4;
				point4..ctor(point2.X - pointStart.X, point2.Y - pointStart.Y);
				Point point5;
				point5..ctor(point3.X - pointStart.X, point3.Y - pointStart.Y);
				return Utils.PlotLine(x + point4.X, y + point4.Y, x + point5.X, y + point5.Y, plot, false);
			}, true);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0040288C File Offset: 0x00400A8C
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
					Point item = list[0];
					if (!WorldGen.InWorld(item.X, item.Y, 1))
					{
						list.Remove(item);
					}
					else
					{
						hashSet.Add(item);
						list.Remove(item);
						if (plot(item.X, item.Y))
						{
							Point item2;
							item2..ctor(item.X - 1, item.Y);
							if (!hashSet.Contains(item2))
							{
								list2.Add(item2);
							}
							item2..ctor(item.X + 1, item.Y);
							if (!hashSet.Contains(item2))
							{
								list2.Add(item2);
							}
							item2..ctor(item.X, item.Y - 1);
							if (!hashSet.Contains(item2))
							{
								list2.Add(item2);
							}
							item2..ctor(item.X, item.Y + 1);
							if (!hashSet.Contains(item2))
							{
								list2.Add(item2);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x004029E6 File Offset: 0x00400BE6
		public static int RandomConsecutive(double random, int odds)
		{
			return (int)Math.Log(1.0 - random, 1.0 / (double)odds);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00402A05 File Offset: 0x00400C05
		public static Vector2 RandomVector2(UnifiedRandom random, float min, float max)
		{
			return new Vector2((max - min) * (float)random.NextDouble() + min, (max - min) * (float)random.NextDouble() + min);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00402A26 File Offset: 0x00400C26
		public static Vector2D RandomVector2D(UnifiedRandom random, double min, double max)
		{
			return new Vector2D((max - min) * random.NextDouble() + min, (max - min) * random.NextDouble() + min);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00402A45 File Offset: 0x00400C45
		public static bool IndexInRange<T>(this T[] t, int index)
		{
			return index >= 0 && index < t.Length;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00402A53 File Offset: 0x00400C53
		public static bool IndexInRange<T>(this List<T> t, int index)
		{
			return index >= 0 && index < t.Count;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00402A64 File Offset: 0x00400C64
		public static T SelectRandom<T>(UnifiedRandom random, params T[] choices)
		{
			return choices[random.Next(choices.Length)];
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00402A78 File Offset: 0x00400C78
		public static void DrawBorderStringFourWay(SpriteBatch sb, DynamicSpriteFont font, string text, float x, float y, Color textColor, Color borderColor, Vector2 origin, float scale = 1f)
		{
			Color color = borderColor;
			Vector2 zero = Vector2.Zero;
			for (int i = 0; i < 5; i++)
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
				default:
					zero.X = x;
					zero.Y = y;
					color = textColor;
					break;
				}
				DynamicSpriteFontExtensionMethods.DrawString(sb, font, text, zero, color, 0f, origin, scale, 0, 0f);
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00402B4C File Offset: 0x00400D4C
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

		// Token: 0x060010B7 RID: 4279 RVA: 0x00402BC0 File Offset: 0x00400DC0
		public static Vector2 DrawBorderStringBig(SpriteBatch spriteBatch, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int maxCharactersDisplayed = -1)
		{
			if (maxCharactersDisplayed != -1 && text.Length > maxCharactersDisplayed)
			{
				text.Substring(0, maxCharactersDisplayed);
			}
			DynamicSpriteFont value = FontAssets.DeathText.Value;
			TextSnippet[] snippets = ChatManager.ParseMessage(text, color).ToArray();
			ChatManager.ConvertNormalSnippets(snippets);
			Vector2 textSize = ChatManager.GetStringSize(value, snippets, Vector2.One, -1f);
			int num;
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					ChatManager.DrawColorCodedString(spriteBatch, value, snippets, pos + new Vector2((float)i, (float)j), Color.Black, 0f, new Vector2(anchorx, anchory) * textSize, new Vector2(scale), out num, -1f, true);
				}
			}
			ChatManager.DrawColorCodedString(spriteBatch, value, snippets, pos, color, 0f, new Vector2(anchorx, anchory) * textSize, new Vector2(scale), out num, -1f, false);
			return textSize * scale;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00402CA9 File Offset: 0x00400EA9
		public static void DrawInvBG(SpriteBatch sb, Rectangle R, Color c = default(Color))
		{
			Utils.DrawInvBG(sb, R.X, R.Y, R.Width, R.Height, c);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00402CCA File Offset: 0x00400ECA
		public static void DrawInvBG(SpriteBatch sb, float x, float y, float w, float h, Color c = default(Color))
		{
			Utils.DrawInvBG(sb, (int)x, (int)y, (int)w, (int)h, c);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00402CE0 File Offset: 0x00400EE0
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

		// Token: 0x060010BB RID: 4283 RVA: 0x00402EF8 File Offset: 0x004010F8
		public static string ReadEmbeddedResource(string path)
		{
			string result;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00402F54 File Offset: 0x00401154
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

		// Token: 0x060010BD RID: 4285 RVA: 0x00403191 File Offset: 0x00401391
		public static void DrawSettingsPanel(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			Utils.DrawPanel(TextureAssets.SettingsPanel.Value, 2, 0, spriteBatch, position, width, color);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x004031A8 File Offset: 0x004013A8
		public static void DrawSettings2Panel(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			Utils.DrawPanel(TextureAssets.SettingsPanel.Value, 2, 0, spriteBatch, position, width, color);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x004031C0 File Offset: 0x004013C0
		public static void DrawPanel(Texture2D texture, int edgeWidth, int edgeShove, SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			spriteBatch.Draw(texture, position, new Rectangle?(new Rectangle(0, 0, edgeWidth, texture.Height)), color);
			spriteBatch.Draw(texture, new Vector2(position.X + (float)edgeWidth, position.Y), new Rectangle?(new Rectangle(edgeWidth + edgeShove, 0, texture.Width - (edgeWidth + edgeShove) * 2, texture.Height)), color, 0f, Vector2.Zero, new Vector2((width - (float)(edgeWidth * 2)) / (float)(texture.Width - (edgeWidth + edgeShove) * 2), 1f), 0, 0f);
			spriteBatch.Draw(texture, new Vector2(position.X + width - (float)edgeWidth, position.Y), new Rectangle?(new Rectangle(texture.Width - edgeWidth, 0, edgeWidth, texture.Height)), color);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00403298 File Offset: 0x00401498
		public static void DrawRectangle(SpriteBatch sb, Vector2 start, Vector2 end, Color colorStart, Color colorEnd, float width)
		{
			Utils.DrawLine(sb, start, new Vector2(start.X, end.Y), colorStart, colorEnd, width);
			Utils.DrawLine(sb, start, new Vector2(end.X, start.Y), colorStart, colorEnd, width);
			Utils.DrawLine(sb, end, new Vector2(start.X, end.Y), colorStart, colorEnd, width);
			Utils.DrawLine(sb, end, new Vector2(end.X, start.Y), colorStart, colorEnd, width);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0040331C File Offset: 0x0040151C
		public static void DrawLaser(SpriteBatch sb, Texture2D tex, Vector2 start, Vector2 end, Vector2 scale, Utils.LaserLineFraming framing)
		{
			Vector2 vector2 = Vector2.Normalize(end - start);
			float num = (end - start).Length();
			float rotation = vector2.ToRotation() - 1.5707964f;
			if (vector2.HasNaNs())
			{
				return;
			}
			float distanceCovered;
			Rectangle frame;
			Vector2 origin;
			Color color;
			framing(0, start, num, default(Rectangle), out distanceCovered, out frame, out origin, out color);
			sb.Draw(tex, start, new Rectangle?(frame), color, rotation, frame.Size() / 2f, scale, 0, 0f);
			num -= distanceCovered * scale.Y;
			Vector2 vector3 = start + vector2 * ((float)frame.Height - origin.Y) * scale.Y;
			if (num > 0f)
			{
				float num2 = 0f;
				while (num2 + 1f < num)
				{
					framing(1, vector3, num - num2, frame, out distanceCovered, out frame, out origin, out color);
					if (num - num2 < (float)frame.Height)
					{
						distanceCovered *= (num - num2) / (float)frame.Height;
						frame.Height = (int)(num - num2);
					}
					sb.Draw(tex, vector3, new Rectangle?(frame), color, rotation, origin, scale, 0, 0f);
					num2 += distanceCovered * scale.Y;
					vector3 += vector2 * distanceCovered * scale.Y;
				}
			}
			framing(2, vector3, num, default(Rectangle), out distanceCovered, out frame, out origin, out color);
			sb.Draw(tex, vector3, new Rectangle?(frame), color, rotation, origin, scale, 0, 0f);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x004034C2 File Offset: 0x004016C2
		public static void DrawLine(SpriteBatch spriteBatch, Point start, Point end, Color color)
		{
			Utils.DrawLine(spriteBatch, new Vector2((float)(start.X << 4), (float)(start.Y << 4)), new Vector2((float)(end.X << 4), (float)(end.Y << 4)), color);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x004034FC File Offset: 0x004016FC
		public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
		{
			float num = Vector2.Distance(start, end);
			Vector2 vector = (end - start) / num;
			Vector2 vector2 = start;
			Vector2 screenPosition = Main.screenPosition;
			float rotation = vector.ToRotation();
			for (float num2 = 0f; num2 <= num; num2 += 4f)
			{
				float num3 = num2 / num;
				spriteBatch.Draw(TextureAssets.BlackTile.Value, vector2 - screenPosition, null, new Color(new Vector4(num3, num3, num3, 1f) * color.ToVector4()), rotation, Vector2.Zero, 0.25f, 0, 0f);
				vector2 = start + num2 * vector;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x004035B0 File Offset: 0x004017B0
		public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color colorStart, Color colorEnd, float width)
		{
			float num = Vector2.Distance(start, end);
			Vector2 vector = (end - start) / num;
			Vector2 vector2 = start;
			Vector2 screenPosition = Main.screenPosition;
			float rotation = vector.ToRotation();
			float scale = width / 16f;
			for (float num2 = 0f; num2 <= num; num2 += width)
			{
				float amount = num2 / num;
				spriteBatch.Draw(TextureAssets.BlackTile.Value, vector2 - screenPosition, null, Color.Lerp(colorStart, colorEnd, amount), rotation, Vector2.Zero, scale, 0, 0f);
				vector2 = start + num2 * vector;
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00403651 File Offset: 0x00401851
		public static void DrawRectForTilesInWorld(SpriteBatch spriteBatch, Rectangle rect, Color color)
		{
			Utils.DrawRectForTilesInWorld(spriteBatch, new Point(rect.X, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height), color);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0040368A File Offset: 0x0040188A
		public static void DrawRectForTilesInWorld(SpriteBatch spriteBatch, Point start, Point end, Color color)
		{
			Utils.DrawRect(spriteBatch, new Vector2((float)(start.X << 4), (float)(start.Y << 4)), new Vector2((float)((end.X << 4) - 4), (float)((end.Y << 4) - 4)), color);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x004036C5 File Offset: 0x004018C5
		public static void DrawRect(SpriteBatch spriteBatch, Rectangle rect, Color color)
		{
			Utils.DrawRect(spriteBatch, new Vector2((float)rect.X, (float)rect.Y), new Vector2((float)(rect.X + rect.Width), (float)(rect.Y + rect.Height)), color);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00403704 File Offset: 0x00401904
		public static void DrawRect(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
		{
			Utils.DrawLine(spriteBatch, start, new Vector2(start.X, end.Y), color);
			Utils.DrawLine(spriteBatch, start, new Vector2(end.X, start.Y), color);
			Utils.DrawLine(spriteBatch, end, new Vector2(start.X, end.Y), color);
			Utils.DrawLine(spriteBatch, end, new Vector2(end.X, start.Y), color);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00403775 File Offset: 0x00401975
		public static void DrawRect(SpriteBatch spriteBatch, Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft, Color color)
		{
			Utils.DrawLine(spriteBatch, topLeft, topRight, color);
			Utils.DrawLine(spriteBatch, topRight, bottomRight, color);
			Utils.DrawLine(spriteBatch, bottomRight, bottomLeft, color);
			Utils.DrawLine(spriteBatch, bottomLeft, topLeft, color);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x004037A4 File Offset: 0x004019A4
		public static void DrawCursorSingle(SpriteBatch sb, Color color, float rot = float.NaN, float scale = 1f, Vector2 manualPosition = default(Vector2), int cursorSlot = 0, int specialMode = 0)
		{
			bool flag = false;
			bool flag2 = true;
			bool flag3 = true;
			Vector2 origin = Vector2.Zero;
			Vector2 vector;
			vector..ctor((float)Main.mouseX, (float)Main.mouseY);
			if (manualPosition != Vector2.Zero)
			{
				vector = manualPosition;
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
				origin..ctor(8f);
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
			Vector2 vector2 = Vector2.One;
			if ((Main.ThickMouse && cursorSlot == 0) || cursorSlot == 1)
			{
				vector2 = Main.DrawThickCursor(cursorSlot == 1);
			}
			if (flag2)
			{
				sb.Draw(TextureAssets.Cursors[cursorSlot].Value, vector + vector2 + Vector2.One, null, color.MultiplyRGB(new Color(0.2f, 0.2f, 0.2f, 0.5f)), rot, origin, scale * 1.1f, 0, 0f);
			}
			if (flag3)
			{
				sb.Draw(TextureAssets.Cursors[cursorSlot].Value, vector + vector2, null, color, rot, origin, scale, 0, 0f);
			}
		}

		/// <summary> <ToWorldCoordinates>
		/// 		Converts from Tile coordinates to World coordinates.
		/// 		<para /> If this overload has <c>autoAddX</c> and <c>autoAddY</c> parameters, those parameters default to 8, meaning that they can be omitted and the resulting world coordinate will be in the center of the tile. Zeros can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> If this overload has a <c>autoAddXY</c> parameter, that will be added to the resulting world coordinate. <c>new Vector2(8)</c> can be used to get thw world coordinate in the center of the tile. <c>Vector2.Zero</c> can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> <CoordinateConversionNote>
		/// 		<para /> By convention, Tile coordinates should be represented by represented using <see cref="T:Microsoft.Xna.Framework.Point" /> or <see cref="T:Terraria.DataStructures.Point16" /> and World coordinates should be represented using <see cref="T:Microsoft.Xna.Framework.Vector2" />. This convention is not always followed, so naming variables appropriately is highly recommended.
		/// 		<para /> Please read the <see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see> for more information.
		/// 	</CoordinateConversionNote> 
		/// 	</ToWorldCoordinates> </summary>
		// Token: 0x060010CB RID: 4299 RVA: 0x00403929 File Offset: 0x00401B29
		public static Vector2 ToWorldCoordinates(this Point p, Vector2 autoAddXY)
		{
			return p.ToWorldCoordinates(autoAddXY.X, autoAddXY.Y);
		}

		/// <summary> <ToWorldCoordinates>
		/// 		Converts from Tile coordinates to World coordinates.
		/// 		<para /> If this overload has <c>autoAddX</c> and <c>autoAddY</c> parameters, those parameters default to 8, meaning that they can be omitted and the resulting world coordinate will be in the center of the tile. Zeros can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> If this overload has a <c>autoAddXY</c> parameter, that will be added to the resulting world coordinate. <c>new Vector2(8)</c> can be used to get thw world coordinate in the center of the tile. <c>Vector2.Zero</c> can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> <CoordinateConversionNote>
		/// 		<para /> By convention, Tile coordinates should be represented by represented using <see cref="T:Microsoft.Xna.Framework.Point" /> or <see cref="T:Terraria.DataStructures.Point16" /> and World coordinates should be represented using <see cref="T:Microsoft.Xna.Framework.Vector2" />. This convention is not always followed, so naming variables appropriately is highly recommended.
		/// 		<para /> Please read the <see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see> for more information.
		/// 	</CoordinateConversionNote> 
		/// 	</ToWorldCoordinates> </summary>
		// Token: 0x060010CC RID: 4300 RVA: 0x0040393D File Offset: 0x00401B3D
		public static Vector2 ToWorldCoordinates(this Point16 p, Vector2 autoAddXY)
		{
			return p.ToVector2().ToWorldCoordinates(autoAddXY);
		}

		/// <summary> <ToWorldCoordinates>
		/// 		Converts from Tile coordinates to World coordinates.
		/// 		<para /> If this overload has <c>autoAddX</c> and <c>autoAddY</c> parameters, those parameters default to 8, meaning that they can be omitted and the resulting world coordinate will be in the center of the tile. Zeros can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> If this overload has a <c>autoAddXY</c> parameter, that will be added to the resulting world coordinate. <c>new Vector2(8)</c> can be used to get thw world coordinate in the center of the tile. <c>Vector2.Zero</c> can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> <CoordinateConversionNote>
		/// 		<para /> By convention, Tile coordinates should be represented by represented using <see cref="T:Microsoft.Xna.Framework.Point" /> or <see cref="T:Terraria.DataStructures.Point16" /> and World coordinates should be represented using <see cref="T:Microsoft.Xna.Framework.Vector2" />. This convention is not always followed, so naming variables appropriately is highly recommended.
		/// 		<para /> Please read the <see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see> for more information.
		/// 	</CoordinateConversionNote> 
		/// 	</ToWorldCoordinates> </summary>
		// Token: 0x060010CD RID: 4301 RVA: 0x0040394B File Offset: 0x00401B4B
		public static Vector2 ToWorldCoordinates(this Vector2 v, float autoAddX = 8f, float autoAddY = 8f)
		{
			return v.ToWorldCoordinates(new Vector2(autoAddX, autoAddY));
		}

		/// <summary> <ToWorldCoordinates>
		/// 		Converts from Tile coordinates to World coordinates.
		/// 		<para /> If this overload has <c>autoAddX</c> and <c>autoAddY</c> parameters, those parameters default to 8, meaning that they can be omitted and the resulting world coordinate will be in the center of the tile. Zeros can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> If this overload has a <c>autoAddXY</c> parameter, that will be added to the resulting world coordinate. <c>new Vector2(8)</c> can be used to get thw world coordinate in the center of the tile. <c>Vector2.Zero</c> can be used to get the world coordinate of the top left corner instead.
		/// 		<para /> <CoordinateConversionNote>
		/// 		<para /> By convention, Tile coordinates should be represented by represented using <see cref="T:Microsoft.Xna.Framework.Point" /> or <see cref="T:Terraria.DataStructures.Point16" /> and World coordinates should be represented using <see cref="T:Microsoft.Xna.Framework.Vector2" />. This convention is not always followed, so naming variables appropriately is highly recommended.
		/// 		<para /> Please read the <see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see> for more information.
		/// 	</CoordinateConversionNote> 
		/// 	</ToWorldCoordinates> </summary>
		// Token: 0x060010CE RID: 4302 RVA: 0x0040395A File Offset: 0x00401B5A
		public static Vector2 ToWorldCoordinates(this Vector2 v, Vector2 autoAddXY)
		{
			return v * 16f + autoAddXY;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0040396D File Offset: 0x00401B6D
		public static Point ToPoint(this Point16 p)
		{
			return new Point((int)p.X, (int)p.Y);
		}

		/// <summary> Converts this Vector2 to a Point16, resulting in X and Y values rounded towards 0. If the intention is to convert to Tile coordinates from World coordinates, use <see cref="M:Terraria.Utils.ToTileCoordinates16(Microsoft.Xna.Framework.Vector2)" /> instead. </summary>
		// Token: 0x060010D0 RID: 4304 RVA: 0x00403980 File Offset: 0x00401B80
		public static Point16 ToPoint16(this Vector2 v)
		{
			return new Point16((short)v.X, (short)v.Y);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00403998 File Offset: 0x00401B98
		public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
		{
			return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).UtcDateTime;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x004039B4 File Offset: 0x00401BB4
		public static T NextEnum<T>(this T src) where T : struct
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException("Argument " + typeof(T).FullName + " is not an Enum");
			}
			T[] Arr = (T[])Enum.GetValues(src.GetType());
			int i = Array.IndexOf<T>(Arr, src) + 1;
			if (Arr.Length != i)
			{
				return Arr[i];
			}
			return Arr[0];
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00403A30 File Offset: 0x00401C30
		public static T PreviousEnum<T>(this T src) where T : struct
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException("Argument " + typeof(T).FullName + " is not an Enum");
			}
			T[] Arr = (T[])Enum.GetValues(src.GetType());
			int i = Array.IndexOf<T>(Arr, src) - 1;
			if (i >= 0)
			{
				return Arr[i];
			}
			return Arr[Arr.Length - 1];
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00403AAB File Offset: 0x00401CAB
		public static Version MajorMinor(this Version v)
		{
			return new Version(v.Major, v.Minor);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00403ABE File Offset: 0x00401CBE
		public static Version MajorMinorBuild(this Version v)
		{
			if (v.Build >= 0)
			{
				return new Version(v.Major, v.Minor, v.Build);
			}
			return v.MajorMinor();
		}

		/// <summary>
		/// Returns a random element from the provided array.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="r"></param>
		/// <param name="array"></param>
		/// <returns></returns>
		// Token: 0x060010D6 RID: 4310 RVA: 0x00403AE7 File Offset: 0x00401CE7
		public static T Next<T>(this UnifiedRandom r, T[] array)
		{
			return array[r.Next(array.Length)];
		}

		/// <summary>
		/// Returns a random element from the provided list.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="r"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		// Token: 0x060010D7 RID: 4311 RVA: 0x00403AF8 File Offset: 0x00401CF8
		public static T Next<T>(this UnifiedRandom r, IList<T> list)
		{
			return list[r.Next(list.Count)];
		}

		/// <summary>
		/// Generates a random value between 0f (inclusive) and <paramref name="maxValue" /> (exclusive). <br />It will not return <paramref name="maxValue" />.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		// Token: 0x060010D8 RID: 4312 RVA: 0x00403B0C File Offset: 0x00401D0C
		public static float NextFloat(this UnifiedRandom r, float maxValue)
		{
			return (float)r.NextDouble() * maxValue;
		}

		/// <summary>
		/// Generates a random value between <paramref name="minValue" /> (inclusive) and <paramref name="maxValue" /> (exclusive). <br />It will not return <paramref name="maxValue" />.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		// Token: 0x060010D9 RID: 4313 RVA: 0x00403B17 File Offset: 0x00401D17
		public static float NextFloat(this UnifiedRandom r, float minValue, float maxValue)
		{
			return (float)r.NextDouble() * (maxValue - minValue) + minValue;
		}

		/// <summary>
		/// Returns true or false randomly with equal chance.
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		// Token: 0x060010DA RID: 4314 RVA: 0x00403B26 File Offset: 0x00401D26
		public static bool NextBool(this UnifiedRandom r)
		{
			return r.NextDouble() < 0.5;
		}

		/// <summary> Returns true 1 out of X times. </summary>
		// Token: 0x060010DB RID: 4315 RVA: 0x00403B39 File Offset: 0x00401D39
		public static bool NextBool(this UnifiedRandom r, int consequent)
		{
			if (consequent < 1)
			{
				throw new ArgumentOutOfRangeException("consequent", "consequent must be greater than or equal to 1.");
			}
			return r.Next(consequent) == 0;
		}

		/// <summary> Returns true X out of Y times. </summary>
		// Token: 0x060010DC RID: 4316 RVA: 0x00403B59 File Offset: 0x00401D59
		public static bool NextBool(this UnifiedRandom r, int antecedent, int consequent)
		{
			if (antecedent > consequent)
			{
				throw new ArgumentOutOfRangeException("antecedent", "antecedent must be less than or equal to consequent.");
			}
			return r.Next(consequent) < antecedent;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00403B79 File Offset: 0x00401D79
		public static int Repeat(int value, int length)
		{
			if (value < 0)
			{
				return value % length + length;
			}
			return value % length;
		}

		/// <summary>
		/// Bit packs a BitArray into a Byte Array and then sends the byte array
		/// <BitArrayUsage>
		/// 		<para /> Useful for sending/receiving a large number of flags/bools. If the data length is unknown to the receiver, length would need to be sent as well.
		/// 		This example sends a bool array of length <c>ItemLoader.ItemCount</c>, which will be consistent between the sender and receiver, so length is not sent.
		/// 		<code>
		/// // Field
		/// bool[] itemsFound = new bool[ItemLoader.ItemCount];
		///
		/// // NetSend
		/// Utils.SendBitArray(new (itemsFound), writer);
		///
		/// // NetReceive
		/// var itemsFoundData = Utils.ReceiveBitArray(ItemLoader.ItemCount, reader);
		/// itemFoundData.CopyTo(itemsFound, 0);
		/// 		</code>
		/// 	</BitArrayUsage>
		/// </summary>
		// Token: 0x060010DE RID: 4318 RVA: 0x00403B88 File Offset: 0x00401D88
		public static void SendBitArray(BitArray arr, BinaryWriter writer)
		{
			byte[] result = new byte[(arr.Length - 1) / 8 + 1];
			arr.CopyTo(result, 0);
			writer.Write(result);
		}

		/// <summary>
		/// Receives the result of SendBitArray, and returns the corresponding BitArray
		/// <BitArrayUsage>
		/// 		<para /> Useful for sending/receiving a large number of flags/bools. If the data length is unknown to the receiver, length would need to be sent as well.
		/// 		This example sends a bool array of length <c>ItemLoader.ItemCount</c>, which will be consistent between the sender and receiver, so length is not sent.
		/// 		<code>
		/// // Field
		/// bool[] itemsFound = new bool[ItemLoader.ItemCount];
		///
		/// // NetSend
		/// Utils.SendBitArray(new (itemsFound), writer);
		///
		/// // NetReceive
		/// var itemsFoundData = Utils.ReceiveBitArray(ItemLoader.ItemCount, reader);
		/// itemFoundData.CopyTo(itemsFound, 0);
		/// 		</code>
		/// 	</BitArrayUsage>
		/// </summary>
		// Token: 0x060010DF RID: 4319 RVA: 0x00403BB8 File Offset: 0x00401DB8
		public static BitArray ReceiveBitArray(int BitArrLength, BinaryReader reader)
		{
			byte[] receive = new byte[(BitArrLength - 1) / 8 + 1];
			receive = reader.ReadBytes(receive.Length);
			return new BitArray(receive);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00403BE4 File Offset: 0x00401DE4
		public static void OpenToURL(string url)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				Process.Start("explorer.exe", "\"" + url + "\"");
				return;
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				Process.Start("open", "\"" + url + "\"");
				return;
			}
			Process.Start("xdg-open", "\"" + url + "\"");
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00403C5C File Offset: 0x00401E5C
		public static void ShowFancyErrorMessage(string message, int returnToMenu, UIState returnToState = null)
		{
			if (!Main.dedServ)
			{
				Logging.tML.Error(message);
				Interface.errorMessage.Show(message, returnToMenu, returnToState, "", false, false, null);
				return;
			}
			Utils.LogAndConsoleErrorMessage(message);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00403C8C File Offset: 0x00401E8C
		public static void LogAndChatAndConsoleInfoMessage(string message)
		{
			Logging.tML.Info(message);
			if (Main.dedServ)
			{
				Console.WriteLine(message);
				return;
			}
			Main.NewText(message, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00403CBC File Offset: 0x00401EBC
		public static void LogAndConsoleInfoMessage(string message)
		{
			Logging.tML.Info(message);
			if (Main.dedServ)
			{
				Console.WriteLine(message);
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00403CD6 File Offset: 0x00401ED6
		public static void LogAndConsoleInfoMessageFormat(string format, params object[] args)
		{
			Utils.LogAndConsoleInfoMessage(string.Format(format, args));
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00403CE4 File Offset: 0x00401EE4
		public static void LogAndConsoleErrorMessage(string message)
		{
			Logging.tML.Error(message);
			if (Main.dedServ)
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine("ERROR: " + message);
				Console.ResetColor();
			}
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00403D14 File Offset: 0x00401F14
		internal static string CleanChatTags(string text)
		{
			return string.Join("", from x in ChatManager.ParseMessage(text, Color.White)
			where x.GetType() == typeof(TextSnippet)
			select x.Text);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00403D80 File Offset: 0x00401F80
		internal static void HandleSaveErrorMessageLogging(NetworkText message, bool broadcast)
		{
			Utils.LogAndConsoleInfoMessage(message.ToString());
			if (Main.gameMenu && Main.menuMode == 10)
			{
				Interface.pendingErrorMessages.Push(message.ToString());
				return;
			}
			if (!Main.gameMenu)
			{
				if (broadcast)
				{
					ChatHelper.BroadcastChatMessage(message, Color.OrangeRed, -1);
					return;
				}
				Main.NewText(message, new Color?(Color.OrangeRed));
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00403DE0 File Offset: 0x00401FE0
		internal static NetworkText CreateSaveErrorMessage(string localizationKey, Dictionary<string, string> errors, bool doubleNewline = false)
		{
			string separator = doubleNewline ? "\n\n" : "\n";
			object[] array = new object[1];
			array[0] = separator + string.Join(separator, from x in errors
			select x.Key + ":\n" + x.Value);
			return NetworkText.FromKey(localizationKey, array);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00403E3D File Offset: 0x0040203D
		private static void AddArgToDictionary(string text, ref string text2, ref Dictionary<string, string> dictionary)
		{
			if (text == null)
			{
				return;
			}
			if (!dictionary.TryAdd(text.ToLower(), text2))
			{
				Console.WriteLine("Unexpected Issue with Launch Arguments: Duplicate Launch Arg \"" + text + "\"");
			}
			text2 = "";
		}

		// Token: 0x04000EF5 RID: 3829
		public const long MaxCoins = 999999999L;

		// Token: 0x04000EF6 RID: 3830
		public static Dictionary<DynamicSpriteFont, float[]> charLengths = new Dictionary<DynamicSpriteFont, float[]>();

		// Token: 0x04000EF7 RID: 3831
		private static Regex _substitutionRegex = new Regex("{(\\?(?:!)?)?([a-zA-Z][\\w\\.]*)}", RegexOptions.Compiled);

		// Token: 0x04000EF8 RID: 3832
		private const ulong RANDOM_MULTIPLIER = 25214903917UL;

		// Token: 0x04000EF9 RID: 3833
		private const ulong RANDOM_ADD = 11UL;

		// Token: 0x04000EFA RID: 3834
		private const ulong RANDOM_MASK = 281474976710655UL;

		// Token: 0x020007F3 RID: 2035
		// (Invoke) Token: 0x06004FC8 RID: 20424
		public delegate bool TileActionAttempt(int x, int y);

		// Token: 0x020007F4 RID: 2036
		// (Invoke) Token: 0x06004FCC RID: 20428
		public delegate void LaserLineFraming(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distanceCovered, out Rectangle frame, out Vector2 origin, out Color color);

		// Token: 0x020007F5 RID: 2037
		// (Invoke) Token: 0x06004FD0 RID: 20432
		public delegate Color ColorLerpMethod(float percent);

		// Token: 0x020007F6 RID: 2038
		public struct ChaseResults
		{
			// Token: 0x040067AE RID: 26542
			public bool InterceptionHappens;

			// Token: 0x040067AF RID: 26543
			public Vector2 InterceptionPosition;

			// Token: 0x040067B0 RID: 26544
			public float InterceptionTime;

			// Token: 0x040067B1 RID: 26545
			public Vector2 ChaserVelocity;
		}
	}
}
