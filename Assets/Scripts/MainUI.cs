using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    public GameObject LoginPanel;
    public GameObject RankingPanel; // 로그인 & 랭킹 패널
    public GameObject LoginFailPanel;

    public Button LoginExitButton;
    public Button RankingExitButton; // 로그인 & 랭킹 나가는 버튼
    public Button FailButton;

    public Button StartButton;
    public Button RankingButton;    // 시작 버튼과 랭킹 버튼

    public Button GameStartButton; // 게임 시작 버튼 (로그인 이후)


    void Start()
    {
        LoginPanel.SetActive(false); 
        RankingPanel.SetActive(false); // 로그인 & 랭킹 패널 비활성화
        LoginFailPanel.SetActive(false);

        StartButton.onClick.AddListener(StartButtonClick);
        RankingButton.onClick.AddListener(RankingButtonClick);

        LoginExitButton.onClick.AddListener(LoginExit);
        RankingExitButton.onClick.AddListener(RankingExit);
        FailButton.onClick.AddListener(FailExit);

        // GameStartButton.onClick.AddListener(GameStart);
    }

    void StartButtonClick()
    {
        LoginPanel.SetActive(true); // 로그인 패널 활성화
    }

    void RankingButtonClick()
    {
        RankingPanel.SetActive(true); // 랭킹 패널 활성화
    }

    void LoginExit()
    {
        LoginPanel.SetActive(false); // 로그인 패널 끄기
    }

    void RankingExit()
    {
        RankingPanel.SetActive(false); // 랭킹 패널 끄기
    }

    void FailExit()
    {
        LoginFailPanel.SetActive(false);
    }

}
