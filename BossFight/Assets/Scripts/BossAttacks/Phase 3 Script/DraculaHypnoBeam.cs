using UnityEngine;

public class DraculaHypnoBeam : MonoBehaviour
{

    public float beamDuration = 2f;
    public float hypnotizedDuration = 2f;
    public float escapeMashCount = 5;
    public GameObject beamVisual; // Assign visual GameObject (turn on/off)

    public Transform player;
    private PlayerHypno playerHypnosis; // Script on player

    private bool beamActive = false;
    private float beamEndTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        beamVisual.SetActive(false);
    }

    public void ActivateBeam(Transform playerTransform)
    {
        playerHypnosis = player.GetComponent<PlayerHypno>();

        Vector3 dir = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        beamVisual.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        beamActive = true;
        beamEndTime = Time.time + beamDuration;
        beamVisual.SetActive(true);
    }

    void Update()
    {
        if (!beamActive) return;

        if (Time.time >= beamEndTime)
        {
            DeactivateBeam();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (beamActive && other.CompareTag("Player") && playerHypnosis != null)
        {
            playerHypnosis.BeginHypnosis(hypnotizedDuration, escapeMashCount, transform.position);
        }
    }

    void DeactivateBeam()
    {
        beamActive = false;
        beamVisual.SetActive(false);
    }
}
