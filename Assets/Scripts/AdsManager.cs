using UnityEngine;
using Rubber;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour 
{

	public GameObject videoİcon1, videoİcon2, removeAdsGameobject, premiumGameobjec;

    LevelProperties levelProperties;
    MethodsForTutotial methodsForTutotial;
    SceneDirector sceneDirector;

	private string isPremiumStringKey = "isPremium";
    private string sessionCountKey = "sessionCount";
    private static bool isPayed = false;
    internal bool isHint = false;
    internal bool isDirectPass = false;
    internal static bool rewarded = false;
    private int sessionCount;

    internal static bool IsPremium // Premium flag for runtime
    {
        get
        {
            return isPayed;
        }
        set
        {
            isPayed = value;
        }
    }
	private RewardBasedVideoAd rewardBasedVideoAd;
    internal InterstitialAd interstitial;

    private void Awake()
    {
        //CheckPremiumFeature();
    }

    void Start()
    {

        sessionCount= ReadSessionCount();
        methodsForTutotial = GameObject.Find("Tutorial Events").GetComponent<MethodsForTutotial>();
        GameObject creator_local = GameObject.Find("Creator");

        sceneDirector = creator_local.GetComponent<SceneDirector>();
        levelProperties = creator_local.GetComponent<LevelProperties>();

        this.rewardBasedVideoAd = RewardBasedVideoAd.Instance;
        this.rewardBasedVideoAd.OnAdRewarded += RewardBasedVideoAd_OnAdRewarded;
        this.rewardBasedVideoAd.OnAdClosed += RewardBasedVideoAd_OnAdClosed;
    }
   
    private void RewardBasedVideoAd_OnAdClosed(object sender, System.EventArgs e)
    {
        sceneDirector.XButtonTasks();
        RequestVideoAd();
    }

    private void RewardBasedVideoAd_OnAdRewarded(object sender, Reward e) 
	{
        methodsForTutotial.HideHintPanel(SceneDirector.activeScene.buildIndex);
        levelProperties.CloseTutorials();
        rewarded = true;
	}

    void CheckPremiumFeature()
    {
        try
        {
            if (
                (PlayerPrefs.HasKey(isPremiumStringKey) && PlayerPrefs.GetString(isPremiumStringKey) == "Premium")
                || isPayed
                )
            {
                print("premium varmıs");
                MakeGamePremiumFeatured();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void RequestVideoAd() 
    {
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-6575341029276583/3303646036";// gerçek Throw Ball Ad ID ***
		//string adUnitId = "ca-app-pub-3940256099942544/5224354917";// Test ID from Google
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3940256099942544/1712485313";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		AdRequest request = new AdRequest.Builder()
		//.AddTestDevice(AdRequest.TestDeviceSimulator)
		//.AddTestDevice("***************************") //device ID >> Asus
		.Build();
		this.rewardBasedVideoAd.LoadAd( request, adUnitId); // video yüklenir 
    }

    public void ShowRewardBasedVideo()
    {
        if (IsPremium)///// PREMİUM İSE
        {
			print ("premium");
            methodsForTutotial.HideHintPanel(SceneDirector.activeScene.buildIndex);
            levelProperties.CloseTutorials();
            rewarded = true;
			return;
        }
        else
		{
		print ("Premium değil"+ "yükleme durumu"+ rewardBasedVideoAd.IsLoaded());
            if (rewardBasedVideoAd.IsLoaded())
            {
                rewardBasedVideoAd.Show();// Reklamı Göster
            }
        }
    }

    internal void RequestInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Destroy();
        }

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6575341029276583/9865184952";// gerçek Throw Ball Ad ID ***
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712";// Test ID from Google
#elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
                string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
        interstitial.OnAdOpening += İnterstitial_OnAdOpening;
        interstitial.OnAdClosed += İnterstitial_OnAdClosed;
        interstitial.OnAdFailedToLoad += İnterstitial_OnAdFailedToLoad;
    }

    private void İnterstitial_OnAdOpening(object sender, System.EventArgs e)
    {
        // oyunu durdur
        Time.timeScale = 0;
    }
    private void İnterstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        Time.timeScale = 1;
        interstitial.Destroy();
    }
    private void İnterstitial_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        interstitial.Destroy();
    }

    public void ShowInterstialAd()
    {
        if (IsPremium)///// PREMİUM İSE
        {
            print("Premium Reklam İzlemeyecek");
            return;
        }
        else
        {
            print("Premium Değil" + "yükleme durumu" + rewardBasedVideoAd.IsLoaded());
            if (interstitial.IsLoaded())
            {
                interstitial.Show();// Reklamı Göster
            }
        }
    }

    public void DecideHintOrPass ( int prm )
    {
        if (prm == 0)
        {
            isHint = true;
            isDirectPass = false;
        }
        else
        {
            isDirectPass = true;
            isHint = false;
        } 
    }
	
    public void HintRewarTask()
    {
        methodsForTutotial.ShowHintPanel(SceneDirector.activeScene.buildIndex); // ipucunu göster**
        sceneDirector.XButtonTasks();
        Instantier.canPlacePlayer = false;
    }
	
    public void DirectPassTask()
    {
        sceneDirector.XButtonTasks();
        FinishingLevel.isLevelCompleted = true;
    }

    void WriteSessionCount(int count)
    {
        PlayerPrefs.SetInt(sessionCountKey, count);
        print("sessionCount Yazıldı: " + sessionCount);
    }

    int ReadSessionCount()
    {
        int sc = 0;
        if (PlayerPrefs.HasKey(sessionCountKey)){
            sc = PlayerPrefs.GetInt(sessionCountKey);
        }
        print("sessionCount Okundu: " + sc);
        if (sc % 7 == 6)
        {
            RequestInterstitial();
        }
        return sc;
    }

    internal void CheckInterstitialTime()
    {
        sessionCount += 1;  
        WriteSessionCount(sessionCount);
        if (sessionCount >=7)
        {
            if (interstitial != null)
            {
                sessionCount = 0;
                WriteSessionCount(sessionCount);
                ShowInterstialAd();
                return;
            }
        }
        else if (sessionCount==6)
        {
            RequestInterstitial();
        }
    }

    private void Update()
    {
        if (rewarded)
        {
            if (!isHint)
            {
                if (isDirectPass)
                {
                    isDirectPass = false;
                    DirectPassTask();
                }
            }
            else
            {
                isHint = false;
                HintRewarTask();
            }
            rewarded = false;
        }
    }

	public void BuyPremium()
	{
		//IAPManager.instanceofthis.BuyPremiumBundle ();
	}

	public void MakeGamePremiumFeatured()
	{
		videoİcon1.SetActive(false);    videoİcon2.SetActive(false);
		removeAdsGameobject.SetActive(false);
		premiumGameobjec.SetActive(true);

		IsPremium = true;
        WriteOnDiskPremium(isPremiumStringKey,"Premium",true);
	}

    private void WriteOnDiskPremium(string premiumKey,string premiumValue,bool isPremium)
    {
        if(isPremium)
        PlayerPrefs.SetString(premiumKey, premiumValue);
    }

}   


