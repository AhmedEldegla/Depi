namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SkillEntityConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> entity)
    {
        entity.ToTable("Skills");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(e => e.NameEn)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(e => e.Description)
            .HasMaxLength(500);

        entity.HasIndex(e => e.NameEn).IsUnique();

        entity.HasData(
            Skill.Create("البرمجة", "Programming", null, true, 1),
            Skill.Create("تطوير الويب", "Web Development", null, true, 2),
            Skill.Create("تطبيقات الهاتف", "Mobile Development", null, true, 3),
            Skill.Create("الذكاء الاصطناعي", "AI & Machine Learning", null, true, 4),
            Skill.Create("قواعد البيانات", "Database", null, true, 5),
            Skill.Create("تصميم الجرافيك", "Graphic Design", null, true, 6),
            Skill.Create("UI/UX التصميم", "UI/UX Design", null, true, 7),
            Skill.Create("الكتابة التقنية", "Technical Writing", null, true, 8),
            Skill.Create("إدارة المشاريع", "Project Management", null, true, 9),
            Skill.Create("التسويق الإلكتروني", "Digital Marketing", null, true, 10)
        );
    }
}