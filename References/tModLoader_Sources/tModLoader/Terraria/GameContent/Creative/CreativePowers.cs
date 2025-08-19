using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.Net;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000641 RID: 1601
	public class CreativePowers
	{
		// Token: 0x02000CDC RID: 3292
		public abstract class APerPlayerTogglePower : ICreativePower, IOnPlayerJoining
		{
			// Token: 0x17000973 RID: 2419
			// (get) Token: 0x06006198 RID: 24984 RVA: 0x006D444D File Offset: 0x006D264D
			// (set) Token: 0x06006199 RID: 24985 RVA: 0x006D4455 File Offset: 0x006D2655
			public ushort PowerId { get; set; }

			// Token: 0x17000974 RID: 2420
			// (get) Token: 0x0600619A RID: 24986 RVA: 0x006D445E File Offset: 0x006D265E
			// (set) Token: 0x0600619B RID: 24987 RVA: 0x006D4466 File Offset: 0x006D2666
			public string ServerConfigName { get; set; }

			// Token: 0x17000975 RID: 2421
			// (get) Token: 0x0600619C RID: 24988 RVA: 0x006D446F File Offset: 0x006D266F
			// (set) Token: 0x0600619D RID: 24989 RVA: 0x006D4477 File Offset: 0x006D2677
			public PowerPermissionLevel CurrentPermissionLevel { get; set; }

			// Token: 0x17000976 RID: 2422
			// (get) Token: 0x0600619E RID: 24990 RVA: 0x006D4480 File Offset: 0x006D2680
			// (set) Token: 0x0600619F RID: 24991 RVA: 0x006D4488 File Offset: 0x006D2688
			public PowerPermissionLevel DefaultPermissionLevel { get; set; }

			// Token: 0x060061A0 RID: 24992 RVA: 0x006D4491 File Offset: 0x006D2691
			public bool IsEnabledForPlayer(int playerIndex)
			{
				return this._perPlayerIsEnabled.IndexInRange(playerIndex) && this._perPlayerIsEnabled[playerIndex];
			}

			// Token: 0x060061A1 RID: 24993 RVA: 0x006D44AC File Offset: 0x006D26AC
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				byte b = reader.ReadByte();
				if (b == 0)
				{
					this.Deserialize_SyncEveryone(reader, userId);
					return;
				}
				if (b != 1)
				{
					return;
				}
				int playerIndex = (int)reader.ReadByte();
				bool state = reader.ReadBoolean();
				if (Main.netMode == 2)
				{
					playerIndex = userId;
					if (!CreativePowersHelper.IsAvailableForPlayer(this, playerIndex))
					{
						return;
					}
				}
				this.SetEnabledState(playerIndex, state);
			}

			// Token: 0x060061A2 RID: 24994 RVA: 0x006D44FC File Offset: 0x006D26FC
			private void Deserialize_SyncEveryone(BinaryReader reader, int userId)
			{
				int num = (int)Math.Ceiling((double)((float)this._perPlayerIsEnabled.Length / 8f));
				if (Main.netMode == 2 && !CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					reader.ReadBytes(num);
					return;
				}
				for (int i = 0; i < num; i++)
				{
					BitsByte bitsByte = reader.ReadByte();
					for (int j = 0; j < 8; j++)
					{
						int num2 = i * 8 + j;
						if (num2 != Main.myPlayer)
						{
							if (num2 >= this._perPlayerIsEnabled.Length)
							{
								break;
							}
							this.SetEnabledState(num2, bitsByte[j]);
						}
					}
				}
			}

			// Token: 0x060061A3 RID: 24995 RVA: 0x006D458C File Offset: 0x006D278C
			public void SetEnabledState(int playerIndex, bool state)
			{
				this._perPlayerIsEnabled[playerIndex] = state;
				if (Main.netMode == 2)
				{
					NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 3);
					packet.Writer.Write(1);
					packet.Writer.Write((byte)playerIndex);
					packet.Writer.Write(state);
					NetManager.Instance.Broadcast(packet, -1);
				}
			}

			// Token: 0x060061A4 RID: 24996 RVA: 0x006D45EB File Offset: 0x006D27EB
			public void DebugCall()
			{
				this.RequestUse();
			}

			// Token: 0x060061A5 RID: 24997 RVA: 0x006D45F4 File Offset: 0x006D27F4
			internal void RequestUse()
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
				packet.Writer.Write(1);
				packet.Writer.Write((byte)Main.myPlayer);
				packet.Writer.Write(!this._perPlayerIsEnabled[Main.myPlayer]);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060061A6 RID: 24998 RVA: 0x006D4654 File Offset: 0x006D2854
			public void Reset()
			{
				for (int i = 0; i < this._perPlayerIsEnabled.Length; i++)
				{
					this._perPlayerIsEnabled[i] = this._defaultToggleState;
				}
			}

			// Token: 0x060061A7 RID: 24999 RVA: 0x006D4684 File Offset: 0x006D2884
			public void OnPlayerJoining(int playerIndex)
			{
				int num = (int)Math.Ceiling((double)((float)this._perPlayerIsEnabled.Length / 8f));
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, num + 1);
				packet.Writer.Write(0);
				for (int i = 0; i < num; i++)
				{
					BitsByte bitsByte = 0;
					for (int j = 0; j < 8; j++)
					{
						int num2 = i * 8 + j;
						if (num2 >= this._perPlayerIsEnabled.Length)
						{
							break;
						}
						bitsByte[j] = this._perPlayerIsEnabled[num2];
					}
					packet.Writer.Write(bitsByte);
				}
				NetManager.Instance.SendToClient(packet, playerIndex);
			}

			// Token: 0x060061A8 RID: 25000 RVA: 0x006D472C File Offset: 0x006D292C
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				GroupOptionButton<bool> groupOptionButton = CreativePowersHelper.CreateToggleButton(info);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, Main.OurFavoriteColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnLeftClick += this.button_OnClick;
				groupOptionButton.OnUpdate += this.button_OnUpdate;
				elements.Add(groupOptionButton);
			}

			// Token: 0x060061A9 RID: 25001 RVA: 0x006D4788 File Offset: 0x006D2988
			private void button_OnUpdate(UIElement affectedElement)
			{
				bool currentOption = this._perPlayerIsEnabled[Main.myPlayer];
				GroupOptionButton<bool> groupOptionButton = affectedElement as GroupOptionButton<bool>;
				groupOptionButton.SetCurrentOption(currentOption);
				if (affectedElement.IsMouseHovering)
				{
					string originalText = Language.GetTextValue(groupOptionButton.IsSelected ? (this._powerNameKey + "_Enabled") : (this._powerNameKey + "_Disabled"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref originalText, this._powerNameKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref originalText, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref originalText);
					Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x060061AA RID: 25002 RVA: 0x006D4836 File Offset: 0x006D2A36
			private void button_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				if (this.GetIsUnlocked() && CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					this.RequestUse();
				}
			}

			// Token: 0x060061AB RID: 25003
			public abstract bool GetIsUnlocked();

			// Token: 0x04007A48 RID: 31304
			internal string _powerNameKey;

			// Token: 0x04007A49 RID: 31305
			internal Point _iconLocation;

			// Token: 0x04007A4A RID: 31306
			internal bool _defaultToggleState;

			// Token: 0x04007A4B RID: 31307
			private bool[] _perPlayerIsEnabled = new bool[255];

			// Token: 0x02000E5B RID: 3675
			private enum SubMessageType : byte
			{
				// Token: 0x04007D55 RID: 32085
				SyncEveryone,
				// Token: 0x04007D56 RID: 32086
				SyncOnePlayer
			}
		}

		// Token: 0x02000CDD RID: 3293
		public abstract class APerPlayerSliderPower : ICreativePower, IOnPlayerJoining, IProvideSliderElement, IPowerSubcategoryElement
		{
			// Token: 0x17000977 RID: 2423
			// (get) Token: 0x060061AD RID: 25005 RVA: 0x006D486B File Offset: 0x006D2A6B
			// (set) Token: 0x060061AE RID: 25006 RVA: 0x006D4873 File Offset: 0x006D2A73
			public ushort PowerId { get; set; }

			// Token: 0x17000978 RID: 2424
			// (get) Token: 0x060061AF RID: 25007 RVA: 0x006D487C File Offset: 0x006D2A7C
			// (set) Token: 0x060061B0 RID: 25008 RVA: 0x006D4884 File Offset: 0x006D2A84
			public string ServerConfigName { get; set; }

			// Token: 0x17000979 RID: 2425
			// (get) Token: 0x060061B1 RID: 25009 RVA: 0x006D488D File Offset: 0x006D2A8D
			// (set) Token: 0x060061B2 RID: 25010 RVA: 0x006D4895 File Offset: 0x006D2A95
			public PowerPermissionLevel CurrentPermissionLevel { get; set; }

			// Token: 0x1700097A RID: 2426
			// (get) Token: 0x060061B3 RID: 25011 RVA: 0x006D489E File Offset: 0x006D2A9E
			// (set) Token: 0x060061B4 RID: 25012 RVA: 0x006D48A6 File Offset: 0x006D2AA6
			public PowerPermissionLevel DefaultPermissionLevel { get; set; }

			// Token: 0x060061B5 RID: 25013 RVA: 0x006D48AF File Offset: 0x006D2AAF
			public bool GetRemappedSliderValueFor(int playerIndex, out float value)
			{
				value = 0f;
				if (!this._cachePerPlayer.IndexInRange(playerIndex))
				{
					return false;
				}
				value = this.RemapSliderValueToPowerValue(this._cachePerPlayer[playerIndex]);
				return true;
			}

			// Token: 0x060061B6 RID: 25014
			public abstract float RemapSliderValueToPowerValue(float sliderValue);

			// Token: 0x060061B7 RID: 25015 RVA: 0x006D48DC File Offset: 0x006D2ADC
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				int num = (int)reader.ReadByte();
				float num2 = reader.ReadSingle();
				if (Main.netMode == 2)
				{
					num = userId;
					if (!CreativePowersHelper.IsAvailableForPlayer(this, num))
					{
						return;
					}
				}
				this._cachePerPlayer[num] = num2;
				if (num == Main.myPlayer)
				{
					this._sliderCurrentValueCache = num2;
					this.UpdateInfoFromSliderValueCache();
				}
			}

			// Token: 0x060061B8 RID: 25016
			internal abstract void UpdateInfoFromSliderValueCache();

			// Token: 0x060061B9 RID: 25017 RVA: 0x006D4929 File Offset: 0x006D2B29
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060061BA RID: 25018 RVA: 0x006D4930 File Offset: 0x006D2B30
			public void DebugCall()
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 5);
				packet.Writer.Write((byte)Main.myPlayer);
				packet.Writer.Write(0f);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060061BB RID: 25019
			public abstract UIElement ProvideSlider();

			// Token: 0x060061BC RID: 25020 RVA: 0x006D4978 File Offset: 0x006D2B78
			internal float GetSliderValue()
			{
				if (Main.netMode == 1 && this._needsToCommitChange)
				{
					return this._currentTargetValue;
				}
				return this._sliderCurrentValueCache;
			}

			// Token: 0x060061BD RID: 25021 RVA: 0x006D4997 File Offset: 0x006D2B97
			internal void SetValueKeyboard(float value)
			{
				if (value != this._currentTargetValue && CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					this._currentTargetValue = value;
					this._needsToCommitChange = true;
				}
			}

			// Token: 0x060061BE RID: 25022 RVA: 0x006D49C0 File Offset: 0x006D2BC0
			internal void SetValueGamepad()
			{
				float sliderValue = this.GetSliderValue();
				float num = UILinksInitializer.HandleSliderVerticalInput(sliderValue, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				if (num != sliderValue)
				{
					this.SetValueKeyboard(num);
				}
			}

			// Token: 0x060061BF RID: 25023 RVA: 0x006D49FF File Offset: 0x006D2BFF
			public void PushChangeAndSetSlider(float value)
			{
				if (CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					value = MathHelper.Clamp(value, 0f, 1f);
					this._sliderCurrentValueCache = value;
					this._currentTargetValue = value;
					this.PushChange(value);
				}
			}

			// Token: 0x060061C0 RID: 25024 RVA: 0x006D4A38 File Offset: 0x006D2C38
			public GroupOptionButton<int> GetOptionButton(CreativePowerUIElementRequestInfo info, int optionIndex, int currentOptionIndex)
			{
				GroupOptionButton<int> groupOptionButton = CreativePowersHelper.CreateCategoryButton<int>(info, optionIndex, currentOptionIndex);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, CreativePowersHelper.CommonSelectedColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnUpdate += this.categoryButton_OnUpdate;
				return groupOptionButton;
			}

			// Token: 0x060061C1 RID: 25025 RVA: 0x006D4A80 File Offset: 0x006D2C80
			private void categoryButton_OnUpdate(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					GroupOptionButton<int> groupOptionButton = affectedElement as GroupOptionButton<int>;
					string originalText = Language.GetTextValue(this._powerNameKey + (groupOptionButton.IsSelected ? "_Opened" : "_Closed"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref originalText, this._powerNameKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref originalText, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref originalText);
					Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
				}
				this.AttemptPushingChange();
			}

			// Token: 0x060061C2 RID: 25026 RVA: 0x006D4B18 File Offset: 0x006D2D18
			private void AttemptPushingChange()
			{
				if (this._needsToCommitChange && DateTime.UtcNow.CompareTo(this._nextTimeWeCanPush) != -1)
				{
					this.PushChange(this._currentTargetValue);
				}
			}

			// Token: 0x060061C3 RID: 25027 RVA: 0x006D4B50 File Offset: 0x006D2D50
			internal void PushChange(float newSliderValue)
			{
				this._needsToCommitChange = false;
				this._sliderCurrentValueCache = newSliderValue;
				this._nextTimeWeCanPush = DateTime.UtcNow;
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 5);
				packet.Writer.Write((byte)Main.myPlayer);
				packet.Writer.Write(newSliderValue);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060061C4 RID: 25028 RVA: 0x006D4BB0 File Offset: 0x006D2DB0
			public virtual void Reset()
			{
				for (int i = 0; i < this._cachePerPlayer.Length; i++)
				{
					this.ResetForPlayer(i);
				}
			}

			// Token: 0x060061C5 RID: 25029 RVA: 0x006D4BD7 File Offset: 0x006D2DD7
			public virtual void ResetForPlayer(int playerIndex)
			{
				this._cachePerPlayer[playerIndex] = this._sliderDefaultValue;
				if (playerIndex == Main.myPlayer)
				{
					this._sliderCurrentValueCache = this._sliderDefaultValue;
					this._currentTargetValue = this._sliderDefaultValue;
				}
			}

			// Token: 0x060061C6 RID: 25030 RVA: 0x006D4C07 File Offset: 0x006D2E07
			public void OnPlayerJoining(int playerIndex)
			{
				this.ResetForPlayer(playerIndex);
			}

			// Token: 0x060061C7 RID: 25031
			public abstract bool GetIsUnlocked();

			// Token: 0x04007A50 RID: 31312
			internal Point _iconLocation;

			// Token: 0x04007A51 RID: 31313
			internal float _sliderCurrentValueCache;

			// Token: 0x04007A52 RID: 31314
			internal string _powerNameKey;

			// Token: 0x04007A53 RID: 31315
			internal float[] _cachePerPlayer = new float[256];

			// Token: 0x04007A54 RID: 31316
			internal float _sliderDefaultValue;

			// Token: 0x04007A55 RID: 31317
			private float _currentTargetValue;

			// Token: 0x04007A56 RID: 31318
			private bool _needsToCommitChange;

			// Token: 0x04007A57 RID: 31319
			private DateTime _nextTimeWeCanPush = DateTime.UtcNow;
		}

		// Token: 0x02000CDE RID: 3294
		public abstract class ASharedButtonPower : ICreativePower
		{
			// Token: 0x1700097B RID: 2427
			// (get) Token: 0x060061C9 RID: 25033 RVA: 0x006D4C33 File Offset: 0x006D2E33
			// (set) Token: 0x060061CA RID: 25034 RVA: 0x006D4C3B File Offset: 0x006D2E3B
			public ushort PowerId { get; set; }

			// Token: 0x1700097C RID: 2428
			// (get) Token: 0x060061CB RID: 25035 RVA: 0x006D4C44 File Offset: 0x006D2E44
			// (set) Token: 0x060061CC RID: 25036 RVA: 0x006D4C4C File Offset: 0x006D2E4C
			public string ServerConfigName { get; set; }

			// Token: 0x1700097D RID: 2429
			// (get) Token: 0x060061CD RID: 25037 RVA: 0x006D4C55 File Offset: 0x006D2E55
			// (set) Token: 0x060061CE RID: 25038 RVA: 0x006D4C5D File Offset: 0x006D2E5D
			public PowerPermissionLevel CurrentPermissionLevel { get; set; }

			// Token: 0x1700097E RID: 2430
			// (get) Token: 0x060061CF RID: 25039 RVA: 0x006D4C66 File Offset: 0x006D2E66
			// (set) Token: 0x060061D0 RID: 25040 RVA: 0x006D4C6E File Offset: 0x006D2E6E
			public PowerPermissionLevel DefaultPermissionLevel { get; set; }

			// Token: 0x060061D1 RID: 25041 RVA: 0x006D4C77 File Offset: 0x006D2E77
			public ASharedButtonPower()
			{
				this.OnCreation();
			}

			// Token: 0x060061D2 RID: 25042 RVA: 0x006D4C88 File Offset: 0x006D2E88
			public void RequestUse()
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 0);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060061D3 RID: 25043 RVA: 0x006D4CAD File Offset: 0x006D2EAD
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				if (Main.netMode != 2 || CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					this.UsePower();
				}
			}

			// Token: 0x060061D4 RID: 25044
			internal abstract void UsePower();

			// Token: 0x060061D5 RID: 25045
			internal abstract void OnCreation();

			// Token: 0x060061D6 RID: 25046 RVA: 0x006D4CC8 File Offset: 0x006D2EC8
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				GroupOptionButton<bool> groupOptionButton = CreativePowersHelper.CreateSimpleButton(info);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, CreativePowersHelper.CommonSelectedColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnLeftClick += this.button_OnClick;
				groupOptionButton.OnUpdate += this.button_OnUpdate;
				elements.Add(groupOptionButton);
			}

			// Token: 0x060061D7 RID: 25047 RVA: 0x006D4D24 File Offset: 0x006D2F24
			private void button_OnUpdate(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string originalText = Language.GetTextValue(this._powerNameKey);
					CreativePowersHelper.AddDescriptionIfNeeded(ref originalText, this._descriptionKey);
					CreativePowersHelper.AddUnlockTextIfNeeded(ref originalText, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref originalText);
					Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x060061D8 RID: 25048 RVA: 0x006D4D89 File Offset: 0x006D2F89
			private void button_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				if (CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					this.RequestUse();
				}
			}

			// Token: 0x060061D9 RID: 25049
			public abstract bool GetIsUnlocked();

			// Token: 0x04007A5C RID: 31324
			internal Point _iconLocation;

			// Token: 0x04007A5D RID: 31325
			internal string _powerNameKey;

			// Token: 0x04007A5E RID: 31326
			internal string _descriptionKey;
		}

		// Token: 0x02000CDF RID: 3295
		public abstract class ASharedTogglePower : ICreativePower, IOnPlayerJoining
		{
			// Token: 0x1700097F RID: 2431
			// (get) Token: 0x060061DA RID: 25050 RVA: 0x006D4D9E File Offset: 0x006D2F9E
			// (set) Token: 0x060061DB RID: 25051 RVA: 0x006D4DA6 File Offset: 0x006D2FA6
			public ushort PowerId { get; set; }

			// Token: 0x17000980 RID: 2432
			// (get) Token: 0x060061DC RID: 25052 RVA: 0x006D4DAF File Offset: 0x006D2FAF
			// (set) Token: 0x060061DD RID: 25053 RVA: 0x006D4DB7 File Offset: 0x006D2FB7
			public string ServerConfigName { get; set; }

			// Token: 0x17000981 RID: 2433
			// (get) Token: 0x060061DE RID: 25054 RVA: 0x006D4DC0 File Offset: 0x006D2FC0
			// (set) Token: 0x060061DF RID: 25055 RVA: 0x006D4DC8 File Offset: 0x006D2FC8
			public PowerPermissionLevel CurrentPermissionLevel { get; set; }

			// Token: 0x17000982 RID: 2434
			// (get) Token: 0x060061E0 RID: 25056 RVA: 0x006D4DD1 File Offset: 0x006D2FD1
			// (set) Token: 0x060061E1 RID: 25057 RVA: 0x006D4DD9 File Offset: 0x006D2FD9
			public PowerPermissionLevel DefaultPermissionLevel { get; set; }

			// Token: 0x17000983 RID: 2435
			// (get) Token: 0x060061E2 RID: 25058 RVA: 0x006D4DE2 File Offset: 0x006D2FE2
			// (set) Token: 0x060061E3 RID: 25059 RVA: 0x006D4DEA File Offset: 0x006D2FEA
			public bool Enabled { get; private set; }

			// Token: 0x060061E4 RID: 25060 RVA: 0x006D4DF3 File Offset: 0x006D2FF3
			public void SetPowerInfo(bool enabled)
			{
				this.Enabled = enabled;
			}

			// Token: 0x060061E5 RID: 25061 RVA: 0x006D4DFC File Offset: 0x006D2FFC
			public void Reset()
			{
				this.Enabled = false;
			}

			// Token: 0x060061E6 RID: 25062 RVA: 0x006D4E08 File Offset: 0x006D3008
			public void OnPlayerJoining(int playerIndex)
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
				packet.Writer.Write(this.Enabled);
				NetManager.Instance.SendToClient(packet, playerIndex);
			}

			// Token: 0x060061E7 RID: 25063 RVA: 0x006D4E40 File Offset: 0x006D3040
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				bool powerInfo = reader.ReadBoolean();
				if (Main.netMode != 2 || CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					this.SetPowerInfo(powerInfo);
					if (Main.netMode == 2)
					{
						NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
						packet.Writer.Write(this.Enabled);
						NetManager.Instance.Broadcast(packet, -1);
					}
				}
			}

			// Token: 0x060061E8 RID: 25064 RVA: 0x006D4EA0 File Offset: 0x006D30A0
			private void RequestUse()
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
				packet.Writer.Write(!this.Enabled);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060061E9 RID: 25065 RVA: 0x006D4EDC File Offset: 0x006D30DC
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				GroupOptionButton<bool> groupOptionButton = CreativePowersHelper.CreateToggleButton(info);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, Main.OurFavoriteColor);
				this.CustomizeButton(groupOptionButton);
				groupOptionButton.OnLeftClick += this.button_OnClick;
				groupOptionButton.OnUpdate += this.button_OnUpdate;
				elements.Add(groupOptionButton);
			}

			// Token: 0x060061EA RID: 25066 RVA: 0x006D4F30 File Offset: 0x006D3130
			private void button_OnUpdate(UIElement affectedElement)
			{
				bool enabled = this.Enabled;
				GroupOptionButton<bool> groupOptionButton = affectedElement as GroupOptionButton<bool>;
				groupOptionButton.SetCurrentOption(enabled);
				if (affectedElement.IsMouseHovering)
				{
					string buttonTextKey = this.GetButtonTextKey();
					string originalText = Language.GetTextValue(buttonTextKey + (groupOptionButton.IsSelected ? "_Enabled" : "_Disabled"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref originalText, buttonTextKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref originalText, this.GetIsUnlocked(), buttonTextKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref originalText);
					Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x060061EB RID: 25067 RVA: 0x006D4FC5 File Offset: 0x006D31C5
			private void button_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				if (CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					this.RequestUse();
				}
			}

			// Token: 0x060061EC RID: 25068
			internal abstract void CustomizeButton(UIElement button);

			// Token: 0x060061ED RID: 25069
			internal abstract string GetButtonTextKey();

			// Token: 0x060061EE RID: 25070
			public abstract bool GetIsUnlocked();
		}

		// Token: 0x02000CE0 RID: 3296
		public abstract class ASharedSliderPower : ICreativePower, IOnPlayerJoining, IProvideSliderElement, IPowerSubcategoryElement
		{
			// Token: 0x17000984 RID: 2436
			// (get) Token: 0x060061F0 RID: 25072 RVA: 0x006D4FE2 File Offset: 0x006D31E2
			// (set) Token: 0x060061F1 RID: 25073 RVA: 0x006D4FEA File Offset: 0x006D31EA
			public ushort PowerId { get; set; }

			// Token: 0x17000985 RID: 2437
			// (get) Token: 0x060061F2 RID: 25074 RVA: 0x006D4FF3 File Offset: 0x006D31F3
			// (set) Token: 0x060061F3 RID: 25075 RVA: 0x006D4FFB File Offset: 0x006D31FB
			public string ServerConfigName { get; set; }

			// Token: 0x17000986 RID: 2438
			// (get) Token: 0x060061F4 RID: 25076 RVA: 0x006D5004 File Offset: 0x006D3204
			// (set) Token: 0x060061F5 RID: 25077 RVA: 0x006D500C File Offset: 0x006D320C
			public PowerPermissionLevel CurrentPermissionLevel { get; set; }

			// Token: 0x17000987 RID: 2439
			// (get) Token: 0x060061F6 RID: 25078 RVA: 0x006D5015 File Offset: 0x006D3215
			// (set) Token: 0x060061F7 RID: 25079 RVA: 0x006D501D File Offset: 0x006D321D
			public PowerPermissionLevel DefaultPermissionLevel { get; set; }

			// Token: 0x060061F8 RID: 25080 RVA: 0x006D5028 File Offset: 0x006D3228
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				float num = reader.ReadSingle();
				if (Main.netMode != 2 || CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					this._sliderCurrentValueCache = num;
					this.UpdateInfoFromSliderValueCache();
					if (Main.netMode == 2)
					{
						NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
						packet.Writer.Write(num);
						NetManager.Instance.Broadcast(packet, -1);
					}
				}
			}

			// Token: 0x060061F9 RID: 25081
			internal abstract void UpdateInfoFromSliderValueCache();

			// Token: 0x060061FA RID: 25082 RVA: 0x006D5088 File Offset: 0x006D3288
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060061FB RID: 25083 RVA: 0x006D5090 File Offset: 0x006D3290
			public void DebugCall()
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
				packet.Writer.Write(0f);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060061FC RID: 25084
			public abstract UIElement ProvideSlider();

			// Token: 0x060061FD RID: 25085 RVA: 0x006D50C6 File Offset: 0x006D32C6
			internal float GetSliderValue()
			{
				if (Main.netMode == 1 && this._needsToCommitChange)
				{
					return this._currentTargetValue;
				}
				return this.GetSliderValueInner();
			}

			// Token: 0x060061FE RID: 25086 RVA: 0x006D50E5 File Offset: 0x006D32E5
			internal virtual float GetSliderValueInner()
			{
				return this._sliderCurrentValueCache;
			}

			// Token: 0x060061FF RID: 25087 RVA: 0x006D50ED File Offset: 0x006D32ED
			internal void SetValueKeyboard(float value)
			{
				if (value != this._currentTargetValue)
				{
					this.SetValueKeyboardForced(value);
				}
			}

			// Token: 0x06006200 RID: 25088 RVA: 0x006D50FF File Offset: 0x006D32FF
			internal void SetValueKeyboardForced(float value)
			{
				if (CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					this._currentTargetValue = value;
					this._needsToCommitChange = true;
				}
			}

			// Token: 0x06006201 RID: 25089 RVA: 0x006D511C File Offset: 0x006D331C
			internal void SetValueGamepad()
			{
				float sliderValue = this.GetSliderValue();
				float num = UILinksInitializer.HandleSliderVerticalInput(sliderValue, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				if (num != sliderValue)
				{
					this.SetValueKeyboard(num);
				}
			}

			// Token: 0x06006202 RID: 25090 RVA: 0x006D515C File Offset: 0x006D335C
			public GroupOptionButton<int> GetOptionButton(CreativePowerUIElementRequestInfo info, int optionIndex, int currentOptionIndex)
			{
				GroupOptionButton<int> groupOptionButton = CreativePowersHelper.CreateCategoryButton<int>(info, optionIndex, currentOptionIndex);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, CreativePowersHelper.CommonSelectedColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnUpdate += this.categoryButton_OnUpdate;
				return groupOptionButton;
			}

			// Token: 0x06006203 RID: 25091 RVA: 0x006D51A4 File Offset: 0x006D33A4
			private void categoryButton_OnUpdate(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					GroupOptionButton<int> groupOptionButton = affectedElement as GroupOptionButton<int>;
					string originalText = Language.GetTextValue(this._powerNameKey + (groupOptionButton.IsSelected ? "_Opened" : "_Closed"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref originalText, this._powerNameKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref originalText, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref originalText);
					Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
				}
				this.AttemptPushingChange();
			}

			// Token: 0x06006204 RID: 25092 RVA: 0x006D523C File Offset: 0x006D343C
			private void AttemptPushingChange()
			{
				if (this._needsToCommitChange && DateTime.UtcNow.CompareTo(this._nextTimeWeCanPush) != -1)
				{
					this._needsToCommitChange = false;
					this._sliderCurrentValueCache = this._currentTargetValue;
					this._nextTimeWeCanPush = DateTime.UtcNow;
					NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
					packet.Writer.Write(this._currentTargetValue);
					NetManager.Instance.SendToServerOrLoopback(packet);
				}
			}

			// Token: 0x06006205 RID: 25093 RVA: 0x006D52AF File Offset: 0x006D34AF
			public virtual void Reset()
			{
				this._sliderCurrentValueCache = 0f;
			}

			// Token: 0x06006206 RID: 25094 RVA: 0x006D52BC File Offset: 0x006D34BC
			public void OnPlayerJoining(int playerIndex)
			{
				if (this._syncToJoiningPlayers)
				{
					NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
					packet.Writer.Write(this._sliderCurrentValueCache);
					NetManager.Instance.SendToClient(packet, playerIndex);
				}
			}

			// Token: 0x06006207 RID: 25095
			public abstract bool GetIsUnlocked();

			// Token: 0x04007A68 RID: 31336
			internal Point _iconLocation;

			// Token: 0x04007A69 RID: 31337
			internal float _sliderCurrentValueCache;

			// Token: 0x04007A6A RID: 31338
			internal string _powerNameKey;

			// Token: 0x04007A6B RID: 31339
			internal bool _syncToJoiningPlayers = true;

			// Token: 0x04007A6C RID: 31340
			internal float _currentTargetValue;

			// Token: 0x04007A6D RID: 31341
			private bool _needsToCommitChange;

			// Token: 0x04007A6E RID: 31342
			private DateTime _nextTimeWeCanPush = DateTime.UtcNow;
		}

		// Token: 0x02000CE1 RID: 3297
		public class GodmodePower : CreativePowers.APerPlayerTogglePower, IPersistentPerPlayerContent
		{
			// Token: 0x06006209 RID: 25097 RVA: 0x006D5316 File Offset: 0x006D3516
			public GodmodePower()
			{
				this._powerNameKey = "CreativePowers.Godmode";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.Godmode;
			}

			// Token: 0x0600620A RID: 25098 RVA: 0x006D5334 File Offset: 0x006D3534
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x0600620B RID: 25099 RVA: 0x006D5338 File Offset: 0x006D3538
			public void Save(Player player, BinaryWriter writer)
			{
				bool value = base.IsEnabledForPlayer(Main.myPlayer);
				writer.Write(value);
			}

			// Token: 0x0600620C RID: 25100 RVA: 0x006D5358 File Offset: 0x006D3558
			public void ResetDataForNewPlayer(Player player)
			{
				player.savedPerPlayerFieldsThatArentInThePlayerClass.godmodePowerEnabled = this._defaultToggleState;
			}

			// Token: 0x0600620D RID: 25101 RVA: 0x006D536C File Offset: 0x006D356C
			public void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool godmodePowerEnabled = reader.ReadBoolean();
				player.savedPerPlayerFieldsThatArentInThePlayerClass.godmodePowerEnabled = godmodePowerEnabled;
			}

			// Token: 0x0600620E RID: 25102 RVA: 0x006D538C File Offset: 0x006D358C
			public void ApplyLoadedDataToOutOfPlayerFields(Player player)
			{
				if (player.savedPerPlayerFieldsThatArentInThePlayerClass.godmodePowerEnabled != base.IsEnabledForPlayer(player.whoAmI))
				{
					base.RequestUse();
				}
			}
		}

		// Token: 0x02000CE2 RID: 3298
		public class FarPlacementRangePower : CreativePowers.APerPlayerTogglePower, IPersistentPerPlayerContent
		{
			// Token: 0x0600620F RID: 25103 RVA: 0x006D53AD File Offset: 0x006D35AD
			public FarPlacementRangePower()
			{
				this._powerNameKey = "CreativePowers.InfinitePlacementRange";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.BlockPlacementRange;
				this._defaultToggleState = true;
			}

			// Token: 0x06006210 RID: 25104 RVA: 0x006D53D2 File Offset: 0x006D35D2
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06006211 RID: 25105 RVA: 0x006D53D8 File Offset: 0x006D35D8
			public void Save(Player player, BinaryWriter writer)
			{
				bool value = base.IsEnabledForPlayer(Main.myPlayer);
				writer.Write(value);
			}

			// Token: 0x06006212 RID: 25106 RVA: 0x006D53F8 File Offset: 0x006D35F8
			public void ResetDataForNewPlayer(Player player)
			{
				player.savedPerPlayerFieldsThatArentInThePlayerClass.farPlacementRangePowerEnabled = this._defaultToggleState;
			}

			// Token: 0x06006213 RID: 25107 RVA: 0x006D540C File Offset: 0x006D360C
			public void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool farPlacementRangePowerEnabled = reader.ReadBoolean();
				player.savedPerPlayerFieldsThatArentInThePlayerClass.farPlacementRangePowerEnabled = farPlacementRangePowerEnabled;
			}

			// Token: 0x06006214 RID: 25108 RVA: 0x006D542C File Offset: 0x006D362C
			public void ApplyLoadedDataToOutOfPlayerFields(Player player)
			{
				if (player.savedPerPlayerFieldsThatArentInThePlayerClass.farPlacementRangePowerEnabled != base.IsEnabledForPlayer(player.whoAmI))
				{
					base.RequestUse();
				}
			}
		}

		// Token: 0x02000CE3 RID: 3299
		public class StartDayImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x06006215 RID: 25109 RVA: 0x006D544D File Offset: 0x006D364D
			internal override void UsePower()
			{
				if (Main.netMode != 1)
				{
					Main.SkipToTime(0, true);
				}
			}

			// Token: 0x06006216 RID: 25110 RVA: 0x006D545E File Offset: 0x006D365E
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartDayImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeDawn;
			}

			// Token: 0x06006217 RID: 25111 RVA: 0x006D548C File Offset: 0x006D368C
			public override bool GetIsUnlocked()
			{
				return true;
			}
		}

		// Token: 0x02000CE4 RID: 3300
		public class StartNightImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x06006219 RID: 25113 RVA: 0x006D5497 File Offset: 0x006D3697
			internal override void UsePower()
			{
				if (Main.netMode != 1)
				{
					Main.SkipToTime(0, false);
				}
			}

			// Token: 0x0600621A RID: 25114 RVA: 0x006D54A8 File Offset: 0x006D36A8
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartNightImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeDusk;
			}

			// Token: 0x0600621B RID: 25115 RVA: 0x006D54D6 File Offset: 0x006D36D6
			public override bool GetIsUnlocked()
			{
				return true;
			}
		}

		// Token: 0x02000CE5 RID: 3301
		public class StartNoonImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x0600621D RID: 25117 RVA: 0x006D54E1 File Offset: 0x006D36E1
			internal override void UsePower()
			{
				if (Main.netMode != 1)
				{
					Main.SkipToTime(27000, true);
				}
			}

			// Token: 0x0600621E RID: 25118 RVA: 0x006D54F6 File Offset: 0x006D36F6
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartNoonImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeNoon;
			}

			// Token: 0x0600621F RID: 25119 RVA: 0x006D5524 File Offset: 0x006D3724
			public override bool GetIsUnlocked()
			{
				return true;
			}
		}

		// Token: 0x02000CE6 RID: 3302
		public class StartMidnightImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x06006221 RID: 25121 RVA: 0x006D552F File Offset: 0x006D372F
			internal override void UsePower()
			{
				if (Main.netMode != 1)
				{
					Main.SkipToTime(16200, false);
				}
			}

			// Token: 0x06006222 RID: 25122 RVA: 0x006D5544 File Offset: 0x006D3744
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartMidnightImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeMidnight;
			}

			// Token: 0x06006223 RID: 25123 RVA: 0x006D5572 File Offset: 0x006D3772
			public override bool GetIsUnlocked()
			{
				return true;
			}
		}

		// Token: 0x02000CE7 RID: 3303
		public class ModifyTimeRate : CreativePowers.ASharedSliderPower, IPersistentPerWorldContent
		{
			// Token: 0x17000988 RID: 2440
			// (get) Token: 0x06006225 RID: 25125 RVA: 0x006D557D File Offset: 0x006D377D
			// (set) Token: 0x06006226 RID: 25126 RVA: 0x006D5585 File Offset: 0x006D3785
			public int TargetTimeRate { get; private set; }

			// Token: 0x06006227 RID: 25127 RVA: 0x006D558E File Offset: 0x006D378E
			public ModifyTimeRate()
			{
				this._powerNameKey = "CreativePowers.ModifyTimeRate";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.ModifyTime;
			}

			// Token: 0x06006228 RID: 25128 RVA: 0x006D55AC File Offset: 0x006D37AC
			public override void Reset()
			{
				this._sliderCurrentValueCache = 0f;
				this.TargetTimeRate = 1;
			}

			// Token: 0x06006229 RID: 25129 RVA: 0x006D55C0 File Offset: 0x006D37C0
			internal override void UpdateInfoFromSliderValueCache()
			{
				this.TargetTimeRate = (int)Math.Round((double)Utils.Remap(this._sliderCurrentValueCache, 0f, 1f, 1f, 24f, true));
			}

			// Token: 0x0600622A RID: 25130 RVA: 0x006D55F0 File Offset: 0x006D37F0
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uIVerticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uIVerticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(87f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uIVerticalSlider);
				UIElement.ElementEvent value;
				if ((value = CreativePowers.ModifyTimeRate.<>O.<0>__UpdateUseMouseInterface) == null)
				{
					value = (CreativePowers.ModifyTimeRate.<>O.<0>__UpdateUseMouseInterface = new UIElement.ElementEvent(CreativePowersHelper.UpdateUseMouseInterface));
				}
				uipanel.OnUpdate += value;
				UIText uIText = new UIText("x24", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uIText.OnMouseOver += this.Button_OnMouseOver;
				uIText.OnMouseOut += this.Button_OnMouseOut;
				uIText.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uIText);
				UIText uIText2 = new UIText("x12", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uIText2.OnMouseOver += this.Button_OnMouseOver;
				uIText2.OnMouseOut += this.Button_OnMouseOut;
				uIText2.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uIText2);
				UIText uIText3 = new UIText("x1", 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uIText3.OnMouseOver += this.Button_OnMouseOver;
				uIText3.OnMouseOut += this.Button_OnMouseOut;
				uIText3.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uIText3);
				return uipanel;
			}

			// Token: 0x0600622B RID: 25131 RVA: 0x006D57D2 File Offset: 0x006D39D2
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600622C RID: 25132 RVA: 0x006D57F4 File Offset: 0x006D39F4
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600622D RID: 25133 RVA: 0x006D5816 File Offset: 0x006D3A16
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600622E RID: 25134 RVA: 0x006D5838 File Offset: 0x006D3A38
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uIText = listeningElement as UIText;
				if (uIText != null)
				{
					uIText.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600622F RID: 25135 RVA: 0x006D5870 File Offset: 0x006D3A70
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uIText = listeningElement as UIText;
				if (uIText != null)
				{
					uIText.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006230 RID: 25136 RVA: 0x006D58A7 File Offset: 0x006D3AA7
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06006231 RID: 25137 RVA: 0x006D58AA File Offset: 0x006D3AAA
			public void Save(BinaryWriter writer)
			{
				writer.Write(this._sliderCurrentValueCache);
			}

			// Token: 0x06006232 RID: 25138 RVA: 0x006D58B8 File Offset: 0x006D3AB8
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				this._sliderCurrentValueCache = reader.ReadSingle();
				this.UpdateInfoFromSliderValueCache();
			}

			// Token: 0x06006233 RID: 25139 RVA: 0x006D58CC File Offset: 0x006D3ACC
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadSingle();
			}

			// Token: 0x06006234 RID: 25140 RVA: 0x006D58D8 File Offset: 0x006D3AD8
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string originalText = "x" + this.TargetTimeRate.ToString();
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref originalText);
					Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x02000E5C RID: 3676
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04007D57 RID: 32087
				public static UIElement.ElementEvent <0>__UpdateUseMouseInterface;
			}
		}

		// Token: 0x02000CE8 RID: 3304
		public class DifficultySliderPower : CreativePowers.ASharedSliderPower, IPersistentPerWorldContent
		{
			// Token: 0x17000989 RID: 2441
			// (get) Token: 0x06006235 RID: 25141 RVA: 0x006D5920 File Offset: 0x006D3B20
			// (set) Token: 0x06006236 RID: 25142 RVA: 0x006D5928 File Offset: 0x006D3B28
			public float StrengthMultiplierToGiveNPCs { get; private set; }

			// Token: 0x06006237 RID: 25143 RVA: 0x006D5931 File Offset: 0x006D3B31
			public DifficultySliderPower()
			{
				this._powerNameKey = "CreativePowers.DifficultySlider";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.EnemyStrengthSlider;
			}

			// Token: 0x06006238 RID: 25144 RVA: 0x006D594F File Offset: 0x006D3B4F
			public override void Reset()
			{
				this._sliderCurrentValueCache = 0f;
				this.UpdateInfoFromSliderValueCache();
			}

			// Token: 0x06006239 RID: 25145 RVA: 0x006D5964 File Offset: 0x006D3B64
			internal override void UpdateInfoFromSliderValueCache()
			{
				if (this._sliderCurrentValueCache <= 0.33f)
				{
					this.StrengthMultiplierToGiveNPCs = Utils.Remap(this._sliderCurrentValueCache, 0f, 0.33f, 0.5f, 1f, true);
				}
				else
				{
					this.StrengthMultiplierToGiveNPCs = Utils.Remap(this._sliderCurrentValueCache, 0.33f, 1f, 1f, 3f, true);
				}
				float strengthMultiplierToGiveNPCs = (float)Math.Round((double)(this.StrengthMultiplierToGiveNPCs * 20f)) / 20f;
				this.StrengthMultiplierToGiveNPCs = strengthMultiplierToGiveNPCs;
			}

			// Token: 0x0600623A RID: 25146 RVA: 0x006D59F0 File Offset: 0x006D3BF0
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uIVerticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(82f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uIVerticalSlider);
				UIElement.ElementEvent value;
				if ((value = CreativePowers.DifficultySliderPower.<>O.<0>__UpdateUseMouseInterface) == null)
				{
					value = (CreativePowers.DifficultySliderPower.<>O.<0>__UpdateUseMouseInterface = new UIElement.ElementEvent(CreativePowersHelper.UpdateUseMouseInterface));
				}
				uipanel.OnUpdate += value;
				uIVerticalSlider.OnUpdate += this.UpdateSliderColorAndShowMultiplierMouseOver;
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 0f, "x3", "Images/UI/WorldCreation/IconDifficultyMaster", new UIElement.ElementEvent(this.MouseOver_Master), new UIElement.MouseEvent(this.Click_Master));
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 0.33333334f, "x2", "Images/UI/WorldCreation/IconDifficultyExpert", new UIElement.ElementEvent(this.MouseOver_Expert), new UIElement.MouseEvent(this.Click_Expert));
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 0.6666667f, "x1", "Images/UI/WorldCreation/IconDifficultyNormal", new UIElement.ElementEvent(this.MouseOver_Normal), new UIElement.MouseEvent(this.Click_Normal));
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 1f, "x0.5", "Images/UI/WorldCreation/IconDifficultyCreative", new UIElement.ElementEvent(this.MouseOver_Journey), new UIElement.MouseEvent(this.Click_Journey));
				return uipanel;
			}

			// Token: 0x0600623B RID: 25147 RVA: 0x006D5B5A File Offset: 0x006D3D5A
			private void Click_Master(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600623C RID: 25148 RVA: 0x006D5B7C File Offset: 0x006D3D7C
			private void Click_Expert(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.66f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600623D RID: 25149 RVA: 0x006D5B9E File Offset: 0x006D3D9E
			private void Click_Normal(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.33f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600623E RID: 25150 RVA: 0x006D5BC0 File Offset: 0x006D3DC0
			private void Click_Journey(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600623F RID: 25151 RVA: 0x006D5BE4 File Offset: 0x006D3DE4
			private static void AddIndication(UIPanel panel, float yAnchor, string indicationText, string iconImagePath, UIElement.ElementEvent updateEvent, UIElement.MouseEvent clickEvent)
			{
				UIImage uIImage = new UIImage(Main.Assets.Request<Texture2D>(iconImagePath))
				{
					HAlign = 1f,
					VAlign = yAnchor,
					Left = new StyleDimension(4f, 0f),
					Top = new StyleDimension(2f, 0f),
					RemoveFloatingPointsFromDrawPosition = true
				};
				UIElement uielement = uIImage;
				UIElement.MouseEvent value;
				if ((value = CreativePowers.DifficultySliderPower.<>O.<1>__Button_OnMouseOut) == null)
				{
					value = (CreativePowers.DifficultySliderPower.<>O.<1>__Button_OnMouseOut = new UIElement.MouseEvent(CreativePowers.DifficultySliderPower.Button_OnMouseOut));
				}
				uielement.OnMouseOut += value;
				UIElement uielement2 = uIImage;
				UIElement.MouseEvent value2;
				if ((value2 = CreativePowers.DifficultySliderPower.<>O.<2>__Button_OnMouseOver) == null)
				{
					value2 = (CreativePowers.DifficultySliderPower.<>O.<2>__Button_OnMouseOver = new UIElement.MouseEvent(CreativePowers.DifficultySliderPower.Button_OnMouseOver));
				}
				uielement2.OnMouseOver += value2;
				if (updateEvent != null)
				{
					uIImage.OnUpdate += updateEvent;
				}
				if (clickEvent != null)
				{
					uIImage.OnLeftClick += clickEvent;
				}
				panel.Append(uIImage);
			}

			// Token: 0x06006240 RID: 25152 RVA: 0x006D5CA6 File Offset: 0x006D3EA6
			private static void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006241 RID: 25153 RVA: 0x006D5CBD File Offset: 0x006D3EBD
			private static void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006242 RID: 25154 RVA: 0x006D5CD4 File Offset: 0x006D3ED4
			private void MouseOver_Journey(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Creative");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x06006243 RID: 25155 RVA: 0x006D5D08 File Offset: 0x006D3F08
			private void MouseOver_Normal(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Normal");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x06006244 RID: 25156 RVA: 0x006D5D3C File Offset: 0x006D3F3C
			private void MouseOver_Expert(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Expert");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x06006245 RID: 25157 RVA: 0x006D5D70 File Offset: 0x006D3F70
			private void MouseOver_Master(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Master");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x06006246 RID: 25158 RVA: 0x006D5DA4 File Offset: 0x006D3FA4
			private void UpdateSliderColorAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string originalText = "x" + this.StrengthMultiplierToGiveNPCs.ToString("F2");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref originalText);
					Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
				}
				UIVerticalSlider uIVerticalSlider = affectedElement as UIVerticalSlider;
				if (uIVerticalSlider != null)
				{
					uIVerticalSlider.EmptyColor = Color.Black;
					Color filledColor = Main.masterMode ? Main.hcColor : (Main.expertMode ? Main.mcColor : ((this.StrengthMultiplierToGiveNPCs >= 1f) ? Color.White : Main.creativeModeColor));
					uIVerticalSlider.FilledColor = filledColor;
				}
			}

			// Token: 0x06006247 RID: 25159 RVA: 0x006D5E43 File Offset: 0x006D4043
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06006248 RID: 25160 RVA: 0x006D5E46 File Offset: 0x006D4046
			public void Save(BinaryWriter writer)
			{
				writer.Write(this._sliderCurrentValueCache);
			}

			// Token: 0x06006249 RID: 25161 RVA: 0x006D5E54 File Offset: 0x006D4054
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				this._sliderCurrentValueCache = reader.ReadSingle();
				this.UpdateInfoFromSliderValueCache();
			}

			// Token: 0x0600624A RID: 25162 RVA: 0x006D5E68 File Offset: 0x006D4068
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadSingle();
			}

			// Token: 0x02000E5D RID: 3677
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04007D58 RID: 32088
				public static UIElement.ElementEvent <0>__UpdateUseMouseInterface;

				// Token: 0x04007D59 RID: 32089
				public static UIElement.MouseEvent <1>__Button_OnMouseOut;

				// Token: 0x04007D5A RID: 32090
				public static UIElement.MouseEvent <2>__Button_OnMouseOver;
			}
		}

		// Token: 0x02000CE9 RID: 3305
		public class ModifyWindDirectionAndStrength : CreativePowers.ASharedSliderPower
		{
			// Token: 0x0600624B RID: 25163 RVA: 0x006D5E71 File Offset: 0x006D4071
			public ModifyWindDirectionAndStrength()
			{
				this._powerNameKey = "CreativePowers.ModifyWindDirectionAndStrength";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.WindDirection;
				this._syncToJoiningPlayers = false;
			}

			// Token: 0x0600624C RID: 25164 RVA: 0x006D5E96 File Offset: 0x006D4096
			internal override void UpdateInfoFromSliderValueCache()
			{
				Main.windSpeedCurrent = (Main.windSpeedTarget = MathHelper.Lerp(-0.8f, 0.8f, this._sliderCurrentValueCache));
			}

			// Token: 0x0600624D RID: 25165 RVA: 0x006D5EB8 File Offset: 0x006D40B8
			internal override float GetSliderValueInner()
			{
				return Utils.GetLerpValue(-0.8f, 0.8f, Main.windSpeedTarget, false);
			}

			// Token: 0x0600624E RID: 25166 RVA: 0x006D5ECF File Offset: 0x006D40CF
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x0600624F RID: 25167 RVA: 0x006D5ED4 File Offset: 0x006D40D4
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uIVerticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uIVerticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(132f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uIVerticalSlider);
				UIElement.ElementEvent value;
				if ((value = CreativePowers.ModifyWindDirectionAndStrength.<>O.<0>__UpdateUseMouseInterface) == null)
				{
					value = (CreativePowers.ModifyWindDirectionAndStrength.<>O.<0>__UpdateUseMouseInterface = new UIElement.ElementEvent(CreativePowersHelper.UpdateUseMouseInterface));
				}
				uipanel.OnUpdate += value;
				UIText uIText = new UIText(Language.GetText("CreativePowers.WindWest"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uIText.OnMouseOut += this.Button_OnMouseOut;
				uIText.OnMouseOver += this.Button_OnMouseOver;
				uIText.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uIText);
				UIText uIText2 = new UIText(Language.GetText("CreativePowers.WindEast"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uIText2.OnMouseOut += this.Button_OnMouseOut;
				uIText2.OnMouseOver += this.Button_OnMouseOver;
				uIText2.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uIText2);
				UIText uIText3 = new UIText(Language.GetText("CreativePowers.WindNone"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uIText3.OnMouseOut += this.Button_OnMouseOut;
				uIText3.OnMouseOver += this.Button_OnMouseOver;
				uIText3.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uIText3);
				return uipanel;
			}

			// Token: 0x06006250 RID: 25168 RVA: 0x006D60C5 File Offset: 0x006D42C5
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006251 RID: 25169 RVA: 0x006D60E7 File Offset: 0x006D42E7
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006252 RID: 25170 RVA: 0x006D6109 File Offset: 0x006D4309
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006253 RID: 25171 RVA: 0x006D612C File Offset: 0x006D432C
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uIText = listeningElement as UIText;
				if (uIText != null)
				{
					uIText.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006254 RID: 25172 RVA: 0x006D6164 File Offset: 0x006D4364
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uIText = listeningElement as UIText;
				if (uIText != null)
				{
					uIText.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006255 RID: 25173 RVA: 0x006D619C File Offset: 0x006D439C
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					int num = (int)(Main.windSpeedCurrent * 50f);
					string originalText = "";
					if (num < 0)
					{
						originalText += Language.GetTextValue("GameUI.EastWind", Math.Abs(num));
					}
					else if (num > 0)
					{
						originalText += Language.GetTextValue("GameUI.WestWind", num);
					}
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref originalText);
					Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x02000E5E RID: 3678
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04007D5B RID: 32091
				public static UIElement.ElementEvent <0>__UpdateUseMouseInterface;
			}
		}

		// Token: 0x02000CEA RID: 3306
		public class ModifyRainPower : CreativePowers.ASharedSliderPower
		{
			// Token: 0x06006256 RID: 25174 RVA: 0x006D621B File Offset: 0x006D441B
			public ModifyRainPower()
			{
				this._powerNameKey = "CreativePowers.ModifyRainPower";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.RainStrength;
				this._syncToJoiningPlayers = false;
			}

			// Token: 0x06006257 RID: 25175 RVA: 0x006D6240 File Offset: 0x006D4440
			internal override void UpdateInfoFromSliderValueCache()
			{
				if (this._sliderCurrentValueCache == 0f)
				{
					Main.StopRain();
				}
				else
				{
					Main.StartRain();
				}
				Main.cloudAlpha = this._sliderCurrentValueCache;
				Main.maxRaining = this._sliderCurrentValueCache;
			}

			// Token: 0x06006258 RID: 25176 RVA: 0x006D6271 File Offset: 0x006D4471
			internal override float GetSliderValueInner()
			{
				return Main.cloudAlpha;
			}

			// Token: 0x06006259 RID: 25177 RVA: 0x006D6278 File Offset: 0x006D4478
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x0600625A RID: 25178 RVA: 0x006D627C File Offset: 0x006D447C
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uIVerticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uIVerticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(132f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uIVerticalSlider);
				UIElement.ElementEvent value;
				if ((value = CreativePowers.ModifyRainPower.<>O.<0>__UpdateUseMouseInterface) == null)
				{
					value = (CreativePowers.ModifyRainPower.<>O.<0>__UpdateUseMouseInterface = new UIElement.ElementEvent(CreativePowersHelper.UpdateUseMouseInterface));
				}
				uipanel.OnUpdate += value;
				UIText uIText = new UIText(Language.GetText("CreativePowers.WeatherMonsoon"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uIText.OnMouseOut += this.Button_OnMouseOut;
				uIText.OnMouseOver += this.Button_OnMouseOver;
				uIText.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uIText);
				UIText uIText2 = new UIText(Language.GetText("CreativePowers.WeatherClearSky"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uIText2.OnMouseOut += this.Button_OnMouseOut;
				uIText2.OnMouseOver += this.Button_OnMouseOver;
				uIText2.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uIText2);
				UIText uIText3 = new UIText(Language.GetText("CreativePowers.WeatherDrizzle"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uIText3.OnMouseOut += this.Button_OnMouseOut;
				uIText3.OnMouseOver += this.Button_OnMouseOver;
				uIText3.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uIText3);
				return uipanel;
			}

			// Token: 0x0600625B RID: 25179 RVA: 0x006D646D File Offset: 0x006D466D
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600625C RID: 25180 RVA: 0x006D648F File Offset: 0x006D468F
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600625D RID: 25181 RVA: 0x006D64B1 File Offset: 0x006D46B1
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600625E RID: 25182 RVA: 0x006D64D4 File Offset: 0x006D46D4
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uIText = listeningElement as UIText;
				if (uIText != null)
				{
					uIText.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600625F RID: 25183 RVA: 0x006D650C File Offset: 0x006D470C
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uIText = listeningElement as UIText;
				if (uIText != null)
				{
					uIText.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006260 RID: 25184 RVA: 0x006D6544 File Offset: 0x006D4744
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string originalText = Main.maxRaining.ToString("P0");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref originalText);
					Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x02000E5F RID: 3679
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04007D5C RID: 32092
				public static UIElement.ElementEvent <0>__UpdateUseMouseInterface;
			}
		}

		// Token: 0x02000CEB RID: 3307
		public class FreezeTime : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x06006261 RID: 25185 RVA: 0x006D6583 File Offset: 0x006D4783
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.FreezeTime));
			}

			// Token: 0x06006262 RID: 25186 RVA: 0x006D6595 File Offset: 0x006D4795
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.FreezeTime";
			}

			// Token: 0x06006263 RID: 25187 RVA: 0x006D659C File Offset: 0x006D479C
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06006264 RID: 25188 RVA: 0x006D659F File Offset: 0x006D479F
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x06006265 RID: 25189 RVA: 0x006D65B0 File Offset: 0x006D47B0
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool powerInfo = reader.ReadBoolean();
				base.SetPowerInfo(powerInfo);
			}

			// Token: 0x06006266 RID: 25190 RVA: 0x006D65CB File Offset: 0x006D47CB
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}
		}

		// Token: 0x02000CEC RID: 3308
		public class FreezeWindDirectionAndStrength : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x06006268 RID: 25192 RVA: 0x006D65DC File Offset: 0x006D47DC
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.WindFreeze));
			}

			// Token: 0x06006269 RID: 25193 RVA: 0x006D65EE File Offset: 0x006D47EE
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.FreezeWindDirectionAndStrength";
			}

			// Token: 0x0600626A RID: 25194 RVA: 0x006D65F5 File Offset: 0x006D47F5
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x0600626B RID: 25195 RVA: 0x006D65F8 File Offset: 0x006D47F8
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x0600626C RID: 25196 RVA: 0x006D6608 File Offset: 0x006D4808
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool powerInfo = reader.ReadBoolean();
				base.SetPowerInfo(powerInfo);
			}

			// Token: 0x0600626D RID: 25197 RVA: 0x006D6623 File Offset: 0x006D4823
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}
		}

		// Token: 0x02000CED RID: 3309
		public class FreezeRainPower : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x0600626F RID: 25199 RVA: 0x006D6634 File Offset: 0x006D4834
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.RainFreeze));
			}

			// Token: 0x06006270 RID: 25200 RVA: 0x006D6646 File Offset: 0x006D4846
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.FreezeRainPower";
			}

			// Token: 0x06006271 RID: 25201 RVA: 0x006D664D File Offset: 0x006D484D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06006272 RID: 25202 RVA: 0x006D6650 File Offset: 0x006D4850
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x06006273 RID: 25203 RVA: 0x006D6660 File Offset: 0x006D4860
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool powerInfo = reader.ReadBoolean();
				base.SetPowerInfo(powerInfo);
			}

			// Token: 0x06006274 RID: 25204 RVA: 0x006D667B File Offset: 0x006D487B
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}
		}

		// Token: 0x02000CEE RID: 3310
		public class StopBiomeSpreadPower : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x06006276 RID: 25206 RVA: 0x006D668C File Offset: 0x006D488C
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.StopBiomeSpread));
			}

			// Token: 0x06006277 RID: 25207 RVA: 0x006D669E File Offset: 0x006D489E
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.StopBiomeSpread";
			}

			// Token: 0x06006278 RID: 25208 RVA: 0x006D66A5 File Offset: 0x006D48A5
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06006279 RID: 25209 RVA: 0x006D66A8 File Offset: 0x006D48A8
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x0600627A RID: 25210 RVA: 0x006D66B8 File Offset: 0x006D48B8
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool powerInfo = reader.ReadBoolean();
				base.SetPowerInfo(powerInfo);
			}

			// Token: 0x0600627B RID: 25211 RVA: 0x006D66D3 File Offset: 0x006D48D3
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}
		}

		// Token: 0x02000CEF RID: 3311
		public class SpawnRateSliderPerPlayerPower : CreativePowers.APerPlayerSliderPower, IPersistentPerPlayerContent
		{
			// Token: 0x1700098A RID: 2442
			// (get) Token: 0x0600627D RID: 25213 RVA: 0x006D66E4 File Offset: 0x006D48E4
			// (set) Token: 0x0600627E RID: 25214 RVA: 0x006D66EC File Offset: 0x006D48EC
			public float StrengthMultiplierToGiveNPCs { get; private set; }

			// Token: 0x0600627F RID: 25215 RVA: 0x006D66F5 File Offset: 0x006D48F5
			public SpawnRateSliderPerPlayerPower()
			{
				this._powerNameKey = "CreativePowers.NPCSpawnRateSlider";
				this._sliderDefaultValue = 0.5f;
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.EnemySpawnRate;
			}

			// Token: 0x06006280 RID: 25216 RVA: 0x006D671E File Offset: 0x006D491E
			public bool GetShouldDisableSpawnsFor(int playerIndex)
			{
				if (!this._cachePerPlayer.IndexInRange(playerIndex))
				{
					return false;
				}
				if (playerIndex == Main.myPlayer)
				{
					return this._sliderCurrentValueCache == 0f;
				}
				return this._cachePerPlayer[playerIndex] == 0f;
			}

			// Token: 0x06006281 RID: 25217 RVA: 0x006D6755 File Offset: 0x006D4955
			internal override void UpdateInfoFromSliderValueCache()
			{
			}

			// Token: 0x06006282 RID: 25218 RVA: 0x006D6758 File Offset: 0x006D4958
			public override float RemapSliderValueToPowerValue(float sliderValue)
			{
				if (sliderValue < 0.5f)
				{
					return Utils.Remap(sliderValue, 0f, 0.5f, 0.1f, 1f, true);
				}
				return Utils.Remap(sliderValue, 0.5f, 1f, 1f, 10f, true);
			}

			// Token: 0x06006283 RID: 25219 RVA: 0x006D67A4 File Offset: 0x006D49A4
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uIVerticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uIVerticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(77f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uIVerticalSlider);
				UIElement.ElementEvent value;
				if ((value = CreativePowers.SpawnRateSliderPerPlayerPower.<>O.<0>__UpdateUseMouseInterface) == null)
				{
					value = (CreativePowers.SpawnRateSliderPerPlayerPower.<>O.<0>__UpdateUseMouseInterface = new UIElement.ElementEvent(CreativePowersHelper.UpdateUseMouseInterface));
				}
				uipanel.OnUpdate += value;
				UIText uIText = new UIText("x10", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uIText.OnMouseOut += this.Button_OnMouseOut;
				uIText.OnMouseOver += this.Button_OnMouseOver;
				uIText.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uIText);
				UIText uIText2 = new UIText("x1", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uIText2.OnMouseOut += this.Button_OnMouseOut;
				uIText2.OnMouseOver += this.Button_OnMouseOver;
				uIText2.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uIText2);
				UIText uIText3 = new UIText("x0", 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uIText3.OnMouseOut += this.Button_OnMouseOut;
				uIText3.OnMouseOver += this.Button_OnMouseOver;
				uIText3.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uIText3);
				return uipanel;
			}

			// Token: 0x06006284 RID: 25220 RVA: 0x006D6988 File Offset: 0x006D4B88
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uIText = listeningElement as UIText;
				if (uIText != null)
				{
					uIText.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006285 RID: 25221 RVA: 0x006D69C0 File Offset: 0x006D4BC0
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uIText = listeningElement as UIText;
				if (uIText != null)
				{
					uIText.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006286 RID: 25222 RVA: 0x006D69F7 File Offset: 0x006D4BF7
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboard(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006287 RID: 25223 RVA: 0x006D6A19 File Offset: 0x006D4C19
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboard(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006288 RID: 25224 RVA: 0x006D6A3B File Offset: 0x006D4C3B
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboard(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06006289 RID: 25225 RVA: 0x006D6A60 File Offset: 0x006D4C60
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string originalText = "x" + this.RemapSliderValueToPowerValue(base.GetSliderValue()).ToString("F2");
					if (this.GetShouldDisableSpawnsFor(Main.myPlayer))
					{
						originalText = Language.GetTextValue(this._powerNameKey + "EnemySpawnsDisabled");
					}
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref originalText);
					Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600628A RID: 25226 RVA: 0x006D6AD6 File Offset: 0x006D4CD6
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x0600628B RID: 25227 RVA: 0x006D6ADC File Offset: 0x006D4CDC
			public void Save(Player player, BinaryWriter writer)
			{
				float sliderCurrentValueCache = this._sliderCurrentValueCache;
				writer.Write(sliderCurrentValueCache);
			}

			// Token: 0x0600628C RID: 25228 RVA: 0x006D6AF7 File Offset: 0x006D4CF7
			public void ResetDataForNewPlayer(Player player)
			{
				player.savedPerPlayerFieldsThatArentInThePlayerClass.spawnRatePowerSliderValue = this._sliderDefaultValue;
			}

			// Token: 0x0600628D RID: 25229 RVA: 0x006D6B0C File Offset: 0x006D4D0C
			public void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				float spawnRatePowerSliderValue = reader.ReadSingle();
				player.savedPerPlayerFieldsThatArentInThePlayerClass.spawnRatePowerSliderValue = spawnRatePowerSliderValue;
			}

			// Token: 0x0600628E RID: 25230 RVA: 0x006D6B2C File Offset: 0x006D4D2C
			public void ApplyLoadedDataToOutOfPlayerFields(Player player)
			{
				base.PushChangeAndSetSlider(player.savedPerPlayerFieldsThatArentInThePlayerClass.spawnRatePowerSliderValue);
			}

			// Token: 0x02000E60 RID: 3680
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04007D5D RID: 32093
				public static UIElement.ElementEvent <0>__UpdateUseMouseInterface;
			}
		}
	}
}
