
// 300UF_TestDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "300UF_Test.h"
#include "300UF_TestDlg.h"
#include "resource.h"
#include <vector>
#include <string>

#pragma pack(push)
#pragma pack(1)
struct defMsg{
	WCHAR name[15];
	WCHAR sex[1];
	WCHAR nation[2];
	WCHAR bY[4],bM[2],bD[2];
	WCHAR address[35];
	WCHAR id[18];
	WCHAR depart[15];
	WCHAR tsY[4],tsM[2],tsD[2];
	WCHAR tpY[4],tpM[2],tpD[2];
};
#pragma pack(pop)




CMy300UF_TestDlg *CMy300;




TCHAR	szExePath[MAX_PATH];


#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif



// ����Ӧ�ó��򡰹��ڡ��˵���� CAboutDlg �Ի���

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// �Ի�������
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

// ʵ��
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CMy300UF_TestDlg �Ի���




CMy300UF_TestDlg::CMy300UF_TestDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMy300UF_TestDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMy300UF_TestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_TAB1, m_Tab);
	DDX_Control(pDX, IDC_COMBO1, C_ComBo);
	DDX_Control(pDX, IDC_COMBO2, m_ComBot);
	DDX_Control(pDX, IDC_BUTTON_ReadCard, m_ButtonReadCard);
}

BEGIN_MESSAGE_MAP(CMy300UF_TestDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB1, &CMy300UF_TestDlg::OnTcnSelchangeTab1)
	ON_BN_CLICKED(IDC_BUTTON2, &CMy300UF_TestDlg::OnBnClickedButton2)
	ON_NOTIFY(NM_THEMECHANGED, IDC_COMBO1, &CMy300UF_TestDlg::OnNMThemeChangedCombo1)
	ON_CBN_SELCHANGE(IDC_COMBO1, &CMy300UF_TestDlg::OnCbnSelchangeCombo1)
	ON_CBN_SELCHANGE(IDC_COMBO2, &CMy300UF_TestDlg::OnCbnSelchangeCombo2)
	ON_BN_CLICKED(IDC_BUTTON1, &CMy300UF_TestDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON_Conn, &CMy300UF_TestDlg::OnBnClickedButtonConn)
	ON_BN_CLICKED(IDC_BUTTON_ReadCard, &CMy300UF_TestDlg::OnBnClickedButtonReadcard)
	ON_WM_CLOSE()
END_MESSAGE_MAP()


// CMy300UF_TestDlg ��Ϣ�������

