# Restaurant Management API
This project was built using ASP.NET Core API project template.  
This API can be accessible at: <a>https://restaurants.bsite.net</a>

## Features

- Manage restaurants, menus, orders, and reservations.
- Retrieve detailed information about restaurants.
- Add and update menus and menu items.
- Post and vote on restaurant reviews.

## Endpoints
some of the endpoints in this project include the following: 
### Get Restaurants

**Endpoint:** `GET /api/restaurants`

- Returns a list of available restaurants.

### Get Restaurant by ID

**Endpoint:** `GET /api/restaurants/{restaurantId}`

- Retrieve details about a specific restaurant by ID.

### Get Menu Items Images

**Endpoint:** `GET /api/restaurants/{restaurantId}/MenuItemsImages`

- Retrieve URLs of menu item images for a restaurant.

### Get Restaurant Menus

**Endpoint:** `GET /api/restaurants/{restaurantId}/MenuTypes`

- Returns a list of restaurant menu types.

### Get Names of Restaurants

**Endpoint:** `GET /api/restaurants/RestaurantNames`

- Retrieve names of all restaurants.


### Search for Restaurants

**Endpoint:** `GET /api/restaurants/SearchRestaurants`

- Search for restaurants based on various filters.

### Get Restaurant Reviews

**Endpoint:** `GET /api/restaurants/{restaurantId}/Reviews`

- Retrieve reviews for a specific restaurant.

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- Basic knowledge of ASP.NET and C#
- Familiarity with HTTP requests and APIs

### Installation

1. Clone this repository to your local machine.
2. Navigate to the project directory using the command line.
3. Run the following command to build the project:
 ``dotnet build``
4. Run the following command to start the API:
``dotnet run``
