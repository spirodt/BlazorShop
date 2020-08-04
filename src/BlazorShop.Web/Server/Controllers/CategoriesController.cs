﻿namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.Categories;
    using Models.Products;
    using Services.Categories;

    using static Common.Constants;

    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
            => this.categoriesService = categoriesService;

        [HttpGet]
        public async Task<IEnumerable<CategoriesListingResponseModel>> All()
            => await this.categoriesService.AllAsync();

        [HttpGet(Id)]
        public async Task<IEnumerable<ProductsListingResponseModel>> Details(int id)
            => await this.categoriesService.DetailsAsync(id);

        [HttpPost]
        [Authorize(Roles = AdministratorRole)]
        public async Task<ActionResult> Create(CategoriesRequestModel model)
        {
            var id = await this.categoriesService.CreateAsync(model.Name);

            return Created(nameof(this.Create), id);
        }

        [HttpPut(Id)]
        [Authorize(Roles = AdministratorRole)]
        public async Task<ActionResult> Update(int id, CategoriesRequestModel model)
            => await this.categoriesService
                .UpdateAsync(id, model.Name)
                .ToActionResult();

        [HttpDelete(Id)]
        [Authorize(Roles = AdministratorRole)]
        public async Task<ActionResult> Delete(int id)
            => await this.categoriesService
                .DeleteAsync(id)
                .ToActionResult();
    }
}