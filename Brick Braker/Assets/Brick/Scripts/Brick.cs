using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = Resources.Load<ParticleSystem>("Brick Break Particles");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        if (transform.parent.gameObject.activeSelf)
        {
            if (LevelManager.instance == null)
            {
                MainMenuManager.instance.BrickCount--;
                MainMenuManager.instance.disabledBricks.Add(gameObject);
            }
            else
            {
                LevelManager.instance.BrickCount--;
                LevelManager.instance.disabledBricks.Add(gameObject);
            }
            ParticleSystem temp = Instantiate(ps);
            temp.transform.position = transform.position;

            temp.Play();
        }

    }
}
