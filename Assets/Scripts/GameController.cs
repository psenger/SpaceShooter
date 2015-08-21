using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText scoreText;

	private bool gameOver;
	private bool restart;

	private int score;

	/** Called the first frame **/
	void Start() {

		gameOver = false;
		restart = false;

		restartText.text = "";
		gameOverText.text = "";

		score = 0;
		UpdateScore ();
		StartCoroutine ( SpawnWaves () );
	}

	public void Update(){
		if (restart) {
			if (Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel( Application.loadedLevel );
			}
		}
	} 
	
	IEnumerator SpawnWaves() {

		while( true ) {
			yield return new WaitForSeconds( startWait );
			for ( int i = 0; i < hazardCount; i++ ) {
				float x = Random.Range ( -spawnValues.x, spawnValues.x);
				Vector3 spawnPosition = new Vector3 ( x, spawnValues.y, spawnValues.z );
				Quaternion spawnRotation = Quaternion.identity; // new Quaternion ();
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds( spawnWait );
			}

			yield return new WaitForSeconds( waveWait );

			if ( gameOver ) {
				restartText.text = "Press 'R' for restart";
				restart = true;
				break;
			}
		}

	}

	public void AddScore( int newScoreValue ){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}

}
