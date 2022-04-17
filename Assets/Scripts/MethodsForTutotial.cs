using System.Collections;
using UnityEngine;
using Rubber;

public class MethodsForTutotial : MonoBehaviour
{

    public Animator swipeTutorial;//animasyonu tekrar için
    public GameObject player; //enerji vermek için    

    public GameObject[] hintsArray;
    
    public void LunchBall()
    {
        float k = .85f;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(60f*k, 41.25f*k);
        StartCoroutine(LunchBallEnum());
    }

    IEnumerator LunchBallEnum()
    {
        yield return new WaitForSeconds(3f);
        player.GetComponent<TrailRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        
        player.transform.localPosition = new Vector2(0.581f, 0.761f);

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        if (!swipeTutorial.GetCurrentAnimatorStateInfo(0).IsName("st"))
        {
            swipeTutorial.SetTrigger("restart");
            player.GetComponent<TrailRenderer>().enabled = true;
        }
    }

    public void ShowHintPanel(int index )
    {
        index -= 1;     hintsArray[index].SetActive(true);
    }

    public void HideHintPanel(int index)
    {
        try
        {
            if (hintsArray[(index-1)] != null )
            {
                index -= 1; hintsArray[index].SetActive(false); Instantier.canPlacePlayer = true;
            }
        }catch(System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
        
    }
}
