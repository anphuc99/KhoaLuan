using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackLogin : MonoBehaviour
{
    // Start is called before the first frame update
    private Button btn;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(back);
    }

    private void back()
    {
        Event.emit(Events.login, null);
    }
}

