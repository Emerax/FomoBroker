public interface IGameStateRunner {
    public void Reset();
    /// <summary>
    /// "Update" the state. Returns time remaining in state. Or -1 if unknown.
    /// </summary>
    public float Run(float elapsed);

    public bool IsStateOver(out GameState nextState);
}
