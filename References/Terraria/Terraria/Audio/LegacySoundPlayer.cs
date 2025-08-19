using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria.ID;

namespace Terraria.Audio
{
	// Token: 0x02000482 RID: 1154
	public class LegacySoundPlayer
	{
		// Token: 0x06002DFD RID: 11773 RVA: 0x005BFBD0 File Offset: 0x005BDDD0
		public LegacySoundPlayer(IServiceProvider services)
		{
			this._services = services;
			this._trackedInstances = new List<SoundEffectInstance>();
			this.LoadAll();
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x005BFD95 File Offset: 0x005BDF95
		public void Reload()
		{
			this.CreateAllSoundInstances();
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x005BFDA0 File Offset: 0x005BDFA0
		private void LoadAll()
		{
			this.SoundMech[0] = this.Load("Sounds/Mech_0");
			this.SoundGrab = this.Load("Sounds/Grab");
			this.SoundPixie = this.Load("Sounds/Pixie");
			this.SoundDig[0] = this.Load("Sounds/Dig_0");
			this.SoundDig[1] = this.Load("Sounds/Dig_1");
			this.SoundDig[2] = this.Load("Sounds/Dig_2");
			this.SoundThunder[0] = this.Load("Sounds/Thunder_0");
			this.SoundThunder[1] = this.Load("Sounds/Thunder_1");
			this.SoundThunder[2] = this.Load("Sounds/Thunder_2");
			this.SoundThunder[3] = this.Load("Sounds/Thunder_3");
			this.SoundThunder[4] = this.Load("Sounds/Thunder_4");
			this.SoundThunder[5] = this.Load("Sounds/Thunder_5");
			this.SoundThunder[6] = this.Load("Sounds/Thunder_6");
			this.SoundResearch[0] = this.Load("Sounds/Research_0");
			this.SoundResearch[1] = this.Load("Sounds/Research_1");
			this.SoundResearch[2] = this.Load("Sounds/Research_2");
			this.SoundResearch[3] = this.Load("Sounds/Research_3");
			this.SoundTink[0] = this.Load("Sounds/Tink_0");
			this.SoundTink[1] = this.Load("Sounds/Tink_1");
			this.SoundTink[2] = this.Load("Sounds/Tink_2");
			this.SoundPlayerHit[0] = this.Load("Sounds/Player_Hit_0");
			this.SoundPlayerHit[1] = this.Load("Sounds/Player_Hit_1");
			this.SoundPlayerHit[2] = this.Load("Sounds/Player_Hit_2");
			this.SoundFemaleHit[0] = this.Load("Sounds/Female_Hit_0");
			this.SoundFemaleHit[1] = this.Load("Sounds/Female_Hit_1");
			this.SoundFemaleHit[2] = this.Load("Sounds/Female_Hit_2");
			this.SoundPlayerKilled = this.Load("Sounds/Player_Killed");
			this.SoundChat = this.Load("Sounds/Chat");
			this.SoundGrass = this.Load("Sounds/Grass");
			this.SoundDoorOpen = this.Load("Sounds/Door_Opened");
			this.SoundDoorClosed = this.Load("Sounds/Door_Closed");
			this.SoundMenuTick = this.Load("Sounds/Menu_Tick");
			this.SoundMenuOpen = this.Load("Sounds/Menu_Open");
			this.SoundMenuClose = this.Load("Sounds/Menu_Close");
			this.SoundShatter = this.Load("Sounds/Shatter");
			this.SoundCamera = this.Load("Sounds/Camera");
			for (int i = 0; i < this.SoundCoin.Length; i++)
			{
				this.SoundCoin[i] = this.Load("Sounds/Coin_" + i);
			}
			for (int j = 0; j < this.SoundDrip.Length; j++)
			{
				this.SoundDrip[j] = this.Load("Sounds/Drip_" + j);
			}
			for (int k = 0; k < this.SoundZombie.Length; k++)
			{
				this.SoundZombie[k] = this.Load("Sounds/Zombie_" + k);
			}
			for (int l = 0; l < this.SoundLiquid.Length; l++)
			{
				this.SoundLiquid[l] = this.Load("Sounds/Liquid_" + l);
			}
			for (int m = 0; m < this.SoundRoar.Length; m++)
			{
				this.SoundRoar[m] = this.Load("Sounds/Roar_" + m);
			}
			for (int n = 0; n < this.SoundSplash.Length; n++)
			{
				this.SoundSplash[n] = this.Load("Sounds/Splash_" + n);
			}
			this.SoundDoubleJump = this.Load("Sounds/Double_Jump");
			this.SoundRun = this.Load("Sounds/Run");
			this.SoundCoins = this.Load("Sounds/Coins");
			this.SoundUnlock = this.Load("Sounds/Unlock");
			this.SoundMaxMana = this.Load("Sounds/MaxMana");
			this.SoundDrown = this.Load("Sounds/Drown");
			for (int num = 1; num < this.SoundItem.Length; num++)
			{
				this.SoundItem[num] = this.Load("Sounds/Item_" + num);
			}
			for (int num2 = 1; num2 < this.SoundNpcHit.Length; num2++)
			{
				this.SoundNpcHit[num2] = this.Load("Sounds/NPC_Hit_" + num2);
			}
			for (int num3 = 1; num3 < this.SoundNpcKilled.Length; num3++)
			{
				this.SoundNpcKilled[num3] = this.Load("Sounds/NPC_Killed_" + num3);
			}
			this.TrackableSounds = new Asset<SoundEffect>[SoundID.TrackableLegacySoundCount];
			this.TrackableSoundInstances = new SoundEffectInstance[this.TrackableSounds.Length];
			for (int num4 = 0; num4 < this.TrackableSounds.Length; num4++)
			{
				this.TrackableSounds[num4] = this.Load("Sounds/Custom" + Path.DirectorySeparatorChar.ToString() + SoundID.GetTrackableLegacySoundPath(num4));
			}
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x005C02E8 File Offset: 0x005BE4E8
		public void CreateAllSoundInstances()
		{
			foreach (SoundEffectInstance soundEffectInstance in this._trackedInstances)
			{
				soundEffectInstance.Dispose();
			}
			this._trackedInstances.Clear();
			this.SoundInstanceMech[0] = this.CreateInstance(this.SoundMech[0]);
			this.SoundInstanceGrab = this.CreateInstance(this.SoundGrab);
			this.SoundInstancePixie = this.CreateInstance(this.SoundGrab);
			this.SoundInstanceDig[0] = this.CreateInstance(this.SoundDig[0]);
			this.SoundInstanceDig[1] = this.CreateInstance(this.SoundDig[1]);
			this.SoundInstanceDig[2] = this.CreateInstance(this.SoundDig[2]);
			this.SoundInstanceTink[0] = this.CreateInstance(this.SoundTink[0]);
			this.SoundInstanceTink[1] = this.CreateInstance(this.SoundTink[1]);
			this.SoundInstanceTink[2] = this.CreateInstance(this.SoundTink[2]);
			this.SoundInstancePlayerHit[0] = this.CreateInstance(this.SoundPlayerHit[0]);
			this.SoundInstancePlayerHit[1] = this.CreateInstance(this.SoundPlayerHit[1]);
			this.SoundInstancePlayerHit[2] = this.CreateInstance(this.SoundPlayerHit[2]);
			this.SoundInstanceFemaleHit[0] = this.CreateInstance(this.SoundFemaleHit[0]);
			this.SoundInstanceFemaleHit[1] = this.CreateInstance(this.SoundFemaleHit[1]);
			this.SoundInstanceFemaleHit[2] = this.CreateInstance(this.SoundFemaleHit[2]);
			this.SoundInstancePlayerKilled = this.CreateInstance(this.SoundPlayerKilled);
			this.SoundInstanceChat = this.CreateInstance(this.SoundChat);
			this.SoundInstanceGrass = this.CreateInstance(this.SoundGrass);
			this.SoundInstanceDoorOpen = this.CreateInstance(this.SoundDoorOpen);
			this.SoundInstanceDoorClosed = this.CreateInstance(this.SoundDoorClosed);
			this.SoundInstanceMenuTick = this.CreateInstance(this.SoundMenuTick);
			this.SoundInstanceMenuOpen = this.CreateInstance(this.SoundMenuOpen);
			this.SoundInstanceMenuClose = this.CreateInstance(this.SoundMenuClose);
			this.SoundInstanceShatter = this.CreateInstance(this.SoundShatter);
			this.SoundInstanceCamera = this.CreateInstance(this.SoundCamera);
			this.SoundInstanceSplash[0] = this.CreateInstance(this.SoundRoar[0]);
			this.SoundInstanceSplash[1] = this.CreateInstance(this.SoundSplash[1]);
			this.SoundInstanceDoubleJump = this.CreateInstance(this.SoundRoar[0]);
			this.SoundInstanceRun = this.CreateInstance(this.SoundRun);
			this.SoundInstanceCoins = this.CreateInstance(this.SoundCoins);
			this.SoundInstanceUnlock = this.CreateInstance(this.SoundUnlock);
			this.SoundInstanceMaxMana = this.CreateInstance(this.SoundMaxMana);
			this.SoundInstanceDrown = this.CreateInstance(this.SoundDrown);
			this.SoundInstanceMoonlordCry = this.CreateInstance(this.SoundNpcKilled[10]);
			for (int i = 0; i < this.SoundThunder.Length; i++)
			{
				this.SoundInstanceThunder[i] = this.CreateInstance(this.SoundThunder[i]);
			}
			for (int j = 0; j < this.SoundResearch.Length; j++)
			{
				this.SoundInstanceResearch[j] = this.CreateInstance(this.SoundResearch[j]);
			}
			for (int k = 0; k < this.SoundCoin.Length; k++)
			{
				this.SoundInstanceCoin[k] = this.CreateInstance(this.SoundCoin[k]);
			}
			for (int l = 0; l < this.SoundDrip.Length; l++)
			{
				this.SoundInstanceDrip[l] = this.CreateInstance(this.SoundDrip[l]);
			}
			for (int m = 0; m < this.SoundZombie.Length; m++)
			{
				this.SoundInstanceZombie[m] = this.CreateInstance(this.SoundZombie[m]);
			}
			for (int n = 0; n < this.SoundLiquid.Length; n++)
			{
				this.SoundInstanceLiquid[n] = this.CreateInstance(this.SoundLiquid[n]);
			}
			for (int num = 0; num < this.SoundRoar.Length; num++)
			{
				this.SoundInstanceRoar[num] = this.CreateInstance(this.SoundRoar[num]);
			}
			for (int num2 = 1; num2 < this.SoundItem.Length; num2++)
			{
				this.SoundInstanceItem[num2] = this.CreateInstance(this.SoundItem[num2]);
			}
			for (int num3 = 1; num3 < this.SoundNpcHit.Length; num3++)
			{
				this.SoundInstanceNpcHit[num3] = this.CreateInstance(this.SoundNpcHit[num3]);
			}
			for (int num4 = 1; num4 < this.SoundNpcKilled.Length; num4++)
			{
				this.SoundInstanceNpcKilled[num4] = this.CreateInstance(this.SoundNpcKilled[num4]);
			}
			for (int num5 = 0; num5 < this.TrackableSounds.Length; num5++)
			{
				this.TrackableSoundInstances[num5] = this.CreateInstance(this.TrackableSounds[num5]);
			}
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x005C07DC File Offset: 0x005BE9DC
		private SoundEffectInstance CreateInstance(Asset<SoundEffect> asset)
		{
			SoundEffectInstance soundEffectInstance = asset.Value.CreateInstance();
			this._trackedInstances.Add(soundEffectInstance);
			return soundEffectInstance;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x005C0802 File Offset: 0x005BEA02
		private Asset<SoundEffect> Load(string assetName)
		{
			return XnaExtensions.Get<IAssetRepository>(this._services).Request<SoundEffect>(assetName, 2);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x005C0818 File Offset: 0x005BEA18
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
					Vector2 vector = new Vector2(Main.screenPosition.X + (float)Main.screenWidth * 0.5f, Main.screenPosition.Y + (float)Main.screenHeight * 0.5f);
					float num4 = Math.Abs((float)x - vector.X);
					float num5 = Math.Abs((float)y - vector.Y);
					float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
					int num7 = 2500;
					if (num6 < (float)num7)
					{
						flag = true;
						if (type == 43)
						{
							num3 = ((float)x - vector.X) / 900f;
						}
						else
						{
							num3 = ((float)x - vector.X) / ((float)Main.screenWidth * 0.5f);
						}
						num2 = 1f - num6 / (float)num7;
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
						num2 *= Main.ambientVolume * (float)(Main.gameInactive ? 0 : 1);
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
					if (type == 0)
					{
						int num8 = Main.rand.Next(3);
						if (this.SoundInstanceDig[num8] != null)
						{
							this.SoundInstanceDig[num8].Stop();
						}
						this.SoundInstanceDig[num8] = this.SoundDig[num8].Value.CreateInstance();
						this.SoundInstanceDig[num8].Volume = num2;
						this.SoundInstanceDig[num8].Pan = num3;
						this.SoundInstanceDig[num8].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceDig[num8];
					}
					else if (type == 43)
					{
						int num9 = Main.rand.Next(this.SoundThunder.Length);
						int num10 = 0;
						while (num10 < this.SoundThunder.Length && this.SoundInstanceThunder[num9] != null && this.SoundInstanceThunder[num9].State == SoundState.Playing)
						{
							num9 = Main.rand.Next(this.SoundThunder.Length);
							num10++;
						}
						if (this.SoundInstanceThunder[num9] != null)
						{
							this.SoundInstanceThunder[num9].Stop();
						}
						this.SoundInstanceThunder[num9] = this.SoundThunder[num9].Value.CreateInstance();
						this.SoundInstanceThunder[num9].Volume = num2;
						this.SoundInstanceThunder[num9].Pan = num3;
						this.SoundInstanceThunder[num9].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceThunder[num9];
					}
					else if (type == 63)
					{
						int num11 = Main.rand.Next(1, 4);
						if (this.SoundInstanceResearch[num11] != null)
						{
							this.SoundInstanceResearch[num11].Stop();
						}
						this.SoundInstanceResearch[num11] = this.SoundResearch[num11].Value.CreateInstance();
						this.SoundInstanceResearch[num11].Volume = num2;
						this.SoundInstanceResearch[num11].Pan = num3;
						soundEffectInstance = this.SoundInstanceResearch[num11];
					}
					else if (type == 64)
					{
						if (this.SoundInstanceResearch[0] != null)
						{
							this.SoundInstanceResearch[0].Stop();
						}
						this.SoundInstanceResearch[0] = this.SoundResearch[0].Value.CreateInstance();
						this.SoundInstanceResearch[0].Volume = num2;
						this.SoundInstanceResearch[0].Pan = num3;
						soundEffectInstance = this.SoundInstanceResearch[0];
					}
					else if (type == 1)
					{
						int num12 = Main.rand.Next(3);
						if (this.SoundInstancePlayerHit[num12] != null)
						{
							this.SoundInstancePlayerHit[num12].Stop();
						}
						this.SoundInstancePlayerHit[num12] = this.SoundPlayerHit[num12].Value.CreateInstance();
						this.SoundInstancePlayerHit[num12].Volume = num2;
						this.SoundInstancePlayerHit[num12].Pan = num3;
						soundEffectInstance = this.SoundInstancePlayerHit[num12];
					}
					else if (type == 2)
					{
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
						if (num == 1)
						{
							int num13 = Main.rand.Next(3);
							if (num13 == 1)
							{
								num = 18;
							}
							if (num13 == 2)
							{
								num = 19;
							}
						}
						else if (num == 55 || num == 53)
						{
							num2 *= 0.75f;
							if (num == 55)
							{
								num2 *= 0.75f;
							}
							if (this.SoundInstanceItem[num] != null && this.SoundInstanceItem[num].State == SoundState.Playing)
							{
								return null;
							}
						}
						else if (num == 37)
						{
							num2 *= 0.5f;
						}
						else if (num == 52)
						{
							num2 *= 0.35f;
						}
						else if (num == 157)
						{
							num2 *= 0.7f;
						}
						else if (num == 158)
						{
							num2 *= 0.8f;
						}
						if (num == 159)
						{
							if (this.SoundInstanceItem[num] != null && this.SoundInstanceItem[num].State == SoundState.Playing)
							{
								return null;
							}
							num2 *= 0.75f;
						}
						else if (num != 9 && num != 10 && num != 24 && num != 26 && num != 34 && num != 43 && num != 103 && num != 156 && num != 162 && this.SoundInstanceItem[num] != null)
						{
							this.SoundInstanceItem[num].Stop();
						}
						this.SoundInstanceItem[num] = this.SoundItem[num].Value.CreateInstance();
						this.SoundInstanceItem[num].Volume = num2;
						this.SoundInstanceItem[num].Pan = num3;
						if (num == 53)
						{
							this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-20, -11) * 0.02f;
						}
						else if (num == 55)
						{
							this.SoundInstanceItem[num].Pitch = (float)(-(float)Main.rand.Next(-20, -11)) * 0.02f;
						}
						else if (num == 132)
						{
							this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-20, 21) * 0.001f;
						}
						else if (num == 153)
						{
							this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-50, 51) * 0.003f;
						}
						else if (num == 156)
						{
							this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-50, 51) * 0.002f;
							this.SoundInstanceItem[num].Volume *= 0.6f;
						}
						else
						{
							this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-6, 7) * 0.01f;
						}
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
					}
					else if (type == 3)
					{
						if (num >= 20 && num <= 54)
						{
							num2 *= 0.5f;
						}
						if (num == 57 && this.SoundInstanceNpcHit[num] != null && this.SoundInstanceNpcHit[num].State == SoundState.Playing)
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
					}
					else if (type == 4)
					{
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
						if (num == 10 && this.SoundInstanceNpcKilled[num] != null && this.SoundInstanceNpcKilled[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceNpcKilled[num] = this.SoundNpcKilled[num].Value.CreateInstance();
						this.SoundInstanceNpcKilled[num].Volume = num2;
						this.SoundInstanceNpcKilled[num].Pan = num3;
						this.SoundInstanceNpcKilled[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceNpcKilled[num];
					}
					else if (type == 5)
					{
						if (this.SoundInstancePlayerKilled != null)
						{
							this.SoundInstancePlayerKilled.Stop();
						}
						this.SoundInstancePlayerKilled = this.SoundPlayerKilled.Value.CreateInstance();
						this.SoundInstancePlayerKilled.Volume = num2;
						this.SoundInstancePlayerKilled.Pan = num3;
						soundEffectInstance = this.SoundInstancePlayerKilled;
					}
					else if (type == 6)
					{
						if (this.SoundInstanceGrass != null)
						{
							this.SoundInstanceGrass.Stop();
						}
						this.SoundInstanceGrass = this.SoundGrass.Value.CreateInstance();
						this.SoundInstanceGrass.Volume = num2;
						this.SoundInstanceGrass.Pan = num3;
						this.SoundInstanceGrass.Pitch = (float)Main.rand.Next(-30, 31) * 0.01f;
						soundEffectInstance = this.SoundInstanceGrass;
					}
					else if (type == 7)
					{
						if (this.SoundInstanceGrab != null)
						{
							this.SoundInstanceGrab.Stop();
						}
						this.SoundInstanceGrab = this.SoundGrab.Value.CreateInstance();
						this.SoundInstanceGrab.Volume = num2;
						this.SoundInstanceGrab.Pan = num3;
						this.SoundInstanceGrab.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceGrab;
					}
					else if (type == 8)
					{
						if (this.SoundInstanceDoorOpen != null)
						{
							this.SoundInstanceDoorOpen.Stop();
						}
						this.SoundInstanceDoorOpen = this.SoundDoorOpen.Value.CreateInstance();
						this.SoundInstanceDoorOpen.Volume = num2;
						this.SoundInstanceDoorOpen.Pan = num3;
						this.SoundInstanceDoorOpen.Pitch = (float)Main.rand.Next(-20, 21) * 0.01f;
						soundEffectInstance = this.SoundInstanceDoorOpen;
					}
					else if (type == 9)
					{
						if (this.SoundInstanceDoorClosed != null)
						{
							this.SoundInstanceDoorClosed.Stop();
						}
						this.SoundInstanceDoorClosed = this.SoundDoorClosed.Value.CreateInstance();
						this.SoundInstanceDoorClosed.Volume = num2;
						this.SoundInstanceDoorClosed.Pan = num3;
						this.SoundInstanceDoorClosed.Pitch = (float)Main.rand.Next(-20, 21) * 0.01f;
						soundEffectInstance = this.SoundInstanceDoorClosed;
					}
					else if (type == 10)
					{
						if (this.SoundInstanceMenuOpen != null)
						{
							this.SoundInstanceMenuOpen.Stop();
						}
						this.SoundInstanceMenuOpen = this.SoundMenuOpen.Value.CreateInstance();
						this.SoundInstanceMenuOpen.Volume = num2;
						this.SoundInstanceMenuOpen.Pan = num3;
						soundEffectInstance = this.SoundInstanceMenuOpen;
					}
					else if (type == 11)
					{
						if (this.SoundInstanceMenuClose != null)
						{
							this.SoundInstanceMenuClose.Stop();
						}
						this.SoundInstanceMenuClose = this.SoundMenuClose.Value.CreateInstance();
						this.SoundInstanceMenuClose.Volume = num2;
						this.SoundInstanceMenuClose.Pan = num3;
						soundEffectInstance = this.SoundInstanceMenuClose;
					}
					else if (type == 12)
					{
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
						}
					}
					else if (type == 13)
					{
						if (this.SoundInstanceShatter != null)
						{
							this.SoundInstanceShatter.Stop();
						}
						this.SoundInstanceShatter = this.SoundShatter.Value.CreateInstance();
						this.SoundInstanceShatter.Volume = num2;
						this.SoundInstanceShatter.Pan = num3;
						soundEffectInstance = this.SoundInstanceShatter;
					}
					else if (type == 14)
					{
						if (Style == 542)
						{
							int num14 = 7;
							this.SoundInstanceZombie[num14] = this.SoundZombie[num14].Value.CreateInstance();
							this.SoundInstanceZombie[num14].Volume = num2 * 0.4f;
							this.SoundInstanceZombie[num14].Pan = num3;
							soundEffectInstance = this.SoundInstanceZombie[num14];
						}
						else if (Style == 489 || Style == 586)
						{
							int num15 = Main.rand.Next(21, 24);
							this.SoundInstanceZombie[num15] = this.SoundZombie[num15].Value.CreateInstance();
							this.SoundInstanceZombie[num15].Volume = num2 * 0.4f;
							this.SoundInstanceZombie[num15].Pan = num3;
							soundEffectInstance = this.SoundInstanceZombie[num15];
						}
						else
						{
							int num16 = Main.rand.Next(3);
							this.SoundInstanceZombie[num16] = this.SoundZombie[num16].Value.CreateInstance();
							this.SoundInstanceZombie[num16].Volume = num2 * 0.4f;
							this.SoundInstanceZombie[num16].Pan = num3;
							soundEffectInstance = this.SoundInstanceZombie[num16];
						}
					}
					else if (type == 15)
					{
						float num17 = 1f;
						if (num == 4)
						{
							num = 1;
							num17 = 0.25f;
						}
						if (this.SoundInstanceRoar[num] == null || this.SoundInstanceRoar[num].State == SoundState.Stopped)
						{
							this.SoundInstanceRoar[num] = this.SoundRoar[num].Value.CreateInstance();
							this.SoundInstanceRoar[num].Volume = num2 * num17;
							this.SoundInstanceRoar[num].Pan = num3;
							soundEffectInstance = this.SoundInstanceRoar[num];
						}
					}
					else if (type == 16)
					{
						if (this.SoundInstanceDoubleJump != null)
						{
							this.SoundInstanceDoubleJump.Stop();
						}
						this.SoundInstanceDoubleJump = this.SoundDoubleJump.Value.CreateInstance();
						this.SoundInstanceDoubleJump.Volume = num2;
						this.SoundInstanceDoubleJump.Pan = num3;
						this.SoundInstanceDoubleJump.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceDoubleJump;
					}
					else if (type == 17)
					{
						if (this.SoundInstanceRun != null)
						{
							this.SoundInstanceRun.Stop();
						}
						this.SoundInstanceRun = this.SoundRun.Value.CreateInstance();
						this.SoundInstanceRun.Volume = num2;
						this.SoundInstanceRun.Pan = num3;
						this.SoundInstanceRun.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceRun;
					}
					else if (type == 18)
					{
						this.SoundInstanceCoins = this.SoundCoins.Value.CreateInstance();
						this.SoundInstanceCoins.Volume = num2;
						this.SoundInstanceCoins.Pan = num3;
						soundEffectInstance = this.SoundInstanceCoins;
					}
					else if (type == 19)
					{
						if (this.SoundInstanceSplash[num] == null || this.SoundInstanceSplash[num].State == SoundState.Stopped)
						{
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
							if (num == 4)
							{
								if (this.SoundInstanceSplash[5] == null || this.SoundInstanceSplash[5].State == SoundState.Stopped)
								{
									soundEffectInstance = this.SoundInstanceSplash[num];
								}
							}
							else if (num == 5)
							{
								if (this.SoundInstanceSplash[4] == null || this.SoundInstanceSplash[4].State == SoundState.Stopped)
								{
									soundEffectInstance = this.SoundInstanceSplash[num];
								}
							}
							else
							{
								soundEffectInstance = this.SoundInstanceSplash[num];
							}
						}
					}
					else if (type == 20)
					{
						int num18 = Main.rand.Next(3);
						if (this.SoundInstanceFemaleHit[num18] != null)
						{
							this.SoundInstanceFemaleHit[num18].Stop();
						}
						this.SoundInstanceFemaleHit[num18] = this.SoundFemaleHit[num18].Value.CreateInstance();
						this.SoundInstanceFemaleHit[num18].Volume = num2;
						this.SoundInstanceFemaleHit[num18].Pan = num3;
						soundEffectInstance = this.SoundInstanceFemaleHit[num18];
					}
					else if (type == 21)
					{
						int num19 = Main.rand.Next(3);
						if (this.SoundInstanceTink[num19] != null)
						{
							this.SoundInstanceTink[num19].Stop();
						}
						this.SoundInstanceTink[num19] = this.SoundTink[num19].Value.CreateInstance();
						this.SoundInstanceTink[num19].Volume = num2;
						this.SoundInstanceTink[num19].Pan = num3;
						soundEffectInstance = this.SoundInstanceTink[num19];
					}
					else if (type == 22)
					{
						if (this.SoundInstanceUnlock != null)
						{
							this.SoundInstanceUnlock.Stop();
						}
						this.SoundInstanceUnlock = this.SoundUnlock.Value.CreateInstance();
						this.SoundInstanceUnlock.Volume = num2;
						this.SoundInstanceUnlock.Pan = num3;
						soundEffectInstance = this.SoundInstanceUnlock;
					}
					else if (type == 23)
					{
						if (this.SoundInstanceDrown != null)
						{
							this.SoundInstanceDrown.Stop();
						}
						this.SoundInstanceDrown = this.SoundDrown.Value.CreateInstance();
						this.SoundInstanceDrown.Volume = num2;
						this.SoundInstanceDrown.Pan = num3;
						soundEffectInstance = this.SoundInstanceDrown;
					}
					else if (type == 24)
					{
						this.SoundInstanceChat = this.SoundChat.Value.CreateInstance();
						this.SoundInstanceChat.Volume = num2;
						this.SoundInstanceChat.Pan = num3;
						soundEffectInstance = this.SoundInstanceChat;
					}
					else if (type == 25)
					{
						this.SoundInstanceMaxMana = this.SoundMaxMana.Value.CreateInstance();
						this.SoundInstanceMaxMana.Volume = num2;
						this.SoundInstanceMaxMana.Pan = num3;
						soundEffectInstance = this.SoundInstanceMaxMana;
					}
					else if (type == 26)
					{
						int num20 = Main.rand.Next(3, 5);
						this.SoundInstanceZombie[num20] = this.SoundZombie[num20].Value.CreateInstance();
						this.SoundInstanceZombie[num20].Volume = num2 * 0.9f;
						this.SoundInstanceZombie[num20].Pan = num3;
						this.SoundInstanceZombie[num20].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num20];
					}
					else if (type == 27)
					{
						if (this.SoundInstancePixie != null && this.SoundInstancePixie.State == SoundState.Playing)
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
					}
					else if (type == 28)
					{
						if (this.SoundInstanceMech[num] != null && this.SoundInstanceMech[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceMech[num] = this.SoundMech[num].Value.CreateInstance();
						this.SoundInstanceMech[num].Volume = num2;
						this.SoundInstanceMech[num].Pan = num3;
						this.SoundInstanceMech[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceMech[num];
					}
					else if (type == 29)
					{
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
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 44)
					{
						num = Main.rand.Next(106, 109);
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 1) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 45)
					{
						num = 109;
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.3f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 46)
					{
						if (this.SoundInstanceZombie[110] != null && this.SoundInstanceZombie[110].State == SoundState.Playing)
						{
							return null;
						}
						if (this.SoundInstanceZombie[111] != null && this.SoundInstanceZombie[111].State == SoundState.Playing)
						{
							return null;
						}
						num = Main.rand.Next(110, 112);
						if (Main.rand.Next(300) == 0)
						{
							if (Main.rand.Next(3) == 0)
							{
								num = 114;
							}
							else if (Main.rand.Next(2) == 0)
							{
								num = 113;
							}
							else
							{
								num = 112;
							}
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.9f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 45)
					{
						num = 109;
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 1) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 30)
					{
						num = Main.rand.Next(10, 12);
						if (Main.rand.Next(300) == 0)
						{
							num = 12;
							if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
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
					}
					else if (type == 31)
					{
						num = 13;
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.35f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-40, 21) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 32)
					{
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.15f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 26) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 67)
					{
						num = Main.rand.Next(118, 121);
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.3f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-5, 6) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 68)
					{
						num = Main.rand.Next(126, 129);
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.22f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-5, 6) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 69)
					{
						num = Main.rand.Next(129, 131);
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-5, 6) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 66)
					{
						num = Main.rand.Next(121, 124);
						if (this.SoundInstanceZombie[121] != null && this.SoundInstanceZombie[121].State == SoundState.Playing)
						{
							return null;
						}
						if (this.SoundInstanceZombie[122] != null && this.SoundInstanceZombie[122].State == SoundState.Playing)
						{
							return null;
						}
						if (this.SoundInstanceZombie[123] != null && this.SoundInstanceZombie[123].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.45f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-15, 16) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 33)
					{
						num = 15;
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 31) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type >= 47 && type <= 52)
					{
						num = 133 + type - 47;
						for (int i = 133; i <= 138; i++)
						{
							if (this.SoundInstanceItem[i] != null && this.SoundInstanceItem[i].State == SoundState.Playing)
							{
								this.SoundInstanceItem[i].Stop();
							}
						}
						this.SoundInstanceItem[num] = this.SoundItem[num].Value.CreateInstance();
						this.SoundInstanceItem[num].Volume = num2 * 0.45f;
						this.SoundInstanceItem[num].Pan = num3;
						soundEffectInstance = this.SoundInstanceItem[num];
					}
					else if (type >= 53 && type <= 62)
					{
						num = 139 + type - 53;
						if (this.SoundInstanceItem[num] != null && this.SoundInstanceItem[num].State == SoundState.Playing)
						{
							this.SoundInstanceItem[num].Stop();
						}
						this.SoundInstanceItem[num] = this.SoundItem[num].Value.CreateInstance();
						this.SoundInstanceItem[num].Volume = num2 * 0.7f;
						this.SoundInstanceItem[num].Pan = num3;
						soundEffectInstance = this.SoundInstanceItem[num];
					}
					else if (type == 34)
					{
						float num21 = (float)num / 50f;
						if (num21 > 1f)
						{
							num21 = 1f;
						}
						num2 *= num21;
						num2 *= 0.2f;
						num2 *= 1f - Main.shimmerAlpha;
						if (num2 <= 0f || x == -1 || y == -1)
						{
							if (this.SoundInstanceLiquid[0] != null && this.SoundInstanceLiquid[0].State == SoundState.Playing)
							{
								this.SoundInstanceLiquid[0].Stop();
							}
						}
						else if (this.SoundInstanceLiquid[0] != null && this.SoundInstanceLiquid[0].State == SoundState.Playing)
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
					}
					else if (type == 35)
					{
						float num22 = (float)num / 50f;
						if (num22 > 1f)
						{
							num22 = 1f;
						}
						num2 *= num22;
						num2 *= 0.65f;
						num2 *= 1f - Main.shimmerAlpha;
						if (num2 <= 0f || x == -1 || y == -1)
						{
							if (this.SoundInstanceLiquid[1] != null && this.SoundInstanceLiquid[1].State == SoundState.Playing)
							{
								this.SoundInstanceLiquid[1].Stop();
							}
						}
						else if (this.SoundInstanceLiquid[1] != null && this.SoundInstanceLiquid[1].State == SoundState.Playing)
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
					}
					else if (type == 36)
					{
						int num23 = Style;
						if (Style == -1)
						{
							num23 = 0;
						}
						this.SoundInstanceRoar[num23] = this.SoundRoar[num23].Value.CreateInstance();
						this.SoundInstanceRoar[num23].Volume = num2;
						this.SoundInstanceRoar[num23].Pan = num3;
						if (Style == -1)
						{
							this.SoundInstanceRoar[num23].Pitch += 0.6f;
						}
						soundEffectInstance = this.SoundInstanceRoar[num23];
					}
					else if (type == 37)
					{
						int num24 = Main.rand.Next(57, 59);
						if (Main.starGame)
						{
							num2 *= 0.15f;
						}
						else
						{
							num2 *= (float)Style * 0.05f;
						}
						this.SoundInstanceItem[num24] = this.SoundItem[num24].Value.CreateInstance();
						this.SoundInstanceItem[num24].Volume = num2;
						this.SoundInstanceItem[num24].Pan = num3;
						this.SoundInstanceItem[num24].Pitch = (float)Main.rand.Next(-40, 41) * 0.01f;
						soundEffectInstance = this.SoundInstanceItem[num24];
					}
					else if (type == 38)
					{
						if (Main.starGame)
						{
							num2 *= 0.15f;
						}
						int num25 = Main.rand.Next(5);
						this.SoundInstanceCoin[num25] = this.SoundCoin[num25].Value.CreateInstance();
						this.SoundInstanceCoin[num25].Volume = num2;
						this.SoundInstanceCoin[num25].Pan = num3;
						this.SoundInstanceCoin[num25].Pitch = (float)Main.rand.Next(-40, 41) * 0.002f;
						soundEffectInstance = this.SoundInstanceCoin[num25];
					}
					else if (type == 39)
					{
						this.SoundInstanceDrip[Style] = this.SoundDrip[Style].Value.CreateInstance();
						this.SoundInstanceDrip[Style].Volume = num2 * 0.5f;
						this.SoundInstanceDrip[Style].Pan = num3;
						this.SoundInstanceDrip[Style].Pitch = (float)Main.rand.Next(-30, 31) * 0.01f;
						soundEffectInstance = this.SoundInstanceDrip[Style];
					}
					else if (type == 40)
					{
						if (this.SoundInstanceCamera != null)
						{
							this.SoundInstanceCamera.Stop();
						}
						this.SoundInstanceCamera = this.SoundCamera.Value.CreateInstance();
						this.SoundInstanceCamera.Volume = num2;
						this.SoundInstanceCamera.Pan = num3;
						soundEffectInstance = this.SoundInstanceCamera;
					}
					else if (type == 41)
					{
						this.SoundInstanceMoonlordCry = this.SoundNpcKilled[10].Value.CreateInstance();
						this.SoundInstanceMoonlordCry.Volume = 1f / (1f + (new Vector2((float)x, (float)y) - Main.player[Main.myPlayer].position).Length());
						this.SoundInstanceMoonlordCry.Pan = num3;
						this.SoundInstanceMoonlordCry.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceMoonlordCry;
					}
					else if (type == 42)
					{
						soundEffectInstance = this.TrackableSounds[num].Value.CreateInstance();
						soundEffectInstance.Volume = num2;
						soundEffectInstance.Pan = num3;
						this.TrackableSoundInstances[num] = soundEffectInstance;
					}
					else if (type == 65)
					{
						if (this.SoundInstanceZombie[115] != null && this.SoundInstanceZombie[115].State == SoundState.Playing)
						{
							return null;
						}
						if (this.SoundInstanceZombie[116] != null && this.SoundInstanceZombie[116].State == SoundState.Playing)
						{
							return null;
						}
						if (this.SoundInstanceZombie[117] != null && this.SoundInstanceZombie[117].State == SoundState.Playing)
						{
							return null;
						}
						int num26 = Main.rand.Next(115, 118);
						this.SoundInstanceZombie[num26] = this.SoundZombie[num26].Value.CreateInstance();
						this.SoundInstanceZombie[num26].Volume = num2 * 0.5f;
						this.SoundInstanceZombie[num26].Pan = num3;
						soundEffectInstance = this.SoundInstanceZombie[num26];
					}
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

