
namespace Presentation
{
    partial class frmProductAU
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtImage = new System.Windows.Forms.TextBox();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Brand :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Model :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Price :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Stock :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 376);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Imagen :";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Font = new System.Drawing.Font("Trebuchet MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(150, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 29);
            this.label8.TabIndex = 7;
            this.label8.Text = "Product";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 406);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(399, 44);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(306, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(210, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(106, 67);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(288, 22);
            this.txtName.TabIndex = 9;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(106, 120);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(288, 22);
            this.txtDescription.TabIndex = 10;
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(106, 169);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(288, 22);
            this.txtBrand.TabIndex = 11;
            this.txtBrand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBrand_KeyPress);
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(106, 217);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(288, 22);
            this.txtModel.TabIndex = 12;
            this.txtModel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtModel_KeyPress);
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(106, 265);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(156, 22);
            this.txtPrice.TabIndex = 13;
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(106, 317);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(156, 22);
            this.txtStock.TabIndex = 14;
            this.txtStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStock_KeyPress);
            // 
            // txtImage
            // 
            this.txtImage.Enabled = false;
            this.txtImage.Location = new System.Drawing.Point(106, 370);
            this.txtImage.Name = "txtImage";
            this.txtImage.Size = new System.Drawing.Size(213, 22);
            this.txtImage.TabIndex = 15;
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(325, 369);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(75, 23);
            this.btnExaminar.TabIndex = 16;
            this.btnExaminar.Text = "Examinar";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // frmProductAU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 456);
            this.Controls.Add(this.btnExaminar);
            this.Controls.Add(this.txtImage);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProductAU";
            this.Text = "frmProductAU";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtImage;
        private System.Windows.Forms.Button btnExaminar;
    }
}