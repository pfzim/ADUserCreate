using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.IO;
using System.Management.Automation.Host;
using System.Security;

namespace ADUserCreate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //comboDepartment.SelectedIndex = 0;
            generate_password(16, "0123456789abcdefghijklmnopqrstuvwxvzABCDEFGHIJKLMNOPQRSTUVWXVZ");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int failed = 0;
            string corp_phone = null;
            string phone = null;
            string bday = null;

            textEnLastName.BackColor = System.Drawing.Color.White;
            textEnLastName.ForeColor = System.Drawing.Color.Black;
            textEnFirstName.BackColor = System.Drawing.Color.White;
            textEnFirstName.ForeColor = System.Drawing.Color.Black;
            textLogin.BackColor = System.Drawing.Color.White;
            textLogin.ForeColor = System.Drawing.Color.Black;
            textPassword.BackColor = System.Drawing.Color.White;
            textPassword.ForeColor = System.Drawing.Color.Black;
            textEnPosition.BackColor = System.Drawing.Color.White;
            textEnPosition.ForeColor = System.Drawing.Color.Black;
            textRuFirstName.BackColor = System.Drawing.Color.White;
            textRuFirstName.ForeColor = System.Drawing.Color.Black;
            textRuLastName.BackColor = System.Drawing.Color.White;
            textRuLastName.ForeColor = System.Drawing.Color.Black;
            textRuPosition.BackColor = System.Drawing.Color.White;
            textRuPosition.ForeColor = System.Drawing.Color.Black;
            comboDepartment.BackColor = System.Drawing.Color.White;
            comboDepartment.ForeColor = System.Drawing.Color.Black;

            if (textEnLastName.Text == null || textEnLastName.Text.Length == 0)
            {
                textEnLastName.BackColor = System.Drawing.Color.Red;
                textEnLastName.ForeColor = System.Drawing.Color.White;
                failed++;
            }

            if (textEnFirstName.Text == null || textEnFirstName.Text.Length == 0)
            {
                textEnFirstName.BackColor = System.Drawing.Color.Red;
                textEnFirstName.ForeColor = System.Drawing.Color.White;
                failed++;
            }

            if (textLogin.Text == null || textLogin.Text.Length == 0)
            {
                textLogin.BackColor = System.Drawing.Color.Red;
                textLogin.ForeColor = System.Drawing.Color.White;
                failed++;
            }

            if (textPassword.Text == null || textPassword.Text.Length == 0)
            {
                textPassword.BackColor = System.Drawing.Color.Red;
                textPassword.ForeColor = System.Drawing.Color.White;
                failed++;
            }

            if (textCorpPhone.Text != null && textCorpPhone.Text.Length > 0 && System.Text.RegularExpressions.Regex.Match(textCorpPhone.Text, @"^\d{3}$").Success)
            {
                corp_phone = textCorpPhone.Text;
            }

            if (textCellPhone.Text != null && textCellPhone.Text.Length > 0 && System.Text.RegularExpressions.Regex.Match(textCellPhone.Text, @"^\+7 \d{3} \d{3} \d{4}$").Success)
            {
                phone = textCellPhone.Text;
            }

            if (textBirthday.Text != null && textBirthday.Text.Length > 0 && System.Text.RegularExpressions.Regex.Match(textBirthday.Text, @"^(?:(?:0[1-9])|(?:[12][0-9])|(?:3[01]))\.(?:(?:0[1-9])|(?:1[0-2]))$").Success)
            {
                bday = textBirthday.Text;
            }

            if (textEnPosition.Text == null || textEnPosition.Text.Length == 0)
            {
                textEnPosition.BackColor = System.Drawing.Color.Red;
                textEnPosition.ForeColor = System.Drawing.Color.White;
                failed++;
            }

            if (textRuFirstName.Text == null || textRuFirstName.Text.Length == 0)
            {
                textRuFirstName.BackColor = System.Drawing.Color.Red;
                textRuFirstName.ForeColor = System.Drawing.Color.White;
                failed++;
            }

            if (textRuLastName.Text == null || textRuLastName.Text.Length == 0)
            {
                textRuLastName.BackColor = System.Drawing.Color.Red;
                textRuLastName.ForeColor = System.Drawing.Color.White;
                failed++;
            }

            if (textRuPosition.Text == null || textRuPosition.Text.Length == 0)
            {
                textRuPosition.BackColor = System.Drawing.Color.Red;
                textRuPosition.ForeColor = System.Drawing.Color.White;
                failed++;
            }

            if (comboDepartment.Text == null || comboDepartment.Text.Length == 0)
            {
                comboDepartment.BackColor = System.Drawing.Color.Red;
                comboDepartment.ForeColor = System.Drawing.Color.White;
                failed++;
            }

            if (failed > 0)
            {
                return;
            }

            string textOrganisation;
            string[] groups;

            if(comboDepartment.Text == "Horizon")
            {
                textOrganisation = "Horizon";
                groups = new string[] { "$GetSignature", "$Horizon" };
            }
            else if (comboDepartment.Text == "Air")
            {
                textOrganisation = "Air";
                groups = new string[] { };
            }
            else
            {
                textOrganisation = "Mediainstinct";
                if (comboDepartment.Text == "OOH Buying")
                {
                    groups = new string[] { "$GetSignature", "$OOH Buying" };
                }
                else if (comboDepartment.Text == "Accounting")
                {
                    groups = new string[] { "$GetSignature", "$Accounting" };
                }
                else if (comboDepartment.Text == "Digital")
                {
                    groups = new string[] { "$GetSignature", "$Digital" };
                }
                else if (comboDepartment.Text == "Finance")
                {
                    groups = new string[] { "$GetSignature", "$Finance" };
                }
                else if (comboDepartment.Text == "HR")
                {
                    groups = new string[] { "$GetSignature", "$HR" };
                }
                else if (comboDepartment.Text == "Lawyer")
                {
                    groups = new string[] { "$GetSignature", "$Lawyer" };
                }
                else if (comboDepartment.Text == "Match")
                {
                    groups = new string[] { "$GetSignature", "$Match" };
                }
                else if (comboDepartment.Text == "Media planning")
                {
                    groups = new string[] { "$GetSignature", "$Media planing" };
                }
                else if (comboDepartment.Text == "New Business")
                {
                    groups = new string[] { "$GetSignature", "$NewBusiness" };
                }
                else if (comboDepartment.Text == "OOH Buying")
                {
                    groups = new string[] { "$GetSignature", "$OOHBuying" };
                }
                else if (comboDepartment.Text == "Print Buying")
                {
                    groups = new string[] { "$GetSignature", "$Print Buying" };
                }
                else if (comboDepartment.Text == "Strategy & Research")
                {
                    groups = new string[] { "$GetSignature", "$Strategy" };
                }
                else if (comboDepartment.Text == "TV Buying")
                {
                    groups = new string[] { "$GetSignature", "$TV Buying" };
                }
                else if (comboDepartment.Text == "Print Production")
                {
                    groups = new string[] { "$GetSignature", "$OOH Production" };
                }
                else
                {
                    groups = new string[] { "$GetSignature" };
                }
            }

            // Creating the PrincipalContext
            PrincipalContext principalContext = null;

            try
            {
                string context;
                if (textOrganisation == "Air")
                {
                    context = "OU=Mediainstinct,OU=Air,DC=srv1,DC=sbcmedia,DC=ru";
                }
                else if (textOrganisation == "Horizon")
                {
                    context = "OU=Horizon,OU=Mediainstinct,DC=srv1,DC=sbcmedia,DC=ru";
                }
                else
                {
                    context = "OU=" + textOrganisation + ",DC=srv1,DC=sbcmedia,DC=ru";
                }

                principalContext = new PrincipalContext(ContextType.Domain, "sbcmedia", context);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Failed to create PrincipalContext. Exception: " + exc);
                Application.Exit();
            }

            // Check if user object already exists in the store
            UserPrincipal usr = UserPrincipal.FindByIdentity(principalContext, textLogin.Text);
            if (usr != null)
            {
                MessageBox.Show(textLogin.Text + " already exists. Please use a different User Logon Name.");
                return;
            }

            // Create the new UserPrincipal object
            UserPrincipal userPrincipal = new UserPrincipal(principalContext);

            userPrincipal.UserPrincipalName = textLogin.Text + "@srv1.sbcmedia.ru";
            userPrincipal.Surname = textEnLastName.Text;
            userPrincipal.GivenName = textEnFirstName.Text;
            //userPrincipal.DisplayName = textEnLastName.Text + ' ' + textEnFirstName.Text;
            //userPrincipal.Name = textEnLastName.Text + ' ' + textEnFirstName.Text;
            userPrincipal.DisplayName = textEnFirstName.Text + ' ' + textEnLastName.Text;
            userPrincipal.Name = textEnFirstName.Text + ' ' + textEnLastName.Text;
            userPrincipal.SamAccountName = textLogin.Text;
            userPrincipal.SetPassword(textPassword.Text);
            userPrincipal.EmailAddress = textEnFirstName.Text + '.' + textEnLastName.Text + "@mediainstinctgroup.ru";

            userPrincipal.Enabled = true;
            userPrincipal.PasswordNeverExpires = true;

            try
            {
                userPrincipal.Save();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception creating user object. " + exc);
                return;
            }

            File.AppendAllText("users.txt", DateTime.Now.ToString() + " : " + textEnLastName.Text + ' ' + textEnFirstName.Text + " : " + textLogin.Text + " : " + textPassword.Text + Environment.NewLine);

            if (userPrincipal.GetUnderlyingObjectType() == typeof(DirectoryEntry))
            {
                DirectoryEntry entry = (DirectoryEntry)userPrincipal.GetUnderlyingObject();

                if (phone != null)
                {
                    entry.Properties["mobile"].Value = phone;
                }
                if (corp_phone != null)
                {
                    entry.Properties["telephoneNumber"].Value = corp_phone;
                }
                entry.Properties["title"].Value = textEnPosition.Text;
                entry.Properties["description"].Value = textRuFirstName.Text + ' ' + textRuLastName.Text;
                if(bday != null)
                {
                    entry.Properties["info"].Value = textRuPosition.Text + ", " + bday;
                }
                else
                {
                    entry.Properties["info"].Value = textRuPosition.Text;
                }
                entry.Properties["department"].Value = comboDepartment.Text;
                entry.Properties["company"].Value = textOrganisation;

                try
                {
                    entry.CommitChanges();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Exception modifying info of the user. " + exc);
                    return;
                }
            }

            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "sbcmedia", "DC=srv1,DC=sbcmedia,DC=ru"))
                {
                    foreach(string groupName in groups)
                    {
                        GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName);
                        group.Members.Add(pc, IdentityType.UserPrincipalName, userPrincipal.UserPrincipalName);
                        group.Save();
                    }
                }
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException exc)
            {
                MessageBox.Show("Exception modifying group. " + exc);
                return;
            }

            //PSCredential credential = null;
            var password = new SecureString();
            Array.ForEach("password".ToCharArray(), password.AppendChar);
            PSCredential credential = new PSCredential("login", password);

            WSManConnectionInfo ci = new WSManConnectionInfo(new Uri("https://outlook.office365.com/powershell-liveid/"),
                                                    "http://schemas.microsoft.com/powershell/Microsoft.Exchange",
                                                    credential);

            ci.AuthenticationMechanism = AuthenticationMechanism.Basic;

            try
            {
				using (Runspace runspace = RunspaceFactory.CreateRunspace(ci))
				{
					using (PowerShell session = PowerShell.Create())
					{
						runspace.Open();
						session.Runspace = runspace;

						var pwd = new SecureString();
						Array.ForEach(textPassword.Text.ToCharArray(), pwd.AppendChar);

                        var result = session.AddCommand("New-Mailbox")
							.AddParameter("Alias", textLogin.Text)
							.AddParameter("Name", textEnFirstName.Text + ' ' + textEnLastName.Text)
							.AddParameter("FirstName", textEnFirstName.Text)
							.AddParameter("LastName", textEnLastName.Text)
							.AddParameter("DisplayName", textEnFirstName.Text + ' ' + textEnLastName.Text)
							.AddParameter("MicrosoftOnlineServicesID", textEnFirstName.Text + '.' + textEnLastName.Text + "@mediainstinctgroup.ru")
							.AddParameter("Password", pwd)
							.AddParameter("ResetPasswordOnNextLogon", false)
							.Invoke();

                        if (session.HadErrors)
						{
							string err_msg = null;
							foreach (var error in session.Streams.Error)
							{
								err_msg += error + "\n";
							}

							MessageBox.Show("Create mailbox failed!\n\n" + err_msg);
							return;
						}
						
						session.Commands.Clear();

                        if (textOrganisation == "Horizon")
						{
							result = session.AddCommand("New-Mailbox")
								.AddParameter("Shared")
								.AddParameter("Name", textEnFirstName.Text + ' ' + textEnLastName.Text + "Horizon")
								.AddParameter("Alias", textLogin.Text+"_hz")
								.AddParameter("PrimarySmtpAddress", textEnFirstName.Text + '.' + textEnLastName.Text + "@horizonmedia.ru")
								.AddParameter("DisplayName", textEnFirstName.Text + ' ' + textEnLastName.Text)
								.Invoke();

							if (session.HadErrors)
							{
								string err_msg = null;
								foreach (var error in session.Streams.Error)
								{
									err_msg += error + "\n";
								}

								MessageBox.Show("Create shared mailbox failed!\n\n" + err_msg);
								return;
							}
							
							session.Commands.Clear();

                            result = session.AddCommand("Set-Mailbox")
                                .AddParameter("Identity", textLogin.Text + "_hz")
                                .AddParameter("GrantSendOnBehalfTo", textEnFirstName.Text + '.' + textEnLastName.Text + "@mediainstinctgroup.ru")
                                .Invoke();

                            if (session.HadErrors)
                            {
                                string err_msg = null;
                                foreach (var error in session.Streams.Error)
                                {
                                    err_msg += error + "\n";
                                }

                                MessageBox.Show("Create shared mailbox failed!\n\n" + err_msg);
                                return;
                            }

                            session.Commands.Clear();

                            
                            result = session.AddCommand("Add-MailboxPermission")
                                .AddParameter("Identity", textLogin.Text + "_hz")
                                .AddParameter("User", textEnFirstName.Text + '.' + textEnLastName.Text + "@mediainstinctgroup.ru")
                                .AddParameter("AccessRights", "FullAccess")
                                .AddParameter("InheritanceType", "All")
                                .Invoke();

                            if (session.HadErrors)
                            {
                                string err_msg = null;
                                foreach (var error in session.Streams.Error)
                                {
                                    err_msg += error + "\n";
                                }

                                MessageBox.Show("Create shared mailbox failed!\n\n" + err_msg);
                                return;
                            }
                            session.Commands.Clear();

                            result = session.AddCommand("Add-RecipientPermission")
                                .AddParameter("Identity", textLogin.Text + "_hz")
                                .AddParameter("Trustee", textEnFirstName.Text + '.' + textEnLastName.Text + "@mediainstinctgroup.ru")
                                .AddParameter("AccessRights", "SendAs")
                                .AddParameter("confirm", false)
                                .Invoke();

                            if (session.HadErrors)
                            {
                                string err_msg = null;
                                foreach (var error in session.Streams.Error)
                                {
                                    err_msg += error + "\n";
                                }

                                MessageBox.Show("Create shared mailbox failed!\n\n" + err_msg);
                                return;
                            }
                            session.Commands.Clear();
                        }

                        session.AddCommand("Exit-PSSession").Invoke();
                        runspace.Close();
                    }
                }
			}

            catch (Exception exc)
            {
                MessageBox.Show("Create mailbox error. " + exc);
                return;
            }

            InitialSessionState initialSession = InitialSessionState.CreateDefault();
            initialSession.ImportPSModule(new[] { "MSOnline" });

            try
            {
                using (Runspace runspace = RunspaceFactory.CreateRunspace(initialSession))
                {
                    using (PowerShell session = PowerShell.Create())
                    {
                        runspace.Open();
                        session.Runspace = runspace;

                        var pwd = new SecureString();
                        Array.ForEach(textPassword.Text.ToCharArray(), pwd.AppendChar);

                        var result = session.AddCommand("Connect-MsolService")
                            .AddParameter("Credential", credential)
                            .Invoke();

                        if (session.HadErrors)
                        {
                            string err_msg = null;
                            foreach (var error in session.Streams.Error)
                            {
                                err_msg += error + "\n";
                            }

                            MessageBox.Show("Create mailbox failed!\n\n" + err_msg);
                            return;
                        }
                        session.Commands.Clear();

                        result = session.AddCommand("Set-MsolUser")
                            .AddParameter("UserPrincipalName", textEnFirstName.Text + '.' + textEnLastName.Text + "@mediainstinctgroup.ru")
                            .AddParameter("UsageLocation", "RU")
                            .Invoke();

                        if (session.HadErrors)
                        {
                            string err_msg = null;
                            foreach (var error in session.Streams.Error)
                            {
                                err_msg += error + "\n";
                            }

                            MessageBox.Show("Create mailbox failed!\n\n" + err_msg);
                            return;
                        }
                        session.Commands.Clear();

                        result = session.AddCommand("Set-MsolUserLicense")
                            .AddParameter("UserPrincipalName", textEnFirstName.Text + '.' + textEnLastName.Text + "@mediainstinctgroup.ru")
                            .AddParameter("AddLicenses", "reseller-account:EXCHANGESTANDARD")
                            .Invoke();

                        if (session.HadErrors)
                        {
                            string err_msg = null;
                            foreach (var error in session.Streams.Error)
                            {
                                err_msg += error + "\n";
                            }

                            MessageBox.Show("Create mailbox failed!\n\n" + err_msg);
                            return;
                        }
                        session.Commands.Clear();

                        runspace.Close();
                    }
                }
            }

            catch (Exception exc)
            {
                MessageBox.Show("Create mailbox error. " + exc);
                return;
            }

            if ((textOrganisation != "Air")
                && MessageBox.Show("Insert new initialized eToken", "Certificate", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    PowerShell ps = PowerShell.Create();

                    ps.Commands.AddScript(
                        "$pkcs10 = New-Object -ComObject X509Enrollment.CX509CertificateRequestPkcs10;" +
                        "$pkcs10.InitializeFromTemplateName(0x1,\"Win2003Пользовательсосмарт-картой\");" +
                        "$pkcs10.Encode();" +
                        "$pkcs7 = New-Object -ComObject X509enrollment.CX509CertificateRequestPkcs7;" +
                        "$pkcs7.InitializeFromInnerRequest($pkcs10);" +
                        "$pkcs7.RequesterName = \"SBCMEDIA\\" + textLogin.Text + "\";" +
                        "$signer = New-Object -ComObject X509Enrollment.CSignerCertificate;" +
                        "$cert = Get-ChildItem Cert:\\CurrentUser\\My | Where-Object {$_.Extensions | Where-Object {$_.Oid.Value -eq \"2.5.29.37\" -and $_.EnhancedKeyUsages[\"1.3.6.1.4.1.311.20.2.1\"]}};" +
                        "$base64 = [Convert]::ToBase64String($cert.RawData);" +
                        "$signer = New-Object -ComObject X509Enrollment.CSignerCertificate;" + 
                        "$signer.Initialize(0, 0, 1,$base64);" +
                        "$pkcs7.SignerCertificate = $signer;" +
                        "$Request = New-Object -ComObject X509Enrollment.CX509Enrollment;" +
                        "$Request.InitializeFromRequest($pkcs7);" +
                        "$Request.Enroll();"
                        );

                    var result = ps.Invoke();
                    if (ps.HadErrors)
                    {
                        string err_msg = null;
                        foreach (var error in ps.Streams.Error)
                        {
                            err_msg += error + "\n";
                        }

                        MessageBox.Show("Create certificate errors:\n\n" + err_msg);
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Create certificate failed. " + exc);
                }

            }

            MessageBox.Show("User successfully added!");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void generate_password(int len, string password_chars)
        {
            //string password_chars = "0123456789abcdefghijklmnopqrstuvwxvzABCDEFGHIJKLMNOPQRSTUVWXVZ";
            textPassword.Text = "";
            int i;
            Random rnd = new Random();
            for (i = 0; i < len; i++)
            {
                textPassword.Text += password_chars[rnd.Next(password_chars.Length)];
            }
        }

        private void buttonGeneratePassword_Click(object sender, EventArgs e)
        {
            generate_password(8, "23456789abcdefghijkmnopqrstuvwxvz");
        }

        static Dictionary<char, string> translation = new Dictionary<char, string>()
        {
            { 'а', "a" },
            { 'б', "b" },
            { 'в', "v" },
            { 'г', "g" },
            { 'д', "d" },
            { 'е', "e" },
            { 'ё', "yo" },
            { 'ж', "zh" },
            { 'з', "z" },
            { 'и', "i" },
            { 'й', "y" },
            { 'к', "k" },
            { 'л', "l" },
            { 'м', "m" },
            { 'н', "n" },
            { 'о', "o" },
            { 'п', "p" },
            { 'р', "r" },
            { 'с', "s" },
            { 'т', "t" },
            { 'у', "u" },
            { 'ф', "f" },
            { 'х', "kh" },
            { 'ц', "ts" },
            { 'ч', "ch" },
            { 'ш', "sh" },
            { 'щ', "shch" },
            { 'ъ', "" },
            { 'ы', "y" },
            { 'ь', "" },
            { 'э', "e" },
            { 'ю', "yu" },
            { 'я', "ya"},
            { 'А', "A" },
            { 'Б', "B" },
            { 'В', "V" },
            { 'Г', "G" },
            { 'Д', "D" },
            { 'Е', "E" },
            { 'Ё', "Yo" },
            { 'Ж', "Zh" },
            { 'З', "Z" },
            { 'И', "I" },
            { 'Й', "Y" },
            { 'К', "K" },
            { 'Л', "L" },
            { 'М', "M" },
            { 'Н', "N" },
            { 'О', "O" },
            { 'П', "P" },
            { 'Р', "R" },
            { 'С', "S" },
            { 'Т', "T" },
            { 'У', "U" },
            { 'Ф', "F" },
            { 'Х', "Kh" },
            { 'Ц', "Ts" },
            { 'Ч', "Ch" },
            { 'Ш', "Sh" },
            { 'Щ', "Shch" },
            { 'Ъ', "" },
            { 'Ы', "Y" },
            { 'Ь', "" },
            { 'Э', "E" },
            { 'Ю', "Yu" },
            { 'Я', "Ya"}
        };

        public static string GetTranslit(string sourceText)
        {
            StringBuilder ans = new StringBuilder();
            for (int i = 0; i < sourceText.Length; i++)
            {
                if (translation.ContainsKey(sourceText[i]))
                {
                    ans.Append(translation[sourceText[i]]);
                }
                else
                {
                    ans.Append(sourceText[i].ToString());
                }
            }
            return ans.ToString();
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            string[] full_name = textLogin.Text.Trim().Replace("  ", " ").Split(' ');

            textEnFirstName.Text = GetTranslit(full_name[1]);
            textEnLastName.Text = GetTranslit(full_name[0]);
            textRuFirstName.Text = full_name[1];
            textRuLastName.Text = full_name[0];
            textLogin.Text = (textEnFirstName.Text[0] + "." + textEnLastName.Text).ToLower();
        }

    }
}
