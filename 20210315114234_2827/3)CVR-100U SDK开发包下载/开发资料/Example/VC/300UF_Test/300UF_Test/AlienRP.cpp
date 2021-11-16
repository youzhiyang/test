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

	if( m_bmp.m_hObject != NULL )//判断
		m_bmp.DeleteObject();

	HBITMAP hbmp = (HBITMAP)::LoadImage(AfxGetInstanceHandle(), 
		szXPPath, IMAGE_BITMAP, 0, 0, LR_CREATEDIBSECTION|LR_LOADFROMFILE);

	//该断程序用来取得加载的BMP的信息
	DIBSECTION ds;
	BOOL bAttach = m_bmp.Attach(hbmp);

	BITMAPINFOHEADER &bminfo = ds.dsBmih; 
	m_bmp.GetObject( sizeof(ds), &ds );
	int cx=bminfo.biWidth;  //得到图像宽度
	int cy=bminfo.biHeight; //得到图像高度

	//得到了图像的宽度和高度后,我们就可以对图像大小进行适应,即调整控件的大小,让它正好显示一张图片
	CRect rect;
	GetDlgItem(IDC_STATIC_AR)->GetWindowRect(&rect);
	ScreenToClient(&rect);
	GetDlgItem(IDC_STATIC_AR)->MoveWindow(rect.left,rect.top,cx,cy,true);//调整大小	

	//（客户区设备上下文）用于客户区的输出，
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