BOOL CMy300UF_TestDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// ��������...���˵�����ӵ�ϵͳ�˵��С�

	// IDM_ABOUTBOX ������ϵͳ���Χ�ڡ�
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// ���ô˶Ի����ͼ�ꡣ��Ӧ�ó��������ڲ��ǶԻ���ʱ����ܽ��Զ�
	//  ִ�д˲���
	SetIcon(m_hIcon, TRUE);			// ���ô�ͼ��
	SetIcon(m_hIcon, FALSE);		// ����Сͼ��

	// TODO: �ڴ���Ӷ���ĳ�ʼ������
	m_Tab.InsertItem(0,_T("���֤"));  
	m_Tab.InsertItem(1,_T("����˾���֤"));  
	//m_Tab.InsertItem(2,_T("M1��"));  
	Tab_IDCard.Create(IDD_IDCard,GetDlgItem(IDC_TAB1));  
	Iab_AlienRP.Create(IDD_AlienResidencePermit,GetDlgItem(IDC_TAB1));  
	CardM1.Create(IDD_M1Card,GetDlgItem(IDC_TAB1));  
	//���IDC_TABTEST�ͻ�����С  
	CRect rs;  
	m_Tab.GetClientRect(&rs);  
	//�����ӶԻ����ڸ������е�λ�ã�����ʵ���޸�  
	rs.top+=22;  
	rs.bottom-=1;  
	rs.left+=1;  
	rs.right-=1;  

	//�����ӶԻ���ߴ粢�ƶ���ָ��λ��  
	Tab_IDCard.MoveWindow(&rs);  
	Iab_AlienRP.MoveWindow(&rs);  
	CardM1.MoveWindow(&rs); 
	//�ֱ��������غ���ʾ  
	Tab_IDCard.ShowWindow(true);  
	Iab_AlienRP.ShowWindow(false);  
	CardM1.ShowWindow(false); 

	//����Ĭ�ϵ�ѡ�  
	m_Tab.SetCurSel(0); 

	CString tmp;
	C_ComBo.InsertString(0,_T("USB"));
	for(int i=1;i<=16;i++)
	{
		tmp.Format(_T("Com%d"),i);
		C_ComBo.InsertString(i,tmp);
	}
	C_ComBo.SetCurSel(0);
	iPort = 1001;

	m_ComBot.InsertString(0,_T("9600"));
	m_ComBot.InsertString(1,_T("115200"));
	m_ComBot.SetCurSel(0);
	m_ComBot.EnableWindow(false);

	m_Auto = false;
	m_bOpen = true;
	iPort = 0;
	m_CardType = -1;

	m_Baudrate = 9600;
	m_ButtonReadCard.EnableWindow(false);

	CardM1.UnInit();
	////////////////////////////////////////
	//��ʼ����̬��
	CMy300 = this;

	GetModuleFileName(AfxGetApp()->m_hInstance, szExePath, sizeof(szExePath));

	CString szDLLPath(szExePath);
	int nPos = szDLLPath.ReverseFind(_T('\\')) + 1;
	szExePath[nPos] = _T('\0');
	szDLLPath.Format(_T("%s%s"), szExePath, _T("termb.dll"));
	//szDLLPath.Format(_T("%s%s"), szExePath, _T("CVR_YAReader.dll"));

	//CString szDLLPath(_T("C:\\CertReader\\HS_RdCard\\termb.dll"));

	dllHandle = LoadLibrary(szDLLPath);
	if (dllHandle!=NULL)
	{
		CVR_InitComm			= (lpCVR_InitComm)GetProcAddress(dllHandle,"CVR_InitComm");
		CVR_CloseComm			= (lpCVR_CloseComm)GetProcAddress(dllHandle,"CVR_CloseComm");
		CVR_Authenticate		= (lpCVR_Authenticate)GetProcAddress(dllHandle,"CVR_Authenticate");
		CVR_AuthenticateForNoJudge = (lpCVR_AuthenticateForNoJudge)GetProcAddress(dllHandle,"CVR_AuthenticateForNoJudge");
		CVR_Read_Content		= (lpCVR_Read_Content)GetProcAddress(dllHandle,"CVR_Read_FPContent");
		CVR_GetNationCode		= (lpGetNationCode)GetProcAddress(dllHandle,"GetNationCode");
		CVR_SetComBaudrate		= (lpCVR_SetBaudRate)GetProcAddress(dllHandle,"CVR_SetBaudRate");

	/*	M1_MF_HL_Request        = (lpM1_MF_HL_Request)GetProcAddress(dllHandle,"M1_MF_HL_Request");
		M1_MF_HL_Auth			= (lpM1_MF_HL_Auth)GetProcAddress(dllHandle,"M1_MF_HL_Auth");
		M1_MF_HL_Read			= (lpM1_MF_HL_Read)GetProcAddress(dllHandle,"M1_MF_HL_Read");
		M1_MF_HL_Write			= (lpM1_MF_HL_Write)GetProcAddress(dllHandle,"M1_MF_HL_Write");*/
	
#ifdef UNICODE
		CVR_GetPeopleName			= (lpGetPeopleName)GetProcAddress(dllHandle,"GetPeopleNameU");
		CVR_GetPeopleSex			= (lpGetPeopleSex)GetProcAddress(dllHandle,"GetPeopleSexU");
		CVR_GetPeopleNation			= (lpGetPeopleNation)GetProcAddress(dllHandle,"GetPeopleNationU");
		CVR_GetPeopleBirthday		= (lpGetPeopleBirthday)GetProcAddress(dllHandle,"GetPeopleBirthdayU");
		CVR_GetPeopleAddress		= (lpGetPeopleAddress)GetProcAddress(dllHandle,"GetPeopleAddressU");
		CVR_GetPeopleIDCode			= (lpGetPeopleIDCode)GetProcAddress(dllHandle,"GetPeopleIDCodeU");
		CVR_GetDepartment			= (lpGetDepartment)GetProcAddress(dllHandle,"GetDepartmentU");
		CVR_GetStartDate			= (lpGetStartDate)GetProcAddress(dllHandle,"GetStartDateU");
		CVR_GetEndDate				= (lpGetEndDate)GetProcAddress(dllHandle,"GetEndDateU");
		CVR_GetSAMID				= (lpCVR_GetSAMIDU)GetProcAddress(dllHandle,"CVR_GetSAMIDU");
		CVR_GetPeopleChineseName	= (lpGetPeopleChineseName)GetProcAddress(dllHandle,"GetPeopleChineseNameU");
		CVR_GetPeopleCertVersion	= (lpGetPeopleCertVersion)GetProcAddress(dllHandle,"GetPeopleCertVersionU");
		CVR_GetNationCodeStrU		= (lpGetNationCodeStrU)GetProcAddress(dllHandle,"GetNationCodeU");
		CVR_GetIDCardUID			= (lpCVR_GetIDCardUID)GetProcAddress(dllHandle,"CVR_GetIDCardUIDU");
		CVR_lpGetPassCheckID		= (lpGetPassCheckID)GetProcAddress(dllHandle,"GetPassCheckIDU");
		CVR_lpGetIssuesNum			= (lpGetIssuesNum)GetProcAddress(dllHandle,"GetIssuesNumU");
		CVR_lpGetCertType			= (lpGetCertType)GetProcAddress(dllHandle,"GetCertTypeU");


#else
		CVR_GetPeopleName			= (lpGetPeopleName)GetProcAddress(dllHandle,"GetPeopleName");
		CVR_GetPeopleSex			= (lpGetPeopleSex)GetProcAddress(dllHandle,"GetPeopleSex");
		CVR_GetPeopleNation			= (lpGetPeopleNation)GetProcAddress(dllHandle,"GetPeopleNation");
		CVR_GetPeopleBirthday		= (lpGetPeopleBirthday)GetProcAddress(dllHandle,"GetPeopleBirthday");
		CVR_GetPeopleAddress		= (lpGetPeopleAddress)GetProcAddress(dllHandle,"GetPeopleAddress");
		CVR_GetPeopleIDCode			= (lpGetPeopleIDCode)GetProcAddress(dllHandle,"GetPeopleIDCode");
		CVR_GetDepartment			= (lpGetDepartment)GetProcAddress(dllHandle,"GetDepartment");
		CVR_GetStartDate			= (lpGetStartDate)GetProcAddress(dllHandle,"GetStartDate");
		CVR_GetEndDate				= (lpGetEndDate)GetProcAddress(dllHandle,"GetEndDate");
		CVR_GetPeopleChineseName	= (lpCVR_GetSAMIDU)GetProcAddress(dllHandle,"GetPeopleChineseName");
		CVR_GetPeopleCertVersion	= (lpCVR_GetSAMIDU)GetProcAddress(dllHandle,"GetPeopleCertVersion");
		CVR_GetIDCardUID			= (lpCVR_GetIDCardUID)GetProcAddress(dllHandle,"CVR_GetIDCardUID");
		CVR_lpGetPassCheckID			= (lpGetPassCheckID)GetProcAddress(dllHandle,"GetPassCheckID");
		CVR_lpGetIssuesNum			= (lpGetIssuesNum)GetProcAddress(dllHandle,"GetIssuesNum");
		CVR_lpGetCertType			= (lpGetCertType)GetProcAddress(dllHandle,"GetCertType");
#endif
	}






	return TRUE;  // ���ǽ��������õ��ؼ������򷵻� TRUE
}

