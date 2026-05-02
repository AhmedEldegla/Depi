using DEPI.Application.Repositories.Community;
using DEPI.Domain.Entities.Community;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class CommunityPostRepository : Repository<CommunityPost>, ICommunityPostRepository
{
    public CommunityPostRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<CommunityPost>> GetPublishedPostsAsync()
    {
        return await _dbSet.Where(p => p.Status == PostStatus.Published).OrderByDescending(p => p.CreatedAt).ToListAsync();
    }

    public async Task<List<CommunityPost>> GetByAuthorIdAsync(string authorId)
    {
        return await _dbSet.Where(p => p.AuthorId == authorId).ToListAsync();
    }

    public async Task<List<CommunityPost>> SearchPostsAsync(string searchTerm)
    {
        return await _dbSet.Where(p => p.Title.Contains(searchTerm) || p.Content.Contains(searchTerm)).ToListAsync();
    }

    public async Task<List<CommunityPost>> GetFeaturedPostsAsync(int count)
    {
        return await _dbSet.Where(p => p.IsFeatured).OrderByDescending(p => p.CreatedAt).Take(count).ToListAsync();
    }

    public async Task<List<CommunityPost>> GetByCategoryAsync(string category, int count)
    {
        return await _dbSet.Where(p => p.Category == category).OrderByDescending(p => p.CreatedAt).Take(count).ToListAsync();
    }
}

public class PostCommentRepository : Repository<PostComment>, IPostCommentRepository
{
    public PostCommentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<PostComment>> GetByPostIdAsync(Guid postId)
    {
        return await _dbSet.Where(c => c.PostId == postId).OrderBy(c => c.CreatedAt).ToListAsync();
    }

    public async Task<List<PostComment>> GetByAuthorIdAsync(string authorId)
    {
        return await _dbSet.Where(c => c.AuthorId == authorId).ToListAsync();
    }

    public async Task<int> GetCommentCountAsync(Guid postId)
    {
        return await _dbSet.CountAsync(c => c.PostId == postId);
    }
}

public class PostLikeRepository : Repository<PostLike>, IPostLikeRepository
{
    public PostLikeRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> IsLikedAsync(Guid postId, string userId)
    {
        return await _dbSet.AnyAsync(l => l.PostId == postId && l.UserId == userId);
    }

    public async Task<int> GetLikeCountAsync(Guid postId)
    {
        return await _dbSet.CountAsync(l => l.PostId == postId);
    }
}

public class PostBookmarkRepository : Repository<PostBookmark>, IPostBookmarkRepository
{
    public PostBookmarkRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<PostBookmark>> GetByUserIdAsync(string userId)
    {
        return await _dbSet.Where(b => b.UserId == userId).ToListAsync();
    }

    public async Task<bool> IsBookmarkedAsync(Guid postId, string userId)
    {
        return await _dbSet.AnyAsync(b => b.PostId == postId && b.UserId == userId);
    }
}

public class ForumThreadRepository : Repository<ForumThread>, IForumThreadRepository
{
    public ForumThreadRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<ForumThread>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _dbSet.Where(t => t.CategoryId == categoryId).OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<List<ForumThread>> GetByAuthorIdAsync(string authorId)
    {
        return await _dbSet.Where(t => t.AuthorId == authorId).ToListAsync();
    }

    public async Task<List<ForumThread>> SearchThreadsAsync(string searchTerm)
    {
        return await _dbSet.Where(t => t.Title.Contains(searchTerm)).ToListAsync();
    }

    public async Task<List<ForumThread>> GetUnansweredThreadsAsync(int count)
    {
        return await _dbSet.Where(t => t.RepliesCount == 0).OrderByDescending(t => t.CreatedAt).Take(count).ToListAsync();
    }
}

public class ForumCategoryRepository : Repository<ForumCategory>, IForumCategoryRepository
{
    public ForumCategoryRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<ForumCategory>> GetActiveCategoriesAsync()
    {
        return await _dbSet.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder).ToListAsync();
    }
}

public class ForumReplyRepository : Repository<ForumReply>, IForumReplyRepository
{
    public ForumReplyRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<ForumReply>> GetByThreadIdAsync(Guid threadId)
    {
        return await _dbSet.Where(r => r.ThreadId == threadId).OrderBy(r => r.CreatedAt).ToListAsync();
    }

    public async Task<List<ForumReply>> GetAcceptedAnswersAsync(Guid threadId)
    {
        return await _dbSet.Where(r => r.ThreadId == threadId && r.IsAcceptedAnswer).ToListAsync();
    }
}
