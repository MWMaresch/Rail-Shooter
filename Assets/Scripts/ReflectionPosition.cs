using UnityEngine;

public class ReflectionPosition : MonoBehaviour {

    public GameObject waterSurface;
    private Transform mainCam;
    private float waterPosY;

	// Use this for initialization
	void Start () {
        //so we don't have to keep looking for it every frame
        mainCam = Camera.main.transform;
        waterPosY = waterSurface.transform.position.y;
	}
	
	void FixedUpdate () {
        transform.position = new Vector3(mainCam.position.x, waterPosY*2f - mainCam.position.y, mainCam.position.z);

    }
}
