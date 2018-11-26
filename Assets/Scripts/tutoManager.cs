using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutoManager : MonoBehaviour
{
    public void Close_tuto()
    {
        SceneManager.LoadScene(2);
    }
}
