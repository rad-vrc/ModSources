ExampleMod/Content/TileEntities フォルダ学習まとめ:

- AdvancedPylonTileEntity.cs (TEModdedPylon)
  - 目的: ランダム間欠動作する改造パイロン。isActive を同期管理。
  - API: NetSend/NetReceive で WriteFlags/ReadFlags、Update で1/180でトグル→サーバなら TileEntitySharing 送信。

- BasicTileEntity.cs（ModTileEntity + ModTile + ModItem セット）
  - 目的: 雨で水が溜まり100%で水バケツを排出する樽。ネット同期と描画/操作連動の完全例。
  - 主要API:
    - ModTileEntity: SaveData/LoadData、NetSend/NetReceive、Update（雨時加算）、IsTileValidForEntity。
    - 水量: WaterFillLevel(Clamp+10%間隔同期)、WaterFillPercentage、WaterFillStage。
    - ModTile: SetStaticDefaults(設置/保護/マップエントリ)、KillMultiTileでTE Kill、SetDrawPositionsでframeY調整、MapHoverText/MouseOver/RightClick/ KillTile（サーバ側でアイテム生成）。
    - ネット: TileEntitySharing、MessageID.TileManipulation 使用。
    - ModItem: DefaultToPlaceableTile、レシピ。

- SimplePylonTileEntity.cs（TEModdedPylon）
  - 目的: 親の標準パイロン実装そのまま使用（Modプロパティ設定目的）。sealed クラス。
