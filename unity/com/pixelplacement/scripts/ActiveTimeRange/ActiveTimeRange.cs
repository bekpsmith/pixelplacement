using UnityEngine;
using System.Collections;

public class ActiveTimeRange : MonoBehaviour{
	public float minValue=0, maxValue=10, minLimit=0, maxLimit=23;
	public bool status = true;
	public System.DateTime startTime, endTime;
	public System.Action OnActivated;
	public System.Action OnDeactivated;
	public System.TimeSpan duration;
	
	void Start(){
		//check availability every minute to limit overhead:
		InvokeRepeating("CheckTimeAvailability", 0, 60);
	}
	
	public void CheckTimeAvailability(){
		Calculate();
		
		float currentHour = System.DateTime.Now.Hour;	
		
		if (currentHour >= (int)minValue && currentHour < (int)(minValue+(float)duration.TotalHours)) {
			//dispatch on:
			if (!status) {
				status = true;
				if (OnActivated != null) {
					OnActivated();
				}
			}
		}else{
			//dispatch off:
			if (status) {
				status = false;
				if (OnDeactivated != null) {
					OnDeactivated();
				}
			}
		}
	}
	
	public void Calculate(){
		//define time points:
		startTime = new System.DateTime(1,1,1,(int)minValue,0,0,0);
		endTime = new System.DateTime(1,1,1,(int)maxValue,0,0,0);
				
		//correct 24 hour loop around:
		if (endTime.Subtract(startTime).Hours == 23) {
			endTime = startTime.AddDays(1);
		}else{
			endTime = endTime.AddHours(1);
		}
		
		//calculate duration:
		duration = endTime.Subtract(startTime);	
	}
}