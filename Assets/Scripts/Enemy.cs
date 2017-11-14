using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// 敵 クラス 
public class Enemy : Token
{
    /// 管理 オブジェクト 
    public static TokenMgr < Enemy > parent = null;
    // プレハブ から 敵 を 生成
    public static Enemy Add( List < Vec2D > path)
    {
        Enemy e = parent.Add( 0, 0);
        if( e == null)
        {
            return null;
        }
        e.Init(path);
        return e;
    }
    // アニメーション 用 の スプライト 
    public Sprite spr0;
    public Sprite spr1;

    // アニメーション タイマー
    int tAnim = 0;

    // 速度 パラメータ 
    float _speed = 0; // 速度 
    float _tSpeed = 0; // 補完 値( 0. 0〜 100. 0) 
    // 経路 座標 の リスト 
    List < Vec2D > _path;
    // 経路 の 現在 の 番号
    int _pathIdx;
    // チップ 座標 
    Vec2D _prev; // 1 つ 前
    Vec2D _next; // 1 つ 先

    /// 初期化
    public void Init( List < Vec2D > path)
    { 
        // 経路 を コピー
        _path = path;
        _pathIdx = 0;
        // 移動 速度 
        _speed = 2.0f;
        _tSpeed = 0;

        //移動先を更新
        MoveNext();
        // _prev に 反映 する
        _prev.Copy(_next);
        // 一つ 上 に ずらす 
        _prev.y += Field.GetChipSize();
        // 一度 座標 を 更新 し て おく
        FixedUpdate();
    }

    // アニメーション 更新 
    void FixedUpdate()
    {
        tAnim++;
        if (tAnim % 32 < 16)
        {
            SetSprite(spr0);
        }else{
            SetSprite(spr1);
        }
        // 速度 タイマー 更新
        _tSpeed += _speed;
        if(_tSpeed >= 200.0f) {
        // 移動 先 を 次に 進める
        _tSpeed -= 200.0f;
        MoveNext();
        }
        // ⑤ 速度 タイマー に 対応 する 位置 に 線形 補 間 で 移動 する
        X = Mathf. Lerp(_prev.x, _next.x, _tSpeed / 200.0f);
        Y = Mathf. Lerp(_prev.y, _next.y, _tSpeed / 200.0f);

    }

    void MoveNext()
    {
        if (_pathIdx >= _path.Count) 
        {
        // ⑨ ゴール に たどりつい た
            _tSpeed = 200.0f;
            return;
        }
        // ⑧ 移動 先 を 移動 元 に コピー する
        _prev.Copy(_next);
        // ⑦ チップ 座標 を 取り出す
        Vec2D v = _path[_pathIdx];
        _next.x = Field.ToWorldX( v.X);
        _next.y = Field.ToWorldY( v.Y);
        // パス 番号 を 進める
        _pathIdx ++;
    }



}