void CMy300UF_TestDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CMy300UF_TestDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // ���ڻ��Ƶ��豸������

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// ʹͼ���ڹ����������о���
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// ����ͼ��
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

//���û��϶���С������ʱϵͳ���ô˺���ȡ�ù��
//��ʾ��
HCURSOR CMy300UF_TestDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}
void CMy300UF_TestDlg::SelchangeTab1()
{
	int CurSel = m_Tab.GetCurSel();  
	switch(CurSel)  
	{  
	case 0:  
		Tab_IDCard.ShowWindow(true);  
		Iab_AlienRP.ShowWindow(false);  
		CardM1.ShowWindow(false); 
		break;  
	case 1:  
		Tab_IDCard.ShowWindow(false);  
		Iab_AlienRP.ShowWindow(true);
		CardM1.ShowWindow(false); 
		break;  
	case 2:  
		Tab_IDCard.ShowWindow(false);  
		Iab_AlienRP.ShowWindow(false); 
		CardM1.ShowWindow(true); 
		break; 
	default:
		break;
	}
}


void CMy300UF_TestDlg::OnTcnSelchangeTab1(NMHDR *pNMHDR, LRESULT *pResult)
{
	// TODO: Add your control notification handler code here
	int CurSel = m_Tab.GetCurSel();  
	switch(CurSel)  
	{  
	case 0:  
		Tab_IDCard.ShowWindow(true);  
		Iab_AlienRP.ShowWindow(false);  
		CardM1.ShowWindow(false); 
		break;  
	case 1:  
		Tab_IDCard.ShowWindow(false);  
		Iab_AlienRP.ShowWindow(true);
		CardM1.ShowWindow(false); 
		break;  
	case 2:  
		Tab_IDCard.ShowWindow(false);  
		Iab_AlienRP.ShowWindow(false); 
		CardM1.ShowWindow(true); 
		break; 
	default:
		break;
	}  
	*pResult = 0;
}


