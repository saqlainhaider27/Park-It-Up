using UnityEngine;

public interface ICar {
    
    float Speed { get;}
    float Acceleration { get;}
    

    void Move(Vector3 _direction);

}
