using DataContext.Models;

namespace Web.Models.Response
{
    public class ResponsePosition
    {
        public int Id { get; set; }
        public int IdDrivingSchool { get; set; }
        public ResponseDrivingSchool DrivingSchool { get; set; }
        public int IdRefPosition { get; set; }
        public ResponseRefPosition RefPosition { get; set; }
        public string IdUser { get; set; }
        public ApplicationUser User { get; set; }
        public int Salary { get; set; }
        public List<ResponseHavings> Havings { get; set; }
    }
}
