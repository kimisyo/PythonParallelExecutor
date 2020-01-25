/*
The MIT License (MIT)

Copyright (c) 2020 Kimisyo.

Permission is hereby granted, free of charge, to any person obtaining a copy 
of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights 
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
of the Software, and to permit persons to whom the Software is furnished to do 
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR 
IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PythonParallelExecutor
{

    // データを受け渡しするためのイベント引数
    public class MyEventArgs : EventArgs
    {
        private readonly ProcessData _Source;
        private readonly string _Data;

        public MyEventArgs(ProcessData Source, string Data)
        {
            _Source = Source;
            _Data = Data;
        }
        public ProcessData Source { get { return _Source; } }
        public string Data { get { return _Data; } }
    }

    // メインフォーム
    public partial class MainForm : Form
    {
        public BindingList<ProcessData> bindingDataList { get; set; }
        public Dictionary<int, ProcessData> processidToProcessDataMap = new Dictionary<int, ProcessData>();
        public delegate void MyEventHandler(MyEventArgs e);
        public event MyEventHandler MyProgressEvent;

        public MainForm()
        {
            InitializeComponent();
        }

        // New ボタンクリック時
        private void NewButton_Click(object sender, EventArgs e)
        {
            PythonCommandExecutorForm pcef = new PythonCommandExecutorForm();
            pcef.parentForm = this;
            pcef.ShowDialog();
        }

        // プロセス終了時に実行されるイベントハンドラ
        public void onExited(object sender, EventArgs e)
        {
            Process p = (Process)sender;
            Console.WriteLine("process finished! " + p.Id);
            p.WaitForExit();

            int exitCode = p.ExitCode;
            int processId = p.Id;

            p.CancelOutputRead();
            p.CancelErrorRead();
            p.Close();
            p.Dispose();
            p = null;
            processidToProcessDataMap[processId].setProcess(null);
            
            if(processidToProcessDataMap[processId].status == "canceling")
            {
                processidToProcessDataMap[processId].status = "canceled";
            }
            else
            {
                processidToProcessDataMap[processId].status = "finish";
            }
            processidToProcessDataMap[processId].endTime = DateTime.Now;
            processidToProcessDataMap[processId].exitCode = Convert.ToString(exitCode);
            PythonCommandExecutorForm fcef = processidToProcessDataMap[processId].getDetailForm();
            // ボタンの状態を戻す
            if (fcef != null)
            {
                this.Invoke((MethodInvoker)(() => fcef.ExecuteButton.Enabled = true));
                this.Invoke((MethodInvoker)(() => fcef.CancelButton.Enabled = false));
            }

            // データグリッドをリフレッシュ
            this.Invoke((MethodInvoker)(() => bindingDataList.ResetBindings()));
        }

        /// 標準出力データを受け取った時の処理
        public void p_OutputDataReceived(object sender,
            System.Diagnostics.DataReceivedEventArgs e)
        {
            processMessage(sender, e);
        }

        /// 標準エラー出力を受け取った時の処理
        public void p_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            processMessage(sender, e);
        }

        /// プロセスの出力データを受け取り保持する
        void processMessage(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (e != null && e.Data != null && e.Data.Length > 0)
            {
                Process p = (Process)sender;
                if (p != null)
                {
                    // String data = p.Id + ":" + e.Data + "\r\n";
                    String data = e.Data + "\r\n";
                    ProcessData pd = processidToProcessDataMap[p.Id];
                    pd.getOutputStringBuilder().Append(data);

                    // UI更新のイベントを発行する
                    if (MyProgressEvent != null)
                    {
                        MyProgressEvent(new MyEventArgs(pd, data));
                    }
                }
            }
        }

        /// データグリッドのセルをクリック時
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = (DataGridView)sender;

            if (e.RowIndex < 0) return;

            //キャンセルの場合
            if (dgv.Columns[e.ColumnIndex].Name == "CancelButton")
            {
                int processId = (int)dgv[0, e.RowIndex].Value;
                Process p = processidToProcessDataMap[processId].getProcess();

                if (p != null)
                {
                    try
                    {
                        p.Kill();
                        processidToProcessDataMap[processId].status = "canceling";
                        this.Invoke((MethodInvoker)(() => bindingDataList.ResetBindings()));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            // 詳細表示の場合
            if (dgv.Columns[e.ColumnIndex].Name == "DetailButton")
            {
                int processId = (int)dgv[0, e.RowIndex].Value;
                ProcessData pd = processidToProcessDataMap[processId];
                PythonCommandExecutorForm pcef = null;
                pcef = pd.getDetailForm();
                if (pcef == null)
                {
                    // フォームが起動していないためフォームを起動する
                    pcef = new PythonCommandExecutorForm();
                    pd.setDetailForm(pcef);
                    pcef.parentForm = this;
                    pcef.FileNameTextBox.Text = pd.getFileName();
                    pcef.ArgmentsTextBox.Text = pd.Arguments;
                    pcef.WorkingDirectoryTextBox.Text = pd.getWorkingDirectory();
                    pcef.StandardInputTextBox.Text = pd.getInputString();
                    pcef.FileNameTextBox.Enabled = false;
                    pcef.ArgmentsTextBox.Enabled = false;
                    pcef.WorkingDirectoryTextBox.Enabled = false;
                    pcef.StandardOutputText.Text = pd.getOutputStringBuilder().ToString();
                    pcef.StandardOutputText.SelectionStart = pcef.StandardOutputText.Text.Length;
                    pcef.StandardOutputText.Focus();
                    pcef.StandardOutputText.ScrollToCaret();
                    // タイトルにPIDを付与する
                    pcef.Text = processId + " - " + pcef.Text;
                    // プロセスが終了の場合
                    if (pd.getProcess() == null || pd.getProcess().HasExited)
                    {
                        pcef.CancelButton.Enabled = false;
                        pcef.ExecuteButton.Enabled = true;
                        pcef.currentProcessId = pd.processId;
                    }
                    // プロセスが実行中の場合
                    else
                    {
                        pcef.CancelButton.Enabled = true;
                        pcef.ExecuteButton.Enabled = false;
                        pcef.currentProcessId = pd.processId;
                        pcef.currentProcess = pd.getProcess();
                    }
                    pcef.Show(this);
                }
                else
                {
                    pcef.Focus();
                }
            }
        }

        // フォームロード時
        private void MainForm_Load(object sender, EventArgs e)
        {
            bindingDataList = new BindingList<ProcessData>();
            this.dataGridView1.DataSource = bindingDataList;

            DataGridViewButtonColumn dgbc = new DataGridViewButtonColumn();
            dgbc.UseColumnTextForButtonValue = true;
            dgbc.Name = "CancelButton";
            dgbc.Text = "Cancel";
            dgbc.HeaderText = "operation";
            dgbc.Width = 100;
            this.dataGridView1.Columns.Insert(this.dataGridView1.Columns.Count, dgbc);

            dgbc = new DataGridViewButtonColumn();
            dgbc.UseColumnTextForButtonValue = true;
            dgbc.Name = "DetailButton";
            dgbc.Text = "Detail";
            dgbc.HeaderText = "operation";
            dgbc.Width = 100;
            this.dataGridView1.Columns.Insert(this.dataGridView1.Columns.Count, dgbc);
        }
    }

    /// データグリッド表示用のプロセスデータクラス
    public class ProcessData
    {
        // データグリッドで自動で見せる列
        public int processId { get; set; }
        public string Arguments { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public String exitCode { get; set; }
        public String status { get; set; }


        // データグリッドで自動で見せない列
        private Process process;
        private String inputString;
        private StringBuilder outputStringBuilder;
        private string workingDirectory;
        private PythonCommandExecutorForm detailForm;
        
        private string fileName;

        public ProcessData()
        {
            this.outputStringBuilder = new StringBuilder();
        }

        public void setProcess(Process process)
        {
            this.process = process;
        }
        public Process getProcess()
        {
            return this.process;
        }
        public void setOutputStringBuilder(StringBuilder outputStringBuilder)
        {
            this.outputStringBuilder = outputStringBuilder;
        }
        public StringBuilder getOutputStringBuilder()
        {
            return this.outputStringBuilder;
        }
        public void setWorkingDirectory(String workingDirectory)
        {
            this.workingDirectory = workingDirectory;
        }
        public String getWorkingDirectory()
        {
            return this.workingDirectory;
        }
        public void setFileName(String fileName)
        {
            this.fileName = fileName;
        }
        public String getFileName()
        {
            return this.fileName;
        }
        public String getInputString()
        {
            return this.inputString;
        }
        public void setInputString(String inputString)
        {
            this.inputString = inputString;
        }
        public void setDetailForm(PythonCommandExecutorForm detailForm)
        {
            this.detailForm = detailForm;
        }
        public PythonCommandExecutorForm getDetailForm()
        {
            return this.detailForm;
        }

    } 
}
