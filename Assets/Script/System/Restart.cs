using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void Rewind()
    {
        SoundManager.Instance.PlaySfx("Rewind");
        SceneManager.LoadScene("Main");
    }
}
