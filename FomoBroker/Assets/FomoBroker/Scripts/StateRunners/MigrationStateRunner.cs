public class MigrationStateRunner : IGameStateRunner {
    private readonly RunManager runManager;

    public MigrationStateRunner(RunManager runManager) {
        this.runManager = runManager;
    }

    public bool IsStateOver(out GameState nextState) {
        nextState = GameState.DIVIDENDS;
        return !runManager.RunnersAreRunning;
    }

    public void Reset() { }

    public float Run(float elapsed) {
        return -1;
    }
}
