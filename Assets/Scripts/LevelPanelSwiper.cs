using UnityEngine;


public class LevelPanelSwiper : MonoBehaviour {

    private const float scaleMultipler= 2.832425f;
    internal GameObject container;
    internal Vector2 floatpoint, loc1, loc2, loc3, loc4, loc5, loc6, loc7;


    public void RightSwipeLevelScreen()
    {
        if (floatpoint == loc1) {      container.transform.localPosition = loc2; floatpoint = loc2; }
        else if (floatpoint == loc2) { container.transform.localPosition = loc3; floatpoint = loc3; }
        else if (floatpoint == loc3) { container.transform.localPosition = loc4; floatpoint = loc4; }
        else if (floatpoint == loc4) { container.transform.localPosition = loc5; floatpoint = loc5; }
        else if (floatpoint == loc5) { container.transform.localPosition = loc1; floatpoint = loc1; }
    }

    public void LeftSwipeLevelScreen()
    {
        if (floatpoint == loc1) {      container.transform.localPosition = loc5; floatpoint = loc5; }
        else if (floatpoint == loc2) { container.transform.localPosition = loc1; floatpoint = loc1; }
        else if (floatpoint == loc3) { container.transform.localPosition = loc2; floatpoint = loc2; }
        else if (floatpoint == loc4) { container.transform.localPosition = loc3; floatpoint = loc3; }
        else if (floatpoint == loc5) { container.transform.localPosition = loc4; floatpoint = loc4; }
    }

    // Use this for initialization
    void Start () {
        loc1 = new Vector2(0f * scaleMultipler, -0f);
        loc2 = new Vector2(-500f * scaleMultipler, -0f);
        loc3 = new Vector2(-1000f * scaleMultipler, -0f);
        loc4 = new Vector2(-1500f * scaleMultipler, -0f);
        loc5 = new Vector2(-2000f * scaleMultipler, -0f);
        loc6 = new Vector2(-2500f * scaleMultipler, -0f);
        loc7 = new Vector2(-3000f * scaleMultipler, -0f);
    }

    private void OnEnable()
    {
        container = gameObject;
        container.transform.localPosition = loc1;
        floatpoint = container.transform.localPosition;
    }
}
