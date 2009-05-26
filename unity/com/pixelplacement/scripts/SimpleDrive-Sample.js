// A very simplistic car driving on the x-z plane.
var speed = 1;
var rotationSpeed = 100.0;

function FixedUpdate () {
	var translation = Input.GetAxis ("Vertical") * speed;
	var rotation = Input.GetAxis ("Horizontal") * rotationSpeed;

	translation *= Time.deltaTime;
	rotation *= Time.deltaTime;

	transform.Translate (0, 0, translation);

	transform.Rotate (0, rotation, 0);
	Camera.main.transform.position.x=transform.position.x;
	Camera.main.transform.position.z=transform.position.z;
}