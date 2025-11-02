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
   
    /// <summary>
    /// 体力最大値
    /// </summary>
    public float MaxHealth = 100f;
    [SerializeField]
    private float currentHealth;
    /// <summary>
    /// 外部から現在の体力を取得させるためのプロパティ
    /// </summary>
    public float GetCurrentHealth
    {
        get { return currentHealth; }
    }
    /// <summary>
    /// 疲労回復速度
    /// </summary>
    public float RecoveryRate = 1f;

    /// <summary>
    /// 疲れやすさ
    /// </summary>
    public float FatigueRate = 1f;

    /// <summary>
    /// コロニストの年齢
    /// </summary>
    public int ColonistAge = 20;

    /// <summary>
    /// 年齢によって色を変更する
    /// </summary>
    public Material YoungMaterial;
    public Material NormalMaterial;
    public Material OldMaterial;

    /// <summary>
    /// Colonistの３Dモデル表示部分
    /// </summary>
    private MeshRenderer[] colonistMeshRenderers = new MeshRenderer[2];

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
        currentHealth = MaxHealth;
        //現在の体力をMaxにする  

        //3D表示部分を取得
        colonistMeshRenderers = GetComponentsInChildren<MeshRenderer>();

        //コロニストの年齢を決める
        ColonistAge = Random.Range(18, 70);

        //コロニストの年齢が20歳まで
        if (ColonistAge < 20)
        {
            RecoveryRate = 2f;
            FatigueRate = 0.5f;
            MoveSpeed = 5f;

            //foreachは配列に対してすべての要素に変更を加えたい時に使う
            foreach(var renderer in colonistMeshRenderers)
            {
                renderer.material = YoungMaterial;


            }
        }
        else if (ColonistAge < 40)
        {
            RecoveryRate = 1f;
            FatigueRate = 1f;
            MoveSpeed = 2f;
            foreach (var renderer in colonistMeshRenderers)
            {
                renderer.material = NormalMaterial;

            }
        }
        else //４０歳より上
        {
            RecoveryRate = 0.5f;
            FatigueRate = 2f;
            MoveSpeed = 1f;
            foreach (var renderer in colonistMeshRenderers)
            {
                renderer.material = OldMaterial;

            }
        }
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

                //現在の体力をじわじわ回復させる
                currentHealth += RecoveryRate*2f * Time.deltaTime;
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

                //現在の体力値から1秒間で５ポイント体力をへらす
                currentHealth -= FatigueRate * 5f * Time.deltaTime;
                //現在の体力が20ポイント下回ったら
                if(currentHealth <= 20f)
                {
                    //回復するために寝る状態にする
                    State = ColonistState.Sleep;
                }
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

                //現在の体力を1秒間に10ポイント減らす
                currentHealth -=FatigueRate* 10f * Time.deltaTime;

                //現在の体力が20ポイントより少なくなったら
                if(currentHealth <= 20f)
                {
                    //体力を回復させるためにSleepにする
                    State = ColonistState.Sleep;
                }

                if(timer <= 0f)
                {
                    
                    State = ColonistState.Sleep;
                    //timerを10秒から15秒に設定
                    timer = Random.Range(10f,15f);
                    //stateをColonistState.Sleepにする
                    //timer10秒
                    
                        State = ColonistState.Idle;
                    

                }
                break;
            case ColonistState.Sleep://就寝

                //1秒間に8ポイント回復させる
                currentHealth +=RecoveryRate* 8f * Time.deltaTime;
                //もしコロニストの体力が完全に回復したら
                if (currentHealth >= MaxHealth)
                {
                    State = ColonistState.Idle;
                    timer = Random.Range(1f,5f);


                }

                break;
        }
    }
}
