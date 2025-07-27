using UnityEngine;
using System.Collections;
using System;

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
    public float offset;
    public bool toggleTP;
    void Start()
    {
        
    }

    void Update()
    {
        if (toggleTP) StartRepeating();
        else
        {
            StopRepeating();
        }
    }

    void Teleport()
    {
        // random point inside the radius
        Vector2 randomPoint = UnityEngine.Random.insideUnitCircle * radius;
        Vector2 newPos = new Vector2(centerPoint.position.x, centerPoint.position.y) + new Vector2(randomPoint.x, randomPoint.y);

        GameObject entrance = Instantiate(teleporter_1, transform.position, Quaternion.identity);

        transform.position = newPos;
        Vector2 spawnPos = (Vector2)transform.position + new Vector2(0, offset);
        GameObject exit = Instantiate(teleporter_2, spawnPos, Quaternion.identity);

        entrance_anim = entrance.GetComponent<Animator>();
        exit_anim = exit.GetComponent<Animator>();
        entrance_anim.SetTrigger("tele");
        Destroy(entrance, 1f);

        exit_anim.SetTrigger("tele");
        Destroy(exit, 1f);
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

    public float repeatRate = 7f;
    private Coroutine repeatCoroutine;
    public void StartRepeating()
    {
        if (repeatCoroutine == null)
        {
            repeatCoroutine = StartCoroutine(CallRepeatedly());
        }
    }

    public void StopRepeating()
    {
        if (repeatCoroutine != null)
        {
            StopCoroutine(repeatCoroutine);
            repeatCoroutine = null;
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
