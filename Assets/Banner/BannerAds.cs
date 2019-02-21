using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    public string gameId;
    public bool isTestMode = false;
    public string placementName;
    public Button showBannerButton;
    public Button hideBannerButton;
    private void Awake()
    {
        Advertisement.Initialize(gameId, isTestMode);
        if (!showBannerButton || !hideBannerButton)
        {
            Debug.LogError("UI Not Configured");
        }

        hideBannerButton.interactable = false;
    }

    private void OnEnable()
    {
        showBannerButton.onClick.AddListener(ShowBanner);
        hideBannerButton.onClick.AddListener(HideBanner);
    }

    private void OnDisable()
    {
        showBannerButton.onClick.RemoveAllListeners();
        hideBannerButton.onClick.RemoveAllListeners();
    }

    public void ShowBanner()
    {
        BannerOptions options = new BannerOptions
        {
            showCallback = OnBannerShown,
            hideCallback = OnBannerHidden
        };
        Advertisement.Banner.Show(placementName, options);
    }

    public void OnBannerShown()
    {
        hideBannerButton.interactable = true;
        Debug.Log("Banner should show.");
    }

    public void OnBannerHidden()
    {
        hideBannerButton.interactable = false;
        Debug.Log("Banner has been hidden");
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    void Update()
    {
        if (showBannerButton)
        {
            //Check if Banner is Ready or if Banner has been Loaded
            showBannerButton.interactable = (Advertisement.IsReady(placementName) || Advertisement.Banner.isLoaded);
        }       
    }
}