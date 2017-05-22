using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
	public float upForce;					//Upward force of the "flap".
	private bool isDead = false;			//Has the player collided with a wall?


	private Animator anim;					//Reference to the Animator component.
	private Rigidbody2D rb2d;				//Holds a reference to the Rigidbody2D component of the bird.

	[SerializeField]
	//private Vector2 _currentPosition;

	void Start()
	{
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
		//_currentPosition = transform.position;
	}

	void Update()
	{
		//Don't allow control if the bird has died.
		if (isDead == false) 
		{
  
			//Look for input to trigger a "flap".
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Joystick1Button0)) 
			{
				//...tell the animator about it and then...
				anim.SetTrigger("Flap");
				//...zero out the birds current y velocity before...
				rb2d.velocity = Vector2.zero;
				//	new Vector2(rb2d.velocity.x, 0);
				//..giving the bird some upward force.
				rb2d.AddForce(new Vector2(0, upForce));
				//transform.position = new Vector2(_currentPosition.x, transform.position.y + 0.5f);
			}

			//if (Input.GetMouseButtonDown (1) || Input.GetKeyDown (KeyCode.DownArrow)) {
			//	anim.SetTrigger("Flap");
			//	transform.position = new Vector2(_currentPosition.x, transform.position.y - 0.5f);
			//}
		}
	}

    void OnCollisionEnter2D(Collision2D other)
    {
		if (other.gameObject.tag == "Scenery") 
		{
			// Zero out the bird's velocity
			rb2d.velocity = Vector2.zero;
			rb2d.constraints = RigidbodyConstraints2D.None;
			// If the bird collides with something set it to dead...
			if (isDead == false) { 
				isDead = true;
				//...tell the Animator about it...
				anim.SetTrigger ("Die");
				//...and tell the game control about it.
				GameControl.instance.BirdDied ();
			}
		}
	}
}
