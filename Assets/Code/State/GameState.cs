using UnityEngine;

/// <summary>
/// Attach this script to an empty GameObject called GameState. Then, it can be
/// accessed with GameObject.Find ("GameState").GetComponent<GameState> ();
/// </summary>
public class GameState : MonoBehaviour
{
	public PlayerWaves playerWaves; 
	public static GameState gameState;

	void Awake() {
		// Make sure there's only one instance of the game state
		gameState = this;

		if (gameState.playerWaves == null) {
			gameState.playerWaves = new PlayerWaves ();
		}
	}
}
