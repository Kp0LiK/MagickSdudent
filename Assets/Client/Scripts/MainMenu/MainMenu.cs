using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private Button _playButton;
   [SerializeField] private Button _exitButton;
   [SerializeField] private TMP_Text _title;

   private void Start() 
      => _title.DOColor(Color.white, 1f).SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo);

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
      
   }

   private void ExitGame()
   {
      Application.Quit();
   }
}
