using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.VR;

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

            int lowerLimit = 250;
            int upperLimit = 260;

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
                print(InputTracking.GetLocalRotation(VRNode.CenterEye));
                print("X: " + angles.eulerAngles.x + "  Y: " + angles.eulerAngles.y + "  Z: " + angles.eulerAngles.z);
                */

                transform.position += transform.forward *1;
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