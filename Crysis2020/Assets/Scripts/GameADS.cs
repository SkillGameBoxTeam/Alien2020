using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class GameADS : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private string gameId = "1111111"; // идентификатор приложения
    [SerializeField] UI_Control uI_Control;
    private Button myButton; // кнопка, которая будет показывать ролик
    public string myPlacementId = "rewardedVideo"; // идентификатор видео, по умолчанию 'rewardedVideo'

    void Start()
    {
        myButton = uI_Control.getMoreHeartsButton;
        myButton.interactable = Advertisement.IsReady(myPlacementId);
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, false);
    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
    }

    void IUnityAdsListener.OnUnityAdsReady(string placementId)
    {
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    void IUnityAdsListener.OnUnityAdsDidError(string message)
    {
        // ошибка
    }

    void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
    {
        // дополнительные действия, которые необходимо предпринять, когда конечные пользователи запускают объявление.
    }

    void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            UI_Control.Instance.GetMoreHearts();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // не вознаграждайте пользователя за пропуск объявления.
        }
        else if (showResult == ShowResult.Failed)
        {
            // объявление не было завершено из-за ошибки.
        }
    }
}
