using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;

public class Ads : MonoBehaviour
{
    public string gameId;
    public string placementName;
    public bool isTestMode = false;
    public Button showAdButton;
    private void Awake()
    {
        Monetization.Initialize(gameId, isTestMode);
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
        if (showAdButton != null)
        {
            showAdButton.interactable = Monetization.IsReady(placementName);
        }
    }

    public void ShowAd()
    {
        //Get Content for a placement
        PlacementContent placementContent = Monetization.GetPlacementContent(placementName);

        //Convert to ShowAdPlacementContent to show the ad
        //This works for most ad types (Video, Playable, Display, Promo, AR, etc.)
        //IAP Promo does require additional configuration. (See: PromoExample)
        ShowAdPlacementContent showAd = (ShowAdPlacementContent)placementContent;

        //We can check if this is a rewarded placement from the placement content
        if (showAd.rewarded == true)
        {
            ShowAdCallbacks callbacks = new ShowAdCallbacks
            {
                finishCallback = HandleReward
            };
            Debug.Log("Showing Rewarded Ad - Placement: " + placementContent.placementId);
            showAd.Show(callbacks);
        }
        else
        {
            Debug.Log("Showing Non-rewarded Ad - Placement: " + placementContent.placementId);
            showAd.Show();
        }
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