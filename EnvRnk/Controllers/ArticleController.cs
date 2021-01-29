using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvRnk.Services.DTOs.Article;
using EnvRnk.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnvRnk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IUploadService uploadService;
        private readonly IRnkUserService rnkUserService;
        private readonly IFileDeleteService fileDeleteService;

        public ArticleController(IArticleService articleService, 
            IUploadService uploadService, 
            IRnkUserService rnkUserService, 
            IFileDeleteService fileDeleteService)
        {
            this.articleService = articleService;
            this.uploadService = uploadService;
            this.rnkUserService = rnkUserService;
            this.fileDeleteService = fileDeleteService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ArticleAddRequest request)
        {
            var rnkUser = await rnkUserService.GetByAspId(request.AspAuthorId);
            if (rnkUser == null)
                return BadRequest("Something went wrong try again later.");

            string coverUrl = request.Cover == null ?
                null :
                await uploadService.UploadImage(request.Cover, request.AspAuthorId);

            await articleService.Create(request, rnkUser.Id, coverUrl);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string mode,
            [FromQuery] string title, 
            [FromQuery] string email, 
            [FromQuery] string aspUserId)
        {
            var rnkUser = aspUserId == null ? null : await rnkUserService.GetByAspId(new Guid(aspUserId));
            var articles = await articleService
                .GetForList(title, email, rnkUser?.Id, mode == "new", mode == "ranking", mode == "trending");
            return Ok(articles);
        }

        [HttpPost("u/{aspUserId}/a/{articleId}/{point}")]
        [Authorize]
        public async Task<IActionResult> PointArticle([FromRoute] string aspUserId, [FromRoute] string articleId, [FromRoute] int point)
        {
            var rnkUser = await rnkUserService.GetByAspId(new Guid(aspUserId));
            if (rnkUser == null)
                return BadRequest("Something went wrong try again later.");

            var plus = point == 1;
            var changedArticle = await articleService.PointArticle(new Guid(articleId), rnkUser.Id, plus);
            if (changedArticle == null)
                return BadRequest("Something went wrong try again later.");

            return Ok(changedArticle);
        }

        [HttpPost("u/{aspUserId}/a/{articleId}/rmvScore")]
        [Authorize]
        public async Task<IActionResult> RemoveScore([FromRoute] string aspUserId, [FromRoute] string articleId)
        {
            var rnkUser = await rnkUserService.GetByAspId(new Guid(aspUserId));
            if (rnkUser == null)
                return BadRequest("Something went wrong try again later.");

            var changedArticle = await articleService.RemoveScore(new Guid(articleId), rnkUser.Id);
            if (changedArticle == null)
                return BadRequest("Something went wrong try again later.");

            return Ok(changedArticle);
        }

        [HttpGet("u/{aspUserId}/a/{articleId}")]
        public async Task<IActionResult> Get(
            [FromRoute] string aspUserId,
            [FromRoute] Guid articleId)
        {
            var rnkUser = aspUserId == "null" ? null : await rnkUserService.GetByAspId(new Guid(aspUserId));
            var article = await articleService
                .GetDetails(articleId, rnkUser?.Id);
            return Ok(article);
        }

        [HttpDelete("u/{aspUserId}/a/{articleId}")]
        [Authorize]
        public async Task<IActionResult> Delete(
            [FromRoute] string aspUserId,
            [FromRoute] Guid articleId)
        {
            if (aspUserId == null)
                return BadRequest();
            var rnkUser = await rnkUserService.GetByAspId(new Guid(aspUserId));

            var article = await articleService.GetArticleDb(articleId, rnkUser.Id);
            if (article.CoverUrl != null)
                fileDeleteService.DeleteFile(article.CoverUrl);

            await articleService.Delete(article);

            return Ok();
        }
    }
}
