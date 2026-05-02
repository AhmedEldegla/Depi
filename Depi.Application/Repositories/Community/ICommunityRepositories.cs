using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DEPI.Domain.Entities.Community;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Community;

public interface ICommunityPostRepository : IRepository<CommunityPost>
{
    Task<List<CommunityPost>> GetPublishedPostsAsync();
    Task<List<CommunityPost>> GetByAuthorIdAsync(string authorId);
    Task<List<CommunityPost>> SearchPostsAsync(string searchTerm);
    Task<List<CommunityPost>> GetFeaturedPostsAsync(int count);
    Task<List<CommunityPost>> GetByCategoryAsync(string category, int count);
}

public interface IPostCommentRepository : IRepository<PostComment>
{
    Task<List<PostComment>> GetByPostIdAsync(Guid postId);
    Task<List<PostComment>> GetByAuthorIdAsync(string authorId);
    Task<int> GetCommentCountAsync(Guid postId);
}

public interface IPostLikeRepository : IRepository<PostLike>
{
    Task<bool> IsLikedAsync(Guid postId, string userId);
    Task<int> GetLikeCountAsync(Guid postId);
}

public interface IPostBookmarkRepository : IRepository<PostBookmark>
{
    Task<List<PostBookmark>> GetByUserIdAsync(string userId);
    Task<bool> IsBookmarkedAsync(Guid postId, string userId);
}

public interface IForumThreadRepository : IRepository<ForumThread>
{
    Task<List<ForumThread>> GetByCategoryIdAsync(Guid categoryId);
    Task<List<ForumThread>> GetByAuthorIdAsync(string authorId);
    Task<List<ForumThread>> SearchThreadsAsync(string searchTerm);
    Task<List<ForumThread>> GetUnansweredThreadsAsync(int count);
}

public interface IForumCategoryRepository : IRepository<ForumCategory>
{
    Task<List<ForumCategory>> GetActiveCategoriesAsync();
}

public interface IForumReplyRepository : IRepository<ForumReply>
{
    Task<List<ForumReply>> GetByThreadIdAsync(Guid threadId);
    Task<List<ForumReply>> GetAcceptedAnswersAsync(Guid threadId);
}