using System;
using UnityEngine;
using UnityEngine.UI;

public class LastLevelTexts : MonoBehaviour {

    private Text text;

    private string TR_lastlevelTexts = "Tebrikler :)\nTüm bölümleri bitirdin\n"
        + Environment.NewLine + "Zaman ayırıp oynadığın için\nteşekkürler.\n"
        + Environment.NewLine + "İleride yeni bölümler ekleyebilirim\n"+
          "bu yüzden tavsiye, görüş yada\neleştirilerini yazarsan sevinirim\n"
        + Environment.NewLine + "info@yldzgames.com",
        ENG_lastlevelTexts = "Congratulations :)\nYou have completed all levels\n"
        + Environment.NewLine + "Thank you for your time\n"
        + Environment.NewLine + "I can add new levels in future\n"
          + "so if you have any advice or comment\nplease send me\n"
        + Environment.NewLine + "info@yldzgames.com";
    // Use this for initialization
    void Start () {
        text = gameObject.GetComponent<Text>();

        if (PlayerPrefs.GetString("Lng") == "tr")
        {
            text.text = TR_lastlevelTexts;
        }
        else
        {
            text.text = ENG_lastlevelTexts;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
