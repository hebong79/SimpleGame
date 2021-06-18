using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

/*
 *   [ 계산기 프로그램 실습 ]
 *   
 *   정수만 사용 가능하고,  정수로만 출력된다.
 *   
 *  개선사항: 실수도 출력가능하게 만들자
 */

public class CTestCalculatorUI : MonoBehaviour
{
    // 버튼 타입
    public enum EBtn
    {
        e0 = 0,
        e1,
        e9 = 9,
        ePoint,
        eEqual,
        ePlus,
        eMinus,
        eMutiply,
        eDevide,
        eBack,
        eClear,
        eClearAll,
        eMod,

        eMax,
    }

    [SerializeField] List<Button> m_Buttons;        // 버튼 리스트
    [SerializeField] Text m_txtResult;              // 최종결과 텍스트
    [SerializeField] Text m_txtSub;                 // 중간 기록 텍스트

    private bool m_isClickNumber = true;// 숫자를 클릭했는가?

    private int m_nNumPos = 0;          // m_Nums의 위치값
    private string m_strNum1 = "";      // 좌측값
    private string m_strNum2 = "";      // 우측값
    private string m_strSubNum = "";    // 중간계산 기록을 위한 문자열
    private int m_nOperatorIdx = 0;     // 바로전에 선택된 연산자 인덱스 번호
    private int m_iClickedPoint = 0;    // 점을 클릭했는지 여부( float 인지 체크 )    

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }


    public void Initialize()
    {
        // 주의: for문 또는 람다식 안에서는 클로저 때문에 i 가 전부 같은 값을 가지는 현상이 있다.
        //       "int idx = i" 와 같은 형태로 지역변수를 받아서 사용해야 한다.
        for (int i = 0; i < m_Buttons.Count; i++)
        {
            int idx = i;
            
            m_Buttons[idx].onClick.AddListener(delegate
            {
               OnClicked_Number(idx);
            });

            // 람다식 표선
            //m_Buttons[idx].onClick.AddListener(() =>
            //{
            //    OnClicked_Number(idx);
            //});

        }
    }

    public void OnClicked_Number(int iIndex)
    {
        if ( iIndex >= 0 && iIndex <= 9 )
        {
            OnClicked_NumberOnly(iIndex);
            m_isClickNumber = true;
        }
        else
        {
            switch ( iIndex )
            {
                case (int)EBtn.ePoint:
                    OnClicked_Point();
                    break;
                case (int)EBtn.eEqual:
                    OnClicked_Equal();
                    break;
                case (int)EBtn.ePlus:
                    OnClicked_Plus();
                    break;
                case (int)EBtn.eMinus:
                    OnClicked_Minus();
                    break;
                case (int)EBtn.eMutiply:
                    OnClicked_Multiply();
                    break;
                case (int)EBtn.eDevide:
                    OnClicked_Devide();
                    break;
                case (int)EBtn.eBack:
                    OnClicked_Back();
                    break;
                case (int)EBtn.eClear:
                    OnClicked_Clear();
                    break;
                case (int)EBtn.eClearAll:
                    OnClicked_ClearAll();
                    break;
                case (int)EBtn.eMod:
                    OnClicked_Mod();
                    break;
            }
  
            m_isClickNumber = false;

            if (iIndex == (int)EBtn.eBack)
                m_isClickNumber = true;
        }
    }

    public void OnClicked_NumberOnly(int iIndex)
    {
        if (m_nNumPos < 1)
        {
            m_strNum1 += iIndex;

            if (m_iClickedPoint == 0)
            {
                int nValue = int.Parse(m_strNum1);
                m_txtResult.text = nValue.ToString();
            }
            else
            {
                double fValue = double.Parse(m_strNum1);
                m_txtResult.text = fValue.ToString();
            }

            // 중간기록은 초기화 하기
            m_txtSub.text = "";

        }
        else
        {
            m_strNum2 += iIndex;

            if( m_iClickedPoint == 0 )
            {
                int nValue = int.Parse(m_strNum2);
                m_txtResult.text = nValue.ToString();
            }
            else
            {
                double fValue = double.Parse(m_strNum2);
                m_txtResult.text = fValue.ToString();
            }
        }
    }

    public void OnClicked_Point()
    {
        if (m_nNumPos < 1)
        {
            m_strNum1 += ".";
            m_txtResult.text = m_strNum1;

            m_iClickedPoint = 1;
        }
        else
        {
            m_strNum2 += ".";
            m_txtResult.text = m_strNum2;
            m_iClickedPoint = 1;
        }
    }

    // 정보값들 초기화 하기
    public void Clear()
    {
        m_strNum1 = "";
        m_strNum2 = "";
        m_strSubNum = "";
        m_nNumPos = 0;
        m_iClickedPoint = 0;
    }

    // 컴포넌트 초기화 하기
    public void ClearTextComponent()
    {
        m_txtResult.text = "0";
        m_txtSub.text = "";
    }


    public void OnClicked_Equal()
    {
        if (!m_isClickNumber)
            return;

        if (m_strNum1 == "" || m_strNum2 == "")
        {
            Clear();
            return;
        }

        CalculateNumber();
     
        m_strSubNum += m_strNum2 + " = ";
        m_txtSub.text = m_strSubNum;

        // 초기화
        Clear();

        m_isClickNumber = false;

    }

    // 결과값 계산및 출력
    public double CalculateNumber()
    {
        if (m_strNum1 == "" || m_strNum2 == "")
        {
            return 0;
        }
        double fLeft = Convert.ToDouble(m_strNum1);
        double fRight = double.Parse(m_strNum2);
        double fResult = 0;

        switch (m_nOperatorIdx)
        {
            case (int)EBtn.ePlus:
                fResult = fLeft + fRight;
                break;
            case (int)EBtn.eMinus:
                fResult = fLeft - fRight;
                break;
            case (int)EBtn.eMutiply:
                fResult = fLeft * fRight;
                break;
            case (int)EBtn.eDevide:
                fResult = fLeft / fRight;
                break;
            case (int)EBtn.eMod:
                fResult = fLeft % fRight;
                break;
        }

        m_txtResult.text = fResult.ToString();
        
        return fResult;
    }

    //덧셈
    public void OnClicked_Plus()
    {
        if (!m_isClickNumber)
            return;

        if (m_nNumPos > 0)
        {
            double value = CalculateNumber();
            m_strNum1 = (m_iClickedPoint == 0) ? ((int)value).ToString() : value.ToString();
            m_strSubNum += m_strNum2 + " + ";
        }
        else
        {
            m_strSubNum += m_strNum1 + " + ";
        }
        m_txtSub.text = m_strSubNum;

        m_nOperatorIdx = (int)EBtn.ePlus;

        m_strNum2 = "";
        m_nNumPos++;

        m_isClickNumber = false;
    }
    // 뺄셈
    public void OnClicked_Minus()
    {
        if (!m_isClickNumber)
            return;

        if (m_nNumPos > 0)
        {
            double value = CalculateNumber();
            m_strNum1 = (m_iClickedPoint == 0) ? ((int)value).ToString() : value.ToString();
            m_strSubNum += m_strNum2 + " - ";
        }
        else
        {
            m_strSubNum += m_strNum1 + " - ";
        }

        m_txtSub.text = m_strSubNum;

        m_nOperatorIdx = (int)EBtn.eMinus;

        m_strNum2 = "";
        m_nNumPos++;
        m_isClickNumber = false;
    }

    // 곱셈
    public void OnClicked_Multiply()
    {
        if (!m_isClickNumber)
            return;

        if (m_nNumPos > 0)
        {
            double value = CalculateNumber();
            m_strNum1 = (m_iClickedPoint == 0) ? ((int)value).ToString() : value.ToString();

            m_strSubNum += m_strNum2 + " x ";
        }
        else
        {
            m_strSubNum += m_strNum1 + " x ";
        }

        m_txtSub.text = m_strSubNum;

        m_nOperatorIdx = (int)EBtn.eMutiply;

        m_strNum2 = "";
        m_nNumPos++;
        m_isClickNumber = false;
    }

    // 나눗셈
    public void OnClicked_Devide()
    {
        if (!m_isClickNumber)
            return;

        if (m_nNumPos > 0)
        {
            double value = CalculateNumber();
            m_strNum1 = (m_iClickedPoint == 0) ? ((int)value).ToString() : value.ToString();

            m_strSubNum += m_strNum2 + " ÷ ";
        }
        else
        {
            m_strSubNum += m_strNum1 + " ÷ ";
        }

        m_txtSub.text = m_strSubNum;

        m_nOperatorIdx = (int)EBtn.eDevide;

        m_strNum2 = "";
        m_nNumPos++;
        m_isClickNumber = false;
    }

    // 나머지 연산
    public void OnClicked_Mod()
    {
        if (!m_isClickNumber)
            return;

        if (m_nNumPos > 0)
        {
            double value = CalculateNumber();
            m_strNum1 = (m_iClickedPoint == 0) ? ((int)value).ToString() : value.ToString();

            m_strSubNum += m_strNum2 + " % ";
        }
        else
        {
            m_strSubNum += m_strNum1 + " % ";
        }

        m_txtSub.text = m_strSubNum;

        m_nOperatorIdx = (int)EBtn.eMod;

        m_strNum2 = "";
        m_nNumPos++;
        m_isClickNumber = false;
    }

    // 백스페이스 ( 마지막 글자 삭제 )
    public void OnClicked_Back()
    {
        if( m_nNumPos > 0 )
        {
            int nLen = m_strNum2.Length;
            if (nLen > 0)
            {
                m_strNum2 = m_strNum2.Substring(0, nLen-1);
                m_txtResult.text = m_strNum2;
            }
        }
        else
        {
            int nLen = m_strNum1.Length;
            if (nLen > 0)
            {
                m_strNum1 = m_strNum1.Substring(0, nLen - 1);
                m_txtResult.text = m_strNum1;
            }
        }
    }

    // 초기화
    public void OnClicked_Clear()
    {
        Clear();
        ClearTextComponent();
    }

    // 초기화
    public void OnClicked_ClearAll()
    {
        Clear();
        ClearTextComponent();
    }

}
