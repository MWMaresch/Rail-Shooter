using UnityEngine;
using UnityEngine.UI;

public class InternalResChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture.width = GlobalOptions.InternalWidth;
        GetComponent<RawImage>().texture.height = GlobalOptions.InternalHeight;
        //Debug.Log("internal res width is " + GlobalOptions.InternalWidth);
    }
}
