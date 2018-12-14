using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour {

    public Button resume;

    private void Update()
    {
        resume.onClick.AddListener(ChangeSelect); //Command that calls a function when the toggle button is clicked
    }

    void ChangeSelect()
    {
            GameManager.resumeChecker = true;
    }
}
