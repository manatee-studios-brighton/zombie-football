using UnityEngine;

public class CopyLimb : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;
    [SerializeField] private ConfigurableJoint mConfigurableJoint;


    private Quaternion _targetInitialRotation;
    // Start is called before the first frame update
    void Start()
    {
        mConfigurableJoint = GetComponent<ConfigurableJoint>();
        _targetInitialRotation = targetLimb.transform.localRotation;
    }
    

    private void FixedUpdate() {
        mConfigurableJoint.targetRotation = CopyRotation();
    }

    private Quaternion CopyRotation() {
        return Quaternion.Inverse(targetLimb.localRotation) * _targetInitialRotation;
    }
}