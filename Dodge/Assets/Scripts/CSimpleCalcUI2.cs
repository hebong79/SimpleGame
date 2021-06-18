using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CSimpleCalcUI2 : MonoBehaviour
{
    [SerializeField] Text m_txtResult;          // 최종결과 텍스트
    [SerializeField] List<Button> m_BtnNums;    // 버튼

    [SerializeField] Button m_Button_Plus;      // 버튼
    [SerializeField] Button m_Button_Minus;     // 버튼
    [SerializeField] Button m_Button_Equal;     // 버튼
    [SerializeField] Button m_Button_Clear;     // 버튼

    private string m_strNum1 = "";              // 좌측값
    private string m_strNum2 = "";              // 우측값
    
    //----------------
    private float m_nLeft = 0;

    private bool m_bPlus = false;
    private bool m_bMinus = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_BtnNums.Count; i++)
        {
            int idx = i;
            m_BtnNums[i].onClick.AddListener(() => OnClicked_Num(idx));
        }

        m_Button_Plus.onClick.AddListener(OnClicked_Pluse);
        m_Button_Minus.onClick.AddListener(OnClicked_Minus);
        m_Button_Equal.onClick.AddListener(OnClicked_Equal);
        m_Button_Clear.onClick.AddListener(OnClicked_Clear);
    }


    public void OnClicked_Num(int idx)
    {
        m_txtResult.text = "";

        if ( m_bPlus || m_bMinus )
        {
            m_strNum2 += idx;
            m_txtResult.text = m_strNum2;
        }
        else
        {
            m_strNum1 += idx;
            m_txtResult.text = m_strNum1;
        }
    }

    private void Click_Operator()
    {
        if (m_bPlus)
        {
            if (!m_strNum2.Equals(""))
                m_nLeft += int.Parse(m_strNum2);

            m_strNum2 = "";
        }
        else if( m_bMinus)
        {
            if (!m_strNum2.Equals(""))
                m_nLeft -= int.Parse(m_strNum2);

            m_strNum2 = "";

        }
        else
        {
            if (!m_strNum1.Equals(""))
                m_nLeft = (float)int.Parse(m_strNum1);

            m_strNum1 = "";
        }
    }


    public void OnClicked_Pluse()
    {
        Click_Operator();
        m_bPlus = true;
        m_bMinus = false;
    }
    public void OnClicked_Minus()
    {
        Click_Operator();
        m_bMinus = true;
        m_bPlus = false;
    }


    public void OnClicked_Equal()
    {
        Debug.LogFormat(" m_strNum1 = {0}, m_strNum2 = {1}", m_nLeft, m_strNum2);
        float nLeft = m_nLeft; 
        int nRight = 0;
        if( !m_strNum2.Equals("")) 
            nRight = int.Parse(m_strNum2);

        float fResult = CalculateNumber(nLeft, nRight);

        m_txtResult.text = string.Format("{0:0.#}", fResult);
        ClearNum();
    }


    public float CalculateNumber(float nLeft, float nRight)
    {
        float nRes = 0;

        if (m_bPlus)
        {
            nRes = nLeft + nRight;
        }
        else if (m_bMinus)
        {
            nRes = nLeft - nRight;
        }

        return nRes;
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

        m_bPlus = false;
        m_bMinus = false;
    }


    //private EOP m_eOperator = EOP.eNone;        // 연산자 타입    
    //public enum EOP
    //{
    //    eNone = 0,
    //    ePlus = 1,
    //    eMinus,
    //    eMultiply,
    //    eDevide
    //}
    //bool IsClickOperator
    //{
    //    get { return m_eOperator != EOP.eNone; }
    //}

    //public void OnClicked_Num(int idx)
    //{
    //    m_txtResult.text = "";

    //    if (IsClickOperator)
    //    {
    //        m_strNum2 += idx;
    //        m_txtResult.text = m_strNum2;
    //    }
    //    else{
    //        m_strNum1 += idx;
    //        m_txtResult.text = m_strNum1;
    //    }
    //}

    //private void Click_Operator()
    //{
    //    if (IsClickOperator)
    //    {
    //        if (!m_strNum2.Equals(""))
    //            m_nLeft = CalculateNumber(m_nLeft, int.Parse(m_strNum2));

    //        m_strNum2 = "";
    //    }
    //    else
    //    {
    //        if (!m_strNum1.Equals(""))
    //            m_nLeft = (float)int.Parse(m_strNum1);

    //        m_strNum1 = "";
    //    }
    //}
    //public float CalculateNumber(float nLeft, float nRight)
    //{
    //    float nRes = 0;
    //    switch (m_eOperator)
    //    {
    //        case EOP.ePlus:
    //            nRes = nLeft + nRight;
    //            break;
    //        case EOP.eMinus:
    //            nRes = nLeft - nRight;
    //            break;
    //            //case EOP.eMultiply:
    //            //    nRes = nLeft * nRight;
    //            //    break;
    //            //case EOP.eDevide:
    //            //    nRes = (float)nLeft / nRight;
    //            //    break;
    //    }
    //    return nRes;
    //}

    //public void ClearNum()
    //{
    //    m_nLeft = 0;
    //    m_strNum1 = "";
    //    m_strNum2 = "";
    //    m_eOperator = EOP.eNone;

    //}

}
