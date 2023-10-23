using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FC12.SupportExtensions.Models;
using Microsoft.Office.Interop.Outlook;
using Newtonsoft.Json;
using FC12.SupportExtensions.Outlook;
using System.Runtime.CompilerServices;
using Integra.Client;


namespace FC12.SupportExtensions
{
    public partial class SupportMessageForm : Form
    {
        private static SupportRequest Request { get; set; }
        private static IntegraClient _integraClient;
        private readonly bool DebugState;

        public SupportMessageForm(SupportRequest request, IntegraClient integraClient = null, bool debug = false)
        {
            Request = request;
            DebugState = debug;
            _integraClient = integraClient;
            CenterToScreen();
            InitializeComponent();
        }

        private void DebugRun()
        {
            string SupportRequestJson = JsonConvert.SerializeObject(Request, Newtonsoft.Json.Formatting.Indented);
            MessageBox.Show(SupportRequestJson);
        } 

        private void SendRun()
        {
            
            OutlookHelper.CreateEmailSample(Request);
            ExecuteIntegraRequest();
            this.Close();
        }

        #region [Form Control Elements]
        private void SupportMessageForm_Load(object sender, EventArgs e)
        {
            BatchNameValueLabel.Text = Request.BatchName;
        }

        private void PriorityHighRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PriorityHighRadioButton.Checked) Request.RequestPriority = RequestPriority.High;
        }

        private void PriorityLowRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PriorityLowRadioButton.Checked) Request.RequestPriority = RequestPriority.Low;
        }

        private void PriorityMediumRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PriorityMediumRadioButton.Checked) Request.RequestPriority = RequestPriority.Medium;
        }

        private void PriorityUrgentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PriorityUrgentRadioButton.Checked) Request.RequestPriority = RequestPriority.Urgent;
        }

        private void CategoryLayOutCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CategoryCheckBoxHandler(CategoryLayOutCheckBox);
        }

        private void CategoryAttributeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CategoryCheckBoxHandler(CategoryAttributeCheckBox);
        }

        private void CategoryExceptionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CategoryCheckBoxHandler(CategoryExceptionCheckBox);
        }

        private void CategoryAnotherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CategoryCheckBoxHandler(CategoryAnotherCheckBox);
        }

        private void CategoryCheckBoxHandler(CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                Request.AddCategory(checkBox.Text);
            }
            else
            {
                Request.RemoveCategory(checkBox.Text);
            }
        }

        private void CommentValueTextBox_TextChanged(object sender, EventArgs e)
        {
            Request.Comment = CommentValueTextBox.Text;
        }

        #endregion
        private void SendButton_Click(object sender, EventArgs e)
        {
            if (Request.IsValid())
            {
                CategoriesGroup.BackColor = Color.Transparent;
            } 
            else
            {
                CategoriesGroup.BackColor = Color.Tomato;
                MessageBox.Show("Выберите хотя бы одну категорию");
                return;
            }

            if (this.DebugState)
            {
                DebugRun();
            }
            else
            {
                SendRun(); 
            }
        }

        private static void ExecuteIntegraRequest()
        {
            CreateSupportRequestDto requestDto = new CreateSupportRequestDto()
            {
                BatchId = Request.BatchId,
                BatchOwner = Request.BatchOwner,
                Categories = Request.Categories,
                Comment = Request.Comment,
                Priority = (Priority)Request.RequestPriority,
                Smid = Request.SMID
            };

            _integraClient.SupportRequestPOSTAsync(requestDto);
        }
    }
}
