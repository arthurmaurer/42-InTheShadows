using UnityEngine;
using System.Collections;

public class WigglingLamp : MonoBehaviour
{

    void Start()
    {
        Rigidbody   rb = GetComponent<Rigidbody>();
        HingeJoint  hinge = GetComponent<HingeJoint>();
        Vector3     force = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            0
        );

        force = force.normalized * 0.1f;

        hinge.axis = force;

        rb.AddForce(force, ForceMode.Impulse);
    }
}
