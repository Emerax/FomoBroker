using System.Collections.Generic;
public class TradingBidStateRunner : IGameStateRunner {
    private readonly float stateTime;

    private List<StockForSale> stocksForSale;

    public TradingBidStateRunner(List<StockForSale> stocksForSale) {
        this.stocksForSale = stocksForSale;
        Reset();
    }

    public void Reset() {
    }

    public bool IsStateOver(out GameState nextState) {
        nextState = GameState.ACTION;
        return stocksForSale.Count == 0;
    }

    public float Run(float elapsed) {
        return -1;
    }
}
