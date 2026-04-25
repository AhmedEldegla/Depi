namespace DEPI.Domain.Common.Messages;

public static class Strings
{
    public static class Validation
    {
        public const string EmailRequired = "البريد الإلكتروني مطلوب";
        public const string UserNameRequired = "اسم المستخدم مطلوب";
        public const string FirstNameRequired = "الاسم الأول مطلوب";
        public const string LastNameRequired = "الاسم الأخير مطلوب";
        public const string PasswordRequired = "كلمة المرور مطلوبة";
        public const string RoleNameRequired = "اسم الدور مطلوب";
        public const string InvalidEmail = "البريد الإلكتروني غير صحيح";
        public const string PasswordTooShort = "كلمة المرور يجب أن تكون 6 أحرف على الأقل";
    }

    public static class Errors
    {
        public const string NotFound = "غير موجود";
        public const string AlreadyExists = "موجود مسبقاً";
        public const string Unauthorized = "غير مصرح";
        public const string Forbidden = "ممنوع";
        public const string InvalidCredentials = "بيانات الاعتماد غير صحيحة";
        public const string EmailAlreadyExists = "البريد الإلكتروني موجود مسبقاً";
        public const string UserNameAlreadyExists = "اسم المستخدم موجود مسبقاً";
        
        public const string CannotUpdateDeleted = "لا يمكن تحديث عنصر محذوف";
        public const string CannotVerifyDeletedUser = "لا يمكن التحقق من هوية مستخدم محذوف";
        public const string CannotDeleteActive = "لا يمكن حذف عنصر نشط";
        public const string SelfReview = "لا يمكنك تقييم نفسك";
    }

    public static class Messages
    {
        public const string LoginSuccess = "تم تسجيل الدخول بنجاح";
        public const string RegisterSuccess = "تم التسجيل بنجاح";
        public const string CreateSuccess = "تم الإنشاء بنجاح";
        public const string UpdateSuccess = "تم التحديث بنجاح";
        public const string DeleteSuccess = "تم الحذف بنجاح";
        
        public const string EmailNotVerified = "يجب التحقق من البريد الإلكتروني";
        public const string AccountSuspended = "الحساب معلق";
        public const string AccountBanned = "الحساب محظور";
        public const string RefreshTokenExpired = "رمز التجديد منتهية الصلاحية";
    }

    public static class General
    {
        public const string Required = "مطلوب";
        public const string Invalid = "غير صالح";
        public const string Success = "نجاح";
        public const string Failed = "فشل";
        public const string Loading = "جاري التحميل";
        public const string NoData = "لا توجد بيانات";
    }
}