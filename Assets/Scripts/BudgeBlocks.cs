using UnityEngine;

public class BudgeBlocks : MonoBehaviour {

    public bool isAction = false;
    public Transform startMarker;
    public Transform endMarker;
    public float speed;
    private static float startTime;
    private float AB;

    void Start()
    {
        AB = Vector3.Distance(startMarker.position, endMarker.position);
    }

    static bool a = true;
    static bool b = false;

    static float x;
    float cp;
    void Update()
    {
        if (isAction)
        {
            if (a && !b)
            {
                if (startTime == 0) { startTime = Time.time; }
                x = (Time.time - startTime) * speed;
                cp = (x / AB);
                transform.position = Vector3.Lerp(startMarker.position, endMarker.position, cp);
                if(x >= AB) { FirstMethod(); }
            }   
            if (b && !a)
            {
                x = (Time.time - startTime) * speed;
                cp = (x / AB);
                transform.position = Vector3.Lerp(endMarker.position,startMarker.position, cp);
                if (x >= AB) { SecondMethod(); }
            }
        }
    }

    static void FirstMethod()
    {
        startTime = Time.time/* + AB*/;
        
        a = false;
        b = true;
    }

    static void SecondMethod()// 
    {
        a = true;
        b = false;
        startTime = Time.time;
    }
}
