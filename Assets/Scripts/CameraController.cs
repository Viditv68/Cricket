using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private PlayableDirector directorl;
    [SerializeField] private Transform player;

    [SerializeField] private float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z - offset);
        if(GameManager.Instance.playVowlCutScene)
        {
            directorl.Play();
        }
    
    }


    public void SetStatusOfCinemchineBrain(bool _flag)
    {
        _cinemachineBrain.enabled = _flag;
    }
}
