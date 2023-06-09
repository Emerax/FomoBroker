public class TradingSelectStateRunner : IGameStateRunner {
    private readonly float stateTime;

    private float remainingTime;

    public TradingSelectStateRunner(float stateTime) {
        this.stateTime = stateTime;
        Reset();
    }

    public void Reset() {
        remainingTime = stateTime;
    }

    public bool IsStateOver(out GameState nextState) {
        nextState = GameState.TRADING_BID;
        return remainingTime <= 0;
    }

    public float Run(float elapsed) {
        remainingTime -= elapsed;
        return remainingTime;
    }
}
