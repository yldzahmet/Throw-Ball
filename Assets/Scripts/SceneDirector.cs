using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rubber
{
    public class SceneDirector : MonoBehaviour
    {
        internal static bool isInGame;
        internal static Scene activeScene;

        public Animator sceneTransition;

        Instantier instant;
        LevelProperties levelProperties;
        MethodsForTutotial methodsForTutotial;
        AdsManager adsManager;
        StarManager starManager;

        public GameObject errorPanel;
        public GameObject effects, levels;
        public GameObject optionsButton;
        public GameObject menuPanel;
        public GameObject hintTriggerButton, hintTriggerPanel;
        public GameObject levelNumberShower;
        public Text levelText;
        int lastLevelNumber=0;
        public Text nCount;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += ActiveteScene;
            FinishingLevel.DoneLevel += BolumGec;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= ActiveteScene;
            FinishingLevel.DoneLevel -= BolumGec;
        }

        void Awake()
        {
            if (PlayerPrefs.HasKey("level"))
            {
                lastLevelNumber = PlayerPrefs.GetInt("level");
                print("level " + lastLevelNumber + " olarak hafızadan alındı");
            }
        }

        void Start()
        {
            starManager = FindObjectOfType<StarManager>();
            methodsForTutotial = GameObject.Find("Tutorial Events").GetComponent<MethodsForTutotial>();
            levelProperties = GetComponent<LevelProperties>();
            adsManager = GetComponent<AdsManager>();
            instant = GetComponent<Instantier>();
            sceneTransition = GameObject.FindGameObjectWithTag("scenegeçiş").GetComponent<Animator>();
        }

        public GameObject menuStarsParent;
        internal void UpdateLevelNumberOnGUI(byte index)
        {
            levelText.text = index.ToString();
            int starCount = 0;
            try
            {
                starCount = StarManager.starsList[index-1];
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }

            for (int r = 0; r < 3;)
            {
                if (r>=starCount)
                {
                    menuStarsParent.transform.GetChild(r).gameObject.SetActive(false);
                }
                else
                {
                    menuStarsParent.transform.GetChild(r).gameObject.SetActive(true);
                }
                r++;
            }
        }

        public void EnableLevelMenu()
        {
            LevelUIEditor();
            starManager.GetStarCountsEach5();
            DetectCollisions.LockCollision = true;
            levels.SetActive(true);
            effects.SetActive(false);
        }

        public void DisableLevelMenu()
        {
            levels.SetActive(false);
            effects.SetActive(true);
        }

        public void UnloadLevel()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        }

        public void NextLevelScene()
        {
            byte m = StarManager.SumTotalStarsInEach5((byte)(levelindex_ - 5));
            if (levelindex_%5==0 &&
                m < 10)
            {
                nCount.text = (10 - m).ToString();
                errorPanel.SetActive(true);
            }
            else
            {
                FinishingLevel.isLevCompEventFired = false;
                int tempLevel = levelindex_;
                tempLevel++;
                try
                {
                    adsManager.RequestVideoAd();// // Create an ad request. // Her sahne yüklendiğinde yeni istekte bulunacak
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e.Message);
                }

                UnloadLevel();// direk button göreviydi ordan buraya koyduk
                SceneManager.LoadSceneAsync(tempLevel, LoadSceneMode.Additive);
                if (tempLevel == 61 || tempLevel == 62 || tempLevel == 63 || tempLevel == 64 || tempLevel == 65 || tempLevel == 66
                    || tempLevel == 67 || tempLevel == 68 || tempLevel == 69 || tempLevel == 70 || tempLevel == 71 || tempLevel == 72
                     || tempLevel == 73 || tempLevel == 74 || tempLevel == 75) { sceneTransition.SetTrigger("2menügeri"); }
                // 60 ve üstü açılırken ekranda  menü düzgün görünsündiye****************************************************
                else { sceneTransition.SetTrigger("1menügeri"); }
                if (tempLevel == 76)
                {
                    return;
                }
                OpenOptionsButton();

                print("onenabled");
                Instantier.canPlacePlayer = true;
                instant.SyncColorsWithStart();
                FinishingLevel.isInBasket = true;
            }
            
        }

        int levelindex_;// yüklenen sahne 

        public void LaodScene_(int lvlindex)
        {
            try
            {
                adsManager.RequestVideoAd();// // Create an ad request. // Her sahne yüklendiğinde yeni istekte bulunacak
            }catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }

            levelindex_ = lvlindex;
            SceneManager.LoadSceneAsync(levelindex_, LoadSceneMode.Additive);

            levels.SetActive(false);
            OpenOptionsButton();
            Instantier.canPlacePlayer = true;
            instant.SyncColorsWithStart();
        }

        internal void ActiveteScene(Scene scene, LoadSceneMode loadmode)// Sahne yüklendiği zaman çalışır
        {
            SceneManager.SetActiveScene(scene);// level sahnesi aktiflenir
            activeScene = scene; // Static sahne güncelendi
            levelindex_ = activeScene.buildIndex;
            UpdateLevelNumberOnGUI((byte)levelindex_);
        }

        public void LaodLastLevel()
        {
            try
            {
                adsManager.RequestVideoAd();// // Create an ad request. // Her sahne yüklendiğinde yeni istekte bulunacak
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
            DetectCollisions.LockCollision = true;
            if (lastLevelNumber != 76)// olmayan leveli açmaya çalışırsa // +1 // 
            {
                SceneManager.LoadSceneAsync(lastLevelNumber + 1, LoadSceneMode.Additive);
                effects.SetActive(false);
                Instantier.canPlacePlayer = true;
                instant.SyncColorsWithStart();
                OpenOptionsButton();
            }
            else // başka bölüm kalmadığı için en son geçilmiş olan bölüm açılır
            {
                SceneManager.LoadSceneAsync(lastLevelNumber, LoadSceneMode.Additive);// direk "lastLevelNumber" açılır
                effects.SetActive(false);
                Instantier.canPlacePlayer = true;
                instant.SyncColorsWithStart();
                OpenOptionsButton();
            }
        }

        public void RefreshScene_InterLevelMenu()
        {
            FinishingLevel.isLevCompEventFired = false;
            try
            {
                adsManager.RequestVideoAd();// // Create an ad request. // Her sahne yüklendiğinde yeni istekte bulunacak
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }

            methodsForTutotial.HideHintPanel(activeScene.buildIndex); // Hint sayfasını kapat
            DetectCollisions.LockCollision = true;

            int tempLevel = activeScene.buildIndex;
            if (tempLevel == 61 || tempLevel == 62 || tempLevel == 63 || tempLevel == 64 || tempLevel == 65 || tempLevel == 66
                || tempLevel == 67 || tempLevel == 68 || tempLevel == 69 || tempLevel == 70 || tempLevel == 71 || tempLevel == 72
                 || tempLevel == 73 || tempLevel == 74 || tempLevel == 75) { sceneTransition.SetTrigger("2menügeri"); }
            else { sceneTransition.SetTrigger("1menügeri"); }

            Destroy(instant._Player);
            print("level" + activeScene.buildIndex);
            UnloadLevel();
            LaodScene_(activeScene.buildIndex);

            print("onenabled");
            Instantier.canPlacePlayer = true;
            instant.SyncColorsWithStart();

            levelProperties.CloseTutorials();// OpenTutorials sayfasını kapat
            FinishingLevel.isInBasket = true;
        }

        public void RefreshScene_InGameMenu()
        {
            menuPanel.SetActive(false);
            try
            {
                adsManager.RequestVideoAd();// // Create an ad request. // Her sahne yüklendiğinde yeni istekte bulunacak
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
            methodsForTutotial.HideHintPanel(activeScene.buildIndex); // Hint sayfasını kapat
            DetectCollisions.LockCollision = true;
            Destroy(instant._Player);
            print("level" + activeScene.buildIndex);
            UnloadLevel();
            LaodScene_(activeScene.buildIndex);
            print("onenabled");
            Instantier.canPlacePlayer = true;
            instant.SyncColorsWithStart();
            levelProperties.CloseTutorials();// OpenTutorials sayfasını kapat
        }

        public void OptionsButtonTasks()
        {
            Destroy(instant._Player);
            Destroy(instant._bluePoint);
            Instantier.canPlacePlayer = false;
            if (menuPanel.activeSelf == false)// oyun içi menu açma
            {
                menuPanel.SetActive(true);
                CloseOptionsButton();
            }
        }

        public void HintTriggerTask()
        {
            levelProperties.CloseTutorials();// OpenTutorials sayfasını kapat
            methodsForTutotial.HideHintPanel(activeScene.buildIndex); // Hint sayfasını kapat
            Instantier.canPlacePlayer = false;// üst methodlar bunu true yaptığı için sıralaması önemli
            Destroy(instant._Player);
            Destroy(instant._bluePoint);
        }

        public void XButtonTasks()
        {
            menuPanel.SetActive(false);
            hintTriggerPanel.SetActive(false);
            OpenOptionsButton();

            if(!LevelProperties.isTutorialOpen && !LevelProperties.isHintOpen && activeScene.buildIndex!=76 )
            {
                Instantier.canPlacePlayer = true;
            }
            
        }

        public void HomeButtonTasks()
        {
            FinishingLevel.isLevCompEventFired = false;
            menuPanel.SetActive(false);
            effects.SetActive(true);
            CloseOptionsButton();

            levelProperties.CloseTutorials();// OpenTutorials sayfasını kapat
            methodsForTutotial.HideHintPanel(activeScene.buildIndex); // Hint sayfasını kapat

            sceneTransition.Play("New State");

            UnloadLevel();

            Instantier.canPlacePlayer = false;//oyuncu koyamasın
            FinishingLevel.isInBasket = true;//bölüm geçebilsin
        }

        public void OpenOptionsButton()
        {
            if (LevelProperties.activatingScene != 76)
            {
                hintTriggerButton.SetActive(true);
            }
            optionsButton.SetActive(true);
        }

        public void CloseOptionsButton()
        {
            optionsButton.SetActive(false);
            hintTriggerButton.SetActive(false);
            //İnstantier.canPlacePlayer = true;
        }

        internal void EraseLevelProgres()
        {
            lastLevelNumber = 0;
            PlayerPrefs.SetInt("level", lastLevelNumber);
            Debug.Log("level " + lastLevelNumber + " sıfırlandı ");
        }

        private void BolumGec()
        {
            FinishingLevel.isLevCompEventFired = true;
            SaveLevelProgress();
            Instantier.canPlacePlayer = false;
            if (LevelProperties.menuAnimType == 0)
            {
                GameObject.FindGameObjectWithTag("scenegeçiş").GetComponent<Animator>().SetTrigger("1menü"); // level completed soundu animason içine gömülü
            }
            else if (LevelProperties.menuAnimType == 1)
            {
                GameObject.FindGameObjectWithTag("scenegeçiş").GetComponent<Animator>().SetTrigger("2menü"); //level completed soundu animason içine gömülü
            }
            optionsButton.SetActive(false);
            hintTriggerButton.SetActive(false);
        }

        internal void SaveLevelProgress()

        {
            int templvl = activeScene.buildIndex;
            StarManager.SaveStars(templvl);
            StarManager.tryCount = 0;
            if (templvl >= lastLevelNumber)
            {
                lastLevelNumber = templvl;
                PlayerPrefs.SetInt("level", lastLevelNumber);
                Debug.Log("level " + lastLevelNumber + " KAYDEDİLDİ ");
            }
        }

        public GameObject[] levelButtons;

        internal void LevelUIEditor()
        {
            byte u;
            if (lastLevelNumber > 4)
            {
                u = DecideButtonsVisibility();
            }
            else
            {
                u = (byte)lastLevelNumber;
            }
            
            int tempNmbrForActButtons = u;
            for (int x = 0 ; x <= tempNmbrForActButtons;)
            {
                if (x == 75) { break; }
                levelButtons[x].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                levelButtons[x].GetComponent<Button>().interactable = true;
                levelButtons[x].GetComponentInChildren<Text>().color = new Color(1f, 1f, 1f, 1f);
                x++;
            }

            int tempLevel = u;
            tempLevel += 1;

            for (; tempLevel < levelButtons.Length;)
            {
                //print("for sayım" + tempLevel);
                levelButtons[tempLevel].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
                levelButtons[tempLevel].GetComponent<Button>().interactable = false;
                levelButtons[tempLevel].GetComponentInChildren<Text>().color = new Color(1f, 1f, 1f, 0.2f);

                tempLevel++;
            }
            tempLevel = 0;
        }

        internal byte DecideButtonsVisibility()
        {
            byte result = 0;

            byte sts = StarManager.SumTotalStarsInEach5(0);
            if (sts >0 && sts <10)
            {
                if (StarManager.starsList[0] != 0) { result = 1; }
               
                if (StarManager.starsList[1] != 0) { result = 2; }
                
                if (StarManager.starsList[2] != 0) { result = 3; }
                
                if (StarManager.starsList[3] != 0) { result = 4; }
                
            }
            else if (sts>=10)
            {
                result = 9;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(5);
            if (sts > 0 && sts < 10)
            {
                result = 9;
            }
            else if (sts >= 10)
            {
                result = 14;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(10);
            if (sts > 0 && sts < 10)
            {
                result = 14;
            }
            else if (sts >= 10)
            {
                result = 19;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(15);
            if (sts > 0 && sts < 10)
            {
                result = 19;
            }
            else if (sts >= 10)
            {
                result = 24;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(20);
            if (sts > 0 && sts < 10)
            {
                result = 24;
            }
            else if (sts >= 10)
            {
                result = 29;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(25);
            if (sts > 0 && sts < 10)
            {
                result = 29;
            }
            else if (sts >= 10)
            {
                result = 34;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(30);
            if (sts > 0 && sts < 10)
            {
                result = 34;
            }
            else if (sts >= 10)
            {
                result = 39;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(35);
            if (sts > 0 && sts < 10)
            {
                result = 39;
            }
            else if (sts >= 10)
            {
                result = 44;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(40);
            if (sts > 0 && sts < 10)
            {
                result = 44;
            }
            else if (sts >= 10)
            {
                result = 49;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(45);
            if (sts > 0 && sts < 10)
            {
                result = 49;
            }
            else if (sts >= 10)
            {
                result = 54;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(50);
            if (sts > 0 && sts < 10)
            {
                result = 54;
            }
            else if (sts >= 10)
            {
                result = 59;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(55);
            if (sts > 0 && sts < 10)
            {
                result = 59;
            }
            else if (sts >= 10)
            {
                result = 64;
            }
            sts = StarManager.SumTotalStarsInEach5(60);
            if (sts > 0 && sts < 10)
            {
                result = 64;
            }
            else if (sts >= 10)
            {
                result = 69;
            }
            else
            {
                return result;
            }
            sts = StarManager.SumTotalStarsInEach5(65);
            if (sts > 0 && sts < 10)
            {
                result = 69;
            }
            else if (sts >= 10)
            {
                result = 74;
            }
            return result;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.E))
            {
                EraseLevelProgres();
            }
            if (Input.GetKey(KeyCode.L))
            {
                lastLevelNumber = 74;
                PlayerPrefs.SetInt("level", lastLevelNumber);
                Debug.Log("level " + lastLevelNumber + " KAYDEDİLDİ ");
                for (int i = 0; i < 75;)
                {
                    StarManager.starsList[i] = 2;
                    i++;
                }
            }
            if (Input.GetKey(KeyCode.P))
            {
                PlayerPrefs.DeleteAll();
                for(int i = 0; i < 75;)
                {
                    StarManager.starsList[i] = 0;
                    i++;
                }
                StarManager.JsonHelper.ArrayToJson<int>(StarManager.starsList);
                print("Tüm PlayerPrefs verileri silindi");
            }
        }
    }
}


