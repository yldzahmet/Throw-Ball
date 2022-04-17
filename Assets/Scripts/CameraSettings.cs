using UnityEngine;

//[ExecuteInEditMode]
public class CameraSettings : MonoBehaviour {

    private GameObject creatorRef;
    public Canvas levelCanvas;
    private Camera mainCamera;

	// Use this for initialization
	void OnEnable ()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        levelCanvas = this.gameObject.GetComponent<Canvas>();
        levelCanvas.worldCamera = mainCamera;
	}
}
