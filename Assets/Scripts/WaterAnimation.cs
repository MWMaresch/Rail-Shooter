using UnityEngine;

public class WaterAnimation : MonoBehaviour {

    private float waterSpeed;
    private float offset = 0f;
    

    // Use this for initialization
    void Start ()
    {
        waterSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().zSpeed * 0.1f;
        GetComponent<Renderer>().sortingLayerName = "Background";
        GetComponent<MeshRenderer>().material.SetInt("_ZWrite", 0);
        GetComponent<MeshRenderer>().material.renderQueue = 3000;
    }
	
	// FixedUpdate is called once every 16ms
	void FixedUpdate ()
    {
        offset += waterSpeed;
        if (offset > 16f)
            offset -= 16f;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, -offset);
        GetComponent<Renderer>().material.SetTextureOffset("_DetailAlbedoMap", new Vector2(0,-offset));
        GetComponent<Renderer>().material.SetTextureOffset("_OcclusionMap", new Vector2(0, -offset));

    }
}
