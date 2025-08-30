ExampleMod/Content/Biomes フォルダ学習まとめ:

- ExampleSurfaceBackgroundStyle.cs (ModSurfaceBackgroundStyle)
  - 目的: 地上背景のフェード制御とテクスチャ選択(遠景/中景/近景)
  - API: ModifyFarFades(fades, transitionSpeed), ChooseFarTexture, ChooseMiddleTexture(フレーム進行), ChooseCloseTexture(ref scale, ref parallax, ref a, ref b)
  - 注意: BackgroundTextureLoader.GetBackgroundSlot(Mod, path) で背景スロット取得、Slot と fades の相互排他処理

- ExampleSurfaceBiome.cs (ModBiome)
  - 目的: 地上バイオームのシーン効果/音楽/背景/マップ背景/たいまつ・焚火・トーチ神対応
  - API: WaterStyle, SurfaceBackgroundStyle, TileColorStyle, Music, BiomeTorchItemType, BiomeCampfireItemType, BestiaryIcon/BackgroundPath/BackgroundColor/MapBackground, IsBiomeActive(Player), Priority
  - 条件: タイル数(ExampleBiomeTileCount>=40)、世界中心1/3幅、地上(ZoneSky/Overworld)

- ExampleUndergroundBackgroundStyle.cs (ModUndergroundBackgroundStyle)
  - 目的: 地下背景のテクスチャセットを配列に充填
  - API: FillTextureArray(int[] textureSlots)

- ExampleUndergroundBiome.cs (ModBiome)
  - 目的: 地下バイオームのシーン効果/音楽/背景
  - API: UndergroundBackgroundStyle, Music, Priority, Bestiary関連, IsBiomeActive(Player)
  - 条件: Rock/Dirt層、高さ中央1/3、タイル数>=40
  - 参考: GetWeight(Player) の優先競合時重み付け(コメント例)

- ExampleWaterStyle.cs (ModWaterStyle)
  - 目的: 水の見た目/挙動(滝、スプラッシュダスト、雫Gore、髪色、雨バリアント/テクスチャ)
  - API: Load(), ChooseWaterfallStyle(), GetSplashDust(), GetDropletGore(), LightColorMultiplier, BiomeHairColor, GetRainVariant, GetRainTexture()
  - 資産: Mod.Assets.Request<Texture2D>("Content/Biomes/ExampleRain")

- ExampleWaterfallStyle.cs (ModWaterfallStyle)
  - 目的: 滝の発光
  - API: AddLight(i,j) -> Lighting.AddLight(ToWorldCoordinates(), Color.White*0.5)
