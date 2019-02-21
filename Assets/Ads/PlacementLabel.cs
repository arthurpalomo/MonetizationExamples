using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlacementLabel : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Ads adManager;

    void Start()
    {
        label.SetText(adManager.placementName);        
    }
}