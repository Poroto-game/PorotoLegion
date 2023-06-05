using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
// Change the text on the text component.
//  private TMP_Text m_TextComponent;
//  m_TextComponent.text = "Some new line of text.";

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _newScoreText;
    [SerializeField]
    private TMP_Text _newAmmoText;
    [SerializeField]
    private GameObject _congratulations_PopUp;
    [SerializeField]
    private GameObject _main_Menu_Button;
    [SerializeField]
    private GameObject _gameOver_popup;
    [SerializeField]
    private Slider _comboSlider;
    private int _currentScene;
    public int sliderRecoverSpeed = 15;
    public int comboAmmoGain = 3;
    private GameManager _gameManager;



    // Start is called before the first frame update
    private void Start()
    {
        _newScoreText.text = "Score: " + 0;
        _newAmmoText.text = "Ammo: " + GameManager.ammo;
        _congratulations_PopUp.gameObject.SetActive(false);
        _main_Menu_Button.gameObject.SetActive(false);
        //_gameOver_popup.gameObject.SetActive(false);
        _currentScene = SceneManager.GetActiveScene().buildIndex;
        _comboSlider = GameObject.Find("Combo Slider").GetComponent<Slider>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();


        if(_currentScene < 5)
            _comboSlider.transform.gameObject.SetActive(false);
        else
            _comboSlider.transform.gameObject.SetActive(true);


    }


    private void Update()
    {
        _comboSlider.value -= Time.deltaTime * sliderRecoverSpeed;

        if(_comboSlider.value > 99)
        {
            _gameManager.AddAmmo(comboAmmoGain);
            _comboSlider.value = 0;
        }
    }

    public void UpdateScoreUI(int playerScore)
    {
        _newScoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateAmmoUI(int weaponAmmo)
    {
        _newAmmoText.text = "Ammo: " + weaponAmmo.ToString();
    }

    public void FinishTutorial()
    {
        _congratulations_PopUp.gameObject.SetActive(true);
        _main_Menu_Button.gameObject.SetActive(true);
    }

    public void GameOverScreen()
    {
        _gameOver_popup.gameObject.SetActive(true);
    }

    public void AddPointsToCombo(int points)
    {
        _comboSlider.value += points;
    }
    

}
