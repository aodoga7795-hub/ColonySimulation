using UnityEngine;

public class ColonistAI : MonoBehaviour
{
    /// <summary>
    /// Enum型で宣言したコロニストの状態
    /// </summary>
    public enum ColonistState
    {
        Idle,
        Move,
        Mine,
        Sleep
    }
    public float MoveSpeed = 2.0f;
    private Vector3 targetPosition = new Vector3(2, 0, 2);

    public ColonistState State;
    /// <summary>
    /// コロニストの状態を変更するためのタイマー
    /// [SerializeField]のようなものを属性(Attribute)という
    /// </summary>
    [SerializeField]
    private float timer = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //コロニストの状態をIdleからはじめる
        State = ColonistState.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        //1フレームにかかった時間をTimerから減算していく
        timer -= Time.deltaTime;
        //小かっこの中の変数を使って処理を分岐(switch)させる
        switch (State)
        {
            case ColonistState.Idle://待機
                //もしタイマーが０秒を下回ったら
                if (timer <= 0f)
                {
                    //コロニストの状態を動くという状態に変更
                    State = ColonistState.Move;
                    //ターゲットのポジションを決めてあげる
                    targetPosition = new Vector3(
           Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                    timer = 2f;
                }
                break;
            case ColonistState.Move://移動
                transform.position = Vector3.MoveTowards(
            transform.position, targetPosition, MoveSpeed * Time.deltaTime);

                //if文は、もし小括弧内の条件だったら、中括弧内の処理を行う
                //自分の位置とターゲットの位置が10㎝より近くなったら
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    //次の行動を行う
                    State = ColonistState.Mine;
                    //掘削時間は1秒から5秒までのランダム
                    timer = Random.Range(1f, 5f);
                  

                }
                break;
            case ColonistState.Mine://発掘
                //仮で採掘アニメーション再生の代わりにログを出力
                Debug.Log("Colonist is mining!");
                //毎フレーム回転させ続ける
                transform.Rotate(Vector3.up * 30f * Time.deltaTime);
                if(timer <= 0f)
                {
                    
                    State = ColonistState.Sleep;
                    //timerを10秒から15秒に設定
                    timer = Random.Range(10f,15f);
                    //stateをColonistState.Sleepにする
                    //timer10秒


                }
                break;
            case ColonistState.Sleep://就寝
                if (timer <= 0f)
                {
                    State = ColonistState.Idle;
                    timer = Random.Range(1f,5f);


                }

                break;
        }
    }
}
