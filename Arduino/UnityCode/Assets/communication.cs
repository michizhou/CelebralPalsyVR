using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class communication : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 9600);

    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }

    void Update()
    {


        try
        {
            //print(sp.ReadLine());
            string sentence = sp.ReadLine();

            //string[] splitArray = sentence.Split((new string[] , char.Parse("/"));
            string[] arr = sentence.Split('/');

            /*
            print(arr[0]);
            print(arr[1]);
            print(arr[2]);
            print("----------");
            */

            int XVal = int.Parse(arr[0]);
            int YVal = int.Parse(arr[1]);
            int ZVal = int.Parse(arr[2]);

            int lowerLimit = 260;
            int upperLimit = 270;

            int fluctRange = 7;
            bool walking = false;

    
            if ((lowerLimit - fluctRange <= XVal && XVal <= upperLimit + fluctRange) &&
                 (lowerLimit - fluctRange <= YVal && YVal <= upperLimit + fluctRange) &&
                 (lowerLimit - fluctRange <= ZVal && ZVal <= upperLimit + fluctRange))
            {
                // standing
                print("Standing");
                walking = false;
            }
            else {
                // walking
                print("Walking");
                walking = true;

                /*
                print(XVal);
                print(YVal);
                print(ZVal);
                print("----------");
                */
            }

        }
        catch (System.Exception)
        {
        }
    }
}