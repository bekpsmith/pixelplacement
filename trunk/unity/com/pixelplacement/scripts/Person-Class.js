class Person{
	public var age : int;
	public var sex : String;
	
	public function Person(age : int,sex : String){
		this.age=age;
		this.sex=sex;
		Debug.Log("New person was made.");
	}
}