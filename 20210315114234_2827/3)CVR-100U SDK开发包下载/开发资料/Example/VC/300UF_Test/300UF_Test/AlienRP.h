
#pragma once
#include "Resource.h"

#ifdef _WIN32_WCE
#error "CDHtmlDialog is not supported for Windows CE."
#endif 

// AlienRP dialog

class AlienRP : public CDHtmlDialog
{
	DECLARE_DYNCREATE(AlienRP)

public:
	AlienRP(CWnd* pParent = NULL);   // standard constructor
	virtual ~AlienRP();
// Overrides
	HRESULT OnButtonOK(IHTMLElement *pElement);
	HRESULT OnButtonCancel(IHTMLElement *pElement);

// Dialog Data
	enum { IDD = IDD_AlienResidencePermit, IDH = IDR_HTML_ALIENRP1 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	virtual BOOL OnInitDialog();
	DECLARE_MESSAGE_MAP()
	DECLARE_DHTML_EVENT_MAP()
public:
		//外籍
	CString m_name;//英文姓名
	CString m_stChineseName;// 中文姓名
	CString m_stCertVer;// 证件版本
	CString m_sex;// 性别
	CString m_nation;//  国籍
	CString m_birth;//出生日期
	CString m_idcode;//证件号码 
	CString m_depart;// 签发机关
	CString m_depCode;// 签发机关号码
	CString m_valid;//有效期限
	//CString m_nation_code;//国籍代码
	CBitmap m_bmp;	

	//通用
	CString m_ReadTime;
	CString m_SamID;
	CString szXPPath;
	bool m_ShowP;
	void ShowPhoto();

	
	afx_msg void OnPaint();
	afx_msg void OnBnClickedButton1();
};
