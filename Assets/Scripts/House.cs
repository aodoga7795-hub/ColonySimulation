using UnityEngine;

/// <summary>
/// Sleepのときに住人がここにきて寝るようにする
/// </summary>
public class House : MonoBehaviour
{
    /// <summary>
    /// 家で休む際のボーナス
    /// </summary>
    public float RecoveryBonus = 2f;
   


     public Vector3 GetHousePosition()
    {
        //Houseの世界座標を返します
        return this.transform.position;
    }
   
}