CString CMy300UF_TestDlg::GetTempPathx()
{
	CString strPathBase(_T(""));
	TCHAR sztempdirectory[MAX_PATH];

	int i = GetTempPath(MAX_PATH - 1, sztempdirectory);
	if (sztempdirectory[i - 1] != _T('\\'))
	{
		lstrcat(sztempdirectory, _T("\\"));

	}
	CString strTempdirectory(sztempdirectory);
	strPathBase = strTempdirectory + _T("chinaidcard\\");
	CreateDirectory(strPathBase, NULL);
	return strPathBase;
}

void CMy300UF_TestDlg::OnLoadCivicPic() 
{     
	////////////////////////////////////////////��ʾ����
	int nLen;
	CString strVal;
	TCHAR szContent[MAX_PATH];
	nLen = sizeof(szContent);
	if(CVR_GetPeopleName)//����
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleName((BYTE*)szContent, &nLen);
		m_civicName = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleName�����Ҳ���"));
	}

	if(CVR_GetPeopleSex)//�Ա�
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleSex((BYTE*)szContent, &nLen);
		m_civicSex = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleSex�����Ҳ���"));
	}

	if(CVR_GetPeopleNation)//����
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleNation((BYTE*)szContent, &nLen);
		m_civicNation = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleNation�����Ҳ���"));
	}

	if(CVR_GetPeopleBirthday)//��������
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleBirthday((BYTE*)szContent, &nLen);
		m_civicBirthday = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleBirthday�����Ҳ���"));
	}

	if(CVR_GetPeopleAddress)//��ַ
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleAddress((BYTE*)szContent, &nLen);
		m_civicAddress = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleAddress�����Ҳ���"));
	}

	if(CVR_GetPeopleIDCode)//֤������
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleIDCode((BYTE*)szContent, &nLen);
		m_civicCardID = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleIDCode�����Ҳ���"));
	}

	if(CVR_GetDepartment)//ǩ������
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetDepartment((BYTE*)szContent, &nLen);
		m_civicDepartment = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetDepartment�����Ҳ���"));
	}

	if(CVR_GetStartDate && CVR_GetEndDate)//��Ч����
	{
		CString strStart, strEnd, strResult;
		memset(szContent, 0, sizeof(szContent));
		CVR_GetStartDate((BYTE*)szContent, &nLen);
		strStart = szContent;
		memset(szContent, 0, sizeof(szContent));
		CVR_GetEndDate((BYTE*)szContent, &nLen);
		strEnd = szContent;
		strResult = strStart + _T("-") + strEnd;
		m_civicValidDate = strResult;
	}
	else
	{
		AfxMessageBox(_T("GetStartDate��GetEndDate�����Ҳ���"));
	}
	if (CVR_GetNationCode)//�������
	{
		int nNationCode = 0, nLen = sizeof(int);
		CVR_GetNationCode((BYTE*)&nNationCode, &nLen);
		TCHAR szNationCode[64] = {0};
		_itot_s(nNationCode, szNationCode, 10);
		m_civicNationCode = szNationCode;
	}
	

	if (CVR_GetIDCardUID)//UID
	{
		char szNationStr[64] = {0};
		int nLen = sizeof(szNationStr);
		CVR_GetIDCardUID(szNationStr, nLen);
		m_UID.Format(_T("%s"),szNationStr);
	}

	////////////////////////////////////////////��ʾ��Ƭ
	//ShowPhoto(IDC_STATIC_IDPHOTO);
}

