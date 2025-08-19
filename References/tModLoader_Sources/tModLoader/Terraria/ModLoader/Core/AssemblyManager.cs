using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;
using log4net;
using Microsoft.Xna.Framework;
using MonoMod.RuntimeDetour;
using Terraria.Localization;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.UI;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000351 RID: 849
	public static class AssemblyManager
	{
		// Token: 0x06002F60 RID: 12128 RVA: 0x005340A8 File Offset: 0x005322A8
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void Unload()
		{
			foreach (AssemblyManager.ModLoadContext alc in AssemblyManager.loadedModContexts.Values)
			{
				AssemblyManager.oldLoadContexts.Add(new WeakReference<AssemblyLoadContext>(alc));
				alc.Unload();
			}
			AssemblyManager.loadedModContexts.Clear();
			for (int i = 0; i < 10; i++)
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x00534130 File Offset: 0x00532330
		internal static IEnumerable<string> OldLoadContexts()
		{
			using (List<WeakReference<AssemblyLoadContext>>.Enumerator enumerator = AssemblyManager.oldLoadContexts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AssemblyLoadContext alc;
					if (enumerator.Current.TryGetTarget(out alc))
					{
						yield return alc.Name;
					}
				}
			}
			List<WeakReference<AssemblyLoadContext>>.Enumerator enumerator = default(List<WeakReference<AssemblyLoadContext>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0053413C File Offset: 0x0053233C
		private static Mod Instantiate(AssemblyManager.ModLoadContext mod)
		{
			Mod result;
			try
			{
				Type modType;
				AssemblyManager.VerifyMod(mod.Name, mod.assembly, out modType);
				Mod mod2 = (Mod)Activator.CreateInstance(modType, true);
				mod2.File = mod.modFile;
				mod2.Code = mod.assembly;
				mod2.Logger = LogManager.GetLogger(mod2.Name);
				mod2.Side = mod.properties.side;
				mod2.DisplayName = mod.properties.displayName;
				mod2.TModLoaderVersion = mod.properties.buildVersion;
				mod2.TranslationForMods = (mod.properties.translationMod ? mod.properties.RefNames(true).ToList<string>() : null);
				mod2.SourceFolder = (Directory.Exists(mod.properties.modSource) ? mod.properties.modSource : "");
				result = mod2;
			}
			catch (Exception ex)
			{
				ex.Data["mod"] = mod.Name;
				throw;
			}
			finally
			{
				MemoryTracking.Update(mod.Name).code += mod.bytesLoaded;
			}
			return result;
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x0053426C File Offset: 0x0053246C
		private static void VerifyMod(string modName, Assembly assembly, out Type modType)
		{
			string asmName = new AssemblyName(assembly.FullName).Name;
			if (asmName != modName)
			{
				throw new Exception(Language.GetTextValue("tModLoader.BuildErrorModNameDoesntMatchAssemblyName", modName, asmName));
			}
			if (!AssemblyManager.GetLoadableTypes(assembly).Any(delegate(Type t)
			{
				string @namespace = t.Namespace;
				return @namespace != null && @namespace.StartsWith(modName);
			}))
			{
				throw new Exception(Language.GetTextValue("tModLoader.BuildErrorNamespaceFolderDontMatch"));
			}
			Type[] modTypes = (from t in AssemblyManager.GetLoadableTypes(assembly)
			where t.IsSubclassOf(typeof(Mod)) && !t.IsAbstract
			select t).ToArray<Type>();
			if (modTypes.Length > 1)
			{
				throw new Exception(modName + " has multiple classes extending Mod. Only one Mod per mod is supported at the moment");
			}
			modType = (modTypes.SingleOrDefault<Type>() ?? typeof(Mod));
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x00534348 File Offset: 0x00532548
		internal static List<Mod> InstantiateMods(List<LocalMod> modsToLoad, CancellationToken token)
		{
			List<AssemblyManager.ModLoadContext> modList = (from m in modsToLoad
			select new AssemblyManager.ModLoadContext(m)).ToList<AssemblyManager.ModLoadContext>();
			foreach (AssemblyManager.ModLoadContext mod4 in modList)
			{
				AssemblyManager.loadedModContexts.Add(mod4.Name, mod4);
			}
			foreach (AssemblyManager.ModLoadContext mod2 in modList)
			{
				foreach (string depName in mod2.properties.RefNames(true))
				{
					AssemblyManager.ModLoadContext dep;
					if (AssemblyManager.loadedModContexts.TryGetValue(depName, out dep))
					{
						mod2.AddDependency(dep);
					}
				}
			}
			if (Debugger.IsAttached)
			{
				ModCompile.activelyModding = true;
			}
			List<Mod> result;
			try
			{
				Interface.loadMods.SetLoadStage("tModLoader.MSSandboxing", modsToLoad.Count);
				int i = 0;
				foreach (AssemblyManager.ModLoadContext mod3 in modList)
				{
					token.ThrowIfCancellationRequested();
					UILoadMods loadMods = Interface.loadMods;
					int i2 = i++;
					string name = mod3.Name;
					BuildProperties properties = mod3.properties;
					loadMods.SetCurrentMod(i2, name, ((properties != null) ? properties.displayName : null) ?? "", mod3.modFile.Version);
					mod3.LoadAssemblies();
				}
				foreach (AssemblyManager.ModLoadContext modLoadContext in modList)
				{
					modLoadContext.ClearAssemblyBytes();
				}
				Interface.loadMods.SetLoadStage("tModLoader.MSInstantiating", -1);
				MemoryTracking.Checkpoint();
				result = modList.Select(delegate(AssemblyManager.ModLoadContext mod)
				{
					token.ThrowIfCancellationRequested();
					return AssemblyManager.Instantiate(mod);
				}).ToList<Mod>();
			}
			catch (AggregateException ae)
			{
				ae.Data["mods"] = (from e in ae.InnerExceptions
				select (string)e.Data["mod"]).ToArray<string>();
				throw;
			}
			return result;
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x005345D8 File Offset: 0x005327D8
		private static string GetModAssemblyFileName(this TmodFile modFile)
		{
			return modFile.Name + ".dll";
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x005345EA File Offset: 0x005327EA
		public static byte[] GetModAssembly(this TmodFile modFile)
		{
			return modFile.GetBytes(modFile.GetModAssemblyFileName());
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x005345F8 File Offset: 0x005327F8
		public static byte[] GetModPdb(this TmodFile modFile)
		{
			return modFile.GetBytes(Path.ChangeExtension(modFile.GetModAssemblyFileName(), "pdb"));
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x00534610 File Offset: 0x00532810
		private static AssemblyManager.ModLoadContext GetLoadContext(string name)
		{
			AssemblyManager.ModLoadContext value;
			if (!AssemblyManager.loadedModContexts.TryGetValue(name, out value))
			{
				throw new KeyNotFoundException(name);
			}
			return value;
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x00534634 File Offset: 0x00532834
		public static IEnumerable<Assembly> GetModAssemblies(string name)
		{
			return AssemblyManager.GetLoadContext(name).assemblies.Values;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x00534648 File Offset: 0x00532848
		public static bool GetAssemblyOwner(Assembly assembly, out string modName)
		{
			modName = null;
			AssemblyManager.ModLoadContext mlc = AssemblyLoadContext.GetLoadContext(assembly) as AssemblyManager.ModLoadContext;
			if (mlc == null)
			{
				return false;
			}
			modName = mlc.Name;
			if (AssemblyManager.loadedModContexts[modName] != mlc)
			{
				throw new Exception("Attempt to retrieve owner for mod assembly from a previous load");
			}
			return true;
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x0053468C File Offset: 0x0053288C
		internal static bool FirstModInStackTrace(StackTrace stack, out string modName)
		{
			for (int i = 0; i < stack.FrameCount; i++)
			{
				MethodBase method = stack.GetFrame(i).GetMethod();
				Assembly assembly2;
				if (method == null)
				{
					assembly2 = null;
				}
				else
				{
					Type declaringType = method.DeclaringType;
					assembly2 = ((declaringType != null) ? declaringType.Assembly : null);
				}
				Assembly assembly = assembly2;
				if (assembly != null && AssemblyManager.GetAssemblyOwner(assembly, out modName))
				{
					return true;
				}
			}
			modName = null;
			return false;
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x005346E8 File Offset: 0x005328E8
		public static IEnumerable<Mod> GetDependencies(Mod mod)
		{
			return from m in AssemblyManager.GetLoadContext(mod.Name).dependencies
			select ModLoader.GetMod(mod.Name);
		}

		/// <summary>
		/// Gets all <see cref="T:System.Type" />s loadable from the given <see cref="T:System.Reflection.Assembly" />.
		/// <para /> For a non-mod Assembly, will simply return <see cref="M:System.Reflection.Assembly.GetTypes" />.
		/// <para /> When used on a modded Assembly (using <see cref="P:Terraria.ModLoader.Mod.Code" />), this will return all the Types that are able to be loaded. This will specifically exclude Types that inherit from other mods using <see cref="T:Terraria.ModLoader.ExtendsFromModAttribute" />. Failure to use this method as a replacement for <see cref="M:System.Reflection.Assembly.GetTypes" /> to query the Types from other mods (for the purposes of autoloading content, for example) will result in exceptions.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		// Token: 0x06002F6D RID: 12141 RVA: 0x00534728 File Offset: 0x00532928
		public static Type[] GetLoadableTypes(Assembly assembly)
		{
			AssemblyManager.ModLoadContext mlc = AssemblyLoadContext.GetLoadContext(assembly) as AssemblyManager.ModLoadContext;
			if (mlc == null)
			{
				return assembly.GetTypes();
			}
			return mlc.loadableTypes[assembly];
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x00534758 File Offset: 0x00532958
		private static Dictionary<Assembly, Type[]> GetLoadableTypes(AssemblyManager.ModLoadContext mod, MetadataLoadContext mlc)
		{
			Dictionary<Assembly, Type[]> result;
			try
			{
				Func<Type, bool> <>9__2;
				result = mod.Assemblies.ToDictionary((Assembly a) => a, delegate(Assembly asm)
				{
					IEnumerable<Type> types = mlc.LoadFromAssemblyName(asm.GetName()).GetTypes();
					Func<Type, bool> predicate;
					if ((predicate = <>9__2) == null)
					{
						predicate = (<>9__2 = ((Type mType) => AssemblyManager.IsLoadable(mod, mType)));
					}
					return (from mType in types.Where(predicate)
					select AssemblyManager.GetType(asm, mType)).ToArray<Type>();
				});
			}
			catch (Exception e)
			{
				Type type = (Type)e.Data["type"];
				if (type != null)
				{
					MethodInfo[] methods = type.GetMethods();
					int i = 0;
					while (i < methods.Length)
					{
						MethodInfo method = methods[i];
						if (method.IsAbstract && method.DeclaringType != null && method.DeclaringType != type)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
							if (method.DeclaringType.Assembly.FullName == Assembly.GetExecutingAssembly().FullName)
							{
								string str = "This mod seems to contain a class which inherits from a tModLoader class but does not implement required abstract methods. Use tModPorter to update required methods.\n\n";
								defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 2);
								defaultInterpolatedStringHandler.AppendLiteral("The method \"");
								defaultInterpolatedStringHandler.AppendFormatted(method.Name);
								defaultInterpolatedStringHandler.AppendLiteral("\" in the class \"");
								defaultInterpolatedStringHandler.AppendFormatted(type.FullName);
								defaultInterpolatedStringHandler.AppendLiteral("\" caused this error.\n\n");
								throw new Exception(str + defaultInterpolatedStringHandler.ToStringAndClear() + e.Message, e);
							}
							string str2 = "This mod seems to contain a class which inherits from a class in another mod but does not implement required abstract methods.\n\n";
							defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 2);
							defaultInterpolatedStringHandler.AppendLiteral("The method \"");
							defaultInterpolatedStringHandler.AppendFormatted(method.Name);
							defaultInterpolatedStringHandler.AppendLiteral("\" in the class \"");
							defaultInterpolatedStringHandler.AppendFormatted(type.FullName);
							defaultInterpolatedStringHandler.AppendLiteral("\" caused this error.\n\n");
							throw new Exception(str2 + defaultInterpolatedStringHandler.ToStringAndClear() + e.Message, e);
						}
						else
						{
							i++;
						}
					}
				}
				throw new GetLoadableTypesException("This mod seems to inherit from classes in another mod. Use the [ExtendsFromMod] attribute to allow this mod to load when that mod is not enabled.\n\n" + ((type != null) ? ("The \"" + type.FullName + "\" class caused this error.\n\n") : "") + e.Message, e);
			}
			return result;
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x00534980 File Offset: 0x00532B80
		private static bool IsLoadable(AssemblyManager.ModLoadContext mod, Type type)
		{
			bool result;
			try
			{
				Func<CustomAttributeTypedArgument, bool> <>9__1;
				foreach (CustomAttributeData attr in type.GetCustomAttributesData())
				{
					if (attr.AttributeType.AssemblyQualifiedName == typeof(ExtendsFromModAttribute).AssemblyQualifiedName)
					{
						IEnumerable<CustomAttributeTypedArgument> source = (IEnumerable<CustomAttributeTypedArgument>)attr.ConstructorArguments[0].Value;
						Func<CustomAttributeTypedArgument, bool> predicate;
						if ((predicate = <>9__1) == null)
						{
							predicate = (<>9__1 = ((CustomAttributeTypedArgument v) => mod.IsModDependencyPresent((string)v.Value)));
						}
						if (!source.All(predicate))
						{
							return false;
						}
					}
				}
				if (type.BaseType != null && !AssemblyManager.IsLoadable(mod, type.BaseType))
				{
					result = false;
				}
				else if (type.DeclaringType != null && !AssemblyManager.IsLoadable(mod, type.DeclaringType))
				{
					result = false;
				}
				else
				{
					result = type.GetInterfaces().All((Type i) => AssemblyManager.IsLoadable(mod, i));
				}
			}
			catch (FileNotFoundException ex)
			{
				ex.Data["type"] = type;
				throw;
			}
			return result;
		}

		/// <summary>
		/// Gets and validates the <see cref="T:System.Type" /> from the given <see cref="T:System.Reflection.Assembly" />.
		/// </summary>
		/// <param name="assembly">Assembly to load type from</param>
		/// <param name="type">Target type to get</param>
		/// <returns></returns>
		// Token: 0x06002F70 RID: 12144 RVA: 0x00534AC4 File Offset: 0x00532CC4
		[NullableContext(1)]
		[return: Nullable(2)]
		private static Type GetType(Assembly assembly, Type type)
		{
			Type type2;
			try
			{
				type2 = assembly.GetType(type.FullName, true, false);
			}
			catch (TypeLoadException ex)
			{
				ex.Data["type"] = type;
				throw;
			}
			return type2;
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x00534B08 File Offset: 0x00532D08
		internal static Task JITModAsync(Mod mod, CancellationToken token)
		{
			return AssemblyManager.JITAssembliesAsync(AssemblyManager.GetModAssemblies(mod.Name), mod.PreJITFilter, token);
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x00534B21 File Offset: 0x00532D21
		internal static void JITMod(Mod mod)
		{
			AssemblyManager.JITAssemblies(AssemblyManager.GetModAssemblies(mod.Name), mod.PreJITFilter);
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x00534B3C File Offset: 0x00532D3C
		public static void JITAssemblies(IEnumerable<Assembly> assemblies, PreJITFilter filter)
		{
			AssemblyManager.JITAssembliesAsync(assemblies, filter, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x00534B64 File Offset: 0x00532D64
		public static Task JITAssembliesAsync(IEnumerable<Assembly> assemblies, PreJITFilter filter, CancellationToken token)
		{
			AssemblyManager.<JITAssembliesAsync>d__24 <JITAssembliesAsync>d__;
			<JITAssembliesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<JITAssembliesAsync>d__.assemblies = assemblies;
			<JITAssembliesAsync>d__.filter = filter;
			<JITAssembliesAsync>d__.token = token;
			<JITAssembliesAsync>d__.<>1__state = -1;
			<JITAssembliesAsync>d__.<>t__builder.Start<AssemblyManager.<JITAssembliesAsync>d__24>(ref <JITAssembliesAsync>d__);
			return <JITAssembliesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x00534BB8 File Offset: 0x00532DB8
		private static Task ForEachAsync<T>(IEnumerable<T> elements, Action<T> action, CancellationToken token)
		{
			AssemblyManager.<ForEachAsync>d__25<T> <ForEachAsync>d__;
			<ForEachAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ForEachAsync>d__.elements = elements;
			<ForEachAsync>d__.action = action;
			<ForEachAsync>d__.token = token;
			<ForEachAsync>d__.<>1__state = -1;
			<ForEachAsync>d__.<>t__builder.Start<AssemblyManager.<ForEachAsync>d__25<T>>(ref <ForEachAsync>d__);
			return <ForEachAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x00534C0C File Offset: 0x00532E0C
		private static void ForceJITOnMethod(MethodBase method)
		{
			if (method.GetMethodBody() != null)
			{
				RuntimeHelpers.PrepareMethod(method.MethodHandle);
			}
			MethodInfo methodInfo = method as MethodInfo;
			if (methodInfo != null)
			{
				bool isNewSlot = (methodInfo.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.VtableLayoutMask;
				if (methodInfo.IsVirtual && !isNewSlot)
				{
					Type declaringType = methodInfo.DeclaringType;
					if (!(((declaringType != null) ? new bool?(declaringType.IsInterface) : null) ?? true) && methodInfo.GetBaseDefinition() == methodInfo)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(57, 1);
						defaultInterpolatedStringHandler.AppendFormatted<MethodBase>(method);
						defaultInterpolatedStringHandler.AppendLiteral(" overrides a method which doesn't exist in any base class");
						throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
					}
				}
			}
		}

		/// <summary>
		/// Searches the assembly dependency tree for a subtype of <paramref name="parentType" /> with <see cref="P:System.Type.FullName" /> = <paramref name="name" />
		/// </summary>
		// Token: 0x06002F77 RID: 12151 RVA: 0x00534CC4 File Offset: 0x00532EC4
		public static Type FindSubtype(Type parentType, string name)
		{
			return (from t in AssemblyManager.FindTypes(parentType.Assembly, name)
			where t.IsAssignableTo(parentType)
			select t).FirstOrDefault<Type>();
		}

		/// <summary>
		/// Searches the assembly dependency tree of <paramref name="referencedAssembly" /> for a type with <see cref="P:System.Type.FullName" /> = <paramref name="name" />
		/// </summary>
		// Token: 0x06002F78 RID: 12152 RVA: 0x00534D05 File Offset: 0x00532F05
		public static IEnumerable<Type> FindTypes(Assembly referencedAssembly, string name)
		{
			Type type = referencedAssembly.GetType(name);
			if (type != null)
			{
				yield return type;
			}
			foreach (Assembly assembly in AssemblyManager.EnumerateDependents(referencedAssembly))
			{
				Type type2 = assembly.GetType(name);
				if (type2 != null)
				{
					yield return type2;
				}
			}
			IEnumerator<Assembly> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x00534D1C File Offset: 0x00532F1C
		private static IEnumerable<Assembly> EnumerateDependents(Assembly referencedAssembly)
		{
			AssemblyLoadContext hostAlc = AssemblyLoadContext.GetLoadContext(referencedAssembly);
			IEnumerable<Assembly> deps = AssemblyManager.GetDependents(hostAlc).SelectMany((AssemblyLoadContext alc) => alc.Assemblies);
			Assembly root = AssemblyManager.GetRoot(hostAlc);
			if (referencedAssembly != root)
			{
				deps = deps.Concat(from a in hostAlc.Assemblies
				where a != referencedAssembly
				select a);
			}
			return deps;
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x00534DA4 File Offset: 0x00532FA4
		private static Assembly GetRoot(AssemblyLoadContext alc)
		{
			AssemblyManager.ModLoadContext mlc = alc as AssemblyManager.ModLoadContext;
			Assembly result;
			if (mlc != null)
			{
				result = mlc.assembly;
			}
			else
			{
				if (alc != AssemblyManager.tMLAlc)
				{
					throw new NotSupportedException();
				}
				result = Assembly.GetExecutingAssembly();
			}
			return result;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x00534DDC File Offset: 0x00532FDC
		private static IEnumerable<AssemblyLoadContext> GetDependents(AssemblyLoadContext alc)
		{
			AssemblyManager.ModLoadContext mlc = alc as AssemblyManager.ModLoadContext;
			IEnumerable<AssemblyLoadContext> result;
			if (mlc != null)
			{
				AssemblyManager.ModLoadContext modLoadContext = mlc;
				List<AssemblyManager.ModLoadContext> list;
				if ((list = modLoadContext.dependents) == null)
				{
					list = (modLoadContext.dependents = new TopoSort<AssemblyManager.ModLoadContext>(AssemblyManager.loadedModContexts.Values, (AssemblyManager.ModLoadContext c) => c.dependencies, null).AllDependendents(mlc).ToList<AssemblyManager.ModLoadContext>());
				}
				result = list;
			}
			else
			{
				if (alc != AssemblyManager.tMLAlc)
				{
					throw new NotSupportedException();
				}
				result = AssemblyManager.loadedModContexts.Values;
			}
			return result;
		}

		// Token: 0x04001C97 RID: 7319
		private static readonly List<WeakReference<AssemblyLoadContext>> oldLoadContexts = new List<WeakReference<AssemblyLoadContext>>();

		// Token: 0x04001C98 RID: 7320
		private static readonly Dictionary<string, AssemblyManager.ModLoadContext> loadedModContexts = new Dictionary<string, AssemblyManager.ModLoadContext>();

		// Token: 0x04001C99 RID: 7321
		private static AssemblyLoadContext tMLAlc = AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly());

		// Token: 0x02000A8B RID: 2699
		private class ModLoadContext : AssemblyLoadContext
		{
			// Token: 0x06005956 RID: 22870 RVA: 0x006A13EC File Offset: 0x0069F5EC
			public ModLoadContext(LocalMod mod) : base(mod.Name, true)
			{
				this.modFile = mod.modFile;
				this.properties = mod.properties;
				base.Unloading += this.ModLoadContext_Unloading;
			}

			// Token: 0x06005957 RID: 22871 RVA: 0x006A1451 File Offset: 0x0069F651
			private void ModLoadContext_Unloading(AssemblyLoadContext obj)
			{
				this.dependencies = null;
				this.dependents = null;
				this.assembly = null;
				this.assemblies = null;
				this.loadableTypes = null;
			}

			// Token: 0x06005958 RID: 22872 RVA: 0x006A1476 File Offset: 0x0069F676
			public void AddDependency(AssemblyManager.ModLoadContext dep)
			{
				this.dependencies.Add(dep);
			}

			// Token: 0x06005959 RID: 22873 RVA: 0x006A1484 File Offset: 0x0069F684
			public void LoadAssemblies()
			{
				try
				{
					using (this.modFile.Open())
					{
						foreach (string dll in this.properties.dllReferences)
						{
							this.LoadAssembly(this.modFile.GetBytes("lib/" + dll + ".dll"), null);
						}
						this.assembly = ((Debugger.IsAttached && File.Exists(this.properties.eacPath)) ? this.LoadAssembly(this.modFile.GetModAssembly(), File.ReadAllBytes(this.properties.eacPath)) : this.LoadAssembly(this.modFile.GetModAssembly(), this.modFile.GetModPdb()));
					}
					MetadataLoadContext mlc = new MetadataLoadContext(new AssemblyManager.ModLoadContext.MetadataResolver(this), null);
					this.loadableTypes = AssemblyManager.GetLoadableTypes(this, mlc);
				}
				catch (Exception ex)
				{
					ex.Data["mod"] = base.Name;
					throw;
				}
			}

			// Token: 0x0600595A RID: 22874 RVA: 0x006A159C File Offset: 0x0069F79C
			private Assembly LoadAssembly(byte[] code, byte[] pdb = null)
			{
				Assembly result;
				using (MemoryStream codeStrm = new MemoryStream(code, false))
				{
					using (MemoryStream pdbStrm = (pdb == null) ? null : new MemoryStream(pdb, false))
					{
						Assembly asm = base.LoadFromStream(codeStrm, pdbStrm);
						string name = asm.GetName().Name;
						this.assemblyBytes[name] = code;
						this.assemblies[name] = asm;
						this.bytesLoaded += (long)code.Length + ((pdb != null) ? ((long)pdb.Length) : 0L);
						if (Program.LaunchParameters.ContainsKey("-dumpasm"))
						{
							string dumpdir = Path.Combine(Main.SavePath, "asmdump");
							Directory.CreateDirectory(dumpdir);
							File.WriteAllBytes(Path.Combine(dumpdir, asm.FullName + ".dll"), code);
							if (pdb != null)
							{
								File.WriteAllBytes(Path.Combine(dumpdir, asm.FullName + ".pdb"), code);
							}
						}
						result = asm;
					}
				}
				return result;
			}

			// Token: 0x0600595B RID: 22875 RVA: 0x006A16A8 File Offset: 0x0069F8A8
			protected override Assembly Load(AssemblyName assemblyName)
			{
				Assembly redirected = AssemblyManager.AssemblyRedirects.GetAssembly(assemblyName.Name);
				if (redirected != null)
				{
					return redirected;
				}
				Assembly asm;
				if (this.assemblies.TryGetValue(assemblyName.Name, out asm))
				{
					return asm;
				}
				return (from dep in this.dependencies
				select dep.Load(assemblyName)).FirstOrDefault((Assembly a) => a != null);
			}

			// Token: 0x0600595C RID: 22876 RVA: 0x006A1730 File Offset: 0x0069F930
			internal bool IsModDependencyPresent(string name)
			{
				return name == base.Name || this.dependencies.Any((AssemblyManager.ModLoadContext d) => d.IsModDependencyPresent(name));
			}

			// Token: 0x0600595D RID: 22877 RVA: 0x006A1776 File Offset: 0x0069F976
			internal void ClearAssemblyBytes()
			{
				this.assemblyBytes.Clear();
			}

			// Token: 0x04006D5F RID: 27999
			public readonly TmodFile modFile;

			// Token: 0x04006D60 RID: 28000
			public readonly BuildProperties properties;

			// Token: 0x04006D61 RID: 28001
			public List<AssemblyManager.ModLoadContext> dependencies = new List<AssemblyManager.ModLoadContext>();

			// Token: 0x04006D62 RID: 28002
			public List<AssemblyManager.ModLoadContext> dependents;

			// Token: 0x04006D63 RID: 28003
			public Assembly assembly;

			// Token: 0x04006D64 RID: 28004
			public Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

			// Token: 0x04006D65 RID: 28005
			public Dictionary<string, byte[]> assemblyBytes = new Dictionary<string, byte[]>();

			// Token: 0x04006D66 RID: 28006
			public Dictionary<Assembly, Type[]> loadableTypes;

			// Token: 0x04006D67 RID: 28007
			public long bytesLoaded;

			// Token: 0x02000E29 RID: 3625
			private class MetadataResolver : MetadataAssemblyResolver
			{
				// Token: 0x06006568 RID: 25960 RVA: 0x006DF1E0 File Offset: 0x006DD3E0
				public MetadataResolver(AssemblyManager.ModLoadContext mod)
				{
					this.mod = mod;
				}

				// Token: 0x06006569 RID: 25961 RVA: 0x006DF1F0 File Offset: 0x006DD3F0
				public override Assembly Resolve(MetadataLoadContext context, AssemblyName assemblyName)
				{
					Assembly existing = context.GetAssemblies().SingleOrDefault((Assembly a) => a.GetName().FullName == assemblyName.FullName);
					if (existing != null)
					{
						return existing;
					}
					Assembly runtime = this.mod.LoadFromAssemblyName(assemblyName);
					if (string.IsNullOrEmpty(runtime.Location))
					{
						return context.LoadFromByteArray(((AssemblyManager.ModLoadContext)AssemblyLoadContext.GetLoadContext(runtime)).assemblyBytes[assemblyName.Name]);
					}
					return context.LoadFromAssemblyPath(runtime.Location);
				}

				// Token: 0x04007C5D RID: 31837
				private readonly AssemblyManager.ModLoadContext mod;
			}
		}

		// Token: 0x02000A8C RID: 2700
		private static class AssemblyRedirects
		{
			// Token: 0x0600595E RID: 22878 RVA: 0x006A1784 File Offset: 0x0069F984
			private static Assembly hook_ValidateAssemblyNameWithSimpleName(AssemblyManager.AssemblyRedirects.orig_ValidateAssemblyNameWithSimpleName orig, Assembly assembly, [Nullable(2)] string requestedSimpleName)
			{
				Assembly redirect;
				if (AssemblyManager.AssemblyRedirects._redirects.TryGetValue(requestedSimpleName, out redirect) && assembly == redirect)
				{
					return assembly;
				}
				return orig(assembly, requestedSimpleName);
			}

			// Token: 0x0600595F RID: 22879 RVA: 0x006A17B4 File Offset: 0x0069F9B4
			public static Assembly GetAssembly(string name)
			{
				Assembly asm;
				if (!AssemblyManager.AssemblyRedirects._redirects.TryGetValue(name, out asm))
				{
					return null;
				}
				return asm;
			}

			// Token: 0x06005960 RID: 22880 RVA: 0x006A17D4 File Offset: 0x0069F9D4
			// Note: this type is marked as 'beforefieldinit'.
			static AssemblyRedirects()
			{
				Dictionary<string, Assembly> dictionary = new Dictionary<string, Assembly>();
				dictionary["tModLoader"] = Assembly.GetExecutingAssembly();
				dictionary["FNA"] = typeof(Vector2).Assembly;
				dictionary["Ionic.Zip.Reduced"] = typeof(ZipFile).Assembly;
				AssemblyManager.AssemblyRedirects._redirects = dictionary;
				AssemblyManager.AssemblyRedirects._hook = new Hook(typeof(AssemblyLoadContext).GetMethod("ValidateAssemblyNameWithSimpleName", BindingFlags.Static | BindingFlags.NonPublic), new Func<AssemblyManager.AssemblyRedirects.orig_ValidateAssemblyNameWithSimpleName, Assembly, string, Assembly>(AssemblyManager.AssemblyRedirects.hook_ValidateAssemblyNameWithSimpleName));
			}

			// Token: 0x04006D68 RID: 28008
			private static Dictionary<string, Assembly> _redirects;

			// Token: 0x04006D69 RID: 28009
			private static Hook _hook;

			// Token: 0x02000E2D RID: 3629
			// (Invoke) Token: 0x06006572 RID: 25970
			private delegate Assembly orig_ValidateAssemblyNameWithSimpleName(Assembly assembly, [Nullable(2)] string requestedSimpleName);
		}

		// Token: 0x02000A8D RID: 2701
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006D6A RID: 28010
			[Nullable(new byte[]
			{
				0,
				0,
				1,
				0
			})]
			public static Func<Assembly, IEnumerable<Type>> <0>__GetLoadableTypes;
		}
	}
}
