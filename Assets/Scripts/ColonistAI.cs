using UnityEngine;

public class ColonistAI : MonoBehaviour
{
    /// <summary>
    /// Enum型で宣言したコロニストの状態
    /// </summary>
    public enum ColonistState
    {
        Idle,//待機
        Move,//移動
        Mine,//採掘
        Sleep,//就寝
        Carry,//運ぶ
        Rest, //休憩
        Eat,//食事
        Dead //死亡
        
    }

    public ColonistState State;

    public enum JobType
    {
        Invalid = -1,//定義されていない
            Miner,　//採掘者
            Carrier　//運搬者
    }
    //いったん全ての住人は採掘者とする
    public JobType Job = JobType.Miner;


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
    [SerializeField]
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

    /// <summary>
    /// ベーカリー(食事する場所）の位置
    /// </summary>
    public Transform BakeryPosition;

    /// <summary>
    /// ベーカリーの機能
    /// </summary>
    public Bakery Bakery;

    /// <summary>
    /// 採掘場の機能
    /// </summary>
    public MineSite MineSite;

    /// <summary>
    /// 運搬中の採掘資産
    /// </summary>
    private float carryingAmount = 0f;

    /// <summary>
    /// コロニストが持てる採掘資産の最大値
    /// </summary>
    private float carryingCapacity = 10f;

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

        //コロニストの年齢が30歳まで
        if (ColonistAge < 30)
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
        else if (ColonistAge < 50)
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
        else //5０歳より上
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
        stress += 1f * Time.deltaTime;

        //ストレスが限界を超えたら勝手に休憩に入る
        if(stress >= 100f)
        {
            Debug.Log($"{name}はストレスが限界！休憩に入ります！");
            State = ColonistState.Rest;
        }
        else if (hunger <= 30f)//空腹度が３０を下回ったとしても
        {
            Debug.Log($"{name}はお腹が減ったので、休憩に入ります");
            State = ColonistState.Eat;
        }


