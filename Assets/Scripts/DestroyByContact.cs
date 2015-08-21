using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log("Cannot find the 'GameController'");
		}
	}

	void OnTriggerEnter(Collider other) {
		// Debug.Log (other.name);
		// Debug.Log (other.tag);
		if ( other.tag == "Boundary" ) {
			return;
		}

		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}

		gameController.AddScore (scoreValue);
		Destroy( other.gameObject );
		Destroy( gameObject );
	}
}
