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

    public void UpdateBestCoinUI()
    {
        int bestCoin = GameDataManager.Instance.GetBestCoin();
        highScoreText.text = $"Best : {bestCoin}";
    }

    public void OnGameEnd()
    {
        GameDataManager.Instance.TryUpdateBestCoin(model.CoinCount);
        UpdateBestCoinUI();
        gameOverText.gameObject.SetActive(true);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && gameOverText.gameObject.activeSelf == true)
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }

}
