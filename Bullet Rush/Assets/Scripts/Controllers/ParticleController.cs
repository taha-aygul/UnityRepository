using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem myParticles;

    [SerializeField] private float particleTime;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnDestroy()
    {
        if (timer <= particleTime-1)
        {
            PlayerLevelManager.Instance.AddXP(3);
        }
    }
   
    

}
