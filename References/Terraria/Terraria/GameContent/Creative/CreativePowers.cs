using System;
using System.Collections.Generic;
using System.IO;
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
	// Token: 0x020002C3 RID: 707
	public class CreativePowers
	{
		// Token: 0x020006B0 RID: 1712
		public abstract class APerPlayerTogglePower : ICreativePower, IOnPlayerJoining
		{
			// Token: 0x170003D6 RID: 982
			// (get) Token: 0x06003580 RID: 13696 RVA: 0x0060A2DC File Offset: 0x006084DC
			// (set) Token: 0x06003581 RID: 13697 RVA: 0x0060A2E4 File Offset: 0x006084E4
			public ushort PowerId { get; set; }

			// Token: 0x170003D7 RID: 983
			// (get) Token: 0x06003582 RID: 13698 RVA: 0x0060A2ED File Offset: 0x006084ED
			// (set) Token: 0x06003583 RID: 13699 RVA: 0x0060A2F5 File Offset: 0x006084F5
			public string ServerConfigName { get; set; }

			// Token: 0x170003D8 RID: 984
			// (get) Token: 0x06003584 RID: 13700 RVA: 0x0060A2FE File Offset: 0x006084FE
			// (set) Token: 0x06003585 RID: 13701 RVA: 0x0060A306 File Offset: 0x00608506
			public PowerPermissionLevel CurrentPermissionLevel { get; set; }

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x06003586 RID: 13702 RVA: 0x0060A30F File Offset: 0x0060850F
			// (set) Token: 0x06003587 RID: 13703 RVA: 0x0060A317 File Offset: 0x00608517
			public PowerPermissionLevel DefaultPermissionLevel { get; set; }

			// Token: 0x06003588 RID: 13704 RVA: 0x0060A320 File Offset: 0x00608520
			public bool IsEnabledForPlayer(int playerIndex)
			{
				return this._perPlayerIsEnabled.IndexInRange(playerIndex) && this._perPlayerIsEnabled[playerIndex];
			}

			// Token: 0x06003589 RID: 13705 RVA: 0x0060A33C File Offset: 0x0060853C
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				CreativePowers.APerPlayerTogglePower.SubMessageType subMessageType = (CreativePowers.APerPlayerTogglePower.SubMessageType)reader.ReadByte();
				if (subMessageType == CreativePowers.APerPlayerTogglePower.SubMessageType.SyncEveryone)
				{
					this.Deserialize_SyncEveryone(reader, userId);
					return;
				}
				if (subMessageType != CreativePowers.APerPlayerTogglePower.SubMessageType.SyncOnePlayer)
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

			// Token: 0x0600358A RID: 13706 RVA: 0x0060A38C File Offset: 0x0060858C
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

			// Token: 0x0600358B RID: 13707 RVA: 0x0060A41C File Offset: 0x0060861C
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

			// Token: 0x0600358C RID: 13708 RVA: 0x0060A47B File Offset: 0x0060867B
			public void DebugCall()
			{
				this.RequestUse();
			}

			// Token: 0x0600358D RID: 13709 RVA: 0x0060A484 File Offset: 0x00608684
			internal void RequestUse()
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
				packet.Writer.Write(1);
				packet.Writer.Write((byte)Main.myPlayer);
				packet.Writer.Write(!this._perPlayerIsEnabled[Main.myPlayer]);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x0600358E RID: 13710 RVA: 0x0060A4E4 File Offset: 0x006086E4
			public void Reset()
			{
				for (int i = 0; i < this._perPlayerIsEnabled.Length; i++)
				{
					this._perPlayerIsEnabled[i] = this._defaultToggleState;
				}
			}

			// Token: 0x0600358F RID: 13711 RVA: 0x0060A514 File Offset: 0x00608714
			public void OnPlayerJoining(int playerIndex)
			{
				int num = (int)Math.Ceiling((double)((float)this._perPlayerIsEnabled.Length / 8f));
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, num + 1);
				packet.Writer.Write(0);
				for (int i = 0; i < num; i++)
				{
					BitsByte bb = 0;
					for (int j = 0; j < 8; j++)
					{
						int num2 = i * 8 + j;
						if (num2 >= this._perPlayerIsEnabled.Length)
						{
							break;
						}
						bb[j] = this._perPlayerIsEnabled[num2];
					}
					packet.Writer.Write(bb);
				}
				NetManager.Instance.SendToClient(packet, playerIndex);
			}

			// Token: 0x06003590 RID: 13712 RVA: 0x0060A5BC File Offset: 0x006087BC
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				GroupOptionButton<bool> groupOptionButton = CreativePowersHelper.CreateToggleButton(info);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, Main.OurFavoriteColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnLeftClick += this.button_OnClick;
				groupOptionButton.OnUpdate += this.button_OnUpdate;
				elements.Add(groupOptionButton);
			}

			// Token: 0x06003591 RID: 13713 RVA: 0x0060A618 File Offset: 0x00608818
			private void button_OnUpdate(UIElement affectedElement)
			{
				bool currentOption = this._perPlayerIsEnabled[Main.myPlayer];
				GroupOptionButton<bool> groupOptionButton = affectedElement as GroupOptionButton<bool>;
				groupOptionButton.SetCurrentOption(currentOption);
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue(groupOptionButton.IsSelected ? (this._powerNameKey + "_Enabled") : (this._powerNameKey + "_Disabled"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, this._powerNameKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref textValue, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref textValue);
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x06003592 RID: 13714 RVA: 0x0060A6C6 File Offset: 0x006088C6
			private void button_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				if (!this.GetIsUnlocked())
				{
					return;
				}
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				this.RequestUse();
			}

			// Token: 0x06003593 RID: 13715
			public abstract bool GetIsUnlocked();

			// Token: 0x040061E4 RID: 25060
			internal string _powerNameKey;

			// Token: 0x040061E5 RID: 25061
			internal Point _iconLocation;

			// Token: 0x040061E6 RID: 25062
			internal bool _defaultToggleState;

			// Token: 0x040061E7 RID: 25063
			private bool[] _perPlayerIsEnabled = new bool[255];

			// Token: 0x02000840 RID: 2112
			private enum SubMessageType : byte
			{
				// Token: 0x040065DA RID: 26074
				SyncEveryone,
				// Token: 0x040065DB RID: 26075
				SyncOnePlayer
			}
		}

		// Token: 0x020006B1 RID: 1713
		public abstract class APerPlayerSliderPower : ICreativePower, IOnPlayerJoining, IProvideSliderElement, IPowerSubcategoryElement
		{
			// Token: 0x170003DA RID: 986
			// (get) Token: 0x06003595 RID: 13717 RVA: 0x0060A6FD File Offset: 0x006088FD
			// (set) Token: 0x06003596 RID: 13718 RVA: 0x0060A705 File Offset: 0x00608905
			public ushort PowerId { get; set; }

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x06003597 RID: 13719 RVA: 0x0060A70E File Offset: 0x0060890E
			// (set) Token: 0x06003598 RID: 13720 RVA: 0x0060A716 File Offset: 0x00608916
			public string ServerConfigName { get; set; }

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x06003599 RID: 13721 RVA: 0x0060A71F File Offset: 0x0060891F
			// (set) Token: 0x0600359A RID: 13722 RVA: 0x0060A727 File Offset: 0x00608927
			public PowerPermissionLevel CurrentPermissionLevel { get; set; }

			// Token: 0x170003DD RID: 989
			// (get) Token: 0x0600359B RID: 13723 RVA: 0x0060A730 File Offset: 0x00608930
			// (set) Token: 0x0600359C RID: 13724 RVA: 0x0060A738 File Offset: 0x00608938
			public PowerPermissionLevel DefaultPermissionLevel { get; set; }

			// Token: 0x0600359D RID: 13725 RVA: 0x0060A741 File Offset: 0x00608941
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

			// Token: 0x0600359E RID: 13726
			public abstract float RemapSliderValueToPowerValue(float sliderValue);

			// Token: 0x0600359F RID: 13727 RVA: 0x0060A76C File Offset: 0x0060896C
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

			// Token: 0x060035A0 RID: 13728
			internal abstract void UpdateInfoFromSliderValueCache();

			// Token: 0x060035A1 RID: 13729 RVA: 0x0060A7B9 File Offset: 0x006089B9
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060035A2 RID: 13730 RVA: 0x0060A7C0 File Offset: 0x006089C0
			public void DebugCall()
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 5);
				packet.Writer.Write((byte)Main.myPlayer);
				packet.Writer.Write(0f);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060035A3 RID: 13731
			public abstract UIElement ProvideSlider();

			// Token: 0x060035A4 RID: 13732 RVA: 0x0060A808 File Offset: 0x00608A08
			internal float GetSliderValue()
			{
				if (Main.netMode == 1 && this._needsToCommitChange)
				{
					return this._currentTargetValue;
				}
				return this._sliderCurrentValueCache;
			}

			// Token: 0x060035A5 RID: 13733 RVA: 0x0060A827 File Offset: 0x00608A27
			internal void SetValueKeyboard(float value)
			{
				if (value == this._currentTargetValue)
				{
					return;
				}
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				this._currentTargetValue = value;
				this._needsToCommitChange = true;
			}

			// Token: 0x060035A6 RID: 13734 RVA: 0x0060A850 File Offset: 0x00608A50
			internal void SetValueGamepad()
			{
				float sliderValue = this.GetSliderValue();
				float num = UILinksInitializer.HandleSliderVerticalInput(sliderValue, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				if (num != sliderValue)
				{
					this.SetValueKeyboard(num);
				}
			}

			// Token: 0x060035A7 RID: 13735 RVA: 0x0060A88F File Offset: 0x00608A8F
			public void PushChangeAndSetSlider(float value)
			{
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				value = MathHelper.Clamp(value, 0f, 1f);
				this._sliderCurrentValueCache = value;
				this._currentTargetValue = value;
				this.PushChange(value);
			}

			// Token: 0x060035A8 RID: 13736 RVA: 0x0060A8C8 File Offset: 0x00608AC8
			public GroupOptionButton<int> GetOptionButton(CreativePowerUIElementRequestInfo info, int optionIndex, int currentOptionIndex)
			{
				GroupOptionButton<int> groupOptionButton = CreativePowersHelper.CreateCategoryButton<int>(info, optionIndex, currentOptionIndex);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, CreativePowersHelper.CommonSelectedColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnUpdate += this.categoryButton_OnUpdate;
				return groupOptionButton;
			}

			// Token: 0x060035A9 RID: 13737 RVA: 0x0060A910 File Offset: 0x00608B10
			private void categoryButton_OnUpdate(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					GroupOptionButton<int> groupOptionButton = affectedElement as GroupOptionButton<int>;
					string textValue = Language.GetTextValue(this._powerNameKey + (groupOptionButton.IsSelected ? "_Opened" : "_Closed"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, this._powerNameKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref textValue, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref textValue);
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
				this.AttemptPushingChange();
			}

			// Token: 0x060035AA RID: 13738 RVA: 0x0060A9A8 File Offset: 0x00608BA8
			private void AttemptPushingChange()
			{
				if (!this._needsToCommitChange)
				{
					return;
				}
				if (DateTime.UtcNow.CompareTo(this._nextTimeWeCanPush) == -1)
				{
					return;
				}
				this.PushChange(this._currentTargetValue);
			}

			// Token: 0x060035AB RID: 13739 RVA: 0x0060A9E4 File Offset: 0x00608BE4
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

			// Token: 0x060035AC RID: 13740 RVA: 0x0060AA44 File Offset: 0x00608C44
			public virtual void Reset()
			{
				for (int i = 0; i < this._cachePerPlayer.Length; i++)
				{
					this.ResetForPlayer(i);
				}
			}

			// Token: 0x060035AD RID: 13741 RVA: 0x0060AA6B File Offset: 0x00608C6B
			public virtual void ResetForPlayer(int playerIndex)
			{
				this._cachePerPlayer[playerIndex] = this._sliderDefaultValue;
				if (playerIndex == Main.myPlayer)
				{
					this._sliderCurrentValueCache = this._sliderDefaultValue;
					this._currentTargetValue = this._sliderDefaultValue;
				}
			}

			// Token: 0x060035AE RID: 13742 RVA: 0x0060AA9B File Offset: 0x00608C9B
			public void OnPlayerJoining(int playerIndex)
			{
				this.ResetForPlayer(playerIndex);
			}

			// Token: 0x060035AF RID: 13743
			public abstract bool GetIsUnlocked();

			// Token: 0x040061EC RID: 25068
			internal Point _iconLocation;

			// Token: 0x040061ED RID: 25069
			internal float _sliderCurrentValueCache;

			// Token: 0x040061EE RID: 25070
			internal string _powerNameKey;

			// Token: 0x040061EF RID: 25071
			internal float[] _cachePerPlayer = new float[256];

			// Token: 0x040061F0 RID: 25072
			internal float _sliderDefaultValue;

			// Token: 0x040061F1 RID: 25073
			private float _currentTargetValue;

			// Token: 0x040061F2 RID: 25074
			private bool _needsToCommitChange;

			// Token: 0x040061F3 RID: 25075
			private DateTime _nextTimeWeCanPush = DateTime.UtcNow;
		}

		// Token: 0x020006B2 RID: 1714
		public abstract class ASharedButtonPower : ICreativePower
		{
			// Token: 0x170003DE RID: 990
			// (get) Token: 0x060035B1 RID: 13745 RVA: 0x0060AAC7 File Offset: 0x00608CC7
			// (set) Token: 0x060035B2 RID: 13746 RVA: 0x0060AACF File Offset: 0x00608CCF
			public ushort PowerId { get; set; }

			// Token: 0x170003DF RID: 991
			// (get) Token: 0x060035B3 RID: 13747 RVA: 0x0060AAD8 File Offset: 0x00608CD8
			// (set) Token: 0x060035B4 RID: 13748 RVA: 0x0060AAE0 File Offset: 0x00608CE0
			public string ServerConfigName { get; set; }

			// Token: 0x170003E0 RID: 992
			// (get) Token: 0x060035B5 RID: 13749 RVA: 0x0060AAE9 File Offset: 0x00608CE9
			// (set) Token: 0x060035B6 RID: 13750 RVA: 0x0060AAF1 File Offset: 0x00608CF1
			public PowerPermissionLevel CurrentPermissionLevel { get; set; }

			// Token: 0x170003E1 RID: 993
			// (get) Token: 0x060035B7 RID: 13751 RVA: 0x0060AAFA File Offset: 0x00608CFA
			// (set) Token: 0x060035B8 RID: 13752 RVA: 0x0060AB02 File Offset: 0x00608D02
			public PowerPermissionLevel DefaultPermissionLevel { get; set; }

			// Token: 0x060035B9 RID: 13753 RVA: 0x0060AB0B File Offset: 0x00608D0B
			public ASharedButtonPower()
			{
				this.OnCreation();
			}

			// Token: 0x060035BA RID: 13754 RVA: 0x0060AB1C File Offset: 0x00608D1C
			public void RequestUse()
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 0);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060035BB RID: 13755 RVA: 0x0060AB41 File Offset: 0x00608D41
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				if (Main.netMode == 2 && !CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					return;
				}
				this.UsePower();
			}

			// Token: 0x060035BC RID: 13756
			internal abstract void UsePower();

			// Token: 0x060035BD RID: 13757
			internal abstract void OnCreation();

			// Token: 0x060035BE RID: 13758 RVA: 0x0060AB5C File Offset: 0x00608D5C
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				GroupOptionButton<bool> groupOptionButton = CreativePowersHelper.CreateSimpleButton(info);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, CreativePowersHelper.CommonSelectedColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnLeftClick += this.button_OnClick;
				groupOptionButton.OnUpdate += this.button_OnUpdate;
				elements.Add(groupOptionButton);
			}

			// Token: 0x060035BF RID: 13759 RVA: 0x0060ABB8 File Offset: 0x00608DB8
			private void button_OnUpdate(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue(this._powerNameKey);
					CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, this._descriptionKey);
					CreativePowersHelper.AddUnlockTextIfNeeded(ref textValue, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref textValue);
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x060035C0 RID: 13760 RVA: 0x0060AC1D File Offset: 0x00608E1D
			private void button_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				this.RequestUse();
			}

			// Token: 0x060035C1 RID: 13761
			public abstract bool GetIsUnlocked();

			// Token: 0x040061F8 RID: 25080
			internal Point _iconLocation;

			// Token: 0x040061F9 RID: 25081
			internal string _powerNameKey;

			// Token: 0x040061FA RID: 25082
			internal string _descriptionKey;
		}

		// Token: 0x020006B3 RID: 1715
		public abstract class ASharedTogglePower : ICreativePower, IOnPlayerJoining
		{
			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x060035C2 RID: 13762 RVA: 0x0060AC33 File Offset: 0x00608E33
			// (set) Token: 0x060035C3 RID: 13763 RVA: 0x0060AC3B File Offset: 0x00608E3B
			public ushort PowerId { get; set; }

			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x060035C4 RID: 13764 RVA: 0x0060AC44 File Offset: 0x00608E44
			// (set) Token: 0x060035C5 RID: 13765 RVA: 0x0060AC4C File Offset: 0x00608E4C
			public string ServerConfigName { get; set; }

			// Token: 0x170003E4 RID: 996
			// (get) Token: 0x060035C6 RID: 13766 RVA: 0x0060AC55 File Offset: 0x00608E55
			// (set) Token: 0x060035C7 RID: 13767 RVA: 0x0060AC5D File Offset: 0x00608E5D
			public PowerPermissionLevel CurrentPermissionLevel { get; set; }

			// Token: 0x170003E5 RID: 997
			// (get) Token: 0x060035C8 RID: 13768 RVA: 0x0060AC66 File Offset: 0x00608E66
			// (set) Token: 0x060035C9 RID: 13769 RVA: 0x0060AC6E File Offset: 0x00608E6E
			public PowerPermissionLevel DefaultPermissionLevel { get; set; }

			// Token: 0x170003E6 RID: 998
			// (get) Token: 0x060035CA RID: 13770 RVA: 0x0060AC77 File Offset: 0x00608E77
			// (set) Token: 0x060035CB RID: 13771 RVA: 0x0060AC7F File Offset: 0x00608E7F
			public bool Enabled { get; private set; }

			// Token: 0x060035CC RID: 13772 RVA: 0x0060AC88 File Offset: 0x00608E88
			public void SetPowerInfo(bool enabled)
			{
				this.Enabled = enabled;
			}

			// Token: 0x060035CD RID: 13773 RVA: 0x0060AC91 File Offset: 0x00608E91
			public void Reset()
			{
				this.Enabled = false;
			}

			// Token: 0x060035CE RID: 13774 RVA: 0x0060AC9C File Offset: 0x00608E9C
			public void OnPlayerJoining(int playerIndex)
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
				packet.Writer.Write(this.Enabled);
				NetManager.Instance.SendToClient(packet, playerIndex);
			}

			// Token: 0x060035CF RID: 13775 RVA: 0x0060ACD4 File Offset: 0x00608ED4
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				bool powerInfo = reader.ReadBoolean();
				if (Main.netMode == 2 && !CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					return;
				}
				this.SetPowerInfo(powerInfo);
				if (Main.netMode == 2)
				{
					NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
					packet.Writer.Write(this.Enabled);
					NetManager.Instance.Broadcast(packet, -1);
				}
			}

			// Token: 0x060035D0 RID: 13776 RVA: 0x0060AD34 File Offset: 0x00608F34
			private void RequestUse()
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
				packet.Writer.Write(!this.Enabled);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060035D1 RID: 13777 RVA: 0x0060AD70 File Offset: 0x00608F70
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				GroupOptionButton<bool> groupOptionButton = CreativePowersHelper.CreateToggleButton(info);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, Main.OurFavoriteColor);
				this.CustomizeButton(groupOptionButton);
				groupOptionButton.OnLeftClick += this.button_OnClick;
				groupOptionButton.OnUpdate += this.button_OnUpdate;
				elements.Add(groupOptionButton);
			}

			// Token: 0x060035D2 RID: 13778 RVA: 0x0060ADC4 File Offset: 0x00608FC4
			private void button_OnUpdate(UIElement affectedElement)
			{
				bool enabled = this.Enabled;
				GroupOptionButton<bool> groupOptionButton = affectedElement as GroupOptionButton<bool>;
				groupOptionButton.SetCurrentOption(enabled);
				if (affectedElement.IsMouseHovering)
				{
					string buttonTextKey = this.GetButtonTextKey();
					string textValue = Language.GetTextValue(buttonTextKey + (groupOptionButton.IsSelected ? "_Enabled" : "_Disabled"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, buttonTextKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref textValue, this.GetIsUnlocked(), buttonTextKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref textValue);
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x060035D3 RID: 13779 RVA: 0x0060AE59 File Offset: 0x00609059
			private void button_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				this.RequestUse();
			}

			// Token: 0x060035D4 RID: 13780
			internal abstract void CustomizeButton(UIElement button);

			// Token: 0x060035D5 RID: 13781
			internal abstract string GetButtonTextKey();

			// Token: 0x060035D6 RID: 13782
			public abstract bool GetIsUnlocked();
		}

		// Token: 0x020006B4 RID: 1716
		public abstract class ASharedSliderPower : ICreativePower, IOnPlayerJoining, IProvideSliderElement, IPowerSubcategoryElement
		{
			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x060035D8 RID: 13784 RVA: 0x0060AE6F File Offset: 0x0060906F
			// (set) Token: 0x060035D9 RID: 13785 RVA: 0x0060AE77 File Offset: 0x00609077
			public ushort PowerId { get; set; }

			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x060035DA RID: 13786 RVA: 0x0060AE80 File Offset: 0x00609080
			// (set) Token: 0x060035DB RID: 13787 RVA: 0x0060AE88 File Offset: 0x00609088
			public string ServerConfigName { get; set; }

			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x060035DC RID: 13788 RVA: 0x0060AE91 File Offset: 0x00609091
			// (set) Token: 0x060035DD RID: 13789 RVA: 0x0060AE99 File Offset: 0x00609099
			public PowerPermissionLevel CurrentPermissionLevel { get; set; }

			// Token: 0x170003EA RID: 1002
			// (get) Token: 0x060035DE RID: 13790 RVA: 0x0060AEA2 File Offset: 0x006090A2
			// (set) Token: 0x060035DF RID: 13791 RVA: 0x0060AEAA File Offset: 0x006090AA
			public PowerPermissionLevel DefaultPermissionLevel { get; set; }

			// Token: 0x060035E0 RID: 13792 RVA: 0x0060AEB4 File Offset: 0x006090B4
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				float num = reader.ReadSingle();
				if (Main.netMode == 2 && !CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					return;
				}
				this._sliderCurrentValueCache = num;
				this.UpdateInfoFromSliderValueCache();
				if (Main.netMode == 2)
				{
					NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
					packet.Writer.Write(num);
					NetManager.Instance.Broadcast(packet, -1);
				}
			}

			// Token: 0x060035E1 RID: 13793
			internal abstract void UpdateInfoFromSliderValueCache();

			// Token: 0x060035E2 RID: 13794 RVA: 0x0060A7B9 File Offset: 0x006089B9
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060035E3 RID: 13795 RVA: 0x0060AF18 File Offset: 0x00609118
			public void DebugCall()
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
				packet.Writer.Write(0f);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060035E4 RID: 13796
			public abstract UIElement ProvideSlider();

			// Token: 0x060035E5 RID: 13797 RVA: 0x0060AF4E File Offset: 0x0060914E
			internal float GetSliderValue()
			{
				if (Main.netMode == 1 && this._needsToCommitChange)
				{
					return this._currentTargetValue;
				}
				return this.GetSliderValueInner();
			}

			// Token: 0x060035E6 RID: 13798 RVA: 0x0060AF6D File Offset: 0x0060916D
			internal virtual float GetSliderValueInner()
			{
				return this._sliderCurrentValueCache;
			}

			// Token: 0x060035E7 RID: 13799 RVA: 0x0060AF75 File Offset: 0x00609175
			internal void SetValueKeyboard(float value)
			{
				if (value == this._currentTargetValue)
				{
					return;
				}
				this.SetValueKeyboardForced(value);
			}

			// Token: 0x060035E8 RID: 13800 RVA: 0x0060AF88 File Offset: 0x00609188
			internal void SetValueKeyboardForced(float value)
			{
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				this._currentTargetValue = value;
				this._needsToCommitChange = true;
			}

			// Token: 0x060035E9 RID: 13801 RVA: 0x0060AFA8 File Offset: 0x006091A8
			internal void SetValueGamepad()
			{
				float sliderValue = this.GetSliderValue();
				float num = UILinksInitializer.HandleSliderVerticalInput(sliderValue, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				if (num != sliderValue)
				{
					this.SetValueKeyboard(num);
				}
			}

			// Token: 0x060035EA RID: 13802 RVA: 0x0060AFE8 File Offset: 0x006091E8
			public GroupOptionButton<int> GetOptionButton(CreativePowerUIElementRequestInfo info, int optionIndex, int currentOptionIndex)
			{
				GroupOptionButton<int> groupOptionButton = CreativePowersHelper.CreateCategoryButton<int>(info, optionIndex, currentOptionIndex);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, CreativePowersHelper.CommonSelectedColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnUpdate += this.categoryButton_OnUpdate;
				return groupOptionButton;
			}

			// Token: 0x060035EB RID: 13803 RVA: 0x0060B030 File Offset: 0x00609230
			private void categoryButton_OnUpdate(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					GroupOptionButton<int> groupOptionButton = affectedElement as GroupOptionButton<int>;
					string textValue = Language.GetTextValue(this._powerNameKey + (groupOptionButton.IsSelected ? "_Opened" : "_Closed"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, this._powerNameKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref textValue, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref textValue);
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
				this.AttemptPushingChange();
			}

			// Token: 0x060035EC RID: 13804 RVA: 0x0060B0C8 File Offset: 0x006092C8
			private void AttemptPushingChange()
			{
				if (!this._needsToCommitChange)
				{
					return;
				}
				if (DateTime.UtcNow.CompareTo(this._nextTimeWeCanPush) == -1)
				{
					return;
				}
				this._needsToCommitChange = false;
				this._sliderCurrentValueCache = this._currentTargetValue;
				this._nextTimeWeCanPush = DateTime.UtcNow;
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
				packet.Writer.Write(this._currentTargetValue);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}

			// Token: 0x060035ED RID: 13805 RVA: 0x0060B13D File Offset: 0x0060933D
			public virtual void Reset()
			{
				this._sliderCurrentValueCache = 0f;
			}

			// Token: 0x060035EE RID: 13806 RVA: 0x0060B14C File Offset: 0x0060934C
			public void OnPlayerJoining(int playerIndex)
			{
				if (!this._syncToJoiningPlayers)
				{
					return;
				}
				NetPacket packet = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
				packet.Writer.Write(this._sliderCurrentValueCache);
				NetManager.Instance.SendToClient(packet, playerIndex);
			}

			// Token: 0x060035EF RID: 13807
			public abstract bool GetIsUnlocked();

			// Token: 0x04006204 RID: 25092
			internal Point _iconLocation;

			// Token: 0x04006205 RID: 25093
			internal float _sliderCurrentValueCache;

			// Token: 0x04006206 RID: 25094
			internal string _powerNameKey;

			// Token: 0x04006207 RID: 25095
			internal bool _syncToJoiningPlayers = true;

			// Token: 0x04006208 RID: 25096
			internal float _currentTargetValue;

			// Token: 0x04006209 RID: 25097
			private bool _needsToCommitChange;

			// Token: 0x0400620A RID: 25098
			private DateTime _nextTimeWeCanPush = DateTime.UtcNow;
		}

		// Token: 0x020006B5 RID: 1717
		public class GodmodePower : CreativePowers.APerPlayerTogglePower, IPersistentPerPlayerContent
		{
			// Token: 0x060035F1 RID: 13809 RVA: 0x0060B1A7 File Offset: 0x006093A7
			public GodmodePower()
			{
				this._powerNameKey = "CreativePowers.Godmode";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.Godmode;
			}

			// Token: 0x060035F2 RID: 13810 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x060035F3 RID: 13811 RVA: 0x0060B1C8 File Offset: 0x006093C8
			public void Save(Player player, BinaryWriter writer)
			{
				bool value = base.IsEnabledForPlayer(Main.myPlayer);
				writer.Write(value);
			}

			// Token: 0x060035F4 RID: 13812 RVA: 0x0060B1E8 File Offset: 0x006093E8
			public void ResetDataForNewPlayer(Player player)
			{
				player.savedPerPlayerFieldsThatArentInThePlayerClass.godmodePowerEnabled = this._defaultToggleState;
			}

			// Token: 0x060035F5 RID: 13813 RVA: 0x0060B1FC File Offset: 0x006093FC
			public void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool godmodePowerEnabled = reader.ReadBoolean();
				player.savedPerPlayerFieldsThatArentInThePlayerClass.godmodePowerEnabled = godmodePowerEnabled;
			}

			// Token: 0x060035F6 RID: 13814 RVA: 0x0060B21C File Offset: 0x0060941C
			public void ApplyLoadedDataToOutOfPlayerFields(Player player)
			{
				if (player.savedPerPlayerFieldsThatArentInThePlayerClass.godmodePowerEnabled != base.IsEnabledForPlayer(player.whoAmI))
				{
					base.RequestUse();
				}
			}
		}

		// Token: 0x020006B6 RID: 1718
		public class FarPlacementRangePower : CreativePowers.APerPlayerTogglePower, IPersistentPerPlayerContent
		{
			// Token: 0x060035F7 RID: 13815 RVA: 0x0060B23D File Offset: 0x0060943D
			public FarPlacementRangePower()
			{
				this._powerNameKey = "CreativePowers.InfinitePlacementRange";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.BlockPlacementRange;
				this._defaultToggleState = true;
			}

			// Token: 0x060035F8 RID: 13816 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x060035F9 RID: 13817 RVA: 0x0060B264 File Offset: 0x00609464
			public void Save(Player player, BinaryWriter writer)
			{
				bool value = base.IsEnabledForPlayer(Main.myPlayer);
				writer.Write(value);
			}

			// Token: 0x060035FA RID: 13818 RVA: 0x0060B284 File Offset: 0x00609484
			public void ResetDataForNewPlayer(Player player)
			{
				player.savedPerPlayerFieldsThatArentInThePlayerClass.farPlacementRangePowerEnabled = this._defaultToggleState;
			}

			// Token: 0x060035FB RID: 13819 RVA: 0x0060B298 File Offset: 0x00609498
			public void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool farPlacementRangePowerEnabled = reader.ReadBoolean();
				player.savedPerPlayerFieldsThatArentInThePlayerClass.farPlacementRangePowerEnabled = farPlacementRangePowerEnabled;
			}

			// Token: 0x060035FC RID: 13820 RVA: 0x0060B2B8 File Offset: 0x006094B8
			public void ApplyLoadedDataToOutOfPlayerFields(Player player)
			{
				if (player.savedPerPlayerFieldsThatArentInThePlayerClass.farPlacementRangePowerEnabled != base.IsEnabledForPlayer(player.whoAmI))
				{
					base.RequestUse();
				}
			}
		}

		// Token: 0x020006B7 RID: 1719
		public class StartDayImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x060035FD RID: 13821 RVA: 0x0060B2D9 File Offset: 0x006094D9
			internal override void UsePower()
			{
				if (Main.netMode == 1)
				{
					return;
				}
				Main.SkipToTime(0, true);
			}

			// Token: 0x060035FE RID: 13822 RVA: 0x0060B2EB File Offset: 0x006094EB
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartDayImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeDawn;
			}

			// Token: 0x060035FF RID: 13823 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}
		}

		// Token: 0x020006B8 RID: 1720
		public class StartNightImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x06003601 RID: 13825 RVA: 0x0060B321 File Offset: 0x00609521
			internal override void UsePower()
			{
				if (Main.netMode == 1)
				{
					return;
				}
				Main.SkipToTime(0, false);
			}

			// Token: 0x06003602 RID: 13826 RVA: 0x0060B333 File Offset: 0x00609533
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartNightImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeDusk;
			}

			// Token: 0x06003603 RID: 13827 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}
		}

		// Token: 0x020006B9 RID: 1721
		public class StartNoonImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x06003605 RID: 13829 RVA: 0x0060B361 File Offset: 0x00609561
			internal override void UsePower()
			{
				if (Main.netMode == 1)
				{
					return;
				}
				Main.SkipToTime(27000, true);
			}

			// Token: 0x06003606 RID: 13830 RVA: 0x0060B377 File Offset: 0x00609577
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartNoonImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeNoon;
			}

			// Token: 0x06003607 RID: 13831 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}
		}

		// Token: 0x020006BA RID: 1722
		public class StartMidnightImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x06003609 RID: 13833 RVA: 0x0060B3A5 File Offset: 0x006095A5
			internal override void UsePower()
			{
				if (Main.netMode == 1)
				{
					return;
				}
				Main.SkipToTime(16200, false);
			}

			// Token: 0x0600360A RID: 13834 RVA: 0x0060B3BB File Offset: 0x006095BB
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartMidnightImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeMidnight;
			}

			// Token: 0x0600360B RID: 13835 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}
		}

		// Token: 0x020006BB RID: 1723
		public class ModifyTimeRate : CreativePowers.ASharedSliderPower, IPersistentPerWorldContent
		{
			// Token: 0x170003EB RID: 1003
			// (get) Token: 0x0600360D RID: 13837 RVA: 0x0060B3E9 File Offset: 0x006095E9
			// (set) Token: 0x0600360E RID: 13838 RVA: 0x0060B3F1 File Offset: 0x006095F1
			public int TargetTimeRate { get; private set; }

			// Token: 0x0600360F RID: 13839 RVA: 0x0060B3FA File Offset: 0x006095FA
			public ModifyTimeRate()
			{
				this._powerNameKey = "CreativePowers.ModifyTimeRate";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.ModifyTime;
			}

			// Token: 0x06003610 RID: 13840 RVA: 0x0060B418 File Offset: 0x00609618
			public override void Reset()
			{
				this._sliderCurrentValueCache = 0f;
				this.TargetTimeRate = 1;
			}

			// Token: 0x06003611 RID: 13841 RVA: 0x0060B42C File Offset: 0x0060962C
			internal override void UpdateInfoFromSliderValueCache()
			{
				this.TargetTimeRate = (int)Math.Round((double)Utils.Remap(this._sliderCurrentValueCache, 0f, 1f, 1f, 24f, true));
			}

			// Token: 0x06003612 RID: 13842 RVA: 0x0060B45C File Offset: 0x0060965C
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uiverticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uiverticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(87f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uiverticalSlider);
				uipanel.OnUpdate += CreativePowersHelper.UpdateUseMouseInterface;
				UIText uitext = new UIText("x24", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uitext.OnMouseOver += this.Button_OnMouseOver;
				uitext.OnMouseOut += this.Button_OnMouseOut;
				uitext.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uitext);
				UIText uitext2 = new UIText("x12", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uitext2.OnMouseOver += this.Button_OnMouseOver;
				uitext2.OnMouseOut += this.Button_OnMouseOut;
				uitext2.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uitext2);
				UIText uitext3 = new UIText("x1", 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uitext3.OnMouseOver += this.Button_OnMouseOver;
				uitext3.OnMouseOut += this.Button_OnMouseOut;
				uitext3.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uitext3);
				return uipanel;
			}

			// Token: 0x06003613 RID: 13843 RVA: 0x0060B62F File Offset: 0x0060982F
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003614 RID: 13844 RVA: 0x0060B651 File Offset: 0x00609851
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003615 RID: 13845 RVA: 0x0060B673 File Offset: 0x00609873
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003616 RID: 13846 RVA: 0x0060B698 File Offset: 0x00609898
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003617 RID: 13847 RVA: 0x0060B6D0 File Offset: 0x006098D0
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003618 RID: 13848 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06003619 RID: 13849 RVA: 0x0060B707 File Offset: 0x00609907
			public void Save(BinaryWriter writer)
			{
				writer.Write(this._sliderCurrentValueCache);
			}

			// Token: 0x0600361A RID: 13850 RVA: 0x0060B715 File Offset: 0x00609915
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				this._sliderCurrentValueCache = reader.ReadSingle();
				this.UpdateInfoFromSliderValueCache();
			}

			// Token: 0x0600361B RID: 13851 RVA: 0x0060B729 File Offset: 0x00609929
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadSingle();
			}

			// Token: 0x0600361C RID: 13852 RVA: 0x0060B734 File Offset: 0x00609934
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string cursorText = "x" + this.TargetTimeRate.ToString();
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref cursorText);
					Main.instance.MouseTextNoOverride(cursorText, 0, 0, -1, -1, -1, -1, 0);
				}
			}
		}

		// Token: 0x020006BC RID: 1724
		public class DifficultySliderPower : CreativePowers.ASharedSliderPower, IPersistentPerWorldContent
		{
			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x0600361D RID: 13853 RVA: 0x0060B77C File Offset: 0x0060997C
			// (set) Token: 0x0600361E RID: 13854 RVA: 0x0060B784 File Offset: 0x00609984
			public float StrengthMultiplierToGiveNPCs { get; private set; }

			// Token: 0x0600361F RID: 13855 RVA: 0x0060B78D File Offset: 0x0060998D
			public DifficultySliderPower()
			{
				this._powerNameKey = "CreativePowers.DifficultySlider";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.EnemyStrengthSlider;
			}

			// Token: 0x06003620 RID: 13856 RVA: 0x0060B7AB File Offset: 0x006099AB
			public override void Reset()
			{
				this._sliderCurrentValueCache = 0f;
				this.UpdateInfoFromSliderValueCache();
			}

			// Token: 0x06003621 RID: 13857 RVA: 0x0060B7C0 File Offset: 0x006099C0
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

			// Token: 0x06003622 RID: 13858 RVA: 0x0060B84C File Offset: 0x00609A4C
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uiverticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(82f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uiverticalSlider);
				uipanel.OnUpdate += CreativePowersHelper.UpdateUseMouseInterface;
				uiverticalSlider.OnUpdate += this.UpdateSliderColorAndShowMultiplierMouseOver;
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 0f, "x3", "Images/UI/WorldCreation/IconDifficultyMaster", new UIElement.ElementEvent(this.MouseOver_Master), new UIElement.MouseEvent(this.Click_Master));
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 0.33333334f, "x2", "Images/UI/WorldCreation/IconDifficultyExpert", new UIElement.ElementEvent(this.MouseOver_Expert), new UIElement.MouseEvent(this.Click_Expert));
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 0.6666667f, "x1", "Images/UI/WorldCreation/IconDifficultyNormal", new UIElement.ElementEvent(this.MouseOver_Normal), new UIElement.MouseEvent(this.Click_Normal));
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 1f, "x0.5", "Images/UI/WorldCreation/IconDifficultyCreative", new UIElement.ElementEvent(this.MouseOver_Journey), new UIElement.MouseEvent(this.Click_Journey));
				return uipanel;
			}

			// Token: 0x06003623 RID: 13859 RVA: 0x0060B673 File Offset: 0x00609873
			private void Click_Master(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003624 RID: 13860 RVA: 0x0060B9A7 File Offset: 0x00609BA7
			private void Click_Expert(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.66f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003625 RID: 13861 RVA: 0x0060B9C9 File Offset: 0x00609BC9
			private void Click_Normal(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.33f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003626 RID: 13862 RVA: 0x0060B62F File Offset: 0x0060982F
			private void Click_Journey(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003627 RID: 13863 RVA: 0x0060B9EC File Offset: 0x00609BEC
			private static void AddIndication(UIPanel panel, float yAnchor, string indicationText, string iconImagePath, UIElement.ElementEvent updateEvent, UIElement.MouseEvent clickEvent)
			{
				UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>(iconImagePath, 1))
				{
					HAlign = 1f,
					VAlign = yAnchor,
					Left = new StyleDimension(4f, 0f),
					Top = new StyleDimension(2f, 0f),
					RemoveFloatingPointsFromDrawPosition = true
				};
				uiimage.OnMouseOut += CreativePowers.DifficultySliderPower.Button_OnMouseOut;
				uiimage.OnMouseOver += CreativePowers.DifficultySliderPower.Button_OnMouseOver;
				if (updateEvent != null)
				{
					uiimage.OnUpdate += updateEvent;
				}
				if (clickEvent != null)
				{
					uiimage.OnLeftClick += clickEvent;
				}
				panel.Append(uiimage);
			}

			// Token: 0x06003628 RID: 13864 RVA: 0x00570D36 File Offset: 0x0056EF36
			private static void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003629 RID: 13865 RVA: 0x00570D36 File Offset: 0x0056EF36
			private static void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600362A RID: 13866 RVA: 0x0060BA94 File Offset: 0x00609C94
			private void MouseOver_Journey(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Creative");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600362B RID: 13867 RVA: 0x0060BAC8 File Offset: 0x00609CC8
			private void MouseOver_Normal(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Normal");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600362C RID: 13868 RVA: 0x0060BAFC File Offset: 0x00609CFC
			private void MouseOver_Expert(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Expert");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600362D RID: 13869 RVA: 0x0060BB30 File Offset: 0x00609D30
			private void MouseOver_Master(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Master");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600362E RID: 13870 RVA: 0x0060BB64 File Offset: 0x00609D64
			private void UpdateSliderColorAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string cursorText = "x" + this.StrengthMultiplierToGiveNPCs.ToString("F2");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref cursorText);
					Main.instance.MouseTextNoOverride(cursorText, 0, 0, -1, -1, -1, -1, 0);
				}
				UIVerticalSlider uiverticalSlider = affectedElement as UIVerticalSlider;
				if (uiverticalSlider == null)
				{
					return;
				}
				uiverticalSlider.EmptyColor = Color.Black;
				Color filledColor;
				if (Main.masterMode)
				{
					filledColor = Main.hcColor;
				}
				else if (Main.expertMode)
				{
					filledColor = Main.mcColor;
				}
				else if (this.StrengthMultiplierToGiveNPCs < 1f)
				{
					filledColor = Main.creativeModeColor;
				}
				else
				{
					filledColor = Color.White;
				}
				uiverticalSlider.FilledColor = filledColor;
			}

			// Token: 0x0600362F RID: 13871 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06003630 RID: 13872 RVA: 0x0060B707 File Offset: 0x00609907
			public void Save(BinaryWriter writer)
			{
				writer.Write(this._sliderCurrentValueCache);
			}

			// Token: 0x06003631 RID: 13873 RVA: 0x0060B715 File Offset: 0x00609915
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				this._sliderCurrentValueCache = reader.ReadSingle();
				this.UpdateInfoFromSliderValueCache();
			}

			// Token: 0x06003632 RID: 13874 RVA: 0x0060B729 File Offset: 0x00609929
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadSingle();
			}
		}

		// Token: 0x020006BD RID: 1725
		public class ModifyWindDirectionAndStrength : CreativePowers.ASharedSliderPower
		{
			// Token: 0x06003633 RID: 13875 RVA: 0x0060BC07 File Offset: 0x00609E07
			public ModifyWindDirectionAndStrength()
			{
				this._powerNameKey = "CreativePowers.ModifyWindDirectionAndStrength";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.WindDirection;
				this._syncToJoiningPlayers = false;
			}

			// Token: 0x06003634 RID: 13876 RVA: 0x0060BC2C File Offset: 0x00609E2C
			internal override void UpdateInfoFromSliderValueCache()
			{
				Main.windSpeedCurrent = (Main.windSpeedTarget = MathHelper.Lerp(-0.8f, 0.8f, this._sliderCurrentValueCache));
			}

			// Token: 0x06003635 RID: 13877 RVA: 0x0060BC4E File Offset: 0x00609E4E
			internal override float GetSliderValueInner()
			{
				return Utils.GetLerpValue(-0.8f, 0.8f, Main.windSpeedTarget, false);
			}

			// Token: 0x06003636 RID: 13878 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06003637 RID: 13879 RVA: 0x0060BC68 File Offset: 0x00609E68
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uiverticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uiverticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(132f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uiverticalSlider);
				uipanel.OnUpdate += CreativePowersHelper.UpdateUseMouseInterface;
				UIText uitext = new UIText(Language.GetText("CreativePowers.WindWest"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uitext.OnMouseOut += this.Button_OnMouseOut;
				uitext.OnMouseOver += this.Button_OnMouseOver;
				uitext.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uitext);
				UIText uitext2 = new UIText(Language.GetText("CreativePowers.WindEast"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uitext2.OnMouseOut += this.Button_OnMouseOut;
				uitext2.OnMouseOver += this.Button_OnMouseOver;
				uitext2.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uitext2);
				UIText uitext3 = new UIText(Language.GetText("CreativePowers.WindNone"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uitext3.OnMouseOut += this.Button_OnMouseOut;
				uitext3.OnMouseOver += this.Button_OnMouseOver;
				uitext3.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uitext3);
				return uipanel;
			}

			// Token: 0x06003638 RID: 13880 RVA: 0x0060B673 File Offset: 0x00609873
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003639 RID: 13881 RVA: 0x0060B62F File Offset: 0x0060982F
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600363A RID: 13882 RVA: 0x0060B651 File Offset: 0x00609851
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600363B RID: 13883 RVA: 0x0060BE4C File Offset: 0x0060A04C
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600363C RID: 13884 RVA: 0x0060BE84 File Offset: 0x0060A084
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600363D RID: 13885 RVA: 0x0060BEBC File Offset: 0x0060A0BC
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					int num = (int)(Main.windSpeedCurrent * 50f);
					string text = "";
					if (num < 0)
					{
						text += Language.GetTextValue("GameUI.EastWind", Math.Abs(num));
					}
					else if (num > 0)
					{
						text += Language.GetTextValue("GameUI.WestWind", num);
					}
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref text);
					Main.instance.MouseTextNoOverride(text, 0, 0, -1, -1, -1, -1, 0);
				}
			}
		}

		// Token: 0x020006BE RID: 1726
		public class ModifyRainPower : CreativePowers.ASharedSliderPower
		{
			// Token: 0x0600363E RID: 13886 RVA: 0x0060BF3B File Offset: 0x0060A13B
			public ModifyRainPower()
			{
				this._powerNameKey = "CreativePowers.ModifyRainPower";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.RainStrength;
				this._syncToJoiningPlayers = false;
			}

			// Token: 0x0600363F RID: 13887 RVA: 0x0060BF60 File Offset: 0x0060A160
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

			// Token: 0x06003640 RID: 13888 RVA: 0x0060BF91 File Offset: 0x0060A191
			internal override float GetSliderValueInner()
			{
				return Main.cloudAlpha;
			}

			// Token: 0x06003641 RID: 13889 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06003642 RID: 13890 RVA: 0x0060BF98 File Offset: 0x0060A198
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uiverticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uiverticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(132f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uiverticalSlider);
				uipanel.OnUpdate += CreativePowersHelper.UpdateUseMouseInterface;
				UIText uitext = new UIText(Language.GetText("CreativePowers.WeatherMonsoon"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uitext.OnMouseOut += this.Button_OnMouseOut;
				uitext.OnMouseOver += this.Button_OnMouseOver;
				uitext.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uitext);
				UIText uitext2 = new UIText(Language.GetText("CreativePowers.WeatherClearSky"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uitext2.OnMouseOut += this.Button_OnMouseOut;
				uitext2.OnMouseOver += this.Button_OnMouseOver;
				uitext2.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uitext2);
				UIText uitext3 = new UIText(Language.GetText("CreativePowers.WeatherDrizzle"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uitext3.OnMouseOut += this.Button_OnMouseOut;
				uitext3.OnMouseOver += this.Button_OnMouseOver;
				uitext3.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uitext3);
				return uipanel;
			}

			// Token: 0x06003643 RID: 13891 RVA: 0x0060B673 File Offset: 0x00609873
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003644 RID: 13892 RVA: 0x0060B651 File Offset: 0x00609851
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003645 RID: 13893 RVA: 0x0060B62F File Offset: 0x0060982F
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003646 RID: 13894 RVA: 0x0060C17C File Offset: 0x0060A37C
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003647 RID: 13895 RVA: 0x0060C1B4 File Offset: 0x0060A3B4
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003648 RID: 13896 RVA: 0x0060C1EC File Offset: 0x0060A3EC
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string cursorText = Main.maxRaining.ToString("P0");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref cursorText);
					Main.instance.MouseTextNoOverride(cursorText, 0, 0, -1, -1, -1, -1, 0);
				}
			}
		}

		// Token: 0x020006BF RID: 1727
		public class FreezeTime : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x06003649 RID: 13897 RVA: 0x0060C22B File Offset: 0x0060A42B
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.FreezeTime));
			}

			// Token: 0x0600364A RID: 13898 RVA: 0x0060C23D File Offset: 0x0060A43D
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.FreezeTime";
			}

			// Token: 0x0600364B RID: 13899 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x0600364C RID: 13900 RVA: 0x0060C244 File Offset: 0x0060A444
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x0600364D RID: 13901 RVA: 0x0060C254 File Offset: 0x0060A454
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool powerInfo = reader.ReadBoolean();
				base.SetPowerInfo(powerInfo);
			}

			// Token: 0x0600364E RID: 13902 RVA: 0x0060C26F File Offset: 0x0060A46F
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}
		}

		// Token: 0x020006C0 RID: 1728
		public class FreezeWindDirectionAndStrength : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x06003650 RID: 13904 RVA: 0x0060C280 File Offset: 0x0060A480
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.WindFreeze));
			}

			// Token: 0x06003651 RID: 13905 RVA: 0x0060C292 File Offset: 0x0060A492
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.FreezeWindDirectionAndStrength";
			}

			// Token: 0x06003652 RID: 13906 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06003653 RID: 13907 RVA: 0x0060C244 File Offset: 0x0060A444
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x06003654 RID: 13908 RVA: 0x0060C29C File Offset: 0x0060A49C
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool powerInfo = reader.ReadBoolean();
				base.SetPowerInfo(powerInfo);
			}

			// Token: 0x06003655 RID: 13909 RVA: 0x0060C26F File Offset: 0x0060A46F
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}
		}

		// Token: 0x020006C1 RID: 1729
		public class FreezeRainPower : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x06003657 RID: 13911 RVA: 0x0060C2B7 File Offset: 0x0060A4B7
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.RainFreeze));
			}

			// Token: 0x06003658 RID: 13912 RVA: 0x0060C2C9 File Offset: 0x0060A4C9
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.FreezeRainPower";
			}

			// Token: 0x06003659 RID: 13913 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x0600365A RID: 13914 RVA: 0x0060C244 File Offset: 0x0060A444
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x0600365B RID: 13915 RVA: 0x0060C2D0 File Offset: 0x0060A4D0
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool powerInfo = reader.ReadBoolean();
				base.SetPowerInfo(powerInfo);
			}

			// Token: 0x0600365C RID: 13916 RVA: 0x0060C26F File Offset: 0x0060A46F
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}
		}

		// Token: 0x020006C2 RID: 1730
		public class StopBiomeSpreadPower : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x0600365E RID: 13918 RVA: 0x0060C2EB File Offset: 0x0060A4EB
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.StopBiomeSpread));
			}

			// Token: 0x0600365F RID: 13919 RVA: 0x0060C2FD File Offset: 0x0060A4FD
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.StopBiomeSpread";
			}

			// Token: 0x06003660 RID: 13920 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06003661 RID: 13921 RVA: 0x0060C244 File Offset: 0x0060A444
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x06003662 RID: 13922 RVA: 0x0060C304 File Offset: 0x0060A504
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool powerInfo = reader.ReadBoolean();
				base.SetPowerInfo(powerInfo);
			}

			// Token: 0x06003663 RID: 13923 RVA: 0x0060C26F File Offset: 0x0060A46F
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}
		}

		// Token: 0x020006C3 RID: 1731
		public class SpawnRateSliderPerPlayerPower : CreativePowers.APerPlayerSliderPower, IPersistentPerPlayerContent
		{
			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x06003665 RID: 13925 RVA: 0x0060C31F File Offset: 0x0060A51F
			// (set) Token: 0x06003666 RID: 13926 RVA: 0x0060C327 File Offset: 0x0060A527
			public float StrengthMultiplierToGiveNPCs { get; private set; }

			// Token: 0x06003667 RID: 13927 RVA: 0x0060C330 File Offset: 0x0060A530
			public SpawnRateSliderPerPlayerPower()
			{
				this._powerNameKey = "CreativePowers.NPCSpawnRateSlider";
				this._sliderDefaultValue = 0.5f;
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.EnemySpawnRate;
			}

			// Token: 0x06003668 RID: 13928 RVA: 0x0060C359 File Offset: 0x0060A559
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

			// Token: 0x06003669 RID: 13929 RVA: 0x0003C3EC File Offset: 0x0003A5EC
			internal override void UpdateInfoFromSliderValueCache()
			{
			}

			// Token: 0x0600366A RID: 13930 RVA: 0x0060C390 File Offset: 0x0060A590
			public override float RemapSliderValueToPowerValue(float sliderValue)
			{
				if (sliderValue < 0.5f)
				{
					return Utils.Remap(sliderValue, 0f, 0.5f, 0.1f, 1f, true);
				}
				return Utils.Remap(sliderValue, 0.5f, 1f, 1f, 10f, true);
			}

			// Token: 0x0600366B RID: 13931 RVA: 0x0060C3DC File Offset: 0x0060A5DC
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uiverticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uiverticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(77f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uiverticalSlider);
				uipanel.OnUpdate += CreativePowersHelper.UpdateUseMouseInterface;
				UIText uitext = new UIText("x10", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uitext.OnMouseOut += this.Button_OnMouseOut;
				uitext.OnMouseOver += this.Button_OnMouseOver;
				uitext.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uitext);
				UIText uitext2 = new UIText("x1", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uitext2.OnMouseOut += this.Button_OnMouseOut;
				uitext2.OnMouseOver += this.Button_OnMouseOver;
				uitext2.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uitext2);
				UIText uitext3 = new UIText("x0", 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uitext3.OnMouseOut += this.Button_OnMouseOut;
				uitext3.OnMouseOver += this.Button_OnMouseOver;
				uitext3.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uitext3);
				return uipanel;
			}

			// Token: 0x0600366C RID: 13932 RVA: 0x0060C5B0 File Offset: 0x0060A7B0
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600366D RID: 13933 RVA: 0x0060C5E8 File Offset: 0x0060A7E8
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600366E RID: 13934 RVA: 0x0060C61F File Offset: 0x0060A81F
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboard(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600366F RID: 13935 RVA: 0x0060C641 File Offset: 0x0060A841
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboard(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003670 RID: 13936 RVA: 0x0060C663 File Offset: 0x0060A863
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboard(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06003671 RID: 13937 RVA: 0x0060C688 File Offset: 0x0060A888
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string cursorText = "x" + this.RemapSliderValueToPowerValue(base.GetSliderValue()).ToString("F2");
					if (this.GetShouldDisableSpawnsFor(Main.myPlayer))
					{
						cursorText = Language.GetTextValue(this._powerNameKey + "EnemySpawnsDisabled");
					}
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref cursorText);
					Main.instance.MouseTextNoOverride(cursorText, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x06003672 RID: 13938 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06003673 RID: 13939 RVA: 0x0060C700 File Offset: 0x0060A900
			public void Save(Player player, BinaryWriter writer)
			{
				float sliderCurrentValueCache = this._sliderCurrentValueCache;
				writer.Write(sliderCurrentValueCache);
			}

			// Token: 0x06003674 RID: 13940 RVA: 0x0060C71B File Offset: 0x0060A91B
			public void ResetDataForNewPlayer(Player player)
			{
				player.savedPerPlayerFieldsThatArentInThePlayerClass.spawnRatePowerSliderValue = this._sliderDefaultValue;
			}

			// Token: 0x06003675 RID: 13941 RVA: 0x0060C730 File Offset: 0x0060A930
			public void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				float spawnRatePowerSliderValue = reader.ReadSingle();
				player.savedPerPlayerFieldsThatArentInThePlayerClass.spawnRatePowerSliderValue = spawnRatePowerSliderValue;
			}

			// Token: 0x06003676 RID: 13942 RVA: 0x0060C750 File Offset: 0x0060A950
			public void ApplyLoadedDataToOutOfPlayerFields(Player player)
			{
				base.PushChangeAndSetSlider(player.savedPerPlayerFieldsThatArentInThePlayerClass.spawnRatePowerSliderValue);
			}
		}
	}
}
