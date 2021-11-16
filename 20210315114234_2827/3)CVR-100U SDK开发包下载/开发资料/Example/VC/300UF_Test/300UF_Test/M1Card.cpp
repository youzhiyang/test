// M1Card.cpp : implementation file
//

#include "stdafx.h"
#include "M1Card.h"
#include "300UF_TestDlg.h"

extern CMy300UF_TestDlg *CMy300;



// M1Card dialog

IMPLEMENT_DYNCREATE(M1Card, CDHtmlDialog)

M1Card::M1Card(CWnd* pParent /*=NULL*/)
	: CDHtmlDialog(M1Card::IDD, M1Card::IDH, pParent)
{
	Block = 4;//块号
	CardID = _T("");//卡号
	CSTagType = _T("");
	WriteBuffer = _T("01 02 03 04 05 06");
	CSKey = _T("FF FF FF FF FF FF");
	iIfOpen = 0;
	M1_MF_GetFisCardID = NULL;
	M1_MF_HL_Request = NULL;
	M1_MF_HL_Auth = NULL;
	M1_MF_HL_Read = NULL;
	M1_MF_HL_Write = NULL;
	Com_bOpen = true;
}

M1Card::~M1Card()
{
}

void M1Card::DoDataExchange(CDataExchange* pDX)
{
	CDHtmlDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMBO1, ModeComBox);
	DDX_Text(pDX, IDC_EDIT6,  WriteBuffer);
	DDX_Text(pDX, IDC_EDIT1,  Block);
	DDX_Text(pDX, IDC_EDIT2,  CardID);
	DDX_Text(pDX, IDC_EDIT3,  CSTagType);
	DDX_Text(pDX, IDC_EDIT4,  CSKey);
	DDX_Control(pDX, IDC_BUTTON2, m_ButInit);
	DDX_Control(pDX, IDC_BUTTON3, m_ButClear);
	DDX_Control(pDX, IDC_BUTTON1, m_ButFindCard);
	DDX_Control(pDX, IDC_BUTTON6, m_ButRZ);
	DDX_Control(pDX, IDC_BUTTON7, m_ButReadM1);
	DDX_Control(pDX, IDC_BUTTON8, m_ButWriteM1);
	DDX_Control(pDX, IDC_BUTTON9, m_ButGETFisCard);
}

BOOL M1Card::OnInitDialog()
{
	CDHtmlDialog::OnInitDialog();
	return TRUE;  // return TRUE  unless you set the focus to a control
}

BEGIN_MESSAGE_MAP(M1Card, CDHtmlDialog)
	ON_BN_CLICKED(IDC_BUTTON2, &M1Card::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &M1Card::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON1, &M1Card::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON6, &M1Card::OnBnClickedButton6)
	ON_BN_CLICKED(IDC_BUTTON7, &M1Card::OnBnClickedButton7)
	ON_BN_CLICKED(IDC_BUTTON8, &M1Card::OnBnClickedButton8)
	ON_BN_CLICKED(IDC_BUTTON9, &M1Card::OnBnClickedButton9)
END_MESSAGE_MAP()

BEGIN_DHTML_EVENT_MAP(M1Card)
	DHTML_EVENT_ONCLICK(_T("ButtonOK"), OnButtonOK)
	DHTML_EVENT_ONCLICK(_T("ButtonCancel"), OnButtonCancel)
END_DHTML_EVENT_MAP()



// M1Card message handlers

HRESULT M1Card::OnButtonOK(IHTMLElement* /*pElement*/)
{
	OnOK();
	return S_OK;
}

HRESULT M1Card::OnButtonCancel(IHTMLElement* /*pElement*/)
{
	OnCancel();
	return S_OK;
}

void M1Card::OnBnClickedButton2()
{
	// TODO: Add your control notification handler code here
	int Eor = 0;
	CString OutPut;
	M1_MF_HL_Request        = (lpM1_MF_HL_Request)GetProcAddress(CMy300->dllHandle,"M1_MF_HL_Request");
	M1_MF_HL_Auth			= (lpM1_MF_HL_Auth)GetProcAddress(CMy300->dllHandle,"M1_MF_HL_Auth");
	M1_MF_HL_Read			= (lpM1_MF_HL_Read)GetProcAddress(CMy300->dllHandle,"M1_MF_HL_Read");
	M1_MF_HL_Write			= (lpM1_MF_HL_Write)GetProcAddress(CMy300->dllHandle,"M1_MF_HL_Write");
	M1_MF_GetFisCardID      = (lpM1_MF_GetFisCardID)GetProcAddress(CMy300->dllHandle,"M1_MF_GetFisCardID");
	ModeComBox.InsertString(0,_T("IDLE模式"));
	ModeComBox.InsertString(1,_T("ALL模式"));
	ModeComBox.SetCurSel(0);

	m_ButClear.EnableWindow(true);
	m_ButFindCard.EnableWindow(true);
	m_ButRZ.EnableWindow(true);
	m_ButReadM1.EnableWindow(true);
	m_ButWriteM1.EnableWindow(true);
	m_ButGETFisCard.EnableWindow(true);
	OutPut = _T("初始化成功");
	AddString(OutPut);

}


