using UnityEngine;

public class Temple : MonoBehaviour {
    [SerializeField]
    private ParticleSystem trashParticles;
    [SerializeField]
    private ParticleSystem hypeParticles;
    private float attraction = 10;

    public float Attraction { get => attraction; }
    public Transform MoneySpawnTransform => trashParticles.transform;

    public void ChangeAttraction(float value) {
        attraction += value;
        attraction = Mathf.Clamp(attraction, 10, Mathf.Infinity);
        if(value > 0) {
            hypeParticles.Play();
        }
        else {
            trashParticles.Play();
        }
    }
}
