using UnityEngine;

public class WaterDraft : MonoBehaviour {

    public float randXScale;
    public float randYScale;
    public float minimumSize;
    public GameObject waveL;
    public GameObject waveR;

    private GameObject player;
    private float distScale;
    private bool renderToggle;

    void Start()
    {
        renderToggle = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per 16ms
    void FixedUpdate () {
        renderToggle = !renderToggle;
        waveL.GetComponent<Renderer>().enabled = renderToggle;
        waveR.GetComponent<Renderer>().enabled = renderToggle;
        if (player.transform.position.y < -1.7f)
        {
            distScale = Mathf.Max(minimumSize, (-player.transform.position.y - 1.7f));
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.localScale = new Vector3(distScale + Random.Range(-randXScale, randXScale), distScale * (1 + Random.Range(-randYScale, randYScale)), 1);
        }
        else
            transform.localScale = Vector3.zero;
    }
}
