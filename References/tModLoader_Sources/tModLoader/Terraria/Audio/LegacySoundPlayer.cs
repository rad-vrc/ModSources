using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria.ID;

namespace Terraria.Audio
{
	// Token: 0x02000764 RID: 1892
	public class LegacySoundPlayer
	{
		// Token: 0x06004C81 RID: 19585 RVA: 0x0066DAD4 File Offset: 0x0066BCD4
		public LegacySoundPlayer(IServiceProvider services)
		{
			this._services = services;
			this._trackedInstances = new List<SoundEffectInstance>();
			this.LoadAll();
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x0066DC9F File Offset: 0x0066BE9F
		public void Reload()
		{
			this.CreateAllSoundInstances();
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x0066DCA7 File Offset: 0x0066BEA7
		private void LoadAll()
		{
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x0066DCAC File Offset: 0x0066BEAC
		public void CreateAllSoundInstances()
		{
			foreach (SoundEffectInstance soundEffectInstance in this._trackedInstances)
			{
				soundEffectInstance.Dispose();
			}
			this._trackedInstances.Clear();
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x0066DD08 File Offset: 0x0066BF08
		private SoundEffectInstance CreateInstance(Asset<SoundEffect> asset)
		{
			SoundEffectInstance soundEffectInstance = asset.Value.CreateInstance();
			this._trackedInstances.Add(soundEffectInstance);
			return soundEffectInstance;
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x0066DD2E File Offset: 0x0066BF2E
		private Asset<SoundEffect> Load(string assetName)
		{
			return XnaExtensions.Get<IAssetRepository>(this._services).Request<SoundEffect>(assetName, 2);
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x0066DD44 File Offset: 0x0066BF44
		public SoundEffectInstance PlaySound(int type, int x = -1, int y = -1, int Style = 1, float volumeScale = 1f, float pitchOffset = 0f)
		{
			int num = Style;
			try
			{
				if (Main.dedServ)
				{
					return null;
				}
				if (Main.soundVolume == 0f && (type < 30 || type > 35))
				{
					return null;
				}
				bool flag = false;
				float num2 = 1f;
				float num3 = 0f;
				if (x == -1 || y == -1)
				{
					flag = true;
				}
				else
				{
					if (WorldGen.gen)
					{
						return null;
					}
					if (Main.netMode == 2)
					{
						return null;
					}
					Vector2 vector;
					vector..ctor(Main.screenPosition.X + (float)Main.screenWidth * 0.5f, Main.screenPosition.Y + (float)Main.screenHeight * 0.5f);
					float num24 = Math.Abs((float)x - vector.X);
					float num4 = Math.Abs((float)y - vector.Y);
					float num5 = (float)Math.Sqrt((double)(num24 * num24 + num4 * num4));
					int num6 = 2500;
					if (num5 < (float)num6)
					{
						flag = true;
						num3 = ((type != 43) ? (((float)x - vector.X) / ((float)Main.screenWidth * 0.5f)) : (((float)x - vector.X) / 900f));
						num2 = 1f - num5 / (float)num6;
					}
				}
				if (num3 < -1f)
				{
					num3 = -1f;
				}
				if (num3 > 1f)
				{
					num3 = 1f;
				}
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				if (num2 <= 0f && (type < 34 || type > 35 || type > 39))
				{
					return null;
				}
				if (flag)
				{
					if (this.DoesSoundScaleWithAmbientVolume(type))
					{
						num2 *= Main.ambientVolume * !Main.gameInactive;
						if (Main.gameMenu)
						{
							num2 = 0f;
						}
					}
					else
					{
						num2 *= Main.soundVolume;
					}
					if (num2 > 1f)
					{
						num2 = 1f;
					}
					if (num2 <= 0f && (type < 30 || type > 35) && type != 39)
					{
						return null;
					}
					SoundEffectInstance soundEffectInstance = null;
					switch (type)
					{
					case 0:
					{
						int num7 = Main.rand.Next(3);
						if (this.SoundInstanceDig[num7] != null)
						{
							this.SoundInstanceDig[num7].Stop();
						}
						this.SoundInstanceDig[num7] = this.SoundDig[num7].Value.CreateInstance();
						this.SoundInstanceDig[num7].Volume = num2;
						this.SoundInstanceDig[num7].Pan = num3;
						this.SoundInstanceDig[num7].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceDig[num7];
						goto IL_25A3;
					}
					case 1:
					{
						int num8 = Main.rand.Next(3);
						if (this.SoundInstancePlayerHit[num8] != null)
						{
							this.SoundInstancePlayerHit[num8].Stop();
						}
						this.SoundInstancePlayerHit[num8] = this.SoundPlayerHit[num8].Value.CreateInstance();
						this.SoundInstancePlayerHit[num8].Volume = num2;
						this.SoundInstancePlayerHit[num8].Pan = num3;
						soundEffectInstance = this.SoundInstancePlayerHit[num8];
						goto IL_25A3;
					}
					case 2:
						if (num == 176)
						{
							num2 *= 0.9f;
						}
						if (num == 129)
						{
							num2 *= 0.6f;
						}
						if (num == 123)
						{
							num2 *= 0.5f;
						}
						if (num == 124 || num == 125)
						{
							num2 *= 0.65f;
						}
						if (num == 116)
						{
							num2 *= 0.5f;
						}
						if (num <= 37)
						{
							if (num != 1)
							{
								if (num == 37)
								{
									num2 *= 0.5f;
								}
							}
							else
							{
								int num25 = Main.rand.Next(3);
								if (num25 == 1)
								{
									num = 18;
								}
								if (num25 == 2)
								{
									num = 19;
								}
							}
						}
						else
						{
							switch (num)
							{
							case 52:
								num2 *= 0.35f;
								break;
							case 53:
							case 55:
								num2 *= 0.75f;
								if (num == 55)
								{
									num2 *= 0.75f;
								}
								if (this.SoundInstanceItem[num] != null && this.SoundInstanceItem[num].State == null)
								{
									return null;
								}
								break;
							case 54:
								break;
							default:
								if (num != 157)
								{
									if (num == 158)
									{
										num2 *= 0.8f;
									}
								}
								else
								{
									num2 *= 0.7f;
								}
								break;
							}
						}
						if (num <= 34)
						{
							if (num <= 24)
							{
								if (num - 9 <= 1 || num == 24)
								{
									goto IL_727;
								}
							}
							else if (num == 26 || num == 34)
							{
								goto IL_727;
							}
						}
						else if (num <= 103)
						{
							if (num == 43 || num == 103)
							{
								goto IL_727;
							}
						}
						else
						{
							if (num == 156)
							{
								goto IL_727;
							}
							if (num != 159)
							{
								if (num == 162)
								{
									goto IL_727;
								}
							}
							else
							{
								if (this.SoundInstanceItem[num] != null && this.SoundInstanceItem[num].State == null)
								{
									return null;
								}
								num2 *= 0.75f;
								goto IL_727;
							}
						}
						if (this.SoundInstanceItem[num] != null)
						{
							this.SoundInstanceItem[num].Stop();
						}
						IL_727:
						this.SoundInstanceItem[num] = this.SoundItem[num].Value.CreateInstance();
						this.SoundInstanceItem[num].Volume = num2;
						this.SoundInstanceItem[num].Pan = num3;
						if (num <= 55)
						{
							if (num == 53)
							{
								this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-20, -11) * 0.02f;
								goto IL_88C;
							}
							if (num == 55)
							{
								this.SoundInstanceItem[num].Pitch = (float)(-(float)Main.rand.Next(-20, -11)) * 0.02f;
								goto IL_88C;
							}
						}
						else
						{
							if (num == 132)
							{
								this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-20, 21) * 0.001f;
								goto IL_88C;
							}
							if (num == 153)
							{
								this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-50, 51) * 0.003f;
								goto IL_88C;
							}
							if (num == 156)
							{
								this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-50, 51) * 0.002f;
								this.SoundInstanceItem[num].Volume *= 0.6f;
								goto IL_88C;
							}
						}
						this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-6, 7) * 0.01f;
						IL_88C:
						if (num == 26 || num == 35 || num == 47)
						{
							this.SoundInstanceItem[num].Volume = num2 * 0.75f;
							this.SoundInstanceItem[num].Pitch = Main.musicPitch;
						}
						if (num == 169)
						{
							this.SoundInstanceItem[num].Pitch -= 0.8f;
						}
						soundEffectInstance = this.SoundInstanceItem[num];
						goto IL_25A3;
					case 3:
						if (num >= 20 && num <= 54)
						{
							num2 *= 0.5f;
						}
						if (num == 57 && this.SoundInstanceNpcHit[num] != null && this.SoundInstanceNpcHit[num].State == null)
						{
							return null;
						}
						if (num == 57)
						{
							num2 *= 0.6f;
						}
						if (num == 55 || num == 56)
						{
							num2 *= 0.5f;
						}
						if (this.SoundInstanceNpcHit[num] != null)
						{
							this.SoundInstanceNpcHit[num].Stop();
						}
						this.SoundInstanceNpcHit[num] = this.SoundNpcHit[num].Value.CreateInstance();
						this.SoundInstanceNpcHit[num].Volume = num2;
						this.SoundInstanceNpcHit[num].Pan = num3;
						this.SoundInstanceNpcHit[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceNpcHit[num];
						goto IL_25A3;
					case 4:
						if (num >= 23 && num <= 57)
						{
							num2 *= 0.5f;
						}
						if (num == 61)
						{
							num2 *= 0.6f;
						}
						if (num == 62)
						{
							num2 *= 0.6f;
						}
						if (num == 10 && this.SoundInstanceNpcKilled[num] != null && this.SoundInstanceNpcKilled[num].State == null)
						{
							return null;
						}
						this.SoundInstanceNpcKilled[num] = this.SoundNpcKilled[num].Value.CreateInstance();
						this.SoundInstanceNpcKilled[num].Volume = num2;
						this.SoundInstanceNpcKilled[num].Pan = num3;
						this.SoundInstanceNpcKilled[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceNpcKilled[num];
						goto IL_25A3;
					case 5:
						if (this.SoundInstancePlayerKilled != null)
						{
							this.SoundInstancePlayerKilled.Stop();
						}
						this.SoundInstancePlayerKilled = this.SoundPlayerKilled.Value.CreateInstance();
						this.SoundInstancePlayerKilled.Volume = num2;
						this.SoundInstancePlayerKilled.Pan = num3;
						soundEffectInstance = this.SoundInstancePlayerKilled;
						goto IL_25A3;
					case 6:
						if (this.SoundInstanceGrass != null)
						{
							this.SoundInstanceGrass.Stop();
						}
						this.SoundInstanceGrass = this.SoundGrass.Value.CreateInstance();
						this.SoundInstanceGrass.Volume = num2;
						this.SoundInstanceGrass.Pan = num3;
						this.SoundInstanceGrass.Pitch = (float)Main.rand.Next(-30, 31) * 0.01f;
						soundEffectInstance = this.SoundInstanceGrass;
						goto IL_25A3;
					case 7:
						if (this.SoundInstanceGrab != null)
						{
							this.SoundInstanceGrab.Stop();
						}
						this.SoundInstanceGrab = this.SoundGrab.Value.CreateInstance();
						this.SoundInstanceGrab.Volume = num2;
						this.SoundInstanceGrab.Pan = num3;
						this.SoundInstanceGrab.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceGrab;
						goto IL_25A3;
					case 8:
						if (this.SoundInstanceDoorOpen != null)
						{
							this.SoundInstanceDoorOpen.Stop();
						}
						this.SoundInstanceDoorOpen = this.SoundDoorOpen.Value.CreateInstance();
						this.SoundInstanceDoorOpen.Volume = num2;
						this.SoundInstanceDoorOpen.Pan = num3;
						this.SoundInstanceDoorOpen.Pitch = (float)Main.rand.Next(-20, 21) * 0.01f;
						soundEffectInstance = this.SoundInstanceDoorOpen;
						goto IL_25A3;
					case 9:
						if (this.SoundInstanceDoorClosed != null)
						{
							this.SoundInstanceDoorClosed.Stop();
						}
						this.SoundInstanceDoorClosed = this.SoundDoorClosed.Value.CreateInstance();
						this.SoundInstanceDoorClosed.Volume = num2;
						this.SoundInstanceDoorClosed.Pan = num3;
						this.SoundInstanceDoorClosed.Pitch = (float)Main.rand.Next(-20, 21) * 0.01f;
						soundEffectInstance = this.SoundInstanceDoorClosed;
						goto IL_25A3;
					case 10:
						if (this.SoundInstanceMenuOpen != null)
						{
							this.SoundInstanceMenuOpen.Stop();
						}
						this.SoundInstanceMenuOpen = this.SoundMenuOpen.Value.CreateInstance();
						this.SoundInstanceMenuOpen.Volume = num2;
						this.SoundInstanceMenuOpen.Pan = num3;
						soundEffectInstance = this.SoundInstanceMenuOpen;
						goto IL_25A3;
					case 11:
						if (this.SoundInstanceMenuClose != null)
						{
							this.SoundInstanceMenuClose.Stop();
						}
						this.SoundInstanceMenuClose = this.SoundMenuClose.Value.CreateInstance();
						this.SoundInstanceMenuClose.Volume = num2;
						this.SoundInstanceMenuClose.Pan = num3;
						soundEffectInstance = this.SoundInstanceMenuClose;
						goto IL_25A3;
					case 12:
						if (Main.hasFocus)
						{
							if (this.SoundInstanceMenuTick != null)
							{
								this.SoundInstanceMenuTick.Stop();
							}
							this.SoundInstanceMenuTick = this.SoundMenuTick.Value.CreateInstance();
							this.SoundInstanceMenuTick.Volume = num2;
							this.SoundInstanceMenuTick.Pan = num3;
							soundEffectInstance = this.SoundInstanceMenuTick;
							goto IL_25A3;
						}
						goto IL_25A3;
					case 13:
						if (this.SoundInstanceShatter != null)
						{
							this.SoundInstanceShatter.Stop();
						}
						this.SoundInstanceShatter = this.SoundShatter.Value.CreateInstance();
						this.SoundInstanceShatter.Volume = num2;
						this.SoundInstanceShatter.Pan = num3;
						soundEffectInstance = this.SoundInstanceShatter;
						goto IL_25A3;
					case 14:
					{
						if (Style != 489)
						{
							if (Style == 542)
							{
								int num9 = 7;
								this.SoundInstanceZombie[num9] = this.SoundZombie[num9].Value.CreateInstance();
								this.SoundInstanceZombie[num9].Volume = num2 * 0.4f;
								this.SoundInstanceZombie[num9].Pan = num3;
								soundEffectInstance = this.SoundInstanceZombie[num9];
								goto IL_25A3;
							}
							if (Style != 586)
							{
								int num10 = Main.rand.Next(3);
								this.SoundInstanceZombie[num10] = this.SoundZombie[num10].Value.CreateInstance();
								this.SoundInstanceZombie[num10].Volume = num2 * 0.4f;
								this.SoundInstanceZombie[num10].Pan = num3;
								soundEffectInstance = this.SoundInstanceZombie[num10];
								goto IL_25A3;
							}
						}
						int num11 = Main.rand.Next(21, 24);
						this.SoundInstanceZombie[num11] = this.SoundZombie[num11].Value.CreateInstance();
						this.SoundInstanceZombie[num11].Volume = num2 * 0.4f;
						this.SoundInstanceZombie[num11].Pan = num3;
						soundEffectInstance = this.SoundInstanceZombie[num11];
						goto IL_25A3;
					}
					case 15:
					{
						float num12 = 1f;
						if (num == 4)
						{
							num = 1;
							num12 = 0.25f;
						}
						if (this.SoundInstanceRoar[num] == null || this.SoundInstanceRoar[num].State == 2)
						{
							this.SoundInstanceRoar[num] = this.SoundRoar[num].Value.CreateInstance();
							this.SoundInstanceRoar[num].Volume = num2 * num12;
							this.SoundInstanceRoar[num].Pan = num3;
							soundEffectInstance = this.SoundInstanceRoar[num];
							goto IL_25A3;
						}
						goto IL_25A3;
					}
					case 16:
						if (this.SoundInstanceDoubleJump != null)
						{
							this.SoundInstanceDoubleJump.Stop();
						}
						this.SoundInstanceDoubleJump = this.SoundDoubleJump.Value.CreateInstance();
						this.SoundInstanceDoubleJump.Volume = num2;
						this.SoundInstanceDoubleJump.Pan = num3;
						this.SoundInstanceDoubleJump.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceDoubleJump;
						goto IL_25A3;
					case 17:
						if (this.SoundInstanceRun != null)
						{
							this.SoundInstanceRun.Stop();
						}
						this.SoundInstanceRun = this.SoundRun.Value.CreateInstance();
						this.SoundInstanceRun.Volume = num2;
						this.SoundInstanceRun.Pan = num3;
						this.SoundInstanceRun.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceRun;
						goto IL_25A3;
					case 18:
						this.SoundInstanceCoins = this.SoundCoins.Value.CreateInstance();
						this.SoundInstanceCoins.Volume = num2;
						this.SoundInstanceCoins.Pan = num3;
						soundEffectInstance = this.SoundInstanceCoins;
						goto IL_25A3;
					case 19:
						if (this.SoundInstanceSplash[num] != null && this.SoundInstanceSplash[num].State != 2)
						{
							goto IL_25A3;
						}
						this.SoundInstanceSplash[num] = this.SoundSplash[num].Value.CreateInstance();
						if (num == 2 || num == 3)
						{
							num2 *= 0.75f;
						}
						if (num == 4 || num == 5)
						{
							num2 *= 0.75f;
							this.SoundInstanceSplash[num].Pitch = (float)Main.rand.Next(-20, 1) * 0.01f;
						}
						else
						{
							this.SoundInstanceSplash[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						}
						this.SoundInstanceSplash[num].Volume = num2;
						this.SoundInstanceSplash[num].Pan = num3;
						if (num != 4)
						{
							if (num != 5)
							{
								soundEffectInstance = this.SoundInstanceSplash[num];
								goto IL_25A3;
							}
							if (this.SoundInstanceSplash[4] == null || this.SoundInstanceSplash[4].State == 2)
							{
								soundEffectInstance = this.SoundInstanceSplash[num];
								goto IL_25A3;
							}
							goto IL_25A3;
						}
						else
						{
							if (this.SoundInstanceSplash[5] == null || this.SoundInstanceSplash[5].State == 2)
							{
								soundEffectInstance = this.SoundInstanceSplash[num];
								goto IL_25A3;
							}
							goto IL_25A3;
						}
						break;
					case 20:
					{
						int num13 = Main.rand.Next(3);
						if (this.SoundInstanceFemaleHit[num13] != null)
						{
							this.SoundInstanceFemaleHit[num13].Stop();
						}
						this.SoundInstanceFemaleHit[num13] = this.SoundFemaleHit[num13].Value.CreateInstance();
						this.SoundInstanceFemaleHit[num13].Volume = num2;
						this.SoundInstanceFemaleHit[num13].Pan = num3;
						soundEffectInstance = this.SoundInstanceFemaleHit[num13];
						goto IL_25A3;
					}
					case 21:
					{
						int num14 = Main.rand.Next(3);
						if (this.SoundInstanceTink[num14] != null)
						{
							this.SoundInstanceTink[num14].Stop();
						}
						this.SoundInstanceTink[num14] = this.SoundTink[num14].Value.CreateInstance();
						this.SoundInstanceTink[num14].Volume = num2;
						this.SoundInstanceTink[num14].Pan = num3;
						soundEffectInstance = this.SoundInstanceTink[num14];
						goto IL_25A3;
					}
					case 22:
						if (this.SoundInstanceUnlock != null)
						{
							this.SoundInstanceUnlock.Stop();
						}
						this.SoundInstanceUnlock = this.SoundUnlock.Value.CreateInstance();
						this.SoundInstanceUnlock.Volume = num2;
						this.SoundInstanceUnlock.Pan = num3;
						soundEffectInstance = this.SoundInstanceUnlock;
						goto IL_25A3;
					case 23:
						if (this.SoundInstanceDrown != null)
						{
							this.SoundInstanceDrown.Stop();
						}
						this.SoundInstanceDrown = this.SoundDrown.Value.CreateInstance();
						this.SoundInstanceDrown.Volume = num2;
						this.SoundInstanceDrown.Pan = num3;
						soundEffectInstance = this.SoundInstanceDrown;
						goto IL_25A3;
					case 24:
						this.SoundInstanceChat = this.SoundChat.Value.CreateInstance();
						this.SoundInstanceChat.Volume = num2;
						this.SoundInstanceChat.Pan = num3;
						soundEffectInstance = this.SoundInstanceChat;
						goto IL_25A3;
					case 25:
						this.SoundInstanceMaxMana = this.SoundMaxMana.Value.CreateInstance();
						this.SoundInstanceMaxMana.Volume = num2;
						this.SoundInstanceMaxMana.Pan = num3;
						soundEffectInstance = this.SoundInstanceMaxMana;
						goto IL_25A3;
					case 26:
					{
						int num15 = Main.rand.Next(3, 5);
						this.SoundInstanceZombie[num15] = this.SoundZombie[num15].Value.CreateInstance();
						this.SoundInstanceZombie[num15].Volume = num2 * 0.9f;
						this.SoundInstanceZombie[num15].Pan = num3;
						this.SoundInstanceZombie[num15].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num15];
						goto IL_25A3;
					}
					case 27:
						if (this.SoundInstancePixie != null && this.SoundInstancePixie.State == null)
						{
							this.SoundInstancePixie.Volume = num2;
							this.SoundInstancePixie.Pan = num3;
							this.SoundInstancePixie.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
							return null;
						}
						if (this.SoundInstancePixie != null)
						{
							this.SoundInstancePixie.Stop();
						}
						this.SoundInstancePixie = this.SoundPixie.Value.CreateInstance();
						this.SoundInstancePixie.Volume = num2;
						this.SoundInstancePixie.Pan = num3;
						this.SoundInstancePixie.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstancePixie;
						goto IL_25A3;
					case 28:
						if (this.SoundInstanceMech[num] != null && this.SoundInstanceMech[num].State == null)
						{
							return null;
						}
						this.SoundInstanceMech[num] = this.SoundMech[num].Value.CreateInstance();
						this.SoundInstanceMech[num].Volume = num2;
						this.SoundInstanceMech[num].Pan = num3;
						this.SoundInstanceMech[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceMech[num];
						goto IL_25A3;
					case 29:
						if (num >= 24 && num <= 87)
						{
							num2 *= 0.5f;
						}
						if (num >= 88 && num <= 91)
						{
							num2 *= 0.7f;
						}
						if (num >= 93 && num <= 99)
						{
							num2 *= 0.4f;
						}
						if (num == 92)
						{
							num2 *= 0.5f;
						}
						if (num == 103)
						{
							num2 *= 0.4f;
						}
						if (num == 104)
						{
							num2 *= 0.55f;
						}
						if (num == 100 || num == 101)
						{
							num2 *= 0.25f;
						}
						if (num == 102)
						{
							num2 *= 0.4f;
						}
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == null)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
						goto IL_25A3;
					case 43:
					{
						int num16 = Main.rand.Next(this.SoundThunder.Length);
						int i = 0;
						while (i < this.SoundThunder.Length && this.SoundInstanceThunder[num16] != null && this.SoundInstanceThunder[num16].State == null)
						{
							num16 = Main.rand.Next(this.SoundThunder.Length);
							i++;
						}
						if (this.SoundInstanceThunder[num16] != null)
						{
							this.SoundInstanceThunder[num16].Stop();
						}
						this.SoundInstanceThunder[num16] = this.SoundThunder[num16].Value.CreateInstance();
						this.SoundInstanceThunder[num16].Volume = num2;
						this.SoundInstanceThunder[num16].Pan = num3;
						this.SoundInstanceThunder[num16].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceThunder[num16];
						goto IL_25A3;
					}
					case 44:
						num = Main.rand.Next(106, 109);
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 1) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
						goto IL_25A3;
					case 45:
						num = 109;
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == null)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.3f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
						goto IL_25A3;
					case 46:
						if (this.SoundInstanceZombie[110] != null && this.SoundInstanceZombie[110].State == null)
						{
							return null;
						}
						if (this.SoundInstanceZombie[111] != null && this.SoundInstanceZombie[111].State == null)
						{
							return null;
						}
						num = Main.rand.Next(110, 112);
						if (Main.rand.Next(300) == 0)
						{
							num = ((Main.rand.Next(3) == 0) ? 114 : ((Main.rand.Next(2) != 0) ? 112 : 113));
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.9f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
						goto IL_25A3;
					case 63:
					{
						int num17 = Main.rand.Next(1, 4);
						if (this.SoundInstanceResearch[num17] != null)
						{
							this.SoundInstanceResearch[num17].Stop();
						}
						this.SoundInstanceResearch[num17] = this.SoundResearch[num17].Value.CreateInstance();
						this.SoundInstanceResearch[num17].Volume = num2;
						this.SoundInstanceResearch[num17].Pan = num3;
						soundEffectInstance = this.SoundInstanceResearch[num17];
						goto IL_25A3;
					}
					case 64:
						if (this.SoundInstanceResearch[0] != null)
						{
							this.SoundInstanceResearch[0].Stop();
						}
						this.SoundInstanceResearch[0] = this.SoundResearch[0].Value.CreateInstance();
						this.SoundInstanceResearch[0].Volume = num2;
						this.SoundInstanceResearch[0].Pan = num3;
						soundEffectInstance = this.SoundInstanceResearch[0];
						goto IL_25A3;
					}
					if (type <= 45)
					{
						switch (type)
						{
						case 30:
							num = Main.rand.Next(10, 12);
							if (Main.rand.Next(300) == 0)
							{
								num = 12;
								if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == null)
								{
									return null;
								}
							}
							this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
							this.SoundInstanceZombie[num].Volume = num2 * 0.75f;
							this.SoundInstanceZombie[num].Pan = num3;
							if (num != 12)
							{
								this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 1) * 0.01f;
							}
							else
							{
								this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-40, 21) * 0.01f;
							}
							soundEffectInstance = this.SoundInstanceZombie[num];
							goto IL_25A3;
						case 31:
							num = 13;
							this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
							this.SoundInstanceZombie[num].Volume = num2 * 0.35f;
							this.SoundInstanceZombie[num].Pan = num3;
							this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-40, 21) * 0.01f;
							soundEffectInstance = this.SoundInstanceZombie[num];
							goto IL_25A3;
						case 32:
							if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == null)
							{
								return null;
							}
							this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
							this.SoundInstanceZombie[num].Volume = num2 * 0.15f;
							this.SoundInstanceZombie[num].Pan = num3;
							this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 26) * 0.01f;
							soundEffectInstance = this.SoundInstanceZombie[num];
							goto IL_25A3;
						case 33:
							num = 15;
							if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == null)
							{
								return null;
							}
							this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
							this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
							this.SoundInstanceZombie[num].Pan = num3;
							this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 31) * 0.01f;
							soundEffectInstance = this.SoundInstanceZombie[num];
							goto IL_25A3;
						default:
							if (type == 45)
							{
								num = 109;
								this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
								this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
								this.SoundInstanceZombie[num].Pan = num3;
								this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 1) * 0.01f;
								soundEffectInstance = this.SoundInstanceZombie[num];
								goto IL_25A3;
							}
							break;
						}
					}
					else
					{
						if (type - 47 <= 5)
						{
							num = 133 + type - 47;
							for (int j = 133; j <= 138; j++)
							{
								if (this.SoundInstanceItem[j] != null && this.SoundInstanceItem[j].State == null)
								{
									this.SoundInstanceItem[j].Stop();
								}
							}
							this.SoundInstanceItem[num] = this.SoundItem[num].Value.CreateInstance();
							this.SoundInstanceItem[num].Volume = num2 * 0.45f;
							this.SoundInstanceItem[num].Pan = num3;
							soundEffectInstance = this.SoundInstanceItem[num];
							goto IL_25A3;
						}
						switch (type)
						{
						case 66:
							num = Main.rand.Next(121, 124);
							if (this.SoundInstanceZombie[121] != null && this.SoundInstanceZombie[121].State == null)
							{
								return null;
							}
							if (this.SoundInstanceZombie[122] != null && this.SoundInstanceZombie[122].State == null)
							{
								return null;
							}
							if (this.SoundInstanceZombie[123] != null && this.SoundInstanceZombie[123].State == null)
							{
								return null;
							}
							this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
							this.SoundInstanceZombie[num].Volume = num2 * 0.45f;
							this.SoundInstanceZombie[num].Pan = num3;
							this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-15, 16) * 0.01f;
							soundEffectInstance = this.SoundInstanceZombie[num];
							goto IL_25A3;
						case 67:
							num = Main.rand.Next(118, 121);
							if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == null)
							{
								return null;
							}
							this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
							this.SoundInstanceZombie[num].Volume = num2 * 0.3f;
							this.SoundInstanceZombie[num].Pan = num3;
							this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-5, 6) * 0.01f;
							soundEffectInstance = this.SoundInstanceZombie[num];
							goto IL_25A3;
						case 68:
							num = Main.rand.Next(126, 129);
							if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == null)
							{
								return null;
							}
							this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
							this.SoundInstanceZombie[num].Volume = num2 * 0.22f;
							this.SoundInstanceZombie[num].Pan = num3;
							this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-5, 6) * 0.01f;
							soundEffectInstance = this.SoundInstanceZombie[num];
							goto IL_25A3;
						case 69:
							num = Main.rand.Next(129, 131);
							if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == null)
							{
								return null;
							}
							this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
							this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
							this.SoundInstanceZombie[num].Pan = num3;
							this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-5, 6) * 0.01f;
							soundEffectInstance = this.SoundInstanceZombie[num];
							goto IL_25A3;
						}
					}
					if (type >= 53 && type <= 62)
					{
						num = 139 + type - 53;
						if (this.SoundInstanceItem[num] != null && this.SoundInstanceItem[num].State == null)
						{
							this.SoundInstanceItem[num].Stop();
						}
						this.SoundInstanceItem[num] = this.SoundItem[num].Value.CreateInstance();
						this.SoundInstanceItem[num].Volume = num2 * 0.7f;
						this.SoundInstanceItem[num].Pan = num3;
						soundEffectInstance = this.SoundInstanceItem[num];
					}
					else
					{
						switch (type)
						{
						case 34:
						{
							float num18 = (float)num / 50f;
							if (num18 > 1f)
							{
								num18 = 1f;
							}
							num2 *= num18;
							num2 *= 0.2f;
							num2 *= 1f - Main.shimmerAlpha;
							if (num2 <= 0f || x == -1 || y == -1)
							{
								if (this.SoundInstanceLiquid[0] != null && this.SoundInstanceLiquid[0].State == null)
								{
									this.SoundInstanceLiquid[0].Stop();
								}
							}
							else if (this.SoundInstanceLiquid[0] != null && this.SoundInstanceLiquid[0].State == null)
							{
								this.SoundInstanceLiquid[0].Volume = num2;
								this.SoundInstanceLiquid[0].Pan = num3;
								this.SoundInstanceLiquid[0].Pitch = -0.2f;
							}
							else
							{
								this.SoundInstanceLiquid[0] = this.SoundLiquid[0].Value.CreateInstance();
								this.SoundInstanceLiquid[0].Volume = num2;
								this.SoundInstanceLiquid[0].Pan = num3;
								soundEffectInstance = this.SoundInstanceLiquid[0];
							}
							break;
						}
						case 35:
						{
							float num19 = (float)num / 50f;
							if (num19 > 1f)
							{
								num19 = 1f;
							}
							num2 *= num19;
							num2 *= 0.65f;
							num2 *= 1f - Main.shimmerAlpha;
							if (num2 <= 0f || x == -1 || y == -1)
							{
								if (this.SoundInstanceLiquid[1] != null && this.SoundInstanceLiquid[1].State == null)
								{
									this.SoundInstanceLiquid[1].Stop();
								}
							}
							else if (this.SoundInstanceLiquid[1] != null && this.SoundInstanceLiquid[1].State == null)
							{
								this.SoundInstanceLiquid[1].Volume = num2;
								this.SoundInstanceLiquid[1].Pan = num3;
								this.SoundInstanceLiquid[1].Pitch = --0f;
							}
							else
							{
								this.SoundInstanceLiquid[1] = this.SoundLiquid[1].Value.CreateInstance();
								this.SoundInstanceLiquid[1].Volume = num2;
								this.SoundInstanceLiquid[1].Pan = num3;
								soundEffectInstance = this.SoundInstanceLiquid[1];
							}
							break;
						}
						case 36:
						{
							int num20 = Style;
							if (Style == -1)
							{
								num20 = 0;
							}
							this.SoundInstanceRoar[num20] = this.SoundRoar[num20].Value.CreateInstance();
							this.SoundInstanceRoar[num20].Volume = num2;
							this.SoundInstanceRoar[num20].Pan = num3;
							if (Style == -1)
							{
								this.SoundInstanceRoar[num20].Pitch += 0.6f;
							}
							soundEffectInstance = this.SoundInstanceRoar[num20];
							break;
						}
						case 37:
						{
							int num21 = Main.rand.Next(57, 59);
							num2 = ((!Main.starGame) ? (num2 * ((float)Style * 0.05f)) : (num2 * 0.15f));
							this.SoundInstanceItem[num21] = this.SoundItem[num21].Value.CreateInstance();
							this.SoundInstanceItem[num21].Volume = num2;
							this.SoundInstanceItem[num21].Pan = num3;
							this.SoundInstanceItem[num21].Pitch = (float)Main.rand.Next(-40, 41) * 0.01f;
							soundEffectInstance = this.SoundInstanceItem[num21];
							break;
						}
						case 38:
						{
							if (Main.starGame)
							{
								num2 *= 0.15f;
							}
							int num22 = Main.rand.Next(5);
							this.SoundInstanceCoin[num22] = this.SoundCoin[num22].Value.CreateInstance();
							this.SoundInstanceCoin[num22].Volume = num2;
							this.SoundInstanceCoin[num22].Pan = num3;
							this.SoundInstanceCoin[num22].Pitch = (float)Main.rand.Next(-40, 41) * 0.002f;
							soundEffectInstance = this.SoundInstanceCoin[num22];
							break;
						}
						case 39:
							this.SoundInstanceDrip[Style] = this.SoundDrip[Style].Value.CreateInstance();
							this.SoundInstanceDrip[Style].Volume = num2 * 0.5f;
							this.SoundInstanceDrip[Style].Pan = num3;
							this.SoundInstanceDrip[Style].Pitch = (float)Main.rand.Next(-30, 31) * 0.01f;
							soundEffectInstance = this.SoundInstanceDrip[Style];
							break;
						case 40:
							if (this.SoundInstanceCamera != null)
							{
								this.SoundInstanceCamera.Stop();
							}
							this.SoundInstanceCamera = this.SoundCamera.Value.CreateInstance();
							this.SoundInstanceCamera.Volume = num2;
							this.SoundInstanceCamera.Pan = num3;
							soundEffectInstance = this.SoundInstanceCamera;
							break;
						case 41:
							this.SoundInstanceMoonlordCry = this.SoundNpcKilled[10].Value.CreateInstance();
							this.SoundInstanceMoonlordCry.Volume = 1f / (1f + (new Vector2((float)x, (float)y) - Main.player[Main.myPlayer].position).Length());
							this.SoundInstanceMoonlordCry.Pan = num3;
							this.SoundInstanceMoonlordCry.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
							soundEffectInstance = this.SoundInstanceMoonlordCry;
							break;
						case 42:
							soundEffectInstance = this.TrackableSounds[num].Value.CreateInstance();
							soundEffectInstance.Volume = num2;
							soundEffectInstance.Pan = num3;
							this.TrackableSoundInstances[num] = soundEffectInstance;
							break;
						default:
							if (type == 65)
							{
								if (this.SoundInstanceZombie[115] != null && this.SoundInstanceZombie[115].State == null)
								{
									return null;
								}
								if (this.SoundInstanceZombie[116] != null && this.SoundInstanceZombie[116].State == null)
								{
									return null;
								}
								if (this.SoundInstanceZombie[117] != null && this.SoundInstanceZombie[117].State == null)
								{
									return null;
								}
								int num23 = Main.rand.Next(115, 118);
								this.SoundInstanceZombie[num23] = this.SoundZombie[num23].Value.CreateInstance();
								this.SoundInstanceZombie[num23].Volume = num2 * 0.5f;
								this.SoundInstanceZombie[num23].Pan = num3;
								soundEffectInstance = this.SoundInstanceZombie[num23];
							}
							break;
						}
					}
					IL_25A3:
					if (soundEffectInstance != null)
					{
						soundEffectInstance.Pitch += pitchOffset;
						soundEffectInstance.Volume *= volumeScale;
						soundEffectInstance.Play();
						SoundInstanceGarbageCollector.Track(soundEffectInstance);
					}
					return soundEffectInstance;
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06004C88 RID: 19592 RVA: 0x00670354 File Offset: 0x0066E554
		public SoundEffect GetTrackableSoundByStyleId(int id)
		{
			return this.TrackableSounds[id].Value;
		}

		// Token: 0x06004C89 RID: 19593 RVA: 0x00670364 File Offset: 0x0066E564
		public void StopAmbientSounds()
		{
			for (int i = 0; i < this.SoundInstanceLiquid.Length; i++)
			{
				if (this.SoundInstanceLiquid[i] != null)
				{
					this.SoundInstanceLiquid[i].Stop();
				}
			}
		}

		// Token: 0x06004C8A RID: 19594 RVA: 0x0067039C File Offset: 0x0066E59C
		public bool DoesSoundScaleWithAmbientVolume(int soundType)
		{
			switch (soundType)
			{
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 39:
			case 43:
			case 44:
			case 45:
			case 46:
				break;
			case 36:
			case 37:
			case 38:
			case 40:
			case 41:
			case 42:
				return false;
			default:
				if (soundType - 67 > 2)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x040060C1 RID: 24769
		public Asset<SoundEffect>[] SoundDrip = new Asset<SoundEffect>[3];

		// Token: 0x040060C2 RID: 24770
		public SoundEffectInstance[] SoundInstanceDrip = new SoundEffectInstance[3];

		// Token: 0x040060C3 RID: 24771
		public Asset<SoundEffect>[] SoundLiquid = new Asset<SoundEffect>[2];

		// Token: 0x040060C4 RID: 24772
		public SoundEffectInstance[] SoundInstanceLiquid = new SoundEffectInstance[2];

		// Token: 0x040060C5 RID: 24773
		public Asset<SoundEffect>[] SoundMech = new Asset<SoundEffect>[1];

		// Token: 0x040060C6 RID: 24774
		public SoundEffectInstance[] SoundInstanceMech = new SoundEffectInstance[1];

		// Token: 0x040060C7 RID: 24775
		public Asset<SoundEffect>[] SoundDig = new Asset<SoundEffect>[3];

		// Token: 0x040060C8 RID: 24776
		public SoundEffectInstance[] SoundInstanceDig = new SoundEffectInstance[3];

		// Token: 0x040060C9 RID: 24777
		public Asset<SoundEffect>[] SoundThunder = new Asset<SoundEffect>[7];

		// Token: 0x040060CA RID: 24778
		public SoundEffectInstance[] SoundInstanceThunder = new SoundEffectInstance[7];

		// Token: 0x040060CB RID: 24779
		public Asset<SoundEffect>[] SoundResearch = new Asset<SoundEffect>[4];

		// Token: 0x040060CC RID: 24780
		public SoundEffectInstance[] SoundInstanceResearch = new SoundEffectInstance[4];

		// Token: 0x040060CD RID: 24781
		public Asset<SoundEffect>[] SoundTink = new Asset<SoundEffect>[3];

		// Token: 0x040060CE RID: 24782
		public SoundEffectInstance[] SoundInstanceTink = new SoundEffectInstance[3];

		// Token: 0x040060CF RID: 24783
		public Asset<SoundEffect>[] SoundCoin = new Asset<SoundEffect>[5];

		// Token: 0x040060D0 RID: 24784
		public SoundEffectInstance[] SoundInstanceCoin = new SoundEffectInstance[5];

		// Token: 0x040060D1 RID: 24785
		public Asset<SoundEffect>[] SoundPlayerHit = new Asset<SoundEffect>[3];

		// Token: 0x040060D2 RID: 24786
		public SoundEffectInstance[] SoundInstancePlayerHit = new SoundEffectInstance[3];

		// Token: 0x040060D3 RID: 24787
		public Asset<SoundEffect>[] SoundFemaleHit = new Asset<SoundEffect>[3];

		// Token: 0x040060D4 RID: 24788
		public SoundEffectInstance[] SoundInstanceFemaleHit = new SoundEffectInstance[3];

		// Token: 0x040060D5 RID: 24789
		public Asset<SoundEffect> SoundPlayerKilled;

		// Token: 0x040060D6 RID: 24790
		public SoundEffectInstance SoundInstancePlayerKilled;

		// Token: 0x040060D7 RID: 24791
		public Asset<SoundEffect> SoundGrass;

		// Token: 0x040060D8 RID: 24792
		public SoundEffectInstance SoundInstanceGrass;

		// Token: 0x040060D9 RID: 24793
		public Asset<SoundEffect> SoundGrab;

		// Token: 0x040060DA RID: 24794
		public SoundEffectInstance SoundInstanceGrab;

		// Token: 0x040060DB RID: 24795
		public Asset<SoundEffect> SoundPixie;

		// Token: 0x040060DC RID: 24796
		public SoundEffectInstance SoundInstancePixie;

		// Token: 0x040060DD RID: 24797
		public Asset<SoundEffect>[] SoundItem = new Asset<SoundEffect>[SoundID.ItemSoundCount];

		// Token: 0x040060DE RID: 24798
		public SoundEffectInstance[] SoundInstanceItem = new SoundEffectInstance[SoundID.ItemSoundCount];

		// Token: 0x040060DF RID: 24799
		public Asset<SoundEffect>[] SoundNpcHit = new Asset<SoundEffect>[SoundID.NPCHitCount];

		// Token: 0x040060E0 RID: 24800
		public SoundEffectInstance[] SoundInstanceNpcHit = new SoundEffectInstance[SoundID.NPCHitCount];

		// Token: 0x040060E1 RID: 24801
		public Asset<SoundEffect>[] SoundNpcKilled = new Asset<SoundEffect>[SoundID.NPCDeathCount];

		// Token: 0x040060E2 RID: 24802
		public SoundEffectInstance[] SoundInstanceNpcKilled = new SoundEffectInstance[SoundID.NPCDeathCount];

		// Token: 0x040060E3 RID: 24803
		public SoundEffectInstance SoundInstanceMoonlordCry;

		// Token: 0x040060E4 RID: 24804
		public Asset<SoundEffect> SoundDoorOpen;

		// Token: 0x040060E5 RID: 24805
		public SoundEffectInstance SoundInstanceDoorOpen;

		// Token: 0x040060E6 RID: 24806
		public Asset<SoundEffect> SoundDoorClosed;

		// Token: 0x040060E7 RID: 24807
		public SoundEffectInstance SoundInstanceDoorClosed;

		// Token: 0x040060E8 RID: 24808
		public Asset<SoundEffect> SoundMenuOpen;

		// Token: 0x040060E9 RID: 24809
		public SoundEffectInstance SoundInstanceMenuOpen;

		// Token: 0x040060EA RID: 24810
		public Asset<SoundEffect> SoundMenuClose;

		// Token: 0x040060EB RID: 24811
		public SoundEffectInstance SoundInstanceMenuClose;

		// Token: 0x040060EC RID: 24812
		public Asset<SoundEffect> SoundMenuTick;

		// Token: 0x040060ED RID: 24813
		public SoundEffectInstance SoundInstanceMenuTick;

		// Token: 0x040060EE RID: 24814
		public Asset<SoundEffect> SoundShatter;

		// Token: 0x040060EF RID: 24815
		public SoundEffectInstance SoundInstanceShatter;

		// Token: 0x040060F0 RID: 24816
		public Asset<SoundEffect> SoundCamera;

		// Token: 0x040060F1 RID: 24817
		public SoundEffectInstance SoundInstanceCamera;

		// Token: 0x040060F2 RID: 24818
		internal const int ZombieSoundCount = 131;

		// Token: 0x040060F3 RID: 24819
		public Asset<SoundEffect>[] SoundZombie = new Asset<SoundEffect>[131];

		// Token: 0x040060F4 RID: 24820
		public SoundEffectInstance[] SoundInstanceZombie = new SoundEffectInstance[131];

		// Token: 0x040060F5 RID: 24821
		public Asset<SoundEffect>[] SoundRoar = new Asset<SoundEffect>[3];

		// Token: 0x040060F6 RID: 24822
		public SoundEffectInstance[] SoundInstanceRoar = new SoundEffectInstance[3];

		// Token: 0x040060F7 RID: 24823
		public Asset<SoundEffect>[] SoundSplash = new Asset<SoundEffect>[6];

		// Token: 0x040060F8 RID: 24824
		public SoundEffectInstance[] SoundInstanceSplash = new SoundEffectInstance[6];

		// Token: 0x040060F9 RID: 24825
		public Asset<SoundEffect> SoundDoubleJump;

		// Token: 0x040060FA RID: 24826
		public SoundEffectInstance SoundInstanceDoubleJump;

		// Token: 0x040060FB RID: 24827
		public Asset<SoundEffect> SoundRun;

		// Token: 0x040060FC RID: 24828
		public SoundEffectInstance SoundInstanceRun;

		// Token: 0x040060FD RID: 24829
		public Asset<SoundEffect> SoundCoins;

		// Token: 0x040060FE RID: 24830
		public SoundEffectInstance SoundInstanceCoins;

		// Token: 0x040060FF RID: 24831
		public Asset<SoundEffect> SoundUnlock;

		// Token: 0x04006100 RID: 24832
		public SoundEffectInstance SoundInstanceUnlock;

		// Token: 0x04006101 RID: 24833
		public Asset<SoundEffect> SoundChat;

		// Token: 0x04006102 RID: 24834
		public SoundEffectInstance SoundInstanceChat;

		// Token: 0x04006103 RID: 24835
		public Asset<SoundEffect> SoundMaxMana;

		// Token: 0x04006104 RID: 24836
		public SoundEffectInstance SoundInstanceMaxMana;

		// Token: 0x04006105 RID: 24837
		public Asset<SoundEffect> SoundDrown;

		// Token: 0x04006106 RID: 24838
		public SoundEffectInstance SoundInstanceDrown;

		// Token: 0x04006107 RID: 24839
		public Asset<SoundEffect>[] TrackableSounds;

		// Token: 0x04006108 RID: 24840
		public SoundEffectInstance[] TrackableSoundInstances;

		// Token: 0x04006109 RID: 24841
		private readonly IServiceProvider _services;

		// Token: 0x0400610A RID: 24842
		private List<SoundEffectInstance> _trackedInstances;
	}
}
