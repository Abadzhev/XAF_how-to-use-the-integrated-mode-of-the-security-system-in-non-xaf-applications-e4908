﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication {
	public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm {
		public SecurityStrategyComplex Security { get; set; }
		public IObjectSpaceProvider ObjectSpaceProvider { get; set; }
		public MainForm(SecurityStrategyComplex security, IObjectSpaceProvider objectSpaceProvider) {
			InitializeComponent();
			Security = security;
			ObjectSpaceProvider = objectSpaceProvider;
		}
		private void MainForm_Load(object sender, EventArgs e) {
			ShowLoginForm();
		}
		private void LogoffButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
			foreach(Form form in MdiChildren) {
				form.Close();
			}
			Security.Logoff();
			Hide();
			ShowLoginForm();
		}
		private void ShowLoginForm() {
			LoginForm loginForm = new LoginForm(Security, ObjectSpaceProvider);
			DialogResult dialogResult = loginForm.ShowDialog();
			if(dialogResult == DialogResult.OK) {
				CreateListForm();
				Show();
			}
			else {
				Close();
			}
		}
		private void CreateListForm() {
			EmployeeListForm employeeForm = new EmployeeListForm();
			employeeForm.MdiParent = this;
			employeeForm.WindowState = FormWindowState.Maximized;
			employeeForm.Show();
		}
	}
}
