using ReadingList.Application.DTO.BookList.Abstractions;

namespace ReadingList.Application.DTO.User
{
    public class UserDto : EntityDto
    {
        public string Login { get; set; }

        public int ProfileId { get; set; }

        public string Role { get; set; }

        public string Password { get; set; }
    }
}