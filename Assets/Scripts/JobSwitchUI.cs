using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JobSwitchUI : MonoBehaviour
{
    /// <summary>
    /// ColonistAIに直接、Jobの変更を行うため
    /// </summary>
    public ColonistAI ColonistAI;

    public Button SwitchButton;

    /// <summary>
    /// Jobの名前を表示するための機能
    /// </summary>
    public TextMeshProUGUI JobLabel;



    /// <summary>
    /// ColonistUIManagerさんから呼ばれることを想定
    /// </summary>
    /// <param name="colonistAI"></param>
    public void SetSwitchUI(ColonistAI colonistAI)
    {
        this.ColonistAI = colonistAI;
        SwitchButton.onClick.AddListener(ToggleJob);
        UpdateLabel();
    }
    public void ToggleJob()
    {
        //もしColonistのジョブが採掘者だったら
       if(ColonistAI.Job== ColonistAI.JobType.Miner)
        {
            //運搬者に変更します
            ColonistAI.Job = ColonistAI.JobType.Carrier;
        }
       //そうじゃなくてColonistのジョブが運搬者だったら
       else if(ColonistAI.Job == ColonistAI.JobType.Carrier)
        {
            //建築作業員に変更します
            ColonistAI.Job = ColonistAI.JobType.Builder;
        }
       else if (ColonistAI.Job ==ColonistAI.JobType.Builder)
        {//採掘者に変更します
            ColonistAI.Job = ColonistAI.JobType.Miner;
        }
        UpdateLabel();
    }
    /// <summary>
    /// JobLavelにColonistAI.Jobの文字を表示する
    /// </summary>
     void UpdateLabel()
    {
        JobLabel.text = $"Job:{ColonistAI.Job}";
    }

    private void Update()
    {
        //毎フレームColonistAIの職業を監視して、その文字を出し続ける
        UpdateLabel();
    }
}
