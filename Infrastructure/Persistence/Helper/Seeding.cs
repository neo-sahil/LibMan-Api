using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Application.EntityDto.UserDto;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Persistence.Context;

namespace Persistence.Helper
{
    public static class Seeding
    {
        public static void SeedTenant(TenantDbContext context, IConfiguration configuration)
        {
            try
            {
                if(context.Tenants.Any()) return;
                var tenantData = configuration.GetSection("Tenants").Get<List<Tenant>>();
                if(tenantData != null && tenantData.Count > 0)
                {
                    context.Tenants.AddRangeAsync(tenantData);
                }
            }
            catch(Exception ex)
            {

            }
            context.SaveChanges();
        }

        public static void SeedUserProfile(ApplicationDbContext context, IConfiguration config)
        {
            try
            {
                if (context.UserProfiles.Any()) return;

                // var tenantData = await System.IO.File.ReadAllTextAsync("D:/Projects/.Net 6'/OnionAcchitecture/Persistence/Data/UserSeedData.json");

                var userProfileData = config.GetSection("UserProfiles").Get<List<UserProfile>>();

                if (userProfileData != null && userProfileData.Count > 0)
                {
                    context.UserProfiles.AddRange(userProfileData);
                }

            }
            catch (Exception ex)
            {

            }

            context.SaveChanges();

        }

        public static void SeedUsers(ApplicationDbContext context, IConfiguration config)
        {
            try
            {
                if (context.Users.Any()) return;

                var userData = config.GetSection("Users").Get<List<AddUserDto>>();



                foreach(var users in userData)
                {
                    Seeding.CreatePasswodHash(users.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    var user = new User()
                    {
                        Username = users.Username,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Usercode = users.Usercode.ToLower(),
                        UserProfileId = users.UserProfileId,
                        photo = ""
                    };
                    context.Users.Add(user);
                }
            }
            catch (Exception ex)
            {

            }

            context.SaveChanges();

        }

        public static void CreatePasswodHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}