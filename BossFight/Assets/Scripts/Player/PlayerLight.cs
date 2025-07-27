using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class light : MonoBehaviour
{
    private Light2D myLight;
    public float radius_offset;
    public float duration = 5f;
    public bool pulsating_flag;
    public List<float> min_nums;
    public List<float> max_nums;
    public List<float> tees;
    public List<bool> flags;

    void Start()
    {
        myLight = GetComponent<Light2D>();
        flags = new List<bool>() { true, true, true };
        tees = new List<float>() { 0f, 0f, 0f };

    }

    void Update()
    {
        if (pulsating_flag)
        {
            if (myLight.intensity >= max_nums[0])
            {
                flags[0] = false;
            }
            else if (myLight.intensity <= min_nums[0])
            {
                flags[0] = true;
            }
            if (myLight.pointLightInnerRadius >= max_nums[1])
            {
                flags[1] = false;
            }
            else if (myLight.pointLightInnerRadius <= min_nums[1])
            {
                flags[1] = true;
            }
            if (myLight.pointLightOuterRadius >= max_nums[2])
            {
                flags[2] = false;
            }
            else if (myLight.pointLightOuterRadius <= min_nums[2])
            {
                flags[2] = true;
            }

            for (int i = 0; i < 3; i++)
            {
                if (flags[i])
                {
                    tees[i] += Time.deltaTime / duration;
                }
                else
                {
                    tees[i] -= Time.deltaTime / duration;
                }
                tees[i] = Mathf.Clamp01(tees[i]);

                pulse(tees[i], i);

            }
        }
        else
        {
            myLight.intensity = max_nums[0]/2;
        }
        
    }
    void pulsateToggle()
    {
        pulsating_flag = !pulsating_flag;
    }
    void pulse(float t, int key)
    {
        if (key == 0)
        {
            myLight.intensity = Mathf.SmoothStep(min_nums[0], max_nums[0], t);
        }
        if (key == 1)
        {
            myLight.pointLightInnerRadius = Mathf.SmoothStep(min_nums[1], max_nums[1], t);
        }
        if (key == 2)
        {
            myLight.pointLightOuterRadius = Mathf.SmoothStep(min_nums[2], max_nums[2], t);
        }
    }

}
