using System.Collections;
using UnityEngine;

namespace Rubber
{

    public class Instantier : MonoBehaviour
    {
        public delegate void ColorReset();
        public static event ColorReset ColorReseted;
        StarManager starManager;
        FinishingLevel finishingLevel;
        public Animator menuAnimator;
        public GameObject starHolder;
        public GameObject FinishGO;
        public GameObject Pl;
        public GameObject Player, Pin, bluePoint;

        [HideInInspector]
        internal GameObject _Player, _Pin, _bluePoint;
        public GameObject particles;
        private Camera currentCam;
        public static bool canPlacePlayer;

        void Awake()
        {
            starManager = starHolder.GetComponent<StarManager>();
            finishingLevel = FinishGO.GetComponent<FinishingLevel>();
            currentCam = GetComponentInParent<Camera>();
        }

        void Start()
        { 
            MenuActions();
        }

        public void SyncColorsWithStart()
        {
            StartCoroutine(SyncColorsWithStartCoroutine());
        }

        IEnumerator SyncColorsWithStartCoroutine()
        {
            yield return new WaitForSeconds(.1f);
            //print("BaşlangıçRenkResetlemesi");
            if(ColorReseted != null)
            ColorReseted();
        }

        public GameObject Menu;

        public void MenuActions()
        {
            byte k = 25;
            DetectCollisions.LockCollision =false;
            canPlacePlayer = false;                            //player koyabilme
            try
            {
                Pl.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 1000 * k),ForceMode2D.Force);
            }
            catch
            {
                return;
            }
        }

        internal bool CheckSuperBanElement(byte element)
        {
            switch (element)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 9:
                case 21:
                case 26:
                case 27:
                case 30:
                case 59:
                case 69:
                case 70:
                case 71:
                    return true;
                default:
                    return false;
            }
        }

        internal void CheckBanArea() //// Düzenlenecek !!!!!!!!!!!!
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit Hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out Hit))
                {
                    if (Hit.collider.gameObject.CompareTag("banarea"))
                    {

                        print("Hit");
                        GameObject[] banAreas = GameObject.FindGameObjectsWithTag("banarea");
                        try
                        {
                            banArea = true;
                            for (int i = 0; i < banAreas.Length; i++)
                            {
                                banAreas[i].GetComponentInParent<Animator>().SetTrigger("ban");
                            }
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogWarning(e.Message);
                        }
                    }
                }
                else
                {
                    banArea = false;
                }
            }
            else if(Input.GetMouseButton(0) && CheckSuperBanElement((byte)SceneDirector.activeScene.buildIndex))
            {
                RaycastHit Hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out Hit))
                {
                    if (Hit.collider.gameObject.CompareTag("banarea"))
                    {
                        GameObject[] banAreas = GameObject.FindGameObjectsWithTag("banarea");
                        try
                        {
                            banArea = true;
                            //k = 0;
                            Destroy(_bluePoint);
                            Destroy(_Pin);
                            Destroy(_Player);
                            for (int i = 0; i < banAreas.Length; i++)
                            {
                                banAreas[i].GetComponentInParent<Animator>().SetTrigger("ban");
                            }
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogWarning(e.Message);
                        }
                    }
                }
            }

        }

        Vector3 startPos;
        Vector3 reverseVector;
        Vector3 _mpos;
        internal float speed = 5.5f;
        internal static float _playerSpeeedMultipler = 1;
        private Transform pointFortarget;

        bool mouse = true;
        bool banArea = false;

        void Update()
        {
            if (canPlacePlayer)
            {                
                if (Input.GetMouseButtonDown(0))
                {
                    _mpos = currentCam.ScreenToWorldPoint(Input.mousePosition);
                    _mpos.z = -10;

                    starManager.UpdateStarCount();
                    finishingLevel.StopAllCoroutines();
                    FinishingLevel.isInBasket = false;
                    //print("Tuch içinde" + FinishingLevel.kilitFinish);
                    finishingLevel.i = 0;//finishi resetler
                    if (ColorReseted != null)
                    {
                        ColorReseted();
                    }

                    if (_Player) { Destroy(_Player); }
                    if (_bluePoint) { Destroy(_bluePoint); }
                    if (_Pin) { Destroy(_Pin); }

                    _Pin = Instantiate(Pin, _mpos, Quaternion.identity);
                    _bluePoint = Instantiate(bluePoint,_mpos,Quaternion.identity);
                    _Player = Instantiate(Player, _mpos, Quaternion.identity);
                    startPos = _mpos;//start pos kaydedildi
                    DetectCollisions.LockCollision = true;
                }
                else if (Input.GetMouseButton(0))
                {
                    _mpos = currentCam.ScreenToWorldPoint(Input.mousePosition);
                    _mpos.z = -10;
                    reverseVector = _mpos - startPos;
                    float q = reverseVector.magnitude;
                        
                    _Pin.transform.LookAt(_bluePoint.transform);
                    _Pin.transform.localScale = new Vector3(1,1,q);

                    _Player.transform.position = _mpos;
                    _Pin.transform.position = _mpos;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    if (_Player) { _Player.GetComponent<Rigidbody2D>().velocity = -reverseVector * speed* _playerSpeeedMultipler; }

                    DetectCollisions.LockCollision = false;
                    Destroy(_bluePoint);
                    Destroy(_Pin);
                }
            }
        }
    }
}
