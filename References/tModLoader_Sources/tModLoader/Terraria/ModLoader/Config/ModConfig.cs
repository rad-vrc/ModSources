using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// ModConfig provides a way for mods to be configurable. ModConfigs can either be Client specific or Server specific.
	/// When joining a MP server, Client configs are kept but Server configs are synced from the server.
	/// Using serialization attributes such as [DefaultValue(5)] or [JsonIgnore] are critical for proper usage of ModConfig.
	/// tModLoader also provides its own attributes such as ReloadRequiredAttribute and LabelAttribute.
	/// </summary>
	// Token: 0x02000392 RID: 914
	public abstract class ModConfig : ILocalizedModType, IModType
	{
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x0053F4A6 File Offset: 0x0053D6A6
		// (set) Token: 0x0600314F RID: 12623 RVA: 0x0053F4AE File Offset: 0x0053D6AE
		[JsonIgnore]
		public Mod Mod { get; internal set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x0053F4B7 File Offset: 0x0053D6B7
		// (set) Token: 0x06003151 RID: 12625 RVA: 0x0053F4BF File Offset: 0x0053D6BF
		[JsonIgnore]
		public string Name { get; internal set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x0053F4C8 File Offset: 0x0053D6C8
		[JsonIgnore]
		public string FullName
		{
			get
			{
				return this.Mod.Name + "/" + this.Name;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x0053F4E5 File Offset: 0x0053D6E5
		[JsonIgnore]
		public virtual string LocalizationCategory
		{
			get
			{
				return "Configs";
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06003154 RID: 12628 RVA: 0x0053F4EC File Offset: 0x0053D6EC
		[JsonIgnore]
		public virtual LocalizedText DisplayName
		{
			get
			{
				return Language.GetOrRegister(this.GetLocalizationKey("DisplayName"), delegate()
				{
					LabelAttribute legacyLabelAttribute = ConfigManager.GetLegacyLabelAttribute(base.GetType());
					return ((legacyLabelAttribute != null) ? legacyLabelAttribute.LocalizationEntry : null) ?? Regex.Replace(this.Name, "([A-Z])", " $1").Trim();
				});
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06003155 RID: 12629
		[JsonIgnore]
		public abstract ConfigScope Mode { get; }

		// Token: 0x06003156 RID: 12630 RVA: 0x0053F50A File Offset: 0x0053D70A
		public virtual bool Autoload(ref string name)
		{
			return this.Mod.ContentAutoloadingEnabled;
		}

		/// <summary>
		/// This method is called when the ModConfig has been loaded for the first time. This happens before regular Autoloading and Mod.Load. You can use this hook to assign a static reference to this instance for easy access.
		/// tModLoader will automatically assign (and later unload) this instance to a static field named Instance in the class prior to calling this method, if it exists.
		/// </summary>
		// Token: 0x06003157 RID: 12631 RVA: 0x0053F517 File Offset: 0x0053D717
		public virtual void OnLoaded()
		{
		}

		/// <summary>
		/// This hook is called anytime new config values have been set and are ready to take effect. This will always be called right after OnLoaded and anytime new configuration values are ready to be used. The hook won't be called with values that violate NeedsReload. Use this hook to integrate with other code in your Mod to apply the effects of the configuration values. If your NeedsReload is correctly implemented, you should be able to apply the settings without error in this hook. Be aware that this hook can be called in-game and in the main menu, as well as in single player and multiplayer situations.
		/// </summary>
		// Token: 0x06003158 RID: 12632 RVA: 0x0053F519 File Offset: 0x0053D719
		public virtual void OnChanged()
		{
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.Config.ModConfig.AcceptClientChanges(Terraria.ModLoader.Config.ModConfig,System.Int32,Terraria.Localization.NetworkText@)" />
		// Token: 0x06003159 RID: 12633 RVA: 0x0053F51B File Offset: 0x0053D71B
		[Obsolete("Use the updated hook signature")]
		public virtual bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		{
			return true;
		}

		/// <summary>
		/// Called on the Server for ServerSide configs to determine if the changes asked for by the Client will be accepted. Useful for enforcing permissions. Called after a check for NeedsReload.
		/// <br /><br /> In advanced situations <paramref name="pendingConfig" /> can be modified here and the changes will be applied and be synced.
		/// </summary>
		/// <param name="pendingConfig">An instance of the ModConfig with the attempted changes</param>
		/// <param name="whoAmI">The client whoAmI</param>
		/// <param name="message">A message that will be returned to the client, set this to the reason the server rejects the changes.</param>
		/// <returns>Return false to reject client changes</returns>
		// Token: 0x0600315A RID: 12634 RVA: 0x0053F51E File Offset: 0x0053D71E
		public virtual bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
		{
			return true;
		}

		/// <summary>
		/// Called on multiplayer clients after the server accepts or rejects ServerSide config changes made by a client. Can be used to update UI attempting to manually save changes to a ServerSide config (using <see cref="M:Terraria.ModLoader.Config.ModConfig.SaveChanges(Terraria.ModLoader.Config.ModConfig,System.Action{System.String,Microsoft.Xna.Framework.Color},System.Boolean,System.Boolean)" />. For rejections this is only called on the client who requested the changes. 
		/// <br /><br /> <paramref name="player" /> indicates which player requested the changes (see <see cref="F:Terraria.Main.myPlayer" />). 
		/// <br /><br /> <paramref name="success" /> indicates if the changes were accepted and <paramref name="message" /> is the corresponding message from AcceptClientChanges.
		/// </summary>
		// Token: 0x0600315B RID: 12635 RVA: 0x0053F521 File Offset: 0x0053D721
		public virtual void HandleAcceptClientChangesReply(bool success, int player, NetworkText message)
		{
		}

		/// <summary>
		/// tModLoader will call Clone on ModConfig to facilitate proper implementation of the ModConfig user interface and detecting when a reload is required. Modders need to override this method if their config contains reference types. Failure to do so will lead to bugs. See ModConfigShowcaseDataTypes.Clone for examples and explanations.
		/// </summary>
		/// <returns></returns>
		// Token: 0x0600315C RID: 12636 RVA: 0x0053F523 File Offset: 0x0053D723
		public virtual ModConfig Clone()
		{
			return (ModConfig)base.MemberwiseClone();
		}

		/// <summary>
		/// Whether or not a reload is required. The default implementation compares properties and fields annotated with the ReloadRequiredAttribute. Unlike the other ModConfig hooks, this method is called on a clone of the ModConfig that was saved during mod loading. The pendingConfig has values that are about to take effect. Neither of these instances necessarily match the instance used in OnLoaded.
		/// </summary>
		/// <param name="pendingConfig">The other instance of ModConfig to compare against, it contains the values that are pending to take effect</param>
		/// <returns></returns>
		// Token: 0x0600315D RID: 12637 RVA: 0x0053F530 File Offset: 0x0053D730
		public virtual bool NeedsReload(ModConfig pendingConfig)
		{
			foreach (PropertyFieldWrapper variable in ConfigManager.GetFieldsAndProperties(this))
			{
				if (ConfigManager.GetCustomAttributeFromMemberThenMemberType<ReloadRequiredAttribute>(variable, this, null) != null && !ConfigManager.ObjectEquals(variable.GetValue(this), variable.GetValue(pendingConfig)))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Attempts to save changes made to this ModConfig. This must be called on the active ModConfig instance.
		/// <br /><br /> If <paramref name="pendingConfig" /> is provided, it will be used as the source for the changes to apply to the active config instance. If <paramref name="status" /> is provided, it will be called with text and a color to indicate the status of the operation. If <paramref name="silent" /> is false, sounds will play indicating success or failure. If <paramref name="broadcast" /> is false, the chat message informing all players when a ServerSide config is changed saying "Shared config changed: Message: {0}, Mod: {1}, Config: {2}" will not appear on clients. 
		/// <br /><br /> <b>Mod code can run this method in-game, but there are some considerations to keep in mind: </b>
		/// <br /><br /> Calling this method on a <see cref="F:Terraria.ModLoader.Config.ConfigScope.ServerSide" /> config from a multiplayer client will result in <see cref="F:Terraria.ModLoader.Config.ConfigSaveResult.RequestSentToServer" /> being returned and the actual save logic being performed on the server. <see cref="M:Terraria.ModLoader.Config.ModConfig.HandleAcceptClientChangesReply(System.Boolean,System.Int32,Terraria.Localization.NetworkText)" /> will be called on all clients after the server accepts or denies the changes. Calling this method on the server for a ServerSide config is also supported.
		/// <br /><br /> Attempting to save changes that would violate <see cref="M:Terraria.ModLoader.Config.ModConfig.NeedsReload(Terraria.ModLoader.Config.ModConfig)" /> will fail and <see cref="F:Terraria.ModLoader.Config.ConfigSaveResult.NeedsReload" /> will be returned.
		/// <br /><br /> If there is a chance that the changes won't be accepted, or if you want to provide a UI for the user to make changes without them taking effect immediately, you should use a clone of the ModConfig and pass it in as <paramref name="pendingConfig" /> instead of modifying the active ModConfig directly. To make a clone, call the <see cref="M:Terraria.ModLoader.Config.ConfigManager.GeneratePopulatedClone(Terraria.ModLoader.Config.ModConfig)" /> method.
		/// <br /><br /> See <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Common/UI/ExampleFullscreenUI/ExampleFullscreenUI.cs">ExampleFullscreenUI.cs</see> for a complete example of using this method.
		/// </summary>
		// Token: 0x0600315E RID: 12638 RVA: 0x0053F59C File Offset: 0x0053D79C
		public ConfigSaveResult SaveChanges(ModConfig pendingConfig = null, Action<string, Color> status = null, bool silent = true, bool broadcast = true)
		{
			if (this != ConfigManager.GetConfig(this.Mod, this.Name))
			{
				throw new Exception("SaveChanges must be called on the active config.");
			}
			pendingConfig = (pendingConfig ?? this);
			bool pendingIsActive = pendingConfig == this;
			if (Main.gameMenu)
			{
				if (!silent)
				{
					SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
				}
				ConfigManager.Save(pendingConfig);
				ConfigManager.Load(this);
			}
			else
			{
				if (pendingConfig.Mode == ConfigScope.ServerSide && Main.netMode == 1)
				{
					if (status != null)
					{
						status(Language.GetTextValue("tModLoader.ModConfigAskingServerToAcceptChanges"), Color.Yellow);
					}
					ModPacket modPacket = new ModPacket(249, 256);
					modPacket.Write(pendingConfig.Mod.Name);
					modPacket.Write(pendingConfig.Name);
					string json = JsonConvert.SerializeObject(pendingConfig, ConfigManager.serializerSettingsCompact);
					modPacket.Write(broadcast);
					modPacket.Write(json);
					modPacket.Send(-1, -1);
					if (pendingIsActive)
					{
						ConfigManager.Load(this);
					}
					return ConfigSaveResult.RequestSentToServer;
				}
				if (ConfigManager.GetLoadTimeConfig(this.Mod, this.Name).NeedsReload(pendingConfig))
				{
					if (!silent)
					{
						SoundEngine.PlaySound(SoundID.MenuClose, null, null);
					}
					if (status != null)
					{
						status(Language.GetTextValue("tModLoader.ModConfigCantSaveBecauseChangesWouldRequireAReload"), Color.Red);
					}
					if (pendingIsActive)
					{
						ConfigManager.Load(this);
					}
					return ConfigSaveResult.NeedsReload;
				}
				if (!silent)
				{
					SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
				}
				ConfigManager.Save(pendingConfig);
				ConfigManager.Load(this);
				this.OnChanged();
				if (pendingConfig.Mode == ConfigScope.ServerSide && Main.netMode == 2)
				{
					ModPacket p = new ModPacket(249, 256);
					p.Write(true);
					NetworkText.FromKey("tModLoader.ModConfigAccepted", Array.Empty<object>()).Serialize(p);
					p.Write(this.Mod.Name);
					p.Write(this.Name);
					p.Write(broadcast);
					p.Write(byte.MaxValue);
					string json2 = JsonConvert.SerializeObject(this, ConfigManager.serializerSettingsCompact);
					p.Write(json2);
					p.Send(-1, -1);
				}
			}
			if (status != null)
			{
				status(Language.GetTextValue("tModLoader.ModConfigConfigSaved"), Color.Green);
			}
			return ConfigSaveResult.Success;
		}

		/// <summary>
		/// Opens this config in the config UI.
		/// <para /> Can be used to allow your own UI to access the config.
		/// <para /> <paramref name="onClose" /> can be used to run code after the config is closed, such as opening a modded UI or showing a message to the user.
		/// <para /> <paramref name="scrollToOption" /> can be used to scroll to a specific member of the config and highlight it. It can also be used to scroll to the header above a member using the format <c>"Header:{MemberNameHere}"</c>. If the member has <c>[SeparatePage]</c> then the subpage will open automatically as well. Set <paramref name="centerScrolledOption" /> to false if you'd like the config option to be at the top of the list when focused instead of at the center.
		/// </summary>
		/// <param name="onClose">A delegate that is called when the back button is pressed to allow for custom back button behavior.</param>
		/// <param name="scrollToOption">The name of a field of the ModConfig to scroll to.</param>
		/// <param name="centerScrolledOption"></param>
		/// <param name="playSound">Whether <see cref="F:Terraria.ID.SoundID.MenuOpen" /> will be played when the UI is opened.</param>
		// Token: 0x0600315F RID: 12639 RVA: 0x0053F7B8 File Offset: 0x0053D9B8
		public void Open(Action onClose = null, string scrollToOption = null, bool centerScrolledOption = true, bool playSound = true)
		{
			if (playSound)
			{
				SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			}
			Interface.modConfig.SetMod(this.Mod, this, true, onClose, scrollToOption, centerScrolledOption);
			if (Main.gameMenu)
			{
				Main.menuMode = 10024;
				return;
			}
			IngameFancyUI.CoverNextFrame();
			Main.playerInventory = false;
			Main.editChest = false;
			Main.npcChatText = "";
			Main.inFancyUI = true;
			Main.InGameUI.SetState(Interface.modConfig);
		}
	}
}
