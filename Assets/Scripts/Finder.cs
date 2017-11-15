using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Finder : Token
{
   
    public Sprite m_defaultMaterial;
  
    public Sprite m_foundMaterial ;
    

    

    private void OnCollisionEnter2D(Collision2D i_other)
    {

        if (i_other.gameObject.tag == "enemy1")
        {
            SetSprite(m_foundMaterial);
        }

    }

    private void OnCollisionExit2D(Collision2D i_other)
    {

        if (i_other.gameObject.tag == "enemy1")
        {
            SetSprite(m_defaultMaterial);

        }

    }

   

       
}