void CMy300UF_TestDlg::OnLoadCivicPicHK() 
{     
	////////////////////////////////////////////��ʾ����
	int nLen;
	CString strVal;
	TCHAR szContent[MAX_PATH];
	nLen = sizeof(szContent);
	if(CVR_GetPeopleName)//����
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleName((BYTE*)szContent, &nLen);
		m_civicName = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleName�����Ҳ���"));
	}

	if(CVR_GetPeopleSex)//�Ա�
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleSex((BYTE*)szContent, &nLen);
		m_civicSex = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleSex�����Ҳ���"));
	}

	if(CVR_GetPeopleBirthday)//��������
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleBirthday((BYTE*)szContent, &nLen);
		m_civicBirthday = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleBirthday�����Ҳ���"));
	}

	if(CVR_GetPeopleAddress)//��ַ
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleAddress((BYTE*)szContent, &nLen);
		m_civicAddress = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleAddress�����Ҳ���"));
	}

	if(CVR_GetPeopleIDCode)//֤������
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleIDCode((BYTE*)szContent, &nLen);
		m_civicCardID = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleIDCode�����Ҳ���"));
	}

	if(CVR_GetDepartment)//ǩ������
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetDepartment((BYTE*)szContent, &nLen);
		m_civicDepartment = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetDepartment�����Ҳ���"));
	}

	if(CVR_GetStartDate && CVR_GetEndDate)//��Ч����
	{
		CString strStart, strEnd, strResult;
		memset(szContent, 0, sizeof(szContent));
		CVR_GetStartDate((BYTE*)szContent, &nLen);
		strStart = szContent;
		memset(szContent, 0, sizeof(szContent));
		CVR_GetEndDate((BYTE*)szContent, &nLen);
		strEnd = szContent;
		strResult = strStart + _T("-") + strEnd;
		m_civicValidDate = strResult;
	}
	else
	{
		AfxMessageBox(_T("GetStartDate��GetEndDate�����Ҳ���"));
	}


	if(CVR_lpGetIssuesNum)
	{
		int IssuesNum;
		CVR_lpGetIssuesNum(&IssuesNum);
		Tab_IDCard.m_IssuesNum.Format(L"%d",IssuesNum);
	}

	if(CVR_lpGetPassCheckID)
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_lpGetPassCheckID((BYTE*)szContent, &nLen);
		Tab_IDCard.m_PassCheckID = szContent;

	}
	else
	{
		AfxMessageBox(_T("CVR_GetPassCheckIDU�����Ҳ���"));
	}


 
	/*
	if (CVR_GetIDCardUID)//UID
	{
		char szNationStr[64] = {0};
		int nLen = sizeof(szNationStr);
		CVR_GetIDCardUID(szNationStr, nLen);
		m_UID.Format(_T("%s"),szNationStr);
	}*/
	////////////////////////////////////////////��ʾ��Ƭ
	//ShowPhoto(IDC_STATIC_IDPHOTO);
}

CString CMy300UF_TestDlg::GetMoudulePath()
{
	CString strRetun = L"";

	HMODULE module = AfxGetInstanceHandle();
	if (module == NULL)
	{
		return _T("");
	}
	wchar_t szBuff[MAX_PATH];
	GetModuleFileName(module, szBuff, sizeof(szBuff));

	wchar_t acBuf[200];
	GetShortPathName(szBuff, acBuf, 200);
	wchar_t acText[200];
	GetLongPathName(acBuf, acText, 200);

	CString strTemp(acBuf);
	strTemp = acText;
	int n = strTemp.ReverseFind('\\');
	strRetun = strTemp.Mid(0, n);

	return strRetun;
}


