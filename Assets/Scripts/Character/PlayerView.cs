using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerView : MonoBehaviour
{
    [SerializeField] PlayerModel model;

    [SerializeField] TMP_Text currentText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text gameOverText;

    private void Start()
    {
        UpdateBestCoinUI();
        gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        currentText.text = $"Coin : {model.CoinCount}";
    }

    public void OnClickLobbyButton()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void UpdateBestCoinUI()
    {
        int bestCoin = DataManager.Instance.GetBestCoin();
        highScoreText.text = $"Best : {bestCoin}";
    }

    public void OnGameEnd()
    {
        DataManager.Instance.TryUpdateBestCoin(model.CoinCount);
        UpdateBestCoinUI();
        gameOverText.gameObject.SetActive(true);
    }

}
