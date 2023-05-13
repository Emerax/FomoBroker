public class DividendsStateRunner : IGameStateRunner {
    private readonly float stateTime;

    private float remainingTime;

    public DividendsStateRunner(float stateTime) {
        this.stateTime = stateTime;
        Reset();
    }

    public void Reset() {
        remainingTime = stateTime;
    }

    public bool IsStateOver(out GameState nextState) {
        nextState = GameState.ACTION;
        return remainingTime <= 0;
    }

    public float Run(float elapsed) {
        remainingTime -= elapsed;
        return remainingTime;
    }
}
