using UnityEngine;

namespace Rubber
{
    public class DetectCollisions : MonoBehaviour
    {    
        internal static GameObject coll;
        internal GameObject soundlist;
        public AudioClip toGreen, toRed;
        public static bool LockCollision;
        internal AudioSource aSource;

        private void Start()
        {
            soundlist = GameObject.Find("Sound List");
            aSource = soundlist.GetComponent<AudioSource>();
            toGreen = soundlist.GetComponent<SoundManager>().toGreen;
            toRed = soundlist.GetComponent<SoundManager>().toRed;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("player") && !LockCollision)//edit
            {
                coll = col.gameObject;
                if (gameObject.name == "Block1" || gameObject.name == "Block2" || gameObject.name == "Block3")/*
                    iki animatorü olduğu için 
                    changecolor olmazdı*/
                {
                    Animator[] anims;
                    anims = GetComponentsInChildren<Animator>();
                    if (anims[0].GetInteger("which") == 0)
                    {//kırmızı
                        anims[0].SetInteger("which", 1);
                        if (anims.Length != 1)
                        { anims[1].SetInteger("which", 1); }
                        aSource.clip = toRed;
                        aSource.Play();

                        if      (gameObject.name == "Block1") { LevelObjectives.b1 = false; }
                        else if (gameObject.name == "Block2") { LevelObjectives.b2 = false; }
                        else if (gameObject.name == "Block3") { LevelObjectives.b3 = false; }
                    }
                    else
                    {//yeşil
                        anims[0].SetInteger("which", 0);
                        if (anims.Length!=1)
                        { anims[1].SetInteger("which", 0); }
                        aSource.clip = toGreen;
                        aSource.Play();

                        if      (gameObject.name == "Block1") { LevelObjectives.b1 = true; }
                        else if (gameObject.name == "Block2") { LevelObjectives.b2 = true;  }
                        else if (gameObject.name == "Block3") { LevelObjectives.b3 = true;  }
                    }
                    return;
                }
                ChangeColor();
            }
        }

        public void ChangeColor()
        {
            int whichColors = GetComponent<Animator>().GetInteger("which");

            if (whichColors == 0)//kırmızı
            {
                GetComponent<Animator>().SetInteger("which", 1);
                aSource.clip = toRed;
                aSource.Play();

                if      (gameObject.name == "altduvar") { LevelObjectives.bot = false; }
                else if (gameObject.name == "sağduvar") { LevelObjectives.right = false; }
                else if (gameObject.name == "solduvar") { LevelObjectives.left = false; }
                else if (gameObject.name == "ustduvar") { LevelObjectives.top = false; }

            }
            else// yeşil
            {
                GetComponent<Animator>().SetInteger("which", 0);
                aSource.clip = toGreen;
                aSource.Play();

                if      (gameObject.name == "altduvar")  { LevelObjectives.bot = true;  }
                else if (gameObject.name == "sağduvar"){ LevelObjectives.right = true; }
                else if (gameObject.name == "solduvar"){ LevelObjectives.left = true; }
                else if (gameObject.name == "ustduvar"){ LevelObjectives.top = true;  }
            }

        }
    }
}