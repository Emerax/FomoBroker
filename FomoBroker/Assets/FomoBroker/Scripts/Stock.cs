using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    public int religion1=0,religion2= 0,religion3= 0; //religion=numshares
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()

    {

         // update visibility of regligion=numshares to show corresponding shares gameobjects
         //religion 1
         switch(religion1)
            {
            case 1: 
                GameObject.Find ("/Stocks/Religion_1/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
                GameObject.Find ("/Stocks/Religion_1/share_2").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_1/share_3").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_1/share_4").transform.localScale = new Vector3(0, 0, 0);
            //Stocks.SetActive(false);
            Debug.Log("1");
            break;
            case 2:
            Debug.Log("2");
            GameObject.Find ("/Stocks/Religion_1/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_1/share_2").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_1/share_3").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find ("/Stocks/Religion_1/share_4").transform.localScale = new Vector3(0, 0, 0);
            break;
            case 3:
            Debug.Log("3");
            GameObject.Find ("/Stocks/Religion_1/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_1/share_2").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_1/share_3").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_1/share_4").transform.localScale = new Vector3(0, 0, 0);
            break;
            case 4:
            Debug.Log("4");
            GameObject.Find ("/Stocks/Religion_1/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_1/share_2").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_1/share_3").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_1/share_4").transform.localScale = new Vector3(1, 0.1625f, 1);
            break;
            default: //case that happens if no case matches
                GameObject.Find ("/Stocks/Religion_1/share_1").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_1/share_2").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_1/share_3").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_1/share_4").transform.localScale = new Vector3(0, 0, 0);
            Debug.Log("religion 1: no stocks");
            break;
            }
        
        //religion 2
         switch(religion2)
            {
            case 1: 
                GameObject.Find ("/Stocks/Religion_2/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
                GameObject.Find ("/Stocks/Religion_2/share_2").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_2/share_3").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_2/share_4").transform.localScale = new Vector3(0, 0, 0);
            //Stocks.SetActive(false);
            Debug.Log("1");
            break;
            case 2:
            Debug.Log("2");
            GameObject.Find ("/Stocks/Religion_2/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_2/share_2").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_2/share_3").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find ("/Stocks/Religion_2/share_4").transform.localScale = new Vector3(0, 0, 0);
            break;
            case 3:
            Debug.Log("3");
            GameObject.Find ("/Stocks/Religion_2/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_2/share_2").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_2/share_3").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_2/share_4").transform.localScale = new Vector3(0, 0, 0);
            break;
            case 4:
            Debug.Log("4");
            GameObject.Find ("/Stocks/Religion_2/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_2/share_2").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_2/share_3").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_2/share_4").transform.localScale = new Vector3(1, 0.1625f, 1);
            break;
            default: //case that happens if no case matches
                GameObject.Find ("/Stocks/Religion_2/share_1").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_2/share_2").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_2/share_3").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_2/share_4").transform.localScale = new Vector3(0, 0, 0);
            Debug.Log("religion 2: no stocks");
            break;
            }
        
        //religion 3
         switch(religion3)
            {
            case 1: 
                GameObject.Find ("/Stocks/Religion_3/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
                GameObject.Find ("/Stocks/Religion_3/share_2").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_3/share_3").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_3/share_4").transform.localScale = new Vector3(0, 0, 0);
            //Stocks.SetActive(false);
            Debug.Log("1");
            break;
            case 2:
            Debug.Log("2");
            GameObject.Find ("/Stocks/Religion_3/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_3/share_2").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_3/share_3").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find ("/Stocks/Religion_3/share_4").transform.localScale = new Vector3(0, 0, 0);
            break;
            case 3:
            Debug.Log("3");
            GameObject.Find ("/Stocks/Religion_3/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_3/share_2").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_3/share_3").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_3/share_4").transform.localScale = new Vector3(0, 0, 0);
            break;
            case 4:
            Debug.Log("4");
            GameObject.Find ("/Stocks/Religion_3/share_1").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_3/share_2").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_3/share_3").transform.localScale = new Vector3(1, 0.1625f, 1);
            GameObject.Find ("/Stocks/Religion_3/share_4").transform.localScale = new Vector3(1, 0.1625f, 1);
            break;
            default: //case that happens if no case matches
                GameObject.Find ("/Stocks/Religion_3/share_1").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_3/share_2").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_3/share_3").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find ("/Stocks/Religion_3/share_4").transform.localScale = new Vector3(0, 0, 0);
            Debug.Log("no stocks");
            break;
            }
         //cat.SetActive(false);  false to hide, true to show
        
        //print num shares per religion
        print("R1: "+religion1);
        print("R2: "+religion2);
        print("R3: "+religion3);
    }
}
