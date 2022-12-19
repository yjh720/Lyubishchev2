using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyubishchev.TimePeriodCategories;
using Lyubishchev.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using System.Net;

namespace Lyubishchev.Blazor.Pages
{
    public partial class TimePeriodCategories
    {
        private IReadOnlyList<TimePeriodCategoryDto> TimePeriodCategoryList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        private bool CanCreateTimePeriodCategory { get; set; }
        private bool CanEditTimePeriodCategory { get; set; }
        private bool CanDeleteTimePeriodCategory { get; set; }

        private CreateTimePeriodCategoryDto NewTimePeriodCategory { get; set; }

        private Guid EditingTimePeriodCategoryId { get; set; }
        private UpdateTimePeriodCategoryDto EditingTimePeriodCategory { get; set; }

        private Modal CreateTimePeriodCategoryModal { get; set; }
        private Modal EditTimePeriodCategoryModal { get; set; }

        private Validations CreateValidationsRef { get; set; }
        private Validations EditValidationsRef { get; set; }

        public TimePeriodCategories()
        {
            NewTimePeriodCategory = new CreateTimePeriodCategoryDto();
            EditingTimePeriodCategory = new UpdateTimePeriodCategoryDto();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetTimePeriodCategoriesAsync();
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateTimePeriodCategory = await AuthorizationService
                .IsGrantedAnyAsync(LyubishchevPermissions.TimePeriodCategories.Create);

            CanEditTimePeriodCategory = await AuthorizationService
                .IsGrantedAnyAsync(LyubishchevPermissions.TimePeriodCategories.Edit);

            CanDeleteTimePeriodCategory = await AuthorizationService
                .IsGrantedAnyAsync(LyubishchevPermissions.TimePeriodCategories.Delete);
        }

        private async Task GetTimePeriodCategoriesAsync()
        {
            var result = await TimePeriodCategoryAppService.GetListAsync(
                new GetTimePeriodCategoryListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
                }
                );

            TimePeriodCategoryList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<TimePeriodCategoryDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetTimePeriodCategoriesAsync();

            await InvokeAsync(StateHasChanged);
        }

        private void OpenCreateTimePeriodCategoryModal()
        {
            CreateValidationsRef.ClearAll();

            NewTimePeriodCategory = new CreateTimePeriodCategoryDto();
            CreateTimePeriodCategoryModal.Show();
        }

        private void CloseCreateTimePeriodCategoryModal()
        {
            CreateTimePeriodCategoryModal.Hide();
        }

        private void OpenEditTimePeriodCategoryModal(TimePeriodCategoryDto timePeriodCategory)
        {
            EditValidationsRef.ClearAll();

            EditingTimePeriodCategoryId = timePeriodCategory.Id;
            EditingTimePeriodCategory = ObjectMapper.Map<TimePeriodCategoryDto, UpdateTimePeriodCategoryDto>(timePeriodCategory);
            EditTimePeriodCategoryModal.Show();
        }

        private async Task DeleteTimePeriodCategoryAsync(TimePeriodCategoryDto timePeriodCategory)
        {
            var confirmMessage = L["TimePeriodCategory_DeletionConfirmationMessage", timePeriodCategory.Name];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await TimePeriodCategoryAppService.DeleteAsync(timePeriodCategory.Id);
            await GetTimePeriodCategoriesAsync();
        }

        private void CloseEditTimePeriodCategoryModal()
        {
            EditTimePeriodCategoryModal.Hide();
        }

        private async Task CreateTimePeriodCategoryAsync()
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await TimePeriodCategoryAppService.CreateAsync(NewTimePeriodCategory);
                await GetTimePeriodCategoriesAsync();
                CreateTimePeriodCategoryModal.Hide();
            }
        }

        private async Task UpdateTimePeriodCategoryAsync()
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await TimePeriodCategoryAppService.UpdateAsync(EditingTimePeriodCategoryId, EditingTimePeriodCategory);
                await GetTimePeriodCategoriesAsync();
                EditTimePeriodCategoryModal.Hide();
            }

        }
    }
}
