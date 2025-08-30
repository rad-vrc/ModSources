UIに続いて、ExampleMod/Commonの6ファイルを要点学習:

1) ExampleCameraModifier.cs
- 目的: 任意座標へのパン→保持→復帰のカメラモディファイア(ICameraModifier)
- キーAPI: ICameraModifier.Update(ref CameraInfo), CameraInfo.CameraPosition, Utils.GetLerpValue/Remap, Vector2.Lerp
- 状態: framesToLast/framesElapsed, Finished, UniqueIdentity で同一ID多重抑止
- 注意: 非アクティブ/ポーズ時は進行停止(Main.gameInactive/Main.gamePaused)

2) ExampleConditions.cs
- 目的: 再利用するConditionを集中管理
- キーAPI: Condition(labelKey, Func<bool>), Main.LocalPlayer.InModBiome<T>(), DownedBossSystem フラグ
- 注意: フィールド公開で他Modから参照容易、キーはLocalization前提

3) ExampleMapLayer.cs (+ ExampleMapLayerSystem, ExampleItemMapLayer)
- 目的: マップレイヤーでダンジョンアイコン/落ちてる特定アイテム表示
- キーAPI: ModMapLayer.GetDefaultPosition/Draw/GetModdedConstraints, MapOverlayDrawContext.Draw(..., SpriteFrame, Alignment), Language.GetTextValue
- 同期: ModSystem.NetSend/NetReceiveでMain.dungeonX/Y同期
- 注意: タイル座標で渡す(ワールド座標/16)、Main.mapStyleチェック、ModContent.GetInstanceで相対順制御

4) ExamplePlayerDrawLayer.cs
- 目的: プレイヤー頭部レイヤーの子としてアイテムテクスチャ描画
- キーAPI: PlayerDrawLayer(IsHeadLayer, GetDefaultVisibility, GetDefaultPosition, Draw), PlayerDrawLayers.Head, DrawData, drawInfo.DrawDataCache
- 可視条件: HeldItemがExampleItem

5) ExampleRecipeCallbacks.cs
- 目的: レシピコールバック例(消費調整/クラフト後処理)
- キーAPI: Recipe.ConsumeItemCallback署名相当(DontConsumeChain), OnCraftCallbacks(RandomlySpawnFireworks), Projectile.NewProjectile, Player.QuickSpawnItem
- 注意: isDecrafting無視の例、Main.rand判定

6) ManualMusicRegistrationExample.cs
- 目的: 手動の音楽登録(ILoadable)
- キーAPI: ILoadable.Load/Unload, MusicLoader.AddMusic(mod, pathWithoutExtension)
- 注意: Modインスタンスを渡す、拡張子をコードに含めない
