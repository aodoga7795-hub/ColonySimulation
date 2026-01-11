using UnityEngine;
using UnityEngine.UI;

public class ConstructionSiteUI : MonoBehaviour
{
    public Image FillImage;

    public ConstructionSite ConstructionSite;


    private void Update()
    {
        FillImage.fillAmount = ConstructionSite.GetProgress;
    }

}
