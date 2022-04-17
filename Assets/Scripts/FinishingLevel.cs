using System.Collections;
using UnityEngine;
using Rubber;

public class FinishingLevel : MonoBehaviour
{

    
    public delegate void Delegate_DoneLevel();
    public static event Delegate_DoneLevel DoneLevel;
    SoundManager soundManager;

    internal static bool isInBasket = false;
    internal static bool isLevCompEventFired = false;

    private void OnEnable()
    {
        soundManager = GameObject.Find("Sound List").GetComponent<SoundManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        isInBasket = true;
        if (col.CompareTag("player") && !DetectCollisions.LockCollision && !isLevCompEventFired)
        {
            if (gameObject.name == "f1")// kase ile bölüm geçildimi
            {
                try
                {
                    soundManager.PlayİnBasketSound();
                }
                catch { }
                if (col.name == "Plyr") { return; }
                StartCoroutine(TryToPassLevel());
            }
            else // (f2) çıkış ile bölüm geçildimi
            {
                if (LevelObjectives.CheckIsLevelObjectivesDone(SceneDirector.activeScene.buildIndex))
                {
                    Destroy(col.gameObject);
                    DoneLevel();
                    print("f2");
                }
            }
        }
    }

    internal int i = 0;
    internal IEnumerator TryToPassLevel()
    {
        for (;i<7;)
        {
            if (isInBasket)// X
            {
                i++;
                if (i == 7 && LevelObjectives.CheckIsLevelObjectivesDone(SceneDirector.activeScene.buildIndex))
                {
                    //Debug.Log("TEBRİKLER :)");
                    DoneLevel();
                }
                yield return new WaitForSeconds(.25f);
            }
            else
            {
                break;
            } 

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        i = 0;
        isInBasket = false;
    }

    internal static bool isLevelCompleted = false;
    void Update()
    {
        if (/*Input.GetKey(KeyCode.A) ||*/ isLevelCompleted)
        {
            DoneLevel();
            isLevelCompleted = false;
            return;
        }
    }
}
