using MFData.Context;
using MFData.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFData.Core
{
    internal class MFCore : IMFCore
    {
        private readonly MFDatabase database;

        public MFCore(MFDatabase database) =>
            this.database = database;

        public bool EnsureCreated() =>
            database.Database.EnsureCreated();

        public async Task AddBlankAsync(Blank newBlank)
        {
            await database.Blanks.AddAsync(newBlank);
            await database.SaveChangesAsync();
        }

        public async Task AddContactAsync(Contact newContact)
        {
            await database.Contacts.AddAsync(newContact);
            await database.SaveChangesAsync();
        }

        public async Task AddUserEmailAsync(UserEmail newEmail)
        {
            await database.UserEmails.AddAsync(newEmail);
            await database.SaveChangesAsync();
        }

        public async Task<IEnumerable<Blank>> GetBlankAsync() =>
            await database.Blanks.ToListAsync();

        public async Task<IEnumerable<Contact>> GetContactsAsync() =>
            await database.Contacts.ToListAsync();

        public async Task<IEnumerable<UserEmail>> GetUserEmailsAsync() =>
            await database.UserEmails.ToListAsync();

        public async Task RemoveBlankAsync(Blank blank)
        {
            database.Blanks.Remove(blank);
            await database.SaveChangesAsync();
        }

        public async Task RemoveContactAsync(Contact contact)
        {
            database.Contacts.Remove(contact);
            await database.SaveChangesAsync();
        }

        public async Task RemoveUserEmailAsync(UserEmail userEmail)
        {
            database.UserEmails.Remove(userEmail);
            await database.SaveChangesAsync();
        }

        public async Task UpdateBlankAsync(Blank blank)
        {
            database.Blanks.Update(blank);
            await database.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            database.Contacts.Update(contact);
            await database.SaveChangesAsync();

        }

        public async Task UpdateUserEmailAsync(UserEmail userEmail)
        {
            database.UserEmails.Update(userEmail);
            await database.SaveChangesAsync();

        }
    }
}