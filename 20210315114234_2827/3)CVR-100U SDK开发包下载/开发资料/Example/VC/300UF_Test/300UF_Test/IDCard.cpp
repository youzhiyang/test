// IDCard.cpp : implementation file
//

#include "stdafx.h"
#include "300UF_Test.h"
#include "IDCard.h"

#include "300UF_TestDlg.h"

extern CMy300UF_TestDlg *CMy300;


// IDCard dialog

IMPLEMENT_DYNCREATE(IDCard, CDHtmlDialog)

IDCard::IDCard(CWnd* pParent /*=NULL*/)
	: CDHtmlDialog(IDCard::IDD, IDCard::IDH, pParent)
{
	m_ShowP = false;
}

IDCard::~IDCard()
{
}

void IDCard::DoDataExchange(CDataExchange* pDX)
{
	CDHtmlDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_IDName,m_civicName);
	DDX_Text(pDX, IDC_EDIT_IDSex,m_civicSex);
	DDX_Text(pDX, IDC_EDIT_IDNation,m_civicNation);
	DDX_Text(pDX, IDC_EDIT_IDBirthday,m_civicBirthday);
	DDX_Text(pDX, IDC_EDIT_IDDate,m_civicValidDate);
	DDX_Text(pDX, IDC_EDIT_IDCard,m_civicCardID);
	DDX_Text(pDX, IDC_EDIT_IDAddress,m_civicAddress);
	DDX_Text(pDX, IDC_EDIT_IDDepartment,m_civicDepartment);
	DDX_Text(pDX, IDC_EDIT_IDSAMID,m_SamID);
	DDX_Text(pDX, IDC_EDIT_IDReadTime,m_ReadTime);
	

}

BOOL IDCard::OnInitDialog()
{
	CDHtmlDialog::OnInitDialog();


	return TRUE;  // return TRUE  unless you set the focus to a control
}

BEGIN_MESSAGE_MAP(IDCard, CDHtmlDialog)
	//ON_BN_CLICKED(IDC_BUTTON1, &IDCard::OnBnClickedButton1)
	//ON_BN_CLICKED(IDC_BUTTON_Read_IDCard, &IDCard::OnBnClickedButtonReadIdcard)
	ON_WM_PAINT()
	ON_BN_CLICKED(IDC_BUTTONRead, &IDCard::OnBnClickedButtonread)
END_MESSAGE_MAP()

BEGIN_DHTML_EVENT_MAP(IDCard)
	DHTML_EVENT_ONCLICK(_T("ButtonOK"), OnButtonOK)
	DHTML_EVENT_ONCLICK(_T("ButtonCancel"), OnButtonCancel)
END_DHTML_EVENT_MAP()



// IDCard message handlers

HRESULT IDCard::OnButtonOK(IHTMLElement* /*pElement*/)
{
	OnOK();
	return S_OK;
}

HRESULT IDCard::OnButtonCancel(IHTMLElement* /*pElement*/)
{
	OnCancel();
	return S_OK;
}

void IDCard::OnBnClickedButton1()
{
	// TODO: Add your control notification handler code here

}

void IDCard::OnBnClickedButtonReadIdcard()
{
	// TODO: Add your control notification handler code here

}


void IDCard::ShowPhoto()
{
	CEdit *pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDName);
	pEdit->SetWindowText(m_civicName);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDSex);
	pEdit->SetWindowText(m_civicSex);

	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDBirthday);
	pEdit->SetWindowText(m_civicBirthday);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDDate);
	pEdit->SetWindowText(m_civicValidDate);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDCard);
	pEdit->SetWindowText(m_civicCardID);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDAddress);
	pEdit->SetWindowText(m_civicAddress);
	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDDepartment);
	pEdit->SetWindowText(m_civicDepartment);

	pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDReadTime);
	pEdit->SetWindowText(m_ReadTime);


	CStatic * pStaticNation = (CStatic*)GetDlgItem(IDC_STATIC_Nation);
	CStatic * pStaticUID = (CStatic*)GetDlgItem(IDC_STATIC_UID);
	if(m_Type == L"J")
	{
		pStaticNation->SetWindowText(L"ǩ������:");
		pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDNation);
		pEdit->SetWindowText(m_IssuesNum);
		pStaticUID->SetWindowText(L"ͨ��֤����:");
		pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDSAMID);
		pEdit->SetWindowText(m_PassCheckID);
		pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDCAIDTYPE);
		pEdit->SetWindowText(L"�۰�̨�������֤");

	}
	else
	{
		pStaticNation->SetWindowText(L"��    ��:");
		pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDNation);
		pEdit->SetWindowText(m_civicNation);
		pStaticUID->SetWindowText(L"��  ��  ַ:");
		pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDSAMID);
		pEdit->SetWindowText(m_SamID);
		pEdit = (CEdit *)GetDlgItem(IDC_EDIT_IDCAIDTYPE);
		pEdit->SetWindowText(L"���֤");
	}


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
	GetDlgItem(IDC_STATIC_IDPHOTO)->GetWindowRect(&rect);
	ScreenToClient(&rect);
	GetDlgItem(IDC_STATIC_IDPHOTO)->MoveWindow(rect.left,rect.top,cx,cy,true);//������С	

	//���ͻ����豸�����ģ����ڿͻ����������
	CClientDC dc(GetDlgItem(IDC_STATIC_IDPHOTO));
	CRect rcclient;
	GetDlgItem(IDC_STATIC_IDPHOTO)->GetClientRect(&rcclient);   
	CDC dcMemory;
	dcMemory.CreateCompatibleDC(&dc);
	// Select the bitmap into the in-memory DC
	CBitmap* pOldBitmap = dcMemory.SelectObject(&m_bmp);
	dc.BitBlt(rcclient.left, rcclient.top, rcclient.Width(), rcclient.Height(), 
		&dcMemory, rcclient.left, rcclient.top,SRCCOPY);    
	dcMemory.SelectObject(pOldBitmap);
	dcMemory.DeleteDC();
}

void IDCard::OnPaint()
{
	CPaintDC dc(this); // device context for painting
	// TODO: Add your message handler code here
	// Do not call CDHtmlDialog::OnPaint() for painting messages
	if(m_ShowP)
	{
		ShowPhoto();
	}
}


void IDCard::OnBnClickedButtonread()
{
	// TODO: Add your control notification handler code here
	CMy300->ReadCard();
	ShowPhoto();
}
