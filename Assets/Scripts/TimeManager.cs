using UnityEngine;
using UnityEngine.UI;//ボタンやテキストなどの表示に使う


public class TimeManager : MonoBehaviour
{
    public Button PauseButton;
    public Button PlayButton; //1倍速
    public Button Speed2xbutton; //2倍速
    public Button Speed3xbutton; //3倍速


    public AudioClip StopSE;
    public AudioClip PlaySE;
    public AudioClip SpeedUpSE;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ゲームが開始したときは等倍速
        SetTimeScale(1f);

        PauseButton.onClick.AddListener(() =>
        {
            SetTimeScale(0f);
            SEManager.Instance.PlaySE(StopSE);
            });

        PlayButton.onClick.AddListener(() =>
        {
            SetTimeScale(1f);
            SEManager.Instance.PlaySE(PlaySE);
            });

        Speed2xbutton.onClick.AddListener(() =>
        {
            SetTimeScale(2f);
            SEManager.Instance.PlaySE(SpeedUpSE);
        });
        Speed3xbutton.onClick.AddListener(() =>
        {
            SetTimeScale(3f);
            SEManager.Instance.PlaySE(SpeedUpSE);
        });
    }

    /// <summary>
    /// 時間の倍速設定を引数の値によって行う
    /// </summary>
    /// <param name="scale"></param>
    private void SetTimeScale(float scale)
    {

        Time.timeScale = scale;
        Debug.Log($"TimeScale:{scale}");
        //色も設定
        SetButtonColor(scale);

    }
    private void SetButtonColor(float scale)
    {
        //何倍速を押したか
        switch (scale)
        {
            case 0f:
                PauseButton.image.color = Color.white;
                PlayButton.image.color = Color.gray5;
                Speed2xbutton.image.color = Color.gray5;
                Speed3xbutton.image.color = Color.gray5;

                break;

            case 1f:
                PauseButton.image.color = Color.gray5;
                PlayButton.image.color = Color.white;
                Speed2xbutton.image.color = Color.gray5;
                Speed3xbutton.image.color = Color.gray5;

                break;

            case 2f:
                PauseButton.image.color = Color.gray5;
                PlayButton.image.color = Color.gray5;
                Speed2xbutton.image.color = Color.white;
                Speed3xbutton.image.color = Color.gray5;
                break;

            case 3f:
                PauseButton.image.color = Color.gray5;
                PlayButton.image.color = Color.gray5;
                Speed2xbutton.image.color = Color.gray5;
                Speed3xbutton.image.color = Color.white;
                break;
        }
    }
}
