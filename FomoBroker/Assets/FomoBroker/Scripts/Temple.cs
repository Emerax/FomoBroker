using UnityEngine;

public class Temple : MonoBehaviour {
    private float attraction = 10;

    public float Attraction { get => attraction; }

    public void ChangeAttraction(float value) {
        attraction += value;
        attraction = Mathf.Clamp(attraction, 10, Mathf.Infinity);
    }
}
