using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.CodeAnalysis.MSBuild;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using tModPorter;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000367 RID: 871
	internal class tModPorterLaunch
	{
		// Token: 0x0600307F RID: 12415 RVA: 0x0053CCA0 File Offset: 0x0053AEA0
		internal static void Launch(string[] args)
		{
			using (new Hook(typeof(MSBuildWorkspace).Assembly.GetType("Microsoft.CodeAnalysis.MSBuild.BuildHostProcessManager").GetMethod("CreateDotNetCoreBuildHostStartInfo", BindingFlags.Instance | BindingFlags.NonPublic), new Func<Func<object, ProcessStartInfo>, object, ProcessStartInfo>(delegate(Func<object, ProcessStartInfo> orig, object self)
			{
				ProcessStartInfo processStartInfo = orig(self);
				processStartInfo.FileName = Environment.ProcessPath;
				return processStartInfo;
			})))
			{
				using (new ILHook(typeof(MSBuildWorkspace).Assembly.GetType("Microsoft.CodeAnalysis.MSBuild.BuildHostProcessManager").GetMethod("GetNetCoreBuildHostPath", BindingFlags.Static | BindingFlags.NonPublic), delegate(ILContext il)
				{
					ILCursor ilcursor = new ILCursor(il);
					Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
					array[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdstr(i, "BuildHost-netcore"));
					ilcursor.GotoNext(array).Remove().EmitLdstr("../../BuildHost-netcore");
				}))
				{
					Program.Main(args).GetAwaiter().GetResult();
				}
			}
		}
	}
}
