using UnityEngine;

public class DraculaHypnoBeam : MonoBehaviour
{

    public float beamDuration = 3f;
    public float hypnotizedDuration = 2f;
    public float escapeMashCount = 5;
    public GameObject beamVisual; // Assign visual GameObject (turn on/off)

    private Transform player;
    private PlayerHypno playerHypnosis; // Script on player

    private bool beamActive = false;
    private float beamEndTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beamVisual.SetActive(false);
    }

    public void ActivateBeam(Transform playerTransform)
    {
        player = playerTransform;
        playerHypnosis = player.GetComponent<PlayerHypno>();

        beamActive = true;
        beamEndTime = Time.time + beamDuration;
        beamVisual.SetActive(true);
        Debug.Log("Dracula uses Hypno Beam!");
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
