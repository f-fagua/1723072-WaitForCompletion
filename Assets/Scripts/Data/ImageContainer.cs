using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Image Container", menuName = "Unity Support/ImageContainer", order = 2)]
public class ImageContainer : ScriptableObject
{
    [SerializeField]
    private Texture2D[] m_Images;

    public Texture2D[] images
    {
        set { m_Images = value; }
    }

    private int m_CurrentIndex;

    public Texture2D FetchImage()
    {
        var image = m_Images[m_CurrentIndex++];

        if (m_CurrentIndex >= m_Images.Length)
            m_CurrentIndex = 0;

        return image;
    }
}