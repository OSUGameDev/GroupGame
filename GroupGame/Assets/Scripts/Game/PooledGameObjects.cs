using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledGameObjects : MonoBehaviour {

	[SerializeField]
	private GameObject pooledObject; 	//the type of object to be pooled

	[SerializeField]
	private bool canGrow = true; 		//if the number of objects in the pool can grow or not

	[SerializeField]
	private int pooledAmount; 			//the number of objects in the pool

	List<GameObject> pooledObjects;		//The pool of objects

	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject>(); 	//initialize the pooled objects

		for (int i = 0; i < pooledAmount; i++) 		//this loop creates the pooled objects and adds them to the pool (List) and deactivates them.
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}
	/// <summary>
	/// when called, returns an object in the pool that is currently free to use.
	/// It does this by looping through the pool (list) and returning the first object
	/// it finds that is currently deactivated. If there are none and the pool can grow,
	/// it creates a new object, adds it to the pool, and returns that object.
	/// This method is to be used by other scripts in the scene.
	/// </summary>
	/// <returns>GameObject Pooled Object</returns>
	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjects.Count; i++)	//loop through the pool
		{
			if(!(pooledObjects[i].activeInHierarchy))	//if we find an inactive object, return it.
			{
				return pooledObjects[i];
			}
		}

		if(canGrow)			//if no inactive object was found and the pool can grow, create one and return it.
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			pooledObjects.Add(obj);
			return obj;
		}

		return null;		// else return null.
	}
}
