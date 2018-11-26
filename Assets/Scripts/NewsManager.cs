using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{

    Animation _anim;

    public Text newsText; 
    public List<string> noticias;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_anim.IsPlaying("newsTransition"))
        {
            //Debug.Log("playing");
            _anim.Play();
            newsText.text = noticias[Random.Range(0, noticias.Count - 1)];
        }
    }
}
