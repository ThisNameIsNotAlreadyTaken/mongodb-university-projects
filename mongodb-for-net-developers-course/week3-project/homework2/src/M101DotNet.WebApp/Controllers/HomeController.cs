using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using M101DotNet.WebApp.Models;
using M101DotNet.WebApp.Models.Home;
using MongoDB.Driver;

namespace M101DotNet.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var blogContext = new BlogContext();

            var recentPosts = await blogContext.Posts.Find(_ => true).SortByDescending(x => x.CreatedAtUtc).Limit(10).ToListAsync();

            var model = new IndexModel
            {
                RecentPosts = recentPosts
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult NewPost()
        {
            return View(new NewPostModel());
        }

        [HttpPost]
        public async Task<ActionResult> NewPost(NewPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var blogContext = new BlogContext();

            var post = new Post
            {
                Author = User.Identity.Name,
                Content = model.Content,
                Title = model.Title,
                Tags = model.Tags.Split(','),
                CreatedAtUtc = DateTime.UtcNow,
                Comments = new Comment[] {}
            };

            await blogContext.Posts.InsertOneAsync(post);

            return RedirectToAction("Post", new { id = post.Id });
        }

        [HttpGet]
        public async Task<ActionResult> Post(string id)
        {
            var blogContext = new BlogContext();

            var post = await blogContext.Posts.Find(x => x.Id == id).SingleOrDefaultAsync();

            if (post == null)
            {
                return RedirectToAction("Index");
            }

            var model = new PostModel
            {
                Post = post
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Posts(string tag = null)
        {
            var blogContext = new BlogContext();

            var posts = await blogContext.Posts.Find(x => x.Tags.Contains(tag)).SortByDescending(x => x.CreatedAtUtc).ToListAsync();

            if (!posts.Any())
            {
                posts = await blogContext.Posts.Find(_ => true).SortByDescending(x => x.CreatedAtUtc).ToListAsync();
            }

            return View(posts);
        }

        [HttpPost]
        public async Task<ActionResult> NewComment(NewCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", new { id = model.PostId });
            }

            var blogContext = new BlogContext();

            var comment = new Comment
            {
                Author = User.Identity.Name,
                Content = model.Content,
                CreatedAtUtc = DateTime.UtcNow
            };

            await blogContext.Posts.FindOneAndUpdateAsync(x => x.Id == model.PostId, Builders<Post>.Update.Push(x => x.Comments, comment));

            return RedirectToAction("Post", new { id = model.PostId });
        }
    }
}