#pragma once
#include "Resource.h"
#include "afxwin.h"

#ifdef _WIN32_WCE
#error "CDHtmlDialog is not supported for Windows CE."
#endif 

typedef int (PASCAL* lpM1_MF_HL_Request)(BYTE nMode,UINT * pSNR, USHORT * pTagType);
typedef int (PASCAL* lpM1_MF_HL_Auth)(BYTE nMode, UINT nSNR, BYTE nBlock, BYTE nKey[6]);
typedef int (PASCAL* lpM1_MF_HL_Read)(BYTE nMode, UINT nSNR, BYTE nBlock, BYTE nKey[6], BYTE * pReadBuff, UINT nBuff);
typedef int (PASCAL* lpM1_MF_HL_Write)(BYTE nMode, UINT nSNR, BYTE nBlock, BYTE nKey[6], BYTE * pWriteBuff, UINT nBuff);
typedef int (PASCAL* lpM1_MF_GetFisCardID)(BYTE* FisCardID, BYTE* LifeEndData,int* FisCardIDLen);



// M1Card dialog

class M1Card : public CDHtmlDialog
{
	DECLARE_DYNCREATE(M1Card)

public:
	M1Card(CWnd* pParent = NULL);   // standard constructor
	virtual ~M1Card();
// Overrides
	HRESULT OnButtonOK(IHTMLElement *pElement);
	HRESULT OnButtonCancel(IHTMLElement *pElement);

// Dialog Data
	enum { IDD = IDD_M1Card, IDH = IDR_HTML_M1CARD };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	virtual BOOL OnInitDialog();

	DECLARE_MESSAGE_MAP()
	DECLARE_DHTML_EVENT_MAP()

public:
	afx_msg void OnBnClickedButton2();
public:

	lpM1_MF_HL_Request M1_MF_HL_Request;
	lpM1_MF_HL_Auth M1_MF_HL_Auth;
	lpM1_MF_HL_Read M1_MF_HL_Read;
	lpM1_MF_HL_Write M1_MF_HL_Write;
	lpM1_MF_GetFisCardID M1_MF_GetFisCardID;


	BYTE Mode;//读卡模式
	UINT SNR;//卡号数据
	USHORT TagType;//卡类型
	byte Key[6];//密码
	int iIfOpen;//是否内部连接串口
	CComboBox ModeComBox;
	int Block;//块号
	CString CardID;//卡号
	CString CSTagType;
	CString WriteBuffer;
	CString CSKey;
	bool Com_bOpen;
	int iPort;

	void AddString(CString PutData);
	int CStrGetData(CString IntoData,byte *pData,int command_long);
	afx_msg void OnBnClickedButton3();
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedButton6();
	afx_msg void OnBnClickedButton7();
	afx_msg void OnBnClickedButton8();
	CButton m_ButInit;
	CButton m_ButClear;
	CButton m_ButFindCard;
	CButton m_ButRZ;
	CButton m_ButReadM1;
	CButton m_ButWriteM1;
	void UnInit();
	afx_msg void OnBnClickedButton9();
	CButton m_ButGETFisCard;
};
