using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject finishTrigger;
    [SerializeField] GameObject soundDeathPrefab;
    [SerializeField] Light2D globalLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillBoss()
    {
        Instantiate(soundDeathPrefab, transform.position, transform.rotation, transform.parent);
        Instantiate(finishTrigger, transform.position, Quaternion.identity, transform.parent);
        globalLight.intensity = 0.75f;
    }
}
