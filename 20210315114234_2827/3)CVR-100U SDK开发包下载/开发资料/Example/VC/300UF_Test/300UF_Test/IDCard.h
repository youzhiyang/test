
#include "afxwin.h"
#pragma once

#ifdef _WIN32_WCE
#error "CDHtmlDialog is not supported for Windows CE."
#endif 
#include "Resource.h"

// IDCard dialog

class IDCard : public CDHtmlDialog
{
	DECLARE_DYNCREATE(IDCard)

public:
	IDCard(CWnd* pParent = NULL);   // standard constructor
	virtual ~IDCard();
// Overrides
	HRESULT OnButtonOK(IHTMLElement *pElement);
	HRESULT OnButtonCancel(IHTMLElement *pElement);

// Dialog Data
	enum { IDD = IDD_IDCard, IDH = IDR_HTML_IDCARD };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	virtual BOOL OnInitDialog();

	DECLARE_MESSAGE_MAP()
	DECLARE_DHTML_EVENT_MAP()
public:
	bool m_ShowP;
	void ShowPhoto();
	CString m_civicName;
    CString m_civicSex;
	CString m_civicNation;
    CString m_civicBirthday;
	CString m_civicCardID;
	CString m_civicAddress;
	CString m_civicNationCode;
	CString m_ReadTime;
	CString m_SamID;
	CString m_civicValidDate;
	CString m_civicDepartment;
    CBitmap m_bmp;	
	CString szXPPath;

	// ¸Û°ÄÌ¨
	CString m_IssuesNum;
	CString m_PassCheckID;
	CString m_Type;


	//CMy300UF_TestDlg *CMy300;
public:
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedButtonReadIdcard();
	afx_msg void OnPaint();
public:
	afx_msg void OnBnClickedButtonread();
};
