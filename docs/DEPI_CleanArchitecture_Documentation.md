# DEPI Smart Freelance Platform
## Clean Architecture Documentation
### شرح كامل للـ Architecture والكود

---

## Table of Contents

1. [لية نستخدم Clean Architecture؟](#لية-نستخدم-clean-architecture)
2. [Module 01 - Identity (المستخدمين والنظام)](#module-01---identity-المستخدمين-والنظام)
3. [Module 02 - References (البيانات المرجعية)](#module-02---references-البيانات-المرجعية)
4. [Module 03 - Profiles (بيانات إضافية للمستخدم)](#module-03---profiles-بيانات-إضافية-للمستخدم)
5. [Module 04 - Companies (منظمة الشركات)](#module-04---companies-منظمة-الشركات)
6. [Module 05 - Freelancers (المهارات والـ Portfolio)](#module-05---freelancers-المهارات-والـ-portfolio)
7. [Module 06 - Projects (المشاريع والعقود)](#module-06---projects-المشاريع-والعقود)
8. [Module 07 - Payments (المحفظة والـ Connects)](#module-07---payments-المحفظة-والـ-connects)
9. [Module 08 - Recruitment (التوظيف والـ Head Hunters)](#module-08---recruitment-التوظيف-والـ-head-hunters)
10. [Module 09 - Communication (المحادثات والإشعارات)](#module-09---communication-المحادثات-والإشعارات)
11. [Module 10 - Learning (الدورات التعليمية)](#module-10---learning-الدورات-التعليمية)
12. [Module 11 - AI (الذكاء الاصطناعي)](#module-11---ai-الذكاء-الاصطناعي)
13. [Module 12 - Guilds (نظام النقابات الرقمية)](#module-12---guilds-نظام-النقابات-الرقمية)
14. [Module 13 - Students (نظام الطلاب)](#module-13---students-نظام-الطلاب)
15. [Module 14 - Coaching (نظام التدريب الشخصي)](#module-14---coaching-نظام-التدريب-الشخصي)
16. [Base Entities - الأساس](#base-entities---الأساس)
17. [Shared Interfaces](#shared-interfaces)
18. [Infrastructure Layer](#infrastructure-layer)
19. [API Layer](#api-layer)
20. [Flow Diagram](#flow-diagram)
21. [ملخص Decisions](#ملخص-decisions)

---

## لية نستخدم Clean Architecture؟

### المشكلة بدون architecture:

```
❌ كود مخلوط - كل حاجة مع بعض
├── UserController.cs (HTTP + Logic + Database)
├── Validation + Business Rules + SQL Queries
└── النتيجة: كود فوضوي صعب الصيانة
```

### الحل مع Clean Architecture:

```
✅ كود مفصول - كل حاجة في مكانها
├── Domain (Business Logic - عقل المشروع)
├── Application (Use Cases - العمليات)
├── Infrastructure (Database - التخزين)
└── API (HTTP - التواصل)
```

### Benefits:

```
1. Separation of Concerns (فصل الاهتمامات)
   - كل Layer ليها responsibility واحدة
   
2. Testability (قابلية الاختبار)
   - نقدر نtest كل layer لوحده
   
3. Maintainability (قابلية الصيانة)
   - تغيير في layer معينة = مش بيتأثر على الباقي
   
4. Scalability (قابلية التوسع)
   - سهل نضيف features جديدة
   
5. Dependency Inversion
   - الـ Domain مش بdepends على أي حاجة
   - Infrastructure بdepends على Domain
```

---

## Module 01 - Identity (المستخدمين والنظام)

### لية بنحتاج Identity Module؟

```
عندك منصة freelance = لازم تعرف مين اللي بيشتغل ومين اللي بيدفع

المستخدم ممكن يكون:
├── Freelancer (شغال)
├── Client (بيدفع)
├── CompanyOwner (صاحب شركة)
├── HeadHunter (موظف توظيف)
├── Instructor (مدرب)
├── Admin (إدارة المنصة)
└── Student (طالب)
```

### User.cs - مستخدم واحد لكل الأدوار

```csharp
public class User : BaseEntity
{
    // البيانات الأساسية
    public string Email { get; set; }           // لازم يكون unique - login identifier
    public string PasswordHash { get; set; }     // مش هنخزن الباسورد plain text - للأمان
    public string FirstName { get; set; }        // الاسم الأول
    public string LastName { get; set; }         // الاسم الأخير
    
    // البيانات الاختيارية
    public string? PhoneNumber { get; set; }     // رقم تليفون (nullable لأن مش كل المستخدمين بيدوه)
    public DateTime? BirthDate { get; set; }    // تاريخ الميلاد (nullable)
    public string? ProfileImageUrl { get; set; } // صورة شخصية
    
    // حالة الحساب
    public bool IsActive { get; set; }           // هل الحساب مفعل؟ false = banned/deleted
    public bool EmailConfirmed { get; set; }    // هل أكد البريد الإلكتروني؟
    
    // النوع - Enum = userType_id في DB
    public UserType UserType { get; set; }      // أيه نوع المستخدم؟ freelancer/client/admin
    
    // Relationships
    public UserProfile? Profile { get; set; }    // FK => One-to-One relationship
    public ICollection<Session> Sessions { get; set; }  // FK => One-to-Many (user يقدر يسجل دخول من multiple devices)
}
```

**لية بنفصل User عن UserProfile؟**

```
1. Single Responsibility
   - User = بيانات تسجيل الدخول (Email, Password)
   - Profile = بيانات عامة (Bio, Headline, HourlyRate)
   
2. Performance
   - مش كل مستخدم ليه profile كامل
   - Login queries = faster (من غير join مع profile)
   
3. Security
   - User table = sensitive data (Password)
   - Profile = public data
   - بنخليهم في tables منفصلة للأمان
```

### UserType.cs - Enum

```csharp
public enum UserType
{
    Freelancer = 1,    // = 1 in DB (هيسهل الاستعلامات)
    Client = 2,
    CompanyOwner = 3,
    Instructor = 4,
    HeadHunter = 5,
    Admin = 6,
    Student = 7
}
```

**لية بنستخدم enum بدلاً من جدول منفصل في DB؟**

```
✅ Fast: Enum = int في DB = أسرع من JOIN مع جدول
✅ Type Safety: لو كتبت UserType.Client = صح، لو كتبت "client" ممكن تغلط
✅ Compile-time checking: الـ Compiler يcaught الأخطاء قبل ما البرنامج يشتغل
✅ Simplicity: عدد قليل من الخيارات (7 أنواع)

⚠️ مش هينفع لو البيانات بتتغير باستمرار (زي Countries)
   - الدول ممكن تتغير/تتضاف = لازم جدول مستقل
   - أنواع المستخدمين = rarely تتغير = enum OK
```

### Session.cs - تتبع جلسات الدخول

```csharp
public class Session : BaseEntity
{
    public Guid UserId { get; set; }            // FK => مين اللي سجل الدخول؟
    public string RefreshToken { get; set; }     // Token عشوائي للتعريف بالجلسة
    public string? DeviceInfo { get; set; }     // "iPhone 13" or "Chrome on Windows"
    public string? IpAddress { get; set; }      // 192.168.1.1 - للأمان
    public DateTime CreatedAt { get; set; }     // امتى بدأت الجلسة
    public DateTime? ExpiresAt { get; set; }    // امتى هتنتهي (nullable = infinite)
    public bool IsRevoked { get; set; }         // لو true = المستخدم عمل logout
}
```

**لية بنحتاج Session entity؟**

```
بدون Sessions:
❌ Access Token هيسري للأبد
❌ لو حد سرق token = يقدر يستخدمه للأبد
❌ مش هينفع تعمل "logout from all devices"
❌ مش هينفع تعمل "logout from specific device"

مع Sessions:
✅ كل session ليها تاريخ انتهاء
✅ تقدر تعمل revoke لأي session
✅ لو سرقت token = تعمله revoke = غير صالح
✅ أمان أعلى
✅ لو عملت "Sign out from all devices" = revoke كل sessions
✅ لو عملت "Sign out from this device" = revoke session واحدة
```

### SecurityLog.cs - سجل الأمان

```csharp
public class SecurityLog : BaseEntity
{
    public Guid UserId { get; set; }            // FK => مين اللي الحدث ده؟
    public string EventType { get; set; }       // "LOGIN_SUCCESS", "LOGIN_FAILED", "PASSWORD_CHANGED"
    public string? IpAddress { get; set; }      // منين جاي؟
    public string? DeviceInfo { get; set; }     // بستخدم أي جهاز؟
    public string? UserAgent { get; set; }      // Browser info كامل
    public string? Details { get; set; }        // JSON string للأحداث الإضافية
    public DateTime CreatedAt { get; set; }     // امتى الحدث؟
}
```

**الية بنحتاج SecurityLog؟**

```
✅ Forensic Analysis: لو حد اخترق الحساب = نقدر نفهم إيه اللي حصل
✅ Fraud Detection: لو في login من مكان غريب = نكتشفه
✅ Compliance: بعض القوانين بتطلب سجلات أمان (SOC2, GDPR)
✅ Debugging: لو في مشكلة = نفهم إيه اللي حصل
✅ Monitoring: نقدر نعمل dashboards للأمان
```

### Role.cs و Permission.cs - نظام الصلاحيات

```csharp
// Role = Job Title (المسمى الوظيفي)
public class Role : BaseEntity
{
    public string Name { get; set; }            // "Admin", "Freelancer", "Client"
    public string? Description { get; set; }
    
    public ICollection<Permission> Permissions { get; set; }  // Many-to-Many
    public ICollection<UserRole> UserRoles { get; set; }     // Many-to-Many
}

// Permission = Job Description (المهام المحددة)
public class Permission : BaseEntity
{
    public string Name { get; set; }             // "users.create", "projects.delete"
    public string? Description { get; set; }
    public string Module { get; set; }           // "Users", "Projects", "Payments"
    
    public ICollection<Role> Roles { get; set; } // Many-to-Many
}
```

**لية بنفصل Role عن Permission؟**

```
بدون فصل:
❌ Role = "Admin" => ليه صلاحيات ثابتة
❌ لو عايز تضيف permission جديدة = هتغير الكود
❌ لو عايز تاخد permission من role = لازم كود

مع فصل:
✅ Role = مجموعة من Permissions
✅ مثلاً:
   - Role "Senior Freelancer" = [projects.create, proposals.submit, payments.withdraw]
   - Role "Junior Freelancer" = [projects.create, proposals.submit] (بدون withdraw)
   - Role "Admin" = [users.manage, projects.manage, payments.manage]
✅ تغيير الصلاحيات = إضافة/إزالة permissions من الـ Role
✅ مرونة أعلى بدون كود
✅ نقدر نعرض لuser = أنت عندك الصلاحيات دي
```

### UserRole.cs و RolePermission.cs - Many-to-Many

```csharp
// UserRole: مستخدم واحد = عدة أدوار (Freelancer + Student مثلاً)
public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    
    public User User { get; set; }  // Navigation Property
    public Role Role { get; set; }
}

// RolePermission: دور واحد = عدة صلاحيات
public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    
    public Role Role { get; set; }
    public Permission Permission { get; set; }
}
```

**لية بنستخدم intermediary table (Join Table)؟**

```
✅ Many-to-Many relationship = بنحتاج جدول وسيط
✅ بيسمح لنا نضيف metadata إضافية:
   - مثلاً: CreatedAt (امتى اتضاف الـ role للuser)
   - AssignedBy (مين اللي عمل الـ assignment)
✅ Entity Framework = بيعرف يتعامل معاه تلقائياً
✅ بنستخدم Fluent API لتحديد الـ relationships
```

---

## Module 02 - References (البيانات المرجعية)

### لية بنحتاج References Module؟

```
بعض البيانات ثابته مش بتتغير كتير:
├── Countries (مصر، السعودية، الإمارات...) - ممكن تتغير بس rarely
├── Currencies (USD، EUR، EGP...)
└── Categories (Programming, Design, Marketing...)

⚠️ بنعملهم ك entities مش enums لأنهم:
   - كتير (200+ country)
   - ممكن يتغيروا (دولة جديدة، عملة جديدة)
   - محتاجين نقدر نضيفهم من الـ dashboard
```

### Country.cs

```csharp
public class Country : BaseEntity
{
    public string Name { get; set; }             // "Egypt"
    public string Code { get; set; }             // "EG" (2 letters ISO)
    public string? FlagUrl { get; set; }         // 🏳️ صورة العلم
    public string? PhoneCode { get; set; }       // "+20" (للphone number validation)
}
```

### Currency.cs

```csharp
public class Currency : BaseEntity
{
    public string Name { get; set; }             // "US Dollar"
    public string Code { get; set; }             // "USD"
    public string Symbol { get; set; }           // "$"
    public decimal ExchangeRateToUSD { get; set; } // 1.0 (للتحويل)
    public bool IsActive { get; set; }           // هل متاح حالياً؟
}
```

### Category.cs

```csharp
public class Category : BaseEntity
{
    public string Name { get; set; }             // "Web Development"
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; } // FK => Self-referencing (Web Dev -> Frontend/Backend)
    public string? IconUrl { get; set; }
    public int DisplayOrder { get; set; }        // ترتيب الظهور
}
```

**لية Country و ليس enum؟**

```
Countries = 195+ country
❌ لو enum = كل country جديد = كود جديد + rebuild
✅ لو entity = أي حد يقدر يضيف country من الـ dashboard

مش كل حاجة ثابته لازم تكون enum:
- Countries, Categories, Skills, Currencies => entities (many, may change)
- UserType, ProjectStatus, PaymentStatus => enums (few options, rarely change)
```

---

## Module 03 - Profiles (بيانات إضافية للمستخدم)

### لية بنفصل Profile عن User؟

```
✅ Single Responsibility: 
   - User = بيانات تسجيل الدخول
   - Profile = بيانات عامة

✅ Performance: 
   - مش كل مستخدم ليه profile كامل
   - Login = من غير join

✅ Flexibility: 
   - نقدر نضيف fields كتير للprofile من غير ما نغير User

✅ Clean: 
   - لو مسحنا profile = مش لازم نمحي الحساب
```

### UserProfile.cs

```csharp
public class UserProfile : BaseEntity
{
    public Guid UserId { get; set; }             // FK => One-to-One مع User
    public string? Bio { get; set; }             // نبذة شخصية
    public string? Headline { get; set; }        // "Senior Full-Stack Developer"
    public string? Summary { get; set; }         // CV summary
    public decimal? HourlyRate { get; set; }     // السعر بالساعة (nullable)
    
    // الموقع
    public Guid? CountryId { get; set; }        // FK => منين User؟
    public string? City { get; set; }            // "Cairo"
    public string? Address { get; set; }        // عنوان تفصيلي
    public string? Timezone { get; set; }        // "Africa/Cairo"
    
    // التوفر
    public int? Availability { get; set; }      // 1-100: متاح قد إيه
    
    // Relationships
    public User User { get; set; }
    public Country? Country { get; set; }
}
```

### UserSetting.cs - إعدادات المستخدم

```csharp
public class UserSetting : BaseEntity
{
    public Guid UserId { get; set; }
    public string Key { get; set; }              // "email_notifications"
    public string Value { get; set; }           // "true" / "false"
}
```

**لية بنستخدم Key-Value settings بدل fixed columns؟**

```
بدل:
❌ columns: EmailNotif, PushNotif, SmsNotif, WeeklyDigest, Marketing...
❌ لو عايز أضيف setting جديدة = لازم migration
❌ Fixed structure

✅ Key-Value:
   - "email_notifications" = "true"
   - "push_notifications" = "true"
   - "weekly_digest" = "false"
   - "marketing_emails" = "true"

✅ Benefits:
   - Flexible: أي حد يقدر يضيف setting جديدة من غير migration
   - Extensible: third-party plugins تقدر تضيف settings
   - Scalable: بنضيف column واحدة = JSON أو Key-Value
   - Dynamic: settings بتتغير constant = Key-Value أفضل
```

### UserLink.cs - روابط المستخدم

```csharp
public class UserLink : BaseEntity
{
    public Guid UserId { get; set; }
    public LinkType Type { get; set; }           // enum: Website=1, LinkedIn=2, GitHub=3
    public string Url { get; set; }              // "https://github.com/username"
    public int DisplayOrder { get; set; }        // ترتيب الظهور
}
```

---

## Module 04 - Companies (منظمة الشركات)

### لية بنحتاج Companies Module؟

```
Freelancer = شغال لوحده
Company Owner = عنده فريق

Company = 
├── Members (أعضاء الفريق)
├── Artifacts (مستندات، شهادات، ملفات)
└── Projects (المشاريع الخاصة بالشركة)
```

### Company.cs

```csharp
public class Company : BaseEntity
{
    public string Name { get; set; }             // "TechCorp Solutions"
    public string? LogoUrl { get; set; }
    public string? Description { get; set; }
    public string? Website { get; set; }
    public Guid OwnerId { get; set; }            // FK => مين اللي أسس الشركة؟
    public int TeamSize { get; set; }           // 10, 50, 100+
    public string? Industry { get; set; }        // "Technology", "Finance"
    public bool IsVerified { get; set; }         // هل الشركة موثقة؟
    
    public ICollection<CompanyMember> Members { get; set; }
    public ICollection<CompanyArtifact> Artifacts { get; set; }
}
```

### CompanyMember.cs

```csharp
public class CompanyMember : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; }             // "CTO", "Developer", "Designer"
    public CompanyMemberStatus Status { get; set; }
    public DateTime JoinedAt { get; set; }
}
```

---

## Module 05 - Freelancers (المهارات والـ Portfolio)

### لية بنحتاج Freelancers Module؟

```
من غير Freelancer module:
❌ مش هنعرف freelancer عنده أي مهارات
❌ مش هنعرف يعرض أعماله (Portfolio)
❌ مش هنعرف يحدد أسعاره
❌ مش هنعرف يعرض خبرته

مع Freelancer module:
✅ Skill matching: ناقص Python = نقترح عليه Python courses
✅ Portfolio: يعرض شغله
✅ Service packages: يبيع خدمات ثابتة السعر
✅ Experience tracking: تاريخ الخبرات
```

### Freelancer.cs

```csharp
public class Freelancer : BaseEntity
{
    public Guid UserId { get; set; }             // FK => One-to-One مع User
    public Guid? PrimarySkillId { get; set; }   // FK => المهارة الأساسية
    public int? YearsOfExperience { get; set; } // 5 years
    public decimal? EarningBalance { get; set; } // أرباحه المتاحة
    public decimal? TotalEarnings { get; set; } // كل أرباحه من أول يوم
    public string? AvailableUntil { get; set; } // متاح لحد امتى؟ "2024-12-31"
    public bool IsAvailable { get; set; }       // متاح دلوقتي؟
    
    public ICollection<FreelancerSkill> Skills { get; set; }
    public ICollection<PortfolioItem> PortfolioItems { get; set; }
}
```

### Skill.cs و FreelancerSkill.cs

```csharp
// Skill = الفئة العامة (مثل "Python")
public class Skill : BaseEntity
{
    public string Name { get; set; }             // "Python"
    public string? Description { get; set; }
    public Guid? ParentSkillId { get; set; }     // FK => Self-referencing (Python -> Backend)
    public string? IconUrl { get; set; }
    public bool IsVerified { get; set; }        // هل موثق من المنصة؟
    
    public ICollection<FreelancerSkill> FreelancerSkills { get; set; }
}

// FreelancerSkill = علاقة Many-to-Many مع metadata
public class FreelancerSkill : BaseEntity
{
    public Guid FreelancerId { get; set; }
    public Guid SkillId { get; set; }
    public SkillLevel Level { get; set; }       // enum: Beginner=1, Intermediate=2, Expert=3
    public int? YearsOfExperience { get; set; } // خبرته في الـ skill دي
    
    // Relationships
    public Freelancer Freelancer { get; set; }
    public Skill Skill { get; set; }
}

// SkillLevel enum
public enum SkillLevel
{
    Beginner = 1,
    Intermediate = 2,
    Expert = 3
}
```

**لية بنستخدم FreelancerSkill entity بدل ICollection<int>؟**

```
✅ Metadata: نقدر نحدد مستواه في كل skill
   - Skill "Python" + Level = Expert + Years = 10
   
✅ Validation: نقدر نحدد minimum level للproject معين
   - Project requires Expert in Python
   - FreelancerLevel = Beginner -> Not eligible
   
✅ AI Matching: الـ AI يقدر يحسب score بناءً على level + years
   - Score = (Level * 0.6) + (Years * 0.4)
   
✅ Flexibility: لو skill جديد = نقدر نضيف metadata مختلفة
```

### PortfolioItem.cs

```csharp
public class PortfolioItem : BaseEntity
{
    public Guid FreelancerId { get; set; }      // FK => صاحب الـ portfolio
    public string Title { get; set; }            // "E-commerce Website"
    public string? Description { get; set; }
    public string? LiveUrl { get; set; }         // "https://shop.com"
    public string? RepoUrl { get; set; }         // "https://github.com/..."
    public int ViewCount { get; set; }          // كام مرة اتشاف؟
    public bool IsFeatured { get; set; }        // يظهر في الـ profile؟
    public int? Placement { get; set; }          // ترتيب الظهور
    
    public ICollection<MediaFile> MediaFiles { get; set; }
}
```

### ServicePackage.cs

```csharp
public class ServicePackage : BaseEntity
{
    public Guid FreelancerId { get; set; }
    public string Name { get; set; }             // "Basic", "Standard", "Premium"
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int DeliveryDays { get; set; }
    public int Revisions { get; set; }          // كم مراجعة مسموحة؟
    public ServicePackageStatus Status { get; set; }
}
```

---

## Module 06 - Projects (المشاريع والعقود)

### لية بنحتاج Projects Module؟

```
Client = عنده فكرة => ينشئ Project
Freelancer = شايف Project => يبعث Proposal
Client = وافق على Proposal => ينشئ Contract
Contract = فيه Milestones (مراحل)
Milestone = فيه Deliverables (مخرجات)
```

### Project.cs

```csharp
public class Project : BaseEntity
{
    public Guid ClientId { get; set; }           // FK => مين اللي نشر المشروع؟
    public string Title { get; set; }            // "Build E-commerce Website"
    public string? Description { get; set; }
    public ProjectType Type { get; set; }        // enum: FixedPrice=1, Hourly=2
    public ProjectStatus Status { get; set; }    // enum: Draft=1, Active=2, InProgress=3...
    
    // الميزانية
    public decimal BudgetMin { get; set; }       // 500
    public decimal BudgetMax { get; set; }       // 1000
    public string? BudgetCurrency { get; set; }  // "USD"
    
    // التواريخ
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public int ProposalsCount { get; set; }      // عدد الـ proposals
    
    public ICollection<Proposal> Proposals { get; set; }
    public ICollection<ProjectAttachment> Attachments { get; set; }
}

// Enums
public enum ProjectType
{
    FixedPrice = 1,    // سعر ثابت للمشروع كله
    Hourly = 2         // بالساعة
}

public enum ProjectStatus
{
    Draft = 1,
    Active = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5,
    Closed = 6
}
```

**لية بنستخدم BudgetMin و BudgetMax بدل Budget واحد؟**

```
✅ Realistic: Client ممكن يكون مرن ("between 500-1000")
✅ Flexibility: Freelancer يقدر يعرض سعر بين المدى ده
✅ Negotiation: مساحة للتفاوض
✅ Fairness: Client و Freelancer = كلهم مرنين
```

### Proposal.cs

```csharp
public class Proposal : BaseEntity
{
    public Guid ProjectId { get; set; }          // FK => على أي مشروع؟
    public Guid FreelancerId { get; set; }       // FK => مين اللي بعت العرض؟
    public string CoverLetter { get; set; }       // "I am interested in..."
    public decimal ProposedAmount { get; set; }  // 750
    public int DurationInDays { get; set; }      // 30 days
    public ProposalStatus Status { get; set; }   // enum: Pending=1, Shortlisted=2...
    
    public ICollection<Milestone> Milestones { get; set; }
}

public enum ProposalStatus
{
    Pending = 1,
    Shortlisted = 2,
    InterviewScheduled = 3,
    Accepted = 4,
    Rejected = 5,
    Withdrawn = 6
}
```

### Contract.cs و Milestone.cs

```csharp
public class Contract : BaseEntity
{
    public Guid ProjectId { get; set; }          // FK
    public Guid ProposalId { get; set; }         // FK => أي proposal اختاره Client
    public Guid FreelancerId { get; set; }       // FK
    public Guid ClientId { get; set; }           // FK
    public ContractStatus Status { get; set; }   // enum: Active=1, Completed=2...
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal TotalAmount { get; set; }      // المبلغ الكلي
    public decimal PlatformFee { get; set; }     // عمولة المنصة (8-10%)
}

public class Milestone : BaseEntity
{
    public Guid ContractId { get; set; }          // FK => أي contract؟
    public string Title { get; set; }             // "Design Phase"
    public string? Description { get; set; }
    public decimal Amount { get; set; }           // 250
    public MilestoneStatus Status { get; set; }   // enum: Pending=1, InProgress=2...
    public int OrderIndex { get; set; }           // ترتيب المرحلة
    public DateTime? DueDate { get; set; }        // آخر date للتسليم
    
    public ICollection<Deliverable> Deliverables { get; set; }
}
```

**لية بنستخدم Milestones؟**

```
✅ Chunks: المشروع = 10000$ = لازم يتقسم مراحل
✅ Accountability: لو freelancer تأخر = نوقف عند المرحلة دي
✅ Payment: كل مرحلة ليها payment منفصل
✅ Risk management: Client = مش هيدفع كل المبلغ قبل ما يشوف شغل
✅ Dispute resolution: لو في مشكلة = بنوقف milestone معينة
```

---

## Module 07 - Payments (المحفظة والـ Connects)

### لية بنحتاج Payments Module؟

```
❌ بدون نظام دفع:
   - Freelancer = مش هيعرف يستقبل فلوس
   - Client = مش هيعرف يدفع
   - المنصة = مش هنقدر ناخد commission

✅ مع نظام دفع:
   - Wallet = محفظة كل مستخدم
   - Transactions = سجل كل حركة مالية
   - Connects = نظام رصيد للمنصة
```

### Wallet.cs

```csharp
public class Wallet : BaseEntity
{
    public Guid UserId { get; set; }             // FK => One-to-One مع User
    public decimal Balance { get; set; }         // الرصيد الحالي
    public decimal PendingBalance { get; set; }  // رصيد قيد الانتظار
    public string Currency { get; set; }         // "USD", "EGP"
    public bool IsVerified { get; set; }         // هل الحساب البنكي متصل؟
}
```

**لية بنفصل PendingBalance عن Balance؟**

```
✅ User يطلب 500$ withdrawal:
   - Balance: 1000$ => 500$ (pending)
   - PendingBalance: 500$
   - Balance: 500$ (available)

✅ Benefits:
   - User يشوف رصيده الكلي vs اللي يقدر يصرفه
   - Protection: لو الـ milestone cancel = الـ pending بيرجع
   - Transparency: واضح إيه اللي في الطريق
```

### ConnectSystem

```csharp
public class ConnectPackage : BaseEntity
{
    public string Name { get; set; }              // "Basic", "Plus", "Premium"
    public int ConnectCount { get; set; }        // 10, 50, 100
    public decimal Price { get; set; }           // 10$, 40$, 70$
    public bool IsActive { get; set; }
}

public class UserConnect : BaseEntity
{
    public Guid UserId { get; set; }
    public int TotalConnects { get; set; }       // الرصيد الكلي
    public int UsedConnects { get; set; }        // اللي استخدمة
    public int RemainingConnects { get; set; }   // المتبقي
}

public class ConnectTransaction : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid? ConnectPackageId { get; set; }   // FK => لو اشتري package
    public TransactionType Type { get; set; }    // enum: Purchase=1, Used=2, Refund=3
    public int ConnectAmount { get; set; }       // +10 or -1
    public decimal Amount { get; set; }          // السعر المدفوع
    public Guid? ProjectId { get; set; }         // FK => لو استخدم على مشروع
}
```

**اية هي الـ Connects؟**

```
📌 نظام Credit داخلي = زي "عملة المنصة"

📌 Client لازم يشتري Connects عشان يبعت proposal على project

📌 Benefits:
   - Reduce spam: لو كل proposal بتكلف connect = مش هتبعت proposals كتير
   - Revenue: المنصة بتكسب من بيع الـ connects
   - Quality: proposals أكتر quality (لأن فيه تكلفة)
   - Engagement: users يفضلوا على المنصة عشان يستخدموا الـ connects

📌 Commission: 8-10% على كل transaction

📌 Example:
   - Connect Package: 10 connects = 10$
   - Client يشتري = 10 connects
   - Client يبعت proposal = -1 connect
   - لو proposal rejected = +1 connect (refund)
```

---

## Module 08 - Recruitment (التوظيف والـ Head Hunters)

### HeadHunter.cs

```csharp
public class HeadHunter : BaseEntity
{
    public Guid UserId { get; set; }             // FK => One-to-One مع User
    public string CompanyName { get; set; }      // "Talent Scouts LLC"
    public string? Specialization { get; set; }  // "Tech Recruitment"
    public int SuccessfulPlacements { get; set; } // كام واحد وظف بنجاح
    public decimal CommissionRate { get; set; }  // نسبة العمولة
    public decimal TotalEarnings { get; set; }   // أرباحه الكلية
}
```

**لية بنحتاج Head Hunters؟**

```
✅ Companies محتاجة employees = مش freelance
✅ Head Hunter = متخصص في التوظيف = بيوصل talent للشركات
✅ Commission: لو وظف حد = ياخد نسبة من salary

✅ Benefits للشركات:
   - 专业: Head Hunters عندهم network كبير
   - Speed: توظيف أسرع من البحث لوحدك
   - Quality: بيختاروا candidates مناسبين
   - Time saving: الشركات مش هتقضي وقت في Recruitment
```

### JobPost.cs و JobApplication.cs

```csharp
public class JobPost : BaseEntity
{
    public Guid HeadHunterId { get; set; }       // FK => مين اللي نشر؟
    public string Title { get; set; }            // "Senior Software Engineer"
    public string? Description { get; set; }
    public JobType Type { get; set; }            // enum: FullTime=1, PartTime=2...
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string? Location { get; set; }         // "Remote", "Cairo, Egypt"
    public JobPostStatus Status { get; set; }    // enum
    public DateTime? ExpiresAt { get; set; }     // آخر date للتقديم
}

public class JobApplication : BaseEntity
{
    public Guid JobPostId { get; set; }          // FK
    public Guid FreelancerId { get; set; }       // FK
    public string? CoverLetter { get; set; }
    public string? ResumeUrl { get; set; }
    public JobApplicationStatus Status { get; set; }
}
```

---

## Module 09 - Communication (المحادثات والإشعارات)

### لية بنحتاج Communication Module؟

```
بدون communication:
❌ Freelancer و Client = لازم يتواصلوا خارج المنصة
❌ مش في record للمحادثات = لو في dispute = مش هنعرف الحقيقة
❌ المنصة = losing engagement

مع communication:
✅ Chat = كل حاجة داخل المنصة
✅ Notifications = يوصلوا messages
✅ Community = Social aspect = users يفضلوا on المنصة
```

### Conversation.cs و Message.cs

```csharp
public class Conversation : BaseEntity
{
    public ConversationType Type { get; set; }  // enum: Direct=1, Group=2
    public string? Title { get; set; }           // لو group chat
    public DateTime LastMessageAt { get; set; } // للأ先进性 (آخر رسالة)
    
    public ICollection<ConversationParticipant> Participants { get; set; }
    public ICollection<Message> Messages { get; set; }
}

public class ConversationParticipant : BaseEntity
{
    public Guid ConversationId { get; set; }
    public Guid UserId { get; set; }
    public DateTime JoinedAt { get; set; }
    public DateTime? LastReadAt { get; set; }   // للأ unread count
}

public class Message : BaseEntity
{
    public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; }           // FK => مين اللي بعت؟
    public string Content { get; set; }          // النص
    public MessageType Type { get; set; }       // enum: Text=1, File=2, System=3
    public MessageStatus Status { get; set; }   // enum: Sent=1, Delivered=2, Read=3
    public Guid? ReplyToMessageId { get; set; } // FK => لو reply لرسالة معينة
}
```

**لية بنستخدم ConversationParticipant؟**

```
✅ Performance: بنقدر نعرف مين في الـ conversation من غير JOIN كتير
✅ LastReadAt: كل participant عنده آخر message قرأه
✅ Unread Count: نحسبه = LastMessageAt - LastReadAt
✅ Archiving: لو user خرج من chat = history بيفضل
```

### Notification.cs

```csharp
public class Notification : BaseEntity
{
    public Guid UserId { get; set; }             // FK => receiver
    public string Title { get; set; }             // "New Proposal Received"
    public string Body { get; set; }             // "Ahmed sent a proposal..."
    public NotificationType Type { get; set; }  // enum: Proposal=1, Message=2...
    public Guid? RelatedEntityId { get; set; }   // FK => لو project أو proposal
    public bool IsRead { get; set; }             // هل اتقرأ؟
    public DateTime? ReadAt { get; set; }
}
```

---

## Module 10 - Learning (الدورات التعليمية)

### لية بنحتاج Learning Module؟

```
✅ Upskilling: Freelancers يحتاجوا يتعلموا skills جديدة
✅ Retention: لو platform فيها learning = users بتفضل عليها
✅ Revenue: Courses = مصدر دخل إضافي
✅ Community: Students + Instructors = engagement
```

### Course.cs و CourseEnrollment.cs

```csharp
public class Course : BaseEntity
{
    public Guid InstructorId { get; set; }        // FK => User(UserType.Instructor)
    public string Title { get; set; }             // "Advanced Python"
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
    public decimal Price { get; set; }            // 49.99
    public bool IsFree { get; set; }              // free course?
    public int DurationInHours { get; set; }    // 20 hours
    public int EnrollmentCount { get; set; }    // عدد المسجلين
    public double AverageRating { get; set; }    // 4.5
    public CourseLevel Level { get; set; }        // enum: Beginner=1, Intermediate=2...
    public CourseStatus Status { get; set; }    // enum: Draft=1, Published=2...
}

public class CourseEnrollment : BaseEntity
{
    public Guid CourseId { get; set; }            // FK
    public Guid StudentId { get; set; }           // FK
    public DateTime EnrolledAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal ProgressPercentage { get; set; } // 0-100
}
```

---

## Module 11 - AI (الذكاء الاصطناعي)

### لية بنحتاج AI Module؟

```
AI Features:
1️⃣ Profile Scoring: نح score كل freelancer (0-100)
2️⃣ Matching: ن-match بين freelancers و projects
3️⃣ Price Prediction: نpredict سعر المشروع
4️⃣ Vector Search: semantic search للـ skills

⚠️ مش بنخزن AI models هنا - ده للـ metadata + results
```

### ProfileScore.cs

```csharp
public class ProfileScore : BaseEntity
{
    public Guid FreelancerId { get; set; }       // FK
    public decimal OverallScore { get; set; }    // 85.5 / 100
    public decimal SkillScore { get; set; }      // 90.0
    public decimal ExperienceScore { get; set; } // 75.0
    public decimal PortfolioScore { get; set; } // 80.0
    public decimal ReviewScore { get; set; }     // 85.0
    public decimal ActivityScore { get; set; }   // 95.0
    
    public DateTime CalculatedAt { get; set; }   // امتى اتحسب؟
    public string? AlgorithmVersion { get; set; } // "v2.1" - للtrack versioning
}
```

**لية بنخزن الـ scores في DB؟**

```
✅ Performance: مش كل مرة هنحسب score
✅ Historical: نقدر نقارن score قبل وبعد
✅ Caching: بنستخدم الـ stored score بدل recalculate
✅ Audit: لو score غلط = نعرف إيه السبب
✅ Debugging: نقدر نفهم إيه اللي affect الـ score
```

### MatchingResult.cs

```csharp
public class MatchingResult : BaseEntity
{
    public Guid MatchingRunId { get; set; }     // FK => أي run؟
    public Guid ProjectId { get; set; }          // FK => المشروع
    public Guid FreelancerId { get; set; }       // FK => الـ freelancer
    public decimal MatchScore { get; set; }      // 87.5%
    public string? RecommendedSkills { get; set; } // JSON: ["Python", "Django"]
    public string? MissingSkills { get; set; }   // JSON: ["AWS"]
    public string? Recommendation { get; set; }  // "Strong match - recommend invitation"
}
```

**لية بنستخدم MatchingRun؟**

```
✅ Batching: بدل ما نmatch project واحد = نmatch multiple
✅ Scheduling: نقدر نschedule matching كل hour/day
✅ Performance: نعرف إيه الـ run اللي نمرة العملية دي
✅ Error tracking: لو run failed = بنعرف
```

### PricePrediction.cs

```csharp
public class PricePrediction : BaseEntity
{
    public Guid ProjectId { get; set; }          // FK => المشروع
    public decimal PredictedPrice { get; set; }  // 1250$
    public decimal MinPrice { get; set; }       // 1000$
    public decimal MaxPrice { get; set; }        // 1500$
    public string? ConfidenceLevel { get; set; } // "High", "Medium", "Low"
    public string? AlgorithmVersion { get; set; }
}
```

---

## Module 12 - Guilds (نظام النقابات الرقمية)

### لية بنحتاج Guilds Module؟

```
Professional Guilds = نقابات مهنية

مثال:
├── Python Guild: كل Python developers
├── UI/UX Guild: كل designers
└── DevOps Guild: كل DevOps engineers

Benefits:
✅ Community: members بيت помочь بعض
✅ Badges: نقدر نعطي badges للـ guild members
✅ Projects: guild-specific projects
✅ Learning: guild يorganize workshops
✅ Networking: members يتواصلوا مع بعض
```

### Guild.cs

```csharp
public class Guild : BaseEntity
{
    public string Name { get; set; }             // "Python Developers Guild"
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public string? BannerUrl { get; set; }
    public Guid CreatedById { get; set; }       // FK => founder
    public int MemberCount { get; set; }         // denormalized للperformance
    public GuildStatus Status { get; set; }     // enum: Public=1, Private=2
    
    public ICollection<GuildMember> Members { get; set; }
    public ICollection<GuildSkill> RequiredSkills { get; set; }
}

public class GuildMember : BaseEntity
{
    public Guid GuildId { get; set; }            // FK
    public Guid UserId { get; set; }             // FK
    public GuildRole Role { get; set; }          // enum: Member=1, Moderator=2, Leader=3
    public DateTime JoinedAt { get; set; }
    public bool IsActive { get; set; }
}
```

### TeamTask.cs

```csharp
public class TeamTask : BaseEntity
{
    public Guid GuildId { get; set; }            // FK => النقابة
    public Guid? AssignedToId { get; set; }     // FK => مين اللي مسئول؟
    public Guid? CreatedById { get; set; }      // FK => مين اللي أنشأه؟
    public string Title { get; set; }           // "Review PR #123"
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }       // enum: Todo=1, InProgress=2...
    public TaskPriority Priority { get; set; }   // enum: Low=1, Medium=2, High=3
    public DateTime? DueDate { get; set; }
}
```

---

## Module 13 - Students (نظام الطلاب)

### لية بنحتاج Students Module منفصل؟

```
User(UserType.Student) = طالب في university
Student entity = بيانات أكاديمية خاصة بالطالب

مثال:
├── GPA = 3.5
├── Major = Computer Science
├── Portfolio = شغله (مش كامل زي freelancer)
└── Training Projects = مشاريع تدريب
```

### Student.cs

```csharp
public class Student : BaseEntity
{
    public Guid UserId { get; set; }             // FK => One-to-One مع User
    public string? University { get; set; }      // "Cairo University"
    public string? Major { get; set; }           // "Computer Science"
    public string? StudentId { get; set; }       // University ID
    public decimal? GPA { get; set; }           // 3.5
    public GraduationStatus GraduationStatus { get; set; } // enum: InProgress=1...
    public DateTime? ExpectedGraduationDate { get; set; }
    public StudentStatus Status { get; set; }    // enum: Active=1, OnHold=2...
    
    public ICollection<StudentPortfolioItem> PortfolioItems { get; set; }
    public ICollection<TrainingProject> TrainingProjects { get; set; }
}

public class TrainingProject : BaseEntity
{
    public Guid StudentId { get; set; }           // FK
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? RepoUrl { get; set; }
    public string? LiveUrl { get; set; }
    public string? Technologies { get; set; }     // JSON: ["React", "Node.js"]
    public TrainingProjectStatus Status { get; set; }
}
```

---

## Module 14 - Coaching (نظام التدريب الشخصي)

### لية بنحتاج Coaching Module؟

```
Coaching = تدريب شخصي (1-on-1)

مثال:
├── Freelancer محتاج يطور مهاراته = ياخد coaching session
├── Coach = متخصص (Instructor أو Senior Freelancer)

Benefits:
✅ Upskilling: أسرع من الـ courses
✅ Personalized: ليه user واحد
✅ Revenue: Coaches ياخدوا فلوس
✅ Retention: Coach-Student relationship = strong bond
✅ Feedback: student ياخد direct feedback
```

### CoachingSession.cs

```csharp
public class CoachingSession : BaseEntity
{
    public Guid CoachId { get; set; }            // FK => User(Instructor/Freelancer)
    public Guid StudentId { get; set; }          // FK => User(Student/Freelancer)
    public string Topic { get; set; }            // "Career Development"
    public string? Description { get; set; }
    public SessionType Type { get; set; }        // enum: Video=1, Voice=2, Chat=3
    public SessionStatus Status { get; set; }   // enum: Scheduled=1, Completed=2...
    public DateTime ScheduledAt { get; set; }
    public int DurationInMinutes { get; set; }   // 60
    public decimal Price { get; set; }           // 50$
    public decimal? Rating { get; set; }          // 4.8 (بعد completion)
    public string? Feedback { get; set; }         // student feedback
}
```

---

## Base Entities - الأساس

### BaseEntity.cs

```csharp
public class BaseEntity
{
    public Guid Id { get; set; }                 // PK = كل الكيانات ليها ID
    
    // Timestamps = tracking
    public DateTime CreatedAt { get; set; }      // امتى اتخلق؟
    public string? CreatedBy { get; set; }       // مين اللي أنشأه؟ (user ID or system)
    public DateTime? UpdatedAt { get; set; }     // آخر update
    public string? UpdatedBy { get; set; }      // مين اللي update؟
}
```

**لية بنستخدم BaseEntity؟**

```
✅ DRY (Don't Repeat Yourself): كل entity بيورث من BaseEntity
✅ Consistency: كل entity ليها نفس structure
✅ Reusability: أي logic في BaseEntity = applied على كل entities
✅ Maintainability: تغيير واحد = impacts كل entities
✅ Auditability: كل entity معرف مين اللي أنشأه وامتى
```

### AuditableEntity.cs

```csharp
public class AuditableEntity : BaseEntity
{
    public bool IsDeleted { get; set; }           // Soft delete flag
    public DateTime? DeletedAt { get; set; }     // امتى اتمسح؟
    public string? DeletedBy { get; set; }      // مين اللي مسحه؟
}
```

**لية بنستخدم Soft Delete بدل Hard Delete؟**

```
Hard Delete (DELETE FROM users WHERE id = 1):

❌ لو user له relations = cascade delete = ممكن نمحي بيانات مهمة
❌ لو in error = مش هينفع نرجع البيانات
❌ Audit: مش هينفع نعرف مين اللي كان في النظام قبل 2 سنين

Soft Delete (UPDATE users SET IsDeleted = 1):

✅ البيانات بتفضل = historical record
✅ بنعرف ليه اتمسح (DeletedBy, DeletedAt)
✅ لو in error = نقدر نرجعه (IsDeleted = 0)
✅ لو أي entity ليه FK = نقدر نجيبه أو نتعامل معاه
✅ Audit trail: محتاجين للت compliance
✅ GDPR: لازم نقد نرجع البيانات لو user طلب
```

---

## Shared Interfaces

### IRepository.cs

```csharp
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
```

**لية بنستخدم Interfaces في Domain Layer؟**

```
✅ Dependency Inversion: Domain = مش بdepends على Infrastructure
✅ Testability: نقدر نmock الـ repository في tests
✅ Flexibility: نقدر نغير Implementation من غير ما نغير code
✅ Clean Architecture: كل layer ليها interfaces خاصة بيها

✅ Example:
   - Domain يعرفة: IRepository<User>
   - Infrastructure ي実装: EfUserRepository : IRepository<User>
   - لو عايز نغير لـ Dapper = نكتب DapperUserRepository : IRepository<User>
   - الـ Domain = مش هيتأثر
```

### IAuthService.cs

```csharp
public interface IAuthService
{
    Task<AuthResultDto> RegisterAsync(RegisterDto dto);
    Task<AuthResultDto> LoginAsync(LoginDto dto);
    Task<AuthResultDto> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync(string refreshToken);
}
```

**لية Auth interfaces في Domain مش Implementation؟**

```
✅ Domain = "WHAT" (what is authentication?)
✅ Infrastructure = "HOW" (how do we implement it?)

Benefits:
✅ Domain experts (business analysts) يقدروا يفهموا الـ requirements
✅ Infrastructure changes = مش بتأثر على Domain
✅ Testing: نقدر نtest الـ use cases من غير actual auth
✅ لو عايزين نغير auth provider (JWT -> OAuth) = مش هتتأثر الـ Domain
```

---

## Infrastructure Layer

### DepiDbContext.cs

```csharp
public class DepiDbContext : DbContext
{
    // DbSets = Tables
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<SecurityLog> SecurityLogs => Set<SecurityLog>();
    public DbSet<Token> Tokens => Set<Token>();
    
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<MediaFile> MediaFiles => Set<MediaFile>();
    
    // ... 50+ DbSets
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Entity Configurations
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
        });
    }
}
```

**لية بنستخدم Entity Framework DbContext؟**

```
✅ ORM (Object-Relational Mapper):
   - Code = C# objects
   - Database = SQL tables
   - EF = translator بينهم

✅ Benefits:
   - Less SQL = less errors
   - Type safety = compiler catches mistakes
   - Migration support = versioning للـ DB
   - LINQ = queries في C# بدل SQL strings
   - Testing: نقدر نستخدم in-memory database
   - Change tracking = automatic
   - Lazy loading = automatic
```

---

## API Layer

### AuthController.cs

```csharp
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        // 1. Validate input (automatic مع [ApiController])
        // 2. Call AuthService.RegisterAsync()
        // 3. Return response
        var result = await _authService.RegisterAsync(dto);
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        // 1. Validate credentials
        // 2. Generate tokens
        // 3. Return tokens to client
        var result = await _authService.LoginAsync(dto);
        return Ok(result);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
    {
        var result = await _authService.RefreshTokenAsync(dto.RefreshToken);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return Ok();
    }
}
```

**لية بنستخدم DTOs (Data Transfer Objects)؟**

```
✅ Security: مش كل entity fields لازم تت expose للـ API
   - User entity = PasswordHash (❌ مش هنبعتو للـ client)
   - UserDto = FirstName, LastName, Email (✅)

✅ Validation: DTOs ليهم validation rules
   - RegisterDto: Email = required, valid format
   - LoginDto: Email + Password = required

✅ Versioning: نقدر نغير DTO structure من غير ما نغير entity
✅ Performance: نبعت بس الـ data اللي محتاجها
✅ Documentation: Swagger يgenerate documentation تلقائياً
```

### JWT Configuration

```json
{
  "Jwt": {
    "Key": "very-long-secret-key-at-least-32-chars",
    "Issuer": "DEPI.SmartFreelance",
    "Audience": "DEPI.SmartFreelance.API",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  }
}
```

**لية JWT؟**

```
JWT = JSON Web Token = طريقة لـ authentication

بدون JWT:
❌ Session ID في server = استهلاك memory
❌ Hard to scale = كل server لازم يعرف الـ sessions
❌ CSRF attacks = vulnerable
❌ Server restart = sessions بتضيع

مع JWT:
✅ Stateless = server مش بيخزن sessions
✅ Scalable = أي server يقدر validates token
✅ Self-contained = token فيه كل الـ info
✅ Secure = signed + encrypted
✅ Performance = منغير database lookup
✅ Horizontal scaling = easy
```

---

## Flow Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│                        CLIENT (Mobile/Web)                       │
│                                                                  │
│  📱 Mobile App / 🌐 Web Frontend                                │
└─────────────────────────────────────────────────────────────────┘
                                │
                                │ HTTP Requests (REST)
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                     API LAYER (Controllers)                     │
│                                                                  │
│  Controllers:                                                   │
│  ├── AuthController (Register, Login, Logout)                   │
│  ├── UsersController (CRUD Users)                              │
│  ├── ProjectsController (CRUD Projects)                        │
│  ├── ProposalsController (Submit, Accept, Reject)              │
│  ├── ContractsController (Manage Contracts)                     │
│  ├── PaymentsController (Wallet, Transactions)                  │
│  └── ...                                                        │
│                                                                  │
│  Middleware:                                                    │
│  ├── ExceptionMiddleware (Error handling)                       │
│  ├── JwtMiddleware (Authentication)                            │
│  └── RateLimitingMiddleware                                     │
└─────────────────────────────────────────────────────────────────┘
                                │
                                │ Calls
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                   APPLICATION LAYER (Future)                     │
│                                                                  │
│  DTOs:                                                          │
│  ├── UserDto, ProjectDto, ProposalDto, etc.                     │
│                                                                  │
│  Use Cases / MediatR Handlers:                                 │
│  ├── CreateProjectHandler                                       │
│  ├── SubmitProposalHandler                                       │
│  └── ...                                                        │
│                                                                  │
│  Services:                                                      │
│  ├── ProjectService                                             │
│  ├── MatchingService (AI)                                        │
│  └── PaymentService                                             │
└─────────────────────────────────────────────────────────────────┘
                                │
                                │ Uses Interfaces
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                     DOMAIN LAYER (Entities)                      │
│                                                                  │
│  Entities (Business Objects):                                   │
│  ├── User, Role, Permission, Session, SecurityLog               │
│  ├── Project, Proposal, Contract, Milestone                     │
│  ├── Freelancer, Skill, PortfolioItem                          │
│  ├── Wallet, Transaction, Connect                              │
│  ├── Guild, TeamTask                                            │
│  └── ...                                                        │
│                                                                  │
│  + SHARED INTERFACES:                                           │
│  ├── IAuthService (Register, Login, Logout)                    │
│  ├── ITokenService (Generate, Validate)                         │
│  ├── IRepository<T> (CRUD operations)                          │
│  └── IUserRepository (User-specific queries)                    │
└─────────────────────────────────────────────────────────────────┘
                                ▲
                                │
│  Implementation (Dependency Inversion)                          │
└─────────────────────────────────────────────────────────────────┘
                                │
                                │ Implements Interfaces
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                  INFRASTRUCTURE LAYER (Implementation)          │
│                                                                  │
│  Database:                                                      │
│  ├── DepiDbContext (Entity Framework Core)                      │
│  ├── EntityConfigurations (Fluent API)                          │
│  └── Migrations                                                │
│                                                                  │
│  Auth:                                                          │
│  ├── AuthService (Register, Login, Logout)                      │
│  ├── JwtTokenService (Generate, Validate JWTs)                  │
│  ├── UserRepository (User CRUD)                                 │
│  └── SessionRepository (Session management)                     │
│                                                                  │
│  External Services:                                             │
│  ├── EmailService (Send emails)                                 │
│  ├── SmsService (Send SMS)                                      │
│  └── PaymentGateways (Stripe, PayPal)                          │
└─────────────────────────────────────────────────────────────────┘
                                │
                                │ EF Core / ADO.NET
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                       DATABASE (SQL Server)                      │
│                                                                  │
│  Tables:                                                        │
│  ├── Users, Roles, Permissions                                  │
│  ├── Sessions, Tokens, SecurityLogs                            │
│  ├── Projects, Proposals, Contracts, Milestones                 │
│  ├── Freelancers, Skills, PortfolioItems                        │
│  ├── Wallets, Transactions                                     │
│  ├── Guilds, GuildMembers                                      │
│  └── ...                                                        │
│                                                                  │
│  Views, Stored Procedures, Functions                            │
└─────────────────────────────────────────────────────────────────┘
```

---

## ملخص Decisions

| Decision | السبب |
|----------|-------|
| **BaseEntity مع Guid Id** | كل entity محتاج unique ID، Guid = distributed-friendly، no collisions |
| **Soft Delete (IsDeleted)** | بيانات مهمة = بنفضلها، audit trail، GDPR compliance |
| **Enum لStatuses** | Few options، rarely change، performance (int vs string) |
| **Entity لCountries/Skills** | Many options، ممكن تتغير، flexible (dashboard management) |
| **Separate Profile** | Clean separation، performance، flexibility، security |
| **FK بدلاً من Value Objects** | EF Core = better support، queries أسهل، migrations |
| **Many-to-Many tables (Join Tables)** | Metadata (level, years) مش هينفع في simple join |
| **DTOs for API** | Security، validation، versioning، performance |
| **Interfaces in Domain** | Dependency Inversion = testable، flexible، clean architecture |
| **JWT for Auth** | Stateless، scalable، self-contained، secure |
| **Session tracking** | Logout capability، security monitoring، revoke tokens |
| **AuditableEntity** | Compliance، debugging، historical tracking |
| **PendingBalance** | User clarity، protection، transparency |

---

## هيكل المشروع

```
DEPI.SmartFreelance/
├── DEPI.Domain/
│   ├── Common/
│   │   └── Base/
│   │       ├── BaseEntity.cs
│   │       └── AuditableEntity.cs
│   ├── Modules/
│   │   ├── Identity/
│   │   │   ├── Entities/
│   │   │   │   ├── User.cs
│   │   │   │   ├── Role.cs
│   │   │   │   ├── Permission.cs
│   │   │   │   ├── UserRole.cs
│   │   │   │   ├── RolePermission.cs
│   │   │   │   ├── Session.cs
│   │   │   │   ├── SecurityLog.cs
│   │   │   │   └── Token.cs
│   │   │   └── Enums/
│   │   │       └── UserType.cs
│   │   ├── References/
│   │   ├── Profiles/
│   │   ├── Companies/
│   │   ├── Freelancers/
│   │   ├── Projects/
│   │   ├── Payments/
│   │   ├── Recruitment/
│   │   ├── Communication/
│   │   ├── Learning/
│   │   ├── AI/
│   │   ├── Guilds/
│   │   ├── Students/
│   │   └── Coaching/
│   └── Shared/
│       └── Interfaces/
│           ├── IRepository.cs
│           ├── IAuthService.cs
│           └── ITokenService.cs
│
├── DEPI.Application/ (Future)
│   ├── DTOs/
│   ├── Services/
│   └── UseCases/
│
├── DEPI.Infrastructure/
│   ├── Auth/
│   │   ├── Services.cs
│   │   └── AuthExtensions.cs
│   └── Data/
│       └── DepiDbContext.cs
│
└── DEPI.API/
    ├── Controllers/
    │   ├── AuthController.cs
    │   └── UsersController.cs
    ├── Middleware/
    │   └── ExceptionMiddleware.cs
    ├── Extensions/
    │   └── ServiceExtensions.cs
    ├── Program.cs
    └── appsettings.json
```

---

## الإحصائيات

- **Total Entities**: 63
- **Total Modules**: 14
- **Total Lines of Code**: ~3000+
- **Build Status**: ✅ Success (0 errors, 0 warnings)

---

## Next Steps

1. **Application Layer** - DTOs, Use Cases, MediatR
2. **Entity Configurations** - FluentAPI mappings
3. **Database Migrations** - dotnet ef migrations
4. **Unit Tests** - xUnit/NUnit
5. **Additional Controllers** - Projects, Proposals, Contracts

---

*Document created: April 2026*
*Project: DEPI Smart Freelance Platform*
*Architecture: Clean Architecture with .NET 8*
