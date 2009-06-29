function OnTriggerEnter (other : Collider) {
	Destroy(gameObject);
	print("Coin Collected!");
}