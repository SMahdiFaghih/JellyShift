using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class FillLoadingBar : MonoBehaviour
{
    public Image loadingBar;
    public Text loadingPercentage;

    void Start()
    {
        print("Loading");
        loadingBar.fillAmount = 0;
        StartCoroutine("Fill");
    }

    IEnumerator Fill()
    {
        while(loadingBar.fillAmount < 1)
        {
            loadingBar.fillAmount += Time.deltaTime * 0.3f;
            loadingPercentage.text = Math.Ceiling(loadingBar.fillAmount * 100) + "%";
            yield return null;
        }
        LoadFirstLevel();
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
