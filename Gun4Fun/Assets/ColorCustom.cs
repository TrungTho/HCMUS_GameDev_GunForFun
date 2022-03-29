using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorCustom : MonoBehaviour
{
    [SerializeField] Color[] colors;
    [SerializeField] Sprite[] hats;
    public void setColor(int index)
    {
        // Debug.Log(colors[index]);
        PlayerController.localPlayer.setColor(colors[index]);
    }

    public void setHat(int index)
    {
        PlayerController.localPlayer.setHat(hats[index]);
    }

    public void NextScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
