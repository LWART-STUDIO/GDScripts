using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnBullet : MonoBehaviour
{
    [Header("Компанент разрушения меша")]
    //private MeshDestroy _meshDestroy;
    private bool _destroyed=false;
    [HideInInspector]
    public bool Destroy;
    [Header("Звук разрушения")]
    [SerializeField] private AudioSource[] _audio;
    [SerializeField] private GameObject _breakedGlass;
    [SerializeField] private Rigidbody[] _rigidbodies;
    private Vector3 _pointIn;
    private Vector3 _pointOut;
    private void Start()
    {
        if (_breakedGlass != null)
        {
            _rigidbodies=_breakedGlass.GetComponentsInChildren<Rigidbody>();
        }
        //_meshDestroy = GetComponent<MeshDestroy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_destroyed)
        {
            if (other.TryGetComponent<BulletMover>(out BulletMover bulletMover))
            {
               _pointIn=other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                /*//_meshDestroy.DestroyMesh();
                if(_breakedGlass != null)
                {
                    
                    _breakedGlass.SetActive(true);
                    gameObject.SetActive(false);
                }
                if (_audio != null)
                {
                    _audio[Random.Range(0, _audio.Length)].Play();
                }
                _destroyed = true;
                Destroy(gameObject)*/;
                
            }
            if (other.TryGetComponent<GranadeMarker>(out GranadeMarker granade))
            {
                _pointIn = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                /*//_meshDestroy.DestroyMesh();
                if (_breakedGlass != null)
                {

                    _breakedGlass.SetActive(true);
                    gameObject.SetActive(false);
                }
                if (_audio != null)
                {
                    _audio[Random.Range(0, _audio.Length)].Play();
                }
                _destroyed = true;
                Destroy(gameObject);*/

            }
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _pointIn = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                /*//_meshDestroy.DestroyMesh();
                if (_breakedGlass != null)
                {

                    _breakedGlass.SetActive(true);
                    gameObject.SetActive(false);
                }
                if (_audio != null)
                {
                    _audio[Random.Range(0, _audio.Length)].Play();
                }
                _destroyed = true;
                Destroy(gameObject);*/

            }
            if (other.TryGetComponent<PlayerConector>(out PlayerConector player))
            {
                _pointIn = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                /*//_meshDestroy.DestroyMesh();
                if (_breakedGlass != null)
                {

                    _breakedGlass.SetActive(true);
                    gameObject.SetActive(false);
                }
                if (_audio != null)
                {
                    _audio[Random.Range(0, _audio.Length)].Play();
                }
                _destroyed = true;
                Destroy(gameObject);*/

            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (!_destroyed)
        {
            if (other.TryGetComponent<BulletMover>(out BulletMover bulletMover) )
            {
                
                //_meshDestroy.DestroyMesh();
                if (_breakedGlass != null)
                {
                    _pointOut = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    _breakedGlass.SetActive(true);
                    Vector3 moveVerctor = _pointOut - _pointIn;
                    var distance=moveVerctor.magnitude;
                    moveVerctor = moveVerctor / distance;
                    Debug.Log(moveVerctor);
                    for (int i = 0; i < _rigidbodies.Length; i++)
                    {
                        _rigidbodies[i].AddForce(moveVerctor,ForceMode.Impulse);
                    }
                   
                    
                    gameObject.SetActive(false);
                }
                if (_audio != null)
                {
                    _audio[Random.Range(0, _audio.Length)].Play();
                }
                _destroyed = true;
                Destroy(gameObject);
                
            }
            if (other.TryGetComponent<GranadeMarker>(out GranadeMarker granade))
            {

                //_meshDestroy.DestroyMesh();
                if (_breakedGlass != null)
                {
                    _pointOut = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    _breakedGlass.SetActive(true);
                    Vector3 moveVerctor = _pointOut - _pointIn;
                    var distance=moveVerctor.magnitude;
                    moveVerctor = moveVerctor / distance;
                    Debug.Log(moveVerctor);
                    for (int i = 0; i < _rigidbodies.Length; i++)
                    {
                        _rigidbodies[i].AddForce(moveVerctor,ForceMode.Impulse);
                    }
                    
                    gameObject.SetActive(false);
                }
                if (_audio != null)
                {
                   
                    _audio[Random.Range(0, _audio.Length)].Play();
                }
                _destroyed = true;
                Destroy(gameObject);
                
            }
            if (other.TryGetComponent<PlayerConector>(out PlayerConector player))
            {

                //_meshDestroy.DestroyMesh();
                if (_breakedGlass != null)
                {
                    _pointOut = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    _breakedGlass.SetActive(true);
                    Vector3 moveVerctor = _pointOut - _pointIn;
                    var distance = moveVerctor.magnitude;
                    moveVerctor = moveVerctor / distance;
                    Debug.Log(moveVerctor);
                    for (int i = 0; i < _rigidbodies.Length; i++)
                    {
                        _rigidbodies[i].AddForce(moveVerctor, ForceMode.Impulse);
                    }

                    gameObject.SetActive(false);
                }
                if (_audio != null)
                {

                    _audio[Random.Range(0, _audio.Length)].Play();
                }
                _destroyed = true;
                Destroy(gameObject);

            }
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {

                //_meshDestroy.DestroyMesh();
                if (_breakedGlass != null)
                {
                    _pointOut = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    _breakedGlass.SetActive(true);
                    Vector3 moveVerctor = _pointOut - _pointIn;
                    var distance = moveVerctor.magnitude;
                    moveVerctor = moveVerctor / distance;
                    Debug.Log(moveVerctor);
                    for (int i = 0; i < _rigidbodies.Length; i++)
                    {
                        _rigidbodies[i].AddForce(moveVerctor, ForceMode.Impulse);
                    }

                    gameObject.SetActive(false);
                }
                if (_audio != null)
                {

                    _audio[Random.Range(0, _audio.Length)].Play();
                }
                _destroyed = true;
                Destroy(gameObject);

            }
        }
        
    }
    
    private void Update()
    {
        if (Destroy)
        {
            if (!_destroyed)
            {

                //_meshDestroy.DestroyMesh();
                if (_breakedGlass != null)
                {

                    _breakedGlass.SetActive(true);
                    gameObject.SetActive(false);
                }
                if (_audio != null)
                {
                    _audio[Random.Range(0, _audio.Length)].Play();
                }
                _destroyed = true;
                Destroy(gameObject);
                
            }
        }
    }
}
