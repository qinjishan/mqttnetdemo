namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtSubcribeTopic = new System.Windows.Forms.TextBox();
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.txtReceiveMsg = new System.Windows.Forms.TextBox();
            this.txtPublishTopic = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSendMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnmeeting = new System.Windows.Forms.Button();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurrentUserId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTargetUserId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(61, 27);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(53, 12);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "订阅主题";
            // 
            // txtSubcribeTopic
            // 
            this.txtSubcribeTopic.Location = new System.Drawing.Point(146, 24);
            this.txtSubcribeTopic.Name = "txtSubcribeTopic";
            this.txtSubcribeTopic.Size = new System.Drawing.Size(145, 21);
            this.txtSubcribeTopic.TabIndex = 1;
            this.txtSubcribeTopic.Text = "家/客厅/空调/温度";
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(303, 22);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(75, 23);
            this.btnSubscribe.TabIndex = 2;
            this.btnSubscribe.Text = "订阅";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
            // 
            // txtReceiveMsg
            // 
            this.txtReceiveMsg.Location = new System.Drawing.Point(72, 64);
            this.txtReceiveMsg.Multiline = true;
            this.txtReceiveMsg.Name = "txtReceiveMsg";
            this.txtReceiveMsg.Size = new System.Drawing.Size(306, 117);
            this.txtReceiveMsg.TabIndex = 3;
            // 
            // txtPublishTopic
            // 
            this.txtPublishTopic.Location = new System.Drawing.Point(164, 211);
            this.txtPublishTopic.Name = "txtPublishTopic";
            this.txtPublishTopic.Size = new System.Drawing.Size(145, 21);
            this.txtPublishTopic.TabIndex = 5;
            this.txtPublishTopic.Text = "家/客厅/空调/温度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "发布主题";
            // 
            // txtSendMsg
            // 
            this.txtSendMsg.Location = new System.Drawing.Point(81, 259);
            this.txtSendMsg.Multiline = true;
            this.txtSendMsg.Name = "txtSendMsg";
            this.txtSendMsg.Size = new System.Drawing.Size(228, 61);
            this.txtSendMsg.TabIndex = 6;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(137, 344);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "发布";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnmeeting
            // 
            this.btnmeeting.Location = new System.Drawing.Point(532, 184);
            this.btnmeeting.Name = "btnmeeting";
            this.btnmeeting.Size = new System.Drawing.Size(75, 23);
            this.btnmeeting.TabIndex = 8;
            this.btnmeeting.Text = "发起会议";
            this.btnmeeting.UseVisualStyleBackColor = true;
            this.btnmeeting.Click += new System.EventHandler(this.btnmeeting_Click);
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(564, 90);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(100, 21);
            this.txtChannel.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(487, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "加入频道";
            // 
            // txtCurrentUserId
            // 
            this.txtCurrentUserId.Location = new System.Drawing.Point(564, 118);
            this.txtCurrentUserId.Name = "txtCurrentUserId";
            this.txtCurrentUserId.Size = new System.Drawing.Size(100, 21);
            this.txtCurrentUserId.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(487, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "当前用户id:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(487, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "邀请用户id:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtTargetUserId
            // 
            this.txtTargetUserId.Location = new System.Drawing.Point(564, 144);
            this.txtTargetUserId.Name = "txtTargetUserId";
            this.txtTargetUserId.Size = new System.Drawing.Size(100, 21);
            this.txtTargetUserId.TabIndex = 13;
            this.txtTargetUserId.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTargetUserId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCurrentUserId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtChannel);
            this.Controls.Add(this.btnmeeting);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSendMsg);
            this.Controls.Add(this.txtPublishTopic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReceiveMsg);
            this.Controls.Add(this.btnSubscribe);
            this.Controls.Add(this.txtSubcribeTopic);
            this.Controls.Add(this.lbl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtSubcribeTopic;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.TextBox txtReceiveMsg;
        private System.Windows.Forms.TextBox txtPublishTopic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSendMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnmeeting;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCurrentUserId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTargetUserId;
    }
}

