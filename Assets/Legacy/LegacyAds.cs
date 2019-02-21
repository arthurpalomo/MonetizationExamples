using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class LegacyAds : MonoBehaviour
{
    public string gameId;
    public string placementName;
    public bool isTestMode = false;
    public Button showAdButton;
    private void Awake()
    {
        Advertisement.Initialize(gameId, isTestMode);
        if (!showAdButton)
        {
            Debug.LogError("UI Not Configured");
        }
    }

    private void OnEnable()
    {
        showAdButton.onClick.AddListener(ShowAd);
    }

    private void OnDisable()
    {
        showAdButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (showAdButton)
        {
            showAdButton.interactable = Advertisement.IsReady(placementName);
        }
    }

    public void ShowAd()
    {
        ShowOptions options = new ShowOptions
        {
            resultCallback = HandleReward
        };
        Advertisement.Show(placementName, options);
    }

    void HandleReward(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Completed. Grant Reward.");
                break;
            case ShowResult.Failed:
                Debug.Log("Ad Failed. Grant Reward?");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad Skipped");
                break;
        }
    }
}