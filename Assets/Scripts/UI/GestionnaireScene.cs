using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GestionnaireScene : MonoBehaviour
{
 private Focus_UI FocusUIScript;
 private UnityEvent closeAdditiveScene = new UnityEvent();

 private void Start()
 {
  closeAdditiveScene.AddListener(() => FocusUIScript.CallFocus());
  FocusUIScript = Focus_UI.Instance;
 }

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
  SoundSingleton.Instance.PlayClick();
  closeAdditiveScene.Invoke();
 }

 public void Quit()
 {
  Application.Quit();
 }
 
}
