using UnityEngine;

public class MineSite : MonoBehaviour
{

    /// <summary>
    /// 採掘場にためる共有の資産
    /// </summary>
    public float SharedMinedResorce = 0f;

    /// <summary>
    /// コロニストさんがリソースを追加するための処理
    /// </summary>
    /// <param name="amount"></param>
    public void AddResorce(float amount)
    {
        SharedMinedResorce += amount;
        //Mathf.Clamp(変数、最小値、最大値）で制限してくれる
        SharedMinedResorce =
            Mathf.Clamp(SharedMinedResorce,0,9999);
    }
    /// <summary>
    /// コロニストが採掘場からamount分取っていく処理
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
   
    public float TakeResource(float amount)
    {
        //Mathf.Min(変数A、変数B）はどちらが少ないかを計算してくれる
        float taken = Mathf.Min(amount,SharedMinedResorce);
        SharedMinedResorce -= taken;
        //採掘場から取得できる資産の数を返す
        return taken;
    }


}
