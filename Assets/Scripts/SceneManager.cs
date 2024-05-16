using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.startOver = true;
    }
}
