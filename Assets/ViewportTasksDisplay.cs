using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ViewportTasksDisplay : MonoBehaviour
{
    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_ItemPrefab;
    [SerializeField] private int m_ItemsToGenerate;


    void Start()
    {

    }
    public void Generate()
    {
        for (int i = 0; i < m_ItemsToGenerate; i++)
        {
            var item_go = Instantiate(m_ItemPrefab, m_ContentContainer);
            // do something with the instantiated item -- for instance
            item_go.GetComponentInChildren<TextMeshPro>().text = "Item #" + i;
            //item_go.GetComponent<Image>().color = i % 2 == 0 ? Color.yellow : Color.cyan;
            //parent the item to the content container
            item_go.transform.SetParent(m_ContentContainer);
            //reset the item's scale -- this can get munged with UI prefabs
            item_go.transform.localScale = Vector2.one;
        }
    }
}
