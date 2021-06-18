using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;


public class CSimpleCalcUI : MonoBehaviour
{
    [SerializeField] Text m_txtResult;              // 최종결과 텍스트

    [SerializeField] Button m_Button_1;        // 버튼
    [SerializeField] Button m_Button_2;        // 버튼
    [SerializeField] Button m_Button_Plus;     // 버튼
    [SerializeField] Button m_Button_Equal;    // 버튼
    [SerializeField] Button m_Button_Clear;    // 버튼


    private string m_strNum1 = "";      // 좌측값
    private string m_strNum2 = "";      // 우측값
    private bool m_bPlusClick = false;

    //----------------
    private int m_nLeft = 0;
    //private int m_nRight = 0;


    // Start is called before the first frame update
    void Start()
    {
        m_Button_1.onClick.AddListener(OnClicked_Num1);
        m_Button_2.onClick.AddListener(OnClicked_Num2);
        m_Button_Plus.onClick.AddListener(OnClicked_Plus);
        m_Button_Equal.onClick.AddListener(OnClicked_Equal);
        m_Button_Clear.onClick.AddListener(OnClicked_Clear);
    }


    public void OnClicked_Num1()
    {

        if (m_bPlusClick)
        {
            m_strNum2 += "1";
            m_txtResult.text = m_strNum2;
        }
        else
        {
            m_strNum1 += "1";  
            m_txtResult.text = m_strNum1;
        }

    }

    public void OnClicked_Num2()
    {

        if (m_bPlusClick)
        {
            m_strNum2 += "2";
            m_txtResult.text = m_strNum2;
        }
        else
        {
            m_strNum1 += "2";
            m_txtResult.text = m_strNum1;
        }
    }

    public void OnClicked_Plus()
    {
        //if (m_bPlusClick)
        //{
        //    if( m_strNum2 != "")
        //        m_nLeft += int.Parse(m_strNum2);
    
        //    m_strNum2 = "";
        //}
        //else
        //{
        //    if( m_strNum1 != "" )
        //        m_nLeft = int.Parse(m_strNum1);
            
        //    m_strNum1 = "";
        //}

        m_bPlusClick = true;
    }

    public void OnClicked_Equal()
    {
        int nLeft = m_nLeft; 
        int nRight = int.Parse(m_strNum2);

        int nResult = nLeft + nRight;

        m_txtResult.text = nResult.ToString();
        ClearNum();

    }

    public void OnClicked_Clear()
    {
        ClearNum();
        m_txtResult.text = "0";
    }

    public void ClearNum()
    {
        m_nLeft = 0;
        m_strNum1 = "";
        m_strNum2 = "";
        m_bPlusClick = false;
    }

    
}