void CMy300UF_TestDlg::ReadCard()
{
	DWORD dwStart = ::GetTickCount();
	int Eor = -1;

	//int nBaudRet = CVR_SetComBaudrate(m_Baudrate);
	//Eor = CVR_InitComm(iPort);

	if(!m_bOpen)
	{
		if (1==CVR_Authenticate())
		//if (1 == CVR_AuthenticateForNoJudge())
		{
			if (1==CVR_Read_Content() )
			{
				dwStart = ::GetTickCount() - dwStart;
				m_ReadTime.Format(_T("%dms"), dwStart);//����ʱ��
				szXPPath = GetMoudulePath();
				szXPPath += _T("\\zp.bmp");

				if(CVR_lpGetCertType)//֤������
				{
					TCHAR CardType[4];
					memset(CardType, 0, sizeof(CardType));
					int nLen = 4;
					CVR_lpGetCertType((BYTE*)CardType, &nLen);
					CString strVal = CardType;
					if(strVal == 'I')
					{
						m_CardType = 1;
					}
					else
					{
						if(strVal == 'J')
						{
							m_CardType = 2;
						}
						else
						{
							m_CardType = 0;
						}
					}
					switch(m_CardType)
					{
					case 0:
						{
							Iab_AlienRP.m_ShowP = false;
							Tab_IDCard.m_ShowP = true;

							Tab_IDCard.m_Type = strVal;
							m_Tab.SetCurSel(0);
							SelchangeTab1();
							OnLoadCivicPic();


							Tab_IDCard.m_civicName = m_civicName;
							Tab_IDCard.m_civicSex = m_civicSex;
							Tab_IDCard.m_civicNation = m_civicNation;
							Tab_IDCard.m_civicBirthday = m_civicBirthday;
							Tab_IDCard.m_civicCardID = m_civicCardID;
							Tab_IDCard.m_civicAddress = m_civicAddress;
							Tab_IDCard.m_civicNationCode = m_civicNationCode;
							Tab_IDCard.m_civicValidDate = m_civicValidDate;
							Tab_IDCard.m_civicDepartment = m_civicDepartment;
							Tab_IDCard.m_civicDepartment = m_civicDepartment;
							Tab_IDCard.m_ReadTime = m_ReadTime;
							Tab_IDCard.m_SamID = m_UID;
							Tab_IDCard.szXPPath = szXPPath;
							m_Tab.SetCurSel(0);
							Tab_IDCard.ShowPhoto();
							break;
						}
					case 1:
						{
							Iab_AlienRP.m_ShowP = true;
							Tab_IDCard.m_ShowP = false;
							m_Tab.SetCurSel(1);
							SelchangeTab1();
							OnLoadForeinerPic();
							Iab_AlienRP.m_name = m_name;
							Iab_AlienRP.m_stChineseName = m_stChineseName;
							Iab_AlienRP.m_stCertVer = m_stCertVer;
							Iab_AlienRP.m_sex = m_sex;
							Iab_AlienRP.m_nation = m_nation;
							Iab_AlienRP.m_birth = m_birth;
							Iab_AlienRP.m_idcode = m_idcode;
							Iab_AlienRP.m_depart = m_depart;
							Iab_AlienRP.m_depCode = m_depCode;
							Iab_AlienRP.m_valid = m_valid;
							//Iab_AlienRP.m_nation_code = m_UID;
							Iab_AlienRP.m_ReadTime = m_ReadTime;
							Iab_AlienRP.m_SamID = m_UID;
							Iab_AlienRP.szXPPath = szXPPath;
							m_Tab.SetCurSel(1);
							Iab_AlienRP.ShowPhoto();

							break;
						}
					case 2:
						{
							Iab_AlienRP.m_ShowP = false;
							Tab_IDCard.m_ShowP = true;
							m_Tab.SetCurSel(0);
							SelchangeTab1();
							OnLoadCivicPicHK();
							Tab_IDCard.m_Type = strVal;
							Tab_IDCard.m_civicName = m_civicName;
							Tab_IDCard.m_civicSex = m_civicSex;

							Tab_IDCard.m_civicBirthday = m_civicBirthday;
							Tab_IDCard.m_civicCardID = m_civicCardID;
							Tab_IDCard.m_civicAddress = m_civicAddress;

							Tab_IDCard.m_civicValidDate = m_civicValidDate;
							Tab_IDCard.m_civicDepartment = m_civicDepartment;
							Tab_IDCard.m_civicDepartment = m_civicDepartment;
							Tab_IDCard.m_ReadTime = m_ReadTime;
							Tab_IDCard.m_SamID = m_UID;
							Tab_IDCard.szXPPath = szXPPath;
							m_Tab.SetCurSel(0);
							Tab_IDCard.ShowPhoto();

						}
					}

				}
				else
				{
					MessageBox(_T("����CVR_lpGetCertTypeʧ�ܣ�"));
					return;
				}
			}
			else
			{
				MessageBox(_T("����ʧ�ܣ�"));
				return;
			}
		}
		else
		{
			MessageBox(_T("�ҿ�ʧ�ܣ�"));
			return;
		}
	}
	else
	{
		MessageBox(_T("δ���ӣ�"));
		return;
	}    	
	//CVR_CloseComm(); 
}

