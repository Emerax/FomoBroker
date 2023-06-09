using UnityEngine;

public class HoverScalable : MonoBehaviour {
    Vector3 restScale;
    Vector3 restPosition;
    float hoverT = 0.0f;
    bool isHovered = false;
    public Vector3 positionDelta;

    // Start is called before the first frame update
    void Start() {
        restScale = transform.localScale;
        restPosition = transform.localPosition;
    }

    private void OnMouseEnter() {
        isHovered = true;
    }

    private void OnMouseExit() {
        isHovered = false;
    }

    // Update is called once per frame
    void Update() {
        if(isHovered) hoverT = Mathf.MoveTowards(hoverT, 1.0f, Time.deltaTime * 5.0f);
        else hoverT = Mathf.MoveTowards(hoverT, 0.0f, Time.deltaTime * 5.0f);

        transform.localScale = restScale * (1.0f + hoverT * hoverT * 0.2f);
        transform.localPosition = restPosition + positionDelta * (hoverT * hoverT);
    }
}
