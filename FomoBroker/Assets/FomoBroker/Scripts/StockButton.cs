using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockButton : MonoBehaviour
{
    Vector3 restScale;
    float hoverT = 0.0f;
    bool isHovered = false;
    bool selectedForSelling = false;
    public Transform forSaleSymbol;
    public int index;

    [SerializeField]
    public System.Action<int, bool> onClicked;

    // Start is called before the first frame update
    void Start()
    {
        restScale = transform.localScale;
        forSaleSymbol.gameObject.SetActive(selectedForSelling);
    }

    // Update is called once per frame
    void Update()
    {
        if(isHovered) hoverT = Mathf.MoveTowards(hoverT, 1.0f, Time.deltaTime * 5.0f);
        else hoverT = Mathf.MoveTowards(hoverT, 0.0f, Time.deltaTime * 5.0f);

        transform.localScale = restScale * (1.0f+hoverT*hoverT*0.2f);

        if(isHovered && Input.GetMouseButtonDown(0)) {
            onClicked(index, !selectedForSelling);
        }
    }

    void OnMouseEnter() {
        Debug.Log("Mouse enter");
        isHovered = true;
    }

    void OnMouseExit() {
        Debug.Log("Mouse exit");
        isHovered = false;
    }

    public void SetForSale(bool forSale) {
        selectedForSelling = forSale;
        forSaleSymbol.gameObject.SetActive(selectedForSelling);
    }
}
