using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {
    public BuyButton buyButton;
	// Use this for initialization
	public void buyItem()
    {
        buyButton._toBuyItem();
    }
    public void Oke()
    {
        buyButton.Oke();
    }
}
