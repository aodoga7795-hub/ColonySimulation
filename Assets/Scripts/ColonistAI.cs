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
        Sleep,
        Carry,//運ぶ
        Rest, //休憩
        Dead //死亡
        
    }
    public float MoveSpeed = 2.0f;
    private Vector3 targetPosition = new Vector3(2, 0, 2);

    /// <summary>
    /// 採掘場所の位置
    /// </summary>
    public Vector3 MinePoint;



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

    /// <summary>
    /// 採掘スキルで高いほど速い
    /// </summary>
    [Range(0.5f,3f)]
    public float MiningSkill = 1f;

    //採掘量
    public int MinedResource = 0;


    /// <summary>
    /// 空腹度
    /// </summary>
    private float hunger = 100f;

    /// <summary>
    /// ストレス
    /// </summary>
    private float stress = 0f;
    
    /// <summary>
    /// 生きているかの判定
    /// </summary>
    public bool IsAlive
    {
        ///Boolは真偽の判定になるので、条件を作ることができる
        ///今回は体力があって、空腹度も飢えていない状態とする
        ///||は日本語でいうと、”または”です
       
        get { return currentHealth > 0 || hunger > 0; }
    }

    /// <summary>
    /// 倉庫
    /// </summary>
    public Transform Warehouse;

    /// <summary>
    /// 市場の位置
    /// </summary>
    public Transform MarketPosition;




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
            MiningSkill = 3f;

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
            MiningSkill = 2f;
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
            MiningSkill = 1f;
            foreach (var renderer in colonistMeshRenderers)
            {
                renderer.material = OldMaterial;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //!は否定の意味
        //生存していなかったら
        if (!IsAlive)
        {
            State = ColonistState.Dead;
            Debug.Log($"死亡しました");
            return;

        }
        //1フレームにかかった時間をTimerから減算していく
        timer -= Time.deltaTime;

        //1秒間に２ポイントずつ空腹になっていく
        hunger -= 2f * Time.deltaTime;

        //1秒間に１ポイントずつストレスがかかっていく
        stress += 1f + Time.deltaTime;


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
                    targetPosition = MinePoint;
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
                //1秒間にMiningSkillが３の人は360度一回転できる
                transform.Rotate(Vector3.up * 120f *MiningSkill* Time.deltaTime);

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
                    int mined = Mathf.RoundToInt(10* MiningSkill);
                    MinedResource += mined;
                    Debug.Log($"採掘完了{mined}(合計{MinedResource})");
                    
                    //State = ColonistState.Sleep;
                    //timerを10秒から15秒に設定
                    timer = Random.Range(10f,15f);
                    //stateをColonistState.Sleepにする
                    //timer10秒
                    
                    //掘り終わったら運ぶという状態にする
                        State = ColonistState.Carry;

                    //移動先を倉庫の位置にする
                    targetPosition = Warehouse.position;


                }
                break;

            case ColonistState.Carry: //運ぶ状態

                transform.position = Vector3.MoveTowards(
            transform.position, targetPosition, MoveSpeed * Time.deltaTime);

                //体力が回復するまで休ませる？
                //体力があったらもう一回Moveにして採掘場に向かわせるか？
                //休憩する場所に行って、休憩する
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    targetPosition = MarketPosition.position;
                    //次の行動を行う(休憩）
                    State = ColonistState.Rest;
               
                    timer = Random.Range(1f, 5f);
                }


                    break;

            case ColonistState.Rest://休憩
                transform.position = Vector3.MoveTowards(
           transform.position, targetPosition, MoveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    //1秒間に5ポイントずつ空腹を回復させる
                    hunger += 5f * Time.deltaTime;
                    //ストレスも1秒間に５ポイント緩和
                    stress -= 5f * Time.deltaTime;

                    //現在の体力をじわじわっと回復させる
                    currentHealth += RecoveryRate * 2f * Time.deltaTime;

                    //体力と空腹度が８０より回復したら
                    if(currentHealth>80f&& hunger > 80)
                    {
                        timer = 1f;
                        State = ColonistState.Idle;
                        
                    }
                }
                

                break;
            case ColonistState.Sleep://就寝

                //1秒間に8ポイント回復させる
                currentHealth +=RecoveryRate* 8f * Time.deltaTime;


               

                //1秒間に5ポイントずつストレスが減る
                stress -= 5f * Time.deltaTime;

                //もしコロニストの体力が完全に回復し
                
                if (currentHealth >= MaxHealth)
                {
                    State = ColonistState.Idle;
                    timer = Random.Range(1f,5f);


                }

                break;
        }
    }
}
