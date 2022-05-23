using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoketLaunh : MonoBehaviour
{
    [Header("Корректировка конечной точки")]
    [SerializeField] private Vector3 _corrective=new Vector3(0.1f, -1f, 0);
    [HideInInspector]public bool Lanch=false;
    private bool _lunched=false;
    private GameObject _player;
    [Header("Префаб ракеты")]
    [SerializeField] private GameObject _rocketPrefub;
    [Header("Точка откуда стрелять")]
    [SerializeField] private Transform _shootPoint;
    [Header("Звук запуска ракеты")]
    [SerializeField] private AudioSource[] _audio;
    [SerializeField] private float _speed=0.2f;
    private float _timer=0.5f;
    private void Start()
    {
        _player= FindObjectOfType<PlayerConector>().gameObject;
    }
    public void SetCorrective(float speed, float timer,Vector3 corrective)
    {
        _speed = speed;
        _timer= timer;
        _corrective= corrective;
    }
    private void FixedUpdate()
    {
        if (Lanch)
        {
            if (!_lunched)
            {
                StartCoroutine(Timer());
            }
        }
            
    }
    IEnumerator Timer()
    {
        _lunched = true;
        yield return new WaitForSecondsRealtime(_timer);
        _audio[Random.Range(0, _audio.Length)].Play();
        GameObject bullet = Instantiate(_rocketPrefub, _shootPoint.position, _shootPoint.rotation);
        bullet.transform.LookAt(_player.transform.position +_corrective);
        BulletMover bulletMover = bullet.GetComponent<BulletMover>();
        bulletMover.PointToMove = _player.transform.position + _corrective;
        bulletMover.Speed = _speed;
        Debug.Log("Shoot 1");
        Lanch = false;
        _lunched=false;
    }
}
