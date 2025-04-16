using UnityEngine;
using UnityEngine.U2D;
using Unity.Cinemachine;

public class RPGCameraManager : MonoBehaviour
{
    public static RPGCameraManager sharedInstance = null;
    
    public CinemachineCamera virtualCamera;

    void Awake() 
    {
        if (sharedInstance != null && sharedInstance != this) 
        {
            Destroy(gameObject);
        }
        else 
        {
            sharedInstance = this;
        }

        GameObject vCamGameObject = GameObject.FindWithTag("VirtualCamera");

        virtualCamera = vCamGameObject.GetComponent<CinemachineCamera>();
    }
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
