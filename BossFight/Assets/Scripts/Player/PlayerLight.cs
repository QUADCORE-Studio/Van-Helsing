using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class light : MonoBehaviour
{
    private Light2D myLight;
    public float radius_offset;
    private Coroutine pulseCourtine;
    public float repeatRate = 7f;
    public float duration = 5f;
    public bool pulsating_flag;
    public bool slide_flag;
    private string key = "000";
    private List<float> tees;
    public List<float> min_nums;
    private List<float> max_nums;
    private List<float> I_offset;
    private List<float> IR_offset;
    private List<float> OR_offset;
    void Start()
    {
        myLight = GetComponent<Light2D>();
        max_nums = new List<float>() { myLight.intensity, myLight.pointLightInnerRadius, myLight.pointLightOuterRadius };
        I_offset = new List<float>() { max_nums[0]/2 - max_nums[0]/2, max_nums[0]/2 + max_nums[0]/2};
        IR_offset = new List<float>() { max_nums[1]/2 - max_nums[1]/2, max_nums[1]/2 + max_nums[1]/2};
        OR_offset = new List<float>() { max_nums[2]/2 - max_nums[2]/2, max_nums[2]/2 + max_nums[2]/2};
        tees = new List<float>() { 0f, 0f, 0f };
        Debug.Log(I_offset);
        Debug.Log(IR_offset);
        Debug.Log(OR_offset);
    }

    void Update()
    {
        if (pulsating_flag)
        {
            if (myLight.pointLightInnerRadius < min_nums[1])
            {
                
            }
            Debug.Log("pulsating");
            if (slide_flag)
            {
                tees[0] += Time.deltaTime / duration;
                tees[1] += Time.deltaTime / duration;
                tees[2] += Time.deltaTime / duration;
            }
            else
            {
                tees[0] -= Time.deltaTime / duration;
                tees[1] -= Time.deltaTime / duration;
                tees[2] -= Time.deltaTime / duration;
            }
            tees[0] = Mathf.Clamp01(tees[0]);
            tees[1] = Mathf.Clamp01(tees[1]);
            tees[2] = Mathf.Clamp01(tees[2]);
            
            // pulse(tees[0], key);

            // if (tees[0] >= 1f) slide_flag = false;
            // if (tees[0] <= 0f) slide_flag = true;
        }
        else
        {
            myLight.intensity = max_nums[0];
        }
        
    }
    void pulsateToggle()
    {
        if (pulsating_flag)
        {
            pulsating_flag = false;
        }
        else
        {
            pulsating_flag = true;
        }
    }
    void pulse(float t, string key)
    {
        if (key[0] == '1')
        {
            myLight.intensity = Mathf.Lerp(I_offset[0], I_offset[1], t);
        }
        if (key[1] == '1')
        {
            myLight.pointLightInnerRadius = Mathf.Lerp(IR_offset[0], IR_offset[1], t);
        }
        if (key[2] == '1')
        {
            myLight.pointLightOuterRadius = Mathf.Lerp(OR_offset[0], OR_offset[1], t);
        }
    }

    // IEnumerator CallRepeatedly()
    // {
    //     while (true)
    //     {
    //         pulse();
    //         yield return new WaitForSeconds(repeatRate);
    //     }
    // }
}
