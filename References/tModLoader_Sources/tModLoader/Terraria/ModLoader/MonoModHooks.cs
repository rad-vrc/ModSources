using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using log4net;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using MonoMod.Utils;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Default;

namespace Terraria.ModLoader
{
	// Token: 0x020001D7 RID: 471
	public static class MonoModHooks
	{
		// Token: 0x060024CE RID: 9422 RVA: 0x004EA468 File Offset: 0x004E8668
		private static MonoModHooks.DetourList GetDetourList(Assembly asm)
		{
			if (asm == typeof(Action).Assembly)
			{
				throw new ArgumentException("Cannot identify owning assembly of hook. Make sure there are no delegate type changing wrappers between the method/lambda and the Modify/Add/+= call. Eg `new ILContext.Manipulator(action)` is bad");
			}
			MonoModHooks.DetourList list;
			if (!MonoModHooks.assemblyDetours.TryGetValue(asm, out list))
			{
				return MonoModHooks.assemblyDetours[asm] = new MonoModHooks.DetourList();
			}
			return list;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x004EA4BB File Offset: 0x004E86BB
		[Obsolete("No longer required. NativeDetour is gone. Detour should not be used. Hook is safe to use", true)]
		public static void RequestNativeAccess()
		{
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x004EA4C0 File Offset: 0x004E86C0
		internal static void Initialize()
		{
			if (MonoModHooks.isInitialized)
			{
				return;
			}
			DetourManager.DetourApplied += delegate(DetourInfo info)
			{
				Assembly owner = info.Entry.DeclaringType.Assembly;
				MonoModHooks.GetDetourList(owner).detours.Add(info);
				string msg = "Hook " + MonoModHooks.StringRep(info.Method.Method) + " added by " + owner.GetName().Name;
				MethodSignature targetSig = MethodSignature.ForMethod(info.Method.Method);
				MethodSignature detourSig = MethodSignature.ForMethod(info.Entry, true);
				if (detourSig.ParameterCount != targetSig.ParameterCount + 1 || detourSig.FirstParameter.GetMethod("Invoke") == null)
				{
					msg += " WARNING! No orig delegate, incompatible with other hooks to this method";
				}
				Logging.tML.Debug(msg);
			};
			DetourManager.ILHookApplied += delegate(ILHookInfo info)
			{
				Assembly owner = info.ManipulatorMethod.DeclaringType.Assembly;
				MonoModHooks.GetDetourList(owner).ilHooks.Add(info);
				Logging.tML.Debug("ILHook " + MonoModHooks.StringRep(info.Method.Method) + " added by " + owner.GetName().Name);
			};
			MonoModHooks.isInitialized = true;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x004EA524 File Offset: 0x004E8724
		private static string StringRep(MethodBase m)
		{
			string paramString = string.Join(", ", m.GetParameters().Select(delegate(ParameterInfo p)
			{
				Type t = p.ParameterType;
				string s = "";
				if (t.IsByRef)
				{
					s = (p.IsOut ? "out " : "ref ");
					t = t.GetElementType();
				}
				string i;
				return s + (MonoModHooks.defaultAliases.TryGetValue(t, out i) ? i : t.Name);
			}));
			Type declaringType = m.DeclaringType;
			string owner = ((declaringType != null) ? declaringType.FullName : null) ?? ((m is DynamicMethod) ? "dynamic" : "unknown");
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
			defaultInterpolatedStringHandler.AppendFormatted(owner);
			defaultInterpolatedStringHandler.AppendLiteral("::");
			defaultInterpolatedStringHandler.AppendFormatted(m.Name);
			defaultInterpolatedStringHandler.AppendLiteral("(");
			defaultInterpolatedStringHandler.AppendFormatted(paramString);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x004EA5E4 File Offset: 0x004E87E4
		internal static void RemoveAll(Mod mod)
		{
			if (mod is ModLoaderMod)
			{
				return;
			}
			foreach (Assembly asm in AssemblyManager.GetModAssemblies(mod.Name))
			{
				MonoModHooks.DetourList list;
				if (MonoModHooks.assemblyDetours.TryGetValue(asm, out list))
				{
					ILog tML = Logging.tML;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 4);
					defaultInterpolatedStringHandler.AppendLiteral("Unloading ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(list.ilHooks.Count);
					defaultInterpolatedStringHandler.AppendLiteral(" IL hooks, ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(list.detours.Count);
					defaultInterpolatedStringHandler.AppendLiteral(" detours from ");
					defaultInterpolatedStringHandler.AppendFormatted(asm.GetName().Name);
					defaultInterpolatedStringHandler.AppendLiteral(" in ");
					defaultInterpolatedStringHandler.AppendFormatted(mod.Name);
					tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
					foreach (DetourInfo detour in list.detours)
					{
						if (detour.IsApplied)
						{
							detour.Undo();
						}
					}
					foreach (ILHookInfo ilHook in list.ilHooks)
					{
						if (ilHook.IsApplied)
						{
							ilHook.Undo();
						}
					}
				}
			}
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x004EA79C File Offset: 0x004E899C
		internal static void Clear()
		{
			HookEndpointManager.Clear();
			MonoModHooks.assemblyDetours.Clear();
			MonoModHooks._hookCache.Clear();
			Type type = typeof(ReflectionHelper);
			FieldInfo[] array = new FieldInfo[]
			{
				type.GetField("AssemblyCache", BindingFlags.Static | BindingFlags.NonPublic),
				type.GetField("AssembliesCache", BindingFlags.Static | BindingFlags.NonPublic),
				type.GetField("ResolveReflectionCache", BindingFlags.Static | BindingFlags.NonPublic)
			};
			for (int i = 0; i < array.Length; i++)
			{
				((IDictionary)array[i].GetValue(null)).Clear();
			}
		}

		/// <summary>
		/// Adds a hook (implemented by <paramref name="hookDelegate" />) to <paramref name="method" />.
		/// </summary>
		/// <param name="method">The method to hook.</param>
		/// <param name="hookDelegate">The hook delegate to use.</param>
		// Token: 0x060024D4 RID: 9428 RVA: 0x004EA825 File Offset: 0x004E8A25
		public static void Add(MethodBase method, Delegate hookDelegate)
		{
			if (!MonoModHooks._hookCache.TryAdd(new ValueTuple<MethodBase, Delegate>(method, hookDelegate), new Hook(method, hookDelegate)))
			{
				throw new ArgumentException("Delegate has already been applied to this method as a hook!");
			}
		}

		/// <summary>
		/// Adds an IL hook (implemented by <paramref name="callback" />) to <paramref name="method" />.
		/// </summary>
		/// <param name="method">The method to hook.</param>
		/// <param name="callback">The hook delegate to use.</param>
		// Token: 0x060024D5 RID: 9429 RVA: 0x004EA84C File Offset: 0x004E8A4C
		public static void Modify(MethodBase method, ILContext.Manipulator callback)
		{
			if (!MonoModHooks._hookCache.TryAdd(new ValueTuple<MethodBase, Delegate>(method, callback), new ILHook(method, callback)))
			{
				throw new ArgumentException("Delegate has already been applied to this method as a hook!");
			}
		}

		/// <summary>
		/// Dumps the list of currently registered IL hooks to the console. Useful for checking if a hook has been correctly added.
		/// </summary>
		/// <exception cref="T:System.Exception"></exception>
		// Token: 0x060024D6 RID: 9430 RVA: 0x004EA874 File Offset: 0x004E8A74
		public static void DumpILHooks()
		{
			object ilHooksFieldValue = typeof(HookEndpointManager).GetField("ILHooks", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
			IReadOnlyDictionary<ValueTuple<MethodBase, Delegate>, ILHook> ilHooks = ilHooksFieldValue as IReadOnlyDictionary<ValueTuple<MethodBase, Delegate>, ILHook>;
			if (ilHooks != null)
			{
				Logging.tML.Debug("Dump of registered IL Hooks:");
				using (IEnumerator<KeyValuePair<ValueTuple<MethodBase, Delegate>, ILHook>> enumerator = ilHooks.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<ValueTuple<MethodBase, Delegate>, ILHook> item = enumerator.Current;
						ILog tML = Logging.tML;
						string str = item.Key.ToString();
						string str2 = ": ";
						ILHook value = item.Value;
						tML.Debug(str + str2 + ((value != null) ? value.ToString() : null));
					}
					return;
				}
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Failed to get HookEndpointManager.ILHooks: Type is ");
			defaultInterpolatedStringHandler.AppendFormatted<Type>(ilHooksFieldValue.GetType());
			throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		/// <summary>
		/// Dumps the list of currently registered On hooks to the console. Useful for checking if a hook has been correctly added.
		/// </summary>
		/// <exception cref="T:System.Exception"></exception>
		// Token: 0x060024D7 RID: 9431 RVA: 0x004EA958 File Offset: 0x004E8B58
		public static void DumpOnHooks()
		{
			object hooksFieldValue = typeof(HookEndpointManager).GetField("Hooks", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
			IReadOnlyDictionary<ValueTuple<MethodBase, Delegate>, Hook> detours = hooksFieldValue as IReadOnlyDictionary<ValueTuple<MethodBase, Delegate>, Hook>;
			if (detours != null)
			{
				Logging.tML.Debug("Dump of registered Detours:");
				using (IEnumerator<KeyValuePair<ValueTuple<MethodBase, Delegate>, Hook>> enumerator = detours.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<ValueTuple<MethodBase, Delegate>, Hook> item = enumerator.Current;
						ILog tML = Logging.tML;
						string str = item.Key.ToString();
						string str2 = ": ";
						Hook value = item.Value;
						tML.Debug(str + str2 + ((value != null) ? value.ToString() : null));
					}
					return;
				}
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Failed to get HookEndpointManager.Hooks: Type is ");
			defaultInterpolatedStringHandler.AppendFormatted<Type>(hooksFieldValue.GetType());
			throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		/// <summary>
		/// Dumps the information about the given ILContext to a file in Logs/ILDumps/{Mod Name}/{Method Name}.txt<br />
		/// It may be useful to use a tool such as <see href="https://www.diffchecker.com/" /> to compare the IL before and after edits
		/// </summary>
		/// <param name="mod"></param>
		/// <param name="il"></param>
		// Token: 0x060024D8 RID: 9432 RVA: 0x004EAA3C File Offset: 0x004E8C3C
		public static void DumpIL(Mod mod, ILContext il)
		{
			string methodName = il.Method.FullName.Replace(':', '_').Replace('<', '[').Replace('>', ']');
			if (methodName.Contains('?'))
			{
				string text = methodName;
				int num = methodName.LastIndexOf('?') + 1;
				methodName = text.Substring(num, text.Length - num);
			}
			methodName = string.Join("_", methodName.Split(Path.GetInvalidFileNameChars()));
			string filePath = Path.Combine(Logging.LogDir, "ILDumps", mod.Name, methodName + ".txt");
			string folderPath = Path.GetDirectoryName(filePath);
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}
			File.WriteAllText(filePath, il.ToString());
			ILog tML = Logging.tML;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Dumped ILContext \"");
			defaultInterpolatedStringHandler.AppendFormatted(il.Method.FullName);
			defaultInterpolatedStringHandler.AppendLiteral("\" to \"");
			defaultInterpolatedStringHandler.AppendFormatted(filePath);
			defaultInterpolatedStringHandler.AppendLiteral("\"");
			tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x0400173D RID: 5949
		private static Dictionary<Type, string> defaultAliases = new Dictionary<Type, string>
		{
			{
				typeof(object),
				"object"
			},
			{
				typeof(bool),
				"bool"
			},
			{
				typeof(float),
				"float"
			},
			{
				typeof(double),
				"double"
			},
			{
				typeof(decimal),
				"decimal"
			},
			{
				typeof(byte),
				"byte"
			},
			{
				typeof(sbyte),
				"sbyte"
			},
			{
				typeof(short),
				"short"
			},
			{
				typeof(ushort),
				"ushort"
			},
			{
				typeof(int),
				"int"
			},
			{
				typeof(uint),
				"uint"
			},
			{
				typeof(long),
				"long"
			},
			{
				typeof(ulong),
				"ulong"
			},
			{
				typeof(char),
				"char"
			},
			{
				typeof(string),
				"string"
			}
		};

		// Token: 0x0400173E RID: 5950
		private static Dictionary<Assembly, MonoModHooks.DetourList> assemblyDetours = new Dictionary<Assembly, MonoModHooks.DetourList>();

		// Token: 0x0400173F RID: 5951
		private static bool isInitialized;

		// Token: 0x04001740 RID: 5952
		private static ConcurrentDictionary<ValueTuple<MethodBase, Delegate>, IDisposable> _hookCache = new ConcurrentDictionary<ValueTuple<MethodBase, Delegate>, IDisposable>();

		// Token: 0x04001741 RID: 5953
		private const string HookAlreadyAppliedMsg = "Delegate has already been applied to this method as a hook!";

		// Token: 0x02000944 RID: 2372
		private class DetourList
		{
			// Token: 0x04006B2B RID: 27435
			public readonly List<DetourInfo> detours = new List<DetourInfo>();

			// Token: 0x04006B2C RID: 27436
			public readonly List<ILHookInfo> ilHooks = new List<ILHookInfo>();
		}
	}
}
