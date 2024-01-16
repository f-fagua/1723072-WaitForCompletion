using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string m_AssetKey;

    private bool m_Ready = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Addressables.InitializeAsync().Completed += handle =>
        {
            Caching.ClearCache();
            m_Ready = true;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Ready)
            return;
        
        var result = Addressables.DownloadDependenciesAsync(m_AssetKey, true).WaitForCompletion();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
