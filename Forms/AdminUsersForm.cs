using System;
using System.Windows.Forms;
using ApartmentWinForms.Models;
using System.Collections.Generic;
using ApartmentWinForms.Services;
using System.Runtime.CompilerServices;
namespace ApartmentWinForms.Forms;

public partial class AdminUsersForm : Form
{
    public AdminUsersForm()
    {
        InitializeComponent();

        loadUsers();
        dgvUsers.CellClick += dgvUserCellClick;
    }

    private UserDetailsForm? _currentUserDetailsForm = null;

    private void dgvUserCellClick(object send, DataGridViewCellEventArgs e) {
        if (e.RowIndex < 0) return;

        var row = dgvUsers.Rows[e.RowIndex];
        var userId = row.Cells["colID"].Value.ToString();
        if (userId == null) return;
        Guid idString = Guid.Parse(userId);

        string action = dgvUsers.Columns[e.ColumnIndex].Name.ToString();

        if (action == "colDetails") {
            _currentUserDetailsForm?.Close();
            string email = row.Cells["colEmail"].Value.ToString();
            string userStatus = row.Cells["colStatus"].Value.ToString();
            string joinedAt = row.Cells["colJoined"].Value.ToString();
            string name = row.Cells["colName"].Value.ToString();
            string role = row.Cells["colRole"].Value.ToString();
            _currentUserDetailsForm = new UserDetailsForm(name, email, joinedAt, userStatus, idString, role);
            _currentUserDetailsForm.FormClosed += (s, e) => loadUsers();
            _currentUserDetailsForm.Show();
            return;
        }

        string status = action == "colBlock" ? "Blocked" : action == "colReject" ? "Blocked" : "Approved";

        bool updateSuccesss = UserService.UpdateStatus(idString, status);
        if (updateSuccesss) { 
            loadUsers();
        }

        // Console.WriteLine($"User id P{userId} action {action}");
    }

    private void loadUsers() {
        List<User> users = UserService.GetAllUsers();
        dgvUsers.Rows.Clear();
        // Console.WriteLine(users.Count);
        if (users.Count > 0) {
            foreach (User user in users) {
                dgvUsers.Rows.Add(user.UserID, user.Name, user.Email, user.JoinedAt, user.Status, user.Role);
            }
        }

    }

    

    private void btnApprove_Click(object sender, EventArgs e)
    {
        // Placeholder: call AuthService.ApproveUser() here
    }

    private void btnReject_Click(object sender, EventArgs e)
    {
        // Placeholder: call AuthService.RejectUser() here
    }

    private void btnBlock_Click(object sender, EventArgs e)
    {
        // Placeholder: call AuthService.BlockUser() here
    }

    private void btnUnblock_Click(object sender, EventArgs e)
    {
        // Placeholder: call AuthService.UnblockUser() here
    }

    //private void btnDetails_Click(object sender, EventArgs e)
    //{
    //    var details = new UserDetailsForm("John Smith", "john@company.com", "Jan 15, 2025", "Pending");
    //    details.ShowDialog();
    //}
}
