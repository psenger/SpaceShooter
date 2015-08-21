using UnityEngine;
using System.Collections;

/**
 * Adding [System.Serializable] to a class exposes it to the Unity UI
 **/
[System.Serializable]
public class Boundry {
	public float xMin;
	public float xMax;
	public float zMin;
	public float zMax;
}

public class PlayerController : MonoBehaviour {

	public Rigidbody rb;

	public float speed;
	public float tilt;
	public Boundry boundry;

	public GameObject shot;
	public Transform shotSpawnPoint;

	public float fireRate;

	private float nextFire;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	/**
	 * There are two updates. 
	 * + Update is called just before updating every frame.
	 **/
	void Update(){

		// fire bolts
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			// because we do not need to track the object we have in the seen we dont need track the reference.
			// GameObject clone = Instantiate( shot, shotSpawnPoint.position, shotSpawnPoint.rotation ) as GameObject;
			Instantiate( shot, shotSpawnPoint.position, shotSpawnPoint.rotation );

			// play the weapon audio
			GetComponent<AudioSource>().Play();
		} 
	}

	/**
	 * There are two updates.
	 * + FixedUpdate is called automatically before each physic step and once per step 
	 */
	void FixedUpdate() {
		/** 
		 * gets the input from the user 
		 *   -> we will modify the Rigidbody property on the player object.
		 * 
		 **/
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical"); 

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		// restrict the movement by clamping it
		float xRange = Mathf.Clamp(rb.position.x, boundry.xMin, boundry.xMax);
		float yRange = 0.0f;
		float zRange = Mathf.Clamp(rb.position.z, boundry.zMin, boundry.zMax);
		rb.position = new Vector3 (
			xRange, 
			yRange, 
			zRange
			);

		/** tilt the ship **/
		rb.rotation = Quaternion.Euler(0.0f,0.0f, rb.velocity.x * -tilt );


	}

}
