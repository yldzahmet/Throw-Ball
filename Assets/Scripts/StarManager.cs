using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Rubber
{

    public class StarManager : MonoBehaviour
    {
        public GameObject starHolder, s1, s2, s3;
        public GameObject ans2, ans3;//animasyon içindeki starların objeleri
        internal static int tryCount = 0;
        internal static int star = 3;
        internal static bool isPerfect = false;
        internal static int[] starsList = new int[75];
        internal static byte totalStarNumber;

        private string gameDataFileName = "data.json";
        internal static string jsonFilePath;
        internal static string dataAsJson;

        private void Awake()
        {
            jsonFilePath = Path.Combine(Application.persistentDataPath, gameDataFileName);
            print("jsonFilePath = " + jsonFilePath);
            LoadStars();
        }

        internal static byte SumTotalStarsInEach5(byte indexer)
        {
            byte result = 0;
            byte cap;
            cap = (byte)(indexer + 5);
            for (; indexer < cap; )
            {
                result += (byte)starsList[indexer]; 
                indexer++;
            }
            return result;
        }

        public Text[] GroupsOfFive; 

        internal void GetStarCountsEach5()
        {
            for (int i = 0; i < GroupsOfFive.Length;)
            {
                byte k = (byte)(i * 5);
                GroupsOfFive[i].text = SumTotalStarsInEach5(k).ToString();
                i++;
            }
        }

        internal static void Add_Update_StarArray(int[] starArray, int level, int star)
        {
            try
            {
                if (starArray[level - 1] < star) {
                    starArray[level - 1] = star;
                }
                else
                {
                    return;
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
            
        }

        public GameObject levelContainer;

        internal void UpdateStarCount()// her instantiate edildiğinde
        {
            tryCount++;

            if (tryCount > 15)
            {
                ans2.SetActive(false);
                s2.SetActive(false);
                return;

            }
            else if(tryCount>5)
            {
                ans3.SetActive(false);
                s3.SetActive(false);
            }
        }
        
        public void ShowStarsInList()//
        {   
            int containerCount = levelContainer.transform.childCount;

            for (int i = 0; i < containerCount;)// 5 defa
            {
                Transform childContainer = levelContainer.transform.GetChild(i);// 15,30,45,....
                for (int k = 0; k < childContainer.childCount;)// 15 defa
                {
                    Transform levelOject = childContainer.GetChild(k);
                    IterateOverLevels(levelOject);
                    k++;
                }
                i++;
            }
        }

        private void IterateOverLevels(Transform levelOject)
        {
            int levelNumber = int.Parse(levelOject.name);
            int starCount=0;
            try
            {
                starCount = starsList[levelNumber-1];
            }
            catch(System.Exception e)
            {
                Debug.Log(e.Message);
            }
            
            for (int r = 0; r < starCount;)
            {                       //star holder//
                levelOject.transform.GetChild(1).GetChild(r).gameObject.SetActive(true);
                r++;
            }
        }

        internal void RestartStars()// bölğm yeniden açılınca
        {
            tryCount = 0;
            s1.SetActive(true); s2.SetActive(true); s3.SetActive(true);
            StartCoroutine(StarScoreDelayer());
        }

        private IEnumerator StarScoreDelayer()
        {
            yield return new WaitForSeconds(2.0f);
            ans2.SetActive(true); ans3.SetActive(true);
        }

        internal static void SaveStars(int level)// bölüm tmm eventı ile çağır
        {
            
            if (tryCount == 1)
            {
                isPerfect = true;
                Add_Update_StarArray(starsList,level, 3);
            }
            else if (tryCount < 6)
            {   
                Add_Update_StarArray(starsList, level, 3);
            }
            else if (tryCount > 5 && tryCount < 16)
            {
                
                Add_Update_StarArray(starsList, level, 2);
            }
            else
            {
                Add_Update_StarArray(starsList, level, 1);
            }
            JsonHelper.ArrayToJson<int>(starsList);

        }

        internal static void LoadStars()
        {
            if(File.Exists(jsonFilePath))
            starsList = JsonHelper.getJsonArray<int>(jsonFilePath);
        }

        public class JsonHelper
        { 
            //Usage:
            //YouObject[] objects = JsonHelper.getJsonArray<YouObject> (jsonString);
            public static T[] getJsonArray<T>(string jsonFile)
            {
                jsonFile = File.ReadAllText(jsonFilePath);
                Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(jsonFile);
                return wrapper.array;
            }
            //Usage:
            //string jsonString = JsonHelper.arrayToJson<YouObject>(objects);
            public static void ArrayToJson<T>(T[] array)
            {
                try {

                    Wrapper<T> wrapper = new Wrapper<T> { array = array } ;
                    dataAsJson = JsonUtility.ToJson(wrapper);
                    File.WriteAllText(jsonFilePath, dataAsJson);//yaz
                }
                catch(Exception e)
                {
                    Debug.Log(e.Message); 
                }
             }

            [Serializable]
            private class Wrapper<T>
            {
                public T[] array;
            }

        }



    }
}