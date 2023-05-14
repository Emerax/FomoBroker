using System.Collections.Generic;
using System.Linq;

public class TradingBidStateRunner : IGameStateRunner {
    private readonly int stocksToWin;
    private readonly Dictionary<int, PlayerInventory> inventories;

    private List<StockForSale> stocksForSale;

    public TradingBidStateRunner(List<StockForSale> stocksForSale, int stocksToWin, Dictionary<int, PlayerInventory> inventories) {
        this.stocksForSale = stocksForSale;
        this.stocksToWin = stocksToWin;
        this.inventories = inventories;
        Reset();
    }

    public void Reset() {
    }

    public bool IsStateOver(out GameState nextState) {
        nextState = GameState.ACTION;
        if(WeHaveAWinner()) {
            nextState = GameState.GAME_OVER;
            return true;
        }

        return stocksForSale.Count == 0;
    }

    public float Run(float elapsed) {
        return -1;
    }

    private bool WeHaveAWinner() {
        return inventories.Values.Any(i => i.stocks.Any(c => c >= stocksToWin));
    }
}
