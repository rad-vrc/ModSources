# Items 学習 (6-of-10) Placeable

対象: ExampleMod/Content/Items/Placeable/ 以下と Banners, Furniture
tModLoader: 1.4.4

共通パターン
- 配置系ショートハンド
  - Item.DefaultToPlaceableTile(ModContent.TileType<Tile>(), placeStyle? )
  - Item.DefaultToPlaceableWall(ModContent.WallType<Wall>())
  - Item.DefaultToTorch(ModContent.TileType<Tile>(), style, allowWaterPlacement)
  - Item.DefaultToMusicBox(mod, musicSlot, tileType, style) …(MusicBox側のAddMusicBox連携含む)
- 研究数/並び/価格
  - Item.ResearchUnlockCount (例: Block/Wall 100/400, Torch 100, Ore 100, Platform 200)
  - ItemID.Sets.SortingPriorityMaterials[Item.type] などで並び制御
  - Item.SetShopValues(ItemRarityColor.X, Item.buyPrice(...))
- クリエ/シマー/セット
  - ItemID.Sets.Torches[Type] / WaterTorches / SingleUseInGamepad
  - ItemID.Sets.ShimmerTransformToItem[Type] (ShimmerTorch など)

個別メモ
- ExampleOre.cs
  - Research 100, SortingPriorityMaterials=58
  - ItemID.Sets.OreDropsFromSlime[Type]=(3,13)
  - DefaultToPlaceableTile Tiles.ExampleOre

- ExampleSandBlock.cs
  - Research 100
  - ItemID.Sets.SandgunAmmoProjectileData[Type] = (ProjectileType<ExampleSandBallGunProjectile>(), +10dmg)
  - Item.ammo = AmmoID.Sand; Item.notAmmo = true（ツールチップ抑制のための慣習）
  - DefaultToPlaceableTile Tiles.ExampleSand; レシピ: ExampleItem→10 (Workbench)

- ExampleSlopedTile.cs
  - DefaultToPlaceableTile Tiles.ExampleSlopeTile; レシピ: ExampleBlock→1

- ExampleStatue.cs
  - Item.CloneDefaults(ItemID.ArmorStatue); Item.createTile = Tiles.ExampleStatue; placeStyle=0
  - レシピ: ExampleItem + ExampleWorkbench

- ExampleTrap.cs（1クラス複数アイテム）
  - 内部 ILoadable で mod.AddContent(new ExampleTrap(0/1)) を手動追加
  - CloneNewInstances=true（インスタンスカスタムフィールド維持）
  - Name を placeStyle から一意名に分岐（GetInternalNameFromStyle）
  - DefaultToPlaceableTile(Tiles.ExampleTrap, placeStyle)
  - レシピ: DartTrap→変換

- Torch 系
  - ExampleTorch.cs
    - Research 100, ShimmerTransform→ShimmerTorch, Torches=true
    - DefaultToTorch(Tiles.ExampleTorch, style:0, allowWater:false)
    - HoldItem でDust/Light, PostUpdateでLight（水中では無効）
    - レシピ: ExampleItem + ExampleWorkbench
  - ExampleWaterTorch.cs
    - Research 100, Torches=true, WaterTorches=true
    - DefaultToTorch(Tiles.ExampleTorch, style:1, allowWater:true)
    - HoldItem/Worldで緑系ライトを加える（濡れてても点灯）
    - レシピ: ExampleTorch + Gel

- Wall 系
  - ExampleWall.cs / ExampleWallAdvanced.cs
    - Research 400
    - DefaultToPlaceableWall(Walls.ExampleWall / ExampleWallAdvanced)
    - レシピ: ExampleBlock→4 + ExampleWorkbench

- Pylon アイテム
  - ExamplePylonItem.cs: DefaultToPlaceableTile(ExamplePylonTile); SetShopValues(Blue1, 10g)
  - ExamplePylonItemAdvanced.cs: DefaultToPlaceableTile(ExamplePylonTileAdvanced); SetShopValues(LightRed4, 20g)

- Campfire/Lamp/LivingFire/HerbSeeds/MusicBox
  - ExampleCampfire.cs: DefaultToPlaceableTile Tiles.ExampleCampfire; レシピ: 木グループ + ExampleTorch
  - ExampleLamp.cs: Tooltipを LocalizedText.Empty に（辞書キー生成抑止）; DefaultToPlaceableTile Tiles.ExampleLamp
  - ExampleLivingFire.cs: 溶岩耐性, DefaultToPlaceableTile Tiles.ExampleLivingFireTile, PostUpdateで発光, クリスタルボール使用レシピ
  - ExampleHerbSeeds.cs: DisableAutomaticPlaceableDrop; DefaultToPlaceableTile Tiles.ExampleHerb; Research 25
  - ExampleMusicBox.cs: Prefix不可、ShimmerでバニラMusicBoxへ変換、曲連携(AddMusicBox) + DefaultToMusicBox

