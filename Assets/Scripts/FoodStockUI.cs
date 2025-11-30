using UnityEngine;
using TMPro;

public class FoodStockUI : MonoBehaviour
{
    public TextMeshProUGUI FoodStockText;

    public Bakery Bakery;


    // Update is called once per frame
    void Update()
    {
        FoodStockText.text =$"FoodStock: {Bakery.FoodStock}";
    }
}
