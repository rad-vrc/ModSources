using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using log4net;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.Localization;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config
{
	// Token: 0x02000386 RID: 902
	[NullableContext(1)]
	[Nullable(0)]
	public static class ConfigManager
	{
		// Token: 0x060030DE RID: 12510 RVA: 0x0053D5B8 File Offset: 0x0053B7B8
		static ConfigManager()
		{
			TypeCaching.OnClear += delegate()
			{
				ConfigManager.typesWithLocalizationRegistered.Clear();
			};
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x0053D68C File Offset: 0x0053B88C
		internal static void Add(ModConfig config)
		{
			ConfigManager.Load(config);
			List<ModConfig> configList;
			if (!ConfigManager.Configs.TryGetValue(config.Mod, out configList))
			{
				configList = (ConfigManager.Configs[config.Mod] = new List<ModConfig>());
			}
			configList.Add(config);
			FieldInfo field = config.GetType().GetField("Instance", BindingFlags.Static | BindingFlags.Public);
			if (field != null)
			{
				field.SetValue(null, config);
			}
			config.OnLoaded();
			config.OnChanged();
			List<ModConfig> loadTimeConfigList;
			if (!ConfigManager.loadTimeConfigs.TryGetValue(config.Mod, out loadTimeConfigList))
			{
				loadTimeConfigList = (ConfigManager.loadTimeConfigs[config.Mod] = new List<ModConfig>());
			}
			loadTimeConfigList.Add(ConfigManager.GeneratePopulatedClone(config));
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0053D734 File Offset: 0x0053B934
		internal static void FinishSetup()
		{
			foreach (KeyValuePair<Mod, List<ModConfig>> activeConfigs in ConfigManager.Configs)
			{
				foreach (ModConfig config in activeConfigs.Value)
				{
					try
					{
						LocalizedText displayName = config.DisplayName;
						ConfigManager.RegisterLocalizationKeysForMembers(config.GetType());
					}
					catch (Exception ex)
					{
						ex.Data["mod"] = config.Mod.Name;
						throw;
					}
				}
			}
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x0053D7F4 File Offset: 0x0053B9F4
		private static void RegisterLocalizationKeysForMembers(Type type)
		{
			string modName;
			AssemblyManager.GetAssemblyOwner(type.Assembly, out modName);
			using (IEnumerator<PropertyFieldWrapper> enumerator = ConfigManager.GetFieldsAndProperties(type).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PropertyFieldWrapper variable = enumerator.Current;
					LabelAttribute labelObsolete = ConfigManager.GetLegacyLabelAttribute(variable.MemberInfo);
					TooltipAttribute tooltipObsolete = ConfigManager.GetLegacyTooltipAttribute(variable.MemberInfo);
					if (!Attribute.IsDefined(variable.MemberInfo, typeof(JsonIgnoreAttribute)) || labelObsolete != null || Attribute.IsDefined(variable.MemberInfo, typeof(ShowDespiteJsonIgnoreAttribute)))
					{
						ConfigManager.RegisterLocalizationKeysForMemberType(variable.Type, type.Assembly);
						HeaderAttribute header = ConfigManager.GetLocalizedHeader(variable.MemberInfo);
						if (header != null)
						{
							string identifier = header.IsIdentifier ? header.identifier : variable.Name;
							Language.GetOrRegister(header.key, () => Regex.Replace(identifier, "([A-Z])", " $1").Trim() + " Header");
						}
						Language.GetOrRegister(ConfigManager.GetConfigKey<LabelKeyAttribute>(variable.MemberInfo, "Label"), delegate()
						{
							LabelAttribute labelObsolete = labelObsolete;
							return ((labelObsolete != null) ? labelObsolete.LocalizationEntry : null) ?? Regex.Replace(variable.Name, "([A-Z])", " $1").Trim();
						});
						Language.GetOrRegister(ConfigManager.GetConfigKey<TooltipKeyAttribute>(variable.MemberInfo, "Tooltip"), delegate()
						{
							TooltipAttribute tooltipObsolete = tooltipObsolete;
							return ((tooltipObsolete != null) ? tooltipObsolete.LocalizationEntry : null) ?? "";
						});
					}
				}
			}
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x0053D990 File Offset: 0x0053BB90
		private static void RegisterLocalizationKeysForEnumMembers(Type type)
		{
			FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			{
				FieldInfo field = fields[i];
				LabelAttribute labelObsolete = ConfigManager.GetLegacyLabelAttribute(field);
				Language.GetOrRegister(ConfigManager.GetConfigKey<LabelKeyAttribute>(field, "Label"), delegate()
				{
					LabelAttribute labelObsolete = labelObsolete;
					return ((labelObsolete != null) ? labelObsolete.LocalizationEntry : null) ?? Regex.Replace(field.Name, "([A-Z])", " $1").Trim();
				});
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x0053D9F8 File Offset: 0x0053BBF8
		private static void RegisterLocalizationKeysForMemberType(Type type, Assembly owningAssembly)
		{
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					ConfigManager.RegisterLocalizationKeysForMemberType(genericArguments[i], owningAssembly);
				}
			}
			if ((type.IsClass || type.IsEnum) && type.Assembly == owningAssembly && ConfigManager.typesWithLocalizationRegistered.Add(type))
			{
				Language.GetOrRegister(ConfigManager.GetConfigKey<TooltipKeyAttribute>(type, "Tooltip"), () => "");
				if (type.IsClass)
				{
					ConfigManager.RegisterLocalizationKeysForMembers(type);
					return;
				}
				ConfigManager.RegisterLocalizationKeysForEnumMembers(type);
			}
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x0053DA9B File Offset: 0x0053BC9B
		[return: Nullable(2)]
		internal static LabelAttribute GetLegacyLabelAttribute(MemberInfo memberInfo)
		{
			return (LabelAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(LabelAttribute));
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x0053DAB2 File Offset: 0x0053BCB2
		[return: Nullable(2)]
		internal static TooltipAttribute GetLegacyTooltipAttribute(MemberInfo memberInfo)
		{
			return (TooltipAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(TooltipAttribute));
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x0053DACC File Offset: 0x0053BCCC
		internal static void LoadAll()
		{
			foreach (KeyValuePair<Mod, List<ModConfig>> activeConfigs in ConfigManager.Configs)
			{
				foreach (ModConfig config in activeConfigs.Value)
				{
					ConfigManager.Load(config);
				}
			}
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x0053DB54 File Offset: 0x0053BD54
		internal static void OnChangedAll()
		{
			foreach (KeyValuePair<Mod, List<ModConfig>> activeConfigs in ConfigManager.Configs)
			{
				foreach (ModConfig modConfig in activeConfigs.Value)
				{
					modConfig.OnChanged();
				}
			}
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x0053DBDC File Offset: 0x0053BDDC
		internal static void Load(ModConfig config)
		{
			string filename = config.Mod.Name + "_" + config.Name + ".json";
			string path = Path.Combine(ConfigManager.ModConfigPath, filename);
			if (config.Mode == ConfigScope.ServerSide && ModNet.NetReloadActive)
			{
				JsonConvert.PopulateObject(ModNet.pendingConfigs.Single((ModNet.NetConfig x) => x.modname == config.Mod.Name && x.configname == config.Name).json, config, ConfigManager.serializerSettingsCompact);
				return;
			}
			bool jsonFileExists = File.Exists(path);
			string json = jsonFileExists ? File.ReadAllText(path) : "{}";
			try
			{
				JsonConvert.PopulateObject(json, config, ConfigManager.serializerSettings);
			}
			catch (Exception e) when (jsonFileExists && (e is JsonReaderException || e is JsonSerializationException))
			{
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(151, 3);
				defaultInterpolatedStringHandler.AppendLiteral("Then config file ");
				defaultInterpolatedStringHandler.AppendFormatted(config.Name);
				defaultInterpolatedStringHandler.AppendLiteral(" from the mod ");
				defaultInterpolatedStringHandler.AppendFormatted(config.Mod.Name);
				defaultInterpolatedStringHandler.AppendLiteral(" located at ");
				defaultInterpolatedStringHandler.AppendFormatted(path);
				defaultInterpolatedStringHandler.AppendLiteral(" failed to load. The file was likely corrupted somehow, so the defaults will be loaded and the file deleted.");
				tML.Warn(defaultInterpolatedStringHandler.ToStringAndClear());
				File.Delete(path);
				JsonConvert.PopulateObject("{}", config, ConfigManager.serializerSettings);
			}
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x0053DD7C File Offset: 0x0053BF7C
		internal static void Reset(ModConfig pendingConfig)
		{
			JsonConvert.PopulateObject("{}", pendingConfig, ConfigManager.serializerSettings);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x0053DD90 File Offset: 0x0053BF90
		internal static void Save(ModConfig config)
		{
			Directory.CreateDirectory(ConfigManager.ModConfigPath);
			string filename = config.Mod.Name + "_" + config.Name + ".json";
			string path = Path.Combine(ConfigManager.ModConfigPath, filename);
			string json = JsonConvert.SerializeObject(config, ConfigManager.serializerSettings);
			File.WriteAllText(path, json);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x0053DDE8 File Offset: 0x0053BFE8
		internal static void Unload()
		{
			ConfigManager.serializerSettings.ContractResolver = new ReferenceDefaultsPreservingResolver();
			ConfigManager.serializerSettingsCompact.ContractResolver = ConfigManager.serializerSettings.ContractResolver;
			ConfigManager.Configs.SelectMany((KeyValuePair<Mod, List<ModConfig>> configList) => configList.Value).ToList<ModConfig>().ForEach(delegate(ModConfig config)
			{
				FieldInfo instance = config.GetType().GetField("Instance", BindingFlags.Static | BindingFlags.Public);
				if (instance != null)
				{
					instance.SetValue(null, null);
				}
			});
			ConfigManager.Configs.Clear();
			ConfigManager.loadTimeConfigs.Clear();
			Interface.modConfig.Unload();
			Interface.modConfigList.Unload();
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x0053DE92 File Offset: 0x0053C092
		internal static bool AnyModNeedsReload()
		{
			IEnumerable<Mod> mods = ModLoader.Mods;
			Func<Mod, bool> predicate;
			if ((predicate = ConfigManager.<>O.<0>__ModNeedsReload) == null)
			{
				predicate = (ConfigManager.<>O.<0>__ModNeedsReload = new Func<Mod, bool>(ConfigManager.ModNeedsReload));
			}
			return mods.Any(predicate);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x0053DEBC File Offset: 0x0053C0BC
		internal static bool ModNeedsReload(Mod mod)
		{
			if (ConfigManager.Configs.ContainsKey(mod))
			{
				List<ModConfig> configs = ConfigManager.Configs[mod];
				List<ModConfig> loadTimeConfigs = ConfigManager.loadTimeConfigs[mod];
				for (int i = 0; i < configs.Count; i++)
				{
					if (loadTimeConfigs[i].NeedsReload(configs[i]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x0053DF18 File Offset: 0x0053C118
		internal static bool AnyModNeedsReloadCheckOnly(out List<Mod> modsWithChangedConfigs)
		{
			modsWithChangedConfigs = new List<Mod>();
			bool result = false;
			foreach (Mod mod in ModLoader.Mods)
			{
				if (ConfigManager.Configs.ContainsKey(mod))
				{
					List<ModConfig> configs = ConfigManager.Configs[mod];
					List<ModConfig> loadTimeConfigs = ConfigManager.loadTimeConfigs[mod];
					for (int i = 0; i < configs.Count; i++)
					{
						ModConfig configClone = ConfigManager.GeneratePopulatedClone(configs[i]);
						ConfigManager.Load(configClone);
						if (loadTimeConfigs[i].NeedsReload(configClone))
						{
							result = true;
							modsWithChangedConfigs.Add(mod);
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x0053DFB9 File Offset: 0x0053C1B9
		internal static ModConfig GetConfig(ModNet.NetConfig netConfig)
		{
			return ConfigManager.GetConfig(ModLoader.GetMod(netConfig.modname), netConfig.configname);
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x0053DFD4 File Offset: 0x0053C1D4
		internal static ModConfig GetConfig(Mod mod, string config)
		{
			List<ModConfig> configs;
			if (ConfigManager.Configs.TryGetValue(mod, out configs))
			{
				return configs.Single((ModConfig x) => x.Name == config);
			}
			throw new MissingResourceException("Missing config named " + config + " in mod " + mod.Name);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x0053E030 File Offset: 0x0053C230
		internal static ModConfig GetLoadTimeConfig(Mod mod, string config)
		{
			List<ModConfig> configs;
			if (ConfigManager.loadTimeConfigs.TryGetValue(mod, out configs))
			{
				return configs.Single((ModConfig x) => x.Name == config);
			}
			throw new MissingResourceException("Missing config named " + config + " in mod " + mod.Name);
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x0053E08C File Offset: 0x0053C28C
		internal static void HandleInGameChangeConfigPacket(BinaryReader reader, int whoAmI)
		{
			if (Main.netMode == 1)
			{
				bool success = reader.ReadBoolean();
				NetworkText message = NetworkText.Deserialize(reader);
				string modname = reader.ReadString();
				string configname = reader.ReadString();
				bool broadcast = reader.ReadBoolean();
				ModConfig activeConfig = ConfigManager.GetConfig(ModLoader.GetMod(modname), configname);
				int requestor = (int)reader.ReadByte();
				if (success)
				{
					JsonConvert.PopulateObject(reader.ReadString(), activeConfig, ConfigManager.serializerSettingsCompact);
					activeConfig.OnChanged();
					activeConfig.HandleAcceptClientChangesReply(success, requestor, message);
					if (broadcast)
					{
						Main.NewText(Language.GetTextValue("tModLoader.ModConfigSharedConfigChanged", message, modname, configname), byte.MaxValue, byte.MaxValue, byte.MaxValue);
					}
					if (Main.InGameUI.CurrentState == Interface.modConfig)
					{
						Main.InGameUI.SetState(null);
						Main.InGameUI.SetState(Interface.modConfig);
						Interface.modConfig.SetMessage(Language.GetTextValue("tModLoader.ModConfigServerResponse", message), Color.Green);
						return;
					}
				}
				else
				{
					activeConfig.HandleAcceptClientChangesReply(success, requestor, message);
					if (broadcast)
					{
						Main.NewText(Language.GetTextValue("tModLoader.ModConfigServerRejectedChanges", message), byte.MaxValue, byte.MaxValue, byte.MaxValue);
					}
					if (Main.InGameUI.CurrentState == Interface.modConfig)
					{
						Interface.modConfig.SetMessage(Language.GetTextValue("tModLoader.ModConfigServerRejectedChanges", message), Color.Red);
						return;
					}
				}
			}
			else
			{
				string modname2 = reader.ReadString();
				string configname2 = reader.ReadString();
				bool broadcast2 = reader.ReadBoolean();
				string json = reader.ReadString();
				Mod mod = ModLoader.GetMod(modname2);
				ModConfig config = ConfigManager.GetConfig(mod, configname2);
				ModConfig loadTimeConfig = ConfigManager.GetLoadTimeConfig(mod, configname2);
				ModConfig pendingConfig = ConfigManager.GeneratePopulatedClone(config);
				JsonConvert.PopulateObject(json, pendingConfig, ConfigManager.serializerSettingsCompact);
				bool success2 = true;
				NetworkText message2 = NetworkText.FromKey("tModLoader.ModConfigAccepted", Array.Empty<object>());
				if (loadTimeConfig.NeedsReload(pendingConfig))
				{
					success2 = false;
					message2 = NetworkText.FromKey("tModLoader.ModConfigCantSaveBecauseChangesWouldRequireAReload", Array.Empty<object>());
				}
				string stringMessage = "";
				success2 &= config.AcceptClientChanges(pendingConfig, whoAmI, ref message2);
				success2 &= config.AcceptClientChanges(pendingConfig, whoAmI, ref stringMessage);
				if (!string.IsNullOrEmpty(stringMessage))
				{
					message2 = NetworkText.FromLiteral(stringMessage);
				}
				if (success2)
				{
					ConfigManager.Save(pendingConfig);
					json = JsonConvert.SerializeObject(pendingConfig, ConfigManager.serializerSettingsCompact);
					JsonConvert.PopulateObject(json, config, ConfigManager.serializerSettingsCompact);
					config.OnChanged();
					ModPacket p = new ModPacket(249, 256);
					p.Write(true);
					message2.Serialize(p);
					p.Write(modname2);
					p.Write(configname2);
					p.Write(broadcast2);
					p.Write((byte)whoAmI);
					p.Write(json);
					p.Send(-1, -1);
					return;
				}
				ModPacket p2 = new ModPacket(249, 256);
				p2.Write(false);
				message2.Serialize(p2);
				p2.Write(modname2);
				p2.Write(configname2);
				p2.Write(broadcast2);
				p2.Write((byte)whoAmI);
				p2.Send(whoAmI, -1);
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x0053E367 File Offset: 0x0053C567
		public static IEnumerable<PropertyFieldWrapper> GetFieldsAndProperties(object item)
		{
			return ConfigManager.GetFieldsAndProperties(item.GetType());
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x0053E374 File Offset: 0x0053C574
		public static IEnumerable<PropertyFieldWrapper> GetFieldsAndProperties(Type type)
		{
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			return (from x in type.GetFields(BindingFlags.Instance | BindingFlags.Public)
			select new PropertyFieldWrapper(x)).Concat(from x in properties
			select new PropertyFieldWrapper(x));
		}

		/// <summary>
		/// Creates a clone of the provided ModConfig. This clone can be modified independently of the active config.
		/// <br /><br /> Mods can use this method to create a clone of the active config, then use it to populate a UI. The player can use the UI to make several changes then save them all at once.
		/// </summary>
		// Token: 0x060030F5 RID: 12533 RVA: 0x0053E3E0 File Offset: 0x0053C5E0
		public static ModConfig GeneratePopulatedClone(ModConfig original)
		{
			string text = JsonConvert.SerializeObject(original, ConfigManager.serializerSettings);
			ModConfig properClone = original.Clone();
			JsonConvert.PopulateObject(text, properClone, ConfigManager.serializerSettings);
			return properClone;
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x0053E40B File Offset: 0x0053C60B
		[return: Nullable(2)]
		public static object AlternateCreateInstance(Type type)
		{
			if (type == typeof(string))
			{
				return "";
			}
			return Activator.CreateInstance(type, true);
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x0053E42C File Offset: 0x0053C62C
		[NullableContext(2)]
		public static T GetCustomAttributeFromMemberThenMemberType<[Nullable(0)] T>([Nullable(1)] PropertyFieldWrapper memberInfo, object item, object array) where T : Attribute
		{
			T result;
			if ((result = (T)((object)Attribute.GetCustomAttribute(memberInfo.MemberInfo, typeof(T)))) == null)
			{
				result = (T)((object)Attribute.GetCustomAttribute(memberInfo.Type, typeof(T), true));
			}
			return result;
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x0053E46C File Offset: 0x0053C66C
		[return: Nullable(2)]
		public static T GetCustomAttributeFromCollectionMemberThenElementType<[Nullable(0)] T>(MemberInfo memberInfo, Type elementType) where T : Attribute
		{
			T result;
			if ((result = (T)((object)Attribute.GetCustomAttribute(memberInfo, typeof(T)))) == null)
			{
				result = (T)((object)Attribute.GetCustomAttribute(elementType, typeof(T), true));
			}
			return result;
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x0053E4A2 File Offset: 0x0053C6A2
		public static Tuple<UIElement, UIElement> WrapIt(UIElement parent, ref int top, PropertyFieldWrapper memberInfo, object item, int order, [Nullable(2)] object list = null, [Nullable(2)] Type arrayType = null, int index = -1)
		{
			return UIModConfig.WrapIt(parent, ref top, memberInfo, item, order, list, arrayType, index);
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x0053E4B5 File Offset: 0x0053C6B5
		public static void SetPendingChanges(bool changes = true)
		{
			Interface.modConfig.SetPendingChanges(changes);
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x0053E4C4 File Offset: 0x0053C6C4
		[NullableContext(2)]
		public static bool ObjectEquals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a is IEnumerable && b is IEnumerable && !(a is string) && !(b is string))
			{
				return ConfigManager.EnumerableEquals((IEnumerable)a, (IEnumerable)b);
			}
			return a.Equals(b);
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x0053E518 File Offset: 0x0053C718
		public static bool EnumerableEquals(IEnumerable a, IEnumerable b)
		{
			IEnumerator enumeratorA = a.GetEnumerator();
			IEnumerator enumeratorB = b.GetEnumerator();
			bool hasNextA = enumeratorA.MoveNext();
			bool hasNextB = enumeratorB.MoveNext();
			while (hasNextA && hasNextB)
			{
				if (!ConfigManager.ObjectEquals(enumeratorA.Current, enumeratorB.Current))
				{
					return false;
				}
				hasNextA = enumeratorA.MoveNext();
				hasNextB = enumeratorB.MoveNext();
			}
			return !hasNextA && !hasNextB;
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x0053E574 File Offset: 0x0053C774
		internal static string FormatTextAttribute(LocalizedText localizedText, [Nullable(new byte[]
		{
			2,
			1
		})] object[] args)
		{
			if (args == null)
			{
				return localizedText.Value;
			}
			for (int i = 0; i < args.Length; i++)
			{
				string s = args[i] as string;
				if (s != null && s.StartsWith("$"))
				{
					args[i] = Language.GetTextValue(ConfigManager.<FormatTextAttribute>g__FindKeyInScope|38_0(s.Substring(1), localizedText.Key));
				}
			}
			return localizedText.Format(args);
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x0053E5D4 File Offset: 0x0053C7D4
		[return: Nullable(2)]
		private static T GetAndValidate<[Nullable(0)] T>(MemberInfo memberInfo) where T : ConfigKeyAttribute
		{
			T t = (T)((object)Attribute.GetCustomAttribute(memberInfo, typeof(T)));
			T t2 = t;
			bool? flag = (t2 != null) ? new bool?(t2.malformed) : null;
			if (flag != null && flag.GetValueOrDefault())
			{
				string message = typeof(T).Name + " only accepts localization keys for the 'key' parameter.";
				Type type = memberInfo as Type;
				if (type != null)
				{
					message = message + "\nThe class '" + type.FullName + "' caused this exception.";
				}
				else
				{
					string str = message;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(59, 2);
					defaultInterpolatedStringHandler.AppendLiteral("\nThe member '");
					defaultInterpolatedStringHandler.AppendFormatted(memberInfo.Name);
					defaultInterpolatedStringHandler.AppendLiteral("' found in the '");
					defaultInterpolatedStringHandler.AppendFormatted<Type>(memberInfo.DeclaringType);
					defaultInterpolatedStringHandler.AppendLiteral("' class caused this exception.");
					message = str + defaultInterpolatedStringHandler.ToStringAndClear();
				}
				message += "\nClick Open Web Help for more information.";
				throw new ValueNotTranslationKeyException(message);
			}
			return t;
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x0053E6D4 File Offset: 0x0053C8D4
		private static string GetConfigKey<[Nullable(0)] T>(MemberInfo memberInfo, string dataName) where T : ConfigKeyAttribute
		{
			T t = ConfigManager.GetAndValidate<T>(memberInfo);
			return ((t != null) ? t.key : null) ?? ConfigManager.GetDefaultLocalizationKey(memberInfo, dataName);
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x0053E6F8 File Offset: 0x0053C8F8
		private static string GetDefaultLocalizationKey(MemberInfo member, string dataName)
		{
			Type t = member as Type;
			string modName;
			string groupKey = AssemblyManager.GetAssemblyOwner(((t != null) ? t : member.DeclaringType).Assembly, out modName) ? ("Mods." + modName + ".Configs") : "Config";
			string memberKey = (member is Type) ? member.Name : (member.DeclaringType.Name + "." + member.Name);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
			defaultInterpolatedStringHandler.AppendFormatted(groupKey);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted(memberKey);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted(dataName);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x0053E7AA File Offset: 0x0053C9AA
		internal static string GetLocalizedLabel(PropertyFieldWrapper member)
		{
			return ConfigManager.GetLocalizedText<LabelKeyAttribute, LabelArgsAttribute>(member, "Label") ?? member.Name;
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x0053E7C1 File Offset: 0x0053C9C1
		internal static string GetLocalizedTooltip(PropertyFieldWrapper member)
		{
			return ConfigManager.GetLocalizedText<TooltipKeyAttribute, TooltipArgsAttribute>(member, "Tooltip") ?? "";
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x0053E7D8 File Offset: 0x0053C9D8
		[return: Nullable(2)]
		private static string GetLocalizedText<[Nullable(0)] T, [Nullable(0)] TArgs>(PropertyFieldWrapper memberInfo, string dataName) where T : ConfigKeyAttribute where TArgs : ConfigArgsAttribute
		{
			string memberKey = ConfigManager.GetConfigKey<T>(memberInfo.MemberInfo, dataName);
			if (!Language.Exists(memberKey))
			{
				return null;
			}
			LocalizedText memberLocalization = Language.GetText(memberKey);
			if (memberLocalization.Value != "")
			{
				LocalizedText localizedText = memberLocalization;
				TArgs targs = memberInfo.MemberInfo.GetCustomAttribute<TArgs>();
				return ConfigManager.FormatTextAttribute(localizedText, (targs != null) ? targs.args : null);
			}
			if (memberInfo.Type.IsPrimitive)
			{
				return null;
			}
			string typeKey = ConfigManager.GetConfigKey<T>(memberInfo.Type, dataName);
			if (!Language.Exists(typeKey))
			{
				return null;
			}
			LocalizedText text = Language.GetText(typeKey);
			TArgs targs2 = memberInfo.Type.GetCustomAttribute<TArgs>();
			return ConfigManager.FormatTextAttribute(text, (targs2 != null) ? targs2.args : null);
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x0053E888 File Offset: 0x0053CA88
		[return: Nullable(2)]
		internal static HeaderAttribute GetLocalizedHeader(MemberInfo memberInfo)
		{
			HeaderAttribute header = memberInfo.GetCustomAttribute<HeaderAttribute>();
			if (header == null)
			{
				return null;
			}
			if (header.malformed)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(209, 3);
				defaultInterpolatedStringHandler.AppendFormatted("HeaderAttribute");
				defaultInterpolatedStringHandler.AppendLiteral(" only accepts localization keys or identifiers for the 'identifierOrKey' parameter. Neither can have spaces.\nThe member '");
				defaultInterpolatedStringHandler.AppendFormatted(memberInfo.Name);
				defaultInterpolatedStringHandler.AppendLiteral("' found in the '");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(memberInfo.DeclaringType);
				defaultInterpolatedStringHandler.AppendLiteral("' class caused this exception.\nClick Open Web Help for more information.");
				throw new ValueNotTranslationKeyException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			if (header.IsIdentifier)
			{
				header.key = ConfigManager.GetDefaultLocalizationKey(memberInfo.DeclaringType, "Headers." + header.identifier);
			}
			return header;
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x0053E938 File Offset: 0x0053CB38
		[CompilerGenerated]
		internal static string <FormatTextAttribute>g__FindKeyInScope|38_0(string key, string scope)
		{
			if (LanguageManager.Instance.Exists(key))
			{
				return key;
			}
			string[] splitKey = scope.Split(".", StringSplitOptions.None);
			for (int i = splitKey.Length - 1; i >= 0; i--)
			{
				string combinedKey = string.Join(".", splitKey.Take(i + 1)) + "." + key;
				if (LanguageManager.Instance.Exists(combinedKey))
				{
					return combinedKey;
				}
			}
			return key;
		}

		// Token: 0x04001D2B RID: 7467
		internal static readonly IDictionary<Mod, List<ModConfig>> Configs = new Dictionary<Mod, List<ModConfig>>();

		// Token: 0x04001D2C RID: 7468
		private static readonly IDictionary<Mod, List<ModConfig>> loadTimeConfigs = new Dictionary<Mod, List<ModConfig>>();

		// Token: 0x04001D2D RID: 7469
		public static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
		{
			Formatting = 1,
			DefaultValueHandling = 3,
			ObjectCreationHandling = 2,
			NullValueHandling = 1,
			ContractResolver = new ReferenceDefaultsPreservingResolver()
		};

		// Token: 0x04001D2E RID: 7470
		internal static readonly JsonSerializerSettings serializerSettingsCompact = new JsonSerializerSettings
		{
			Formatting = 0,
			DefaultValueHandling = 3,
			ObjectCreationHandling = 2,
			NullValueHandling = 1,
			ContractResolver = ConfigManager.serializerSettings.ContractResolver
		};

		// Token: 0x04001D2F RID: 7471
		private static readonly HashSet<Type> typesWithLocalizationRegistered = new HashSet<Type>();

		// Token: 0x04001D30 RID: 7472
		public static readonly string ModConfigPath = Path.Combine(Main.SavePath, "ModConfigs");

		// Token: 0x04001D31 RID: 7473
		public static readonly string ServerModConfigPath = Path.Combine(Main.SavePath, "ModConfigs", "Server");

		// Token: 0x02000AE7 RID: 2791
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006E80 RID: 28288
			[Nullable(0)]
			public static Func<Mod, bool> <0>__ModNeedsReload;
		}
	}
}
