using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light lightA;
    public Light lightB;
    public Light lightC;
    public Light lightD;

    public int StepOf4;

    // Start is called before the first frame update
    void Start()
    {
        lightA.gameObject.SetActive(true);
        lightB.gameObject.SetActive(false);
        lightC.gameObject.SetActive(false);
        lightD.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearLights();
            StepOf4++;
            if (StepOf4 == 4)
                StepOf4 = 0;

            switch (StepOf4)
            {
                case 0:
                    lightA.gameObject.SetActive(true);
                    break;
                case 1:
                    lightB.gameObject.SetActive(true);
                    break;
                case 2:
                    lightC.gameObject.SetActive(true);
                    break;
                case 3:
                    lightD.gameObject.SetActive(true);
                    break;
            }
        }
    }

    private void ClearLights()
    {
        lightA.gameObject.SetActive(false);
        lightB.gameObject.SetActive(false);
        lightC.gameObject.SetActive(false);
        lightD.gameObject.SetActive(false);
    }
}
