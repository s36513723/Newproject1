using UnityEngine;
using System.Collections;
using System.Collections.Generic;



/// フィールド 情報 の 管理 
public class Field : Token
{
    //何もない
    public const int CHIP_NONE = 0;
    // 開始 地点 
    public const int CHIP_PATH_START = 26;
    //パス(座標リスト)
    List<Vec2D> _path;
    public List<Vec2D> Path
    {
        get{
            return _path;
        }
    }
    /// チップ 1 マス の サイズ を 取得 する
    public static float GetChipSize()
    {
        var spr = GetChipSprite();
        return spr.bounds.size.y;
    }

    /// チップ サイズ の 基準 と なる スプライト を 取得 する 
    static Sprite GetChipSprite()
    {
        return Util.GetSprite("Levels/tileset","tileset_0");
    }
    /// チップ 座標 を ワールド の X 座標 を 取得 する 
    public static float ToWorldX(int i) {
        // カメラビュー の 左下 の 座標 を 取得
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        var spr = GetChipSprite();
        var sprW = spr.bounds.size.x;
        return min.x + (sprW * i) + sprW/ 2;
    }

    /// チップ座標をワールドのY座標を取得する
    public static float ToWorldY( int j){
        // カメラビューの右上の座標を取得する
        Vector2 max = Camera. main. ViewportToWorldPoint( new Vector2( 1, 1));
        var spr = GetChipSprite();
        var sprH = spr. bounds. size. y;
        // Unityでは上下逆になるので、逆さにして変換
        return max.y - (sprH * j) - sprH/ 2;
    }

    //void Start()
    public void Load()
    {
        // マップ 読み込み
        TMXLoader tmx = new TMXLoader();
        tmx.Load("Levels/map");
        // 経路 レイヤー を 取得 
        

    
        Layer2D lPath = tmx.GetLayer("path");
       
        // 開始 地点 を 検索
        Vec2D pos = lPath.Search(CHIP_PATH_START);
        // 座標 リスト を 作成 
        _path = new List<Vec2D>();
        // 開始 座標 を 座標 リスト に 登録
        _path.Add(new Vec2D(pos.X, pos.Y));
        // 通路 を ふさぐ
        lPath.Set(pos.X, pos.Y, CHIP_NONE);
        // 座標 リスト を 作成 
        CreatePath(lPath, pos.X, pos.Y, _path); 
        
    }    

    void CreatePath(Layer2D layer, int x, int y, List <Vec2D> path)
    { 
         // 左・上・右・下 を 調べる
         int[] xTbl = {-1, 0, 1, 0};
         int[] yTbl = {0, -1, 0, 1};
         for (var i = 0; i < xTbl.Length; i++)
         {
             int x2 = x + xTbl[i];
             int y2 = y + yTbl[i];
             int val = layer.Get(x2, y2);
             if (val > CHIP_NONE)
             {
                 // 経路 を 発見
                 // 経路 を ふさぐ
                 layer.Set(x2, y2, CHIP_NONE);
                 // 座標 を 追加
                 path.Add(new Vec2D(x2, y2));
                 // パス 生成 を 再帰 呼び出し
                 CreatePath(layer, x2, y2, path);
             }
         }
     }
}


