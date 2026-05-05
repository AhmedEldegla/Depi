using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Entities.Shared;
using DEPI.Domain.Entities.Wallets;
using DEPI.Domain.Enums;
using DEPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DEPI.API.SeedData;

public static class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext db, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        try { await db.Database.EnsureCreatedAsync(); } catch { return; }

        try { await SeedRolesAsync(roleManager); } catch { }
        try { await SeedCurrenciesAsync(db); } catch { }
        try { await SeedCountriesAsync(db); } catch { }
        try { await SeedSkillsAsync(db); } catch { }
        try { await SeedConnectEarningRulesAsync(db); } catch { }
        try { await SeedUsersAsync(db, userManager); } catch { }
    }

    private static async Task SeedRolesAsync(RoleManager<Role> roleManager)
    {
        if (await roleManager.Roles.AnyAsync())
            return;

        var roles = new[]
        {
            ("Freelancer", "Freelancer role", false),
            ("Client", "Client role", false),
            ("Admin", "Administrator role", false),
            ("Student", "Student role", false),
            ("HeadHunter", "Head Hunter role", false),
            ("Coach", "Coach role", false)
        };

        foreach (var (name, desc, isDefault) in roles)
        {
            var role = Role.Create(name, desc, isDefault);
            await roleManager.CreateAsync(role);
        }
    }

    private static async Task SeedCurrenciesAsync(ApplicationDbContext db)
    {
        if (await db.Currencies.AnyAsync())
            return;

        var currencies = new[]
        {
            Currency.Create("EGP", "Egyptian Pound", "EG", isDefault: true),
            Currency.Create("USD", "US Dollar", "$"),
            Currency.Create("EUR", "Euro", "€"),
            Currency.Create("GBP", "British Pound", "£"),
            Currency.Create("SAR", "Saudi Riyal", "SR"),
            Currency.Create("AED", "UAE Dirham", "د.إ")
        };

        await db.Currencies.AddRangeAsync(currencies);
        await db.SaveChangesAsync();
    }

    private static async Task SeedCountriesAsync(ApplicationDbContext db)
    {
        if (await db.Countries.AnyAsync())
            return;

        var countries = new[]
        {
            Country.Create("مصر", "Egypt", "EG", "EGY", "+20"),
            Country.Create("السعودية", "Saudi Arabia", "SA", "SAU", "+966"),
            Country.Create("الإمارات", "United Arab Emirates", "AE", "ARE", "+971"),
            Country.Create("الأردن", "Jordan", "JO", "JOR", "+962"),
            Country.Create("الكويت", "Kuwait", "KW", "KWT", "+965"),
            Country.Create("قطر", "Qatar", "QA", "QAT", "+974"),
            Country.Create("البحرين", "Bahrain", "BH", "BHR", "+973"),
            Country.Create("عمان", "Oman", "OM", "OMN", "+968"),
            Country.Create("لبنان", "Lebanon", "LB", "LBN", "+961"),
            Country.Create("المغرب", "Morocco", "MA", "MAR", "+212"),
            Country.Create("الجزائر", "Algeria", "DZ", "DZA", "+213"),
            Country.Create("تونس", "Tunisia", "TN", "TUN", "+216"),
            Country.Create("العراق", "Iraq", "IQ", "IRQ", "+964"),
            Country.Create("سوريا", "Syria", "SY", "SYR", "+963"),
            Country.Create("اليمن", "Yemen", "YE", "YEM", "+967"),
            Country.Create("ليبيا", "Libya", "LY", "LBY", "+218"),
            Country.Create("السودان", "Sudan", "SD", "SDN", "+249"),
            Country.Create("فلسطين", "Palestine", "PS", "PSE", "+970")
        };

        await db.Countries.AddRangeAsync(countries);
        await db.SaveChangesAsync();
    }

    private static async Task SeedSkillsAsync(ApplicationDbContext db)
    {
        if (await db.Skills.AnyAsync())
            return;

        var skills = new[]
        {
            Skill.Create("تطوير الويب", "Web Development"),
            Skill.Create("تطوير تطبيقات الجوال", "Mobile App Development"),
            Skill.Create("تصميم جرافيك", "Graphic Design"),
            Skill.Create("تسويق رقمي", "Digital Marketing"),
            Skill.Create("كتابة المحتوى", "Content Writing"),
            Skill.Create("تحليل البيانات", "Data Analysis"),
            Skill.Create("الذكاء الاصطناعي", "Artificial Intelligence"),
            Skill.Create("تطوير الألعاب", "Game Development"),
            Skill.Create("تحرير الفيديو", "Video Editing"),
            Skill.Create("الشبكات والأمن", "Networking & Security"),
            Skill.Create("إدارة المشاريع", "Project Management"),
            Skill.Create("التجارة الإلكترونية", "E-Commerce"),
            Skill.Create("قواعد البيانات", "Databases"),
            Skill.Create("الحوسبة السحابية", "Cloud Computing"),
            Skill.Create("DevOps", "DevOps"),
            Skill.Create("UI/UX Design", "UI/UX Design"),
            Skill.Create("SEO", "SEO"),
            Skill.Create("Blockchain", "Blockchain"),
            Skill.Create("Machine Learning", "Machine Learning"),
            Skill.Create("Cyber Security", "Cyber Security")
        };

        await db.Skills.AddRangeAsync(skills);
        await db.SaveChangesAsync();
    }
    private static async Task SeedUsersAsync(ApplicationDbContext db, UserManager<User> userManager)
    {
        var users = new[]
        {
        // Ahmed Eldegla
        ("ahmed.client@depismart.com", "ahmed_client", "Ahmed", "Eldegla", UserType.Client, "Ahmed@123", "Client"),
        ("ahmed.freelancer@depismart.com", "ahmed_freelancer", "Ahmed", "Eldegla", UserType.Freelancer, "Ahmed@123", "Freelancer"),
        ("ahmed.admin@depismart.com", "ahmed_admin", "Ahmed", "Eldegla", UserType.Admin, "Ahmed@123", "Admin"),
        ("ahmed.student@depismart.com", "ahmed_student", "Ahmed", "Eldegla", UserType.Student, "Ahmed@123", "Student"),
        ("ahmed.hunter@depismart.com", "ahmed_hunter", "Ahmed", "Eldegla", UserType.HeadHunter, "Ahmed@123", "HeadHunter"),
        ("ahmed.coach@depismart.com", "ahmed_coach", "Ahmed", "Eldegla", UserType.Coach, "Ahmed@123", "Coach"),

        // Mahmoud Heggy
        ("mahmoud.client@depismart.com", "mahmoud_client", "Mahmoud", "Heggy", UserType.Client, "Mahmoud@123", "Client"),
        ("mahmoud.freelancer@depismart.com", "mahmoud_freelancer", "Mahmoud", "Heggy", UserType.Freelancer, "Mahmoud@123", "Freelancer"),
        ("mahmoud.admin@depismart.com", "mahmoud_admin", "Mahmoud", "Heggy", UserType.Admin, "Mahmoud@123", "Admin"),
        ("mahmoud.student@depismart.com", "mahmoud_student", "Mahmoud", "Heggy", UserType.Student, "Mahmoud@123", "Student"),
        ("mahmoud.hunter@depismart.com", "mahmoud_hunter", "Mahmoud", "Heggy", UserType.HeadHunter, "Mahmoud@123", "HeadHunter"),
        ("mahmoud.coach@depismart.com", "mahmoud_coach", "Mahmoud", "Heggy", UserType.Coach, "Mahmoud@123", "Coach"),

        // Hamsa Alaa
        ("hamsa.client@depismart.com", "hamsa_client", "Hamsa", "Alaa", UserType.Client, "Hamsa@123", "Client"),
        ("hamsa.freelancer@depismart.com", "hamsa_freelancer", "Hamsa", "Alaa", UserType.Freelancer, "Hamsa@123", "Freelancer"),
        ("hamsa.admin@depismart.com", "hamsa_admin", "Hamsa", "Alaa", UserType.Admin, "Hamsa@123", "Admin"),
        ("hamsa.student@depismart.com", "hamsa_student", "Hamsa", "Alaa", UserType.Student, "Hamsa@123", "Student"),
        ("hamsa.hunter@depismart.com", "hamsa_hunter", "Hamsa", "Alaa", UserType.HeadHunter, "Hamsa@123", "HeadHunter"),
        ("hamsa.coach@depismart.com", "hamsa_coach", "Hamsa", "Alaa", UserType.Coach, "Hamsa@123", "Coach"),

        // Fatma Hassan
        ("fatma.client@depismart.com", "fatma_client", "Fatma", "Hassan", UserType.Client, "Fatma@123", "Client"),
        ("fatma.freelancer@depismart.com", "fatma_freelancer", "Fatma", "Hassan", UserType.Freelancer, "Fatma@123", "Freelancer"),
        ("fatma.admin@depismart.com", "fatma_admin", "Fatma", "Hassan", UserType.Admin, "Fatma@123", "Admin"),
        ("fatma.student@depismart.com", "fatma_student", "Fatma", "Hassan", UserType.Student, "Fatma@123", "Student"),
        ("fatma.hunter@depismart.com", "fatma_hunter", "Fatma", "Hassan", UserType.HeadHunter, "Fatma@123", "HeadHunter"),
        ("fatma.coach@depismart.com", "fatma_coach", "Fatma", "Hassan", UserType.Coach, "Fatma@123", "Coach"),

        // Alyaa Yehia
        ("alyaa.client@depismart.com", "alyaa_client", "Alyaa", "Yehia", UserType.Client, "Alyaa@123", "Client"),
        ("alyaa.freelancer@depismart.com", "alyaa_freelancer", "Alyaa", "Yehia", UserType.Freelancer, "Alyaa@123", "Freelancer"),
        ("alyaa.admin@depismart.com", "alyaa_admin", "Alyaa", "Yehia", UserType.Admin, "Alyaa@123", "Admin"),
        ("alyaa.student@depismart.com", "alyaa_student", "Alyaa", "Yehia", UserType.Student, "Alyaa@123", "Student"),
        ("alyaa.hunter@depismart.com", "alyaa_hunter", "Alyaa", "Yehia", UserType.HeadHunter, "Alyaa@123", "HeadHunter"),
        ("alyaa.coach@depismart.com", "alyaa_coach", "Alyaa", "Yehia", UserType.Coach, "Alyaa@123", "Coach")
    };

        foreach (var (email, username, first, last, type, password, roleName) in users)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser != null)
                continue;

            var user = User.Create(
                email,
                username,
                first,
                last,
                type,
                Gender.Male,
                null,
                null,
                null
            );

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }

        await db.SaveChangesAsync();
    }
    private static async Task SeedConnectEarningRulesAsync(ApplicationDbContext db)
    {
        if (await db.ConnectEarningRules.AnyAsync())
            return;

        var rules = new[]
        {
            new ConnectEarningRule { Name = "Project Completed", Code = "PROJECT_COMPLETE", Description = "Earn connects when you complete a project successfully", ConnectsAwarded = 10, Trigger = EarningTrigger.ProjectCompleted, MaxPerDay = 5, DisplayOrder = 1 },
            new ConnectEarningRule { Name = "5-Star Review", Code = "FIVE_STAR", Description = "Earn connects when you receive a 5-star review", ConnectsAwarded = 5, Trigger = EarningTrigger.FiveStarReview, MaxPerDay = 3, DisplayOrder = 2 },
            new ConnectEarningRule { Name = "Fast Response", Code = "FAST_RESPONSE", Description = "Earn connects for responding to messages within 1 hour", ConnectsAwarded = 2, Trigger = EarningTrigger.FastResponse, MaxPerDay = 10, DisplayOrder = 3 },
            new ConnectEarningRule { Name = "Profile Completed", Code = "PROFILE_COMPLETE", Description = "Earn connects when you complete your profile to 100%", ConnectsAwarded = 15, Trigger = EarningTrigger.ProfileCompleted, MaxPerDay = 1, DisplayOrder = 4 },
            new ConnectEarningRule { Name = "Portfolio Published", Code = "PORTFOLIO_PUBLISH", Description = "Earn connects when you publish a portfolio item", ConnectsAwarded = 3, Trigger = EarningTrigger.PortfolioPublished, MaxPerDay = 5, DisplayOrder = 5 },
            new ConnectEarningRule { Name = "Daily Login", Code = "DAILY_LOGIN", Description = "Earn connects for logging in daily", ConnectsAwarded = 1, Trigger = EarningTrigger.DailyLogin, MaxPerDay = 1, DisplayOrder = 6 },
            new ConnectEarningRule { Name = "First Project Won", Code = "FIRST_PROJECT", Description = "Bonus connects for winning your first project", ConnectsAwarded = 20, Trigger = EarningTrigger.FirstProjectWon, MaxPerDay = 1, DisplayOrder = 7 },
            new ConnectEarningRule { Name = "Guild Joined", Code = "GUILD_JOINED", Description = "Earn connects when you join a Digital Guild", ConnectsAwarded = 5, Trigger = EarningTrigger.GuildJoined, MaxPerDay = 3, DisplayOrder = 8 },
            new ConnectEarningRule { Name = "Course Completed", Code = "COURSE_COMPLETE", Description = "Earn connects when you complete a learning course", ConnectsAwarded = 8, Trigger = EarningTrigger.CourseCompleted, MaxPerDay = 3, DisplayOrder = 9 },
            new ConnectEarningRule { Name = "Certification Earned", Code = "CERT_EARNED", Description = "Earn connects when you earn a certification", ConnectsAwarded = 10, Trigger = EarningTrigger.CertificationEarned, MaxPerDay = 2, DisplayOrder = 10 },
            new ConnectEarningRule { Name = "7-Day Streak", Code = "STREAK_7", Description = "Bonus connects for 7 consecutive days of activity", ConnectsAwarded = 25, Trigger = EarningTrigger.Streak7Days, MaxPerDay = 1, DisplayOrder = 11 }
        };

        await db.ConnectEarningRules.AddRangeAsync(rules);
        await db.SaveChangesAsync();
    }
}
