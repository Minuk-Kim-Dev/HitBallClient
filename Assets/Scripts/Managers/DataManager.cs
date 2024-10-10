using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    #region PlayerInfo

    public string Id { get; set; }
    public string Major {  get; set; }
    public string Name { get; set; }

    #endregion

    public void Init()
    {
        Id = null;
        Major = null;
        Name = null;
    }

    public void SetPlayerInfo(string id, string major, string name)
    {
        Id = id;
        Major = major;
        Name = name;
    }
}
