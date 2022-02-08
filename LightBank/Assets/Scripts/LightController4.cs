using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ExtensionsClass;

public class LightController4 : MonoBehaviour
{
    public Light SceneMaster;

    public float attractorDelay;
    public float repeatRate;

    public bool shuffleLights;

    private Light[] lights;

    public int StepOfN;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneMaster == null)
        {
            Debug.LogErrorFormat("The scene master light was not set in the inpector for {0}", name);
            return;
        }
        else if (SceneMaster.type != LightType.Directional)
        {
            Debug.LogErrorFormat("The SceneMaster was set to a {0} rather than a Directional Light in the inspector for {1}", SceneMaster.type, name);
            return;
        }
        lights = GetComponentsInChildren<Light>();
        if (lights == null)
        {
            Debug.LogErrorFormat("There are no lights under {0}", name);
            return;
        }
        if (shuffleLights)
        {
            RunShuffle();
        }
        ClearLights();
        InvokeRepeating("Attractor", attractorDelay, repeatRate);

    }

    void Attractor()
    {
        ClearLights();
        lights[StepOfN].gameObject.SetActive(true);
        StepOfN++;
        if (StepOfN == lights.Length)
        {
            if (shuffleLights)
            {
                RunShuffle();
            }
            StepOfN = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelInvoke("Attractor");
            ClearLights();
            SceneMaster.gameObject.SetActive(true);
        }
        
    }

    void RunShuffle()
    {
        List<Light> temp = lights.ToList();
        temp.Shuffle();
        lights = temp.ToArray();
    }

    void ClearLights()
    {
        foreach (Light light in lights)
            light.gameObject.SetActive(false);
    }
}
