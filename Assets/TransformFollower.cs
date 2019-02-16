 using UnityEngine;
 using System.Collections;
 
 public class TransformFollower : MonoBehaviour
 {
         public Transform target;
     public float smoothTime = 0.1f;
     public float speed = 3.0f;
     
     public float followDistance = 10f;
     public float verticalBuffer = 1.5f;
     public float horizontalBuffer = 0f;
     
     private Vector3 velocity = Vector3.zero;
     
     public Quaternion rotation = Quaternion.identity;
     
     public float yRotation = 0.0f;
     
     void LateUpdate () {
         Vector3 targetPosition = target.TransformPoint(new Vector3(horizontalBuffer, followDistance, verticalBuffer));
         transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
         //this is the code that solves the problem
         transform.eulerAngles = new Vector3(25, target.transform.eulerAngles.y, 0);
         //------------------------------------------------------------------------
     }
 }