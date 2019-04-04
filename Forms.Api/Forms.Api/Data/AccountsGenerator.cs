﻿using Forms.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Forms.Api.Data
{
    public class AccountsGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AccountDbContext(
                    serviceProvider.GetRequiredService<DbContextOptions<AccountDbContext>>()))
            {
                if (context.Accounts.Any())
                    return;

                var accounts = new List<Account>
                {
                    new Account
                    {
                        FirstName = "Frank",
                        LastName = "Fruitfly",
                        Address = "123 Palm Avenue, Lynnwood, Pretoria, 0015, South Africa",
                        DateOfBirth = new DateTime(1985,01,15),
                        IdPassport = "9012171234567",
                        ProfileImageBase64 = GetImageBase64String("doctor.png"),
                        DateTimeStamp = new DateTime(2000,01,01)
                    },
                    new Account
                    {
                        FirstName = "Sally",
                        LastName = "Shulerz",
                        Address = "58 Rickson Drive, Centurion, Pretoria, 0014, South Africa",
                        DateOfBirth = new DateTime(1992,04,20),
                        IdPassport = "9012171234568",
                        ProfileImageBase64 = GetImageBase64String("seduca.png"),
                        DateTimeStamp = new DateTime(2000,01,02)
                    },
                };

                context.Accounts.AddRange(accounts);
                context.SaveChanges();
            }
        }

        private static string GetImageBase64String(string fileName)
        {
            var path = $"{Environment.CurrentDirectory}/Content/{fileName}";

            byte[] imageArr = File.ReadAllBytes(path);

            return Convert.ToBase64String(imageArr);
        }

    }
}