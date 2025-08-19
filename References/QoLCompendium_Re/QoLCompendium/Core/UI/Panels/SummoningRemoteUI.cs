using System;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Panels
{
	// Token: 0x0200027B RID: 635
	public class SummoningRemoteUI : UIState
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x00081F4C File Offset: 0x0008014C
		public override void OnInitialize()
		{
			this.BossPanel = new UIPanel();
			this.BossPanel.SetPadding(0f);
			this.BossPanel.Left.Set(575f, 0f);
			this.BossPanel.Top.Set(275f, 0f);
			this.BossPanel.Width.Set(390f, 0f);
			this.BossPanel.Height.Set(510f, 0f);
			this.BossPanel.BackgroundColor = new Color(204, 43, 43);
			this.BossPanel.OnLeftMouseDown += this.DragStart;
			this.BossPanel.OnLeftMouseUp += this.DragEnd;
			UIText BossText = new UIText(UISystem.BossText, 1f, false);
			BossText.Left.Set(10f, 0f);
			BossText.Top.Set(10f, 0f);
			BossText.Width.Set(50f, 0f);
			BossText.Height.Set(22f, 0f);
			this.BossPanel.Append(BossText);
			Asset<Texture2D> texture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/KingSlime", 2);
			Asset<Texture2D> EyeOfCthulhuTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/EyeOfCthulhu", 2);
			Asset<Texture2D> EaterOfWorldsTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/EaterOfWorlds", 2);
			Asset<Texture2D> BrainOfCthulhuTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/BrainOfCthulhu", 2);
			Asset<Texture2D> QueenBeeTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/QueenBee", 2);
			Asset<Texture2D> SkeletronTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Skeletron", 2);
			Asset<Texture2D> DeerclopsTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Deerclops", 2);
			Asset<Texture2D> WallOfFleshTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/WallOfFlesh", 2);
			Asset<Texture2D> QueenSlimeTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/QueenSlime", 2);
			Asset<Texture2D> TwinsTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Twins", 2);
			Asset<Texture2D> DestroyerTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Destroyer", 2);
			Asset<Texture2D> SkeletronPrimeTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/SkeletronPrime", 2);
			Asset<Texture2D> PlanteraTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Plantera", 2);
			Asset<Texture2D> GolemTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Golem", 2);
			Asset<Texture2D> DukeFishronTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/DukeFishron", 2);
			Asset<Texture2D> EmpressOfLightTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/EmpressOfLight", 2);
			Asset<Texture2D> LunaticCultistTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/LunaticCultist", 2);
			Asset<Texture2D> MoonLordTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/MoonLord", 2);
			Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete", 2);
			UIImageButton KingSlime = new UIImageButton(texture);
			KingSlime.Left.Set(10f, 0f);
			KingSlime.Top.Set(50f, 0f);
			KingSlime.Width.Set(30f, 0f);
			KingSlime.Height.Set(30f, 0f);
			KingSlime.OnLeftClick += this.KingSlimeClicked;
			this.BossPanel.Append(KingSlime);
			UIImageButton EyeOfCthulhu = new UIImageButton(EyeOfCthulhuTexture);
			EyeOfCthulhu.Left.Set(60f, 0f);
			EyeOfCthulhu.Top.Set(50f, 0f);
			EyeOfCthulhu.Width.Set(26f, 0f);
			EyeOfCthulhu.Height.Set(28f, 0f);
			EyeOfCthulhu.OnLeftClick += this.EyeOfCthulhuClicked;
			this.BossPanel.Append(EyeOfCthulhu);
			UIImageButton EaterOfWorlds = new UIImageButton(EaterOfWorldsTexture);
			EaterOfWorlds.Left.Set(110f, 0f);
			EaterOfWorlds.Top.Set(50f, 0f);
			EaterOfWorlds.Width.Set(24f, 0f);
			EaterOfWorlds.Height.Set(30f, 0f);
			EaterOfWorlds.OnLeftClick += this.EaterOfWorldsClicked;
			this.BossPanel.Append(EaterOfWorlds);
			UIImageButton BrainOfCthulhu = new UIImageButton(BrainOfCthulhuTexture);
			BrainOfCthulhu.Left.Set(160f, 0f);
			BrainOfCthulhu.Top.Set(50f, 0f);
			BrainOfCthulhu.Width.Set(30f, 0f);
			BrainOfCthulhu.Height.Set(30f, 0f);
			BrainOfCthulhu.OnLeftClick += this.BrainOfCthulhuClicked;
			this.BossPanel.Append(BrainOfCthulhu);
			UIImageButton QueenBee = new UIImageButton(QueenBeeTexture);
			QueenBee.Left.Set(210f, 0f);
			QueenBee.Top.Set(50f, 0f);
			QueenBee.Width.Set(30f, 0f);
			QueenBee.Height.Set(30f, 0f);
			QueenBee.OnLeftClick += this.QueenBeeClicked;
			this.BossPanel.Append(QueenBee);
			UIImageButton Skeletron = new UIImageButton(SkeletronTexture);
			Skeletron.Left.Set(260f, 0f);
			Skeletron.Top.Set(50f, 0f);
			Skeletron.Width.Set(26f, 0f);
			Skeletron.Height.Set(30f, 0f);
			Skeletron.OnLeftClick += this.SkeletronClicked;
			this.BossPanel.Append(Skeletron);
			UIImageButton Deerclops = new UIImageButton(DeerclopsTexture);
			Deerclops.Left.Set(310f, 0f);
			Deerclops.Top.Set(50f, 0f);
			Deerclops.Width.Set(48f, 0f);
			Deerclops.Height.Set(40f, 0f);
			Deerclops.OnLeftClick += this.DeerclopsClicked;
			this.BossPanel.Append(Deerclops);
			UIImageButton WallOfFlesh = new UIImageButton(WallOfFleshTexture);
			WallOfFlesh.Left.Set(10f, 0f);
			WallOfFlesh.Top.Set(100f, 0f);
			WallOfFlesh.Width.Set(30f, 0f);
			WallOfFlesh.Height.Set(30f, 0f);
			WallOfFlesh.OnLeftClick += this.WallOfFleshClicked;
			this.BossPanel.Append(WallOfFlesh);
			UIImageButton QueenSlime = new UIImageButton(QueenSlimeTexture);
			QueenSlime.Left.Set(60f, 0f);
			QueenSlime.Top.Set(100f, 0f);
			QueenSlime.Width.Set(28f, 0f);
			QueenSlime.Height.Set(30f, 0f);
			QueenSlime.OnLeftClick += this.QueenSlimeClicked;
			this.BossPanel.Append(QueenSlime);
			UIImageButton Twins = new UIImageButton(TwinsTexture);
			Twins.Left.Set(110f, 0f);
			Twins.Top.Set(100f, 0f);
			Twins.Width.Set(30f, 0f);
			Twins.Height.Set(28f, 0f);
			Twins.OnLeftClick += this.TwinsClicked;
			this.BossPanel.Append(Twins);
			UIImageButton Destroyer = new UIImageButton(DestroyerTexture);
			Destroyer.Left.Set(160f, 0f);
			Destroyer.Top.Set(100f, 0f);
			Destroyer.Width.Set(24f, 0f);
			Destroyer.Height.Set(30f, 0f);
			Destroyer.OnLeftClick += this.DestroyerClicked;
			this.BossPanel.Append(Destroyer);
			UIImageButton SkeletronPrime = new UIImageButton(SkeletronPrimeTexture);
			SkeletronPrime.Left.Set(210f, 0f);
			SkeletronPrime.Top.Set(100f, 0f);
			SkeletronPrime.Width.Set(26f, 0f);
			SkeletronPrime.Height.Set(30f, 0f);
			SkeletronPrime.OnLeftClick += this.SkeletronPrimeClicked;
			this.BossPanel.Append(SkeletronPrime);
			UIImageButton Plantera = new UIImageButton(PlanteraTexture);
			Plantera.Left.Set(260f, 0f);
			Plantera.Top.Set(100f, 0f);
			Plantera.Width.Set(30f, 0f);
			Plantera.Height.Set(30f, 0f);
			Plantera.OnLeftClick += this.PlanteraClicked;
			this.BossPanel.Append(Plantera);
			UIImageButton Golem = new UIImageButton(GolemTexture);
			Golem.Left.Set(310f, 0f);
			Golem.Top.Set(100f, 0f);
			Golem.Width.Set(28f, 0f);
			Golem.Height.Set(30f, 0f);
			Golem.OnLeftClick += this.GolemClicked;
			this.BossPanel.Append(Golem);
			UIImageButton DukeFishron = new UIImageButton(DukeFishronTexture);
			DukeFishron.Left.Set(10f, 0f);
			DukeFishron.Top.Set(150f, 0f);
			DukeFishron.Width.Set(30f, 0f);
			DukeFishron.Height.Set(30f, 0f);
			DukeFishron.OnLeftClick += this.DukeFishronClicked;
			this.BossPanel.Append(DukeFishron);
			UIImageButton EmpressOfLight = new UIImageButton(EmpressOfLightTexture);
			EmpressOfLight.Left.Set(60f, 0f);
			EmpressOfLight.Top.Set(150f, 0f);
			EmpressOfLight.Width.Set(30f, 0f);
			EmpressOfLight.Height.Set(32f, 0f);
			EmpressOfLight.OnLeftClick += this.EmpressOfLightClicked;
			this.BossPanel.Append(EmpressOfLight);
			UIImageButton LunaticCultist = new UIImageButton(LunaticCultistTexture);
			LunaticCultist.Left.Set(110f, 0f);
			LunaticCultist.Top.Set(150f, 0f);
			LunaticCultist.Width.Set(30f, 0f);
			LunaticCultist.Height.Set(30f, 0f);
			LunaticCultist.OnLeftClick += this.LunaticCultistClicked;
			this.BossPanel.Append(LunaticCultist);
			UIImageButton MoonLord = new UIImageButton(MoonLordTexture);
			MoonLord.Left.Set(160f, 0f);
			MoonLord.Top.Set(150f, 0f);
			MoonLord.Width.Set(34f, 0f);
			MoonLord.Height.Set(36f, 0f);
			MoonLord.OnLeftClick += this.MoonLordClicked;
			this.BossPanel.Append(MoonLord);
			UIText EventText = new UIText(UISystem.EventText, 1f, false);
			EventText.Left.Set(10f, 0f);
			EventText.Top.Set(190f, 0f);
			EventText.Width.Set(50f, 0f);
			EventText.Height.Set(22f, 0f);
			this.BossPanel.Append(EventText);
			Asset<Texture2D> texture2 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Rain", 2);
			Asset<Texture2D> WindTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Wind", 2);
			Asset<Texture2D> SandstormTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Sandstorm", 2);
			Asset<Texture2D> PartyTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Party", 2);
			Asset<Texture2D> SlimeRainTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/SlimeRain", 2);
			Asset<Texture2D> BloodMoonTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/BloodMoon", 2);
			Asset<Texture2D> GoblinArmyTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/GoblinArmy", 2);
			Asset<Texture2D> FrostLegionTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/FrostLegion", 2);
			Asset<Texture2D> PiratesTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Pirates", 2);
			Asset<Texture2D> EclipseTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Eclipse", 2);
			Asset<Texture2D> PumpkinMoonTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/PumpkinMoon", 2);
			Asset<Texture2D> FrostMoonTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/FrostMoon", 2);
			Asset<Texture2D> MartiansTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Martians", 2);
			Asset<Texture2D> NebulaPillarTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/NebulaPillar", 2);
			Asset<Texture2D> SolarPillarTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/SolarPillar", 2);
			Asset<Texture2D> StardustPillarTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/StardustPillar", 2);
			Asset<Texture2D> VortexPillarTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/VortexPillar", 2);
			Asset<Texture2D> LunarTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/LunarEvent", 2);
			UIImageButton Rain = new UIImageButton(texture2);
			Rain.Left.Set(10f, 0f);
			Rain.Top.Set(230f, 0f);
			Rain.Width.Set(26f, 0f);
			Rain.Height.Set(22f, 0f);
			Rain.OnLeftClick += this.RainClicked;
			this.BossPanel.Append(Rain);
			UIImageButton Wind = new UIImageButton(WindTexture);
			Wind.Left.Set(60f, 0f);
			Wind.Top.Set(230f, 0f);
			Wind.Width.Set(28f, 0f);
			Wind.Height.Set(26f, 0f);
			Wind.OnLeftClick += this.WindClicked;
			this.BossPanel.Append(Wind);
			UIImageButton Sandstorm = new UIImageButton(SandstormTexture);
			Sandstorm.Left.Set(110f, 0f);
			Sandstorm.Top.Set(230f, 0f);
			Sandstorm.Width.Set(24f, 0f);
			Sandstorm.Height.Set(24f, 0f);
			Sandstorm.OnLeftClick += this.SandstormClicked;
			this.BossPanel.Append(Sandstorm);
			UIImageButton Party = new UIImageButton(PartyTexture);
			Party.Left.Set(160f, 0f);
			Party.Top.Set(230f, 0f);
			Party.Width.Set(22f, 0f);
			Party.Height.Set(28f, 0f);
			Party.OnLeftClick += this.PartyClicked;
			this.BossPanel.Append(Party);
			UIImageButton SlimeRain = new UIImageButton(SlimeRainTexture);
			SlimeRain.Left.Set(210f, 0f);
			SlimeRain.Top.Set(230f, 0f);
			SlimeRain.Width.Set(20f, 0f);
			SlimeRain.Height.Set(26f, 0f);
			SlimeRain.OnLeftClick += this.SlimeRainClicked;
			this.BossPanel.Append(SlimeRain);
			UIImageButton BloodMoon = new UIImageButton(BloodMoonTexture);
			BloodMoon.Left.Set(260f, 0f);
			BloodMoon.Top.Set(230f, 0f);
			BloodMoon.Width.Set(22f, 0f);
			BloodMoon.Height.Set(22f, 0f);
			BloodMoon.OnLeftClick += this.BloodMoonClicked;
			this.BossPanel.Append(BloodMoon);
			UIImageButton GoblinArmy = new UIImageButton(GoblinArmyTexture);
			GoblinArmy.Left.Set(310f, 0f);
			GoblinArmy.Top.Set(230f, 0f);
			GoblinArmy.Width.Set(26f, 0f);
			GoblinArmy.Height.Set(24f, 0f);
			GoblinArmy.OnLeftClick += this.GoblinArmyClicked;
			this.BossPanel.Append(GoblinArmy);
			UIImageButton FrostLegion = new UIImageButton(FrostLegionTexture);
			FrostLegion.Left.Set(10f, 0f);
			FrostLegion.Top.Set(280f, 0f);
			FrostLegion.Width.Set(20f, 0f);
			FrostLegion.Height.Set(24f, 0f);
			FrostLegion.OnLeftClick += this.FrostLegionClicked;
			this.BossPanel.Append(FrostLegion);
			UIImageButton Pirates = new UIImageButton(PiratesTexture);
			Pirates.Left.Set(60f, 0f);
			Pirates.Top.Set(280f, 0f);
			Pirates.Width.Set(24f, 0f);
			Pirates.Height.Set(24f, 0f);
			Pirates.OnLeftClick += this.PiratesClicked;
			this.BossPanel.Append(Pirates);
			UIImageButton Eclipse = new UIImageButton(EclipseTexture);
			Eclipse.Left.Set(110f, 0f);
			Eclipse.Top.Set(280f, 0f);
			Eclipse.Width.Set(24f, 0f);
			Eclipse.Height.Set(24f, 0f);
			Eclipse.OnLeftClick += this.EclipseClicked;
			this.BossPanel.Append(Eclipse);
			UIImageButton PumpkinMoon = new UIImageButton(PumpkinMoonTexture);
			PumpkinMoon.Left.Set(160f, 0f);
			PumpkinMoon.Top.Set(280f, 0f);
			PumpkinMoon.Width.Set(22f, 0f);
			PumpkinMoon.Height.Set(22f, 0f);
			PumpkinMoon.OnLeftClick += this.PumpkinMoonClicked;
			this.BossPanel.Append(PumpkinMoon);
			UIImageButton FrostMoon = new UIImageButton(FrostMoonTexture);
			FrostMoon.Left.Set(210f, 0f);
			FrostMoon.Top.Set(280f, 0f);
			FrostMoon.Width.Set(22f, 0f);
			FrostMoon.Height.Set(22f, 0f);
			FrostMoon.OnLeftClick += this.FrostMoonClicked;
			this.BossPanel.Append(FrostMoon);
			UIImageButton Martians = new UIImageButton(MartiansTexture);
			Martians.Left.Set(260f, 0f);
			Martians.Top.Set(280f, 0f);
			Martians.Width.Set(26f, 0f);
			Martians.Height.Set(24f, 0f);
			Martians.OnLeftClick += this.MartiansClicked;
			this.BossPanel.Append(Martians);
			UIImageButton NebulaPillar = new UIImageButton(NebulaPillarTexture);
			NebulaPillar.Left.Set(310f, 0f);
			NebulaPillar.Top.Set(280f, 0f);
			NebulaPillar.Width.Set(24f, 0f);
			NebulaPillar.Height.Set(26f, 0f);
			NebulaPillar.OnLeftClick += this.NebulaPillarClicked;
			this.BossPanel.Append(NebulaPillar);
			UIImageButton SolarPillar = new UIImageButton(SolarPillarTexture);
			SolarPillar.Left.Set(10f, 0f);
			SolarPillar.Top.Set(330f, 0f);
			SolarPillar.Width.Set(24f, 0f);
			SolarPillar.Height.Set(26f, 0f);
			SolarPillar.OnLeftClick += this.SolarPillarClicked;
			this.BossPanel.Append(SolarPillar);
			UIImageButton StardustPillar = new UIImageButton(StardustPillarTexture);
			StardustPillar.Left.Set(60f, 0f);
			StardustPillar.Top.Set(330f, 0f);
			StardustPillar.Width.Set(24f, 0f);
			StardustPillar.Height.Set(26f, 0f);
			StardustPillar.OnLeftClick += this.StardustPillarClicked;
			this.BossPanel.Append(StardustPillar);
			UIImageButton VortexPillar = new UIImageButton(VortexPillarTexture);
			VortexPillar.Left.Set(110f, 0f);
			VortexPillar.Top.Set(330f, 0f);
			VortexPillar.Width.Set(24f, 0f);
			VortexPillar.Height.Set(26f, 0f);
			VortexPillar.OnLeftClick += this.VortexPillarClicked;
			this.BossPanel.Append(VortexPillar);
			UIImageButton LunarEvent = new UIImageButton(LunarTexture);
			LunarEvent.Left.Set(160f, 0f);
			LunarEvent.Top.Set(330f, 0f);
			LunarEvent.Width.Set(24f, 0f);
			LunarEvent.Height.Set(26f, 0f);
			LunarEvent.OnLeftClick += this.LunarEventClicked;
			this.BossPanel.Append(LunarEvent);
			UIText MinibossesText = new UIText(UISystem.MinibossText, 1f, false);
			MinibossesText.Left.Set(10f, 0f);
			MinibossesText.Top.Set(370f, 0f);
			MinibossesText.Width.Set(50f, 0f);
			MinibossesText.Height.Set(22f, 0f);
			this.BossPanel.Append(MinibossesText);
			Asset<Texture2D> texture3 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/DarkMage", 2);
			Asset<Texture2D> DreadnautilusTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Dreadnautilus", 2);
			Asset<Texture2D> FlyingDutchmanTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/FlyingDutchman", 2);
			Asset<Texture2D> OgreTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Ogre", 2);
			Asset<Texture2D> MourningWoodTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/MourningWood", 2);
			Asset<Texture2D> PumpkingTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Pumpking", 2);
			Asset<Texture2D> EverscreamTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Everscream", 2);
			Asset<Texture2D> SantankTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Santank", 2);
			Asset<Texture2D> IceQueenTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/IceQueen", 2);
			Asset<Texture2D> MartianSaucerTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/MartianSaucer", 2);
			Asset<Texture2D> BetsyTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Betsy", 2);
			UIImageButton DarkMage = new UIImageButton(texture3);
			DarkMage.Left.Set(10f, 0f);
			DarkMage.Top.Set(410f, 0f);
			DarkMage.Width.Set(32f, 0f);
			DarkMage.Height.Set(32f, 0f);
			DarkMage.OnLeftClick += this.DarkMageClicked;
			this.BossPanel.Append(DarkMage);
			UIImageButton Dreadnautilus = new UIImageButton(DreadnautilusTexture);
			Dreadnautilus.Left.Set(60f, 0f);
			Dreadnautilus.Top.Set(410f, 0f);
			Dreadnautilus.Width.Set(40f, 0f);
			Dreadnautilus.Height.Set(34f, 0f);
			Dreadnautilus.OnLeftClick += this.DreadnautilusClicked;
			this.BossPanel.Append(Dreadnautilus);
			UIImageButton FlyingDutchman = new UIImageButton(FlyingDutchmanTexture);
			FlyingDutchman.Left.Set(110f, 0f);
			FlyingDutchman.Top.Set(410f, 0f);
			FlyingDutchman.Width.Set(30f, 0f);
			FlyingDutchman.Height.Set(28f, 0f);
			FlyingDutchman.OnLeftClick += this.FlyingDutchmanClicked;
			this.BossPanel.Append(FlyingDutchman);
			UIImageButton Ogre = new UIImageButton(OgreTexture);
			Ogre.Left.Set(160f, 0f);
			Ogre.Top.Set(410f, 0f);
			Ogre.Width.Set(26f, 0f);
			Ogre.Height.Set(30f, 0f);
			Ogre.OnLeftClick += this.OgreClicked;
			this.BossPanel.Append(Ogre);
			UIImageButton MourningWood = new UIImageButton(MourningWoodTexture);
			MourningWood.Left.Set(210f, 0f);
			MourningWood.Top.Set(410f, 0f);
			MourningWood.Width.Set(30f, 0f);
			MourningWood.Height.Set(30f, 0f);
			MourningWood.OnLeftClick += this.MourningWoodClicked;
			this.BossPanel.Append(MourningWood);
			UIImageButton Pumpking = new UIImageButton(PumpkingTexture);
			Pumpking.Left.Set(260f, 0f);
			Pumpking.Top.Set(410f, 0f);
			Pumpking.Width.Set(30f, 0f);
			Pumpking.Height.Set(30f, 0f);
			Pumpking.OnLeftClick += this.PumpkingClicked;
			this.BossPanel.Append(Pumpking);
			UIImageButton Everscream = new UIImageButton(EverscreamTexture);
			Everscream.Left.Set(310f, 0f);
			Everscream.Top.Set(410f, 0f);
			Everscream.Width.Set(30f, 0f);
			Everscream.Height.Set(30f, 0f);
			Everscream.OnLeftClick += this.EverscreamClicked;
			this.BossPanel.Append(Everscream);
			UIImageButton Santank = new UIImageButton(SantankTexture);
			Santank.Left.Set(10f, 0f);
			Santank.Top.Set(460f, 0f);
			Santank.Width.Set(30f, 0f);
			Santank.Height.Set(30f, 0f);
			Santank.OnLeftClick += this.SantankClicked;
			this.BossPanel.Append(Santank);
			UIImageButton IceQueen = new UIImageButton(IceQueenTexture);
			IceQueen.Left.Set(60f, 0f);
			IceQueen.Top.Set(460f, 0f);
			IceQueen.Width.Set(30f, 0f);
			IceQueen.Height.Set(30f, 0f);
			IceQueen.OnLeftClick += this.IceQueenClicked;
			this.BossPanel.Append(IceQueen);
			UIImageButton MartianSaucer = new UIImageButton(MartianSaucerTexture);
			MartianSaucer.Left.Set(110f, 0f);
			MartianSaucer.Top.Set(460f, 0f);
			MartianSaucer.Width.Set(30f, 0f);
			MartianSaucer.Height.Set(30f, 0f);
			MartianSaucer.OnLeftClick += this.MartianSaucerClicked;
			this.BossPanel.Append(MartianSaucer);
			UIImageButton Betsy = new UIImageButton(BetsyTexture);
			Betsy.Left.Set(160f, 0f);
			Betsy.Top.Set(460f, 0f);
			Betsy.Width.Set(30f, 0f);
			Betsy.Height.Set(30f, 0f);
			Betsy.OnLeftClick += this.BetsyClicked;
			this.BossPanel.Append(Betsy);
			UIImageButton closeButton = new UIImageButton(buttonDeleteTexture);
			closeButton.Left.Set(360f, 0f);
			closeButton.Top.Set(10f, 0f);
			closeButton.Width.Set(22f, 0f);
			closeButton.Height.Set(22f, 0f);
			closeButton.OnLeftClick += this.CloseButtonClicked;
			this.BossPanel.Append(closeButton);
			base.Append(this.BossPanel);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00083C1C File Offset: 0x00081E1C
		private void KingSlimeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 50;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00083C70 File Offset: 0x00081E70
		private void EyeOfCthulhuClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 4;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00083CC4 File Offset: 0x00081EC4
		private void EaterOfWorldsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedBoss1)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 13;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[4].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00083D60 File Offset: 0x00081F60
		private void BrainOfCthulhuClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedBoss1)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 266;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[4].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00083E00 File Offset: 0x00082000
		private void QueenBeeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedBoss2)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 222;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBossWithTwoOptions"), new object[]
				{
					ContentSamples.NpcsByNetId[13].FullName,
					ContentSamples.NpcsByNetId[266].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00083EB8 File Offset: 0x000820B8
		private void SkeletronClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedBoss2)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 35;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBossWithTwoOptions"), new object[]
				{
					ContentSamples.NpcsByNetId[13].FullName,
					ContentSamples.NpcsByNetId[266].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00083F70 File Offset: 0x00082170
		private void DeerclopsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 668;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00083FC8 File Offset: 0x000821C8
		private void WallOfFleshClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedBoss3)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 113;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[35].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00084064 File Offset: 0x00082264
		private void QueenSlimeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (Main.hardMode)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 657;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x000840E4 File Offset: 0x000822E4
		private void TwinsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (Main.hardMode)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 125;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00084160 File Offset: 0x00082360
		private void DestroyerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (Main.hardMode)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 134;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000841E0 File Offset: 0x000823E0
		private void SkeletronPrimeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (Main.hardMode)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 127;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0008425C File Offset: 0x0008245C
		private void PlanteraClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 262;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedThreeMechs"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000842EC File Offset: 0x000824EC
		private void GolemClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedPlantBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 245;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[262].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00084390 File Offset: 0x00082590
		private void DukeFishronClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (Main.hardMode)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 370;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00084410 File Offset: 0x00082610
		private void EmpressOfLightClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedPlantBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 636;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[262].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000844B4 File Offset: 0x000826B4
		private void LunaticCultistClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedGolemBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 439;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[245].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00084558 File Offset: 0x00082758
		private void MoonLordClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedAncientCultist)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 398;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[439].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000845FC File Offset: 0x000827FC
		private void DarkMageClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedBoss2)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 564;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBossWithTwoOptions"), new object[]
				{
					ContentSamples.NpcsByNetId[13].FullName,
					ContentSamples.NpcsByNetId[266].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000846B4 File Offset: 0x000828B4
		private void DreadnautilusClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (Main.hardMode)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 618;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00084734 File Offset: 0x00082934
		private void FlyingDutchmanClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (Main.hardMode)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 491;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x000847B4 File Offset: 0x000829B4
		private void OgreClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedMechBossAny)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 576;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedOneMech"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00084834 File Offset: 0x00082A34
		private void MourningWoodClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedPlantBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 325;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[262].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000848D8 File Offset: 0x00082AD8
		private void PumpkingClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedPlantBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 327;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[262].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0008497C File Offset: 0x00082B7C
		private void EverscreamClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedPlantBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 344;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[262].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00084A20 File Offset: 0x00082C20
		private void SantankClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedPlantBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 346;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[262].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00084AC4 File Offset: 0x00082CC4
		private void IceQueenClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedPlantBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 345;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[262].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00084B68 File Offset: 0x00082D68
		private void MartianSaucerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedGolemBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 395;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[245].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00084C0C File Offset: 0x00082E0C
		private void BetsyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedGolemBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 551;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[245].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00084CB0 File Offset: 0x00082EB0
		private void RainClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 1;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00084D04 File Offset: 0x00082F04
		private void WindClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 2;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00084D58 File Offset: 0x00082F58
		private void SandstormClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 3;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00084DAC File Offset: 0x00082FAC
		private void PartyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 4;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00084E00 File Offset: 0x00083000
		private void SlimeRainClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 5;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00084E54 File Offset: 0x00083054
		private void BloodMoonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 6;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00084EA8 File Offset: 0x000830A8
		private void GoblinArmyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 7;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00084EFC File Offset: 0x000830FC
		private void FrostLegionClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (Main.hardMode)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 8;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00084F78 File Offset: 0x00083178
		private void PiratesClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (Main.hardMode)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 9;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00084FF4 File Offset: 0x000831F4
		private void EclipseClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedMechBossAny)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 10;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedOneMech"), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00085070 File Offset: 0x00083270
		private void PumpkinMoonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedPlantBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 11;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[262].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00085114 File Offset: 0x00083314
		private void FrostMoonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedPlantBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 12;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[262].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000851B8 File Offset: 0x000833B8
		private void MartiansClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedGolemBoss)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 13;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[245].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0008525C File Offset: 0x0008345C
		private void NebulaPillarClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedAncientCultist)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 14;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[439].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00085300 File Offset: 0x00083500
		private void SolarPillarClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedAncientCultist)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 15;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[439].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000853A4 File Offset: 0x000835A4
		private void StardustPillarClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedAncientCultist)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 16;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[439].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00085448 File Offset: 0x00083648
		private void VortexPillarClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedAncientCultist)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 17;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[439].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000854EC File Offset: 0x000836EC
		private void LunarEventClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				if (NPC.downedAncientCultist)
				{
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 18;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
					Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
					SummoningRemoteUI.visible = false;
					return;
				}
				TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[]
				{
					ContentSamples.NpcsByNetId[439].FullName
				}), new Color(255, 240, 20), true);
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00085590 File Offset: 0x00083790
		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, null, null);
				SummoningRemoteUI.visible = false;
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000855C8 File Offset: 0x000837C8
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			this.offset = new Vector2(evt.MousePosition.X - this.BossPanel.Left.Pixels, evt.MousePosition.Y - this.BossPanel.Top.Pixels);
			this.dragging = true;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00085620 File Offset: 0x00083820
		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			this.dragging = false;
			this.BossPanel.Left.Set(end.X - this.offset.X, 0f);
			this.BossPanel.Top.Set(end.Y - this.offset.Y, 0f);
			this.Recalculate();
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00085690 File Offset: 0x00083890
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Vector2 MousePosition;
			MousePosition..ctor((float)Main.mouseX, (float)Main.mouseY);
			if (this.BossPanel.ContainsPoint(MousePosition))
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			if (this.dragging)
			{
				this.BossPanel.Left.Set(MousePosition.X - this.offset.X, 0f);
				this.BossPanel.Top.Set(MousePosition.Y - this.offset.Y, 0f);
				this.Recalculate();
			}
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00085728 File Offset: 0x00083928
		public static void BossClick(int bossID, SoundStyle sound)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = bossID;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00085774 File Offset: 0x00083974
		public static void EventClick(int eventID, SoundStyle sound)
		{
			if (Main.GameUpdateCount - SummoningRemoteUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = eventID;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
			}
		}

		// Token: 0x04000714 RID: 1812
		public UIPanel BossPanel;

		// Token: 0x04000715 RID: 1813
		public static bool visible;

		// Token: 0x04000716 RID: 1814
		public static uint timeStart;

		// Token: 0x04000717 RID: 1815
		private Vector2 offset;

		// Token: 0x04000718 RID: 1816
		public bool dragging;
	}
}
