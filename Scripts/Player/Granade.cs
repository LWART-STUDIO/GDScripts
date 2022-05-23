using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
[RequireComponent(typeof(Explosion))]
public class Granade : MonoBehaviour
{
    private Explosion _explosion;
    [Header("Сила броска или скорость полета")]
    [SerializeField] private float _throwForce;
    private float _time;
    private bool _isThrowen=false;
    private Vector3 _pointToShoot;
    private MeshRenderer[] _meshRenderer;
    [Header("Модель гранаты")]
    [AssetPreview]
    [SerializeField] private GameObject _granadePrefub;
    private Collider _collider;
    [Header("Эту гранату бросает враг?")]
    [SerializeField] private bool _isHoldingEnemy;
    [Header("Окно смерти")]
    private GameObject _deadPanel;
    [Header("Путь по которому будет двигаться граната")]
    [SerializeField] private PathCreator _pathCreator;
    private float _distanceGone;
    private Vector3 _position;
    [Header("Значек прицела")]
    [SerializeField] private GameObject _cross;
    private bool _resized = false;
    [SerializeField] private GameObject _glowParticals;

    private void Start()
    {
        _meshRenderer=_granadePrefub.GetComponentsInChildren<MeshRenderer>();
        _explosion = GetComponent<Explosion>();
        _deadPanel = FindObjectOfType<DeadPanelMarker>().gameObject;
        _collider = GetComponent<Collider>();
    }
    public void Therow(Vector3 point)
    {
        _pointToShoot = point;  
        _isThrowen = true;
    }
    private void FixedUpdate()
    {
       
        if (_isThrowen)
        {
            if (_isHoldingEnemy)
            {
                if (_resized == false)
                {
                    StartCoroutine(EnemyBulletColiderResizer());
                }
            }
            if (_pathCreator == null)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                _time += Time.fixedUnscaledDeltaTime;
                
                var point = _pointToShoot - gameObject.transform.position;
                var distance = point.magnitude;
                Vector3 diraction = point / distance;
                //   rb.AddForce(diraction * _throwForce, ForceMode.Impulse);
                //gameObject.transform.position = MathParabola.Parabola(Vector3.zero, Vector3.up, 1f, _time/5);
                transform.position = Vector3.MoveTowards(transform.position, _pointToShoot, _throwForce * Time.fixedUnscaledDeltaTime);
            }
            else
            {
                StartCoroutine(RBFix());
                _distanceGone += _throwForce * Time.fixedDeltaTime;
                _position = _pathCreator.path.GetPointAtDistance(_distanceGone,EndOfPathInstruction.Stop);
                transform.position = _position;
                
            }
            
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (!_isHoldingEnemy)
        {
            if (!collision.gameObject.TryGetComponent<PlayerConector>(out PlayerConector conector))
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                for (int i = 0; i < _meshRenderer.Length; i++)
                {
                    _meshRenderer[i].enabled = false;
                }
                _collider.enabled = false;
                _explosion.Exploid();
                _throwForce = 0;
                rb.useGravity = false;
                rb.isKinematic = true;
                if (_cross != null)
                {
                    _cross.SetActive(false);
                }
                if(_glowParticals != null)
                {
                    _glowParticals.SetActive(false);
                }
                Destroy(this);
                Debug.Log(collision.gameObject.name);

            }
        }
        else
        {
            if (collision.gameObject.TryGetComponent<PlayerConector>(out PlayerConector conector))
            {
                
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                for (int i = 0; i < _meshRenderer.Length; i++)
                {
                    _meshRenderer[i].enabled = false;
                }
                _collider.enabled = false;
                _explosion.Exploid();
                _throwForce = 0;
                rb.useGravity = false;
                rb.isKinematic = true;
                if (_cross != null)
                {
                    _cross.SetActive(false);
                }
                if (_glowParticals != null)
                {
                    _glowParticals.SetActive(false);
                }
                Destroy(this);
                Debug.Log(collision.gameObject.name);
                ProjectTools.SetChildrenActive(_deadPanel, true);
            }
            if (collision.gameObject.TryGetComponent<BulletMover>(out BulletMover bulletMover))
            {

                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                for (int i = 0; i < _meshRenderer.Length; i++)
                {
                    _meshRenderer[i].enabled = false;
                }
                _collider.enabled = false;
                _explosion.Exploid();
                _throwForce = 0;
                rb.useGravity = false;
                rb.isKinematic = true;
                if (_cross != null)
                {
                    _cross.SetActive(false);
                }
                if (_glowParticals != null)
                {
                    _glowParticals.SetActive(false);
                }
                Destroy(this);
                Debug.Log(collision.gameObject.name);
                
            }
            /*if (!collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy)&&!collision.gameObject.TryGetComponent<EnemyBody>(out EnemyBody enemyBody)&&!collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
               
                

                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                for (int i = 0; i < _meshRenderer.Length; i++)
                {
                    _meshRenderer[i].enabled = false;
                }
                _collider.enabled = false;
                _explosion.Exploid();
                _throwForce = 0;
                if (_cross != null)
                {
                    _cross.SetActive(false);
                }
                Destroy(this);
                Debug.Log(collision.gameObject.name);

            }*/

        }
        


    }
    IEnumerator RBFix()
    {
        yield return new WaitForSeconds(0.2f); 
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }
    private IEnumerator EnemyBulletColiderResizer()
    {
        _resized = true;
        yield return new WaitForSecondsRealtime(4f);
        {
            SphereCollider collider = GetComponent<SphereCollider>();
            float radius = collider.radius;
            collider.radius = radius * 5f;

        }
    }
}
