using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float repeatInteval;
    void Start()
    {
        if (repeatInteval > 0) 
        {
            InvokeRepeating("SpawnObject", 0.0f , repeatInteval);
        }
        
    }

    public GameObject SpawnObject() 
    {
        if (prefabToSpawn != null) 
        {
            GameObject spawned = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

            Camera_Lock cam = Camera.main.GetComponent<Camera_Lock>();
            if (cam != null) 
            {
                cam.target = spawned.transform;
            }
            GameObject miniMapCamObj = GameObject.Find("Mini_Map_Camera");
            if (miniMapCamObj != null)
            {
                Camera_Lock miniMapCam = miniMapCamObj.GetComponent<Camera_Lock>();
                if (miniMapCam != null)
                {
                    miniMapCam.target = spawned.transform;
                }
            }
            return spawned;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