        //小かっこの中の変数を使って処理を分岐(switch)させる
        switch (State)
        {
            case ColonistState.Idle://待機
                HandleIdle();

                break;

            case ColonistState.Move://移動
                HandleMove();
                break;

            case ColonistState.Mine://発掘
                HandleMine();
                break;

            case ColonistState.Carry: //運ぶ状態

                HandleCarry();

                break;

            case ColonistState.Rest://休憩

                HandleRest();
                break;

            case ColonistState.Eat://食事
                HandleEat();
                break;

            case ColonistState.Sleep://就寝

                HandleSleep();
                break;
        }
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
    }

    /// <summary>
    /// 待機中の行動
    /// </summary>
    private void HandleIdle()
    {
        //現在の体力をじわじわ回復させる
        currentHealth += RecoveryRate * 2f * Time.deltaTime;
        //もしタイマーが０秒を下回ったら
        if (timer <= 0f)
        {
            //コロニストの状態を動くという状態に変更
            State = ColonistState.Move;
            //ターゲットのポジションを決めてあげる
            targetPosition = MinePoint;
            timer = 2f;
        }
    }

    /// <summary>
    /// 移動中の行動
    /// </summary>
    private void HandleMove()
    {
        transform.position = Vector3.MoveTowards(
           transform.position, targetPosition, MoveSpeed * Time.deltaTime);

        //現在の体力値から1秒間で５ポイント体力をへらす
        currentHealth -= FatigueRate * 5f * Time.deltaTime;
        //現在の体力が20ポイント下回ったら
        if (currentHealth <= 20f)
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
    }
    /// <summary>
    /// 採掘中の行動
    /// </summary>
    private void HandleMine()
    {
        //もしジョブが運搬者だったら
        if (Job == JobType.Carrier)
        {
            //採掘場の共有資産が自分が持てるキャパシティーに到達しているか
            if (MineSite.SharedMinedResorce >= carryingCapacity)
            {
                //自分が持てるキャパシティー分を採掘場から取得してくる
                carryingAmount = MineSite.TakeResource(carryingCapacity);
                Debug.Log($"{name}が{carryingAmount}分資源を回収しました");
                //取得出来たら運ぶという状態にする
                State = ColonistState.Carry;
                //移動先を倉庫の位置にする
                targetPosition = Warehouse.position;
                //ここから下の処理を行わない
                return;
            }
           

           
        }
        //仮で採掘アニメーション再生の代わりにログを出力
        Debug.Log("Colonist is mining!");
        //毎フレーム回転させ続ける
        //1秒間にMiningSkillが３の人は360度一回転できる
        transform.Rotate(Vector3.up * 120f * MiningSkill * Time.deltaTime);

        //現在の体力を1秒間に10ポイント減らす
        currentHealth -= FatigueRate * 10f * Time.deltaTime;

        //現在の体力が20ポイントより少なくなったら
        if (currentHealth <= 20f)
        {
            //体力を回復させるためにSleepにする
            State = ColonistState.Sleep;
        }

        if (timer <= 0f)
        {
            int mined = Mathf.RoundToInt(10 * MiningSkill);
            //MinedResource += mined;

            //Debug.Log($"採掘完了{mined}(合計{MinedResource})");
            MineSite.AddResorce(mined);

            Debug.Log($"採掘完了{mined}(合計{MineSite.SharedMinedResorce})");
            MinedResource = 0;


            
            //timerを10秒から15秒に設定
            timer = Random.Range(10f, 15f);
            //jobが採掘者だったら
            if(Job == JobType.Miner)
            {
                State = ColonistState.Mine;
            }
            else if (Job == JobType.Carrier)
            {
                State = ColonistState.Carry;
                //移動先を倉庫の位置にする
                targetPosition = Warehouse.position;
                //採掘場の共有資産が自分が持てるキャパシティーに到達しているか
                if (MineSite.SharedMinedResorce >= carryingCapacity)
                {
                    //自分が持てるキャパシティー分を採掘場から取得してくる
                    carryingAmount = MineSite.TakeResource(carryingCapacity);
                    Debug.Log($"{name}が{carryingAmount}分資源を回収しました");
                }
                else
                {
                    //採掘場の共有資産がなかったら自分も採掘を行う
                    State = ColonistState.Mine;
                }
            }

        }
    }

    /// <summary>
    /// 運搬中の行動
    /// </summary>
    private void HandleCarry()
    {
       


        transform.position = Vector3.MoveTowards(
           transform.position, targetPosition, MoveSpeed * Time.deltaTime);

        //体力が回復するまで休ませる？
        //体力があったらもう一回Moveにして採掘場に向かわせるか？
        //休憩する場所に行って、休憩する
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            //倉庫に資源を置く
            //まず倉庫のコンポーネントを取得する
            Warehouse warehouse = Warehouse.GetComponent<Warehouse>();

            //もし倉庫のコンポーネントが見つかったら
            if (warehouse != null)
            {
                //倉庫に採掘した量を追加する
                //carryingAmountはFloat型なのでInt型でキャストします
                //キャストとは（型）変数で変数を型に変換することです
                //今回はfloat（小数点付きの値）をint（整数）に変換しました
                warehouse.Store((int)carryingAmount);
                //倉庫に置いたので運搬中の採掘量を０にする
                carryingAmount = 0;
            }
            

            //体力があった場合
            if (currentHealth > 50 )
            {
                targetPosition = MinePoint;
                //採掘のための移動
                State = ColonistState.Move;
            }
            else//体力が危ない場合
            {
                targetPosition = MarketPosition.position;
                State = ColonistState.Rest;
            }
           
            timer = Random.Range(1f, 5f);
        }

    }
    /// <summary>
    /// 休憩中の行動
    /// </summary>
    private void HandleRest()
    {
        //ターゲットポジションが市場じゃなかったら市場に変更する
        if(targetPosition != MarketPosition.position)
        {
            targetPosition = MarketPosition.position;
        }

        transform.position = Vector3.MoveTowards(
          transform.position, targetPosition, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            
            //ストレスも1秒間に５ポイント緩和
            stress -= 5f * Time.deltaTime;

            //現在の体力をじわじわっと回復させる
            currentHealth += RecoveryRate * 2f * Time.deltaTime;

            //体力が８０より回復し、ストレスがなくなったら
            if (currentHealth > 80f && stress <=0)
            {
                stress = 0f;
                timer = 1f;

                State = ColonistState.Idle;

            }
        }

    }
    /// <summary>
    /// 食事中の行動
    /// </summary>
    private void HandleEat()


    {

        if(targetPosition != BakeryPosition.position)
        {
            targetPosition = BakeryPosition.position;
        }
        transform.position = Vector3.MoveTowards(
          transform.position, targetPosition, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f){

            if (Bakery.CanEat())
            {
                //食事をしてFoodStockを減らす
                Bakery.Eat();

                //食事の場所に行ったら1秒で20ポイント回復

                hunger += 20f * Time.deltaTime;

                //ストレスが1秒で5ポイント減る
                stress -= 5f * Time.deltaTime;

                //体力も回復させる
                currentHealth += 2f * hunger * Time.deltaTime;
                //Mathf.Clamp(値、最小値、最大値）で最小値から最大値までの値に
                //制限してくれます
                currentHealth = Mathf.Clamp(currentHealth,0, MaxHealth);
            }
            else//食べ物がない場合
            {
                currentHealth += 2f * hunger * Time.deltaTime;
            }
            //もし満腹になったら
            if (hunger >= 100f)
            {
                hunger = 100f;
                Debug.Log($"{name}は満腹になりました");
                State = ColonistState.Idle;
            }
        }
      
        
    }
    /// <summary>
    /// 就寝中の行動
    /// </summary>
    private void HandleSleep()
    {
        //1秒間に8ポイント回復させる
        currentHealth += hunger * 8f * Time.deltaTime;

        //1秒間に5ポイントずつストレスが減る
        stress -= 5f * Time.deltaTime;

        //もしコロニストの体力が完全に回復したら

        if (currentHealth >= MaxHealth)
        {
            State = ColonistState.Idle;
            timer = Random.Range(1f, 5f);

        }
    }
}
