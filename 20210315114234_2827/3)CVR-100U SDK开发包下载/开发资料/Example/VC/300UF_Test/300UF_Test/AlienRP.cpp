// AlienRP.cpp : implementation file
//

#include "stdafx.h"
#include "300UF_Test.h"
#include "AlienRP.h"
#include "300UF_TestDlg.h"

extern CMy300UF_TestDlg *CMy300;

// AlienRP dialog

IMPLEMENT_DYNCREATE(AlienRP, CDHtmlDialog)

AlienRP::AlienRP(CWnd* pParent /*=NULL*/)
	: CDHtmlDialog(AlienRP::IDD, AlienRP::IDH, pParent)
{
	m_ShowP = false;

}

AlienRP::~AlienRP()
{
}

void AlienRP::DoDataExchange(CDataExchange* pDX)
{
	CDHtmlDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_AR_EName,m_name );
	DDX_Text(pDX, IDC_EDIT_ARCName,m_stChineseName);
	DDX_Text(pDX, IDC_EDIT_ARStCertVer,m_stCertVer);
	DDX_Text(pDX, IDC_EDIT_ATSex,m_sex);
	DDX_Text(pDX, IDC_EDIT_ARNation,m_nation);
	DDX_Text(pDX, IDC_EDIT_ARbirth,m_birth );
	DDX_Text(pDX, IDC_EDIT_ARIDCode,m_idcode);
	DDX_Text(pDX, IDC_EDIT_ARDepart,m_depart);
	DDX_Text(pDX, IDC_EDIT_ARDepCode,m_valid );
	//DDX_Text(pDX, IDC_EDIT_ARNation_Code,m_nation_code);
}

BOOL AlienRP::OnInitDialog()
{
	CDHtmlDialog::OnInitDialog();
	return TRUE;  // return TRUE  unless you set the focus to a control
}

BEGIN_MESSAGE_MAP(AlienRP, CDHtmlDialog)
	ON_WM_PAINT()
	ON_BN_CLICKED(IDC_BUTTON1, &AlienRP::OnBnClickedButton1)
END_MESSAGE_MAP()

BEGIN_DHTML_EVENT_MAP(AlienRP)
	DHTML_EVENT_ONCLICK(_T("ButtonOK"), OnButtonOK)
	DHTML_EVENT_ONCLICK(_T("ButtonCancel"), OnButtonCancel)
END_DHTML_EVENT_MAP()



// AlienRP message handlers

HRESULT AlienRP::OnButtonOK(IHTMLElement* /*pElement*/)
{
	OnOK();
	return S_OK;
}

HRESULT AlienRP::OnButtonCancel(IHTMLElement* /*pElement*/)
{
	OnCancel();
	return S_OK;
}

void AlienRP::ShowPhoto()
{
	CEdit *pEdit = (CEdit *)GetDlgItem(IDC_EDIT_AR_EName);
	pEdit->SetWindowText(m_name);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARCName);
	pEdit->SetWindowText(m_stChineseName);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARStCertVer);
	pEdit->SetWindowText(m_stCertVer);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ATSex);
	pEdit->SetWindowText(m_sex);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARNation);
	pEdit->SetWindowText(m_nation);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARbirth);
	pEdit->SetWindowText(m_birth);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARIDCode);
	pEdit->SetWindowText(m_idcode);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARDepart);
	pEdit->SetWindowText(m_depart);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARDepCode);
	pEdit->SetWindowText(m_depCode);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARValid);
	pEdit->SetWindowText(m_valid);
//	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARNation_Code);
	//pEdit->SetWindowText(m_nation_code);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARSamID);
	pEdit->SetWindowText(m_SamID);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_ARReadTime);
	pEdit->SetWindowText(m_ReadTime);

	if( m_bmp.m_hObject != NULL )//�ж�
		m_bmp.DeleteObject();

	HBITMAP hbmp = (HBITMAP)::LoadImage(AfxGetInstanceHandle(), 
		szXPPath, IMAGE_BITMAP, 0, 0, LR_CREATEDIBSECTION|LR_LOADFROMFILE);

	//�öϳ�������ȡ�ü��ص�BMP����Ϣ
	DIBSECTION ds;
	BOOL bAttach = m_bmp.Attach(hbmp);

	BITMAPINFOHEADER &bminfo = ds.dsBmih; 
	m_bmp.GetObject( sizeof(ds), &ds );
	int cx=bminfo.biWidth;  //�õ�ͼ����
	int cy=bminfo.biHeight; //�õ�ͼ��߶�

	//�õ���ͼ��Ŀ�Ⱥ͸߶Ⱥ�,���ǾͿ��Զ�ͼ���С������Ӧ,�������ؼ��Ĵ�С,����������ʾһ��ͼƬ
	CRect rect;
	GetDlgItem(IDC_STATIC_AR)->GetWindowRect(&rect);
	ScreenToClient(&rect);
	GetDlgItem(IDC_STATIC_AR)->MoveWindow(rect.left,rect.top,cx,cy,true);//������С	

	//���ͻ����豸�����ģ����ڿͻ����������
	CClientDC dc(GetDlgItem(IDC_STATIC_AR));
	CRect rcclient;
	GetDlgItem(IDC_STATIC_AR)->GetClientRect(&rcclient);   
	CDC dcMemory;
	dcMemory.CreateCompatibleDC(&dc);
	// Select the bitmap into the in-memory DC
	CBitmap* pOldBitmap = dcMemory.SelectObject(&m_bmp);
	BOOL eor = dc.BitBlt(rcclient.left, rcclient.top, rcclient.Width(), rcclient.Height(), 
		&dcMemory, rcclient.left, rcclient.top,SRCCOPY);    
	dcMemory.SelectObject(pOldBitmap);
	dcMemory.DeleteDC();
}
void AlienRP::OnPaint()
{
	CPaintDC dc(this); // device context for painting
	// TODO: Add your message handler code here
	// Do not call CDHtmlDialog::OnPaint() for painting messages
	if(m_ShowP)
	{
		ShowPhoto();
	}
}

void AlienRP::OnBnClickedButton1()
{
	// TODO: Add your control notification handler code here
	CMy300->ReadCard();
	ShowPhoto();
}
