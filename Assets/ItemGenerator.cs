using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
    //carPrefabを入れる
    public GameObject carPrefab;

    //coinPrefabを入れる
    public GameObject coinPrefab;

    //conePrefabを入れる
    public GameObject conePrefab;

    //スタート地点
    private int startPos = -160;

    //ゴール地点
    private int goalPos = 120;

    //x方向の移動範囲
    private float posRange = 3.4f;

    // Use this for initialization
    void Start()
    {
        
        for (int i = startPos; i < goalPos; i += 15)
        {
            int num = Random.Range(0, 10);
            if (num <= 1)
            {
                
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;    
                    cone.transform.position = new Vector3(4 * j, this.transform.position.y, i);
                }
            }
            else
            {
                //レーンごとにアイテムを生成
                for (int j = -1; j < 2; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くz座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置、30%車配置、10%何もなし
                    if (item >= 1 && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(j * posRange, this.transform.position.y, i + offsetZ);
                    }
                    else if (item >= 7 && item <= 9)
                    {
                        //車生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(j * posRange, this.transform.position.y, i + offsetZ);
                    }
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
    }

    //void OnBecameInvisible()
    //{
        //Destroy(this.gameObject);
    //}
}
