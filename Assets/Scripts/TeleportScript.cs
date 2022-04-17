using UnityEngine;
using Rubber;
public class TeleportScript : MonoBehaviour {

    public GameObject panel;
    private GameObject outPortal;
    private GameObject A, B;// inspectordan alınıyor
    private Transform C, D;// get_coponent ile alınıyor
    Instantier instant;

    // Use this for initialization
    void Start () {
		
        instant = (Instantier) FindObjectOfType((typeof(Instantier)));

        if(transform.parent.name== "TelGiriş")
        {
            outPortal = GameObject.FindGameObjectWithTag("teloutput");
        }
        else if(transform.parent.name == "TelGiriş_2")
        {
            outPortal = GameObject.FindGameObjectWithTag("teloutput2");
        }
        else
        {
            outPortal = GameObject.FindGameObjectWithTag("teloutput3");
        }
        
        A = gameObject.transform.parent.GetChild(1).gameObject;
        B = gameObject.transform.parent.GetChild(2).gameObject;
        C = outPortal.transform.GetChild(0);
        D = outPortal.transform.GetChild(1);
    }

    public void GenereateXandY( float a, float yn , out float _x , out float _y , float YNfarkı , float ZRot )
    {
        _x = 0; _y = 0;

        if (a >= yn)
        {
            //print("zrot="+ZRot);
            _x = Mathf.Cos(Mathf.Deg2Rad * (ZRot + YNfarkı));
            _y = Mathf.Sin(Mathf.Deg2Rad * (ZRot + YNfarkı)); print("a>yn _x= " + _x); print("a>yn _y= " + _y);
        }
        else if (a < yn)
        {
            //print("zrot=" + ZRot);
            _x = Mathf.Cos(Mathf.Deg2Rad * (ZRot - YNfarkı));
            _y = Mathf.Sin(Mathf.Deg2Rad * (ZRot - YNfarkı)); print("a<yn _x= " + _x); print("a>yn _y= " + _y);
        }
    }

    Vector2 VectorRotate(float ZRot,  Vector2 _Velocity)
    {
        float _x = 0, _y = 0;

        float cosVel = _Velocity.x / _Velocity.magnitude;
        float sinVel = _Velocity.y / _Velocity.magnitude;   // velocity cosu  //print("cosVel = " + cosVel);

        float cosDeg =Mathf.Rad2Deg * Mathf.Acos(cosVel);   // print("cosDeg= "+cosDeg);// DEGREE

        float yüzeyNormali = transform.parent.rotation.eulerAngles.z +180f;// !!! POZİTİF OLMAK ZORUNDA !!
        // girişin normal açısı
        // print("yn=" + yn);

        float YNfarkı=0;

        if (cosVel >= 0 && sinVel >= 0)
        {
            float a = cosDeg;   /* 1 - kalan aynı açı*/ //print("alve="+a);
            YNfarkı = Mathf.Abs(yüzeyNormali - a);                            // print("ynf=" + YNfarkı);
            GenereateXandY(a, yüzeyNormali, out _x, out _y, YNfarkı, ZRot);
        }
        else if (cosVel<=0 && sinVel>=0)
        {
            float a = cosDeg;   /* 1 - kalan aynı açı*/ //print("alve=" + a);
            YNfarkı = Mathf.Abs(yüzeyNormali - a);  //print("ynf=" + YNfarkı);
            GenereateXandY(a, yüzeyNormali, out _x, out _y, YNfarkı, ZRot);
        }
        else if (cosVel<=0 && sinVel<=0)
        {
            float a =( 360 - cosDeg );  //print("alve=" + a);
            YNfarkı = Mathf.Abs(yüzeyNormali - a);// 
            GenereateXandY(a, yüzeyNormali, out _x, out _y, YNfarkı, ZRot);
        }
        else if (cosVel >=0 && sinVel <=0)
        {

            float a = cosDeg + (360-2*cosDeg);     // 1 - kalan aynı açı*/ //print("alve=" + a);
            YNfarkı = Mathf.Abs(yüzeyNormali - a); //print("ynf=" + YNfarkı);
            GenereateXandY(a, yüzeyNormali, out _x, out _y, YNfarkı, ZRot);
        }
        Vector2 cod =new Vector2(_x, _y);
        return cod;
    }

    Vector3 İnstPoint(Vector3 hitPoint)
    {  
        Vector2 xA = A.transform.position - hitPoint;
        //Vector2 xB = B.transform.position - hitPoint;

        float X = xA.magnitude ; print("X= "+X);

        Vector2 lengthVector = A.transform.position - B.transform.position;
        float length = lengthVector.magnitude;

        float ratio = X / length; print("Ratio = "+ratio);
        Vector3 instPoint= Vector3.Lerp( C.position , D.position , ratio);

        return instPoint;
    }

    Vector2 velocityİnstance;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!DetectCollisions.LockCollision)
        {
            print("------" + outPortal.transform.rotation.eulerAngles.z);
            Rigidbody2D rb = instant._Player.GetComponent<Rigidbody2D>();
            velocityİnstance = rb.velocity;

            Destroy(instant._Player);

            if (instant._Player != null)
                print("temas noktası= " + col.transform.position);
            instant._Player = Instantiate(instant.Player, İnstPoint(col.transform.position), Quaternion.identity);
            rb = instant._Player.GetComponent<Rigidbody2D>();
            rb.velocity = VectorRotate(outPortal.transform.rotation.eulerAngles.z, velocityİnstance) * velocityİnstance.magnitude;
        }
    }
}
