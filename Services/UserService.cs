namespace ApartmentWinForms.Services;

public static class UserService
{
    // Called when AdminUsersForm loads
    public static List<User> GetAllUsers() { }

    // Called for filter tabs in AdminUsersForm
    public static List<User> GetUsersByStatus(string status) { }

    // Called by Approve button in AdminUsersForm
    public static bool ApproveUser(Guid userId) { }

    // Called by Reject button in AdminUsersForm
    public static bool RejectUser(Guid userId) { }

    // Called by Block button in AdminUsersForm
    public static bool BlockUser(Guid userId) { }

    // Called by Unblock button in AdminUsersForm
    public static bool UnblockUser(Guid userId) { }

    // Called for Admin dashboard stat card
    public static int GetTotalUsersCount() { }

    // Called for pending badge in AdminUsersForm
    public static int GetPendingUsersCount() { }

    // Called by Details button in AdminUsersForm
    public static User? GetUserById(Guid userId) { }
}