using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnStart_Click()
    {
        Transform tbUsername = transform.Find("tbUsserName").Find("Text");
        Transform tbPassword = transform.Find("tbPassword").Find("Text");
        Text txtUsername = tbUsername.GetComponent<Text>();
        Text txtPassword = tbPassword.GetComponent<Text>();
        if (txtUsername.text == "a" && txtPassword.text == "b")
        {
            Event.emit(Events.loggedIn,null);
        }
    }
}
