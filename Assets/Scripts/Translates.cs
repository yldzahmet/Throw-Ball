using UnityEngine.UI;
using System;
using UnityEngine;

public class Translates : MonoBehaviour {

    internal static string language; // >>tr,>>eng
    internal string langKeyString = "Lng", tr = "tr", eng = "eng"; // Key for language

    #region  Translates
    private string TR_sonoyun = "SON OYUN", ENG_sonoyun = "LAST GAME";
    private string TR_levelseç = "BÖLÜM SEC", ENG_levelseç = "CHOOSE LEVEL";
    private string TR_reklamkaldır = "SINIRSIZ \nIPUCU \nREKLAMI \nKALDIR", ENG_reklamkaldır = "FREE \nHINTS \nREMOVE \nADS";

    private string TR_tut_lvl_1 = "Topu Sepete At", ENG_tut_lvl_1 = "Throw Ball İnside Basket";
    private string TR_tut_lvl_2 = "Açık gri alanlara \ntop koyabilirsin !!", ENG_tut_lvl_2 = "You can place the ball \n grey fieds";
    private string TR_tut_lvl_6 = "Bölümü geçebilmek için \n Kırmızı duvarları Yesil yapmalısın \ntopu çarptır ve sepete gitsin",
                   ENG_tut_lvl_6 = "You must switch red lights \nto green for finish level \nHit to wall and Throw Ball İnside";
    private string TR_tut_lvl_11 = "2. çıkıs kapısı\nduvar Yesıl olduktan sonra \ntopu dırek buraya gönder"
        ,ENG_tut_lvl_11 = "This is SECOND goal. \n After when red became green \nThrow Ball here";
    private string TR_tut_lvl_21 = "Ulasamadıgın \nyerlere top göndermek \nicin portalları kullan";
    private string ENG_tut_lvl_21 = "Use portals for \nunreacable areas";

    private string TR_panel_baslık = "Dil Seçeneği", ENG_panel_baslık = "Language";
    private string TR_word_tr = "TÜRKÇE", ENG_word_tr = "TURKISH";
    private string TR_word_eng = "İNGİLİZCE", ENG_word_eng = "ENGLISH";

    private string TR_hintİnfo_lvl1 = "Düşündüğünden daha kolay :) \n\nSadece buraya dokun";
    private string ENG_hintİnfo_lvl1 = "Easy than you think :) \n\nJust tap here";
    private string TR_hintİnfos = "Tam zamanında bırak", ENG_hintİnfo = "Release just in time";
    private string TR_soHard = "Dikkatli ol bu çok zor :)", ENG_soHard = "Be Careful this is so hard :)";

    private string TR_premiumHeader = "Premium", ENG_premiumHeader = "Premium";
    private string TR_premiumText = "-Reklamlar yok\n-Sınırsız ipucu\n-Direk geçiş";
    private string ENG_premiumText = "-No ads\n-Full hint\n-Direct pass";
    private string TR_rewardedTriggerText = "İpucu almak için reklam videosu\nizlemek istermisiniz ?";
    private string ENG_rewardedTriggerText = "Do you want to watch\n ad video for hint";

    private string ENG_collectStarsText = "Collect stars to unlock levels!";
    private string TR_collectStarsText = "Bölümleri açmak için yıldız topla!";

    private string TR_needStarCount= "Yıldız daha gerekli :(";
    private string ENG_needStarCount = "More stars need :(";
    #endregion // çeviri metinleri

    public Text sonOyun, levelSeç, ads;
    public Text cont_lvl1, cont_lvl2, cont_lvl6, cont_lvl11, cont_lvl21; // tutorial text objeleri
    public Text panel_DilBaslığı, panel_tr_button, panel_eng_button; // panel text objeleri
    public Text [] info; // Hint text objesi
    public Text soHardText;
    public Text hint_lvl1;
    public Text lastLevelText;
    public Text premium, premiumChild;
    public Text rewardedTrigger;
    public Text collectStars;
    public Text nCount;
    
    private void Awake(){
        if (PlayerPrefs.HasKey(langKeyString)){
            language = PlayerPrefs.GetString(langKeyString);
            LanguageChange(language);
        }
        else
        {
            if (Application.systemLanguage == SystemLanguage.Turkish)
            {
                LanguageChange(tr);
            }
            else if(Application.systemLanguage == SystemLanguage.English)
            {
                LanguageChange(eng);
            }
        }
    }

    private void ConverterWithArray (Text[] arr ,string e)
    {
        try
        {
            for (int i = 0; i < info.Length;)
            {
                arr[i].text = e;
                i++;
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    public void LanguageChange(string lang)
    {
        if (lang == tr)
        {
            PlayerPrefs.SetString(langKeyString, tr);
            sonOyun.text = TR_sonoyun;
            levelSeç.text = TR_levelseç;
            ads.text = TR_reklamkaldır;
            panel_DilBaslığı.text = TR_panel_baslık;
            panel_tr_button.text = TR_word_tr;
            panel_eng_button.text = TR_word_eng;
            hint_lvl1.text = TR_hintİnfo_lvl1;
            cont_lvl1.text = TR_tut_lvl_1;
            cont_lvl2.text = TR_tut_lvl_2;
            cont_lvl6.text = TR_tut_lvl_6;
            cont_lvl11.text = TR_tut_lvl_11;
            cont_lvl21.text = TR_tut_lvl_21;
            ConverterWithArray(info, TR_hintİnfos);
            premium.text = TR_premiumHeader;
            premiumChild.text = TR_premiumText;
            soHardText.text = TR_soHard;
            rewardedTrigger.text = TR_rewardedTriggerText;
            collectStars.text = TR_collectStarsText;
            nCount.text = TR_needStarCount;
        }
        else if (lang == eng)
        {
            PlayerPrefs.SetString(langKeyString, eng);
            sonOyun.text = ENG_sonoyun;
            levelSeç.text = ENG_levelseç;
            ads.text = ENG_reklamkaldır;
            panel_DilBaslığı.text = ENG_panel_baslık;
            panel_tr_button.text = ENG_word_tr;
            panel_eng_button.text = ENG_word_eng;
            hint_lvl1.text = ENG_hintİnfo_lvl1;
            cont_lvl1.text = ENG_tut_lvl_1;
            cont_lvl2.text = ENG_tut_lvl_2;
            cont_lvl6.text = ENG_tut_lvl_6;
            cont_lvl11.text = ENG_tut_lvl_11;
            cont_lvl21.text = ENG_tut_lvl_21;
            ConverterWithArray(info, ENG_hintİnfo);
            premium.text = ENG_premiumHeader;
            premiumChild.text = ENG_premiumText;
            soHardText.text = ENG_soHard;
            rewardedTrigger.text = ENG_rewardedTriggerText;
            collectStars.text = ENG_collectStarsText;
            nCount.text = ENG_needStarCount;
        }
    }
}
