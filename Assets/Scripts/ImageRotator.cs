using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ImageRotator : MonoBehaviour
{
    [SerializeField]
    private Image m_Image;

    [SerializeField]
    private AssetReference m_Images;

    private ImageContainer m_ImageContainer;

    private void Start()
    {
        m_ImageContainer = m_Images.LoadAssetAsync<ImageContainer>().WaitForCompletion();
    }

    public void Update()
    {
        if (m_ImageContainer == null)
            return;

        var texture = m_ImageContainer.FetchImage();
        var rect = new Rect(0, 0, texture.width, texture.height);
        var pivot = new Vector2(0.5f, 0.5f);
        
        if (m_Image != null)
            m_Image.sprite = Sprite.Create(texture, rect, pivot);
    }
    
}
