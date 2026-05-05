using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.Community;
using DEPI.Domain.Entities.Community;
using MediatR;

namespace DEPI.Application.UseCases.Community;

public class CommunityPostResponse { public Guid Id { get; set; } public string Title { get; set; } = string.Empty; public string Content { get; set; } = string.Empty; public string ImageUrl { get; set; } = string.Empty; public PostType Type { get; set; } public PostStatus Status { get; set; } public int ViewsCount { get; set; } public int LikesCount { get; set; } public int CommentsCount { get; set; } public int SharesCount { get; set; } public bool IsPinned { get; set; } public bool IsFeatured { get; set; } public string Category { get; set; } = string.Empty; public string Tags { get; set; } = string.Empty; public DateTime CreatedAt { get; set; } }
public class ForumThreadResponse { public Guid Id { get; set; } public string Title { get; set; } = string.Empty; public string Content { get; set; } = string.Empty; public Guid CategoryId { get; set; } public string CategoryName { get; set; } = string.Empty; public ThreadStatus Status { get; set; } public int ViewsCount { get; set; } public int RepliesCount { get; set; } public bool IsPinned { get; set; } public bool IsSolved { get; set; } public DateTime CreatedAt { get; set; } }

public record CreatePostRequest(string Title, string Content, string? ImageUrl, PostType Type, string? Category, string? Tags);
public record CreateForumThreadRequest(string Title, string Content, Guid CategoryId);
public record CreateForumReplyRequest(Guid ThreadId, string Content);

public record CreatePostCommand(Guid AuthorId, CreatePostRequest Request) : IRequest<CommunityPostResponse>;
public record GetPostsQuery(bool? Featured, string? Category, string? SearchTerm, int Page, int PageSize) : IRequest<List<CommunityPostResponse>>;
public record GetForumThreadsQuery(Guid? CategoryId, string? SearchTerm, int Page, int PageSize) : IRequest<List<ForumThreadResponse>>;
public record CreateForumThreadCommand(Guid AuthorId, CreateForumThreadRequest Request) : IRequest<ForumThreadResponse>;
public record CreateForumReplyCommand(Guid AuthorId, CreateForumReplyRequest Request) : IRequest<Guid>;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CommunityPostResponse>
{
    private readonly ICommunityPostRepository _repo;
    private readonly IMapper _mapper;
    public CreatePostCommandHandler(ICommunityPostRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

    public async Task<CommunityPostResponse> Handle(CreatePostCommand r, CancellationToken ct)
    {
        var post = new CommunityPost { AuthorId = r.AuthorId, Title = r.Request.Title, Content = r.Request.Content, ImageUrl = r.Request.ImageUrl ?? "", Type = r.Request.Type, Status = PostStatus.Published, Category = r.Request.Category ?? "", Tags = r.Request.Tags ?? "" };
        await _repo.AddAsync(post, ct);
        return _mapper.Map<CommunityPostResponse>(post);
    }
}

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<CommunityPostResponse>>
{
    private readonly ICommunityPostRepository _repo;
    private readonly IMapper _mapper;
    public GetPostsQueryHandler(ICommunityPostRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<CommunityPostResponse>> Handle(GetPostsQuery r, CancellationToken ct)
    {
        List<CommunityPost> posts;
        if (r.Featured == true) posts = await _repo.GetFeaturedPostsAsync(r.PageSize);
        else if (!string.IsNullOrWhiteSpace(r.Category)) posts = await _repo.GetByCategoryAsync(r.Category, r.PageSize);
        else if (!string.IsNullOrWhiteSpace(r.SearchTerm)) posts = await _repo.SearchPostsAsync(r.SearchTerm);
        else posts = await _repo.GetPublishedPostsAsync();
        return _mapper.Map<List<CommunityPostResponse>>(posts);
    }
}

public class GetForumThreadsQueryHandler : IRequestHandler<GetForumThreadsQuery, List<ForumThreadResponse>>
{
    private readonly IForumThreadRepository _repo;
    private readonly IForumCategoryRepository _catRepo;
    private readonly IMapper _mapper;
    public GetForumThreadsQueryHandler(IForumThreadRepository repo, IForumCategoryRepository catRepo, IMapper mapper) { _repo = repo; _catRepo = catRepo; _mapper = mapper; }
    public async Task<List<ForumThreadResponse>> Handle(GetForumThreadsQuery r, CancellationToken ct)
    {
        List<ForumThread> threads;
        if (r.CategoryId.HasValue) threads = await _repo.GetByCategoryIdAsync(r.CategoryId.Value);
        else if (!string.IsNullOrWhiteSpace(r.SearchTerm)) threads = await _repo.SearchThreadsAsync(r.SearchTerm);
        else threads = (await _repo.GetAllAsync(ct)).ToList();
        return _mapper.Map<List<ForumThreadResponse>>(threads);
    }
}

public class CreateForumThreadCommandHandler : IRequestHandler<CreateForumThreadCommand, ForumThreadResponse>
{
    private readonly IForumThreadRepository _repo;
    private readonly IMapper _mapper;
    public CreateForumThreadCommandHandler(IForumThreadRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<ForumThreadResponse> Handle(CreateForumThreadCommand r, CancellationToken ct)
    {
        var thread = new ForumThread { AuthorId = r.AuthorId, Title = r.Request.Title, Content = r.Request.Content, CategoryId = r.Request.CategoryId, Status = ThreadStatus.Open };
        await _repo.AddAsync(thread, ct);
        return _mapper.Map<ForumThreadResponse>(thread);
    }
}

public class CreateForumReplyCommandHandler : IRequestHandler<CreateForumReplyCommand, Guid>
{
    private readonly IForumReplyRepository _repo;
    public CreateForumReplyCommandHandler(IForumReplyRepository repo) => _repo = repo;
    public async Task<Guid> Handle(CreateForumReplyCommand r, CancellationToken ct)
    {
        var reply = new ForumReply { ThreadId = r.Request.ThreadId, AuthorId = r.AuthorId, Content = r.Request.Content };
        await _repo.AddAsync(reply, ct);
        return reply.Id;
    }
}
