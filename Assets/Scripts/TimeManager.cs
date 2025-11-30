using UnityEngine;
using UnityEngine.UI;//ボタンやテキストなどの表示に使う


public class TimeManager : MonoBehaviour
{
    public Button PauseButton;
    public Button PlayButton; //1倍速
    public Button Speed2xbutton; //2倍速
    public Button Speed3xbutton; //3倍速


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ゲームが開始したときは等倍速
        SetTimeScale(1f);

        PauseButton.onClick.AddListener(() => SetTimeScale(0f));
        PlayButton.onClick.AddListener(() => SetTimeScale(1f));
        Speed2xbutton.onClick.AddListener(() => SetTimeScale(2f));
        Speed3xbutton.onClick.AddListener(() => SetTimeScale(3f));
    }

    /// <summary>
    /// 時間の倍速設定を引数の値によって行う
    /// </summary>
    /// <param name="scale"></param>
    private void SetTimeScale(float scale)
    {

        Time.timeScale = scale;
        Debug.Log($"TimeScale:{scale}");

    }
    
}
