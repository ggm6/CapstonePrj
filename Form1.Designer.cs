namespace SafeAndSecure
{
    partial class Form1
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
            this.CreateAccount = new System.Windows.Forms.Button();
            this.Login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateAccount
            // 
            this.CreateAccount.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.CreateAccount.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.CreateAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateAccount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CreateAccount.Location = new System.Drawing.Point(280, 104);
            this.CreateAccount.Name = "CreateAccount";
            this.CreateAccount.Size = new System.Drawing.Size(168, 49);
            this.CreateAccount.TabIndex = 0;
            this.CreateAccount.Text = "Create Account";
            this.CreateAccount.UseVisualStyleBackColor = false;
            this.CreateAccount.Click += new System.EventHandler(this.CreateAccount_Click);
            // 
            // Login
            // 
            this.Login.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Login.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Login.Location = new System.Drawing.Point(280, 232);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(168, 44);
            this.Login.TabIndex = 1;
            this.Login.Text = "Login";
            this.Login.UseVisualStyleBackColor = false;
            this.Login.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.CreateAccount);
            this.Name = "Form1";
            this.Text = "Safe&Secure";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button CreateAccount;
        private System.Windows.Forms.Button Login;
    }
}

