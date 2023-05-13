
using UnityEngine;

public class ActionStateRunner : IGameStateRunner {
    private readonly float stateTime;

    private float remainingTime;

    public ActionStateRunner(float stateTime) {
        this.stateTime = stateTime;
        Reset();
    }

    public void Reset() {
        remainingTime = stateTime;
    }

    public float Run(float elapsed) {
        remainingTime -= elapsed;
        Debug.Log($"Remaining time now {remainingTime}");
        return remainingTime;
    }

    public bool IsStateOver(out GameState nextState) {
        nextState = GameState.MIGRATION;
        return remainingTime <= 0;
    }
}
