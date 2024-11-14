using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

    public static T SpawnRandomFromList<T>(Vector3 _spawnLocation, List<T> itemList) where T : MonoBehaviour {
        if (itemList == null || itemList.Count == 0) {
            Debug.LogWarning("The item list is empty or null.");
            return null;
        }

        int randomIndex = Random.Range(0, itemList.Count);
        T selectedObject = itemList[randomIndex];
        T spawnedObject = Instantiate(selectedObject, _spawnLocation, Quaternion.identity);

        return spawnedObject;
    }

}
