using ReadingList.Domain.Entities;
using ReadingList.Domain.Entities.Identity;
using ReadingList.Application.Exceptions;
using Xunit;

namespace ReadingList.Tests
{
    public class ObjectNotExistExceptionTests
    {
        [Fact]
        public void ObjectNotExistForException_GeneratesMessage_BookListWith_ListData_ForUserWith_UserData_DoesNotExist()
        {           
            const string username = "test";
            const int listId = 1;

            var result =
                $"Book list with Id:{listId.ToString()} for user with Email:{username} doesn't exist";

            var exception =
                new ObjectNotExistForException<BookList, User>(new OnExceptionObjectDescriptor
                    {
                        ["Id"] = listId.ToString()
                    },
                    new OnExceptionObjectDescriptor
                    {
                        ["Email"] = username
                    });

            Assert.Equal(result, exception.Message);
        }
        
        [Fact]
        public void ObjectNotExistForException_GeneratesMessage_BookListWith_ListData_DoesNotExist()
        {
            const int listId = 1;

            var result =
                $"Book list with Id:{listId.ToString()} doesn't exist";

            var exception =
                new ObjectNotExistException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = listId.ToString()
                });

            Assert.Equal(result, exception.Message);
        }
        
        [Fact]
        public void ObjectNotExistForException_GeneratesMessage_BookList_ForUserWith_UserData_DoesNotExist_When_BookListDescriptorIsNull()
        {            
            const string email = "test@test.com";

            var result =
                $"Book list for user with Email:{email} doesn't exist";

            var exception =
                new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
                {
                    ["Email"] = email
                });

            Assert.Equal(result, exception.Message);
        }
        
        [Fact]
        public void ObjectNotExistForException_GeneratesMessage_BookListWith_BookListData_ForUser_DoesNotExist_When_UserDescriptorIsNull()
        {            
            const int id = 2;

            var result =
                $"Book list with Id:{id.ToString()} for user doesn't exist";

            var exception =
                new ObjectNotExistForException<BookList, User>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = id.ToString()
                }, null);

            Assert.Equal(result, exception.Message);
        }

        [Fact]
        public void ObjectNotExistException_GeneratesMessage_SharedBookListItemWith_SharedBookListItemData_DoesNotExist()
        {            
            const int id = 2;

            var result =
                $"Shared book list item with Id:{id.ToString()} doesn't exist";

            var exception =
                new ObjectNotExistException<SharedBookListItem>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = id.ToString()
                });

            Assert.Equal(result, exception.Message);
        }

        [Fact]
        public void ObjectNotExistException_GeneratesMessage_PrivateBookListItemWith_PrivateBookListItemData_DoesNotExist()
        {            
            const int id = 2;

            var result =
                $"Private book list item with Id:{id.ToString()} doesn't exist";

            var exception =
                new ObjectNotExistException<PrivateBookListItem>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = id.ToString()
                });

            Assert.Equal(result, exception.Message);
        }
    }
}