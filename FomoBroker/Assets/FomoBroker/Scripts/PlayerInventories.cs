using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventories : NetworkBehaviour {
    [SerializeField]
    private FusionAPI fusion;
    [SerializeField]
    private int startingMoney = 100;
    private Dictionary<int, int> PlayerMoney;
    private readonly Dictionary<int, Stock> PlayerStocks;

    public int MyMoney { get => PlayerMoney[fusion.PlayerID]; set => PlayerMoney[fusion.PlayerID] = value; }
    public Stock MyStocks { get => PlayerStocks[fusion.PlayerID]; set => PlayerStocks[fusion.PlayerID] = value; }

    private void Awake() {
        PlayerMoney = new Dictionary<int, int>() {
            [0] = startingMoney,
            [1] = startingMoney,
            [2] = startingMoney,
        };
    }

    public override void FixedUpdateNetwork() {
        if(Input.anyKeyDown) {
            MyMoney -= 5;
        }
    }


}
