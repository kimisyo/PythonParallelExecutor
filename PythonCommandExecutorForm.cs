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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static PythonParallelExecutor.MainForm;

namespace PythonParallelExecutor
{
    public partial class PythonCommandExecutorForm : Form
    {
        public MainForm parentForm { get; set; }

        public int currentProcessId { get; set; }

        public Process currentProcess { get; set; }

        public MyEventHandler handler;

        public PythonCommandExecutorForm()
        {
            InitializeComponent();
            currentProcessId = -1;
        }

        /// 実行ボタンクリック時の動作
        private void Execute_Click(object sender, EventArgs e)
        {
            // 前処理
            ExecuteButton.Enabled = false;
            CancelButton.Enabled = true;
            this.Invoke((MethodInvoker)(() => this.StandardOutputText.Clear()));

            // プロセスデータ作成
            ProcessData pd = new ProcessData();
            pd.Arguments = this.ArgmentsTextBox.Text.Trim();
            pd.setFileName(this.FileNameTextBox.Text.Trim());
            pd.setWorkingDirectory(this.WorkingDirectoryTextBox.Text.Trim());
            pd.setInputString(this.StandardInputTextBox.Text.Trim());

            // 実行
            try
            {
                Process process = RunCommandLineAsync(pd.getFileName(), pd.getWorkingDirectory(), pd.Arguments, pd.getInputString());

                // プロセス一覧のデータを更新
                if (process != null)
                {
                    // 前の分はけしてもよい
                    if (currentProcessId != -1) {
                        PythonCommandExecutorForm fcef_prev = this.parentForm.processidToProcessDataMap[currentProcessId].getDetailForm();
                        if (fcef_prev != null)
                        {
                            this.parentForm.processidToProcessDataMap[currentProcessId].setDetailForm(null);
                        }
                    } 
                    pd.processId = process.Id;
                    pd.startTime = DateTime.Now;
                    pd.status = "running";
                    pd.setProcess(process);

                    // プロセス一覧に追加
                    parentForm.bindingDataList.Add(pd);
                    parentForm.processidToProcessDataMap.Add(pd.processId, pd);
                    parentForm.processidToProcessDataMap[pd.processId].setDetailForm(this);

                    currentProcess = process;
                    currentProcessId = process.Id;
                    this.Text = pd.processId + " - " + this.Text;
                }
            }
            catch (Exception ex)
            {
                // 失敗時
                MessageBox.Show("プロセスの実行に失敗しました:" + ex.Message);
                ExecuteButton.Enabled = true;
                CancelButton.Enabled = false;
                return;
            }

        }

        /// コマンド実行処理本体
        public Process RunCommandLineAsync(String fileName, String workingDirectory, String argment, String inputString)
        {

            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = fileName;
            psInfo.WorkingDirectory = workingDirectory;
            psInfo.Arguments = argment;

            psInfo.CreateNoWindow = true;
            psInfo.UseShellExecute = false;
            psInfo.RedirectStandardInput = true;
            psInfo.RedirectStandardOutput = true;
            psInfo.RedirectStandardError = true;

            Process p = Process.Start(psInfo);
            p.EnableRaisingEvents = true;
            p.Exited += parentForm.onExited;
            p.OutputDataReceived += parentForm.p_OutputDataReceived;
            p.ErrorDataReceived += parentForm.p_ErrorDataReceived;

            // プロセスの実行
            p.Start();

            // 標準入力への書き込み
            using (StreamWriter sw = p.StandardInput)
            {
                sw.Write(inputString);
            }

            //非同期で出力とエラーの読み取りを開始
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            return p;
        }

        // Clearボタンクリック時の動作
        private void Clear_Click(object sender, EventArgs e)
        {
            // 標準入力エリアのクリア
            this.StandardInputTextBox.Clear();
            // 標準出力エリアのクリア
            this.StandardOutputText.Clear();
        }

        // Closeボタン
        private void Close_Click(object sender, EventArgs e)
        {
            // this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // コールバックイベントハンドラ（データを受け取ってテキストに流す)
        public void CallBackEvent(MyEventArgs e)
        {
            ProcessData pd = e.Source;
            // 自分に関連するデータのみ表示する

            if (this.currentProcess != null)
            {
                try
                {
                    if (this.currentProcess.Id == pd.processId)
                    {
                        this.Invoke((MethodInvoker)(() => StandardOutputText.AppendText(e.Data)));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // フォームが表示されたとき
        private void PythonCommandExecutorForm_Shown(object sender, EventArgs e)
        {
            this.handler = new MyEventHandler(this.CallBackEvent);
            this.parentForm.MyProgressEvent += handler;
            this.StandardOutputText.ScrollToCaret();
        }

        // キャンセルボタンがクリックされた時
        private void Cancel_Click(object sender, EventArgs e)
        {
            if (currentProcess != null)
            {
                try
                {
                    currentProcess.Kill();
                    this.parentForm.processidToProcessDataMap[currentProcess.Id].status = "canceling";
                    this.Invoke((MethodInvoker)(() => this.CancelButton.Enabled = false));
                    this.Invoke((MethodInvoker)(() => this.ExecuteButton.Enabled = true));
                    this.Invoke((MethodInvoker)(() => this.parentForm.bindingDataList.ResetBindings()));
                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2);
                }
            }
        }

        // フォームが閉じた時の処理
        private void PythonCommandExecutorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.parentForm.MyProgressEvent -= this.handler;
            try
            {
                ProcessData pd = this.parentForm.processidToProcessDataMap[this.currentProcessId];
                pd.setDetailForm(null);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PythonCommandExecutorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
