using UnityEngine;
using UnityEngine.UI;

public class MoneyUi : MonoBehaviour { //Actualiza el texto del dinero.

    public Text moneyText;

    private void Update()
    {
        moneyText.text = "€" + PlayerStats.Money.ToString();
    }
}
