using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] PlayerModel model;

    [SerializeField] TMP_Text currentText;
    [SerializeField] TMP_Text highScoreText;

    private void Update()
    {
        currentText.text = $"Coin : {model.CoinCount}";
    }
}
