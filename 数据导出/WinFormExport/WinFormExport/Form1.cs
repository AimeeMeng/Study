using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using WinFormExport.Tools;

namespace WinFormExport
{
    /// <summary>
    /// dataGridView数据导出到word
    /// 有word模板的情况
    /// AimeeMeng 2018-06-25本篇文章介绍了根据word模板导出word文档的简要方法。
    /// </summary>
    public partial class Form1 : Form
    {
        private string SaveFilePath;
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary> 加载模拟数据
        /// 加载模拟数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try {
                List<griddata> gdList = new List<griddata>();
                for (int i = 0; i < 10; i++)
                {
                    griddata gd = new griddata();
                    gd.id = "100" + i;
                    gd.name = "name" + i;
                    gdList.Add(gd);
                }
                dataGridView1.DataSource = gdList;
               
            }
            catch { MessageBox.Show("出错了。"); }
        }

        /// <summary> 导出数据
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if ((MessageBox.Show("确定要导出选中的" + dataGridView1.SelectedRows.Count + "条报告吗﹖", "批量导出", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                    {
                        //弹出选择文件夹对话框
                        FolderBrowserDialog path = new FolderBrowserDialog();
                        path.Description = "选择需要保存的文件夹";
                        path.ShowDialog();
                        SaveFilePath = path.SelectedPath;
                        backgroundWorker1.RunWorkerAsync();
                        progressBar1.Visible = true;
                    }
                }
            }
            catch { MessageBox.Show("导出报告出错了。"); }
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="SaveFilePath"></param>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        private void ExportWordReport(string SaveFilePath,string ID,string name)
        {
            try
            {
                WordDocFileHelper WordTem = new WordDocFileHelper();
                string path = @"TempleteWord\ReportRepair.dotx";
                string fullName = System.Windows.Forms.Application.StartupPath.Substring(0, System.Windows.Forms.Application.StartupPath.LastIndexOf("\\"));
                fullName = fullName.Substring(0, fullName.LastIndexOf("\\")) + "\\" + path;

                #region 方法一 选择文件保存位置后自动批量保存（声明书签数组）
                //_Document oDoc = WordTem.CreateNewDocumentD(fullName);
                //#region 书签内容
                ////声明书签数组  
                //object[] oBookMark = new object[2];
                ////赋值书签名  
                //oBookMark[0] = "XMMC";
                //oBookMark[1] = "YYSJ";
                ////赋值数据到书签的位置  
                //oDoc.Bookmarks.get_Item(ref oBookMark[0]).Range.Text = ID;
                //oDoc.Bookmarks.get_Item(ref oBookMark[1]).Range.Text = name;
                //#endregion
                //WordTem.SaveDocument(SaveFilePath + "\\" + name + ".doc");
                #endregion

                #region 方法二 选择文件保存位置后自动批量保存
                WordTem.CreateNewDocument1(fullName);
                WordTem.InsertValue("XMMC", ID);
                WordTem.InsertValue("YYSJ", name);
                WordTem.SaveDocument(SaveFilePath + "\\" + name + ".doc");
                #endregion

                #region 方法三 自动打开只读文档，保存则需要手动另存为（只能打开一个，其余报错）
                //WordTem.CreateNewDocument(fullName);
                //WordTem.InsertValue("XMMC", ID);
                //WordTem.InsertValue("YYSJ", name);
                //WordTem.OpenDocument("新建word文档");
                #endregion

                #region 方法四 自动打开只读文档，保存则需要手动另存为(传递文件名称)
                //WordTem.CreateNewDocument0(fullName, name);//注释掉killWinWordProcess方法，就会打开多个文档
                //WordTem.InsertValue("XMMC", ID);
                //WordTem.InsertValue("YYSJ", name);
                //WordTem.OpenDocument(name);
                #endregion

                ////弹出保存文件对话框，保存生成的Word  
                //SaveFileDialog sfd = new SaveFileDialog();
                //sfd.Filter = "Word Document(*.doc)|*.doc";
                //sfd.DefaultExt = "Word Document(*.doc)|*.doc";
                //if (sfd.ShowDialog() == DialogResult.OK)
                //{
                //    WordTem.SaveDocument(sfd.FileName);
                //}  
            }
            catch { MessageBox.Show("导出" + name + "出错了。"); }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try {
                DataGridViewRow[] dataRow = new DataGridViewRow[dataGridView1.SelectedRows.Count];
                dataGridView1.SelectedRows.CopyTo(dataRow, 0);
                Parallel.ForEach(dataRow, row =>
                {
                    string id = row.Cells["id"].Value.ToString();
                    string name = row.Cells["name"].Value.ToString();
                    ExportWordReport(SaveFilePath,id, name);
                });
            }
            catch { }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.ClearSelection();
            progressBar1.Visible = false;
            MessageBox.Show("导出成功。");
        }
    }

    public class griddata
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
