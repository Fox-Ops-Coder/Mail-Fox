using MFData.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFData.Core
{
    public interface IMFCore
    {
        Task<bool> EnsureCreatedAsync();

        Task<IEnumerable<UserEmail>> GetUserEmailsAsync();
        Task<IEnumerable<Contact>> GetContactsAsync();
        Task<IEnumerable<Blank>> GetBlankAsync();

        Task AddUserEmailAsync(UserEmail newEmail);
        Task AddContactAsync(Contact newContact);
        Task AddBlankAsync(Blank newBlank);

        Task RemoveUserEmailAsync(UserEmail userEmail);
        Task RemoveContactAsync(Contact contact);
        Task RemoveBlankAsync(Blank blank);

        Task UpdateUserEmailAsync(UserEmail userEmail);
        Task UpdateContactAsync(Contact contact);
        Task UpdateBlankAsync(Blank blank);
    }
}