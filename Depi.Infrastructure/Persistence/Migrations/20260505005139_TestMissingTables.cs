using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DEPI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TestMissingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AILogs_Users_UserId1",
                table: "AILogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachingSessions_Users_CoachId1",
                table: "CoachingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachingSessions_Users_StudentId1",
                table: "CoachingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachProfiles_Users_UserId1",
                table: "CoachProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPosts_Users_AuthorId1",
                table: "CommunityPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_OwnerId1",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyFollowers_Users_UserId1",
                table: "CompanyFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyMembers_Users_UserId1",
                table: "CompanyMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyReviews_Users_AuthorId1",
                table: "CompanyReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectEarnings_Users_UserId1",
                table: "ConnectEarnings");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectPurchases_Users_UserId1",
                table: "ConnectPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectUsages_Users_UserId1",
                table: "ConnectUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollments_Users_StudentId1",
                table: "CourseEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviews_Users_StudentId1",
                table: "CourseReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_InstructorId1",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumReplies_Users_AuthorId1",
                table: "ForumReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumThreads_Users_AuthorId1",
                table: "ForumThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerScores_Users_FreelancerId1",
                table: "FreelancerScores");

            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerSubscriptions_Users_UserId1",
                table: "FreelancerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_GuildMembers_Users_UserId1",
                table: "GuildMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_Users_LeaderId1",
                table: "Guilds");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadHunters_Users_UserId1",
                table: "HeadHunters");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Users_ApplicantId1",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobMatches_Users_FreelancerId1",
                table: "JobMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_OwnerId1",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_LearningPaths_Users_InstructorId1",
                table: "LearningPaths");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgresses_Users_StudentId1",
                table: "LessonProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_PostBookmarks_Users_UserId1",
                table: "PostBookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Users_AuthorId1",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Users_UserId1",
                table: "PostLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostShares_Users_UserId1",
                table: "PostShares");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMatches_Users_FreelancerId1",
                table: "ProjectMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Users_UserId1",
                table: "Recommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillMatches_Users_FreelancerId1",
                table: "SkillMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_Users_UserId1",
                table: "StudentProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_TalentRecommendations_Users_RecommendedUserId1",
                table: "TalentRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguages_Users_UserId1",
                table: "UserLanguages");

            migrationBuilder.DropIndex(
                name: "IX_UserLanguages_UserId1",
                table: "UserLanguages");

            migrationBuilder.DropIndex(
                name: "IX_TalentRecommendations_RecommendedUserId1",
                table: "TalentRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_UserId1",
                table: "StudentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_SkillMatches_FreelancerId1",
                table: "SkillMatches");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_UserId1",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMatches_FreelancerId1",
                table: "ProjectMatches");

            migrationBuilder.DropIndex(
                name: "IX_PostShares_UserId1",
                table: "PostShares");

            migrationBuilder.DropIndex(
                name: "IX_PostLikes_UserId1",
                table: "PostLikes");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_AuthorId1",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_PostBookmarks_UserId1",
                table: "PostBookmarks");

            migrationBuilder.DropIndex(
                name: "IX_LessonProgresses_StudentId1",
                table: "LessonProgresses");

            migrationBuilder.DropIndex(
                name: "IX_LearningPaths_InstructorId1",
                table: "LearningPaths");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_OwnerId1",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_JobMatches_FreelancerId1",
                table: "JobMatches");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_ApplicantId1",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_HeadHunters_UserId1",
                table: "HeadHunters");

            migrationBuilder.DropIndex(
                name: "IX_Guilds_LeaderId1",
                table: "Guilds");

            migrationBuilder.DropIndex(
                name: "IX_GuildMembers_UserId1",
                table: "GuildMembers");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerSubscriptions_UserId1",
                table: "FreelancerSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerScores_FreelancerId1",
                table: "FreelancerScores");

            migrationBuilder.DropIndex(
                name: "IX_ForumThreads_AuthorId1",
                table: "ForumThreads");

            migrationBuilder.DropIndex(
                name: "IX_ForumReplies_AuthorId1",
                table: "ForumReplies");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorId1",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_CourseReviews_StudentId1",
                table: "CourseReviews");

            migrationBuilder.DropIndex(
                name: "IX_CourseEnrollments_StudentId1",
                table: "CourseEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_ConnectUsages_UserId1",
                table: "ConnectUsages");

            migrationBuilder.DropIndex(
                name: "IX_ConnectPurchases_UserId1",
                table: "ConnectPurchases");

            migrationBuilder.DropIndex(
                name: "IX_ConnectEarnings_UserId1",
                table: "ConnectEarnings");

            migrationBuilder.DropIndex(
                name: "IX_CompanyReviews_AuthorId1",
                table: "CompanyReviews");

            migrationBuilder.DropIndex(
                name: "IX_CompanyMembers_UserId1",
                table: "CompanyMembers");

            migrationBuilder.DropIndex(
                name: "IX_CompanyFollowers_UserId1",
                table: "CompanyFollowers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_OwnerId1",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_CommunityPosts_AuthorId1",
                table: "CommunityPosts");

            migrationBuilder.DropIndex(
                name: "IX_CoachProfiles_UserId1",
                table: "CoachProfiles");

            migrationBuilder.DropIndex(
                name: "IX_CoachingSessions_CoachId1",
                table: "CoachingSessions");

            migrationBuilder.DropIndex(
                name: "IX_CoachingSessions_StudentId1",
                table: "CoachingSessions");

            migrationBuilder.DropIndex(
                name: "IX_AILogs_UserId1",
                table: "AILogs");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2495d775-e657-4ee7-8395-f99088610b98"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("32f3b258-4bfb-41e5-8c5d-26b49dd1ec83"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("3a450c78-9900-4c03-b7c4-b57a5fc86369"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("5b6561d0-83dc-4db5-b724-ecdd58986ce6"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("5bf02776-8720-44fa-bb18-967c50564a72"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6030327d-15a0-47f4-9d33-a6d0ab38adf9"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6e8f6626-358c-415e-b9fe-2bc64a415f55"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7edb16bf-278d-4a04-a15f-de65a0a37040"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("83ac73b1-73c9-4e64-abd5-9b519e73775e"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("940752b0-6afc-441b-9364-5ae13e0e1179"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("9d2d4243-d335-43fd-a3c1-dc5c5b6d2363"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a9f00b1a-5836-4044-835f-58cfc64bc983"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("aad7ed0e-d10e-4ca0-91c8-eafc5107576b"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("aefb1dd3-6716-4ea1-9187-229657d89b89"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b40fd706-182d-4930-a237-8bbf07ec2ec9"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b754d726-c444-47b8-b36c-b80b83194f05"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c40e5444-f33b-4a85-b0f9-fafa5fb66ea2"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d85e5575-b959-4702-8a6e-2c6f99e9a19d"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("dac43949-c374-4194-95a6-3024d5a4ac9e"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ed2082ce-f1fc-4b63-a0f7-f11150480bb8"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("399f11ec-9fb8-411d-82e7-b8fdadbce718"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("4c807d8c-bb4e-4d8b-a306-e4dec6d75630"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("5142bf39-d0b8-4e42-955b-88d5146c0241"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("6eae861b-093d-4e02-bd1b-fb0171ea5ae9"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("805117c6-76cd-4b81-9d8d-37bcdd201c49"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("92101ff9-fbb9-41e3-8deb-f5f0760e7c77"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("9af26c3e-2a4a-4d52-8765-076460b36888"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("a9855ac0-9078-46a0-b1a9-4b98aff5ef26"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ad866a67-d3cf-4469-8a31-70a9489f6db3"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ae6df5fe-61a9-4e98-a9a4-f3e7be463ecc"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("cf769eb3-5422-49dc-b212-3b9063210451"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d4585a5d-8b33-47ba-9feb-9d7b082fbb98"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d76fc049-09ae-4d04-934f-3020aa1647b4"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("fab25306-d727-491d-bb5a-5937a93f32ac"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("001947ba-50ba-4ad8-88e0-cc38e9551562"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("0eda0592-b68c-4c4f-8cc0-2e8fc19ac6c5"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("121c3c3f-5170-4fe9-8300-84a9cb7ba853"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("31f08afa-8374-4e93-b753-0fd9bbe9f2a4"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("4ed1884c-0dd4-4019-8e53-2057738e93dd"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("93ca1876-ba68-445e-852a-da9eb89f341c"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("a1ca5da2-81c8-4ac8-81df-1955d2289192"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("e16a2d67-01bd-4fe7-a620-dc2d2ae0d3cd"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("f1c23d5f-8d4d-4a39-89b9-980aecf7985d"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("f64218b6-fcbf-44c1-a488-e6b4be0c0841"));

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserLanguages");

            migrationBuilder.DropColumn(
                name: "RecommendedUserId1",
                table: "TalentRecommendations");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "FreelancerId1",
                table: "SkillMatches");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "FreelancerId1",
                table: "ProjectMatches");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PostShares");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PostLikes");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PostBookmarks");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "LessonProgresses");

            migrationBuilder.DropColumn(
                name: "InstructorId1",
                table: "LearningPaths");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FreelancerId1",
                table: "JobMatches");

            migrationBuilder.DropColumn(
                name: "ApplicantId1",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "HeadHunters");

            migrationBuilder.DropColumn(
                name: "LeaderId1",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "GuildMembers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "FreelancerSubscriptions");

            migrationBuilder.DropColumn(
                name: "FreelancerId1",
                table: "FreelancerScores");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "ForumThreads");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "ForumReplies");

            migrationBuilder.DropColumn(
                name: "InstructorId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "HelpfulnessScore",
                table: "CourseReviews");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "CourseReviews");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "CourseEnrollments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ConnectUsages");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ConnectPurchases");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ConnectEarnings");

            migrationBuilder.DropColumn(
                name: "AgreeCount",
                table: "CompanyReviews");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "CompanyReviews");

            migrationBuilder.DropColumn(
                name: "DisagreeCount",
                table: "CompanyReviews");

            migrationBuilder.DropColumn(
                name: "HelpfulnessScore",
                table: "CompanyReviews");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CompanyMembers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CompanyFollowers");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "CommunityPosts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CoachProfiles");

            migrationBuilder.DropColumn(
                name: "CoachId1",
                table: "CoachingSessions");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "CoachingSessions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AILogs");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnings",
                table: "UserProfiles",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ResponseRate",
                table: "UserProfiles",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OnTimeDeliveryRate",
                table: "UserProfiles",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CompletionRate",
                table: "UserProfiles",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserLanguages",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SkillsScore",
                table: "TalentRecommendations",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecommendedUserId",
                table: "TalentRecommendations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProfileScore",
                table: "TalentRecommendations",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MatchScore",
                table: "TalentRecommendations",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExperienceScore",
                table: "TalentRecommendations",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "StudentProfiles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SkillsAssessmentScore",
                table: "StudentProfiles",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReadinessScore",
                table: "StudentProfiles",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProgressPercentage",
                table: "StudentProfiles",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "StudentProfiles",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedCoachId",
                table: "StudentProfiles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MatchScore",
                table: "SkillMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FreelancerId",
                table: "SkillMatches",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "ScoringRules",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinValue",
                table: "ScoringRules",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxValue",
                table: "ScoringRules",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultScore",
                table: "ScoringRules",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "ScoringCriterias",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "ScoringCriterias",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Recommendations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConfidenceScore",
                table: "Recommendations",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SkillScore",
                table: "ProjectMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReputationScore",
                table: "ProjectMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OverallScore",
                table: "ProjectMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LocationScore",
                table: "ProjectMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FreelancerId",
                table: "ProjectMatches",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExperienceScore",
                table: "ProjectMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BudgetScore",
                table: "ProjectMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailabilityScore",
                table: "ProjectMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "PostShares",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "PostLikes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "PostComments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "PostBookmarks",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "LessonProgresses",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "LearningPaths",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstructorId",
                table: "LearningPaths",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MatchScore",
                table: "Jobs",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BudgetMin",
                table: "Jobs",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BudgetMax",
                table: "Jobs",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SkillScore",
                table: "JobMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalaryScore",
                table: "JobMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OverallScore",
                table: "JobMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LocationScore",
                table: "JobMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FreelancerId",
                table: "JobMatches",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExperienceScore",
                table: "JobMatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicantId",
                table: "JobApplications",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "HeadHunters",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SuccessRate",
                table: "HeadHunters",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionRate",
                table: "HeadHunters",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageResponseTimeHours",
                table: "HeadHunters",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnings",
                table: "Guilds",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinProfileScore",
                table: "Guilds",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeaderId",
                table: "Guilds",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "Guilds",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Earnings",
                table: "GuildProjects",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "GuildMembers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "FreelancerSubscriptions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "FreelancerSubscriptions",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnings",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SkillScore",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ResponsivenessScore",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ResponseRate",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReliabilityScore",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "QualityScore",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectSuccessScore",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OverallScore",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OnTimeDeliveryRate",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FreelancerId",
                table: "FreelancerScores",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "EarningsScore",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CompletionRateScore",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CommunicationScore",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ClientSatisfactionScore",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "FreelancerScores",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "ForumThreads",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "ForumReplies",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Courses",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstructorId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "CourseReviews",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "CourseEnrollments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePaid",
                table: "CourseEnrollments",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ConnectUsages",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Connects",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ConnectPurchases",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPaid",
                table: "ConnectPurchases",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ConnectEarnings",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinRating",
                table: "ConnectEarningRules",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "CompanyReviews",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CompanyMembers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CompanyFollowers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Companies",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Companies",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SentimentScore",
                table: "CommunityPosts",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "CommunityPosts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CoachProfiles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "CoachProfiles",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "CoachProfiles",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "CoachingSessions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoachId",
                table: "CoachingSessions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperature",
                table: "AIModelConfigs",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinConfidenceScore",
                table: "AIModelConfigs",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MatchThreshold",
                table: "AIModelConfigs",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AILogs",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConfidenceScore",
                table: "AILogs",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DisplayOrder", "FlagUrl", "IsActive", "IsDeleted", "Iso2", "Iso3", "Name", "NameEn", "PhoneCode", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("0cbbbab6-06d2-4e76-8de4-28e2892ebbf4"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1084), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 17, "🇸🇩", true, false, "SD", "SDN", "السودان", "Sudan", "+249", null, null },
                    { new Guid("0d9deff2-92cc-488e-a862-325f00c926a6"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1026), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 6, "🇧🇭", true, false, "BH", "BHR", "البحرين", "Bahrain", "+973", null, null },
                    { new Guid("25968969-1b2e-45d7-acd0-fd5617a3bcd2"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1077), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 16, "🇱🇾", true, false, "LY", "LBY", "ليبيا", "Libya", "+218", null, null },
                    { new Guid("304e8de2-f15b-4de5-9379-5ed2ac054730"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1010), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 2, "🇸🇦", true, false, "SA", "SAU", "المملكة العربية السعودية", "Saudi Arabia", "+966", null, null },
                    { new Guid("6056a5bf-e7d2-45f6-9cce-8345bc9759c5"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1041), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 9, "🇮🇶", true, false, "IQ", "IRQ", "العراق", "Iraq", "+964", null, null },
                    { new Guid("77b394a0-cf52-4d19-91a8-1ca379a08307"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1018), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 4, "🇰🇼", true, false, "KW", "KWT", "الكويت", "Kuwait", "+965", null, null },
                    { new Guid("79d97599-cb8b-46af-9860-2e2d16a7225e"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1014), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 3, "🇦🇪", true, false, "AE", "ARE", "الإمارات العربية المتحدة", "United Arab Emirates", "+971", null, null },
                    { new Guid("82c7a6fe-d610-41fc-9a2a-8769d042d7d6"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(971), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1, "🇪🇬", true, false, "EG", "EGY", "مصر", "Egypt", "+20", null, null },
                    { new Guid("8d7989d1-4cfd-49d9-b63a-28a747dbc3b5"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1056), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 14, "🇲🇦", true, false, "MA", "MAR", "المغرب", "Morocco", "+212", null, null },
                    { new Guid("97ba4214-ebd1-445c-99e4-7e85d94445a8"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1094), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 20, "🇺🇸", true, false, "US", "USA", "الولايات المتحدة", "United States", "+1", null, null },
                    { new Guid("9cdd59cd-ce35-4536-a94d-f4da3338e585"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1044), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 10, "🇾🇪", true, false, "YE", "YEM", "اليمن", "Yemen", "+967", null, null },
                    { new Guid("a50a425a-d8c9-4371-914e-30198735afb6"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1087), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 18, "🇴🇲", true, false, "OM", "OMN", "عُمان", "Oman", "+968", null, null },
                    { new Guid("dc409bb1-72ba-4d0c-a433-0ead03d0e174"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1090), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 19, "🇹🇷", true, false, "TR", "TUR", "تركيا", "Turkey", "+90", null, null },
                    { new Guid("eadc6a69-7568-4be9-9f0f-0d6362905606"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1047), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 11, "🇸🇾", true, false, "SY", "SYR", "سوريا", "Syria", "+963", null, null },
                    { new Guid("ed48fac1-1a56-4a2c-8caf-4fc76bbaeb9d"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1033), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 8, "🇱🇧", true, false, "LB", "LBN", "لبنان", "Lebanon", "+961", null, null },
                    { new Guid("ee806d31-48bf-4f1a-b789-8c679ef7ee4e"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1060), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 15, "🇹🇳", true, false, "TN", "TUN", "تونس", "Tunisia", "+216", null, null },
                    { new Guid("ef53d66d-3209-4440-99a1-9b5c064d28b2"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1054), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 13, "🇩🇿", true, false, "DZ", "DZA", "الجزائر", "Algeria", "+213", null, null },
                    { new Guid("fa0907ce-e090-4b46-af6d-875b344f1fb5"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1029), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 7, "🇯🇴", true, false, "JO", "JOR", "الأردن", "Jordan", "+962", null, null },
                    { new Guid("fd8c7064-827b-49a7-9473-16a693f04cef"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1022), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 5, "🇶🇦", true, false, "QA", "QAT", "قطر", "Qatar", "+974", null, null },
                    { new Guid("fe10d5a9-d591-4253-b8f2-af77f46608b1"), new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(1051), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 12, "🇵🇸", true, false, "PS", "PSE", "فلسطين", "Palestine", "+970", null, null }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ExchangeRate", "IsActive", "IsDefault", "IsDeleted", "Name", "Symbol", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("223216aa-3112-49b0-ba84-da3540fc7ef4"), "EUR", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8744), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Euro", "€", null, null },
                    { new Guid("23ffec56-ceef-4d03-8e93-7868a71f4543"), "KWD", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8800), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Kuwaiti Dinar", "د.ك", null, null },
                    { new Guid("30098163-2a41-4eb0-b7f2-b73646f1c409"), "AED", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8791), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "UAE Dirham", "د.إ", null, null },
                    { new Guid("5128b7c7-6b78-45cf-8683-18df673ed7c2"), "CAD", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8822), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Canadian Dollar", "C$", null, null },
                    { new Guid("54aea0ac-584f-4304-b7d4-0b9bcf379069"), "JOD", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8818), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Jordanian Dinar", "د.أ", null, null },
                    { new Guid("62006614-9e13-40bb-a012-0c554ca5cb19"), "EGP", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8637), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Egyptian Pound", "E£", null, null },
                    { new Guid("742d3968-0325-406c-85ac-05ef5d9387ff"), "GBP", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8642), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "British Pound", "£", null, null },
                    { new Guid("78aa9333-0766-43b0-a6ca-8b90b7c55c30"), "OMR", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8814), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Omani Rial", "﷼", null, null },
                    { new Guid("7eef2d72-5de5-4576-9b1b-0b31a73bf90e"), "AUD", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8826), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Australian Dollar", "A$", null, null },
                    { new Guid("8a28219d-781a-464a-a93e-42f064ee977e"), "USD", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8594), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, true, false, "US Dollar", "$", null, null },
                    { new Guid("8be3552f-be14-4bc8-9841-c8cdde117ece"), "BHD", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8809), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Bahraini Dinar", ".د.ب", null, null },
                    { new Guid("acad6903-dde3-493b-8cfe-88cf845ce940"), "INR", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8834), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Indian Rupee", "₹", null, null },
                    { new Guid("c14a43c6-9bb3-4eec-a8c6-bfe810c3110b"), "QAR", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8804), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Qatari Riyal", "﷼", null, null },
                    { new Guid("d3395e49-a1ce-4e08-b9c6-ed4e11b981e6"), "SAR", new DateTime(2026, 5, 5, 0, 51, 33, 835, DateTimeKind.Utc).AddTicks(8764), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Saudi Riyal", "﷼", null, null }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "DisplayOrder", "IsActive", "IsDeleted", "IsVerified", "Name", "NameEn", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("145c5a87-edf7-49b9-95fb-6e8fe6cc273c"), new DateTime(2026, 5, 5, 0, 51, 33, 867, DateTimeKind.Utc).AddTicks(9950), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 7, true, false, true, "UI/UX التصميم", "UI/UX Design", null, null },
                    { new Guid("1afbd511-367d-4fb2-9484-480bc8964d39"), new DateTime(2026, 5, 5, 0, 51, 33, 867, DateTimeKind.Utc).AddTicks(9990), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 10, true, false, true, "التسويق الإلكتروني", "Digital Marketing", null, null },
                    { new Guid("1b50f94d-6a57-410d-b073-e3ae9830231e"), new DateTime(2026, 5, 5, 0, 51, 33, 867, DateTimeKind.Utc).AddTicks(9929), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 1, true, false, true, "البرمجة", "Programming", null, null },
                    { new Guid("289c6d69-d520-4201-90a7-f25ba4e6148f"), new DateTime(2026, 5, 5, 0, 51, 33, 867, DateTimeKind.Utc).AddTicks(9988), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 9, true, false, true, "إدارة المشاريع", "Project Management", null, null },
                    { new Guid("6933f07f-28da-47f8-aa70-a52bca18686b"), new DateTime(2026, 5, 5, 0, 51, 33, 867, DateTimeKind.Utc).AddTicks(9941), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 3, true, false, true, "تطبيقات الهاتف", "Mobile Development", null, null },
                    { new Guid("8bebe810-51ac-41df-9b80-cb17826f1e6c"), new DateTime(2026, 5, 5, 0, 51, 33, 867, DateTimeKind.Utc).AddTicks(9943), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 4, true, false, true, "الذكاء الاصطناعي", "AI & Machine Learning", null, null },
                    { new Guid("a0a40440-5b2e-47b3-a977-bb1f6eba4142"), new DateTime(2026, 5, 5, 0, 51, 33, 867, DateTimeKind.Utc).AddTicks(9948), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 6, true, false, true, "تصميم الجرافيك", "Graphic Design", null, null },
                    { new Guid("acfaa9d9-e593-4fe9-aa1d-84020028248f"), new DateTime(2026, 5, 5, 0, 51, 33, 867, DateTimeKind.Utc).AddTicks(9945), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 5, true, false, true, "قواعد البيانات", "Database", null, null },
                    { new Guid("d64628f6-c6b0-4a81-a97a-9e720fcb2c69"), new DateTime(2026, 5, 5, 0, 51, 33, 867, DateTimeKind.Utc).AddTicks(9986), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 8, true, false, true, "الكتابة التقنية", "Technical Writing", null, null },
                    { new Guid("df778742-2a0d-4029-b257-652bb7c60684"), new DateTime(2026, 5, 5, 0, 51, 33, 867, DateTimeKind.Utc).AddTicks(9938), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 2, true, false, true, "تطوير الويب", "Web Development", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguages_UserId",
                table: "UserLanguages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TalentRecommendations_RecommendedUserId",
                table: "TalentRecommendations",
                column: "RecommendedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_UserId",
                table: "StudentProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillMatches_FreelancerId",
                table: "SkillMatches",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_UserId",
                table: "Recommendations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMatches_FreelancerId",
                table: "ProjectMatches",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_PostShares_UserId",
                table: "PostShares",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_UserId",
                table: "PostLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_AuthorId",
                table: "PostComments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostBookmarks_UserId",
                table: "PostBookmarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonProgresses_StudentId",
                table: "LessonProgresses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPaths_InstructorId",
                table: "LearningPaths",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_OwnerId",
                table: "Jobs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobMatches_FreelancerId",
                table: "JobMatches",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ApplicantId",
                table: "JobApplications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadHunters_UserId",
                table: "HeadHunters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_LeaderId",
                table: "Guilds",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildMembers_UserId",
                table: "GuildMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerSubscriptions_UserId",
                table: "FreelancerSubscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerScores_FreelancerId",
                table: "FreelancerScores",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumThreads_AuthorId",
                table: "ForumThreads",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumReplies_AuthorId",
                table: "ForumReplies",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_StudentId",
                table: "CourseReviews",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_StudentId",
                table: "CourseEnrollments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectUsages_UserId",
                table: "ConnectUsages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectPurchases_UserId",
                table: "ConnectPurchases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectEarnings_UserId",
                table: "ConnectEarnings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReviews_AuthorId",
                table: "CompanyReviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMembers_UserId",
                table: "CompanyMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFollowers_UserId",
                table: "CompanyFollowers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_OwnerId",
                table: "Companies",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPosts_AuthorId",
                table: "CommunityPosts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachProfiles_UserId",
                table: "CoachProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachingSessions_CoachId",
                table: "CoachingSessions",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachingSessions_StudentId",
                table: "CoachingSessions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AILogs_UserId",
                table: "AILogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AILogs_Users_UserId",
                table: "AILogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingSessions_Users_CoachId",
                table: "CoachingSessions",
                column: "CoachId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingSessions_Users_StudentId",
                table: "CoachingSessions",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachProfiles_Users_UserId",
                table: "CoachProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPosts_Users_AuthorId",
                table: "CommunityPosts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_OwnerId",
                table: "Companies",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyFollowers_Users_UserId",
                table: "CompanyFollowers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyMembers_Users_UserId",
                table: "CompanyMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyReviews_Users_AuthorId",
                table: "CompanyReviews",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectEarnings_Users_UserId",
                table: "ConnectEarnings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectPurchases_Users_UserId",
                table: "ConnectPurchases",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectUsages_Users_UserId",
                table: "ConnectUsages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollments_Users_StudentId",
                table: "CourseEnrollments",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviews_Users_StudentId",
                table: "CourseReviews",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumReplies_Users_AuthorId",
                table: "ForumReplies",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumThreads_Users_AuthorId",
                table: "ForumThreads",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerScores_Users_FreelancerId",
                table: "FreelancerScores",
                column: "FreelancerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerSubscriptions_Users_UserId",
                table: "FreelancerSubscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GuildMembers_Users_UserId",
                table: "GuildMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_Users_LeaderId",
                table: "Guilds",
                column: "LeaderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HeadHunters_Users_UserId",
                table: "HeadHunters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Users_ApplicantId",
                table: "JobApplications",
                column: "ApplicantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobMatches_Users_FreelancerId",
                table: "JobMatches",
                column: "FreelancerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_OwnerId",
                table: "Jobs",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LearningPaths_Users_InstructorId",
                table: "LearningPaths",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgresses_Users_StudentId",
                table: "LessonProgresses",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostBookmarks_Users_UserId",
                table: "PostBookmarks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Users_AuthorId",
                table: "PostComments",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Users_UserId",
                table: "PostLikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostShares_Users_UserId",
                table: "PostShares",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMatches_Users_FreelancerId",
                table: "ProjectMatches",
                column: "FreelancerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Users_UserId",
                table: "Recommendations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillMatches_Users_FreelancerId",
                table: "SkillMatches",
                column: "FreelancerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_Users_UserId",
                table: "StudentProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TalentRecommendations_Users_RecommendedUserId",
                table: "TalentRecommendations",
                column: "RecommendedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguages_Users_UserId",
                table: "UserLanguages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AILogs_Users_UserId",
                table: "AILogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachingSessions_Users_CoachId",
                table: "CoachingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachingSessions_Users_StudentId",
                table: "CoachingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachProfiles_Users_UserId",
                table: "CoachProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPosts_Users_AuthorId",
                table: "CommunityPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_OwnerId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyFollowers_Users_UserId",
                table: "CompanyFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyMembers_Users_UserId",
                table: "CompanyMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyReviews_Users_AuthorId",
                table: "CompanyReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectEarnings_Users_UserId",
                table: "ConnectEarnings");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectPurchases_Users_UserId",
                table: "ConnectPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectUsages_Users_UserId",
                table: "ConnectUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollments_Users_StudentId",
                table: "CourseEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviews_Users_StudentId",
                table: "CourseReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumReplies_Users_AuthorId",
                table: "ForumReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumThreads_Users_AuthorId",
                table: "ForumThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerScores_Users_FreelancerId",
                table: "FreelancerScores");

            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerSubscriptions_Users_UserId",
                table: "FreelancerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_GuildMembers_Users_UserId",
                table: "GuildMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_Users_LeaderId",
                table: "Guilds");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadHunters_Users_UserId",
                table: "HeadHunters");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Users_ApplicantId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobMatches_Users_FreelancerId",
                table: "JobMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_OwnerId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_LearningPaths_Users_InstructorId",
                table: "LearningPaths");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgresses_Users_StudentId",
                table: "LessonProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_PostBookmarks_Users_UserId",
                table: "PostBookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Users_AuthorId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Users_UserId",
                table: "PostLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostShares_Users_UserId",
                table: "PostShares");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMatches_Users_FreelancerId",
                table: "ProjectMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Users_UserId",
                table: "Recommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillMatches_Users_FreelancerId",
                table: "SkillMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_Users_UserId",
                table: "StudentProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_TalentRecommendations_Users_RecommendedUserId",
                table: "TalentRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguages_Users_UserId",
                table: "UserLanguages");

            migrationBuilder.DropIndex(
                name: "IX_UserLanguages_UserId",
                table: "UserLanguages");

            migrationBuilder.DropIndex(
                name: "IX_TalentRecommendations_RecommendedUserId",
                table: "TalentRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_UserId",
                table: "StudentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_SkillMatches_FreelancerId",
                table: "SkillMatches");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_UserId",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMatches_FreelancerId",
                table: "ProjectMatches");

            migrationBuilder.DropIndex(
                name: "IX_PostShares_UserId",
                table: "PostShares");

            migrationBuilder.DropIndex(
                name: "IX_PostLikes_UserId",
                table: "PostLikes");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_AuthorId",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_PostBookmarks_UserId",
                table: "PostBookmarks");

            migrationBuilder.DropIndex(
                name: "IX_LessonProgresses_StudentId",
                table: "LessonProgresses");

            migrationBuilder.DropIndex(
                name: "IX_LearningPaths_InstructorId",
                table: "LearningPaths");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_OwnerId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_JobMatches_FreelancerId",
                table: "JobMatches");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_ApplicantId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_HeadHunters_UserId",
                table: "HeadHunters");

            migrationBuilder.DropIndex(
                name: "IX_Guilds_LeaderId",
                table: "Guilds");

            migrationBuilder.DropIndex(
                name: "IX_GuildMembers_UserId",
                table: "GuildMembers");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerSubscriptions_UserId",
                table: "FreelancerSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerScores_FreelancerId",
                table: "FreelancerScores");

            migrationBuilder.DropIndex(
                name: "IX_ForumThreads_AuthorId",
                table: "ForumThreads");

            migrationBuilder.DropIndex(
                name: "IX_ForumReplies_AuthorId",
                table: "ForumReplies");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_CourseReviews_StudentId",
                table: "CourseReviews");

            migrationBuilder.DropIndex(
                name: "IX_CourseEnrollments_StudentId",
                table: "CourseEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_ConnectUsages_UserId",
                table: "ConnectUsages");

            migrationBuilder.DropIndex(
                name: "IX_ConnectPurchases_UserId",
                table: "ConnectPurchases");

            migrationBuilder.DropIndex(
                name: "IX_ConnectEarnings_UserId",
                table: "ConnectEarnings");

            migrationBuilder.DropIndex(
                name: "IX_CompanyReviews_AuthorId",
                table: "CompanyReviews");

            migrationBuilder.DropIndex(
                name: "IX_CompanyMembers_UserId",
                table: "CompanyMembers");

            migrationBuilder.DropIndex(
                name: "IX_CompanyFollowers_UserId",
                table: "CompanyFollowers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_OwnerId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_CommunityPosts_AuthorId",
                table: "CommunityPosts");

            migrationBuilder.DropIndex(
                name: "IX_CoachProfiles_UserId",
                table: "CoachProfiles");

            migrationBuilder.DropIndex(
                name: "IX_CoachingSessions_CoachId",
                table: "CoachingSessions");

            migrationBuilder.DropIndex(
                name: "IX_CoachingSessions_StudentId",
                table: "CoachingSessions");

            migrationBuilder.DropIndex(
                name: "IX_AILogs_UserId",
                table: "AILogs");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0cbbbab6-06d2-4e76-8de4-28e2892ebbf4"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0d9deff2-92cc-488e-a862-325f00c926a6"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("25968969-1b2e-45d7-acd0-fd5617a3bcd2"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("304e8de2-f15b-4de5-9379-5ed2ac054730"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6056a5bf-e7d2-45f6-9cce-8345bc9759c5"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("77b394a0-cf52-4d19-91a8-1ca379a08307"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("79d97599-cb8b-46af-9860-2e2d16a7225e"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("82c7a6fe-d610-41fc-9a2a-8769d042d7d6"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("8d7989d1-4cfd-49d9-b63a-28a747dbc3b5"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("97ba4214-ebd1-445c-99e4-7e85d94445a8"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("9cdd59cd-ce35-4536-a94d-f4da3338e585"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a50a425a-d8c9-4371-914e-30198735afb6"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("dc409bb1-72ba-4d0c-a433-0ead03d0e174"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("eadc6a69-7568-4be9-9f0f-0d6362905606"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ed48fac1-1a56-4a2c-8caf-4fc76bbaeb9d"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ee806d31-48bf-4f1a-b789-8c679ef7ee4e"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ef53d66d-3209-4440-99a1-9b5c064d28b2"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("fa0907ce-e090-4b46-af6d-875b344f1fb5"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("fd8c7064-827b-49a7-9473-16a693f04cef"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("fe10d5a9-d591-4253-b8f2-af77f46608b1"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("223216aa-3112-49b0-ba84-da3540fc7ef4"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("23ffec56-ceef-4d03-8e93-7868a71f4543"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("30098163-2a41-4eb0-b7f2-b73646f1c409"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("5128b7c7-6b78-45cf-8683-18df673ed7c2"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("54aea0ac-584f-4304-b7d4-0b9bcf379069"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("62006614-9e13-40bb-a012-0c554ca5cb19"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("742d3968-0325-406c-85ac-05ef5d9387ff"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("78aa9333-0766-43b0-a6ca-8b90b7c55c30"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("7eef2d72-5de5-4576-9b1b-0b31a73bf90e"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("8a28219d-781a-464a-a93e-42f064ee977e"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("8be3552f-be14-4bc8-9841-c8cdde117ece"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("acad6903-dde3-493b-8cfe-88cf845ce940"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("c14a43c6-9bb3-4eec-a8c6-bfe810c3110b"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d3395e49-a1ce-4e08-b9c6-ed4e11b981e6"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("145c5a87-edf7-49b9-95fb-6e8fe6cc273c"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("1afbd511-367d-4fb2-9484-480bc8964d39"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("1b50f94d-6a57-410d-b073-e3ae9830231e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("289c6d69-d520-4201-90a7-f25ba4e6148f"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("6933f07f-28da-47f8-aa70-a52bca18686b"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("8bebe810-51ac-41df-9b80-cb17826f1e6c"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("a0a40440-5b2e-47b3-a977-bb1f6eba4142"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("acfaa9d9-e593-4fe9-aa1d-84020028248f"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("d64628f6-c6b0-4a81-a97a-9e720fcb2c69"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("df778742-2a0d-4029-b257-652bb7c60684"));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnings",
                table: "UserProfiles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ResponseRate",
                table: "UserProfiles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "OnTimeDeliveryRate",
                table: "UserProfiles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "CompletionRate",
                table: "UserProfiles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserLanguages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "UserLanguages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "SkillsScore",
                table: "TalentRecommendations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "RecommendedUserId",
                table: "TalentRecommendations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProfileScore",
                table: "TalentRecommendations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "MatchScore",
                table: "TalentRecommendations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExperienceScore",
                table: "TalentRecommendations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "RecommendedUserId1",
                table: "TalentRecommendations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "SkillsAssessmentScore",
                table: "StudentProfiles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReadinessScore",
                table: "StudentProfiles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProgressPercentage",
                table: "StudentProfiles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "StudentProfiles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedCoachId",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "StudentProfiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "MatchScore",
                table: "SkillMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "FreelancerId",
                table: "SkillMatches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "FreelancerId1",
                table: "SkillMatches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "ScoringRules",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinValue",
                table: "ScoringRules",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxValue",
                table: "ScoringRules",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultScore",
                table: "ScoringRules",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "ScoringCriterias",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "ScoringCriterias",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConfidenceScore",
                table: "Recommendations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Recommendations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "SkillScore",
                table: "ProjectMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReputationScore",
                table: "ProjectMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "OverallScore",
                table: "ProjectMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "LocationScore",
                table: "ProjectMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "FreelancerId",
                table: "ProjectMatches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExperienceScore",
                table: "ProjectMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "BudgetScore",
                table: "ProjectMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailabilityScore",
                table: "ProjectMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "FreelancerId1",
                table: "ProjectMatches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PostShares",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "PostShares",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PostLikes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "PostLikes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "PostComments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId1",
                table: "PostComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PostBookmarks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "PostBookmarks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "LessonProgresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "LessonProgresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "LearningPaths",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "LearningPaths",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "InstructorId1",
                table: "LearningPaths",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "MatchScore",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "BudgetMin",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "BudgetMax",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId1",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "SkillScore",
                table: "JobMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "SalaryScore",
                table: "JobMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "OverallScore",
                table: "JobMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "LocationScore",
                table: "JobMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "FreelancerId",
                table: "JobMatches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExperienceScore",
                table: "JobMatches",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "FreelancerId1",
                table: "JobMatches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantId",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId1",
                table: "JobApplications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HeadHunters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "SuccessRate",
                table: "HeadHunters",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionRate",
                table: "HeadHunters",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageResponseTimeHours",
                table: "HeadHunters",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "HeadHunters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnings",
                table: "Guilds",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinProfileScore",
                table: "Guilds",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "LeaderId",
                table: "Guilds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "Guilds",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "LeaderId1",
                table: "Guilds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Earnings",
                table: "GuildProjects",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GuildMembers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "GuildMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FreelancerSubscriptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "FreelancerSubscriptions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "FreelancerSubscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnings",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "SkillScore",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ResponsivenessScore",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ResponseRate",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReliabilityScore",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "QualityScore",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectSuccessScore",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "OverallScore",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "OnTimeDeliveryRate",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "FreelancerId",
                table: "FreelancerScores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "EarningsScore",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "CompletionRateScore",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommunicationScore",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ClientSatisfactionScore",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "FreelancerScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "FreelancerId1",
                table: "FreelancerScores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "ForumThreads",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId1",
                table: "ForumThreads",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "ForumReplies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId1",
                table: "ForumReplies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Courses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "InstructorId1",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "CourseReviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "HelpfulnessScore",
                table: "CourseReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "CourseReviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "CourseEnrollments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePaid",
                table: "CourseEnrollments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "CourseEnrollments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ConnectUsages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "ConnectUsages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Connects",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ConnectPurchases",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPaid",
                table: "ConnectPurchases",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "ConnectPurchases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ConnectEarnings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "ConnectEarnings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "MinRating",
                table: "ConnectEarningRules",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "CompanyReviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "AgreeCount",
                table: "CompanyReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId1",
                table: "CompanyReviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "DisagreeCount",
                table: "CompanyReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HelpfulnessScore",
                table: "CompanyReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CompanyMembers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "CompanyMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CompanyFollowers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "CompanyFollowers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Companies",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId1",
                table: "Companies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "SentimentScore",
                table: "CommunityPosts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "CommunityPosts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId1",
                table: "CommunityPosts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CoachProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "CoachProfiles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "CoachProfiles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "CoachProfiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "CoachingSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "CoachingSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CoachId1",
                table: "CoachingSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "CoachingSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperature",
                table: "AIModelConfigs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinConfidenceScore",
                table: "AIModelConfigs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "MatchThreshold",
                table: "AIModelConfigs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AILogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConfidenceScore",
                table: "AILogs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "AILogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DisplayOrder", "FlagUrl", "IsActive", "IsDeleted", "Iso2", "Iso3", "Name", "NameEn", "PhoneCode", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2495d775-e657-4ee7-8395-f99088610b98"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8885), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 6, "🇧🇭", true, false, "BH", "BHR", "البحرين", "Bahrain", "+973", null, null },
                    { new Guid("32f3b258-4bfb-41e5-8c5d-26b49dd1ec83"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8876), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 4, "🇰🇼", true, false, "KW", "KWT", "الكويت", "Kuwait", "+965", null, null },
                    { new Guid("3a450c78-9900-4c03-b7c4-b57a5fc86369"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8937), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 13, "🇩🇿", true, false, "DZ", "DZA", "الجزائر", "Algeria", "+213", null, null },
                    { new Guid("5b6561d0-83dc-4db5-b724-ecdd58986ce6"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8927), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 10, "🇾🇪", true, false, "YE", "YEM", "اليمن", "Yemen", "+967", null, null },
                    { new Guid("5bf02776-8720-44fa-bb18-967c50564a72"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8866), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 2, "🇸🇦", true, false, "SA", "SAU", "المملكة العربية السعودية", "Saudi Arabia", "+966", null, null },
                    { new Guid("6030327d-15a0-47f4-9d33-a6d0ab38adf9"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8881), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 5, "🇶🇦", true, false, "QA", "QAT", "قطر", "Qatar", "+974", null, null },
                    { new Guid("6e8f6626-358c-415e-b9fe-2bc64a415f55"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8941), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 14, "🇲🇦", true, false, "MA", "MAR", "المغرب", "Morocco", "+212", null, null },
                    { new Guid("7edb16bf-278d-4a04-a15f-de65a0a37040"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8930), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 11, "🇸🇾", true, false, "SY", "SYR", "سوريا", "Syria", "+963", null, null },
                    { new Guid("83ac73b1-73c9-4e64-abd5-9b519e73775e"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8993), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 20, "🇺🇸", true, false, "US", "USA", "الولايات المتحدة", "United States", "+1", null, null },
                    { new Guid("940752b0-6afc-441b-9364-5ae13e0e1179"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8920), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 8, "🇱🇧", true, false, "LB", "LBN", "لبنان", "Lebanon", "+961", null, null },
                    { new Guid("9d2d4243-d335-43fd-a3c1-dc5c5b6d2363"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8933), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 12, "🇵🇸", true, false, "PS", "PSE", "فلسطين", "Palestine", "+970", null, null },
                    { new Guid("a9f00b1a-5836-4044-835f-58cfc64bc983"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8890), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 7, "🇯🇴", true, false, "JO", "JOR", "الأردن", "Jordan", "+962", null, null },
                    { new Guid("aad7ed0e-d10e-4ca0-91c8-eafc5107576b"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8989), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 19, "🇹🇷", true, false, "TR", "TUR", "تركيا", "Turkey", "+90", null, null },
                    { new Guid("aefb1dd3-6716-4ea1-9187-229657d89b89"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8975), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 16, "🇱🇾", true, false, "LY", "LBY", "ليبيا", "Libya", "+218", null, null },
                    { new Guid("b40fd706-182d-4930-a237-8bbf07ec2ec9"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8923), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 9, "🇮🇶", true, false, "IQ", "IRQ", "العراق", "Iraq", "+964", null, null },
                    { new Guid("b754d726-c444-47b8-b36c-b80b83194f05"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8946), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 15, "🇹🇳", true, false, "TN", "TUN", "تونس", "Tunisia", "+216", null, null },
                    { new Guid("c40e5444-f33b-4a85-b0f9-fafa5fb66ea2"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8872), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 3, "🇦🇪", true, false, "AE", "ARE", "الإمارات العربية المتحدة", "United Arab Emirates", "+971", null, null },
                    { new Guid("d85e5575-b959-4702-8a6e-2c6f99e9a19d"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8823), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1, "🇪🇬", true, false, "EG", "EGY", "مصر", "Egypt", "+20", null, null },
                    { new Guid("dac43949-c374-4194-95a6-3024d5a4ac9e"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8980), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 17, "🇸🇩", true, false, "SD", "SDN", "السودان", "Sudan", "+249", null, null },
                    { new Guid("ed2082ce-f1fc-4b63-a0f7-f11150480bb8"), new DateTime(2026, 5, 2, 23, 18, 20, 340, DateTimeKind.Utc).AddTicks(8985), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 18, "🇴🇲", true, false, "OM", "OMN", "عُمان", "Oman", "+968", null, null }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ExchangeRate", "IsActive", "IsDefault", "IsDeleted", "Name", "Symbol", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("399f11ec-9fb8-411d-82e7-b8fdadbce718"), "QAR", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8964), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Qatari Riyal", "﷼", null, null },
                    { new Guid("4c807d8c-bb4e-4d8b-a306-e4dec6d75630"), "KWD", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8958), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Kuwaiti Dinar", "د.ك", null, null },
                    { new Guid("5142bf39-d0b8-4e42-955b-88d5146c0241"), "GBP", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8905), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "British Pound", "£", null, null },
                    { new Guid("6eae861b-093d-4e02-bd1b-fb0171ea5ae9"), "EUR", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8909), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Euro", "€", null, null },
                    { new Guid("805117c6-76cd-4b81-9d8d-37bcdd201c49"), "AUD", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8994), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Australian Dollar", "A$", null, null },
                    { new Guid("92101ff9-fbb9-41e3-8deb-f5f0760e7c77"), "USD", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8833), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, true, false, "US Dollar", "$", null, null },
                    { new Guid("9af26c3e-2a4a-4d52-8765-076460b36888"), "EGP", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8873), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Egyptian Pound", "E£", null, null },
                    { new Guid("a9855ac0-9078-46a0-b1a9-4b98aff5ef26"), "AED", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8950), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "UAE Dirham", "د.إ", null, null },
                    { new Guid("ad866a67-d3cf-4469-8a31-70a9489f6db3"), "JOD", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8985), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Jordanian Dinar", "د.أ", null, null },
                    { new Guid("ae6df5fe-61a9-4e98-a9a4-f3e7be463ecc"), "OMR", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8974), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Omani Rial", "﷼", null, null },
                    { new Guid("cf769eb3-5422-49dc-b212-3b9063210451"), "CAD", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8990), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Canadian Dollar", "C$", null, null },
                    { new Guid("d4585a5d-8b33-47ba-9feb-9d7b082fbb98"), "INR", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8999), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Indian Rupee", "₹", null, null },
                    { new Guid("d76fc049-09ae-4d04-934f-3020aa1647b4"), "SAR", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8934), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Saudi Riyal", "﷼", null, null },
                    { new Guid("fab25306-d727-491d-bb5a-5937a93f32ac"), "BHD", new DateTime(2026, 5, 2, 23, 18, 20, 341, DateTimeKind.Utc).AddTicks(8969), new Guid("00000000-0000-0000-0000-000000000000"), null, null, 1m, true, false, false, "Bahraini Dinar", ".د.ب", null, null }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "DisplayOrder", "IsActive", "IsDeleted", "IsVerified", "Name", "NameEn", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("001947ba-50ba-4ad8-88e0-cc38e9551562"), new DateTime(2026, 5, 2, 23, 18, 20, 380, DateTimeKind.Utc).AddTicks(3223), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 1, true, false, true, "البرمجة", "Programming", null, null },
                    { new Guid("0eda0592-b68c-4c4f-8cc0-2e8fc19ac6c5"), new DateTime(2026, 5, 2, 23, 18, 20, 380, DateTimeKind.Utc).AddTicks(3240), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 4, true, false, true, "الذكاء الاصطناعي", "AI & Machine Learning", null, null },
                    { new Guid("121c3c3f-5170-4fe9-8300-84a9cb7ba853"), new DateTime(2026, 5, 2, 23, 18, 20, 380, DateTimeKind.Utc).AddTicks(3285), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 8, true, false, true, "الكتابة التقنية", "Technical Writing", null, null },
                    { new Guid("31f08afa-8374-4e93-b753-0fd9bbe9f2a4"), new DateTime(2026, 5, 2, 23, 18, 20, 380, DateTimeKind.Utc).AddTicks(3237), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 3, true, false, true, "تطبيقات الهاتف", "Mobile Development", null, null },
                    { new Guid("4ed1884c-0dd4-4019-8e53-2057738e93dd"), new DateTime(2026, 5, 2, 23, 18, 20, 380, DateTimeKind.Utc).AddTicks(3317), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 10, true, false, true, "التسويق الإلكتروني", "Digital Marketing", null, null },
                    { new Guid("93ca1876-ba68-445e-852a-da9eb89f341c"), new DateTime(2026, 5, 2, 23, 18, 20, 380, DateTimeKind.Utc).AddTicks(3287), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 9, true, false, true, "إدارة المشاريع", "Project Management", null, null },
                    { new Guid("a1ca5da2-81c8-4ac8-81df-1955d2289192"), new DateTime(2026, 5, 2, 23, 18, 20, 380, DateTimeKind.Utc).AddTicks(3234), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 2, true, false, true, "تطوير الويب", "Web Development", null, null },
                    { new Guid("e16a2d67-01bd-4fe7-a620-dc2d2ae0d3cd"), new DateTime(2026, 5, 2, 23, 18, 20, 380, DateTimeKind.Utc).AddTicks(3282), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 7, true, false, true, "UI/UX التصميم", "UI/UX Design", null, null },
                    { new Guid("f1c23d5f-8d4d-4a39-89b9-980aecf7985d"), new DateTime(2026, 5, 2, 23, 18, 20, 380, DateTimeKind.Utc).AddTicks(3276), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 5, true, false, true, "قواعد البيانات", "Database", null, null },
                    { new Guid("f64218b6-fcbf-44c1-a488-e6b4be0c0841"), new DateTime(2026, 5, 2, 23, 18, 20, 380, DateTimeKind.Utc).AddTicks(3279), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, 6, true, false, true, "تصميم الجرافيك", "Graphic Design", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguages_UserId1",
                table: "UserLanguages",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TalentRecommendations_RecommendedUserId1",
                table: "TalentRecommendations",
                column: "RecommendedUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_UserId1",
                table: "StudentProfiles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_SkillMatches_FreelancerId1",
                table: "SkillMatches",
                column: "FreelancerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_UserId1",
                table: "Recommendations",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMatches_FreelancerId1",
                table: "ProjectMatches",
                column: "FreelancerId1");

            migrationBuilder.CreateIndex(
                name: "IX_PostShares_UserId1",
                table: "PostShares",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_UserId1",
                table: "PostLikes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_AuthorId1",
                table: "PostComments",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_PostBookmarks_UserId1",
                table: "PostBookmarks",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_LessonProgresses_StudentId1",
                table: "LessonProgresses",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPaths_InstructorId1",
                table: "LearningPaths",
                column: "InstructorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_OwnerId1",
                table: "Jobs",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_JobMatches_FreelancerId1",
                table: "JobMatches",
                column: "FreelancerId1");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ApplicantId1",
                table: "JobApplications",
                column: "ApplicantId1");

            migrationBuilder.CreateIndex(
                name: "IX_HeadHunters_UserId1",
                table: "HeadHunters",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_LeaderId1",
                table: "Guilds",
                column: "LeaderId1");

            migrationBuilder.CreateIndex(
                name: "IX_GuildMembers_UserId1",
                table: "GuildMembers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerSubscriptions_UserId1",
                table: "FreelancerSubscriptions",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerScores_FreelancerId1",
                table: "FreelancerScores",
                column: "FreelancerId1");

            migrationBuilder.CreateIndex(
                name: "IX_ForumThreads_AuthorId1",
                table: "ForumThreads",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_ForumReplies_AuthorId1",
                table: "ForumReplies",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId1",
                table: "Courses",
                column: "InstructorId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_StudentId1",
                table: "CourseReviews",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_StudentId1",
                table: "CourseEnrollments",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectUsages_UserId1",
                table: "ConnectUsages",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectPurchases_UserId1",
                table: "ConnectPurchases",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectEarnings_UserId1",
                table: "ConnectEarnings",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReviews_AuthorId1",
                table: "CompanyReviews",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMembers_UserId1",
                table: "CompanyMembers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFollowers_UserId1",
                table: "CompanyFollowers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_OwnerId1",
                table: "Companies",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPosts_AuthorId1",
                table: "CommunityPosts",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_CoachProfiles_UserId1",
                table: "CoachProfiles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CoachingSessions_CoachId1",
                table: "CoachingSessions",
                column: "CoachId1");

            migrationBuilder.CreateIndex(
                name: "IX_CoachingSessions_StudentId1",
                table: "CoachingSessions",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_AILogs_UserId1",
                table: "AILogs",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AILogs_Users_UserId1",
                table: "AILogs",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingSessions_Users_CoachId1",
                table: "CoachingSessions",
                column: "CoachId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingSessions_Users_StudentId1",
                table: "CoachingSessions",
                column: "StudentId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachProfiles_Users_UserId1",
                table: "CoachProfiles",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPosts_Users_AuthorId1",
                table: "CommunityPosts",
                column: "AuthorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_OwnerId1",
                table: "Companies",
                column: "OwnerId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyFollowers_Users_UserId1",
                table: "CompanyFollowers",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyMembers_Users_UserId1",
                table: "CompanyMembers",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyReviews_Users_AuthorId1",
                table: "CompanyReviews",
                column: "AuthorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectEarnings_Users_UserId1",
                table: "ConnectEarnings",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectPurchases_Users_UserId1",
                table: "ConnectPurchases",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectUsages_Users_UserId1",
                table: "ConnectUsages",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollments_Users_StudentId1",
                table: "CourseEnrollments",
                column: "StudentId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviews_Users_StudentId1",
                table: "CourseReviews",
                column: "StudentId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_InstructorId1",
                table: "Courses",
                column: "InstructorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumReplies_Users_AuthorId1",
                table: "ForumReplies",
                column: "AuthorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumThreads_Users_AuthorId1",
                table: "ForumThreads",
                column: "AuthorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerScores_Users_FreelancerId1",
                table: "FreelancerScores",
                column: "FreelancerId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerSubscriptions_Users_UserId1",
                table: "FreelancerSubscriptions",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GuildMembers_Users_UserId1",
                table: "GuildMembers",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_Users_LeaderId1",
                table: "Guilds",
                column: "LeaderId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HeadHunters_Users_UserId1",
                table: "HeadHunters",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Users_ApplicantId1",
                table: "JobApplications",
                column: "ApplicantId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobMatches_Users_FreelancerId1",
                table: "JobMatches",
                column: "FreelancerId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_OwnerId1",
                table: "Jobs",
                column: "OwnerId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LearningPaths_Users_InstructorId1",
                table: "LearningPaths",
                column: "InstructorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgresses_Users_StudentId1",
                table: "LessonProgresses",
                column: "StudentId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostBookmarks_Users_UserId1",
                table: "PostBookmarks",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Users_AuthorId1",
                table: "PostComments",
                column: "AuthorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Users_UserId1",
                table: "PostLikes",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostShares_Users_UserId1",
                table: "PostShares",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMatches_Users_FreelancerId1",
                table: "ProjectMatches",
                column: "FreelancerId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Users_UserId1",
                table: "Recommendations",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillMatches_Users_FreelancerId1",
                table: "SkillMatches",
                column: "FreelancerId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_Users_UserId1",
                table: "StudentProfiles",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TalentRecommendations_Users_RecommendedUserId1",
                table: "TalentRecommendations",
                column: "RecommendedUserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguages_Users_UserId1",
                table: "UserLanguages",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
