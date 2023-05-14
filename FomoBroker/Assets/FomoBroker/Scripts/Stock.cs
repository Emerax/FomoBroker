using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour {
    public int religion1 = 0, religion2 = 0, religion3 = 0; //religion=numshares
    [SerializeField]
    private List<GameObject> religion1Shares = new();
    [SerializeField]
    private List<GameObject> religion2Shares = new();
    [SerializeField]
    private List<GameObject> religion3Shares = new();

    public List<StockButton> stockButtons;

    public void UpdateVisuals(int[] stocks) {
        religion1 = stocks[0];
        religion2 = stocks[1];
        religion3 = stocks[2];
        // update visibility of regligion=numshares to show corresponding shares gameobjects
        //religion 1
        for(int i = 0; i < religion1Shares.Count; i++) {
            religion1Shares[i].SetActive(i < religion1);
        }

        for(int i = 0; i < religion2Shares.Count; i++) {
            religion2Shares[i].SetActive(i < religion2);
        }

        for(int i = 0; i < religion3Shares.Count; i++) {
            religion3Shares[i].SetActive(i < religion3);
        }
    }
}