void CMy300UF_TestDlg::OnLoadForeinerPic() 
{     
	//��ʾ����
	int nLen;
	CString strResult;
	TCHAR szContent[MAX_PATH];
	nLen = sizeof(szContent);
	if(CVR_GetPeopleName)//Ӣ������
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleName((BYTE*)szContent, &nLen);
		m_name = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleName�����Ҳ���"));
	}

	if(CVR_GetPeopleChineseName)//��������
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleChineseName((BYTE*)szContent, &nLen);
		m_stChineseName =szContent;
	}
	if(CVR_GetPeopleCertVersion)//֤���汾
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleCertVersion((BYTE*)szContent, &nLen);
		m_stCertVer = szContent;
	}

	if(CVR_GetPeopleSex)//�Ա�
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleSex((BYTE*)szContent, &nLen);
		strResult = _T("");
		strResult += szContent;
		if (strResult.Find(_T("Ů")) != -1)
		{
			strResult += _T("/F");
		}
		else if (strResult.Find(_T("��")) != -1)
		{
			strResult += _T("/M");
		}
		m_sex = strResult;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleSex�����Ҳ���"));
	}

	if(CVR_GetPeopleNation)//����
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleNation((BYTE*)szContent, &nLen);
		strResult = _T("");
		strResult += szContent;
		if (CVR_GetNationCodeStrU)
		{
			WCHAR szNationStr[64] = {0};
			int nLen = sizeof(szNationStr);
			CVR_GetNationCodeStrU((BYTE*)&szNationStr, &nLen);
			strResult += _T("/");
			strResult += szNationStr;
		}
		m_nation = strResult;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleNation�����Ҳ���"));
	}

	if(CVR_GetPeopleBirthday)//��������
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleBirthday((BYTE*)szContent, &nLen);
		strResult = _T("");
		strResult += szContent;
		int nLength = strResult.GetLength();
		if (nLength == 8)
		{
			CString strTmp;
			strTmp = strResult.Mid(0, 4);
			strTmp += _T(".");
			strTmp += strResult.Mid(4, 2);
			strTmp += _T(".");
			strTmp += strResult.Mid(6, 2);
			m_birth = strTmp;
		}	
	}
	else
	{
		AfxMessageBox(_T("GetPeopleBirthday�����Ҳ���"));
	}

	if(CVR_GetPeopleIDCode)//֤������
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetPeopleIDCode((BYTE*)szContent, &nLen);
		m_idcode = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetPeopleIDCode�����Ҳ���"));
	}

	if(CVR_GetDepartment)
	{
		memset(szContent, 0, sizeof(szContent));
		CVR_GetDepartment((BYTE*)szContent, &nLen);//ǩ������
		strResult = _T("");
		strResult += szContent;
		if (strResult.Find(_T("1500")) != -1)
		{
			strResult = _T("������/Ministry of Public Security");
		}
		m_depart = strResult;
		m_depCode = szContent;
	}
	else
	{
		AfxMessageBox(_T("GetDepartment�����Ҳ���"));
	}

	if(CVR_GetStartDate && CVR_GetEndDate)//��Ч����
	{
		CString strStart, strEnd, strResult;
		memset(szContent, 0, sizeof(szContent));
		CVR_GetStartDate((BYTE*)szContent, &nLen);
		strStart = szContent;
		int nLength = strStart.GetLength();
		if (nLength == 8)
		{
			CString strTmp;
			strTmp = strStart.Mid(0, 4);
			strTmp += _T(".");
			strTmp += strStart.Mid(4, 2);
			strTmp += _T(".");
			strTmp += strStart.Mid(6, 2);
			strStart = strTmp;
		}
		memset(szContent, 0, sizeof(szContent));
		CVR_GetEndDate((BYTE*)szContent, &nLen);
		strEnd = szContent;
		nLength = strEnd.GetLength();
		if (nLength == 8)
		{
			CString strTmp;
			strTmp = strEnd.Mid(0, 4);
			strTmp += _T(".");
			strTmp += strEnd.Mid(4, 2);
			strTmp += _T(".");
			strTmp += strEnd.Mid(6, 2);
			strEnd = strTmp;
		}
		m_valid =  strStart + _T("  -  ") + strEnd;
	}
	else
	{
		AfxMessageBox(_T("GetStartDate��GetEndDate�����Ҳ���"));
	}
	if (CVR_GetIDCardUID)//UID
	{
		char szNationStr[64] = {0};
		int nLen = sizeof(szNationStr);
		CVR_GetIDCardUID(szNationStr, nLen);
		m_UID.Format(_T("%s"),szNationStr);
	}
}


