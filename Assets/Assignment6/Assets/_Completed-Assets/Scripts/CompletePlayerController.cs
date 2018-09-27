using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class CompletePlayerController : MonoBehaviour {

	public float speed;				//Floating point variable to store the player's movement speed.
	public Text countText;			//Store a reference to the UI Text component which will display the number of pickups collected.
	public Text winText;			//Store a reference to the UI Text component which will display the 'You win' message.
    public Text cthulhucountText;   //Store a reference to Cthulhu.
    public Text loseText;           //Store a reference to losing. 

	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int count;				//Integer to store the number of pickups collected so far.
    private int cthulhucount;       //Integer to store Cthulhu...for now.
	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();

		//Initialize count to zero.
		count = 0;
        cthulhucount = 0;
        
		//Initialze winText to a blank string since we haven't won yet at beginning.
		winText.text = "";

        loseText.text = "";

		//Call our SetCountText function which will update the text with the current value for count.
		SetCountText ();
        SetCthulhuCountText();
	}

    private void Update()
    {
        if (cthulhucount == 2)
        {
           gameObject.SetActive(false); 
        }
            }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
	{
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		rb2d.AddForce (movement * speed);
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			//... then set the other object we just collided with to inactive.
			other.gameObject.SetActive(false);
			
			//Add one to the current value of our count variable.
			count = count + 1;
			
			//Update the currently displayed count by calling the SetCountText function.
			SetCountText ();
		}

        //Doing the same thing but now...with Cthulhu. 
        if (other.gameObject.CompareTag("Cthulhu"))
        {
            //... then set the other object we just collided with to inactive.
            other.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            cthulhucount = cthulhucount + 1;
            count = count + 3;

            //Update the currently displayed count by calling the SetCountText function.
            SetCthulhuCountText();
            SetCountText();
               
        }

    }

	//This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
	void SetCountText()
	{
		//Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
		countText.text = "Count: " + count.ToString ();

		//Check if we've collected all 12 pickups. If we have...
		if (count >= 12 && cthulhucount < 2)
			//... then set the text property of our winText object to "You win!"

            winText.text = "You win!";

	}

    //The same thing...but scarier! 
    void SetCthulhuCountText()
    {
        
        cthulhucountText.text = "Danger Count: " + cthulhucount.ToString();

        
        if (cthulhucount >= 2)
           
            loseText.text = "You've either gone mad from the revelation, or fled from the light into the peace and safety of a new dark age...";
    }
}
