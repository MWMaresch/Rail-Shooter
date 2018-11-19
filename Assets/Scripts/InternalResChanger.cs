using UnityEngine;
using UnityEngine.UI;

public class InternalResChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UpdateResolution();
        //Debug.Log("internal res width is " + GlobalOptions.InternalWidth);
    }

    public void UpdateResolution()
    {
        GetComponent<RawImage>().texture.width = GlobalOptions.InternalWidth;
        GetComponent<RawImage>().texture.height = GlobalOptions.InternalHeight;
    }
}
