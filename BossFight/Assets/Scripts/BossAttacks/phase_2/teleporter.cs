using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using Unity.VisualScripting;

public class teleporter : MonoBehaviour
{
    public Transform centerPoint;
    public float radius = 5f;
    public GameObject teleporter_1; 
    public GameObject teleporter_2; 
    private GameObject entrance;
    private GameObject exit;
    private Animator entrance_anim;
    private Animator exit_anim;
    public void OnAnimationEnd()
    {
        Destroy(gameObject, 2f);
    }



    void Start()
    {

        StartRepeating();
    }

    void Update()
    {

    }

    void Teleport()
    {
        Debug.Log("teleporting!");
        // random point inside the radius
        Vector2 randomPoint = UnityEngine.Random.insideUnitCircle * radius;
        Vector2 newPos = new Vector2(centerPoint.position.x, centerPoint.position.y) + new Vector2(randomPoint.x, randomPoint.y);

        GameObject entrance = Instantiate(teleporter_1, transform, true);

        transform.position = newPos;
        GameObject exit = Instantiate(teleporter_2, transform, true);
        
        entrance_anim = entrance.GetComponent<Animator>();
        exit_anim = exit.GetComponent<Animator>();

        entrance_anim.SetTrigger("tele");
        exit_anim.SetTrigger("tele");
    }
    //show teleportable radius in editor 
    void OnDrawGizmosSelected()
    {
        if (centerPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(centerPoint.position, radius);
        }
    }

    public float repeatRate = 4f;
    private Coroutine repeatCoroutine;
    // Start the repeating coroutine to teleport 
    public void StartRepeating()
    {
        if (repeatCoroutine == null)
        {
            repeatCoroutine = StartCoroutine(CallRepeatedly());
        }
    }

    // Stop the repeating coroutine
    public void StopRepeating()
    {
        if (repeatCoroutine != null)
        {
            StopCoroutine(repeatCoroutine);
            repeatCoroutine = null;  // Optionally reset the reference
        }
    }

    IEnumerator CallRepeatedly()
    {
        while (true)
        {
            Teleport();
            yield return new WaitForSeconds(repeatRate);
        }
    }
}
