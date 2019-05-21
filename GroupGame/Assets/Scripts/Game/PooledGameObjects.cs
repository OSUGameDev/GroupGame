using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledGameObjects : MonoBehaviour {

	[SerializeField]
	private GameObject pooledObject; 	//the type of object to be pooled, NO LONGER USED

	[SerializeField]
	private bool canGrow = true; 		//if the number of objects in the pool can grow or not

	[SerializeField]
	private int pooledAmount = 10; 			//the number of objects in the pool

	private List<List<GameObject>> pooledObjects;		//The pool of objects
    private List<GameObject> pristeneObjects;           //objects that aren't tainted by the 

	// Use this for initialization
	void Awake () { //changed to awake because guns were initializing prior to this function call.
        pristeneObjects = new List<GameObject>();
        pooledObjects = new List<List<GameObject>>(); 	//initialize the pooled objects, list of lists. 
	}

	/// <summary>
	/// when called, returns an object in the pool that is currently free to use.
	/// It does this by looping through the pool (list) and returning the first object
	/// it finds that is currently deactivated. If there are none and the pool can grow,
	/// it creates a new object, adds it to the pool, and returns that object.
	/// This method is to be used by other scripts in the scene.
	/// </summary>
	/// <returns>GameObject Pooled Object</returns>
	public GameObject GetPooledObject(int objectId)
	{
        if(objectId < 0 || objectId >= pooledObjects.Count) 
        {
            Debug.LogError("BAD OBJECT ID IN PooledGameObjects: " + objectId + "\nMaybe you didn't initialize the object?");
            return null;
        }
        

        for (int i = 0; i < pooledObjects[objectId].Count; i++)	//loop through the pool
		{
			if(!(pooledObjects[objectId][i].activeInHierarchy))	//if we find an inactive object, return it.
			{
				return pooledObjects[objectId][i];
			}
		}

		if(canGrow)			//if no inactive object was found and the pool can grow, create one and return it.
		{
			GameObject obj = (GameObject)Instantiate(pristeneObjects[objectId]);
			pooledObjects[objectId].Add(obj);
			return obj;
		}

		return null;		// else return null.
	}

    /// <summary>
    /// Initializes the objects.
    /// </summary>
    /// <returns> ObjectId that will be used get a pooled object. </returns>
    public int InitializeObjectType(GameObject pooledObj)
    {
        pristeneObjects.Add(pooledObj);
        pooledObjects.Add(new List<GameObject>());
        for (int i = 0; i < pooledAmount; i++)      //this loop creates the pooled objects and adds them to the pool (List) and deactivates them.
        {
            GameObject obj = (GameObject)Instantiate(pooledObj);
            obj.SetActive(false);

            pooledObjects[pooledObjects.Count-1].Add(obj);
        }

        return pooledObjects.Count - 1; //id is the current size of the list-1
    }

    public override string ToString() 
    {
        int tObjects = 0;
        string str = "PooledObjects: " + pooledObjects.Count + ", ObjectNames: [";
        for(int x = 0; x < pooledObjects.Count; x++) 
            {

            char seperator = ',';
            if(x == pooledObjects.Count - 1) 
            {
                seperator = ']';
            }

            if (pooledObjects[x].Count > 0)
            {
                str += pooledObjects[x][0].name + seperator;
            }

            tObjects += pooledObjects[x].Count;
        }
        if(pooledObjects.Count == 0) { str += ']'; } //closing if empty


        str += "\nCanGrow:" + canGrow + ", InitialPooledObjects:" + pooledAmount + "TotalPooledObjects:" + tObjects;
       

        return str;
    }
}
