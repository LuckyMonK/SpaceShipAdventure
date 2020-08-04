using Gameplay.Spaceships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HPBarController : MonoBehaviour
{
    [SerializeField]
    private Spaceship ship;
    private int hp = 0;

    [SerializeField]
    GameObject heartPrefab;
    [SerializeField]
    Transform parentHPPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ship.HP != hp) {
            hp = ship.HP;

            foreach (Transform child in parentHPPanel) {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < hp; i++) {
                var hp = Instantiate(heartPrefab, parentHPPanel);
                hp.GetComponent<RectTransform>().anchoredPosition = 
                    new Vector3(0, 
                    hp.GetComponent<RectTransform>().anchoredPosition.y + hp.GetComponent<RectTransform>().sizeDelta.y * i, 0);
            }
        }
    }
}
