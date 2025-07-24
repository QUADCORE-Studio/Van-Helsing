using UnityEngine;

public class anim_destroyer : MonoBehaviour
{
    // Start is calle
    // d once before the first execution of Update after the MonoBehaviour is created
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
        Debug.Log("destroying.", gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        
    }
}
