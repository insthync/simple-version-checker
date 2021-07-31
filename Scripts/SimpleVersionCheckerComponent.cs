using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace SimpleVersionChecker
{
    public class SimpleVersionCheckerComponent : MonoBehaviour
    {
        [Tooltip("URL to the JSON file which contains app version for each platforms")]
        public string versioningFileUrl;
        [Tooltip("URL to download Android app")]
        public string androidAppUrl;
        [Tooltip("URL to download iOS app")]
        public string iosAppUrl;
        [Tooltip("URL to download Windows app")]
        public string winAppUrl;
        [Tooltip("URL to download OSX app")]
        public string osxAppUrl;
        [Tooltip("URL to download Linux app")]
        public string linuxAppUrl;
        public int nextSceneIndex = 1;

        private void Start()
        {
            StartCoroutine(VersionCheckRoutine());
        }

        private IEnumerator VersionCheckRoutine()
        {
            var request = UnityWebRequest.Get(versioningFileUrl);
            yield return request.SendWebRequest();
            Debug.LogError(request.downloadHandler.text);
            var data = JsonUtility.FromJson<SimpleVersionCheckerData>(request.downloadHandler.text);
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    if (string.IsNullOrEmpty(data.android_version) || !data.android_version.Equals(Application.version))
                        Application.OpenURL(androidAppUrl);
                    else
                        SceneManager.LoadScene(nextSceneIndex);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    if (string.IsNullOrEmpty(data.ios_version) || !data.ios_version.Equals(Application.version))
                        Application.OpenURL(iosAppUrl);
                    else
                        SceneManager.LoadScene(nextSceneIndex);
                    break;
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    if (string.IsNullOrEmpty(data.win_version) || !data.win_version.Equals(Application.version))
                        Application.OpenURL(winAppUrl);
                    else
                        SceneManager.LoadScene(nextSceneIndex);
                    break;
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                    if (string.IsNullOrEmpty(data.osx_version) || !data.osx_version.Equals(Application.version))
                        Application.OpenURL(osxAppUrl);
                    else
                        SceneManager.LoadScene(nextSceneIndex);
                    break;
                case RuntimePlatform.LinuxPlayer:
                case RuntimePlatform.LinuxEditor:
                    if (string.IsNullOrEmpty(data.linux_version) || !data.linux_version.Equals(Application.version))
                        Application.OpenURL(linuxAppUrl);
                    else
                        SceneManager.LoadScene(nextSceneIndex);
                    break;
                default:
                    break;
            }
        }
    }
}
