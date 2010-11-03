using UnityEditor;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

class BullyTools : EditorWindow {	
	[MenuItem("Bully!/XML Serialization")]
	static void Se(){
		
		Employee[] employees = new Employee[3];
		Employee newEmployee1 = new Employee("Nancy", 29);
		Employee newEmployee2 = new Employee("Mark", 43);
		Employee newEmployee3 = new Employee("Steve", 32);
		employees[0] = newEmployee1;
		employees[1] = newEmployee2;
		employees[2] = newEmployee3;
		
		Directory.CreateDirectory(Application.dataPath + "/AppData");
		StreamWriter sw = new StreamWriter(Application.dataPath + "/AppData/data.txt");
		XmlSerializer xmlizer = new XmlSerializer(typeof(Employee[]));
		xmlizer.Serialize(sw,employees);
		sw.Close();
		AssetDatabase.Refresh();
	}
	
	[MenuItem("Bully!/XML Deserialization")]
	static void De(){
		XmlSerializer xmlizer = new XmlSerializer(typeof(Employee));
		StreamReader sr = new StreamReader(Application.dataPath + "/AppData/data.txt");
		Employee emp = (Employee)xmlizer.Deserialize(sr);
		Debug.Log(emp.name + " " +  emp.age);
	}
	
	[MenuItem("Bully!/Tool")]
	static void T(){
		BullyTools bull = EditorWindow.GetWindow<BullyTools>();
	}
	
	uint objCount = 0;
	
	void OnGUI(){
		//GUILayout.Label(objCount.ToString());
		EditorGUILayout.LabelField("Object Count",objCount.ToString());
		
		GUILayout.BeginArea(new Rect(0,50,200,300));
		if(GUILayout.Button("Press Me")){
			objCount+=1;
			GameObject box =  GameObject.CreatePrimitive(PrimitiveType.Cube);
			box.transform.position = new Vector3(objCount * 2,0,0);
		}
		
		if(objCount>1){
			GUILayout.Button("Remove");
		}
		GUILayout.EndArea();
	}
	
	void OnDestroy(){
		Debug.Log(objCount);	
	}
	
	[MenuItem("Bully!/Save Verticies")]
	static void SaveVerts(){

		GameObject target = Selection.activeGameObject;
		Mesh targetMesh = target.GetComponent<MeshFilter>().mesh;
		
		BlendShape blndShape = new BlendShape();
		blndShape.verticies = targetMesh.vertices;
		blndShape.uvs = targetMesh.uv;
		blndShape.triangles = targetMesh.triangles;
		
		StreamWriter sw = new StreamWriter(Application.dataPath + "/AppData/" + target.name.ToString() + ".xml");
		XmlSerializer xml = new XmlSerializer(typeof(BlendShape));
		xml.Serialize(sw,blndShape);
		
		sw.Close();
		AssetDatabase.Refresh();
	
	}
	
	
}

public class BlendShape{
	public Vector3[] verticies;
	public Vector2[] uvs;
	public int[] triangles;

	public BlendShape(){}
}

public class Employee{
	public string name;
	public uint age;
	
	public Employee(){
		
	}
	
	public Employee(string name, uint age){
		this.name = name;
		this.age = age;
	}
}