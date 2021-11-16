using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CVR100A_U_DSDK_Demo;

namespace CVR100A_U_DSDK_Demo
{
    public partial class MainWnd : Form
    {
        public MainWnd()
        {
            InitializeComponent();
            My_Conbobox();
        }

        int m_Port = 1;
        int m_Baudrate = 9600;
        bool m_ComOpen = false;
        private void buttonReadCard_Click(object sender, EventArgs e)
        {
            try
            {
                int authenticate = CVRSDK.CVR_Authenticate();
                if (authenticate == 1)
                {
                    int readContent = CVRSDK.CVR_Read_Content(4);
                    if (readContent == 1)
                    {
                        this.labelOpResult.Text = "读卡操作成功！";
                        FillData();
                    }
                    else
                    {
                        this.labelOpResult.Text = "读卡操作失败！";
                    }
                }
                else
                {
                    MessageBox.Show("未放卡或卡片放置不正确");
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void FillData()
        {
            try
            {
                int length;

                // 照片保存在当前目录
                String szXPPath = "zp.bmp";
                System.Drawing.Image img = System.Drawing.Image.FromFile(szXPPath);
                System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
                img.Dispose();
                pictureBoxPhoto.Image = bmp;

                byte[] name = new byte[128];
                length = 128;
                CVRSDK.GetPeopleName(ref name[0], ref length);

                byte[] cnName = new byte[128];
                length = 128;
                CVRSDK.GetPeopleChineseName(ref cnName[0], ref length);

                byte[] number = new byte[128];
                length = 128;
                CVRSDK.GetPeopleIDCode(ref number[0], ref length);

                byte[] peopleNation = new byte[128];
                length = 128;
                CVRSDK.GetPeopleNation(ref peopleNation[0], ref length);

                byte[] peopleNationCode = new byte[128];
                length = 128;
                CVRSDK.GetNationCode(ref peopleNationCode[0], ref length);

                byte[] validtermOfStart = new byte[128];
                length = 128;
                CVRSDK.GetStartDate(ref validtermOfStart[0], ref length);

                byte[] birthday = new byte[128];
                length = 128;
                CVRSDK.GetPeopleBirthday(ref birthday[0], ref length);

                byte[] address = new byte[128];
                length = 128;
                CVRSDK.GetPeopleAddress(ref address[0], ref length);

                byte[] validtermOfEnd = new byte[128];
                length = 128;
                CVRSDK.GetEndDate(ref validtermOfEnd[0], ref length);

                byte[] signdate = new byte[128];
                length = 128;
                CVRSDK.GetDepartment(ref signdate[0], ref length);

                byte[] sex = new byte[128];
                length = 128;
                CVRSDK.GetPeopleSex(ref sex[0], ref length);

                byte[] Uid = new byte[128];
                length = 128;

                //CVRSDK.GetIDCardUID(ref Uid[0], 128);

                bool bCivic = true;
                byte[] certType = new byte[32];
                length = 32;
                CVRSDK.GetCertType(ref certType[0], ref length);

                string strType = System.Text.Encoding.ASCII.GetString(certType);
                int nStart = strType.IndexOf("I");
                if (nStart != -1) bCivic = false;

                if (bCivic)
                {
                    labelCnName.Visible = false;
                    labelAddress.Visible = true;
                    labelName.Text = "姓名：" + System.Text.Encoding.GetEncoding("GB2312").GetString(name);
                    labelSex.Text = "性别：" + System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
                    labelNation.Text = "民族：" + System.Text.Encoding.GetEncoding("GB2312").GetString(peopleNation).Replace("\0", "").Trim();
                    labelNationCode.Text = "";
                   // labelNationCode.Text = "民族代码：" + System.Text.Encoding.GetEncoding("GB2312").GetString(peopleNationCode).Replace("\0", "").Trim();
                    labelBirthday.Text = "出生日期：" + System.Text.Encoding.GetEncoding("GB2312").GetString(birthday).Replace("\0", "").Trim();
                    labelIdCardNo.Text = "身份证号：" + System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
                    labelAddress.Text = "地址：" + System.Text.Encoding.GetEncoding("GB2312").GetString(address).Replace("\0", "").Trim();
                    labelDepartment.Text = "签发机关：" + System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();
                    labelValidDate.Text = "有效期限：" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim() + "-" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();
                    //labelSamID.Text = "UID：" + System.Text.Encoding.GetEncoding("GB2312").GetString(Uid).Replace("\0", "").Trim();
                }
                else
                {
                    labelCnName.Visible = true;
                    labelAddress.Visible = false;
                    labelName.Text = "姓名：" + System.Text.Encoding.GetEncoding("GB2312").GetString(name).Replace("\0", "").Trim();
                    labelCnName.Text = "中文姓名：" + System.Text.Encoding.GetEncoding("GB2312").GetString(cnName).Replace("\0", "").Trim();
                    labelSex.Text = "性别：" + System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
                    labelNation.Text = "国籍：" + System.Text.Encoding.GetEncoding("GB2312").GetString(peopleNation).Replace("\0", "").Trim();
                    labelNationCode.Text = "国籍代码：" + System.Text.Encoding.GetEncoding("GB2312").GetString(peopleNationCode).Replace("\0", "").Trim();
                    labelBirthday.Text = "出生日期：" + System.Text.Encoding.GetEncoding("GB2312").GetString(birthday).Replace("\0", "").Trim();
                    labelIdCardNo.Text = "证件号码：" + System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
                    labelDepartment.Text = "签发机关：" + System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();
                    labelValidDate.Text = "有效期限：" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim() + "-" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();
                    //labelSamID.Text = "UID：" + System.Text.Encoding.GetEncoding("GB2312").GetString(Uid).Replace("\0", "").Trim();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

      
        /*  添加下拉列表的选项，USB选择列表 */
        public void My_Conbobox()
        {
            string tmp = "COM";
            comboBoxPORT.Items.Insert(0, "USB");//选择项1
            for (int i = 1; i <= 16; i++)
            {
                tmp = "COM" + (i);
                comboBoxPORT.Items.Insert(i,tmp);//选择项1
            }
            comboBoxBaudrate.Items.Insert(0,"9600");
            comboBoxBaudrate.Items.Insert(1,"115200");
            comboBoxPORT.SelectedIndex = 0;
            comboBoxBaudrate.SelectedIndex = 1;
           
        }

        private void comboBoxPORT_SelectedIndexChanged(object sender, EventArgs e)
        {
             m_Port =  comboBoxPORT.SelectedIndex;
        }

        private void comboBoxBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxBaudrate.SelectedItem.ToString()) //获取选择的内容
            {
                case "9600": m_Baudrate = 9600; break;
                case "115200": m_Baudrate = 115200; break;
            }
        }

        private void buttonOPENCOM_Click(object sender, EventArgs e)
        {
            if (m_ComOpen)
            {
                CVRSDK.CVR_CloseComm();
                m_ComOpen = false;
                buttonOPENCOM.Text = "打开";
                this.labelOpResult.Text = "关闭成功！";
                buttonReadCard.Enabled = false;
            }
            else
            {
                CVRSDK.CVR_SetComBaudrate(m_Baudrate);// 设置波特率
                if (0 == m_Port)    //usb
                {
                    for (int i = 1001; i <= 1016; i++)
                    {
                        if (1 == CVRSDK.CVR_InitComm(i))
                        {
                            m_ComOpen = true;
                            m_Port = i;
                            break;
                        }
                    }
                }
                else if (CVRSDK.CVR_InitComm(m_Port) == 1)  //UART
                {
                    m_ComOpen = true;
                }

                if (!m_ComOpen)
                {
                    this.labelOpResult.Text = "连接失败！";
                    CVRSDK.CVR_CloseComm();
                    return;
                }
                else
                {
                    this.labelOpResult.Text = "连接成功！";
                    buttonOPENCOM.Text = "关闭";
                    buttonReadCard.Enabled = true;
                }
            }
        }
    }
}
