namespace Samids_API
{
    public static class Constants
    {

        //SubjectVerifications 
        //Ok

        //NoGood
        public const string SubjectNotFound = "Sujbect doesn't exist on the database";

        //FacultyVerifications
        //Ok
        public const string FacultyNotFound = "Faculty doesn't exist on the database";
        public const string FacultyNotAuthorized = "Faculty may not have access or not assigned to any subject";
        public const string FacultyDelete = "Faculty successfully deleted";
        //NoGood


        //AttendanceVeriifications
        //Ok

        //NoGood
        public const string StudentNotAuthorized = "Student doesn't have this schedule";


        //StudentVerifications
        //Ok
        public const string StudentDelete = "Student successfully deleted";


        //NoGood
        public const string StudentNotFound = "Student doesn't exist on the database";




        //AuthVerifications
        public const string PasswordSame = "New password cannot be your old password";
        public const string PasswordChanged = "Password successfully changed. All logins will be logged out";


        //User Verifications
        //Ok
        public const string OkEmail = "Email is available";
        public const string OkStudent = "Student is available for register";
        public const string OkDelete = "User successfully deleted";
        public const string OkPassword = "Password is correct";

        //NoGood
        public const string EmailAlreadyExists = "Email is already in use by another user. Please use anothery email";
        public const string UserNotFound = "User doesn't exist on the database";
        public const string StudentAlreadyRegistered = "Student is already registered. Please try logging in";
        public const string UserNotRegistered = "User/Student is not registered, Try registering";
        public const string NGPassword = "Password is incorrect";
    }
}