void M1Card::AddString(CString PutData)
{
	CEdit *pEdit = (CEdit *)GetDlgItem(IDC_EDIT7);
	CString output,tmp;
	pEdit->GetWindowText(output);
	output = output + L"\r\n" + PutData;
    pEdit->SetWindowText(output); 
	//滚动条始终在底部
	pEdit->SetSel(output.GetLength(),output.GetLength()); 			
	pEdit->ScrollWindow(0,0); 
}

int M1Card::CStrGetData(CString IntoData,byte *pData,int command_long)
{
	CString StrIn;
	StrIn = IntoData;
	StrIn.Replace(_T(" "),_T(""));
	int nStart = StrIn.GetLength();
	if(nStart < command_long*2)
	{
		for(int i=0;i< command_long*2 - nStart;i++)
		{
			StrIn = StrIn + _T("0");
		}
	}
	CString tmp_v;
	CStringA tmp_A;
	for(int i=0;i<command_long;i++)
	{
		tmp_A = CW2A(StrIn.Mid(2*i,2));
		pData[i] = strtol(tmp_A.GetBuffer(0) ,NULL,16);
	}
	return 1;
}
void M1Card::OnBnClickedButton3()
{
	// TODO: Add your control notification handler code here
	CEdit *pEdit = (CEdit *)GetDlgItem(IDC_EDIT7);
    pEdit->SetWindowText(_T("")); 
}

void M1Card::OnBnClickedButton1()
{
	// TODO: Add your control notification handler code here
	CString OutPut,tmp;
	Mode = ModeComBox.GetCurSel();
	SNR = 1;
	TagType = 1;
	int Eor = M1_MF_HL_Request(Mode,&SNR,&TagType);
	if(Eor == 0x90)
	{
		OutPut = _T("寻卡成功，卡号数据:");
		CardID.Format(_T("%X"),SNR);
		OutPut = OutPut + CardID;
		tmp.Format(L"%02X",TagType);
		CSTagType = tmp;
		UpdateData(false);
	}
	else
	{
		OutPut.Format(_T("寻卡失败，返回值:%d"),Eor);
	}
	AddString(OutPut);
}

void M1Card::OnBnClickedButton6()
{
	// TODO: Add your control notification handler code here
	UpdateData(true);
	CString OutPut,tmp;
	CStrGetData(CSKey,Key,6);
	//byte KeyA[6] = {0xff,0xff,0xff,0xff,0xff,0xff};
	Mode = ModeComBox.GetCurSel();
	int Eor =  M1_MF_HL_Auth(Mode,SNR,Block,Key);
	if(Eor == 0x90)
	{
		OutPut = _T("卡认证成功");
	}
	else
	{
		OutPut.Format(_T("卡认证失败，返回值:%d"),Eor);
	}
	AddString(OutPut);
}

void M1Card::OnBnClickedButton7()
{
	// TODO: Add your control notification handler code here
	UpdateData(true);
	CString tmp,OutPut;
	CStrGetData(CSKey,Key,6);
	//byte Block = 5;
	byte buffer[16];
	Mode = ModeComBox.GetCurSel();
	if(!SNR)
	{
		SNR = 0;
	}
	int Eor = M1_MF_HL_Read ( Mode,SNR,Block,Key,buffer,16);
	if(Eor == 0x90)
	{
		OutPut = _T("读卡成功：");
		for(int i=0;i< 16;i++)
		{
			tmp.Format(L"%02X ",buffer[i]);
			OutPut = OutPut + tmp;
		}
	}
	else
	{
		OutPut.Format(_T("读卡失败，返回值:%d"),Eor);
	}
	AddString(OutPut);
}

void M1Card::OnBnClickedButton8()
{
	// TODO: Add your control notification handler code here
	UpdateData(true);
	CString OutPut;
	CStrGetData(CSKey,Key,6);
	//byte Block = 6;
	byte buffer[16] = {0};
	Mode = ModeComBox.GetCurSel();
	if(!SNR)
	{
		SNR = 0;
	}

	CStrGetData(WriteBuffer,buffer,16);
	int Eor = M1_MF_HL_Write( Mode,SNR,Block,Key,buffer,16);
	if(Eor == 0x90)
	{
		OutPut = _T("写卡成功");
	}
	else
	{
		OutPut.Format(_T("写卡失败，返回值:%d"),Eor);
	}
	AddString(OutPut);
}

void M1Card::UnInit()
{
	m_ButInit.EnableWindow(false);
	m_ButClear.EnableWindow(false);
	m_ButFindCard.EnableWindow(false);
	m_ButRZ.EnableWindow(false);
	m_ButReadM1.EnableWindow(false);
	m_ButWriteM1.EnableWindow(false);
	m_ButGETFisCard.EnableWindow(false);
}
void M1Card::OnBnClickedButton9()
{
	// TODO: Add your control notification handler code here
	CString OutPut = _T("");
	BYTE FisCardID[64] = {0};
	BYTE LifeEndData[10] = {0};
	int FisCardIDLen = 64;
	int Eor = M1_MF_GetFisCardID(FisCardID,LifeEndData,&FisCardIDLen);
	if(Eor == 0x90)
	{
		OutPut = _T("银行卡卡号：");
		CString tmp;
		tmp = FisCardID;
		OutPut = OutPut + tmp;
		AddString(OutPut);
		tmp = LifeEndData;
		OutPut = _T("有效期：") + tmp;
		AddString(OutPut);
	}
	else
	{
		OutPut.Format(_T("获取银行信息失败，错误码%02X"),Eor);
		AddString(OutPut);
	}
}
