using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColonistManager : MonoBehaviour
{
    /// <summary>
    /// []は配列といい、一つの変数の中で複数のColonistAIを管理できる
    /// </summary>
    public ColonistAI[] Colonists;

    /// <summary>
    /// コロニストのPrefab
    /// </summary>
    public GameObject ColonistPrefab;

    /// <summary>
    /// コロニストの数
    /// </summary>
    public int ColonistCount = 3;

    /// <summary>
    /// UnityEditorからColonistUIManager達を参照
    /// </summary>
    public ColonistUIManager[] ColonistUIManagers;

    /// <summary>
    /// 採掘場の位置
    /// </summary>
    public Transform MinePoint;

    /// <summary>
    /// 倉庫の位置
    /// </summary>
    public Transform WarehousePoint;

    /// <summary>
    /// 市場の位置
    /// </summary>
    public Transform MarketPoint;

    /// <summary>
    /// 食事場の位置
    /// </summary>
    public Transform BakeryPoint;

    /// <summary>
    /// 住人につけられる名前
    /// </summary>
    private string[] possibleNames =
    {
        "Taro",
        "Hanako",
        "Ken",
        "Mina",
        "Aki"
    };

    private List<string> usedName = new List<string>();

    private string GetUniqueName()
    {
        string name;
        //doの中に書かれている処理をWhileの条件の間、繰り返す
        do
        {
            name = possibleNames[Random.Range(0, possibleNames.Length)];
        } while (usedName.Contains(name)
        && usedName.Count < possibleNames.Length);
        //Listにランダムに指定された名前を追加する
        usedName.Add(name);
        //stringのメソッド（）の場合はstringの何かを返してあげないといけない
        return name;
    }

    private void Start()
    {
        //コロニストの数分のColonistAIを準備
        Colonists = new ColonistAI[ColonistCount];

        //コロニストの数分、処理を実行する
        for(int i = 0; i < ColonistCount; i++)
        {
            //登場する位置を決める(１体目は原点、2体目はX軸正の方向に２ｍ...
            Vector3 position = new Vector3(i * 2, 1, 0);
            //GameObjectをScene内に生成
            GameObject instantiateObject= 
                Instantiate(ColonistPrefab,position,Quaternion.identity);

            //一斉命令用のColonistAIを生成したGameObjectから取得
            Colonists[i] = instantiateObject.GetComponent<ColonistAI>();

            //生成された住人に名前をつける
            Colonists[i].gameObject.name = GetUniqueName();

            //コロニストに採掘場への場所を教える
            Colonists[i].MinePoint = MinePoint.position;

            //コロニストに採掘場の機能を教える
            Colonists[i].MineSite = MinePoint.GetComponent<MineSite>();

            //コロニストに倉庫の場所を教える
            Colonists[i].Warehouse = WarehousePoint;

            //コロニストに市場の場所を教える
            Colonists[i].MarketPosition = MarketPoint;

            //コロニストに食事場の場所を教える
            Colonists[i].BakeryPosition = BakeryPoint;

            //コロニストにBakeryの状態を教える
            Colonists[i].Bakery = BakeryPoint.GetComponent<Bakery>();



            //コロニストのUI表示用のマネージャーに生成されたColonistAIをセット
            ColonistUIManagers[i].SetColonistAI(Colonists[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //キーボードのSpaceキーが押されたら
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //for文は（初期値、初期値が指定の値になるまで、初期値を増減させる）までの回数処理を行う
            //iはコロニストの数
            for(int i = 0; i < Colonists.Length; i++)
            {
                Colonists[i].State = ColonistAI.ColonistState.Mine;
            }
        }
    }
}
