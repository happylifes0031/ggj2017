using UnityEngine;

/// <summary>
/// Attach this script to an empty GameObject called GameState. Then, it can be
/// accessed with GameObject.Find ("GameState").GetComponent<GameState> ();
/// </summary>
public class GameState : MonoBehaviour
{
	public Enemies enemies { get; private set; }
	public PlayerWaves playerWaves { get; private set; }
	public Horde horde { get; private set; }
	public static GameState gameState;

	void Awake()
	{
		// Make sure there's only one instance of the game state
		gameState = this;
		playerWaves = this.gameObject.GetComponent<PlayerWaves>();
		enemies = this.gameObject.GetComponent<Enemies>();
		horde = this.gameObject.GetComponent<Horde>();
	}
}