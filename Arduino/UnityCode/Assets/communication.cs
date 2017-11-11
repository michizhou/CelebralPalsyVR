﻿using UnityEngine;
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
            print(sp.ReadLine());
        }
        catch (System.Exception)
        {
        }
    }
}