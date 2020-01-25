namespace PythonParallelExecutor
{
    partial class PythonCommandExecutorForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.StandardOutputText = new System.Windows.Forms.TextBox();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.WorkingDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ArgmentsTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StandardInputTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StandardOutputText
            // 
            this.StandardOutputText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StandardOutputText.Location = new System.Drawing.Point(12, 282);
            this.StandardOutputText.Multiline = true;
            this.StandardOutputText.Name = "StandardOutputText";
            this.StandardOutputText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.StandardOutputText.Size = new System.Drawing.Size(659, 243);
            this.StandardOutputText.TabIndex = 0;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExecuteButton.Location = new System.Drawing.Point(515, 253);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(75, 23);
            this.ExecuteButton.TabIndex = 1;
            this.ExecuteButton.Text = "Execute";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.Execute_Click);
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileNameTextBox.Location = new System.Drawing.Point(194, 12);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(477, 19);
            this.FileNameTextBox.TabIndex = 4;
            this.FileNameTextBox.Text = "C:\\Python36\\python.exe";
            // 
            // WorkingDirectoryTextBox
            // 
            this.WorkingDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WorkingDirectoryTextBox.Location = new System.Drawing.Point(194, 37);
            this.WorkingDirectoryTextBox.Name = "WorkingDirectoryTextBox";
            this.WorkingDirectoryTextBox.Size = new System.Drawing.Size(477, 19);
            this.WorkingDirectoryTextBox.TabIndex = 5;
            this.WorkingDirectoryTextBox.Text = "C:\\tmp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Python Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Working Directory";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Python Command";
            // 
            // ArgmentsTextBox
            // 
            this.ArgmentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ArgmentsTextBox.Location = new System.Drawing.Point(194, 66);
            this.ArgmentsTextBox.Multiline = true;
            this.ArgmentsTextBox.Name = "ArgmentsTextBox";
            this.ArgmentsTextBox.Size = new System.Drawing.Size(477, 39);
            this.ArgmentsTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Standard Input";
            // 
            // StandardInputTextBox
            // 
            this.StandardInputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StandardInputTextBox.Location = new System.Drawing.Point(14, 150);
            this.StandardInputTextBox.Multiline = true;
            this.StandardInputTextBox.Name = "StandardInputTextBox";
            this.StandardInputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.StandardInputTextBox.Size = new System.Drawing.Size(657, 97);
            this.StandardInputTextBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 267);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "Standard Output and Standard Error";
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.Location = new System.Drawing.Point(596, 121);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 13;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.Clear_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(596, 531);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 14;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.Close_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(596, 253);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 15;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // PythonCommandExecutorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 564);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.StandardInputTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ArgmentsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WorkingDirectoryTextBox);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.StandardOutputText);
            this.Name = "PythonCommandExecutorForm";
            this.Text = "Python Executor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PythonCommandExecutorForm_FormClosed);
            this.Load += new System.EventHandler(this.PythonCommandExecutorForm_Load);
            this.Shown += new System.EventHandler(this.PythonCommandExecutorForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox StandardOutputText;
        public System.Windows.Forms.Button ExecuteButton;
        public System.Windows.Forms.TextBox FileNameTextBox;
        public System.Windows.Forms.TextBox WorkingDirectoryTextBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox ArgmentsTextBox;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox StandardInputTextBox;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button ClearButton;
        public System.Windows.Forms.Button CloseButton;
        public System.Windows.Forms.Button CancelButton;
    }
}

