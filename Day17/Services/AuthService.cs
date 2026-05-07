using System.Linq;
using Day15.Models;

namespace Day15.Services
{
    public class AuthService
    {
        private readonly DataService _dataService;
        private User _currentUser;

        public AuthService(DataService dataService)
        {
            _dataService = dataService;
        }

        public User CurrentUser => _currentUser;

        public bool Login(string login, string password)
        {
            var user = _dataService.Users.FirstOrDefault(u => u.Login == login);
            if (user != null && _dataService.VerifyPassword(password, user.PasswordHash))
            {
                _currentUser = user;
                return true;
            }
            return false;
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public bool IsDoctor => _currentUser != null && _currentUser.Role == UserRole.Doctor;
        public bool IsPatient => _currentUser != null && _currentUser.Role == UserRole.Patient;
        public bool IsAuthenticated => _currentUser != null;
    }
}