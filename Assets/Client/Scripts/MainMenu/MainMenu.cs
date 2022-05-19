using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _image;

    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _ambient;
    
    private void Start()
    {
        Time.timeScale = 1f;
        _title.DOColor(new Color(0.68f, 0.06f, 1f), 3f).SetEase(Ease.InOutBounce).SetLoops(-1, LoopType.Yoyo);
        _image.gameObject.SetActive(false);
        _image.DOFade(0, 0);
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(StartGame);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(StartGame);
        _exitButton.onClick.RemoveListener(ExitGame);
    }

    private void StartGame()
    {
        _image.DOFade(1, 3f).OnStart((() =>
            {
                _ambient.DOFade(0, 3f);
                _music.DOFade(0, 3f);
                _image.gameObject.SetActive(true);
            }))
            .OnComplete(() => SceneManager.LoadScene(1));
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}