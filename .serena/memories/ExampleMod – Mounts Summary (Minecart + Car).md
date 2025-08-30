# ExampleMod – Mounts Summary (Minecart + Car)

対象: ExampleMod/Content/Mounts/ExampleMinecartMount.cs, ExampleMount.cs の学習要点。

— ExampleMinecartMount（ModMount） —
- SetStaticDefaults
  - Minecart種別指定: MountID.Sets.Cart[Type] = true; MountID.Sets.FacePlayersVelocity[Type] = true。
  - Mount.SetAsMinecart(MountData, BuffType, frontTexture) で共通プロパティ一括設定。
  - 追加調整: spawnDust, delegations.MinecartDust/LandingSound/BumperSound（DelegateMethods.Minecart.*）。
  - Minecart Upgrade Kit 用の上書き値: MinecartUpgradeRunSpeed/DashSpeed/Acceleration。
- UpdateEffects(Player)
  - ダイヤモンドカート風の視覚: プレイヤー速度・相対位置からDust(91)を NewDustPerfect、GameShaders.Armor.GetSecondaryShader(player.cMinecart) を適用。

— ExampleMount（車 + 風船, ModMount） —
- 専用データ
  - player.mount._mountSpecificData を CarSpecificData で使用（風船の個数・回転配列）。ゲームプレイ影響に使うならModPlayer同期が必要、本例は視覚のみ。
- SetStaticDefaults（挙動/見た目/フレーム/オフセット）
  - Movement: jumpHeight/acceleration/jumpSpeed/blockExtraJumps/constantJump/heightBoost/fallDamage/runSpeed/dashSpeed/flightTimeMax。
  - Misc: fatigueMax/buff、spawnDust。
  - Frame/Offsets: totalFrames, playerYOffsets, xOffset, yOffset, playerHeadOffset, bodyFrame、standing/running/flying/inAir/idle/swim の各 FrameCount/Delay/Start 設定。
  - 非dedサーバ時のテクスチャ寸法設定: textureWidth/textureHeight。
  - 風船テクスチャ読み込み: Mod.Assets.Request<Texture2D>("Content/Items/Armor/SimpleAccessory_Balloon")。
- UpdateEffects(Player)
  - 風船の風抵抗: player.velocity.X に比例して rotations[i] をゆらし、AngleLerp で緩和。
  - 高速移動時にSparkle Dustをプレイヤー矩形で散布。
- SetMount(Player, ref bool skipDust)
  - _mountSpecificData 初期化。Mount標準のDustをバイパスし自前の円形Dust演出生成、skipDust = true。
- Draw(...)
  - drawType == 0（_Backの直前）に風船を追加描画。DateTime.Now.Millisecond から簡易アニメーション。DrawData を playerDrawData に積む。
  - true を返して通常描画は継続。

設計パターン/注意:
- Minecart は SetAsMinecart で土台→必要箇所のみ追加調整。MinecartUpgrade* はUpgrade Kit使用時の上書き値。
- MountData.* の各Frame・Offsetはプレイヤーの表示/当たりと密接。dedサーバではテクスチャ前提コードを避けるガード必須。
- _mountSpecificData は実体（Playerごと）に紐づく視覚用ストレージに最適。ネット同期が必要なゲームプレイ値はModPlayerへ。
