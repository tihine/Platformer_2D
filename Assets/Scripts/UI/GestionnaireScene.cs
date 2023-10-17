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
  SceneManager.LoadScene(sceneName,LoadSceneMode.Additive);
 }

 public void CloseAdditiveScene(string sceneName)
 {
  SceneManager.UnloadSceneAsync(sceneName);
 }

 public void Quit()
 {
  Application.Quit();
 }
 
}
