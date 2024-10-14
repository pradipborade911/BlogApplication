using BlogApplication.DTOs;
using BlogApplication.Models;
using BlogApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogApplication.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService, ILogger<PostController> logger)
        {
            _postService = postService;
        }

        // GET: PostController
        public async Task<ActionResult> Index()
        {
            try
            {
                var posts = await _postService.GetAllPostsAsync();
                return View(posts);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching the posts.");
                return View();
            }
        }

        // GET: PostController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var post = await _postService.GetPostByIdAsync(id);
                if (post == null)
                {
                    return NotFound();
                }
                return View(post);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching the post details.");
                return View();
            }
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the create page.");
                return View();
            }
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostRequestDTO postRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(postRequestDTO);
                }

                await _postService.CreatePostAsync(postRequestDTO);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the post.");
                return View(postRequestDTO);
            }
        }

        // GET: PostController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var post = await _postService.GetPostRequestDTOByIdAsync(id);
                if (post == null)
                {
                    return NotFound();
                }
                return View(post);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching the post for editing.");
                return View();
            }
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PostRequestDTO postDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(postDto);
                }

                await _postService.UpdatePostAsync(id, postDto);
                return RedirectToAction(nameof(AllPosts));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the post.");
                return View(postDto);
            }
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _postService.DeletePostAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the post.");
                return RedirectToAction(nameof(Index));
            }
        }
        // GET: PostController/AllPosts
        public async Task<IActionResult> AllPosts(
            string searchQuery = "",
            List<long> tagIds = null,
            List<long> userIds = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int pageNumber = 1,
            int pageSize = 1)
        {
            try
            {
                var result = await _postService.GetFilteredPostsAsync(searchQuery, tagIds, userIds, startDate, endDate, pageNumber, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching the posts.");
                return View(new PaginatedPostListDTO()); // Return an empty DTO in case of error
            }
        }



    }
}