using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{

    // 出現 タイマー
    int _tAppear = 0;
    // パス 
    List < Vec2D > _path;

    // Use this for initialization
   void Start ()
    {
		// 敵 管理 を 生成
        Enemy.parent = new TokenMgr < Enemy >("Enemy", 128);
        // マップ 管理 を 生成
        //プレハブ を 取得 
        GameObject prefab = null;
        prefab = Util.GetPrefab( prefab, "Field");
        // インスタンス 生成
        Field field = Field.CreateInstance2 < Field >( prefab, 0, 0);
        // マップ 読み込み
        field. Load();
        // パス を 取得
        _path = field.Path;



    }
	
	// Update is called once per frame
	void Update ()
    {
		_tAppear++;
        if (_tAppear % 500 == 0)
            {
            // 敵 を 生成 する テスト
            Enemy. Add(_path);
        }
    }
}
