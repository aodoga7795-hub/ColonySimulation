using UnityEngine;

public class ColonistUIManager : MonoBehaviour
{
    private ColonistHealthUI colonistHealthUI;

    private ColonistStatusUI colonistStatusUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// AwakeはStart（）の実行される前に、実行される初期化用のメソッド
    /// </summary>
    void Awake()
    {
        //GetComponentInChildrenはヒエラルキーWindowの
        //このコンポーネントが追加されたGameObjectの階層下から取得する
        colonistHealthUI = GetComponentInChildren<ColonistHealthUI>();
        colonistStatusUI = GetComponentInChildren<ColonistStatusUI>();
    }



   //ColonistUIManagerが持っている２つのコンポーネントにColonistAIを渡してあげたい
   //小かっこの中身は引数と言って、引数に渡されたものを、この処理の中で使うことができる
   public void SetColonistAI(ColonistAI colonistAI)
    {
        colonistHealthUI.ColonistAI = colonistAI;
        colonistStatusUI.ColonistAI = colonistAI;

    }
   
}
