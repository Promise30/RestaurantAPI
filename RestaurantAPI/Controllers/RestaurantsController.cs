using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public RestaurantsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        [HttpGet(Name = "Restaurants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _dbContext.Restaurants.Include(r => r.Reviews).Select(restaurant => new RestaurantResponse
            {
                RestaurantId = restaurant.RestaurantId,
                RestaurantImageUrl = restaurant.RestaurantImageUrl,
                RestaurantName = restaurant.RestaurantName,
                OverallRating = Math.Round(restaurant.OverallRating, 1),
                ServiceType = restaurant.ServiceType,
                StreetName = restaurant.StreetName
            }).ToListAsync();
            if (restaurants == null || restaurants.Count == 0)
            {
                return NotFound();
            }

            return Ok(restaurants);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{restaurantId:int:min(1)}", Name = "GetRestaurant")]

        public async Task<IActionResult> GetRestaurantById(int restaurantId)
        {
            if (restaurantId < 1)
            {
                return BadRequest("Id value cannot be less than 1");
            }
            var restaurant = await _dbContext.Restaurants
                .Include(r => r.LocalGovernment)
                .ThenInclude(r => r.State!)
                .Include(r => r.Reviews)
                .Include(r => r.MenuTypes)
                    .ThenInclude(mt => mt.MenuItems!)
                .Include(r => r.Delivery)
                .SingleOrDefaultAsync(r => r.RestaurantId == restaurantId);

            if (restaurant == null)
            {
                return NotFound("Restaurant not found.");
            }
            var result = _mapper.Map<DetailedRestaurantResponse>(restaurant);

            return Ok(result);
        }
        [HttpGet("{restaurantId}/MenuItemsImages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMenuItemsImages(int restaurantId)
        {
            if (restaurantId < 1)
            {
                return BadRequest("Id value cannot be less than 1");
            }
            try
            {
                var restaurant = await _dbContext.Restaurants.Include(r => r.MenuTypes).ThenInclude(m => m.MenuItems).SingleOrDefaultAsync(r => r.RestaurantId == restaurantId);
                if (restaurant == null)
                {
                    return NotFound("Restaurant not found");
                }
                var menuItemImages = new List<string>();

                foreach (var menuType in restaurant.MenuTypes)
                {
                    foreach (var menuItem in menuType.MenuItems)
                    {
                        if (!string.IsNullOrEmpty(menuItem.MenuItemImageURL))
                        {
                            menuItemImages.Add(menuItem.MenuItemImageURL);
                        }
                    }
                }
                return Ok(menuItemImages);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong. Try again later");
            }

        }
        [HttpGet("{restaurantId}/MenuTypes")]
        public async Task<IActionResult> GetRestaurantMenus(int restaurantId)
        {
            try
            {
                var restaurant = await _dbContext.Restaurants.Include(mt => mt.MenuTypes).ThenInclude(i => i.MenuItems).SingleOrDefaultAsync(r => r.RestaurantId == restaurantId);
                if (restaurant == null)
                {
                    return NotFound("Restaurant does not exist");
                }
                var menutypes = new List<MenuTypeDTO>();
                foreach (var menutype in restaurant.MenuTypes)
                {
                    var menu = _mapper.Map<MenuTypeDTO>(menutype);
                    menutypes.Add(menu);
                }
                return Ok(menutypes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Sorry went wrong. Try again later");
            }
        }
        [HttpGet("RestaurantNames")]
        public async Task<IActionResult> GetNamesOfRestaurants()
        {
            var names = new List<string>();
            var restaurants = await _dbContext.Restaurants.ToListAsync();
            foreach (var restaurant in restaurants)
            {
                names.Add(restaurant.RestaurantName);
            };
            return Ok(names);
        }



        [HttpGet("SearchRestaurants")]
        public async Task<IActionResult> SearchForRestaurants([FromQuery(Name = "restaurantName")] string restaurantName,
                                                              [FromQuery(Name = "stateId")] int stateId = 0,
                                                              [FromQuery(Name = "localGovernmentId")] int localGovernmentId = 0)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var query = _dbContext.Restaurants.Include(r => r.LocalGovernment).ThenInclude(r => r.State).AsQueryable();
            if (!string.IsNullOrEmpty(restaurantName))
            {
                query = query.Where(r => r.RestaurantName.Contains(restaurantName));
            }
            if (stateId != 0)
            {
                query = query.Where(r => r.LocalGovernment.StateId == stateId);
            }

            if (localGovernmentId != 0)
            {
                query = query.Where(r => r.LocalGovernmentId == localGovernmentId);
            }
            var restaurants = await query.ToListAsync();

            var restaurantResponses = _mapper.Map<List<RestaurantResponse>>(restaurants);

            return Ok(restaurantResponses);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{restaurantId}/Reviews")]
        public async Task<IActionResult> GetRestaurantReview(int restaurantId)
        {
            try
            {
                var restaurant = await _dbContext.Restaurants.Include(r => r.Reviews).SingleOrDefaultAsync(r => r.RestaurantId == restaurantId);
                if (restaurant == null)
                {
                    return NotFound("Restaurant does not exist");
                }
                var reviews = restaurant.Reviews.Select(rw => new ReviewDTO
                {
                    Id = rw.ReviewId,
                    ReviewerName = rw.ReviewerName,
                    Content = rw.Content,
                    UpVoteCount = rw.UpVoteCount,
                    DownVoteCount = rw.DownVoteCount,
                    Rating = rw.Rating,
                    DateCreated = rw.DateCreated
                }).ToList();
                return Ok(reviews);
            }
            catch (Exception)
            {

                return StatusCode(500, "Something went wrong. Please try again later");
            }

        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateRestaurantDetails(RestaurantDTO createRestaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var cuisinesList = createRestaurant.Cuisines?.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                var delivery = _mapper.Map<Delivery>(createRestaurant.Delivery);
                var newRestaurant = new Restaurant
                {
                    RestaurantImageUrl = createRestaurant.RestaurantImageUrl,
                    RestaurantName = createRestaurant.RestaurantName,
                    PhoneNumber = createRestaurant.PhoneNumber,
                    WebsiteUrl = createRestaurant.WebsiteUrl,
                    StreetName = createRestaurant.StreetName,
                    OperationDetails = createRestaurant.OperationDetails,
                    Location = createRestaurant.Location,
                    Delivery = delivery,
                    ServiceType = createRestaurant.ServiceType,
                    Cuisines = string.Join(", ", createRestaurant.Cuisines),
                    LocalGovernmentId = createRestaurant.LocalGovtId,
                };
                await _dbContext.Restaurants.AddAsync(newRestaurant);
                await _dbContext.SaveChangesAsync();
                return CreatedAtRoute("GetRestaurant", new { restaurantId = newRestaurant.RestaurantId }, newRestaurant);

            }
            catch (Exception)
            {

                return StatusCode(500, "Something went wrong.Please try again later.");
            }

        }
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{restaurantId}/AddMenus")]
        public async Task<IActionResult> AddMenus([FromRoute] int restaurantId, [FromBody] List<CreateMenuType> menuTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var restaurant = await _dbContext.Restaurants.Include(mt => mt.MenuTypes).SingleOrDefaultAsync(r => r.RestaurantId == restaurantId);
                if (restaurant == null)
                {
                    return NotFound("Restaurant does not exist.");
                }

                foreach (var menutype in menuTypes)
                {
                    var menu = _mapper.Map<MenuTypeDTO>(menutype);
                    menu.RestaurantId = restaurantId;
                    restaurant.MenuTypes.Add(_mapper.Map<MenuType>(menu));
                }
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong, please try again later");
            }
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{restaurantId}/menuType/{menuTypeId}/AddMenuItem")]
        public async Task<IActionResult> CreateMenuItem([FromRoute] int restaurantId, [FromRoute] int menuTypeId, [FromBody] List<CreateMenuItem> menuItemDTOs)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var restaurant = await _dbContext.Restaurants.Include(r => r.MenuTypes).SingleOrDefaultAsync(r => r.RestaurantId == restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }
            var menuType = await _dbContext.MenuTypes.FirstOrDefaultAsync(mt => mt.MenuTypeId == menuTypeId);
            if (menuType == null)
            {
                return NotFound("Menu type not found.");
            }

            foreach (var menuItemDTO in menuItemDTOs)
            {
                var menuItem = _mapper.Map<MenuItem>(menuItemDTO);
                menuItem.MenuTypeId = menuTypeId;

                menuType.MenuItems.Add(menuItem);
            }


            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{restaurantId}/review")]
        public async Task<IActionResult> AddReview(int restaurantId, ReviewSubmissionDTO userReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newReviewResponse = new ReviewDTO
            {
                Content = userReview.Content,
                DateCreated = userReview.DateCreated,
                Rating = userReview.Rating,
                ReviewerName = userReview.Name
            };

            var review = _mapper.Map<Review>(newReviewResponse);
            review.RestaurantId = restaurantId;


            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();

            var restaurant = await _dbContext.Restaurants.Include(r => r.Reviews).FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);
            restaurant.OverallRating = restaurant.Reviews.Average(r => r.Rating);
            restaurant.TotalReviews = restaurant.Reviews.Count;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("{restaurantId}/reviews/{reviewId}/vote")]
        public async Task<IActionResult> VoteReview([FromRoute] int restaurantId, [FromRoute] int reviewId, bool isUpVote)
        {
            var review = await _dbContext.Reviews.SingleOrDefaultAsync(r => r.ReviewId == reviewId);
            if (review == null)
            {
                return NotFound("Review not found");
            }
            // Check if the user has already voted
            if (isUpVote)
            {
                review.UpVoteCount++;
                review.DownVoteCount = 0;
            }
            else
            {
                review.DownVoteCount++;
                review.UpVoteCount = 0;
            }

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{restaurantId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRestaurant(int restaurantId, [FromBody] RestaurantDTO restaurantDTO)
        {
            if (!ModelState.IsValid || restaurantId < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
                if (restaurant == null)
                {
                    return NotFound("Restaurant not found.");
                }

                var updatedRestaurant = _mapper.Map(restaurantDTO, restaurant);
                updatedRestaurant.RestaurantId = restaurantId;




                _dbContext.Restaurants.Update(updatedRestaurant);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {

                return StatusCode(500, "Something went wrong. Please try again later."); ;
            }
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{restaurantId}/UpdateMenu")]
        public async Task<IActionResult> UpdateRestaurantMenus(int restaurantId, [FromBody] List<CreateMenuType> updateMenuTypes)
        {
            if (!ModelState.IsValid || restaurantId < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);
                if (restaurant == null)
                {
                    return NotFound("Restaurant not found.");
                }
                _mapper.Map<List<MenuType>>(updateMenuTypes);

                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {

                return StatusCode(500, "Something went wrong. Please try again later."); ;
            }
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{restaurantId}/UpdateMenuItems")]
        public async Task<IActionResult> UpdateRestaurantMenuItems(int restaurantId, [FromBody] List<CreateMenuItem> updateMenuItems)
        {
            if (!ModelState.IsValid || restaurantId < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);
                if (restaurant == null)
                {
                    return NotFound("Restaurant not found.");
                }
                _mapper.Map<List<MenuItem>>(updateMenuItems);

                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {

                return StatusCode(500, "Something went wrong. Please try again later."); ;
            }
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{restaurantId}")]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId)
        {
            if (restaurantId < 1)
            {
                return BadRequest();
            }
            try
            {
                var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
                if (restaurant == null)
                {
                    return BadRequest("Submitted data is invalid");
                }
                _dbContext.Restaurants.Remove(restaurant);
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {

                return StatusCode(500, "Something went wrong. Please try again later.");
            }

        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{restaurantId}/Menus/{menuId}")]
        public async Task<IActionResult> DeleteRestaurantMenu(int restaurantId, int menuId)
        {
            try
            {
                var restaurant = await _dbContext.Restaurants.Include(r => r.MenuTypes).SingleOrDefaultAsync(r => r.RestaurantId == restaurantId);
                if (restaurant == null)
                {
                    return NotFound("Restaurant not found.");
                }
                var menuType = restaurant.MenuTypes.SingleOrDefault(mt => mt.MenuTypeId == menuId);
                if (menuType == null)
                {
                    return NotFound("MenuType specified not found.");
                }

                // Remove the menutype
                restaurant.MenuTypes.Remove(menuType);
                await _dbContext.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception)
            {

                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

    }
}
