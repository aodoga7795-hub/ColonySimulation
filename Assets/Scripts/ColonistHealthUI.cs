using UnityEngine;
//UnityEngineのUIの要素を使う宣言
using UnityEngine.UI;

public class ColonistHealthUI : MonoBehaviour
{
    /// <summary>
    /// 体力を参照するため
    /// </summary>
    public ColonistAI ColonistAI;

    /// <summary>
    /// 体力表示用のバー
    /// </summary>
    public Image healthBar;

    /// <summary>
    /// ストレス値用のバー
    /// </summary>
    public Image StressBar;

    /// <summary>
    /// 空腹値用のバー
    /// </summary>
    public Image HungerBar;

    // Update is called once per frame
    void Update()
    {
        //healthBarに現在の体力/最大の体力で出る割合を表示
        healthBar.fillAmount = ColonistAI.GetCurrentHealth / ColonistAI.MaxHealth;

        //StressBarに現在のストレス値/100で出る割合を表示します
        StressBar.fillAmount =
            ColonistAI.GetStress / 100;

        //HungerBarに現在の空腹値/100で出る割合を表示します
        HungerBar.fillAmount =
            ColonistAI.GetHunger / 100;
    }
}
