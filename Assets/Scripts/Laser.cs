using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public Sprite splash;

    //public Camera cam;
    private GameObject crosshair;
    private float lifeTime = 3f;
    private Vector3 direction;
    private bool splashed = false;
    // Use this for initialization
    void Start ()
    {
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Vector3 crosshairScreenPos = Camera.main.WorldToScreenPoint(crosshair.transform.position);
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(new Vector3(crosshairScreenPos.x, crosshairScreenPos.y, 50f));
        transform.LookAt(aimPos);
        direction = transform.forward;
        transform.forward = -Camera.main.transform.forward;
        transform.position += direction *2f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!splashed)
            transform.Rotate(new Vector3(0, 0, 1f), 45f);

        transform.position += direction;
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            splashed = true;
            GetComponent<Animator>().enabled = true;
            lifeTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            transform.position = new Vector3(transform.position.x, -2.6f, transform.position.z);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            direction = new Vector3(0, 0, -0.5f);
        }
    }
}
