## 🧾 قواعد كتابة الـ Commit (Commit Convention)

بنستخدم أسلوب **Conventional Commits** للحفاظ على تاريخ المشروع نظيف، واضح، وسهل الفهم.

---

### 🔹 شكل الـ Commit


type(scope): وصف مختصر


#### مثال:


feat(identity): إضافة كيان session لإدارة refresh tokens


---

### 🔹 أنواع الـ Commits

| النوع | الوصف |
|------|------|
| feat | إضافة ميزة جديدة |
| fix | إصلاح خطأ |
| refactor | تحسين الكود بدون تغيير السلوك |
| chore | مهام عامة (إعدادات، config، setup) |
| docs | تعديل أو إضافة توثيق |
| test | إضافة أو تعديل اختبارات |

---

### 🔹 النطاق (Scope)

الـ scope بيوضح الجزء المتأثر من المشروع:

- identity → موديول الهوية (Users, Roles, Entities)
- auth → التوثيق (Login, Tokens, Security)
- user → العمليات الخاصة بالمستخدم
- session → إدارة الجلسات و refresh tokens
- infrastructure → قاعدة البيانات، الخدمات، الإعدادات

---

### 🔹 أفضل الممارسات

- استخدم صيغة الأمر:
  - add / fix / implement
- خلي الرسالة واضحة ومختصرة
- كل commit يكون له هدف واحد
- تجنب الرسائل العامة مثل:
  - update code
  - fix stuff
  - done

---

### 🔹 مثال كامل


feat(session): إضافة إدارة الجلسات

تخزين refresh token كـ hash لزيادة الأمان
إضافة device fingerprint
دعم إلغاء الجلسات

---

### 🎯 الهدف

تسهيل فهم التغييرات من خلال الـ commits بدون الحاجة لفتح الكود.
