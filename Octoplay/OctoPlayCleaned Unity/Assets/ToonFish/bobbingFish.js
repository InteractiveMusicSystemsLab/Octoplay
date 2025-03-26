#pragma strict

var pos1 : Vector3;
var pos2 : Vector3;
var moveSpeed : float = 0.1;
var moveTo;

function Start(){

    pos1 = transform.position;
}

function Update(){

    //switch
    if(transform.position == pos1)
    {
    transform.LookAt(pos2);
    moveTo = pos2;   
    }
     
    if(transform.position == pos2)
    {
    transform.LookAt(pos1);
    moveTo = pos1;
    }
	//animate
	transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed);

}