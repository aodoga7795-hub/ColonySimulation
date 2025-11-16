using UnityEngine;

public class Warehouse : MonoBehaviour
{
    /// <summary>
    /// 倉庫が保持する資源量
    /// </summary>
    public int StoredResources = 0;

    /// <summary>
    /// 在庫の最大値
    /// </summary>
    private int maxStockAmount = 1000;

    /// <summary>
    /// 外部から倉庫の在庫最大値を取得する
    /// </summary>
    public int GetMaxStockAmount
    {
        get { return maxStockAmount; }
    }
     
    /// <summary>
    ///引数の数、倉庫が保持する資源量を増やす
    /// </summary>
    /// <param name="amount"></param>
    public void Store(int amount)
    {
        StoredResources += amount;
        Debug.Log($"倉庫に{amount}納品" + $"合計：{StoredResources})");
    }



}
