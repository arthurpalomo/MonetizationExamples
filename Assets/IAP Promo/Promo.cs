using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;

public class Promo : MonoBehaviour
{
    public string gameId;
    public string placementName;
    public bool isTestMode = false;
    public Button showPromoButton;
    public Purchasing purchasingSystem;

    private void Awake()
    {
        if (showPromoButton == null)
        {
            Debug.LogError("UI Not Configured");
        }
        if(purchasingSystem == null)
        {
            Debug.LogError("Puchasing Adapter is not set");
        }
        Monetization.Initialize(gameId, isTestMode);
        Monetization.SetPurchasingAdapter(purchasingSystem);

    }

    private void OnEnable()
    {
        showPromoButton.onClick.AddListener(ShowPromo);
    }

    private void OnDisable()
    {
        showPromoButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (showPromoButton != null)
        {
            showPromoButton.interactable = Monetization.IsReady(placementName);
        }
    }

    public void ShowPromo()
    {
        if (Monetization.IsReady(placementName) == false)
        {
            Debug.Log("Placement Not Ready: " + placementName);
            return;
        }

        //Get Content for a placement
        PlacementContent placementContent = Monetization.GetPlacementContent(placementName);
        if(placementContent == null)
        {
            Debug.LogError("Placement Content Empty for Placement ID: " + placementContent.placementId);
            return;
        }

        //Convert to ShowAdPlacementContent to show the ad
        //This works for most ad types (Video, Playable, Display, Promo, AR, etc.)
        //IAP Promo requires that the Purcahsing Adapter is set, otherwise placement will never be Ready
        ShowAdPlacementContent showAd = (ShowAdPlacementContent)placementContent;

        if (showAd == null)
        {
            Debug.LogError("Show Ad Placement Content Empty for Placement ID: " + showAd.placementId);
            return;
        }

        Debug.Log("Showing Promo - Placement: " + showAd.placementId);

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