UINT WINAPI CMy300UF_TestDlg::AutoRun( LPVOID lPvoid )
{
	CMy300UF_TestDlg *pCtrl=(CMy300UF_TestDlg*)lPvoid;
	while(pCtrl->m_Auto)
	{
		pCtrl->ReadCard();
		Sleep(600);
	}
	return 0;
}
void CMy300UF_TestDlg::OnBnClickedButton2()
{
	
}

void CMy300UF_TestDlg::OnNMThemeChangedCombo1(NMHDR *pNMHDR, LRESULT *pResult)
{
	// This feature requires Windows XP or greater.
	// The symbol _WIN32_WINNT must be >= 0x0501.
	// TODO: Add your control notification handler code here
	*pResult = 0;
}

void CMy300UF_TestDlg::OnCbnSelchangeCombo1()
{
	// TODO: Add your control notification handler code here
	iPort = C_ComBo.GetCurSel();
	if(iPort == 0)
	{
		m_ComBot.EnableWindow(false);
	}
	else
	{
		m_ComBot.EnableWindow(true);
	}
}

void CMy300UF_TestDlg::OnCbnSelchangeCombo2()
{
	// TODO: Add your control notification handler code here
	int Baudrate =  m_ComBot.GetCurSel();
	if(Baudrate == 0)
	{
		m_Baudrate = 9600;
	}
	else
	{
		m_Baudrate = 115200;
	}
}

void CMy300UF_TestDlg::OnBnClickedButton1()
{
	// TODO: Add your control notification handler code here

	

}

void CMy300UF_TestDlg::OnBnClickedButtonConn()
{
	// TODO: Add your control notification handler code here
	int Eor = -1;
	if(m_bOpen)
	{
		if(iPort == 0)
		{
			for(int i =1001;i<=1016;i++)
			{
				if(CVR_InitComm(i) == 1)
				{
					Eor = 1;
					iPort = i;
					break;
				}
			}
		}
		else
		{
			Eor = CVR_InitComm(iPort);			
		}
		if(Eor == 1)
		{
			m_bOpen = false;
			m_ButtonReadCard.EnableWindow(true);
			CardM1.m_ButInit.EnableWindow(true);
			
		}
		else
		{
			AfxMessageBox(_T("�򿪴���ʧ��"));
		}
	
	}
	else
	{
		Eor = CVR_CloseComm();
		if(Eor == 1)
		{
			m_bOpen = true;
			m_ButtonReadCard.EnableWindow(false);
			CardM1.UnInit();
		}
		else
		{
			AfxMessageBox(_T("�رմ���ʧ��"));
		}
	}
	SetDlgItemText(IDC_BUTTON_Conn, m_bOpen  ? _T("����") :_T( "�ر�"));
}

void CMy300UF_TestDlg::OnBnClickedButtonReadcard()
{
	// TODO: Add your control notification handler code here
	ReadCard();
	/*if(m_Auto)
	{
		m_Auto = false;
	}
	else
	{
		m_Auto = true;
		//���߳�
		HANDLE h_Run;
		UINT m_nUdpThreadID;
		h_Run=(HANDLE) _beginthreadex(NULL,NULL,AutoRun,this,NULL,&m_nUdpThreadID);
		CloseHandle((HANDLE)h_Run);
	}
	SetDlgItemText(IDC_BUTTON_ReadCard, m_Auto  ? _T("ֹͣ����") :_T( "�Զ�����"));*/

}

void CMy300UF_TestDlg::OnClose()
{
	// TODO: Add your message handler code here and/or call default

	CDialog::OnClose();
	::FreeLibrary(dllHandle);
}