- Banners（敵バナーをアイテム化）
  - ExampleCustomAISlimeNPCBanner.cs / ExampleWormHeadBanner.cs
  - DefaultToPlaceableTile(EnemyBanner, style=(int)EnemyBanner.StyleID.X)
  - SetShopValues(Blue1, 10s)

- Furniture（代表）
  - ExampleBed/Chair/Table/Door/Dresser/Platform/Sink/Clock/Toilet/Chandelier/Chest/Workbench/WideBanner
  - 全て DefaultToPlaceableTile(対応Tile)。Workbench は ModifyResearchSorting で CraftingObjects に分類。
  - 研究数例: Platform=200; Dresser=1; 他は未指定/任意
  - 代表レシピ: ExampleItem + ExampleWorkbench（または既存家具 + ExampleItem）
  - Chest: ExampleChestKey も同ファイルに定義（CloneDefaults(GoldenKey); ResearchUnlockCount=3）
  - Door: Tile は ExampleDoorClosed を指定（開閉別Tile設計）
  - Chandelier: Tile の RandomStyleRange でランダムスタイル配置（挙動はTile側）
  - WideBanner: ExampleWideBannerTile を配置
  - Workbench: 置き台タイル、研究グループ切替

- Relic/Trophy（ボス戦利品）
  - MinionBossRelic.cs: DefaultToPlaceableTile(Tiles.Furniture.MinionBossRelic, style 0); Item.master=true; rarity=Master
  - MinionBossTrophy.cs: DefaultToPlaceableTile(Tiles.Furniture.MinionBossTrophy); rarity=Blue

実装ポイント/落とし穴
- Torch: allowWaterPlacement による Item.noWet 切替と WaterTorches セット、Tile 側での sub tile 実装が必要
- 複数アイテム/1クラス: ILoadable + 非デフォルトコンストラクタ + CloneNewInstances + Name オーバーライドで一意キー
- Sand 弾: ItemID.Sets.SandgunAmmoProjectileData を使い Item.shoot/damage は未使用慣習、AmmoID.Sand + notAmmo=true
- 研究やソート優先度、シマー変換、ショップ価格は ItemID.Sets/Item.SetShopValues で調整
- Workbench などは CreativeHelper.ItemGroup を CraftingObjects に変更して研究UIの分類最適化

関連 API/呼び出し例
- Item.DefaultToPlaceableTile / DefaultToPlaceableWall / DefaultToTorch / DefaultToMusicBox
- ItemID.Sets.* (Torches, WaterTorches, SandgunAmmoProjectileData, SortingPriorityMaterials, OreDropsFromSlime, ShimmerTransformToItem)
- Item.SetShopValues(ItemRarityColor, Item.buyPrice)
- Lighting.AddLight, Dust.NewDustDirect（Torchの演出）
- ModContent.TileType<> / WallType<>

確認済みファイル
- Root: ExampleBar, ExampleBlock, ExampleCampfire, ExampleHerbSeeds, ExampleLamp, ExampleLivingFire, ExampleMusicBox, ExampleOre, ExamplePylonItem, ExamplePylonItemAdvanced, ExampleSandBlock, ExampleSlopedTile, ExampleStatue, ExampleTorch, ExampleTrap, ExampleWall, ExampleWallAdvanced, ExampleWaterTorch
- Banners: ExampleCustomAISlimeNPCBanner, ExampleWormHeadBanner
- Furniture: ExampleBed, ExampleChair, ExampleChandelier, ExampleChest(+Key), ExampleClock, ExampleDoor, ExampleDresser, ExamplePlatform, ExampleSink, ExampleTable, ExampleToilet, ExampleWideBanner, ExampleWorkbench, MinionBossRelic, MinionBossTrophy

要約
- Placeable 全般のショートハンド呼び出しと各 ItemID.Sets の使い分け、Torch と Sand 弾の特殊系、Iloadable による 1クラス多品目、家具の研究・分類調整、Relic/Trophy の Master 設定まで一通り把握。Tile 側実装（例: 水中トーチの sub tile, Chandelier の RandomStyleRange）は別途参照が必要。