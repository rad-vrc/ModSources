# Items 学習 (8-of-10) Weapons

対象: ExampleMod/Content/Items/Weapons/*
tModLoader: 1.4.4

全体パターン
- 近接投げ/保持系: MeleeNoSpeed + channel + noUseGraphic/noMelee + held projectile（Flail/AdvancedFlail/CustomSwingSword/JoustingLance/LastPrism/Yoyo）
- Ranged: DefaultToRangedWeapon or明示設定（useAmmo/UseSound/shoot/shootSpeed）＋ HoldoutOffset/ModifyShootStats/Shootの活用
- Staff/魔法: DefaultToStaff, Item.staff[Type]=true, ModifyManaCost 等
- 弾/消費: CanChooseAmmo/CanConsumeAmmo/OnConsumeAmmo/ModifyShootStats（SpecificAmmoGun, Minigun）
- ユニーク: MultiplePrefixCategory、ModifiedProjectiles（GlobalProjectile設定）、HitModifiersShowcase（ダメージ計算の可視化）

個別要点
- ExampleAdvancedFlail / ExampleFlail
  - ToolTipDamageMultiplier=2f（近接直撃2倍表記）、MeleeNoSpeed, channel, noUseGraphic/noMelee, shoot=専用フレイルProjectile
  - Advanced は Projectile 側で動的調整全量（Ball O’ Hurt 相当）

- ExampleCloneWeapon
  - CloneDefaults(Meowmere)後、shootをExampleCloneProjectileへ差し替え、damage×2, shootSpeed×1.25

- ExampleCustomAmmoGun
  - useAmmo=ModItemのカスタム弾、shoot=ExampleHomingProjectile（武器＋弾のダメージ/KB合算）

- ExampleCustomDamageWeapon
  - DamageType=ExampleDamageClass、魔法/近接Prefix共存の注記（mana無だと魔法prefix制限）

- ExampleCustomResourceWeapon
  - 独自リソース（ExampleResourcePlayer）を消費。ModifyTooltipsでコスト表示、CanUseItem/UseItem で減算

- ExampleCustomSwingSword
  - 連撃コンボ（attackType, comboExpireTimer）、noUseGraphic/noMelee、shoot=held projectile、Shoot で ai[0] に攻撃パターン渡し

- ExampleExplosive
  - Demolitionist判定セット/Research=99、consumable爆弾、shoot=ExampleExplosive Projectile

- ExampleGun
  - Ranged基本実装と多彩なサンプル（弾種置換、扇状散弾、銃口オフセット、3点バースト、同時二種発射、タイプランダム化）

- ExampleJavelin
  - consumable投擲、noUseGraphic/noMelee、shoot=JavelinProjectile

- ExampleJoustingLance
  - DefaultToSpear(held projectile, shootSpeed param注意)、MeleeNoSpeed, channel, StopAnimationOnHurt

- ExampleLastPrism
  - CloneDefaults(LastPrism)→shoot=Holdout、色変更、CanUseItem で多重発射抑止

- ExampleMagicWeapon
  - DefaultToStaff(ProjectileID.BlackBolt, 初期時間/速度)、SetWeaponValues、低HPで ModifyManaCost mult*=0.5

- ExampleMinigun
  - DefaultToRangedWeapon、CanConsumeAmmo 38%節約、NeedsAmmo で ExampleItem>=10 なら弾無し射撃、散布（ModifyShootStats）

- ExampleModifiedProjectilesItem
  - Shoot→Projectile.NewProjectile→取得→GlobalProjectile(ExampleProjectileModifications)に Trail/Flags を設定し false return

- ExampleMultiplePrefixCategoryWeapon
  - Rangedだが MeleePrefix/ MagicPrefix を true、RangedPrefix を false

- ExampleRocketLauncher
  - SpecificLauncherAmmoProjectileFallback=RocketLauncher、RocketIII→Meowmere の置換デモ、DefaultToRangedWeapon(RocketI/Rocket)

- ExampleShootingSword
  - Star Wrath系：頭上から3発落下（ceilingLimit/heading調整）、shootSpeed 影響、return false

- ExampleShortsword
  - Rapier style、MeleeNoSpeed、noUseGraphic/noMelee、shoot=ShortswordProjectile

- ExampleShotgun
  - 8発、±15度散布、速度ランダム縮小、return false、HoldoutOffset 調整

- ExampleSpear
  - Spears/SkipsInitialUseSound セット、ownedProjectileCountsで1本制限、UseItem で音再生

- ExampleSpecificAmmoGun
  - ツールチップに確率/ボーナス反映。CursedBullet禁止（CanChooseAmmo）。3連射で各弾ごとに節約確率変更（CanConsumeAmmo）。OnConsumeAmmoでダメージ増フラグ→次弾で加算

- ExampleStaff
  - Item.staff=true、DefaultToStaff(SparklingBall, 16,25,12)、UseSound上書き、SetWeaponValues/SetShopValues

- ExampleSwingingEnergySword
  - Excalibur系、shoot=剣プロジェクタイル、Item.shootsEveryUse=true、ShootでGetAdjustedItemScaleを渡す＋MP同期

- ExampleSword
  - 基本近接：Melee, Dust 演出, OnHitNPC で OnFire 付与、Rarity=ExampleModRarity

- ExampleWhip/WhipAdvanced
  - DefaultToWhip or 明示設定。SummonMeleeSpeed/chargeable（channel）/noMelee/noUseGraphic。TooltipにTag値表示

- ExampleYoyo
  - Sets.Yoyo/GamepadExtraRange/SmartQuickReach、MeleeNoSpeed, channel, AllowPrefix blacklist（弱体化prefix再抽選）

- HitModifiersShowcase
  - 8モード切替（右クリック/Net同期）、DamageVariation や Knockback/CritDamage/ArmorPenetration/ScalingArmorPenetration/防御デバフ/回避バフの実演。PvP対応分岐。

API/テクニック
- DefaultToRangedWeapon/DefaultToStaff/DefaultToWhip/DefaultToSpear/SetWeaponValues/SetShopValues
- Ammoフック: CanChooseAmmo/CanConsumeAmmo/OnConsumeAmmo/NeedsAmmo/ModifyShootStats
- 持続発射: channel + held projectile + CanUseItem 重複抑止
- 弾/投射: Projectile.NewProjectile vs return false パターン
- 表現: HoldoutOffset, GetAlpha, MeleeEffects, ModifyTooltips（色循環）

落とし穴/Tips
- MeleeNoSpeed を使う held projectile は攻撃速度影響を避ける
- 連射/3点バースト/散弾などは UseTime/UseAnimation/ReuseDelay の関係を正しく設計
- 弾節約や弾種制御は CanConsumeAmmo/CanChooseAmmo/OnConsumeAmmo の役割を区別
- LastPrism/CustomSwing は ownedProjectileCounts とコンボ管理で誤多重発射を抑止
