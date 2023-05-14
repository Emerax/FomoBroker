using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FloatingText : MonoBehaviour
{
    public TextMeshPro tmp;
    public Vector3 velocity;
    public float fadeOutDuration;
    public float lifeTime;

    public void SetMoney(int money) {
        if(money < 0) {
            tmp.color = new Color(1, 0, 0);
        }
        else {
            tmp.color = new Color(0, 1, 0);
        }
        tmp.text = $"${money}";
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + velocity * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if(lifeTime < fadeOutDuration && fadeOutDuration > 0.0f) {
            float alpha = lifeTime / fadeOutDuration;
            Color c = tmp.color;
            c.a = alpha;
            tmp.color = c;
        }
        if(lifeTime < 0.0f) {
            GameObject.Destroy(gameObject);
        }
    }
}
