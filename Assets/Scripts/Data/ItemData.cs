using System;

[Serializable]
public class ItemData
{
    public string id;
    public int amount;

    public ItemData(string _id, int _amount = 0)
    {
        id = _id;
        amount = _amount;
    }
}
