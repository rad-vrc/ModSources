using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.IO;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003A2 RID: 930
	public class BigProgressBarSystem
	{
		// Token: 0x060029BE RID: 10686 RVA: 0x00594CE8 File Offset: 0x00592EE8
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Configuration_OnLoad;
			preferences.OnSave += this.Configuration_Save;
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x00594D0E File Offset: 0x00592F0E
		public void Update()
		{
			if (this._currentBar == null)
			{
				this.TryFindingNPCToTrack();
			}
			if (this._currentBar == null)
			{
				return;
			}
			if (!this._currentBar.ValidateAndCollectNecessaryInfo(ref this._info))
			{
				this._currentBar = null;
			}
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00594D41 File Offset: 0x00592F41
		public void Draw(SpriteBatch spriteBatch)
		{
			if (this._currentBar == null)
			{
				return;
			}
			this._currentBar.Draw(ref this._info, spriteBatch);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00594D60 File Offset: 0x00592F60
		private void TryFindingNPCToTrack()
		{
			Rectangle value = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
			value.Inflate(5000, 5000);
			float num = float.PositiveInfinity;
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.Hitbox.Intersects(value))
				{
					float num2 = npc.Distance(Main.LocalPlayer.Center);
					if (num > num2 && this.TryTracking(i))
					{
						num = num2;
					}
				}
			}
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x00594E00 File Offset: 0x00593000
		public bool TryTracking(int npcIndex)
		{
			if (npcIndex < 0 || npcIndex > 200)
			{
				return false;
			}
			NPC npc = Main.npc[npcIndex];
			if (!npc.active)
			{
				return false;
			}
			BigProgressBarInfo info = new BigProgressBarInfo
			{
				npcIndexToAimAt = npcIndex
			};
			IBigProgressBar bigProgressBar = this._bossBar;
			IBigProgressBar bigProgressBar2;
			if (this._bossBarsByNpcNetId.TryGetValue(npc.netID, out bigProgressBar2))
			{
				bigProgressBar = bigProgressBar2;
			}
			if (!bigProgressBar.ValidateAndCollectNecessaryInfo(ref info))
			{
				return false;
			}
			this._currentBar = bigProgressBar;
			info.showText = true;
			this._info = info;
			return true;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00594E81 File Offset: 0x00593081
		private void Configuration_Save(Preferences obj)
		{
			obj.Put("ShowBossBarHealthText", BigProgressBarSystem.ShowText);
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00594E98 File Offset: 0x00593098
		private void Configuration_OnLoad(Preferences obj)
		{
			BigProgressBarSystem.ShowText = obj.Get<bool>("ShowBossBarHealthText", BigProgressBarSystem.ShowText);
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x00594EAF File Offset: 0x005930AF
		public static void ToggleShowText()
		{
			BigProgressBarSystem.ShowText = !BigProgressBarSystem.ShowText;
		}

		// Token: 0x04004CA8 RID: 19624
		private IBigProgressBar _currentBar;

		// Token: 0x04004CA9 RID: 19625
		private CommonBossBigProgressBar _bossBar = new CommonBossBigProgressBar();

		// Token: 0x04004CAA RID: 19626
		private BigProgressBarInfo _info;

		// Token: 0x04004CAB RID: 19627
		private static TwinsBigProgressBar _twinsBar = new TwinsBigProgressBar();

		// Token: 0x04004CAC RID: 19628
		private static EaterOfWorldsProgressBar _eaterOfWorldsBar = new EaterOfWorldsProgressBar();

		// Token: 0x04004CAD RID: 19629
		private static BrainOfCthuluBigProgressBar _brainOfCthuluBar = new BrainOfCthuluBigProgressBar();

		// Token: 0x04004CAE RID: 19630
		private static GolemHeadProgressBar _golemBar = new GolemHeadProgressBar();

		// Token: 0x04004CAF RID: 19631
		private static MoonLordProgressBar _moonlordBar = new MoonLordProgressBar();

		// Token: 0x04004CB0 RID: 19632
		private static SolarFlarePillarBigProgressBar _solarPillarBar = new SolarFlarePillarBigProgressBar();

		// Token: 0x04004CB1 RID: 19633
		private static VortexPillarBigProgressBar _vortexPillarBar = new VortexPillarBigProgressBar();

		// Token: 0x04004CB2 RID: 19634
		private static NebulaPillarBigProgressBar _nebulaPillarBar = new NebulaPillarBigProgressBar();

		// Token: 0x04004CB3 RID: 19635
		private static StardustPillarBigProgressBar _stardustPillarBar = new StardustPillarBigProgressBar();

		// Token: 0x04004CB4 RID: 19636
		private static NeverValidProgressBar _neverValid = new NeverValidProgressBar();

		// Token: 0x04004CB5 RID: 19637
		private static PirateShipBigProgressBar _pirateShipBar = new PirateShipBigProgressBar();

		// Token: 0x04004CB6 RID: 19638
		private static MartianSaucerBigProgressBar _martianSaucerBar = new MartianSaucerBigProgressBar();

		// Token: 0x04004CB7 RID: 19639
		private static DeerclopsBigProgressBar _deerclopsBar = new DeerclopsBigProgressBar();

		// Token: 0x04004CB8 RID: 19640
		public static bool ShowText = true;

		// Token: 0x04004CB9 RID: 19641
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

		// Token: 0x04004CBA RID: 19642
		private const string _preferencesKey = "ShowBossBarHealthText";
	}
}
