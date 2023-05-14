using UnityEngine;

public class StockButton : MonoBehaviour {
    bool selectedForSelling = false;
    public Transform forSaleSymbol;
    public int index;

    public System.Action<int, bool> onClicked;

    // Start is called before the first frame update
    void Start() {
        forSaleSymbol.gameObject.SetActive(selectedForSelling);
    }

    // Update is called once per frame

    private void OnMouseDown() {
        onClicked.Invoke(index, !selectedForSelling);
    }

    public void SetForSale(bool forSale) {
        selectedForSelling = forSale;
        forSaleSymbol.gameObject.SetActive(selectedForSelling);
    }
}
