using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rubber
{

    public class LevelProperties : MonoBehaviour
    {
        AdsManager adsManager;
        StarManager starManager;
        SceneDirector scenem;
        Instantier instant;

        internal static byte activatingScene;
        
        public GameObject optionsButtonRef;
        public GameObject replay, dPass, hint;
        public GameObject panelMenu;
        public GameObject level1_container;
        public GameObject level2_container;
        public GameObject level6_container;
        public GameObject level11_container;
        public GameObject level21_container;
        public GameObject starHolder;

        void OnEnable()
        {
            adsManager = GetComponent<AdsManager>();
            starManager = starHolder.GetComponent<StarManager>();
            scenem = gameObject.GetComponent<SceneDirector>();
            instant = gameObject.GetComponent<Instantier>();

            optionsButtonRef = scenem.optionsButton;
            //panelMenu = scenem.menuPanel;

            SceneManager.activeSceneChanged += EditLevelChangeCases;// creator için
            SceneManager.activeSceneChanged += OpenTutorials;
        }

        internal static bool isTutorialOpen = false;
        internal static bool isHintOpen = false;
        private void OpenTutorials(Scene deactive, Scene active) // sahne aktiflenince açılır
        {
            if (active.buildIndex == 1)
            {
                level1_container.SetActive(true);
                if (level1_container.GetComponentInParent<MethodsForTutotial>().swipeTutorial.GetCurrentAnimatorStateInfo(0).IsName("st"))
                {
                    level1_container.GetComponentInParent<MethodsForTutotial>().swipeTutorial.SetTrigger("st");
                }
                Instantier.canPlacePlayer = false; isTutorialOpen = true;
            }
            else if (active.buildIndex == 2)
            {
                level2_container.SetActive(true); Instantier.canPlacePlayer = false; isTutorialOpen = true;
            }
            else if(active.buildIndex== 6)
            {
                level6_container.SetActive(true); Instantier.canPlacePlayer = false; isTutorialOpen = true;
            }
            else if (active.buildIndex == 11)
            {
                level11_container.SetActive(true); Instantier.canPlacePlayer = false; isTutorialOpen = true;
            }
            else if (active.buildIndex == 21)
            {
                level21_container.SetActive(true); Instantier.canPlacePlayer = false; isTutorialOpen = true;
            }

        }

        public void CloseTutorials()
        {
            if (SceneDirector.activeScene.buildIndex == 1)// tutorialler açıkmı kapalımı kontrol eklenebilir
            {
                level1_container.SetActive(false);
                if (level1_container.GetComponentInParent<MethodsForTutotial>().swipeTutorial.GetCurrentAnimatorStateInfo(0).IsName("tut"))
                {
                    level1_container.GetComponentInParent<MethodsForTutotial>().swipeTutorial.SetTrigger("normal");
                }

            }
            else if (SceneDirector.activeScene.buildIndex == 2)
            {
                level2_container.SetActive(false);
            }
            else if (SceneDirector.activeScene.buildIndex == 6)
            {
                level6_container.SetActive(false);
            }
            else if (SceneDirector.activeScene.buildIndex == 11)
            {
                level11_container.SetActive(false);
            }
            else if (SceneDirector.activeScene.buildIndex == 21)
            {
                level21_container.SetActive(false);
            }
            isTutorialOpen = false;
            Instantier.canPlacePlayer = true;
        }

        void OnDisable()
        {
            SceneManager.activeSceneChanged -= EditLevelChangeCases;// creator için
            SceneManager.activeSceneChanged -= OpenTutorials;
        }

        internal static int menuAnimType = 0;

        internal void EditLevelChangeCases(Scene A, Scene B/*yüklenen sahne*/) //sahne değiştinde kameraya yön vermek için (Overload Method)
        {
            activatingScene = (byte)B.buildIndex;
            if(A.buildIndex == 0) // leveller açılırsa
            {
                starManager.RestartStars();
                starManager.starHolder.SetActive(true);// sol üstteki yıldızlar açılır
                int LevelNumer = B.buildIndex;
                print(" ACTİVE SCENE >>>>>>>> " + LevelNumer);
                if (LevelNumer >= 61 && LevelNumer <= 75)
                {
                    EditPlayerSetting(1.425f,
                        new Vector3(.5f, .5f, .5f),
                        1.5f,
                        LevelNumer);
                }
                scenem.OpenOptionsButton();
                Instantier.canPlacePlayer = true;

                adsManager.CheckInterstitialTime();// Geçiş Reklamı İçin Kontrol Et 
            }
            if (A.buildIndex != 0 && B.buildIndex == 0 ) // levellerden ana menüye geçerse
            {
                if (A.isLoaded)
                optionsButtonRef.SetActive(false);

                EditPlayerSetting(2.85f, new Vector3(.7f, .7f, .7f),1.0f,B.buildIndex);
                starManager.starHolder.SetActive(false);// sol üstteki yıldızlar kapanır

                if(A.buildIndex==76 && B.buildIndex == 0)
                {
                    LastLevelCloseTasks(hint, 3);
                    LastLevelCloseTasks(replay, 4);
                    LastLevelCloseTasks(dPass, 5);
                }
                instant.MenuActions();
            }
            if (B.buildIndex == 76)
            {
                starManager.starHolder.SetActive(false);// sol üstteki yıldızlar kapanır

                LastLevelOpenTasks(hint, 3);
                LastLevelOpenTasks(replay, 4);
                LastLevelOpenTasks(dPass, 5);

                Instantier.canPlacePlayer = false;//player koyabilme
                //optionsButtonRef.SetActive(true);
            }
        }

        void LastLevelOpenTasks(GameObject g , int i)
        {
            g.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 0.2f);
            g.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        void LastLevelCloseTasks(GameObject g, int i)
        {
            g.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
            g.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }

        internal void EditPlayerSetting(float gravity,Vector3 localScale,float speedMultipler,int currentLevel)
        {
            if (currentLevel == 69)
            {
                instant.Player.GetComponent<Rigidbody2D>().gravityScale = 15;
                goto Gravity;
            }
            instant.Player.GetComponent<Rigidbody2D>().gravityScale = gravity;
            Gravity:
            instant.Player.transform.localScale = localScale;
            Instantier._playerSpeeedMultipler = speedMultipler;
        }
    }
}
