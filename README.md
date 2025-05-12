# CanKicking

缶を飛ばしてゴールを目指すゲーム

## 実装要素
* ゲームのメイン
  * 缶を引っ張る
  * 指を離す
  * 飛ぶ
  * 停止する
* ステージセレクト
* タイトル

## クラス構成

* Installer
    * DIコンテナの役割を背負う
    * クラス名は最後にInstallerをつける

以降のクラス間ではインターフェースを利用して直接依存しないようにする

* Domain
    * UseCase
      * 主なゲームの動作を記述する
      * クラス名は最後にCaseをつける
      * 関心
        * Presenter
        * Repository
    * Entity
      * UseCaseからのみ参照される
      * 参照透過な関数を持つ
      * BurstCompileを利用し処理を高速化する
      * クラス名は最後にEntityをつける
      * 関心
        * なし
* Adapter
  * Presenter
    * UseCaseとViewをつなぐ中間層
    * Viewが持つ生のUnity要素をUseCaseが使いやすい形で公開する
    * クラス名は最後にPresenterをつける
    * 関心
      * View
  * Repository
    * UseCaseとDataStoreをつなぐ中間層
    * ゲーム中のデータはここで持つ
    * クラス名は最後にRepositoryをつける
    * 関心
      * DataStore
  * View
    * UnityのMonoBehaviourのコンポーネントを持ち、表示を司る
    * クラス名は最後にViewをつける
    * 関心
      * なし
  * DataStore
    * データ保存用の構造をもつ
    * クラス名は最後にDataStoreをつける
    * 関心
      * なし