		// Token: 0x06002E04 RID: 11780 RVA: 0x005C2DB4 File Offset: 0x005C0FB4
		public SoundEffect GetTrackableSoundByStyleId(int id)
		{
			return this.TrackableSounds[id].Value;
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x005C2DC4 File Offset: 0x005C0FC4
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

		// Token: 0x06002E06 RID: 11782 RVA: 0x005C2DFC File Offset: 0x005C0FFC
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

		// Token: 0x04005165 RID: 20837
		public Asset<SoundEffect>[] SoundDrip = new Asset<SoundEffect>[3];

		// Token: 0x04005166 RID: 20838
		public SoundEffectInstance[] SoundInstanceDrip = new SoundEffectInstance[3];

		// Token: 0x04005167 RID: 20839
		public Asset<SoundEffect>[] SoundLiquid = new Asset<SoundEffect>[2];

		// Token: 0x04005168 RID: 20840
		public SoundEffectInstance[] SoundInstanceLiquid = new SoundEffectInstance[2];

		// Token: 0x04005169 RID: 20841
		public Asset<SoundEffect>[] SoundMech = new Asset<SoundEffect>[1];

		// Token: 0x0400516A RID: 20842
		public SoundEffectInstance[] SoundInstanceMech = new SoundEffectInstance[1];

		// Token: 0x0400516B RID: 20843
		public Asset<SoundEffect>[] SoundDig = new Asset<SoundEffect>[3];

		// Token: 0x0400516C RID: 20844
		public SoundEffectInstance[] SoundInstanceDig = new SoundEffectInstance[3];

		// Token: 0x0400516D RID: 20845
		public Asset<SoundEffect>[] SoundThunder = new Asset<SoundEffect>[7];

		// Token: 0x0400516E RID: 20846
		public SoundEffectInstance[] SoundInstanceThunder = new SoundEffectInstance[7];

		// Token: 0x0400516F RID: 20847
		public Asset<SoundEffect>[] SoundResearch = new Asset<SoundEffect>[4];

		// Token: 0x04005170 RID: 20848
		public SoundEffectInstance[] SoundInstanceResearch = new SoundEffectInstance[4];

		// Token: 0x04005171 RID: 20849
		public Asset<SoundEffect>[] SoundTink = new Asset<SoundEffect>[3];

		// Token: 0x04005172 RID: 20850
		public SoundEffectInstance[] SoundInstanceTink = new SoundEffectInstance[3];

		// Token: 0x04005173 RID: 20851
		public Asset<SoundEffect>[] SoundCoin = new Asset<SoundEffect>[5];

		// Token: 0x04005174 RID: 20852
		public SoundEffectInstance[] SoundInstanceCoin = new SoundEffectInstance[5];

		// Token: 0x04005175 RID: 20853
		public Asset<SoundEffect>[] SoundPlayerHit = new Asset<SoundEffect>[3];

		// Token: 0x04005176 RID: 20854
		public SoundEffectInstance[] SoundInstancePlayerHit = new SoundEffectInstance[3];

		// Token: 0x04005177 RID: 20855
		public Asset<SoundEffect>[] SoundFemaleHit = new Asset<SoundEffect>[3];

		// Token: 0x04005178 RID: 20856
		public SoundEffectInstance[] SoundInstanceFemaleHit = new SoundEffectInstance[3];

		// Token: 0x04005179 RID: 20857
		public Asset<SoundEffect> SoundPlayerKilled;

		// Token: 0x0400517A RID: 20858
		public SoundEffectInstance SoundInstancePlayerKilled;

		// Token: 0x0400517B RID: 20859
		public Asset<SoundEffect> SoundGrass;

		// Token: 0x0400517C RID: 20860
		public SoundEffectInstance SoundInstanceGrass;

		// Token: 0x0400517D RID: 20861
		public Asset<SoundEffect> SoundGrab;

		// Token: 0x0400517E RID: 20862
		public SoundEffectInstance SoundInstanceGrab;

		// Token: 0x0400517F RID: 20863
		public Asset<SoundEffect> SoundPixie;

		// Token: 0x04005180 RID: 20864
		public SoundEffectInstance SoundInstancePixie;

		// Token: 0x04005181 RID: 20865
		public Asset<SoundEffect>[] SoundItem = new Asset<SoundEffect>[(int)SoundID.ItemSoundCount];

		// Token: 0x04005182 RID: 20866
		public SoundEffectInstance[] SoundInstanceItem = new SoundEffectInstance[(int)SoundID.ItemSoundCount];

		// Token: 0x04005183 RID: 20867
		public Asset<SoundEffect>[] SoundNpcHit = new Asset<SoundEffect>[58];

		// Token: 0x04005184 RID: 20868
		public SoundEffectInstance[] SoundInstanceNpcHit = new SoundEffectInstance[58];

		// Token: 0x04005185 RID: 20869
		public Asset<SoundEffect>[] SoundNpcKilled = new Asset<SoundEffect>[(int)SoundID.NPCDeathCount];

		// Token: 0x04005186 RID: 20870
		public SoundEffectInstance[] SoundInstanceNpcKilled = new SoundEffectInstance[(int)SoundID.NPCDeathCount];

		// Token: 0x04005187 RID: 20871
		public SoundEffectInstance SoundInstanceMoonlordCry;

		// Token: 0x04005188 RID: 20872
		public Asset<SoundEffect> SoundDoorOpen;

		// Token: 0x04005189 RID: 20873
		public SoundEffectInstance SoundInstanceDoorOpen;

		// Token: 0x0400518A RID: 20874
		public Asset<SoundEffect> SoundDoorClosed;

		// Token: 0x0400518B RID: 20875
		public SoundEffectInstance SoundInstanceDoorClosed;

		// Token: 0x0400518C RID: 20876
		public Asset<SoundEffect> SoundMenuOpen;

		// Token: 0x0400518D RID: 20877
		public SoundEffectInstance SoundInstanceMenuOpen;

		// Token: 0x0400518E RID: 20878
		public Asset<SoundEffect> SoundMenuClose;

		// Token: 0x0400518F RID: 20879
		public SoundEffectInstance SoundInstanceMenuClose;

		// Token: 0x04005190 RID: 20880
		public Asset<SoundEffect> SoundMenuTick;

		// Token: 0x04005191 RID: 20881
		public SoundEffectInstance SoundInstanceMenuTick;

		// Token: 0x04005192 RID: 20882
		public Asset<SoundEffect> SoundShatter;

		// Token: 0x04005193 RID: 20883
		public SoundEffectInstance SoundInstanceShatter;

		// Token: 0x04005194 RID: 20884
		public Asset<SoundEffect> SoundCamera;

		// Token: 0x04005195 RID: 20885
		public SoundEffectInstance SoundInstanceCamera;

		// Token: 0x04005196 RID: 20886
		public Asset<SoundEffect>[] SoundZombie = new Asset<SoundEffect>[131];

		// Token: 0x04005197 RID: 20887
		public SoundEffectInstance[] SoundInstanceZombie = new SoundEffectInstance[131];

		// Token: 0x04005198 RID: 20888
		public Asset<SoundEffect>[] SoundRoar = new Asset<SoundEffect>[3];

		// Token: 0x04005199 RID: 20889
		public SoundEffectInstance[] SoundInstanceRoar = new SoundEffectInstance[3];

		// Token: 0x0400519A RID: 20890
		public Asset<SoundEffect>[] SoundSplash = new Asset<SoundEffect>[6];

		// Token: 0x0400519B RID: 20891
		public SoundEffectInstance[] SoundInstanceSplash = new SoundEffectInstance[6];

		// Token: 0x0400519C RID: 20892
		public Asset<SoundEffect> SoundDoubleJump;

		// Token: 0x0400519D RID: 20893
		public SoundEffectInstance SoundInstanceDoubleJump;

		// Token: 0x0400519E RID: 20894
		public Asset<SoundEffect> SoundRun;

		// Token: 0x0400519F RID: 20895
		public SoundEffectInstance SoundInstanceRun;

		// Token: 0x040051A0 RID: 20896
		public Asset<SoundEffect> SoundCoins;

		// Token: 0x040051A1 RID: 20897
		public SoundEffectInstance SoundInstanceCoins;

		// Token: 0x040051A2 RID: 20898
		public Asset<SoundEffect> SoundUnlock;

		// Token: 0x040051A3 RID: 20899
		public SoundEffectInstance SoundInstanceUnlock;

		// Token: 0x040051A4 RID: 20900
		public Asset<SoundEffect> SoundChat;

		// Token: 0x040051A5 RID: 20901
		public SoundEffectInstance SoundInstanceChat;

		// Token: 0x040051A6 RID: 20902
		public Asset<SoundEffect> SoundMaxMana;

		// Token: 0x040051A7 RID: 20903
		public SoundEffectInstance SoundInstanceMaxMana;

		// Token: 0x040051A8 RID: 20904
		public Asset<SoundEffect> SoundDrown;

		// Token: 0x040051A9 RID: 20905
		public SoundEffectInstance SoundInstanceDrown;

		// Token: 0x040051AA RID: 20906
		public Asset<SoundEffect>[] TrackableSounds;

		// Token: 0x040051AB RID: 20907
		public SoundEffectInstance[] TrackableSoundInstances;

		// Token: 0x040051AC RID: 20908
		private readonly IServiceProvider _services;

		// Token: 0x040051AD RID: 20909
		private List<SoundEffectInstance> _trackedInstances;
	}
}
