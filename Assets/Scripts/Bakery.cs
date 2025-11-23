using UnityEngine;

public class Bakery : MonoBehaviour
{
    public float FoodStock = 100;

    /// <summary>
    /// ベーカリーで食事ができるかどうか
    /// </summary>
    /// <returns></returns>
    public bool CanEat()
    {
        return FoodStock > 0;
    }

    public void Eat()
    {
        FoodStock -= Time.deltaTime ;
    }
}
