/*
 * Copyright 2014
 * author: Daybson B.S. Paisante
 * daybson.paisante@outlook.com
 * http://daybsonpaisante.wordpress.com/ 
 */

using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        //rotate and translate by user input
        transform.Rotate(0, Input.GetAxis("Horizontal") * speed, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }
}
