#pragma strict 
public var rotation : float = 0;
public var negrotation : float = 0;

function Start () {

}

function Update () {

transform.eulerAngles = new Vector3(0 , 0 , transform.eulerAngles.z + rotation );
transform.eulerAngles = new Vector3(0 , 0 , transform.eulerAngles.z - negrotation );

}