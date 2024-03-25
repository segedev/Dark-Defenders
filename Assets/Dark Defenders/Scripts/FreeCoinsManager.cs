using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCoinsManager : MonoBehaviour
{
    public AdsManager ads;
    private void Start()
    {
        ads.ShowBanner();
    }
    public void AddMoreCoins()
    {
        ads.PlayRewardedAd(OnRewardedAdSuccess);
    }
    public void OnRewardedAdSuccess()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 5);
    }
}
