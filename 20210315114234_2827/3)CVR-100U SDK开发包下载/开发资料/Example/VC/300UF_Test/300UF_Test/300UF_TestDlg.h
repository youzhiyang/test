
// 300UF_TestDlg.h : 头文件
//

#pragma once
#include "afxcmn.h"
#include "AlienRP.h"
#include "IDCard.h"
#include "M1Card.h"
#include "Resource.h"
#include "afxwin.h"
#include <process.h>
//#if !defined(AFX_MYSTATICDLG_H__E3625259_1439_4AE0_A91F_600E1E50509B__INCLUDED_)
//#define AFX_MYSTATICDLG_H__E3625259_1439_4AE0_A91F_600E1E50509B__INCLUDED_



#if _MSC_VER > 1000
#pragma once
#endif




typedef int(PASCAL *lpCVR_InitComm)(int port);
typedef int(PASCAL *lpCVR_CloseComm)();
typedef int(PASCAL *lpCVR_Authenticate)();
typedef int(PASCAL *lpCVR_AuthenticateForNoJudge)();
typedef int(PASCAL *lpCVR_Read_Content)();
typedef int(PASCAL *lpCVR_SetBaudRate)(int nBaudRate);

typedef int(PASCAL *lpGetPeopleName)( unsigned char *strTmp, int *strLen);
typedef int(PASCAL *lpGetPeopleSex)(unsigned char *strTmp, int *strLen);
typedef int(PASCAL *lpGetPeopleNation)(unsigned char *strTmp, int *strLen);
typedef int(PASCAL *lpGetPeopleBirthday)(unsigned char *strTmp, int *strLen);
typedef int(PASCAL *lpGetPeopleAddress)(unsigned char *strTmp, int *strLen);
typedef int(PASCAL *lpGetPeopleIDCode)(unsigned char *strTmp, int *strLen);
typedef int(PASCAL *lpGetDepartment)(unsigned char *strTmp, int *strLen);
typedef int(PASCAL *lpGetStartDate)(unsigned char *strTmp, int *strLen);
typedef int(PASCAL *lpGetEndDate)(unsigned char *strTmp, int *strLen);
typedef int(PASCAL *lpGetNationCode)(UCHAR *sexData, int * pLen);
typedef int(PASCAL *lpGetNationCodeStrU)(unsigned char * nationData, int *pLen);
typedef int(PASCAL *lpCVR_GetSAMIDU)(unsigned char *SAMID);

typedef int(PASCAL *lpCVR_GetIDCardUID)(char * pUidBuff, int nBuffLen);

typedef int(PASCAL *lpGetPeopleChineseName)( unsigned char *strTmp, int *strLen);
typedef int(PASCAL *lpGetPeopleCertVersion)( unsigned char *strTmp, int *strLen);

// 港澳通行证
typedef int (PASCAL *lpGetPassCheckID)( unsigned char *strTmp, int *strLen);
typedef int (PASCAL *lpGetIssuesNum)(int *IssuesNum);
typedef int (PASCAL *lpGetCertType)( unsigned char *strTmp, int *strLen);


// CMy300UF_TestDlg 对话框
class CMy300UF_TestDlg : public CDialog
{
// 构造
public:
	CMy300UF_TestDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_MY300UF_TEST_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	CTabCtrl m_Tab;
	IDCard Tab_IDCard;
	AlienRP Iab_AlienRP;
	M1Card CardM1;
	afx_msg void OnTcnSelchangeTab1(NMHDR *pNMHDR, LRESULT *pResult);
public:
	CString GetTempPathx();
	CString GetMoudulePath();
	void ShowPhoto(int nPhotoID);
	void OnLoadCivicPic();// 写身份证信息
	void ReadCard();
	void SelchangeTab1();
	void OnLoadForeinerPic();// 写外籍 
	void OnLoadCivicPicHK(); // 写港澳台
	int m_CardType;//证件类型，0为身份证，1为外国人居留证,2为港澳台


	/*lpM1_MF_HL_Request M1_MF_HL_Request;
	lpM1_MF_HL_Auth M1_MF_HL_Auth;
	lpM1_MF_HL_Read M1_MF_HL_Read;
	lpM1_MF_HL_Write M1_MF_HL_Write;*/


	lpCVR_InitComm CVR_InitComm;
	lpCVR_CloseComm CVR_CloseComm;
	lpCVR_Authenticate CVR_Authenticate;
	lpCVR_AuthenticateForNoJudge CVR_AuthenticateForNoJudge;
	lpCVR_Read_Content CVR_Read_Content;
	lpCVR_GetSAMIDU CVR_GetSAMID;
	lpCVR_SetBaudRate CVR_SetComBaudrate;

	lpGetPeopleChineseName CVR_GetPeopleChineseName;
	lpGetPeopleCertVersion CVR_GetPeopleCertVersion;

	lpGetPeopleName CVR_GetPeopleName;
	lpGetPeopleSex CVR_GetPeopleSex;
	lpGetPeopleNation CVR_GetPeopleNation;
	lpGetPeopleBirthday CVR_GetPeopleBirthday;
	lpGetPeopleAddress CVR_GetPeopleAddress;
	lpGetPeopleIDCode CVR_GetPeopleIDCode;
	lpGetDepartment CVR_GetDepartment;
	lpGetStartDate CVR_GetStartDate;
	lpGetEndDate CVR_GetEndDate;
	lpGetNationCode CVR_GetNationCode;
	lpGetNationCodeStrU CVR_GetNationCodeStrU;
	lpCVR_GetIDCardUID CVR_GetIDCardUID;

	
	//港澳台
	lpGetPassCheckID CVR_lpGetPassCheckID;
	lpGetIssuesNum CVR_lpGetIssuesNum;
	lpGetCertType CVR_lpGetCertType;

	HINSTANCE dllHandle;

	//身份证
	CString m_civicName;
    CString m_civicSex;
	CString m_civicNation;
    CString m_civicBirthday;
	CString m_civicCardID;
	CString m_civicAddress;
	CString m_civicNationCode;
	CString m_civicValidDate;
	CString m_civicDepartment;

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


	

	//通用
	CString m_ReadTime;
	CString m_SamID;
	CString szXPPath;
	CString m_UID;//国籍代码
	int iPort;
	int m_Baudrate;

	static UINT WINAPI AutoRun( LPVOID lPvoid );
	bool m_Auto;
	bool m_bOpen;
	afx_msg void OnBnClickedButton2();
	CComboBox C_ComBo;
	afx_msg void OnNMThemeChangedCombo1(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnCbnSelchangeCombo1();
	CComboBox m_ComBot;
	afx_msg void OnCbnSelchangeCombo2();
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedButtonConn();
	afx_msg void OnBnClickedButtonReadcard();
	CButton m_ButtonReadCard;
	afx_msg void OnClose();
};
