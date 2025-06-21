# CanKicking

缶を引っ張って飛ばし、ゴールを目指すゲーム

## todo

* 仕様変更

プレイヤーの動作を変更する。
物理的な挙動で動作していたが、
壁に対する跳ね返りの挙動が不安定でゲーム体験が悪い気がする。

* AddForceで缶を飛ばす処理は変わらず
* 空中では、缶の物理的な回転を行わない
* 着地時に缶の物理的な回転を再開する
* 物理的な回転をしない間一定の速さで回転する
* 壁に衝突した際、回転を反転する

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
