
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
		//�⼮
	CString m_name;//Ӣ������
	CString m_stChineseName;// ��������
	CString m_stCertVer;// ֤���汾
	CString m_sex;// �Ա�
	CString m_nation;//  ����
	CString m_birth;//��������
	CString m_idcode;//֤������ 
	CString m_depart;// ǩ������
	CString m_depCode;// ǩ�����غ���
	CString m_valid;//��Ч����
	//CString m_nation_code;//��������
	CBitmap m_bmp;	

	//ͨ��
	CString m_ReadTime;
	CString m_SamID;
	CString szXPPath;
	bool m_ShowP;
	void ShowPhoto();

	
	afx_msg void OnPaint();
	afx_msg void OnBnClickedButton1();
};
