using UnityEngine;

public class SelectedCarBody : MonoBehaviour {

    [SerializeField] GameObject _carBody;
    public void Show() {
        _carBody.SetActive(true);
    }
    public void Hide() { 
        _carBody.SetActive(false);
    }
}
