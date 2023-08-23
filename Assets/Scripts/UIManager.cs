using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text stateText;
    public PlayerController pc;

    // Update is called once per frame
    void Update()
    {
        stateText.text = pc.state.ToString();
    }
}
