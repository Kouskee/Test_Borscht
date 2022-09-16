using Cinemachine;
using Player;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnPlayer;
    [SerializeField] private Transform _rewardsParent;
    [Header("UI")]
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private UIControllerInput _mobileInput;
    [SerializeField] private GameObject _restart;

    private SpawnerEnemies _spawner;
    private Health _player;
    private Health _playerVar;
    private CinemachineVirtualCamera _camera;
    private Reward _reward;
    private Canvas _mobileInputCanvas;
    
    private int _score;
    private int _totalScore;
    
    public void Init(SpawnerEnemies spawner, Health player, CinemachineVirtualCamera camera, Reward reward)
    {
        _spawner = spawner;
        _player = player;
        _camera = camera;
        _reward = reward;
    }

    private void Awake()
    {
        _mobileInputCanvas = _mobileInput.GetComponent<Canvas>();
    }

    private void Start()
    {
        SpawnPlayer();
        
        EventManager.OnPlayerOnBase.AddListener(OnChangeLocation);
        EventManager.OnPlayerDied.AddListener(OnPlayerDied);
        EventManager.OnEnemyDied.AddListener(OnEnemyDied);
    }

    private void OnChangeLocation(bool onBase)
    {
        if(!onBase) return;
        _spawner.CheckForSpawn();
        
        _totalScore += _score;
        _scoreTxt.text = _totalScore.ToString();
        _score = 0;
    }

    private void PickUpReward(int score)
    {
        _score += score;
    }
    
    private void OnEnemyDied(Transform position)
    {
        if(Random.Range(0f, 10f) > 5) return; // TODO сделать нормальную систему шансов
        var reward = Instantiate(_reward, position.position, Quaternion.identity, _rewardsParent);
        reward.OnPickUp.AddListener(PickUpReward);
    }

    private void OnPlayerDied()
    {
        _spawner.PlayerDied();
        SetActivePlayUi(false);
        Destroy(_playerVar.gameObject);
    }
    
    public void SpawnPlayer()
    {
        SetActivePlayUi(true);
        _score = 0;
        
        if(_playerVar != null)
            Destroy(_playerVar.gameObject);
        _playerVar = Instantiate(_player, _spawnPlayer.position, Quaternion.identity, _spawnPlayer.transform);
        _spawner.NewPlayer(_playerVar);
        
        _camera.Follow = _playerVar.transform;
        _camera.LookAt = _playerVar.transform;
        
        _mobileInput.Init(_playerVar.GetComponent<Movement>());
    }

    public void SetActivePlayUi(bool active)
    {
        _mobileInputCanvas.enabled = active;
        _restart.SetActive(!active);
    }
}
