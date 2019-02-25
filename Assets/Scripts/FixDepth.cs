using UnityEngine;

public class FixDepth : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    public bool adjustTransparency = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate () {
        spriteRenderer.sortingOrder = (int)Mathf.Floor(-transform.position.z);
        if (adjustTransparency && transform.position.z < 23.2f)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (transform.position.z-18f) / 7f);
        }

    }
}
