using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject warmUpVirtualCamera;
    [SerializeField] private GameObject gameVirtualCamera;
    [SerializeField] private GameObject startBowlButton;
    private void OnEnable()
    {
        warmUpVirtualCamera.SetActive(false);
        gameVirtualCamera.SetActive(true);
        //GameManager.Instance.startOver = true;

        StartCoroutine(EnableBowlButton());

    }

    public IEnumerator EnableBowlButton()
    {
        yield return new WaitForSeconds(1f);
        startBowlButton.SetActive(true);
    }

    public void StartBowling()
    {
        GameManager.Instance.startOver = true;
    }

    
}
