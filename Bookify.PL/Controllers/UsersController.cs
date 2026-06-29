using Bookify.BLL.Dtos.User;
using Bookify.DAL.Common.Consts;
using Bookify.PL.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Bookify.PL.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetAllAsync();
            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var vm = _mapper.Map<IEnumerable<UserViewModel>>(result.Value);
            return View(vm);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Create()
        {
            var roles = await _userService.GetRolesAsync();

            if (roles.IsFailure)
                return StatusCode((int)roles.Error.StatusCode, roles.Error.Message);

            var viewModel = new UserFormViewModel
            {
                Roles = roles.Value!.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                }).ToList()
            };
            return PartialView("_Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.FullName)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<UserCreateDto>(model);
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _userService.CreateAsync(dto, currentUserId);

            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var viewModel = _mapper.Map<UserViewModel>(result.Value);
            return PartialView("_UserRow", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Edit(string id)
        {
            var userDto = await _userService.GetForEditAsync(id);
            if (userDto.IsFailure)
                return StatusCode((int)userDto.Error.StatusCode, userDto.Error.Message);

            var viewModel = _mapper.Map<UserFormViewModel>(userDto.Value);

            var roles = await _userService.GetRolesAsync();
            viewModel.Roles = roles.Value?.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).ToList();

            return PartialView("_Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.FullName)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<UserUpdateDto>(model);
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _userService.UpdateAsync(dto, currentUserId);

            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var viewModel = _mapper.Map<UserViewModel>(result.Value);
            return PartialView("_UserRow", viewModel);
        }


        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var userDto = await _userService.GetForResetPasswordAsync(id);
            if (userDto.IsFailure)
                return StatusCode((int)userDto.Error.StatusCode, userDto.Error.Message);

            var viewModel = _mapper.Map<ResetPasswordFormViewModel>(userDto.Value);
            return PartialView("_ResetPasswordForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.Password)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<UserResetPasswordDto>(model);
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _userService.ResetPasswordAsync(dto, currentUserId);

            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var viewModel = _mapper.Map<UserViewModel>(result.Value);
            return PartialView("_UserRow", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _userService.ToggleStatusAsync(id, currentUserId);
            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            return Ok(result.Value);
        }

        public async Task<IActionResult> AllowUserName(UserFormViewModel model)
        {
            var isAllowed = await _userService.IsUserNameAvailableAsync(model.UserName, model.Id);
            return Json(isAllowed);
        }

        public async Task<IActionResult> AllowEmail(UserFormViewModel model)
        {
            var isAllowed = await _userService.IsEmailAvailableAsync(model.Email, model.Id);
            return Json(isAllowed);
        }
    }
}
