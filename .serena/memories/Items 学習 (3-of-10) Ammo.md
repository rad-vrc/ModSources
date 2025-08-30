# Items 学習 (3-of-10) Ammo

目的: 弾/矢/ロケット/ソリューション弾の実装パターン、弾種と発射体の紐付け、地形変換ロジックを把握

共通事項
- 基本: Item.damage は武器ダメージと合算、Item.DamageType=Ranged、Item.consumable、Item.maxStack=CommonMaxStack、Item.ammo=AmmoID.X または 自身のType
- 発射体紐付け: Item.shoot=ModContent.ProjectileType<...>、Item.shootSpeed。ロケットのみ特殊（下記）
- 研究: Item.ResearchUnlockCount=99
- レシピ: CreateRecipe(...).AddIngredient<ExampleItem>().AddTile<ExampleWorkbench>().Register()

矢/弾の代表
- ExampleArrow.cs: 木の矢相当。Item.ammo=AmmoID.Arrow、shoot=ExampleArrowProjectile、knockBack=1.5、shootSpeed=3
- ExampleBullet.cs: 銀弾相当の速度感（Proj.extraUpdates=1 を考慮して shootSpeed=4.5）。ammo=Bullet
- ExampleGravityDebuffBullet.cs: 弾速速い(s=16)。命中時の重力デバフは Projectile 側で実装前提

カスタム弾クラス
- ExampleCustomAmmo.cs:
  - Item.ammo=Item.type（このアイテム自身を弾種の基準に）
  - shoot=ExampleHomingProjectile（ホーミング弾想定）
  - 例: ExampleCustomAmmoGun 側で ammo=ModContent.ItemType<ExampleCustomAmmo>() を要求

ロケット弾の特殊性
- ExampleRocket.cs:
  - AmmoID.Sets.IsSpecialist[Type]=true（茸兜バフ対象）
  - AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[<LauncherID>].Add(Type, <ProjectileType>) で武器毎に出る弾を指定
  - Celebration Mk2 だけは Celeb2Rocket* 系の既存 ProjectileID を指定
  - Item.ammo=AmmoID.Rocket。Item.shoot は設定しない（ロジックが武器×弾種の対応表で決まるため）
  - レシピ: RocketI 100 + ExampleItem、Cyborg 在住条件

ソリューション（地形変換スプレー）
- ExampleSolution.cs + ExampleSolutionProjectile + ExampleSolutionConversion:
  - Item.DefaultToSolution(ModContent.ProjectileType<...>), SortingPriorityTerraforming で並び順
  - Projectile.DefaultToSpray(); aiStyle=0; CanDamage=false; timeLeft<=133; ai[1]==1f で terraformer 強化
  - AI: owner==myPlayer かつ Center.ToTileCoordinates → WorldGen.Convert(x,y, ConversionType, size) 実行
  - Dust 表示: 進行段階 Progress(ai0) に応じてスケール/範囲調整
  - ModBiomeConversion: PostSetupContent で変換登録
    - WallLoader.RegisterConversion(i, Type, ConvertWalls) for natural walls
    - TileLoader.RegisterConversion(..., ConvertSand/ConvertStone)
    - ConvertStone: 上の宝石樹を検出し通常木に差し替え → 床も変換; その他 Chair/Workbench を個別変換

- ExampleHellSolution.cs + ExampleHellSolutionProjectile + ExampleHellSolutionConversion:
  - 地獄化（Ash/AshGrass/AshPlants/溶岩壁）
  - PostSetupContent: Grass/GolfGrass→HellifyGrass, Dirt/Stone/Sand/Clay→ConvertToAsh, Walls(Dirt/Grass/Stone)→HellifyWall
  - HellifyGrass: 上タイルが草系/植物なら先に植物を AshPlants に直書き変換（Tallはトリミング）→ 最後に WorldGen.ConvertTile(..., AshGrass)
  - 木の変換は IsATreeTrunk を用いて幹だけ追跡し、TreeAsh に置換。床も AshGrass に直書き

実装チートシート
- 弾/矢: Item.ammo=AmmoID.Bullet/Arrow、Item.shoot=ModContent.ProjectileType<...>
- 自作弾種: Item.ammo=Item.type（この弾をカテゴリの先頭に）
- ロケット: AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[武器ID].Add(弾Type, 発射体Type)
- ソリューション: Item.DefaultToSolution(...); Projectile.DefaultToSpray(); WorldGen.Convert(...); ModBiomeConversion.PostSetupContent で Tile/Wall conversion 登録
- 表示: ItemID.Sets.SortingPriorityTerraforming[Type] で整地カテゴリ順制御

注意/落とし穴
- ロケットは Item.shoot を設定しない（発射体は対応表で決定）
- Convert系で植物や家具はフレーム崩壊に注意: 必要箇所は TileType/Frame を直接調整 or WorldGen.Convert 併用
- Terraformer 強化（ai1==1）と Dust 演出スケールの分岐
- Projectile 側の extraUpdates と shootSpeed の体感調整（例: 銀弾相当の速度感）