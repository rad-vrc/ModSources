using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.IO;
using Terraria.ModLoader;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000549 RID: 1353
	public class BigProgressBarSystem
	{
		// Token: 0x0600401F RID: 16415 RVA: 0x005DEB88 File Offset: 0x005DCD88
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Configuration_OnLoad;
			preferences.OnSave += this.Configuration_Save;
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x005DEBB0 File Offset: 0x005DCDB0
		public void Update()
		{
			if (!BossBarLoader.CurrentStyle.PreventUpdate)
			{
				if (this._currentBar == null)
				{
					this.TryFindingNPCToTrack();
				}
				if (this._currentBar != null && !this._currentBar.ValidateAndCollectNecessaryInfo(ref this._info))
				{
					this._currentBar = null;
				}
			}
			BossBarLoader.CurrentStyle.Update(this._currentBar, ref this._info);
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x005DEC10 File Offset: 0x005DCE10
		public void Draw(SpriteBatch spriteBatch)
		{
			if (!BossBarLoader.CurrentStyle.PreventDraw && this._currentBar != null)
			{
				BossBarLoader.drawingInfo = new BigProgressBarInfo?(this._info);
				this._currentBar.Draw(ref this._info, spriteBatch);
			}
			BossBarLoader.CurrentStyle.Draw(spriteBatch, this._currentBar, this._info);
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x005DEC6C File Offset: 0x005DCE6C
		private void TryFindingNPCToTrack()
		{
			Rectangle value;
			value..ctor((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
			value.Inflate(5000, 5000);
			float num = float.PositiveInfinity;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.active && nPC.Hitbox.Intersects(value))
				{
					float num2 = nPC.Distance(Main.LocalPlayer.Center);
					if (num > num2 && this.TryTracking(i))
					{
						num = num2;
					}
				}
			}
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x005DED0C File Offset: 0x005DCF0C
		public bool TryTracking(int npcIndex)
		{
			if (npcIndex < 0 || npcIndex > 200)
			{
				return false;
			}
			NPC nPC = Main.npc[npcIndex];
			if (!nPC.active)
			{
				return false;
			}
			BigProgressBarInfo info = new BigProgressBarInfo
			{
				npcIndexToAimAt = npcIndex
			};
			IBigProgressBar bigProgressBar = this._bossBar;
			IBigProgressBar value;
			if (nPC.BossBar != null)
			{
				bigProgressBar = nPC.BossBar;
			}
			else if (this._bossBarsByNpcNetId.TryGetValue(nPC.netID, out value))
			{
				bigProgressBar = value;
			}
			info.showText = true;
			if (!bigProgressBar.ValidateAndCollectNecessaryInfo(ref info))
			{
				return false;
			}
			this._currentBar = bigProgressBar;
			this._info = info;
			return true;
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x005DED9E File Offset: 0x005DCF9E
		private void Configuration_Save(Preferences obj)
		{
			obj.Put("ShowBossBarHealthText", BigProgressBarSystem.ShowText);
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x005DEDB5 File Offset: 0x005DCFB5
		private void Configuration_OnLoad(Preferences obj)
		{
			BigProgressBarSystem.ShowText = obj.Get<bool>("ShowBossBarHealthText", BigProgressBarSystem.ShowText);
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x005DEDCC File Offset: 0x005DCFCC
		public static void ToggleShowText()
		{
			BigProgressBarSystem.ShowText = !BigProgressBarSystem.ShowText;
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x005DEDDB File Offset: 0x005DCFDB
		public NeverValidProgressBar NeverValid
		{
			get
			{
				return BigProgressBarSystem._neverValid;
			}
		}

		/// <summary>
		/// Gets the special IBigProgressBar associated with this vanilla NPCs netID (usually the type).
		/// <para> Reminder: If no special bar exists or NPC.BossBar is not assigned, any NPC with a boss head index will automatically display a common boss bar shared among all simple bosses.</para>
		/// </summary>
		/// <param name="netID">The NPC netID (usually the type)</param>
		/// <param name="bossBar">When this method returns, contains the IBigProgressBar associated with the specified NPC netID</param>
		/// <returns><see langword="true" /> if IBigProgressBar exists; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004028 RID: 16424 RVA: 0x005DEDE2 File Offset: 0x005DCFE2
		public bool TryGetSpecialVanillaBossBar(int netID, out IBigProgressBar bossBar)
		{
			return this._bossBarsByNpcNetId.TryGetValue(netID, out bossBar);
		}

		// Token: 0x04005847 RID: 22599
		private IBigProgressBar _currentBar;

		// Token: 0x04005848 RID: 22600
		private CommonBossBigProgressBar _bossBar = new CommonBossBigProgressBar();

		// Token: 0x04005849 RID: 22601
		private BigProgressBarInfo _info;

		// Token: 0x0400584A RID: 22602
		private static TwinsBigProgressBar _twinsBar = new TwinsBigProgressBar();

		// Token: 0x0400584B RID: 22603
		private static EaterOfWorldsProgressBar _eaterOfWorldsBar = new EaterOfWorldsProgressBar();

		// Token: 0x0400584C RID: 22604
		private static BrainOfCthuluBigProgressBar _brainOfCthuluBar = new BrainOfCthuluBigProgressBar();

		// Token: 0x0400584D RID: 22605
		private static GolemHeadProgressBar _golemBar = new GolemHeadProgressBar();

		// Token: 0x0400584E RID: 22606
		private static MoonLordProgressBar _moonlordBar = new MoonLordProgressBar();

		// Token: 0x0400584F RID: 22607
		private static SolarFlarePillarBigProgressBar _solarPillarBar = new SolarFlarePillarBigProgressBar();

		// Token: 0x04005850 RID: 22608
		private static VortexPillarBigProgressBar _vortexPillarBar = new VortexPillarBigProgressBar();

		// Token: 0x04005851 RID: 22609
		private static NebulaPillarBigProgressBar _nebulaPillarBar = new NebulaPillarBigProgressBar();

		// Token: 0x04005852 RID: 22610
		private static StardustPillarBigProgressBar _stardustPillarBar = new StardustPillarBigProgressBar();

		// Token: 0x04005853 RID: 22611
		private static NeverValidProgressBar _neverValid = new NeverValidProgressBar();

		// Token: 0x04005854 RID: 22612
		private static PirateShipBigProgressBar _pirateShipBar = new PirateShipBigProgressBar();

		// Token: 0x04005855 RID: 22613
		private static MartianSaucerBigProgressBar _martianSaucerBar = new MartianSaucerBigProgressBar();

		// Token: 0x04005856 RID: 22614
		private static DeerclopsBigProgressBar _deerclopsBar = new DeerclopsBigProgressBar();

		// Token: 0x04005857 RID: 22615
		public static bool ShowText = true;

		// Token: 0x04005858 RID: 22616
		private Dictionary<int, IBigProgressBar> _bossBarsByNpcNetId = new Dictionary<int, IBigProgressBar>
		{
			{
				125,
				BigProgressBarSystem._twinsBar
			},
			{
				126,
				BigProgressBarSystem._twinsBar
			},
			{
				13,
				BigProgressBarSystem._eaterOfWorldsBar
			},
			{
				14,
				BigProgressBarSystem._eaterOfWorldsBar
			},
			{
				15,
				BigProgressBarSystem._eaterOfWorldsBar
			},
			{
				266,
				BigProgressBarSystem._brainOfCthuluBar
			},
			{
				245,
				BigProgressBarSystem._golemBar
			},
			{
				246,
				BigProgressBarSystem._golemBar
			},
			{
				249,
				BigProgressBarSystem._neverValid
			},
			{
				517,
				BigProgressBarSystem._solarPillarBar
			},
			{
				422,
				BigProgressBarSystem._vortexPillarBar
			},
			{
				507,
				BigProgressBarSystem._nebulaPillarBar
			},
			{
				493,
				BigProgressBarSystem._stardustPillarBar
			},
			{
				398,
				BigProgressBarSystem._moonlordBar
			},
			{
				396,
				BigProgressBarSystem._moonlordBar
			},
			{
				397,
				BigProgressBarSystem._moonlordBar
			},
			{
				548,
				BigProgressBarSystem._neverValid
			},
			{
				549,
				BigProgressBarSystem._neverValid
			},
			{
				491,
				BigProgressBarSystem._pirateShipBar
			},
			{
				492,
				BigProgressBarSystem._pirateShipBar
			},
			{
				440,
				BigProgressBarSystem._neverValid
			},
			{
				395,
				BigProgressBarSystem._martianSaucerBar
			},
			{
				393,
				BigProgressBarSystem._martianSaucerBar
			},
			{
				394,
				BigProgressBarSystem._martianSaucerBar
			},
			{
				68,
				BigProgressBarSystem._neverValid
			},
			{
				668,
				BigProgressBarSystem._deerclopsBar
			}
		};

		// Token: 0x04005859 RID: 22617
		private const string _preferencesKey = "ShowBossBarHealthText";
	}
}
