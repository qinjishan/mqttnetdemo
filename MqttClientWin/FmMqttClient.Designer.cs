namespace MqttClientWin
{
    partial class FmMqttClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubTopic = new System.Windows.Forms.TextBox();
            this.txtReceiveMessage = new System.Windows.Forms.TextBox();
            this.txtPubTopic = new System.Windows.Forms.TextBox();
            this.发布主题 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "订阅主题";
            // 
            // txtSubTopic
            // 
            this.txtSubTopic.Location = new System.Drawing.Point(132, 28);
            this.txtSubTopic.Name = "txtSubTopic";
            this.txtSubTopic.Size = new System.Drawing.Size(176, 21);
            this.txtSubTopic.TabIndex = 1;
            this.txtSubTopic.Text = "家/客厅/空调/温度";
            // 
            // txtReceiveMessage
            // 
            this.txtReceiveMessage.Location = new System.Drawing.Point(61, 72);
            this.txtReceiveMessage.Multiline = true;
            this.txtReceiveMessage.Name = "txtReceiveMessage";
            this.txtReceiveMessage.Size = new System.Drawing.Size(247, 117);
            this.txtReceiveMessage.TabIndex = 2;
            // 
            // txtPubTopic
            // 
            this.txtPubTopic.Location = new System.Drawing.Point(132, 216);
            this.txtPubTopic.Name = "txtPubTopic";
            this.txtPubTopic.Size = new System.Drawing.Size(176, 21);
            this.txtPubTopic.TabIndex = 4;
            this.txtPubTopic.Text = "家/客厅/空调/温度";
            // 
            // 发布主题
            // 
            this.发布主题.AutoSize = true;
            this.发布主题.Location = new System.Drawing.Point(59, 219);
            this.发布主题.Name = "发布主题";
            this.发布主题.Size = new System.Drawing.Size(53, 12);
            this.发布主题.TabIndex = 3;
            this.发布主题.Text = "发布主题";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(132, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "发布";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(337, 26);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(75, 23);
            this.btnSubscribe.TabIndex = 6;
            this.btnSubscribe.Text = "订阅";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.BtnSubscribe_Click);
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.Location = new System.Drawing.Point(58, 263);
            this.txtSendMessage.Multiline = true;
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(233, 75);
            this.txtSendMessage.TabIndex = 7;
            // 
            // FmMqttClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 450);
            this.Controls.Add(this.txtSendMessage);
            this.Controls.Add(this.btnSubscribe);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtPubTopic);
            this.Controls.Add(this.发布主题);
            this.Controls.Add(this.txtReceiveMessage);
            this.Controls.Add(this.txtSubTopic);
            this.Controls.Add(this.label1);
            this.Name = "FmMqttClient";
            this.Text = "FmMqttClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubTopic;
        private System.Windows.Forms.TextBox txtReceiveMessage;
        private System.Windows.Forms.TextBox txtPubTopic;
        private System.Windows.Forms.Label 发布主题;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.TextBox txtSendMessage;
    }
}