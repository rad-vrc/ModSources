Per-file summary: ExampleMod/Common/Systems (tML 1.4.4)

1) ChestItemWorldGen.cs
- PostWorldGenで特定チェスト（Frozen）にアイテムを追加。Main.chest走査→TileType/FrameXで種別判定→空スロットへ循環 or ランダムに挿入。最大数とランダムスキップで調整。

2) DownedBossSystem.cs
- ボス撃破フラグの保存/読み込み/同期（TagCompound＋WriteFlags/ReadFlags）。ClearWorldで初期化。NetSend/Receiveは順序一致必須。

3) ExampleBiomeTileCount.cs
- TileCountsAvailable(ReadOnlySpan<int>)でModタイル数を受け取りカウントを保持。

4) ExampleGameTipsSystem.cs
- ModifyGameTipVisibilityでロードTipsの可視性制御。Vanilla GameTipIDをHide、他ModのtipもFullName等で探索してHide可能。自ModTipはローカライズ定義。

5) ExampleTownPetSystem.cs
- 町ペット購入（使用）フラグの保存/同期。TagCompound＋WriteFlags/ReadFlags、ClearWorldで初期化。

6) ExampleWorldGenHookingSystem.cs
- WorldGen.VanillaGenPasses名指定でPassLegacyを IL/Detour。Loadで登録。ILContextにILCursorでDelegate注入、Detourではorig前後でログ。匿名メソッド用の手法。

7) ExampleWorldHeaderSystem.cs
- SaveWorldHeaderにヘッダー情報追加。On_UIWorldListItem.DrawSelfで世界選択UIにオーバーレイ表示（特定メニュー時、ヘッダーフラグ有時）。ExampleWorldHeaderPlayer.OnEnterWorldで生成時Modバージョンをチェックし通知。

8) KeybindSystem.cs
- ModKeybind登録/Unloadでnull化。ローカライズキーは Mods.{Mod}.Keybind.{Name}。

9) ModIntegrationsSystem.cs
- PostSetupContentで外部Mod連携（例: BossChecklist）。TryGetModとVersionチェック→Call("LogBoss", ...)で登録。collectiblesやcustomPortrait委譲。DownedBossSystem参照。

10) RubbleWorldGen.cs
- ModifyWorldGenTasksで"Piles"後にGenPass挿入。ApplyPassでスポーン周辺に限定してランダムな瓦礫タイルをTry-Until-Successで配置。

11) SimpleDataAtParticularLocations.cs
- 位置データの小規模疎データ保存例。PosData<byte>[]をTagCompoundで保存/復元。PostWorldGenで中央列にデータ生成→PreUpdateWorldで近傍検索しタイル色を塗る。

12) StatueWorldGen.cs
- On_WorldGen.SetupStatueList detourでGenVars.statueList拡張、必要ならStatuesWithTrapsにインデックス追加。

13) TownNPCRespawnSystem.cs
- Town NPC再出現アンロックフラグの保存/同期。既存ワールド救済としてNPC存在時にtrueへ。ClearWorld/NetSend/Receive実装。

14) TravelingMerchantSystem.cs
- 行商人システムの世界データ管理。PreUpdateWorldでUpdate呼出し。shopItems/ spawnTime を保存/復元、WorldDataパケットでショップ中身を同期（ItemIO.Send/Receive）。
