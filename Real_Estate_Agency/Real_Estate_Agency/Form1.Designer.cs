namespace Real_Estate_Agency {
    partial class Form1 {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.propertyGridView = new System.Windows.Forms.DataGridView();
            this.ProperyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyKind = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyOwnerID = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.peopleGridView = new System.Windows.Forms.DataGridView();
            this.PeopleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeopleLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeopleFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeoplePatronymic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeoplePass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeoplePhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeopleEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.customersDataGridView = new System.Windows.Forms.DataGridView();
            this.CustomersID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomersHumanID = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CustomersPropertyID = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.loadToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peopleGridView)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customersDataGridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(764, 351);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.propertyGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(756, 325);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Имущество";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // propertyGridView
            // 
            this.propertyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.propertyGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProperyID,
            this.PropertyKind,
            this.PropertyName,
            this.PropertyCost,
            this.PropertyOwnerID});
            this.propertyGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridView.Location = new System.Drawing.Point(3, 3);
            this.propertyGridView.Name = "propertyGridView";
            this.propertyGridView.Size = new System.Drawing.Size(750, 319);
            this.propertyGridView.TabIndex = 0;
            // 
            // ProperyID
            // 
            this.ProperyID.HeaderText = "ID";
            this.ProperyID.Name = "ProperyID";
            this.ProperyID.ReadOnly = true;
            // 
            // PropertyKind
            // 
            this.PropertyKind.HeaderText = "Вид";
            this.PropertyKind.Items.AddRange(new object[] {
            "Недвижемое",
            "Движемое"});
            this.PropertyKind.Name = "PropertyKind";
            // 
            // PropertyName
            // 
            this.PropertyName.HeaderText = "Название";
            this.PropertyName.Name = "PropertyName";
            this.PropertyName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PropertyName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PropertyCost
            // 
            this.PropertyCost.HeaderText = "Стоимость";
            this.PropertyCost.Name = "PropertyCost";
            this.PropertyCost.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PropertyCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PropertyOwnerID
            // 
            this.PropertyOwnerID.HeaderText = "Владелец";
            this.PropertyOwnerID.Name = "PropertyOwnerID";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.peopleGridView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(756, 325);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Люди";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // peopleGridView
            // 
            this.peopleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.peopleGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PeopleID,
            this.PeopleLastName,
            this.PeopleFirstName,
            this.PeoplePatronymic,
            this.PeoplePass,
            this.PeoplePhone,
            this.PeopleEmail});
            this.peopleGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peopleGridView.Location = new System.Drawing.Point(3, 3);
            this.peopleGridView.Name = "peopleGridView";
            this.peopleGridView.Size = new System.Drawing.Size(750, 319);
            this.peopleGridView.TabIndex = 1;
            // 
            // PeopleID
            // 
            this.PeopleID.HeaderText = "ID";
            this.PeopleID.Name = "PeopleID";
            this.PeopleID.ReadOnly = true;
            // 
            // PeopleLastName
            // 
            this.PeopleLastName.HeaderText = "Фамилия";
            this.PeopleLastName.Name = "PeopleLastName";
            // 
            // PeopleFirstName
            // 
            this.PeopleFirstName.HeaderText = "Имя";
            this.PeopleFirstName.Name = "PeopleFirstName";
            // 
            // PeoplePatronymic
            // 
            this.PeoplePatronymic.HeaderText = "Отчество";
            this.PeoplePatronymic.Name = "PeoplePatronymic";
            // 
            // PeoplePass
            // 
            this.PeoplePass.HeaderText = "Паспорт";
            this.PeoplePass.Name = "PeoplePass";
            // 
            // PeoplePhone
            // 
            this.PeoplePhone.HeaderText = "Телефон";
            this.PeoplePhone.Name = "PeoplePhone";
            // 
            // PeopleEmail
            // 
            this.PeopleEmail.HeaderText = "Эл. почта";
            this.PeopleEmail.Name = "PeopleEmail";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.customersDataGridView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(756, 325);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Потенциальные покупатели";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // customersDataGridView
            // 
            this.customersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustomersID,
            this.CustomersHumanID,
            this.CustomersPropertyID});
            this.customersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customersDataGridView.Location = new System.Drawing.Point(3, 3);
            this.customersDataGridView.Name = "customersDataGridView";
            this.customersDataGridView.Size = new System.Drawing.Size(750, 319);
            this.customersDataGridView.TabIndex = 1;
            // 
            // CustomersID
            // 
            this.CustomersID.HeaderText = "ИД";
            this.CustomersID.Name = "CustomersID";
            this.CustomersID.ReadOnly = true;
            // 
            // CustomersHumanID
            // 
            this.CustomersHumanID.HeaderText = "Человек";
            this.CustomersHumanID.Name = "CustomersHumanID";
            // 
            // CustomersPropertyID
            // 
            this.CustomersPropertyID.HeaderText = "Недвижимость";
            this.CustomersPropertyID.Name = "CustomersPropertyID";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripButton,
            this.loadToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(788, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Сохранить";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // loadToolStripButton
            // 
            this.loadToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("loadToolStripButton.Image")));
            this.loadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadToolStripButton.Name = "loadToolStripButton";
            this.loadToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.loadToolStripButton.Text = "Загрузить";
            this.loadToolStripButton.Click += new System.EventHandler(this.loadToolStripButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 391);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Агенство недвижимости";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peopleGridView)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customersDataGridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton loadToolStripButton;
        private System.Windows.Forms.DataGridView propertyGridView;
        private System.Windows.Forms.DataGridView peopleGridView;
        private System.Windows.Forms.DataGridView customersDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeopleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeopleLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeopleFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeoplePatronymic;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeoplePass;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeoplePhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeopleEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProperyID;
        private System.Windows.Forms.DataGridViewComboBoxColumn PropertyKind;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyCost;
        private System.Windows.Forms.DataGridViewComboBoxColumn PropertyOwnerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomersID;
        private System.Windows.Forms.DataGridViewComboBoxColumn CustomersHumanID;
        private System.Windows.Forms.DataGridViewComboBoxColumn CustomersPropertyID;
    }
}

