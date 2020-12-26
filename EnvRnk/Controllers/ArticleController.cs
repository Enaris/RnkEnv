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

        public ArticleController(IArticleService articleService, 
            IUploadService uploadService, 
            IRnkUserService rnkUserService)
        {
            this.articleService = articleService;
            this.uploadService = uploadService;
            this.rnkUserService = rnkUserService;
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
        public async Task<IActionResult> Get(ArticleListRequest request)
        {
            var articles = await articleService.GetForList(request);
            return Ok(articles);
        }
    }
}
