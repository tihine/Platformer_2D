using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionnaireScene : MonoBehaviour
{
 public void ChangeScene(string sceneName)
 {
  SceneManager.LoadScene(sceneName);
 }

 public void LoadAdditiveScene(string sceneName)
 {
  SceneManager.LoadSceneAsync(sceneName);
 }

 public void Quit()
 {
  Application.Quit();
 }
 
}
